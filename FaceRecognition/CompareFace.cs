using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using System.Drawing;
using System.IO;
using Emgu.CV.Util;

namespace FaceRecognition
{
    public class CompareFace
    {
        private readonly CascadeClassifier _faceCascade;
        private readonly LBPHFaceRecognizer _faceRecognizer;

        public CompareFace()
        {
            _faceCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");
            _faceRecognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);
        }

        public async Task<int> Compare(string Face1, List<Face> Faces)
        {
            
            byte[] Face1byte = Convert.FromBase64String(Face1);
            string Face1FileName = Guid.NewGuid().ToString();
            try
            {
                Image.FromStream(new MemoryStream(Face1byte)).Save(Face1FileName);

                var ImageFace1 = new Image<Gray, byte>(Face1FileName);

                //var DetectFace1 = _faceCascade.DetectMultiScale(ImageFace1, 1.1, 10, Size.Empty);

                foreach (var Face in Faces)
                {
                    byte[] Face2byte = Convert.FromBase64String(Face.Img);
                    string Face2FileName = Guid.NewGuid().ToString();
                    Image.FromStream(new MemoryStream(Face2byte)).Save(Face2FileName);
                    var ImageFace2 = new Image<Gray, byte>(Face2FileName);

                    //var DetectFace2 = _faceCascade.DetectMultiScale(ImageFace2, 1.1, 10, Size.Empty);

                    //if (DetectFace1.Length == 0 || DetectFace2.Length == 0)
                    //{
                    //    return -1;
                    //}

                    VectorOfMat vectorOfMat = new VectorOfMat();
                    VectorOfInt vectorOfInt = new VectorOfInt();

                    vectorOfMat.Push(ImageFace1);
                    vectorOfInt.Push(new[] {1});

                    _faceRecognizer.Train(vectorOfMat, vectorOfInt);
                    var result = _faceRecognizer.Predict(ImageFace2);

                    if (result.Label == 1 && result.Distance < 50)
                    {
                        return Face.User.Id;
                    }

                }

                return -1;

            }
            catch (Exception)
            {
                return -2;
            }
        }
    }
}
