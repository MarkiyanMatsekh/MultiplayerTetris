using System;
using System.Diagnostics;

namespace Tetris.Contracts
{
    /// <summary>
    /// Describes the size of a two-dimensional rectangle. 
    /// Two <see cref="T:System.Int32"/> values describe the Tetris.Contracts.Size.Width and Tetris.Contracts.Size.Height components, respectively.
    /// </summary>
    [DebuggerDisplay("({Width},{Height})")]
    public struct Size : IEquatable<Size>
    {
        private Int32 _width;
        private Int32 _height;

        private static readonly Size EmptyInstance = new Size { Height = 0, Width = 0 };

        public Size(int width, int height) : this()
        {
            _width = width;
            _height = height;
        }

        /// <summary>
        /// Gets or sets the Tetris.Contracts.Size.Height of this instance of Tetris.Contracts.Size.
        /// </summary>
        /// <returns>
        /// The Tetris.Contracts.Size.Height of this instance of Tetris.Contracts.Size. The default is 0.
        /// </returns>
        public Int32 Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// Gets or sets the Tetris.Contracts.Size.Width of this instance of Tetris.Contracts.Size.
        /// </summary>
        /// <returns>
        /// The Tetris.Contracts.Size.Width of this instance of Tetris.Contracts.Size. The default is 0.
        /// </returns>
        public Int32 Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Gets a value that indicates whether this instance of Tetris.Contracts.Size is Tetris.Contracts.Size.Empty.
        /// </summary>
        /// <remarks>
        /// true if this instance of size is Tetris.Contracts.Size.Empty; otherwise false.
        /// </remarks>
        public Boolean IsEmpty
        {
            get { return Equals(Empty); }
        }

        /// <summary>
        /// Gets a value that represents a static empty Tetris.Contracts.Size.
        /// </summary>
        /// <returns>
        /// An empty instance of Tetris.Contracts.Size.
        /// </returns>
        public static Size Empty
        {
            get { return EmptyInstance; }
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(Size other)
        {
            return _width == other._width && _height == other._height;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param><filterpriority>2</filterpriority>
        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Size && Equals((Size) obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                return (Width*397) ^ Height;
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