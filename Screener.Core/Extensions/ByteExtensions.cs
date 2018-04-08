using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Screener.Core.Extensions {

    public static class ByteExtensions {

        public static byte[] ToBytes(this object obj) {
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream()) {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T ToObject<T>(this byte[] bytes) {
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream(bytes)) {
                return (T) bf.Deserialize(ms);
            }
        }

    }

}