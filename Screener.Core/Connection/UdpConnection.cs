using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using ProtoBuf;
using Screener.Core.Models.Messages;
using Screener.Core.Transaction;

namespace Screener.Core.Connection {

    /// <summary>
    /// UDP соединения
    /// </summary>
    public class UdpConnection : RemoteConnection {

        /// <summary>
        /// Адрес отправки
        /// </summary>
        private readonly IPEndPoint _adress;

        /// <summary>
        /// Порт отправки
        /// </summary>
        private readonly int _sendPort;

        /// <summary>
        /// Нынешнее сообщение для отправки
        /// </summary>
        protected MessageBase UdpMessage;

        /// <summary>
        /// UDP клиент
        /// </summary>
        internal readonly UdpClient UdpClient;

        /// <summary>
        /// Менеджер транзакций
        /// </summary>
        private readonly TransactionManager _transactionManager;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="adress">Адрес отправки</param>
        /// <param name="receivePort">Порт получения</param>
        /// <param name="sendPort">Порт отпрравки</param>
        public UdpConnection(IPEndPoint adress, int receivePort, int sendPort) {
            _adress = adress;
            _sendPort = sendPort;

            UdpClient = new UdpClient(receivePort);

            _transactionManager = new TransactionManager(30000, Sending);
        }

        /// <summary>
        /// Отключение
        /// </summary>
        public override void Dispose() => UdpClient?.Dispose();

        /// <summary>
        /// Установка сообщеняи для отправки
        /// </summary>
        /// <param name="message"></param>
        public void Send(MessageBase message) => UdpMessage = message;

        /// <summary>
        /// Процесс получения сообщений
        /// </summary>
        protected override void Receiving() {
            IPEndPoint remoteIp = null;

            using (this) {
                try {
                    while (Connected) {
                        var data = UdpClient.Receive(ref remoteIp);

                        using (var stream = new MemoryStream(data)) {
                            var message = Serializer.DeserializeWithLengthPrefix<MessageBase>(stream, PrefixStyle.Fixed32);
                            if (message is TransactionMessage transaction) {
                                var build = _transactionManager.Receive(transaction);
                                if (build != null) {
                                    OnMessage(build);
                                }
                            } else {
                                OnMessage(message);
                            }
                        }
                    }
                } catch (Exception e) {
                    Debug.WriteLine(e);
                }
            }
        }

        /// <summary>
        /// Процесс отправки сообщений
        /// </summary>
        protected override void Sending() {
            using (this) {
                try {
                    while (Connected) {
                        if (UdpMessage == null) continue;
                        lock (UdpMessage) {
                            _transactionManager.Send(UdpMessage);
                        }

                        UdpMessage = null;
                    }
                } catch (Exception e) {
                    Debug.WriteLine(e);
                }
            }
        }

        /// <summary>
        /// Непосредственно отправка сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        protected void Sending(MessageBase message) {
            using (var stream = new MemoryStream()) {
                Serializer.SerializeWithLengthPrefix(stream, message, PrefixStyle.Fixed32);

                var data = stream.ToArray();
                UdpClient.Send(data, data.Length, "127.0.0.1", _sendPort);
            }
        }

    }

}