using System.IO;
using System.Linq;
using ProtoBuf;
using Screener.Core.Models.Messages;

namespace Screener.Core.Transaction
{
    public class TransactionPool
    {
        private readonly TransactionMessage[] _transactions;

        public bool IsEnd => _transactions.All(transactionMessage => transactionMessage != null);

        public TransactionPool(TransactionMessage transaction)
        {
            _transactions = new TransactionMessage[transaction.Total];
            _transactions[transaction.SerialNumber] = transaction;
        }

        public bool Add(TransactionMessage transaction)
        {
            if (transaction.TransactionId != _transactions[0].TransactionId) return false;

            _transactions[transaction.SerialNumber] = transaction;
            return true;
        }

        public MessageBase Build()
        {
            using (var stream = new MemoryStream())
            {
                foreach (var transaction in _transactions)
                {
                    stream.Write(transaction.Portion, 0, transaction.Portion.Length);
                }

                stream.Position = 0;
                return Serializer.DeserializeWithLengthPrefix<MessageBase>(stream, PrefixStyle.Fixed32);
            }
        }
    }
}