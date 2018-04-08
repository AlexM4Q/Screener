using System;
using ProtoBuf;

namespace Screener.Core.Models {

    [Serializable]
    [ProtoContract]
    public class ImageBytes {

        [ProtoMember(1)]
        public byte[] Bytes { get; set; }

        [ProtoMember(2)]
        public int Width { get; set; }

        [ProtoMember(3)]
        public int Height { get; set; }

    }

}