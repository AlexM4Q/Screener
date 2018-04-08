using System;
using ProtoBuf;

namespace Screener.Core.Models.Messages {

    [Serializable]
    [ProtoContract]
    public sealed class ProcessScreenMessage : MessageBase {

        [ProtoMember(1)]
        public ImageBytes Image { get; set; }

    }

}