using System;
using Tetris.Core.GameContracts;
using Tetris.Core.GameObjects;
using Tetris.Core.GameObjects.Figures;
using Tetris.Core.Helpers;

namespace Tetris.Core.GameLogic
{
    public class GameField : IUIElement
    {
        private readonly Size _size;
        private IFigure _currentFigure;
        private readonly IInputQueue _queue;
        private readonly ModifyableSprite _sprite;
        private int _peak;

        public GameField(int width, int height, IInputQueue queue)
            : this(new Size(width, height), queue)
        {
        }

        public GameField(Size size, IInputQueue queue)
        {
            _size = size;
            _queue = queue;
            _sprite = new ModifyableSprite(size);
            _currentFigure = new FigureI(3, 0);
        }

        public ISprite GetCurrentView()
        {
            var sprite = new ModifyableSprite(_sprite);

            _currentFigure.ForEachNonEmptyCell((i, j) =>
            {
                var x = _currentFigure.Placement.Left + i;
                var y = _currentFigure.Placement.Top + j;

                if (x < 0 || y < 0 || x > _size.Width - 1 || y > _size.Height - 1 || _currentFigure[i, j].IsEmptyCell())
                    return;

                if (!sprite[x, y].IsEmptyCell())
                    throw new InvalidOperationException("cannot draw current view because figure overlaps ground");

                sprite[x, y] = CurrentFigure[i, j];
            });

            return sprite;
        }

        public ISprite GroundView
        {
            get { return _sprite; }
        }

        public int GroundPeak
        {
            get { return _peak; }
        }

        public Size Size
        {
            get { return _size; }
        }

        public IFigure CurrentFigure
        {
            get { return _currentFigure; }
        }

        public void SetCurrentFigure(IFigure figure)
        {
            _currentFigure = figure;
        }

        public void AttachFigureToTheGround(IFigure figure)
        {
            figure.ForEachNonEmptyCell((i, j) =>
            {
                var x = figure.Placement.Left + i;
                var y = figure.Placement.Top + j;
                var cell = _sprite[x, y];
                if (!cell.IsEmptyCell())
                    throw new InvalidOperationException("can't attach figure to ground - it's already filled");
                _sprite[x, y] = figure[i, j];
                _peak = Math.Max(_peak, y);
            });
        }
    }
}