using System;
using System.Collections.Generic;
using Tetris.Core.GameContracts;

namespace Tetris.Core.GameObjects.Figures
{
    public class Sprite : ISprite
    {
        protected readonly Color[,] _layout;
        private readonly Size _size;

        public Sprite(Size size)
        {
            _size = size;
            _layout = new Color[size.Width, size.Height];
            
            // just ot ensure
            for (int i = 0; i < _size.Width; i++)
                for (int j = 0; j < _size.Height; j++)
                    _layout[i,j] = Color.Transparent;
        }
        public Sprite(Color[,] layout)
        {
            _size = new Size(layout.GetLength(0), layout.GetLength(1));
            _layout = layout;
        }
        public Sprite(Color fillColor, int[,] layout)
        {
            if (layout == null)
                throw new ArgumentNullException("layout");

            _size = new Size(layout.GetLength(0), layout.GetLength(1));
            _layout = new Color[_size.Width, _size.Height];

            for (int i = 0; i < _size.Width; i++)
                for (int j = 0; j < _size.Height; j++)
                    _layout[i, j] = layout[i, j] != 0 ? fillColor : Color.Transparent;

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
        protected Sprite(ISprite other)
        {
            _size = other.Size;
            _layout = new Color[_size.Width, _size.Height];
            for (int i = 0; i < _size.Width; i++)
                for (int j = 0; j < _size.Height; j++)
                    _layout[i, j] = other[i, j];
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
}