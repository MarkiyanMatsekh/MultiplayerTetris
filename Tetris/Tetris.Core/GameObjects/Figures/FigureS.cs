using System.Collections.Generic;

namespace Tetris.Core.GameObjects.Figures
{
    public class FigureS : FigureBase
    {
        private const Color DefaultColor = Color.Azure;
        private static readonly List<int[,]> _bitmap = new List<int[,]>
        {
            new [,] {{0,1,1},
                     {1,1,0},
                     {0,0,0}
                    },
            new [,] {{0,1,0},
                     {0,1,1},
                     {0,0,1}
                    },
            new [,] {{0,0,0},
                     {0,1,1},
                     {1,1,0}
                    },
            new [,] {{1,0,0},
                     {1,1,0},
                     {0,1,0}
                    },
        };

        public FigureS() : this(Offset.Empty) { }
        public FigureS(int x, int y) : this(new Offset(x, y)) { }
        public FigureS(Offset placement) : this(placement, DefaultColor) { }
        public FigureS(Offset placement, Color color)
            : base(placement, new PositionsCollection(color, _bitmap))
        {
        }
    }
}