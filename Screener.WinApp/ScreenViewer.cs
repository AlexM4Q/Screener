using System;
using System.Threading;
using System.Windows.Forms;
using Screener.Core.Models;
using Screener.Core.Models.Messages;
using Screener.Server;
using Screener.WinApp.Extensions;

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
                        var image = ScreenManager.PrintWindow(window);

                        x.Send(new ProcessScreenMessage {
                            Image = new ImageBytes {
                                Bytes = image.ToBytes(),
                                Width = image.Width,
                                Height = image.Height
                            }
                        });

                        Thread.Sleep(100);
                    }
                }).Start();
            };
        }

    }

}