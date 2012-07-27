using System;
using System.Diagnostics;

namespace Tetris.Contracts
{
    /// <summary>
    /// Describes the offset of the component relative to its container. 
    /// Two <see cref="T:System.Int32"/> values describe the Tetris.Contracts.Offset.Left and Tetris.Contracts.Offset.Top components.
    /// </summary>
    [DebuggerDisplay("({Left},{Top})")]
    public class Offset : IEquatable<Offset>
    {
        private Int32 _top;
        private Int32 _left;

        private static readonly Offset EmptyInstance = new Offset (0, 0);

        /// <summary>
        /// Initializes the new instance of the <see cref="T:Tetris.Contracts.Offset"/> structure.
        /// </summary>
        public Offset() : this(0,0)
        {
            
        }

        /// <summary>
        /// Initializes the new instance of the <see cref="T:Tetris.Contracts.Offset"/> structure.
        /// </summary>
        /// <param name="top">Initializes the Tetris.Contracts.Offset.Top component of this instance of Tetris.Contracts.Offset.</param>
        /// <param name="left">Initializes the Tetris.Contracts.Offset.Left component of this instance of Tetris.Contracts.Offset.</param>
        public Offset(int left, int top)
        {
            _top = top;
            _left = left;
        }

        /// <summary>
        /// Gets or sets the Tetris.Contracts.Offset.Top component of this instance of Tetris.Contracts.Offset.
        /// </summary>
        /// <returns>
        /// The Tetris.Contracts.Offset.Top of this instance of Tetris.Contracts.Offset. The default is 0.
        /// </returns>
        public Int32 Top
        {
            get { return _top; }
            set { _top = value; }
        }

        /// <summary>
        /// Gets or sets the Tetris.Contracts.Offset.Left of this instance of Tetris.Contracts.Offset.
        /// </summary>
        /// <returns>
        /// The Tetris.Contracts.Offset.Left of this instance of Tetris.Contracts.Offset. The default is 0.
        /// </returns>
        public Int32 Left
        {
            get { return _left; }
            set { _left = value; }
        }

        /// <summary>
        /// Gets a value that indicates whether this instance of Tetris.Contracts.Offset is Tetris.Contracts.Offset.Empty.
        /// </summary>
        /// <remarks>
        /// true if this instance of size is Tetris.Contracts.Offset.Empty; otherwise false.
        /// </remarks>
        public Boolean IsEmpty
        {
            get { return Equals(Empty); }
        }

        /// <summary>
        /// Gets a value that represents a static empty Tetris.Contracts.Offset.
        /// </summary>
        /// <returns>
        /// An empty instance of Tetris.Contracts.Offset.
        /// </returns>
        public static Offset Empty
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
        public bool Equals(Offset other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _top == other._top && _left == other._left;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Offset) obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                return (_top*397) ^ _left;
            }
        }

        public static bool operator ==(Offset left, Offset right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Offset left, Offset right)
        {
            return !Equals(left, right);
        }
    }
}