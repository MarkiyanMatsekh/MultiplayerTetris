using System;
using System.Collections.Generic;
using Tetris.Contracts;

namespace Tetris.Implementation.Figures
{
    public class FigureL : FigureBase
    {
        private static readonly List<int[,]> _bitmap = new List<int[,]>
        {
            new [,] {{0,0,1},
                     {1,1,1},
                     {0,0,0}
                    },
            new [,] {{0,1,0},
                     {0,1,0},
                     {0,1,1}
                    },
            new [,] {{0,0,0},
                     {1,1,1},
                     {1,0,0}
                    },
            new [,] {{1,1,0},
                     {0,1,0},
                     {0,1,0}
                    },
        };

        private const Color DefaultColor = Color.Violet;

        public FigureL() : this(Offset.Empty) { }
        public FigureL(int x, int y) : this(new Offset(x, y)) { }
        public FigureL(Offset placement) : this(placement, DefaultColor) { }
        public FigureL(Offset placement, Color color)
            : base(placement, new PositionsCollection(color, _bitmap))
        {
        }
    }
}