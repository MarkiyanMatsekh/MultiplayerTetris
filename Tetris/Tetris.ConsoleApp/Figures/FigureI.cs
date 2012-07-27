using Tetris.Contracts;

namespace Tetris.Implementation.Figures
{
    public class FigureI : FigureBase
    {
        private const Color DefaultColor = Color.Red;

        public FigureI() : this(Offset.Empty) { }
        public FigureI(int x, int y) : this(new Offset(x, y)) { }
        public FigureI(Offset placement) : this(placement, DefaultColor) { }
        public FigureI(Offset placement, Color color)
            : base(placement, color, new[,]
            {
                {true, true,true, true}
            })
        {
        }
    }
}