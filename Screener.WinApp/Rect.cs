using System.Drawing;
using System.Runtime.InteropServices;

namespace Screener.WinApp {

    [StructLayout(LayoutKind.Sequential)]
    internal struct Rect {

        private int _Left;
        private int _Top;
        private int _Right;
        private int _Bottom;

        public Rect(Rect rect) : this(rect.Left, rect.Top, rect.Right, rect.Bottom) {
        }

        public Rect(int left, int top, int right, int bottom) {
            _Left = left;
            _Top = top;
            _Right = right;
            _Bottom = bottom;
        }

        public int X {
            get => _Left;
            set => _Left = value;
        }

        public int Y {
            get => _Top;
            set => _Top = value;
        }

        public int Left {
            get => _Left;
            set => _Left = value;
        }

        public int Top {
            get => _Top;
            set => _Top = value;
        }

        public int Right {
            get => _Right;
            set => _Right = value;
        }

        public int Bottom {
            get => _Bottom;
            set => _Bottom = value;
        }

        public int Height {
            get => _Bottom - _Top;
            set => _Bottom = value + _Top;
        }

        public int Width {
            get => _Right - _Left;
            set => _Right = value + _Left;
        }

        public Point Location {
            get => new Point(Left, Top);
            set {
                _Left = value.X;
                _Top = value.Y;
            }
        }

        public Size Size {
            get => new Size(Width, Height);
            set {
                _Right = value.Width + _Left;
                _Bottom = value.Height + _Top;
            }
        }

        public static implicit operator Rectangle(Rect rectangle) => new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height);

        public static implicit operator Rect(Rectangle rectangle) => new Rect(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);

        public static bool operator ==(Rect rectangle1, Rect rectangle2) => rectangle1.Equals(rectangle2);

        public static bool operator !=(Rect rectangle1, Rect rectangle2) => !rectangle1.Equals(rectangle2);

        public override string ToString() => "{Left: " + _Left + "; " + "Top: " + _Top + "; Right: " + _Right + "; Bottom: " + _Bottom + "}";

        public override int GetHashCode() => ToString().GetHashCode();

        public bool Equals(Rect rectangle) => rectangle.Left == _Left && rectangle.Top == _Top && rectangle.Right == _Right && rectangle.Bottom == _Bottom;

        public override bool Equals(object Object) {
            if (Object is Rect rect) {
                return Equals(rect);
            }

            return Object is Rectangle rectangle && Equals(new Rect(rectangle));
        }

    }

}