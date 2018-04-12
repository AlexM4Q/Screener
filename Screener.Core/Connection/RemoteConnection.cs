using System;
using System.Threading;
using Screener.Core.Models.Messages;

namespace Screener.Core.Connection {

    public abstract class RemoteConnection : IDisposable {

        protected readonly Thread ReceivingThread;

        protected readonly Thread SendingThread;

        public Action<MessageBase> OnMessage { get; set; }

        public Func<Status> GetStatus { get; set; }

        protected bool Connected => GetStatus.Invoke() == Status.Connected;

        protected RemoteConnection() {
            ReceivingThread = new Thread(Receiving) {IsBackground = true};
            ReceivingThread.Start();
            SendingThread = new Thread(Sending) {IsBackground = true};
            SendingThread.Start();
        }

        public abstract void Dispose();

        protected abstract void Receiving();

        protected abstract void Sending();

    }

}