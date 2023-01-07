using Autofac;
using Serilog;
using System;
using System.Windows.Forms;
using ScreenApi.ScreenManager;
using System.Configuration;
using System.Drawing;
using Emgu.CV;
using ScreenApi.Utilities;
using StepFunctionEngine;

namespace ScreenApi
{
    static class Program
    {
        private static IContainer Container { get; set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Container = Autofac.GetContainer();
            
            using (var scope = Container.BeginLifetimeScope())
            {
                var logger = scope.Resolve<ILogger>().ForContext("SourceContext", typeof(Program));

                var sg = new ScreenGrabber(logger);
                //sg.FindImage("");


                var runner = new StepFunctionEngineRunner(logger);
                runner.run();
                
                // var hwnd = HwndManager.GetHwndNumber();
                //
                // var window = ScreenGrabber.GetBitmapFromWindow(hwnd);
                // var bmm = new BitmapManager(logger);
                // var image = ImageFileManager.OpenAsBitmap(ConfigurationManager.AppSettings["ImageDirectory"] +
                //                                              "dock.bmp");
                // var ocv = new OpenCVManager();
                // // var tem = CvInvoke.Imread(ConfigurationManager.AppSettings["ImageDirectory"] +
                // //                     "battle.PNG");
                // // var screen = BitmapConverter.ToMat(window);
                // var results = ocv.MatchImageWithCoordinates(window, image);
                //
                //
                // if (results != null) ClickerHelper.ClickCoordinateRelativeToHwnd(results.Value, hwnd);
                // //var template = bmm.OpenBitmapFile() 
                
                // ImageFileManager.SaveImage(ScreenGrabber.GetBitmapFromWindow(hwnd), "main-test");
                //ScreenGrabber.PrintWindow(processes[0].MainWindowHandle);



                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());

                Log.CloseAndFlush();
            }
        }
    }
}
