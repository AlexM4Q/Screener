using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using ProtoBuf;
using Screener.Core.Models.Messages;

namespace Screener.Core.Connection {

    /// <summary>
    /// TCP соединения
    /// </summary>
    public class TcpConnection : RemoteConnection {

        /// <summary>
        /// Очередь сообщений
        /// </summary>
        protected readonly IList<MessageBase> MessagesQueue;

        /// <summary>
        /// TCP клиент
        /// </summary>
        internal readonly TcpClient TcpClient;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="client">TCP клиент</param>
        public TcpConnection(TcpClient client) {
            MessagesQueue = new List<MessageBase>();

            TcpClient = client;
            TcpClient.ReceiveBufferSize = 60 * 1024;
            TcpClient.SendBufferSize = 60 * 1024;
        }

        /// <summary>
        /// Отключение
        /// </summary>
        public override void Dispose() => TcpClient?.Dispose();

        /// <summary>
        /// Добавление сообщения в очередь на отправку
        /// </summary>
        /// <param name="message">Сообщения</param>
        public void Send(MessageBase message) => MessagesQueue.Add(message);

        /// <summary>
        /// Процесс получения сообщений
        /// </summary>
        protected override void Receiving() {
            while (Connected) {
                if (TcpClient.Available > 0) {
                    try {
                        OnMessage(Serializer.DeserializeWithLengthPrefix<MessageBase>(TcpClient.GetStream(), PrefixStyle.Fixed32));
                    } catch (Exception e) {
                        Debug.WriteLine(e);
                    }
                }

                Thread.Sleep(ReceivingDelay);
            }
        }

        /// <summary>
        /// Процесс отправки сообщений
        /// </summary>
        protected override void Sending() {
            while (Connected) {
                if (MessagesQueue.Count > 0) {
                    var message = MessagesQueue[0];

                    try {
                        Serializer.SerializeWithLengthPrefix(TcpClient.GetStream(), message, PrefixStyle.Fixed32);
                    } catch (Exception e) {
                        Debug.WriteLine(e);
                    } finally {
                        MessagesQueue.Remove(message);
                    }
                }

                Thread.Sleep(SendingDelay);
            }
        }

    }

}