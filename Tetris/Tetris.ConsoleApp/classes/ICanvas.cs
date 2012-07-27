using System;

namespace Tetris.Contracts
{
    /// <summary>
    /// Defines the contract that has to be implemented to create a canvas.
    /// </summary>
    public interface ICanvas
    {
        /// <summary>
        /// Gets the size of the canvas.
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Gets or sets the color at the position specified by the horizontal and vertical indexes.
        /// </summary>
        /// <param name="x">Specifies the horizintal index. Should be greater or equal zero and less the the width of the canvas.</param>
        /// <param name="y">Specifies the vertical index. Should be greater or equal zero and less the the height of the canvas.</param>
        /// <returns></returns>
        Color this[Int32 x, Int32 y] { get; set; }
    }
}