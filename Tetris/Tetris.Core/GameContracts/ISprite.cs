using System;
using Tetris.Core.GameObjects;

namespace Tetris.Core.GameContracts
{
    public interface ISprite
    {
        Size Size { get; }
        Color this[Int32 x, Int32 y] { get; }
    }
}