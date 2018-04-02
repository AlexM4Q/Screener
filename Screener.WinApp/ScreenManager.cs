using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Screener.WinApp {

    internal class ScreenManager {

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        public static Bitmap PrintWindow(IntPtr window) {
            GetWindowRect(window, out var rect);

            var bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            var g = Graphics.FromImage(bitmap);
            var handleBitmap = g.GetHdc();

            PrintWindow(window, handleBitmap, 0);

            g.ReleaseHdc(handleBitmap);
            g.Dispose();

            return bitmap;
        }

    }

}