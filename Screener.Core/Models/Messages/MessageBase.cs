using System;
using ProtoBuf;

namespace Screener.Core.Models.Messages {

    [Serializable]
    [ProtoContract]
    [ProtoInclude(500, typeof(ProcessScreenMessage))]
    public class MessageBase {

    }

}