using System.Configuration;
using System.Drawing;

namespace ScreenApi
{
    public class ImageFileManager
    {
        public static Bitmap OpenAsBitmap(string path)
        {
            var image = Image.FromFile(path);
            return (Bitmap)image;
        }

        public static void SaveImage(Bitmap img, string name = "default")
        {
            img.Save(ConfigurationManager.AppSettings["ImageDirectory"]+name+".bmp");
        }
    }
}