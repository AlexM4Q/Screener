using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Screener.Core.Models;

namespace Screener.WinApp.Extensions {

    public static class BitmapExtensions {

        /// <summary>
        /// Преобразует Bitmap объект в обертку над байтами для быстрой работы с ними
        /// </summary>
        /// <param name="bitmap">Bitmap объект</param>
        /// <returns>Обёртка над байтами</returns>
        public static ImageBytes ToImagePixels(this Bitmap bitmap) {
            var width = bitmap.Width;
            var height = bitmap.Height;
            var bpp = Image.GetPixelFormatSize(bitmap.PixelFormat);
            var bytes = new byte[width * height * bpp / 8];

            var rect = new Rectangle(0, 0, width, height);
            var data = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
            bitmap.UnlockBits(data);

            return new ImageBytes {Bytes = bytes, Width = width, Height = height};
        }

        public static byte[] ToBytes(this Bitmap bitmap) => new ImageConverter().ConvertTo(bitmap, typeof(byte[])) as byte[];

    }

}