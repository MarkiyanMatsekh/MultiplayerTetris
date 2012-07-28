using System;
using Tetris.Core.GameContracts;
using Tetris.Core.GameObjects;
using Tetris.Core.GameObjects.Figures;
using Tetris.Core.Helpers;

namespace Tetris.Core.GameLogic
{
    public class Ground : IUIElement
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

        public void AttachFigure(IFigure figure)
        {
            figure.ForEachNonEmptyCell((i, j) =>
            {
                var cell = _sprite[figure.Placement.Left + i, figure.Placement.Top + j];
                if (!cell.IsEmptyCell())
                    throw new InvalidOperationException("can't attach figure to ground - it's already filled");
                _sprite[figure.Placement.Left + i, figure.Placement.Top + j] = figure[i, j];
                var a = 5;
            });
        }
    }
}