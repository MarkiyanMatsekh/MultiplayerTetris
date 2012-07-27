namespace Tetris.Contracts
{
    /// <summary>
    /// Describes the contract that has to be implemented to keep current information about the game field.
    /// </summary>
    public interface IGameField : IUIElement
    {

        /// <summary>
        /// Gets the bounds of the current game field instance.
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Gets the <see cref="T:Tetris.Contracts.IGround"/> implementation that describes the game field's ground.
        /// </summary>
        IGround Ground { get; }

        /// <summary>
        /// Gets the current figure.
        /// </summary>
        IFigure CurrentFigure { get; }

        /// <summary>
        /// Pushes the new <see cref="T:Tetris.Contracts.IFigure"/> implementation into the game field as the current figure.
        /// </summary>
        /// <param name="figure"></param>
        void SetCurrentFigure(IFigure figure);
    }
}