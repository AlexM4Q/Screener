using System;
using System.Net.Sockets;
using Screener.Core.Connection;
using Screener.Core.Models.Messages;

namespace Screener.Client {

    /// <summary>
    /// Клиент
    /// </summary>
    public class ScreenerClient : ClientConnection {

        /// <summary>
        /// Действие при получении сообщения с изображением экрана
        /// </summary>
        public Action<ProcessScreenMessage> OnProcessScreenMessage { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="adress">IP сервера</param>
        /// <param name="tcpPort">потр TCP клиента</param>
        /// <param name="udpReceivePort">порт UDP приема</param>
        /// <param name="udpSendPort">порт UDP отправки</param>
        public ScreenerClient(string adress, int tcpPort, int udpReceivePort, int udpSendPort) : base(new TcpClient(), udpReceivePort, udpSendPort) {
            TcpClient.Connect(adress, tcpPort);
            Status = Status.Connected;

            Start();
        }

        protected override void OnTcpMessageReceived(MessageBase message) {
            switch (message) {
                case ProcessScreenMessage processScreenMessage:
                    OnProcessScreenMessage?.Invoke(processScreenMessage);
                    break;
            }
        }

        protected override void OnUdpMessageReceived(MessageBase message) {
            switch (message) {
                case ProcessScreenMessage processScreenMessage:
                    OnProcessScreenMessage?.Invoke(processScreenMessage);
                    break;
            }
        }

    }

}