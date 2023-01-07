using Serilog;
using System.Drawing;

namespace ScreenApi.ScreenManager
{
    public class BitmapManager
    {
        public static string TEST_IMG_PNG_1 = "G:/SampleImages/test_player_1.png";
        public static string TEST_TEMPLATE_PNG_1 = "G:/SampleImages/test_template_1.png";
        protected ILogger _logger { get; set; }
        public BitmapManager(ILogger logger) {

            _logger = logger.ForContext<BitmapManager>();
        }

        public Bitmap OpenBitmapFile(string path) {
            Bitmap newImage = null;
            using (var image = new Bitmap(path))
            {
                newImage = new Bitmap(image);
            }
            _logger.Debug($"Opened image {path}.");
            return newImage;
        }
        // public void ShowBitmap(Bitmap bitmap) {
        //     _logger.Debug("Showing bitmap window.");
        //     
        //     Form form = new Form();
        //     form.Text = "Image Viewer";
        //     PictureBox pictureBox = new PictureBox();
        //     pictureBox.Image = bitmap;
        //     pictureBox.Dock = DockStyle.Fill;
        //     form.Controls.Add(pictureBox);
        //     MediaTypeNames.Application.Run(form);
        // }
    }
}
