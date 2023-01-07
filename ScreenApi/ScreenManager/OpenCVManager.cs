using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Emgu.CV.UI;

namespace ScreenApi.ScreenManager
{
    public class OpenCVManager
    {
        public static double THRESHHOLD = 0.9;
        public bool MatchImage(Bitmap sourceBitmap, Bitmap templateBitmap, Func<bool> func)
        {
            return this.MatchImage(sourceBitmap.ToImage<Bgr, byte>(), templateBitmap.ToImage<Bgr, byte>(), func);
        }

        public bool MatchImage(Mat sourceBitmap, Mat templateBitmap, Func<bool> func)
        {
            return this.MatchImage(sourceBitmap.ToImage<Bgr, byte>(), templateBitmap.ToImage<Bgr, byte>(), func);
        }

        public bool MatchImage(Image<Bgr, byte> source, Image<Bgr, byte> template, Func<bool> func)
        {
            Image<Bgr, byte> imageToShow = source.Copy();

            using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (maxValues[0] > THRESHHOLD)
                {
                    // This is a match. Do something with it, for example draw a rectangle around it.
                    Rectangle match = new Rectangle(maxLocations[0], template.Size);
                    imageToShow.Draw(match, new Bgr(Color.Red), 3);
                    
                    ImageViewer viewer = new ImageViewer(); //create an image viewer
                    Application.Idle += new EventHandler(delegate(object sender, EventArgs e)
                    {  //run this until application closed (close button click on image viewer)
                        viewer.Image = imageToShow; //draw the image obtained from camera
                    });
                    viewer.ShowDialog(); //show the image viewer
                    
                    func();
                }
            }

            //var results = imageToShow.ToBitmap();
            return true;
        }
        public Point? MatchImageWithCoordinates(Bitmap sourceBitmap, Bitmap templateBitmap)
        {
            return this.MatchImageWithCoordinates(sourceBitmap.ToImage<Bgr, byte>(), templateBitmap.ToImage<Bgr, byte>());
        }
        
        public Point? MatchImageWithCoordinates(Image<Bgr, byte> oldsource, Image<Bgr, byte> template)
        {
            Image<Bgr, byte> source = oldsource.Copy();
            Size newScale = new Size(oldsource.Size.Width * 2, oldsource.Size.Height * 2);
            CvInvoke.Resize(oldsource, dst: source, newScale);
            Image<Bgr, byte> imageToShow = source.Copy();

            Point? results = null;
            
            using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;

                var tempimg = result.ToBitmap();
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);
                
                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (maxValues[0] > THRESHHOLD)
                {
                    // This is a match. Do something with it, for example draw a rectangle around it.
                    Rectangle match = new Rectangle(maxLocations[0], template.Size);
                    results = GetCenter(maxLocations[0], template.Size);
                    
                    // DRAW BOX AND DISPLAY
                    imageToShow.Draw(match, new Bgr(Color.Red), 2);
                    ImageViewer viewer = new ImageViewer(); //create an image viewer
                    Application.Idle += new EventHandler(delegate(object sender, EventArgs e)
                    {  //run this until application closed (close button click on image viewer)
                        viewer.Image = imageToShow; //draw the image obtained from camera
                    });
                    viewer.ShowDialog(); //show the image viewer
                }
            }

            //var results = imageToShow.ToBitmap();
            return results;
        }

        private Point GetCenter(Point topleftPoint, Size size)
        {
            var center = topleftPoint;
            center.X = center.X + size.Width/2;
            center.Y = center.Y + size.Height/2;
            return center;
        }
    }
}
