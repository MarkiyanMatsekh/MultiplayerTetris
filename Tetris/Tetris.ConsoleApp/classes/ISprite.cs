using System;

namespace Tetris.Contracts
{
    /// <summary>
    /// Defines the contract that has to be implemented to create an sprite component.
    /// </summary>
    public interface ISprite
    {
        Size Size { get; }

        Color this[Int32 x, Int32 y] { get; }
    }
}