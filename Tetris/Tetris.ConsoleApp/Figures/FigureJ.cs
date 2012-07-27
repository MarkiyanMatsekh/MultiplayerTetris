using Tetris.Contracts;

namespace Tetris.Implementation.Figures
{
    public class FigureJ : FigureBase
    {
        private const Color DefaultColor = Color.Green;

        public FigureJ() : this(Offset.Empty) { }
        public FigureJ(int x, int y) : this(new Offset(x,y)) { }
        public FigureJ(Offset placement) : this(placement, DefaultColor) { }
        public FigureJ(Offset placement, Color color)
            : base(placement, color, new[,]
            {
                {true, true, true, true},
                {false, false, false, true}
            })
        {

        }
    }
}