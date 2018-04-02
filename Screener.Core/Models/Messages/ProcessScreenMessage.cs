using System;
using System.Drawing;

namespace Screener.Core.Models.Messages {

    [Serializable]
    public class ProcessScreenMessage : MessageBase {

        public Bitmap Screen { get; }

        public ProcessScreenMessage(Bitmap screen) {
            Screen = screen;
        }

    }

}