namespace Tetris.Contracts
{
    /// <summary>
    /// Defines the contract that has to be implemented to render the sprites.
    /// </summary>
    public interface ISpriteRenderer
    {
        /// <summary>
        /// Renders the sprite.
        /// </summary>
        /// <param name="sprite">The sprite to be rendered.</param>
        void Render(ISprite sprite);

        /// <summary>
        /// Renders the sprite.
        /// </summary>
        /// <param name="sprite">The sprite to be rendered.</param>
        /// <param name="placement">Initial placement of the sprite on the rendered field.</param>
        void Render(ISprite sprite, Offset placement);
    }
}