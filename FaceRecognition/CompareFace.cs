using DataLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using System.Drawing;
using System.IO;
using Emgu.CV.Util;

namespace FaceRecognition
{
    public static class CompareFace
    {
        //private static readonly CascadeClassifier _faceCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");
        //private static readonly LBPHFaceRecognizer _faceRecognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);

        //public CompareFace()
        //{
        //    _faceCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");
        //    _faceRecognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);
        //}

        public static async Task<string> Compare(string Face1, List<Face> Faces)
        {
            //CascadeClassifier _faceCascade = new CascadeClassifier("/Users/Lion/source/repos/BioEntry-WebApi/FaceRecognition/haarcascade_frontalface_default.xml");
            LBPHFaceRecognizer _faceRecognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);
            //EigenFaceRecognizer _faceRecognizer = new EigenFaceRecognizer();
            if (Faces.Count == 0 || Faces == null)
            {
                return "-1";
            }   
            byte[] Face1byte = Convert.FromBase64String(Face1);
            string Face1FileName = Guid.NewGuid().ToString();
            string Face2FileName;
            try
            {
                Image.FromStream(new MemoryStream(Face1byte)).Save(Face1FileName);

                var ImageFace1 = new Image<Gray, byte>(Face1FileName);

                //var DetectFace1 = _faceCascade.DetectMultiScale(ImageFace1, 1.1, 10, Size.Empty);

                foreach (var Face in Faces)
                {
                    byte[] Face2byte = Convert.FromBase64String(Face.Img);
                    Face2FileName = Guid.NewGuid().ToString();
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
                        File.Delete(Face2FileName);
                        return Face.User.Id.ToString();
                    }
                    else
                    {
                        File.Delete(Face2FileName);
                    }

                }

                return "-1";

            }
            catch (Exception e)
            {
                return e.ToString();
            }
            finally
            {
                File.Delete(Face1FileName);
                _faceRecognizer.Dispose();
            }
        }
    }
}
