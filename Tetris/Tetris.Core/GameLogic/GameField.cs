using System;
using System.Collections.Generic;
using System.Linq;
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
            _peak = _size.Height;
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
                _peak = Math.Min(_peak, y);
            });

            var fullRows = FindFullRows();

            foreach (var rowNumber in fullRows)
                RemoveRow(rowNumber);

            var rowsDeleted = fullRows.Length;
            //todo: notify multiplayer that n rows were deleted
        }

        private void RemoveRow(int rowNumber)
        {
            for (int i = 0; i < _size.Width; i++)
            {
                for (int j = rowNumber; j > 0; j--)
                {
                    _sprite[i, j] = _sprite[i, j - 1];
                }
            }
            _peak++;
        }

        public void AddRows(int rowsCount)
        {
            if (rowsCount < 1)
                throw new ArgumentOutOfRangeException("rowsCount", "rowsCount should be a positive number");

            if (rowsCount > _size.Height)
                throw new ArgumentOutOfRangeException("rowsCount", "number of added rows cannot exceed the height of gamefield");

            _peak -= rowsCount;

            for (int i = 0; i < _size.Width; i++)
            {
                for (int j = rowsCount; j < _size.Height - rowsCount + 1; j++)
                {
                    if (j < 0)
                        throw new InvalidOperationException("Exceeded game field while adding rows. CollisionDetector should've handle this");

                    _sprite[i, j - rowsCount] = _sprite[i, j];
                }
            }

            var addedRowsColor = Color.Azure;

            for (int i = 0; i < _size.Width; i++)
            {
                for (int j = 0; j < rowsCount; j++)
                {
                    _sprite[i, _size.Height - rowsCount + j] = addedRowsColor;
                }
            }
        }

        private int[] FindFullRows()
        {
            var fullRows = new bool[_size.Height];
            for (int i = 0; i < fullRows.Length; i++)
            {
                fullRows[i] = true;
            }

            for (int i = 0; i < _size.Height; i++)
            {
                for (int j = 0; j < _size.Width; j++)
                {
                    if (_sprite[j, i].IsEmptyCell())
                    {
                        fullRows[i] = false;
                        break;
                    }
                }
            }

            var fullRowsIndecies = new List<int>();
            for (int i = 0; i < fullRows.Length; i++)
            {
                if (fullRows[i])
                    fullRowsIndecies.Add(i);
            }

            // first we process lower rows(i.e. with higher placement.Top value)
            return fullRowsIndecies.OrderByDescending(row => row).ToArray();
        }
    }
}