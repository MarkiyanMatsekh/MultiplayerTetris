using Tetris.ConsoleApp;
using Tetris.Contracts;

namespace Tetris.Implementation.Figures
{
    public class FigureZ : FigureBase
    {
        private const Color DefaultColor = Color.Red;

        public FigureZ() : this(Offset.Empty) { }
        public FigureZ(int x, int y) : this(new Offset(x,y)) { }
        public FigureZ(Offset placement) : this(placement, DefaultColor) { }
        public FigureZ(Offset placement, Color color = Color.Blue)
            : base(placement, color, new[,]
            {
                {true, true, false},
                {false, true, true}
            })
        {
        }
    }
}