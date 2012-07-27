using System;

namespace Tetris.Contracts
{
    /// <summary>
    /// Describes the contract to be implemented to keep the information about the ground row.
    /// </summary>
    public interface IGroundRow : IUIElement
    {
        /// <summary>
        /// Return the width of the current instance of the <see cref="T:Tetris.Contracts.IGroundRow"/>
        /// </summary>
        Int32 Width { get; }

        /// <summary>
        /// Gets or sets the color specified by the horizontal index.
        /// </summary>
        /// <param name="x">Specifies the horizontal index.</param>
        Color this[Int32 x] { get; set; }

        /// <summary>
        /// Returns true if the row is throng.
        /// </summary>
        Boolean IsThroung { get; }
    }
}