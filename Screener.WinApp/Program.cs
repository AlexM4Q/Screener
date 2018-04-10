using System.Windows.Forms;

namespace Screener.WinApp {

    internal static class Program {

        private static void Main() {
            ScreenerAppContext.Instance.Server.Start();

            Application.Run(new ScreenViewer());
        }

    }

}