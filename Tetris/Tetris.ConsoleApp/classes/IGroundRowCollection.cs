using System;
using System.Collections.Generic;

namespace Tetris.Contracts
{
    public interface IGroundRowCollection : IEnumerable<IGroundRow>, IUIElement
    {
        /// <summary>
        /// Gets the <see cref="T:Tetris.Contracts.IGroundRow"/> implementation at the specified index.
        /// </summary>
        /// <param name="index">Specifies the index.</param>
        IGroundRow this[Int32 index] { get; }

        /// <summary>
        /// Adds the row at the end of the collection.
        /// </summary>
        /// <param name="row">The row to be added.</param>
        void AppendRow(IGroundRow row);

        /// <summary>
        /// Adds the row at the beginning of the colleciton.
        /// </summary>
        /// <param name="row">The row to be added.</param>
        void PrependRow(IGroundRow row);

        /// <summary>
        /// Removes throng rows from the collection.
        /// </summary>
        /// <returns>The amount of rows that have been eleiminated.</returns>
        Int32 EliminateThrongRows();
    }
}