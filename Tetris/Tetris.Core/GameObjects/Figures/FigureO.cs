using System.Collections.Generic;

namespace Tetris.Core.GameObjects.Figures
{
    public class FigureO : FigureBase
    {
        private const Color DefaultColor = Color.Orange;

        private static readonly int[,] _layout = new[,]
        {
            {1, 1},
            {1, 1}
        };

        public FigureO() : this(Offset.Empty) { }
        public FigureO(int x, int y) : this(new Offset(x, y)) { }
        public FigureO(Offset placement) : this(placement, DefaultColor) { }
        public FigureO(Offset placement, Color color)
            : base(placement, new PositionsCollection(color, new List<int[,]> 
        {   _layout, 
            _layout, 
            _layout, 
            _layout }))
        {
        }
    }
}