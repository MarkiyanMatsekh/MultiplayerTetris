using Tetris.Core.GameObjects;

namespace Tetris.Core.GameContracts
{
    public interface IRenderer
    {
        void Render(IFigure figure); // todo: stinks a bit
        void Render(ISprite sprite);
        void Render(ISprite sprite, Offset placement);
        void RenderDiff(ISprite oldSprite, ISprite newSprite);
    }
}