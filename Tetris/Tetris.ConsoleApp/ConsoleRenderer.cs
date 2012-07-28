using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Core.GameContracts;
using Tetris.Core.GameObjects;
using Tetris.Core.Helpers;

namespace Tetris.ConsoleApp
{
    public class ConsoleRenderer : IRenderer
    {
        private readonly char _brushSymbol;

        public ConsoleRenderer(char brushSymbol = ' ')
        {
            _brushSymbol = brushSymbol;
        }

        public void Render(IFigure figure)
        {
            Render(figure, figure.Placement);
        }

        public void Render(ISprite sprite)
        {
            Render(sprite, Offset.Empty);
        }

        public void Render(ISprite sprite, Offset offset)
        {
            if (sprite == null)
                throw new ArgumentNullException("sprite");
            if (offset == null)
                throw new ArgumentNullException("offset");

            var consoleColorBackup = Console.BackgroundColor;

            Console.Clear(); // yeaks!

            var consoleBounds = new { Width = Console.WindowWidth, Height = Console.WindowHeight };
            for (var i = 0; i < sprite.Size.Width; i++)
            {
                for (var j = 0; j < sprite.Size.Height; j++)
                {
                    var x = offset.Left + i;
                    var y = offset.Top + j;

                    var brushBackground = sprite[i, j];

                    if (!((x >= 0) && (x < consoleBounds.Width) && (y >= 0)) || brushBackground == Color.Transparent)
                    {
                        continue;
                    }

                    Console.SetCursorPosition(x, y);
                    Console.BackgroundColor = brushBackground.ToConsoleColor();
                    Console.Write(_brushSymbol);
                }
            }

            Console.BackgroundColor = consoleColorBackup;
        }
    }
}
