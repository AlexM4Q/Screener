using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Screener.Core.Models.Messages;

namespace Screener.Core {

    public abstract class ClientConnection {

        protected readonly TcpClient Client;

        protected readonly IList<MessageBase> MessagesQueue;

        protected Status Status;

        protected Thread ReceivingThread;

        protected Thread SendingThread;

        protected ClientConnection(TcpClient client) {
            Client = client;
            Client.ReceiveBufferSize = 16 * 1024;
            Client.SendBufferSize = 16 * 1024;

            MessagesQueue = new List<MessageBase>();

            ReceivingThread = new Thread(ReceivingMethod) {IsBackground = true};
            ReceivingThread.Start();

            SendingThread = new Thread(SendingMethod) {IsBackground = true};
            SendingThread.Start();
        }

        public void Stop() {
            Status = Status.Disconnected;
        }

        public void Send(MessageBase message) {
            MessagesQueue.Add(message);
        }

        protected abstract void OnMessageReceived(MessageBase messageBase);

        private void SendingMethod() {
            while (Status != Status.Disconnected) {
                if (MessagesQueue.Count > 0) {
                    var message = MessagesQueue[0];

                    try {
                        new BinaryFormatter().Serialize(Client.GetStream(), message);
                    } catch {
                        Stop();
                    } finally {
                        MessagesQueue.Remove(message);
                    }
                }

                Thread.Sleep(30);
            }
        }

        private void ReceivingMethod() {
            while (Status != Status.Disconnected) {
                if (Client.Available > 0) {
                    try {
                        OnMessageReceived(new BinaryFormatter().Deserialize(Client.GetStream()) as MessageBase);
                    } catch (Exception e) {
                        var ex = new Exception("Unknown message recieved. Could not deserialize the stream", e);
                        Debug.WriteLine(ex.Message);
                    }
                }

                Thread.Sleep(30);
            }
        }

    }

}