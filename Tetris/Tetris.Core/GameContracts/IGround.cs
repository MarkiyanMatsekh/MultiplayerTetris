using Tetris.Core.GameObjects;

namespace Tetris.Core.GameContracts
{
    public interface IGround : IUIElement
    {
        Size Size { get; }
        Color this[int x, int y] { get; }
        int Peak { get; }
        bool TryAttachFigure(IFigure figure);
    }
}