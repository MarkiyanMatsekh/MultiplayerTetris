﻿using System.Collections.Generic;
using Tetris.Contracts;

namespace Tetris.Implementation.Figures
{
    public class FigureT : FigureBase
    {
        private const Color DefaultColor = Color.Yellow;
        private static readonly List<int[,]> _bitmap = new List<int[,]>
        {
            new [,] {{0,1,0},
                     {1,1,1},
                     {0,0,0}
                    },
            new [,] {{0,1,0},
                     {0,1,1},
                     {0,1,0}
                    },
            new [,] {{0,0,0},
                     {1,1,1},
                     {0,1,0}
                    },
            new [,] {{0,1,0},
                     {1,1,0},
                     {0,1,0}
                    },
        };

        public FigureT() : this(Offset.Empty) { }
        public FigureT(int x, int y) : this(new Offset(x,y)) { }
        public FigureT(Offset placement) : this(placement, DefaultColor) { }
        public FigureT(Offset placement, Color color)
            : base(placement, new PositionsCollection(color, _bitmap))
        {
        }
    }
}