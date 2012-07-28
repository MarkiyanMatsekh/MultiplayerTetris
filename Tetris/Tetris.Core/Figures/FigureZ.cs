using System.Collections.Generic;
using Tetris.Core.GameObjects;

namespace Tetris.Core.Figures
{
    public class FigureZ : FigureBase
    {
        private const Color DefaultColor = Color.Red;
        private static readonly List<int[,]> _bitmap = new List<int[,]>
        {
            new [,] {{1,1,0},
                     {0,1,1},
                     {0,0,0}
                    },
            new [,] {{0,0,1},
                     {0,1,1},
                     {0,1,0}
                    },
            new [,] {{0,0,0},
                     {1,1,0},
                     {0,1,1}
                    },
            new [,] {{0,1,0},
                     {1,1,0},
                     {1,0,0}
                    },
        };

        public FigureZ() : this(Offset.Empty) { }
        public FigureZ(int x, int y) : this(new Offset(x,y)) { }
        public FigureZ(Offset placement) : this(placement, DefaultColor) { }
        public FigureZ(Offset placement, Color color)
            : base(placement, new PositionsCollection(color, _bitmap))
        {
        }
    }
}