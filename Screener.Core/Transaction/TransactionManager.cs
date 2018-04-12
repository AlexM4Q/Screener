using System;
using System.Collections.Generic;
using System.IO;
using ProtoBuf;
using Screener.Core.Models.Messages;

namespace Screener.Core.Transaction
{
    public class TransactionManager
    {
        private readonly int _maxPortionSize;

        private readonly ICollection<TransactionPool> _transactionPools;

        private readonly Action<TransactionMessage> _sender;

        public TransactionManager(int maxPortionSize, Action<TransactionMessage> sender)
        {
            _maxPortionSize = maxPortionSize;
            _sender = sender;
            _transactionPools = new List<TransactionPool>();
        }

        public MessageBase Receive(TransactionMessage transaction)
        {
            foreach (var transactionPool in _transactionPools)
            {
                if (transactionPool.Add(transaction))
                {
                    return transactionPool.IsEnd ? transactionPool.Build() : null;
                }
            }

            var item = new TransactionPool(transaction);
            if (item.IsEnd) return item.Build();

            _transactionPools.Add(item);

            return null;
        }

        public void Send(MessageBase message)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.SerializeWithLengthPrefix(stream, message, PrefixStyle.Fixed32);

                var buffer = stream.ToArray();
                var total = buffer.Length / _maxPortionSize + 1;
                var realPortionSize = buffer.Length / total + 1;

                var transactionId = Guid.NewGuid();
                for (var serialNumber = 0; serialNumber < total; serialNumber++)
                {
                    var portionSize = serialNumber == total - 1 ? buffer.Length - realPortionSize * serialNumber : realPortionSize;
                    var portion = new byte[portionSize];
                    Array.Copy(buffer, serialNumber * realPortionSize, portion, 0, portionSize);

                    _sender(new TransactionMessage
                    {
                        TransactionId = transactionId,
                        Total = total,
                        SerialNumber = serialNumber,
                        Portion = portion
                    });
                }
            }
        }
    }
}