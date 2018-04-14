using System;
using System.Threading;
using Screener.Core.Models.Messages;

namespace Screener.Core.Connection {

    public abstract class RemoteConnection : IDisposable {

        /// <summary>
        /// Поток приема сообщений
        /// </summary>
        protected readonly Thread ReceivingThread;

        /// <summary>
        /// Поток отправки сообщений
        /// </summary>
        protected readonly Thread SendingThread;

        /// <summary>
        /// Действие при получении сообщения
        /// </summary>
        public Action<MessageBase> OnMessage { get; set; }

        /// <summary>
        /// Получатель статуса
        /// </summary>
        public Func<Status> GetStatus { get; set; }

        /// <summary>
        /// Пауза между приемами
        /// </summary>
        public int ReceivingDelay { get; set; }

        /// <summary>
        /// Пауза между отправками
        /// </summary>
        public int SendingDelay { get; set; }

        /// <summary>
        /// true - если есть получатель статуса и соединение установлено, false - иначе
        /// </summary>
        protected bool Connected => GetStatus != null && GetStatus.Invoke() == Status.Connected;

        /// <summary>
        /// Конструктор
        /// </summary>
        protected RemoteConnection() {
            ReceivingThread = new Thread(Receiving) {IsBackground = true};
            SendingThread = new Thread(Sending) {IsBackground = true};
        }

        /// <summary>
        /// Запуск потоков получения и отправки сообщений
        /// </summary>
        internal void Start() {
            ReceivingThread.Start();
            SendingThread.Start();
        }

        /// <summary>
        /// Отключение
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Процесс получения сообщений
        /// </summary>
        protected abstract void Receiving();

        /// <summary>
        /// Процесс отправки сообщений
        /// </summary>
        protected abstract void Sending();

    }

}