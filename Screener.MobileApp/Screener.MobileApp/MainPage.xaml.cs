using System;
using System.IO;
using Screener.Client;
using Screener.Core.Models.Messages;
using Xamarin.Forms;

namespace Screener.MobileApp {

    public partial class MainPage {

        public MainPage() {
            InitializeComponent();
        }

        public void OnConnect(object sender, EventArgs e) {
            var ipAdress = IpAdress.Text;

            var client = new ScreenerClient(ipAdress, 11211, 22121, 22122) {
                OnProcessScreenMessage = screenMessage => Device.BeginInvokeOnMainThread(() => ScreenImage.Source = ImageSource.FromStream(() => new MemoryStream(screenMessage.Image.Bytes)))
            };
        }

    }

}