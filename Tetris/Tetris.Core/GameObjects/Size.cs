using System;
using System.Diagnostics;

namespace Tetris.Core.GameObjects
{
    [DebuggerDisplay("({Width},{Height})")]
    public struct Size : IEquatable<Size>
    {
        private static readonly Size EmptyInstance = new Size(0, 0);

        private readonly int _height;
        private readonly int _width;

        public Size(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public int Height { get { return _height; } }
        public int Width { get { return _width; } }

        public static Size Empty
        {
            get { return EmptyInstance; }
        }
        public Boolean Equals(Size other)
        {
            return Width == other.Width && Height == other.Height;
        }
        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Size && Equals((Size)obj);
        }
        public override Int32 GetHashCode()
        {
            unchecked
            {
                return (Width * 397) ^ Height;
            }
        }
        public static Boolean operator ==(Size left, Size right)
        {
            return left.Equals(right);
        }
        public static Boolean operator !=(Size left, Size right)
        {
            return !left.Equals(right);
        }
    }
}