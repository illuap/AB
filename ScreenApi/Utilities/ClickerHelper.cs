using System;
using System.Drawing;
using ScreenApi.ScreenManager;

namespace ScreenApi.Utilities
{
    public class ClickerHelper
    {
        public static void ClickCoordinateRelativeToHwnd(Point pt, IntPtr hwnd)
        {
            var appRect = HwndManager.GetHwndRect(hwnd);
            MouseOperations.SetCursorPosition(pt.X + appRect.Left, pt.Y + appRect.Top);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
        }
    }
}