using System;

namespace Tetris.Contracts
{
    /// <summary>
    /// Describes the contract for the Ground implementation.
    /// </summary>
    public interface IGround : IUIElement
    {
        /// <summary>
        /// Gets the offset of the current instance of the <see cref="T:Tetris.Contracts.IGround"/> implementation.
        /// </summary>
        Offset Placement { get; }

        /// <summary>
        /// Gets the actual size of the current instance of the <see cref="T:Tetris.Contracts.IGround"/> implementation.
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Gets or sets the color at the position specified by the horizontal and vertical indexes.
        /// </summary>
        /// <param name="x">Specifies the horizintal index. Should be greater or equal zero and less the the width of the canvas.</param>
        /// <param name="y">Specifies the vertical index. Should be greater or equal zero and less the the height of the canvas.</param>
        /// <returns></returns>
        Color this[Int32 x, Int32 y] { get; set; }

        /// <summary>
        /// Gets the collection of the ground rows
        /// </summary>
        IGroundRowCollection Rows { get; }
    }
}