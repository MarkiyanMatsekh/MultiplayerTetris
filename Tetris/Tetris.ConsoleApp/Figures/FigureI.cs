using System.Collections.Generic;
using Tetris.Contracts;

namespace Tetris.Implementation.Figures
{
    public class FigureI : FigureBase
    {
        private static readonly List<int[,]> _bitmap = new List<int[,]>
        {
            new [,] {{0,1,0,0},
                     {0,1,0,0},
                     {0,1,0,0},
                     {0,1,0,0}
                    },
            new [,] {{0,0,0,0},
                     {0,0,0,0},
                     {1,1,1,1},
                     {0,0,0,0}
                    },
            new [,] {{0,0,1,0},
                     {0,0,1,0},
                     {0,0,1,0},
                     {0,0,1,0}
                    },
            new [,] {{0,0,0,0},
                     {1,1,1,1},
                     {0,0,0,0},
                     {0,0,0,0}
                    }
        };

        private const Color DefaultColor = Color.Red;

        public FigureI() : this(Offset.Empty) { }
        public FigureI(int x, int y) : this(new Offset(x, y)) { }
        public FigureI(Offset placement) : this(placement, DefaultColor) { }
        public FigureI(Offset placement, Color color) : base(placement, new PositionsCollection(color, _bitmap))
        {
        }
    }
}