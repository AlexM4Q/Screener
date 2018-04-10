using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Screener.WinApp.Entities {

    internal class ScreenManager {

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string windowName);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr window, out Rectangle rect);

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr window, IntPtr hdcBlt, int nFlags);

        public static Bitmap PrintWindow(IntPtr window) {
            GetWindowRect(window, out var rect);

            var bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            var g = Graphics.FromImage(bitmap);
            var handle = g.GetHdc();

            PrintWindow(window, handle, 0);

            g.ReleaseHdc(handle);
            g.Dispose();

            return bitmap;
        }

    }

}