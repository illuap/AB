using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ScreenApi.ScreenManager
{
    public class HwndManager
    {
        public static IntPtr GetHwndNumber() {
            var processName = ConfigurationManager.AppSettings["ApplicationName"];
            Process[] processes = Process.GetProcessesByName(processName);

            if (processes.Length == 0)
                throw new Exception($"Could not find hwnd with application named {processName}.");

            return processes.FirstOrDefault().MainWindowHandle;
        }
        
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        
        public static RECT GetHwndRect(IntPtr? paramHwnd = null)
        {
            IntPtr hwnd = paramHwnd ?? GetHwndNumber();
            RECT rct = new RECT();
            GetWindowRect(hwnd, out rct);

            return rct;
        }
    }
}
