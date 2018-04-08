using System;
using System.Drawing;
using System.Windows.Forms;
using Screener.Core.Extensions;

namespace Screener.Client.Test {

    internal partial class ScreenTest : Form {

        public ScreenTest() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);

            var client = new ScreenerClient("127.0.0.1", 11000) {OnProcessScreenMessage = x => ScreenViewer.Image = x.Image.Bytes.ToObject<Bitmap>()};
        }

    }

}