using System;
using Tetris.Core.Figures;
using Tetris.Core.GameContracts;
using Tetris.Core.GameObjects;

namespace Tetris.Core.GameLogic
{
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