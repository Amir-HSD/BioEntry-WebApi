using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataLayer;
using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using System.Drawing;
using Emgu.CV.Util;
using System.Drawing.Imaging;
using Emgu.CV.Linemod;

namespace FaceComparator
{
    public class Program
    {
        static void Main(string[] args)
        {
            BioEntryContext ctx = new BioEntryContext();
            IFaceRepo repo = new FaceRepo(ctx);
            LBPHFaceRecognizer _faceRecognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);
            bool Detected = false;
            try
            {
                if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FaceComparator")))
                {
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FaceComparator"));
                }

                byte[] Face1byte = Convert.FromBase64String(args[0]);
                string Face1FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FaceComparator") + Guid.NewGuid().ToString() + ".jpg";
                string Face2FileName;

                try
                {
                    List<Face> Faces = repo.GetAllFace().ToList();
                    Bitmap bitmap1 = new Bitmap(new MemoryStream(Face1byte));
                    bitmap1.Save(Face1FileName, ImageFormat.Jpeg);

                    var ImageFace1 = new Image<Gray, byte>(Face1FileName);

                    foreach (var Face in Faces)
                    {
                        byte[] Face2byte = Convert.FromBase64String(Face.Img);
                        Face2FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FaceComparator") + Guid.NewGuid().ToString();

                        Bitmap bitmap2 = new Bitmap(new MemoryStream(Face2byte));
                        bitmap2.Save(Face2FileName, ImageFormat.Jpeg);

                        var ImageFace2 = new Image<Gray, byte>(Face2FileName);


                        VectorOfMat vectorOfMat = new VectorOfMat();
                        VectorOfInt vectorOfInt = new VectorOfInt();

                        vectorOfMat.Push(ImageFace1);
                        vectorOfInt.Push(new[] { 1 });

                        _faceRecognizer.Train(vectorOfMat, vectorOfInt);
                        var result = _faceRecognizer.Predict(ImageFace2);

                        if (result.Label == 1 && result.Distance < 50)
                        {
                            File.Delete(Face2FileName);
                            Detected = true;
                            Console.WriteLine(Face.User.Id.ToString());
                            //return new Face { Id = Face.Id, Img = Face.Img };
                        }
                        else
                        {
                            File.Delete(Face2FileName);
                        }

                    }

                    if (Detected == false)
                    {
                        Console.WriteLine("-1");
                    }

                    //return null;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    //return null;
                }
                finally
                {
                    File.Delete(Face1FileName);
                    _faceRecognizer.Dispose();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //return null;
            }
            finally
            {
                ctx.Dispose();
                repo.Dispose();
            }
        }
        /*
        static void Main(string[] args)
        {
			BioEntryContext ctx = new BioEntryContext();
            IFaceRepo repo = new FaceRepo(ctx);
            LBPHFaceRecognizer _faceRecognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);
            bool Detected = false;

            try
			{
                if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FaceComparator")))
                {
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FaceComparator"));
                }

                List<Face> Faces = repo.GetAllFace().ToList();
                byte[] Face1byte = Convert.FromBase64String(args[0]);
                string Face1FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FaceComparator") + Guid.NewGuid().ToString() + ".jpg";
                string Face2FileName;

                try
                {
                    Bitmap bitmap1 = new Bitmap(new MemoryStream(Face1byte));
                    bitmap1.Save(Face1FileName, ImageFormat.Jpeg);

                    var ImageFace1 = new Image<Gray, byte>(Face1FileName);

                    foreach (var Face in Faces)
                    {
                        byte[] Face2byte = Convert.FromBase64String(Face.Img);
                        Face2FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FaceComparator") + Guid.NewGuid().ToString();

                        Bitmap bitmap2 = new Bitmap(new MemoryStream(Face2byte));
                        bitmap2.Save(Face2FileName, ImageFormat.Jpeg);

                        var ImageFace2 = new Image<Gray, byte>(Face2FileName);


                        VectorOfMat vectorOfMat = new VectorOfMat();
                        VectorOfInt vectorOfInt = new VectorOfInt();

                        vectorOfMat.Push(ImageFace1);
                        vectorOfInt.Push(new[] { 1 });

                        _faceRecognizer.Train(vectorOfMat, vectorOfInt);
                        var result = _faceRecognizer.Predict(ImageFace2);

                        if (result.Label == 1 && result.Distance < 50)
                        {
                            File.Delete(Face2FileName);
                            Detected =true;
                            Console.WriteLine(Face.User.Id.ToString());
                        }
                        else
                        {
                            File.Delete(Face2FileName);
                        }

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                finally
                {
                    if (!Detected)
                    {
                        Console.WriteLine("-1");
                    }
                    File.Delete(Face1FileName);
                    _faceRecognizer.Dispose();
                }


            }
			catch (Exception e)
			{
                Console.WriteLine(e.ToString());
            }
			finally
			{
				ctx.Dispose();
				repo.Dispose();

			}
        }
        */
    }
}
