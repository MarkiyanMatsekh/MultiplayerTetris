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
        private readonly Ground _ground;
        private IFigure _currentFigure;

        public GameField(int width, int height)
            : this(new Size(width, height))
        {
        }

        public GameField(Size size)
        {
            _size = size;
            _ground = new Ground(_size);
            _currentFigure = new FigureI(3, 0);
        }

        public ISprite GetCurrentView()
        {
            var sprite = new ModifyableSprite(_ground.GetCurrentView());

            _currentFigure.ForEachNonEmptyCell((i, j) =>
            {
                var x = _currentFigure.Placement.Left + i;
                var y = _currentFigure.Placement.Top + j;

                if (x < 0 || y < 0 || x > _size.Width - 1 || y > _size.Height - 1 || _currentFigure[i,j].IsEmptyCell())
                    return;

                if (!sprite[x,y].IsEmptyCell())
                    throw new InvalidOperationException("cannot draw current view because figure overlaps ground");

                sprite[x, y] = CurrentFigure[i, j];
            });

            return sprite;
        }

        public Size Size
        {
            get { return _size; }
        }

        public Ground Ground
        {
            get { return _ground; }
        }

        public IFigure CurrentFigure
        {
            get { return _currentFigure; }
        }

        public void SetCurrentFigure(IFigure figure)
        {
            _currentFigure = figure;
        }
    }
}