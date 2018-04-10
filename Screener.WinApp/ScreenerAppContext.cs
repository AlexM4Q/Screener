using Screener.Server;
using Screener.WinApp.Properties;

namespace Screener.WinApp {

    internal class ScreenerAppContext {

        private static ScreenerAppContext _instance;

        public static ScreenerAppContext Instance => _instance ?? (_instance = new ScreenerAppContext());

        public ScreenerServer Server { get; }

        public ScreenerAppContext() {
            Server = new ScreenerServer(Settings.Default.TcpPort, Settings.Default.UdpReceivePort, Settings.Default.UdpSendPort);
        }

    }

}