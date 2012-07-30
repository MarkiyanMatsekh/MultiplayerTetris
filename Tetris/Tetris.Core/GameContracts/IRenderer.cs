using Tetris.Core.GameObjects;

namespace Tetris.Core.GameContracts
{
    public interface IRenderer
    {
        void Render(ISprite sprite);
        void RenderDiff(ISprite oldSprite, ISprite newSprite);
    }
}