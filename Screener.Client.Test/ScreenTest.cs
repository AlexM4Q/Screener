﻿using System;
using System.Windows.Forms;
using Screener.Client.Test.Properties;
using Screener.Core.Win.Extensions;

namespace Screener.Client.Test {

    internal partial class ScreenTest : Form {

        public ScreenTest() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);

            //var client = new ScreenerClient(
            //    Settings.Default.Host,
            //    Settings.Default.TcpPort,
            //    Settings.Default.UdpReceivePort,
            //    Settings.Default.UdpSendPort
            //) {OnProcessScreenMessage = x => ScreenViewer.Image = x.Image.Bytes.ToImage()};

            var client = new ScreenerClient("127.0.0.1", 11211, 22121, 22122) { OnProcessScreenMessage = x => ScreenViewer.Image = x.Image.Bytes.ToImage() };
        }

    }

}