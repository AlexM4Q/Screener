﻿using System;
using System.Threading;
using System.Windows.Forms;
using Screener.Core.Models;
using Screener.Core.Models.Messages;
using Screener.Core.Win.Extensions;
using Screener.WinApp.Entities;

namespace Screener.WinApp {

    internal partial class ScreenViewer : Form {

        public ScreenViewer() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);

            //var window = ScreenManager.FindWindow(null, "Mozilla Firefox");
            var window = ScreenManager.FindWindow(null, "Визуальные закладки - Mozilla Firefox");
            //var window = ScreenManager.FindWindow(null, "Проигрыватель Windows Media");

            ScreenerAppContext.Instance.Server.OnClientConnected = x => {
                new Thread(() => {
                    while (true) {
                        var image = ScreenManager.PrintWindow(window);

                        x.SendViaUdp(new ProcessScreenMessage {
                            Image = new ImageBytes {
                                Bytes = image.ToBytes(),
                                Width = image.Width,
                                Height = image.Height
                            }
                        });

                        Thread.Sleep(30);
                    }
                }).Start();
            };
        }

        private void OnFpsSliderScroll(object sender, EventArgs e) {
            var fps = FpsSlider.Value;
            FpsLabel.Text = fps.ToString();
        }

    }

}