using System;
using System.Threading;
using System.Windows.Forms;
using Screener.Core.Models.Messages;
using Screener.Server;

namespace Screener.WinApp {

    public partial class ScreenViewer : Form {

        public ScreenViewer() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);

            var window = ScreenManager.FindWindow(null, "Визуальные закладки - Mozilla Firefox");
            var server = new ScreenerServer(11000);
            server.Start();

            server.OnClientConnected = x => {
                new Thread(() => {
                    while (true) {
                        x.Send(new ProcessScreenMessage(ScreenManager.PrintWindow(window)));
                        Thread.Sleep(100);
                    }
                }).Start();
            };
        }

    }

}