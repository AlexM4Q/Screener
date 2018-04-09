using System;
using System.IO;
using Screener.Client;
using Xamarin.Forms;

namespace Screener.MobileApp {

    public partial class MainPage {

        public MainPage() {
            InitializeComponent();
        }

        private static volatile bool a = true;

        public void OnConnect(object sender, EventArgs e) {
            var ipAdress = IpAdress.Text;

            var client = new ScreenerClient(ipAdress, 11000) {
                OnProcessScreenMessage = screenMessage => {
                    Device.BeginInvokeOnMainThread(() => {
                        ScreenImage.Source = ImageSource.FromStream(() => new MemoryStream(screenMessage.Image.Bytes));
                    });
                }
            };
        }

    }

}