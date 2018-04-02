using System;
using System.Windows.Forms;

namespace Screener.Client.Test {

    public partial class ScreenTest : Form {

        public ScreenTest() {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);

            var client = new ScreenerClient("127.0.0.1", 11000) {OnProcessScreenMessage = x => ScreenViewer.Image = x.Screen};
        }

    }

}