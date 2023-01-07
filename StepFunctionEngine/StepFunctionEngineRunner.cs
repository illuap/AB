using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using ScreenApi;
using ScreenApi.ScreenManager;
using Serilog;
using StepFunctionEngine.Models;
using StepFunctionEngine.States;
using StepFunctionEngine.Types;

namespace StepFunctionEngine
{
    public class StepFunctionEngineRunner
    {
        private ILogger _log;
        private ScreenClickerApi _screenClickerApi;
        private HwndManager _hwndManager;
        public StepFunctionEngineRunner(ILogger logger)
        {
            _log = logger.ForContext<StepFunctionEngineRunner>();

            _screenClickerApi = new ScreenClickerApi(logger);
        }

        public void run()
        {
            _log.Information("Starting Runner");
            var input = new BasicActionInputModel()
            {
                srcImg = ScreenGrabber.GetBitmapFromWindow(HwndManager.GetHwndNumber()),
                templateImg = ImageFileManager.OpenAsBitmap(ConfigurationManager.AppSettings["ImageDirectory"] +"confirm.bmp")
            };
            var action = new BasicAction(_log, input);
            action.run();
            _log.Information("Finished Runner");
        }
    }
    
}