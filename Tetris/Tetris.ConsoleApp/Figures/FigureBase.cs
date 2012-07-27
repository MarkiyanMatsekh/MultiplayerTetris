using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tetris.ConsoleApp;
using Tetris.Contracts;

namespace Tetris.Implementation.Figures
{
    public abstract class FigureBase : IFigure
    {
        private readonly Offset _placement;
        private readonly ISprite _sprite;
        private readonly byte _positionNumber;
        private readonly PositionsCollection _positions;

        private class GenericFigure : FigureBase
        {
            public GenericFigure(PositionsCollection positions, byte positionNumber, Offset placement)
                : base(positions, positionNumber, placement) { }
        }

        private FigureBase(PositionsCollection positions, byte positionNumber, Offset placement)
        {
            _positions = positions;
            _positionNumber = positionNumber;
            _sprite = _positions.GetPositionByNumber(positionNumber);
            _placement = placement;
        }

        protected FigureBase(Offset placement, PositionsCollection positions)
        {
            _positions = positions;
            _placement = placement;
            _positionNumber = 0;
            _sprite = _positions.GetPositionByNumber(0);
        }

        public IFigure RotateClockwise()
        {
            return new GenericFigure(_positions, _positions.GetNextPosition(_positionNumber), _placement);
        }

        public Offset Placement
        {
            get { return _placement; }
        }

        public Size Size
        {
            get { return _sprite.Size; }
        }

        public Color this[int x, int y]
        {
            get { return _sprite[x, y]; }
        }

        protected class PositionsCollection
        {
            private const byte NumberOfPositions = 4;
            private readonly ISprite[] _positions;

            public PositionsCollection(Color filledColor, IList<int[,]> bitmap)
            {
                if (bitmap == null)
                    throw new ArgumentNullException("bitmap");
                if (bitmap.Count != NumberOfPositions)
                    throw new InvalidOperationException("there should be always 4 positions");

                _positions = new ISprite[NumberOfPositions];

                for (int i = 0; i < NumberOfPositions; i++)
                    _positions[i] = new Sprite(filledColor, bitmap[i]);
            }

            public ISprite GetPositionByNumber(byte number)
            {
                if (number < 0 || number > NumberOfPositions - 1)
                    throw new IndexOutOfRangeException();
                return _positions[number];
            }

            public byte GetNextPosition(byte pos)
            {
                var nextPos = (byte)((pos + 1) % NumberOfPositions);
                return nextPos;
            }
        }
    }
}