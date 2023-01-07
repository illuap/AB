using Serilog;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScreenApi.ScreenManager
{
    public class ScreenGrabber : IScreenGrabber
    {
        private ILogger _log { get; set; }
        public ScreenGrabber(ILogger logger) {

            _log = logger.ForContext<ScreenGrabber>(); ;
            _log.Information("WOW");
        }


        public bool FindImage(string path) {
            _log.Debug("Finding image!");
            CaptureMyScreen();
            return true;
        }
        public bool FindImage(Bitmap img) {
            _log.Debug("Finding image!");
            CaptureMyScreen();
            return true;
        }
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        public static Bitmap GetBitmapFromWindow(IntPtr hwnd)
        {
            RECT rc = HwndManager.GetHwndRect(hwnd);

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);

            /*
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                graphics.CopyFromScreen(rc.Left, rc.Top, 0, 0, new Size(rc.Width, ), CopyPixelOperation.SourceCopy);
            }
            */
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();

            PrintWindow(hwnd, hdcBitmap, 2);

            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();

            return bmp;
        }


        private void CaptureMyScreen2()
        {
            try
            {
                //Creating a new Bitmap object
                Bitmap captureBitmap = new Bitmap(1024, 768, PixelFormat.Format32bppArgb);
                //Bitmap captureBitmap = new Bitmap(int width, int height, PixelFormat);
                //Creating a Rectangle object which will
                //capture our Current Screen
                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
                //Creating a New Graphics Object
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                //Copying Image from The Screen
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                //Saving the Image File (I am here Saving it in My E drive).
                captureBitmap.Save(ConfigurationManager.AppSettings["ImageDirectory"]+@"\1.png", ImageFormat.Png);
                //Displaying the Successfull Result
                //MessageBox.Show("Screen Captured");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CaptureMyScreen()
        {
            try
            {
                //Creating a new Bitmap object
                Bitmap captureBitmap = new Bitmap(1024, 768, PixelFormat.Format32bppArgb);
                //Bitmap captureBitmap = new Bitmap(int width, int height, PixelFormat);
                //Creating a Rectangle object which will
                //capture our Current Screen
                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
                //Creating a New Graphics Object
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                //Copying Image from The Screen
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                //Saving the Image File (I am here Saving it in My E drive).
                captureBitmap.Save(ConfigurationManager.AppSettings["ImageDirectory"]+@"\1.png", ImageFormat.Png);
                //Displaying the Successfull Result
                //MessageBox.Show("Screen Captured");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
