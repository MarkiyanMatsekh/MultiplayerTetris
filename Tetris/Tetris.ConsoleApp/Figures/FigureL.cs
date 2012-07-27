using System;
using Tetris.Contracts;

namespace Tetris.Implementation.Figures
{
    public class FigureL : FigureBase
    {
        private const Color DefaultColor = Color.Violet;

        public FigureL() : this(Offset.Empty) { }
        public FigureL(int x, int y) : this(new Offset(x, y)) { }
        public FigureL(Offset placement) : this(placement, DefaultColor) { }
        public FigureL(Offset placement, Color color)
            : base(placement, color, new[,]
            {
                {true, true, true, true},
                {true, false, false, false}
            })
        {
        }
    }
}