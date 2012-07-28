using Tetris.Core.GameObjects;

namespace Tetris.Core.GameContracts
{
    public interface IFigure : ISprite
    {
        Offset Placement { get; }

        IFigure RotateClockwise();
        IFigure MoveDown();
        IFigure MoveLeft();
        IFigure MoveRight();
    }
} 