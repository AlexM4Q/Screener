using System.Drawing;

namespace Screener.Core.Win.Extensions {

    public static class BitmapExtensions {

        public static byte[] ToBytes(this Image bitmap) => new ImageConverter().ConvertTo(bitmap, typeof(byte[])) as byte[];

        public static Image ToImage(this byte[] bytes) => new ImageConverter().ConvertFrom(bytes) as Image;

    }

}