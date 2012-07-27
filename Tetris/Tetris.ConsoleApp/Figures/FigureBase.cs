using System;
using System.Collections.Generic;
using System.Linq;
using Tetris.ConsoleApp;
using Tetris.Contracts;

namespace Tetris.Implementation.Figures
{
    public class Sprite : ISprite
    {
        protected readonly Color[,] _layout;
        private readonly Size _size;

        public Sprite(Color[,] layout)
        {
            _layout = layout;
        }

        public Sprite(IDictionary<short, Color> colorMapping, short[,] layout)
        {
            if (colorMapping == null)
                throw new ArgumentNullException("colorMapping");
            if (layout == null)
                throw new ArgumentNullException("layout");

            _size = new Size(layout.GetLength(0), layout.GetLength(1));
            _layout = new Color[_size.Width, _size.Height];

            for (int i = 0; i < _size.Width; i++)
                for (int j = 0; j < _size.Height; j++)
                    _layout[i, j] = colorMapping[layout[i, j]];

        }

        public Size Size
        {
            get
            {
                return _size;
            }
        }

        public Color this[int x, int y]
        {
            get { return _layout[x, y]; }
        }
    }

    public abstract class FigureBase : Sprite, IFigure
    {
        private readonly Offset _placement;

        protected FigureBase(IDictionary<short, Color> colorMapping, short[,] layout, Offset placement)
            : base(colorMapping, layout)
        {
            _placement = placement;
        }

        protected FigureBase(Offset placement, Color emptyColor, Color filledColor, Boolean[,] bitmap)
            : this(new Dictionary<short, Color>
                   {
                       { 0, emptyColor }, 
                       { 1, filledColor }
                   },
                   bitmap.ToShortMatrix(),
                   placement)
        {
        }

        protected FigureBase(Offset placement, Color filledColor, Boolean[,] bitmap)
            : this(placement, Color.Transparent, filledColor, bitmap)
        {
        }

        public Offset Placement
        {
            get { return _placement; }
        }
    }
}