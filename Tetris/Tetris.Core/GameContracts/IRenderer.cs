using Tetris.Core.GameObjects;

namespace Tetris.Core.GameContracts
{
    public interface IRenderer
    {
        void Render(IFigure figure); // todo: stinks a bit
        void Render(ISprite sprite);
        void Render(ISprite sprite, Offset placement);
        // todo: void RenderDiff(sprite1, sprite2)
    }
}