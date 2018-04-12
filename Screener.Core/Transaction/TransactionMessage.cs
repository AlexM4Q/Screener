using System;
using ProtoBuf;
using Screener.Core.Models.Messages;

namespace Screener.Core.Transaction
{
    [Serializable]
    [ProtoContract]
    public sealed class TransactionMessage : MessageBase
    {
        [ProtoMember(1)]
        public Guid TransactionId { get; set; }

        [ProtoMember(2)]
        public int Total { get; set; }

        [ProtoMember(3)]
        public int SerialNumber { get; set; }

        [ProtoMember(4)]
        public byte[] Portion { get; set; }
    }
}