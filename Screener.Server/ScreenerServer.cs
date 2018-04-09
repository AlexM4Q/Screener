using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Screener.Server {

    public class ScreenerServer {
        
        private readonly IList<ScreenerConnection> _connections;

        private readonly int _port;

        private TcpListener _listener;

        private bool _isStarted;

        public Action<ScreenerConnection> OnClientConnected { get; set; }

        public ScreenerServer(int port) {
            _connections = new List<ScreenerConnection>();
            _port = port;
        }
        
        public void Start() {
            if (_isStarted) return;

            _listener = new TcpListener(IPAddress.Any, _port);
            _listener.Start();
            _isStarted = true;

            WaitForConnection();
        }

        public void Stop() {
            if (!_isStarted) return;

            _listener.Stop();
            _isStarted = false;
        }

        private void WaitForConnection() {
            _listener.BeginAcceptTcpClient(ConnectionHandler, null);
        }

        private void ConnectionHandler(IAsyncResult ar) {
            lock (_connections) {
                var connection = new ScreenerConnection(_listener.EndAcceptTcpClient(ar));
                _connections.Add(connection);
                OnClientConnected?.Invoke(connection);
            }

            WaitForConnection();
        }

    }

}