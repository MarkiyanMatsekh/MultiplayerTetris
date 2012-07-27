using System;
using Tetris.Implementation.Figures;

namespace Tetris.Contracts
{
    /// <summary>
    /// Describes the contract for the Ground implementation.
    /// </summary>
    public interface IGround : IUIElement
    {
        /// <summary>
        /// Gets the actual size of the current instance of the <see cref="T:Tetris.Contracts.IGround"/> implementation.
        /// </summary>
        Size Size { get; }

        bool TryAttachFigure(IFigure figure);

        int Peak { get; }

        Color this[int x, int y] { get; }
    }

    public class Ground : IGround
    {
        private readonly Size _size;
        private readonly ModifyableSprite _sprite;
        private int _peak;

        public Ground(Size gameFieldSize)
        {
            _size = gameFieldSize;
            _sprite = new ModifyableSprite(new Color[_size.Width, _size.Height]);
            _peak = 0;
        }

        public int Peak
        {
            get { return _peak; }
        }

        public Color this[int x, int y]
        {
            get { return _sprite[x, y]; }
        }

        public ISprite GetCurrentView()
        {
            return _sprite;
        }

        public Size Size
        {
            get { return _size; }
        }

        public bool TryAttachFigure(IFigure figure)
        {
            throw new NotImplementedException();
        }
    }
}