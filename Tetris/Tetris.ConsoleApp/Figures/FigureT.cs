using Tetris.Contracts;

namespace Tetris.Implementation.Figures
{
    public class FigureT : FigureBase
    {
        private const Color DefaultColor = Color.Yellow;

        public FigureT() : this(Offset.Empty) { }
        public FigureT(int x, int y) : this(new Offset(x,y)) { }
        public FigureT(Offset placement) : this(placement, DefaultColor) { }
        public FigureT(Offset placement, Color color)
            : base(placement, color, new[,]
            {
                {false, true, false},
                {true, true, true}
            })
        {

        }
    }
}