using Tetris.Contracts;

namespace Tetris.Implementation.Figures
{
    public class FigureO : FigureBase
    {
        private const Color DefaultColor = Color.Orange;

        public FigureO() : this(Offset.Empty) { }
        public FigureO(int x, int y) : this(new Offset(x,y)) { }
        public FigureO(Offset placement): this(placement, DefaultColor){}
        public FigureO(Offset placement, Color color)
            : base(placement, color, new[,]
            {
                {true, true},
                {true, true}
            })
        {
        }
    }
}