using System;
using System.Drawing;
using ScreenApi.ScreenManager;
using ScreenApi.Utilities;
using Serilog;

namespace ScreenApi
{
    /// <summary>
    /// Role is to integrate
    /// 1) Finding image
    /// 2) Getting location
    /// 3) Clicking.
    /// 4) Return _____?
    /// </summary>
    public class ScreenClickerApi
    {
        private IntPtr _hwnd = HwndManager.GetHwndNumber();
        private ILogger _log;
        private OpenCVManager _openCv;
        public ScreenClickerApi(ILogger logger)
        {
            _log = logger.ForContext<ScreenClickerApi>();
            _openCv = new OpenCVManager();
            
        }

        /// <summary>
        /// Using App settings Hwnd 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public bool FindAndClick(Bitmap src, Bitmap template)
        {
            _log.Information("Starting FindAndClick");
            var results = false;
            var foundImgPoint = _openCv.MatchImageWithCoordinates(src, template);
            if (foundImgPoint != null) {
                ClickerHelper.ClickCoordinateRelativeToHwnd(foundImgPoint.Value, _hwnd);
                _log.Information("Found image.");
                results = true;
            }
            else {
                _log.Information("Couldn't find image.");
            }

            _log.Information("Finished FindAndClick");
            return results;
        }

    }
}