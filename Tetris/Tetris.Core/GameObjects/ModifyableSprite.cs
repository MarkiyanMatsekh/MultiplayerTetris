using System.Collections.Generic;
using Tetris.Core.GameContracts;

namespace Tetris.Core.GameObjects.Figures
{
    public class ModifyableSprite : Sprite
    {
        public ModifyableSprite(Size size) : base(size){}
        public ModifyableSprite(Color[,] layout) : base(layout) {}
        public ModifyableSprite(Color fillColor, int[,] layout) : base(fillColor, layout) {}
        public ModifyableSprite(IDictionary<short, Color> colorMapping, short[,] layout) : base(colorMapping, layout) {}
        public ModifyableSprite(ISprite other) : base(other) {}

        public new Color this[int x, int y]
        {
            get { return _layout[x, y]; }
            set { _layout[x, y] = value; }
        }

    }
}