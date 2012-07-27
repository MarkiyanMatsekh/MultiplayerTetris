using Tetris.Contracts;

namespace Tetris.Implementation.Figures
{
    public class FigureS : FigureBase
    {
        private const Color DefaultColor = Color.Azure;

        public FigureS() : this(Offset.Empty) { }
        public FigureS(int x, int y) : this(new Offset(x, y)) { }
        public FigureS(Offset placement) : this(placement, DefaultColor) { }
        public FigureS(Offset placement, Color color)
            : base(placement, color, new[,]
            {
                {false, true, true},
                {true, true, false}
            })
        {

        }
    }
}