namespace Tetris.Contracts
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