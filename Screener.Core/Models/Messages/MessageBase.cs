using System;
using ProtoBuf;
using Screener.Core.Transaction;

namespace Screener.Core.Models.Messages {

    [Serializable]
    [ProtoContract]
    [ProtoInclude(500, typeof(ProcessScreenMessage))]
    [ProtoInclude(501, typeof(TransactionMessage))]
    public class MessageBase {

    }

}