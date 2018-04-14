using System;
using System.Net;
using System.Net.Sockets;
using Screener.Core.Models.Messages;

namespace Screener.Core.Connection {

    /// <summary>
    /// Клиент подключение
    /// </summary>
    public abstract class ClientConnection : IDisposable {

        /// <summary>
        /// TCP соединение
        /// </summary>
        protected readonly TcpConnection TcpConnection;

        /// <summary>
        /// UDP соединение
        /// </summary>
        protected readonly UdpConnection UdpConnection;

        /// <summary>
        /// Статус соединения
        /// </summary>
        protected Status Status { get; set; }

        public TcpClient TcpClient => TcpConnection.TcpClient;

        public UdpClient UdpClient => UdpConnection.UdpClient;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="client">TCP клиент</param>
        /// <param name="udpReceivePort">порт UDP приема</param>
        /// <param name="udpSendPort">порт UDP отправки</param>
        protected ClientConnection(TcpClient client, int udpReceivePort, int udpSendPort) {
            TcpConnection = new TcpConnection(client) {
                OnMessage = OnTcpMessageReceived,
                GetStatus = () => Status
            };

            UdpConnection = new UdpConnection(client.Client.RemoteEndPoint as IPEndPoint, udpReceivePort, udpSendPort) {
                OnMessage = OnUdpMessageReceived,
                GetStatus = () => Status
            };
        }

        /// <summary>
        /// Запуск соединений
        /// </summary>
        protected void Start() {
            TcpConnection.Start();
            UdpConnection.Start();
        }

        /// <summary>
        /// Завершение соединений
        /// </summary>
        public void Dispose() {
            Status = Status.Disconnected;
            TcpConnection.Dispose();
            UdpConnection.Dispose();
        }

        /// <summary>
        /// Отправка по TCP
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void SendViaTcp(MessageBase message) => TcpConnection.Send(message);

        /// <summary>
        /// Отправка по TCP
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void SendViaUdp(MessageBase message) => UdpConnection.Send(message);

        /// <summary>
        /// Получение сообщения по TCP
        /// </summary>
        /// <param name="message">Сообщение</param>
        protected abstract void OnTcpMessageReceived(MessageBase message);

        /// <summary>
        /// Получение сообщения по UDP
        /// </summary>
        /// <param name="message">Сообщение</param>
        protected abstract void OnUdpMessageReceived(MessageBase message);

    }

}