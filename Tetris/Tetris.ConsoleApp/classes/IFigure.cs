namespace Tetris.Contracts
{
    /// <summary>
    /// Defines the contract that has to be implemented to create a figure.
    /// </summary>
    public interface IFigure : ISprite
    {
        /// <summary>
        /// Gets the <see cref="T:Tetris.Contracts.Offset"/> of the figure relative to the container.
        /// </summary>
        Offset Placement { get; }
        bool IsFullyInside(Size size);

        IFigure RotateClockwise();

        //IFigure MoveDown();
        //IFigure MoveLeft();
        //IFigure MoveRight();

    }
} 