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
        const byte widthBorderOffset = 1;
        const byte heightBorderOffset = 1;

        private readonly char _brushSymbol;

        public ConsoleRenderer(char brushSymbol = ' ')
        {
            _brushSymbol = brushSymbol;
            Console.CursorVisible = false;
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

            var width = sprite.Size.Width;
            var height = sprite.Size.Height;


            for (int i = widthBorderOffset; i < width + widthBorderOffset; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write('-');
                Console.SetCursorPosition(i, height + heightBorderOffset);
                Console.Write('-');
            }
            for (int j = heightBorderOffset; j < height + heightBorderOffset; j++)
            {
                Console.SetCursorPosition(0, j);
                Console.Write('|');
                Console.SetCursorPosition(width + widthBorderOffset, j);
                Console.Write('|');
            }
            Console.SetCursorPosition(0, 0);
            Console.Write('/');
            Console.SetCursorPosition(0, width + widthBorderOffset);
            Console.Write('\\');
            Console.SetCursorPosition(height + heightBorderOffset, 0);
            Console.Write('\\');
            Console.SetCursorPosition(height + heightBorderOffset, width + widthBorderOffset);
            Console.Write('/');


            var consoleBounds = new { Width = Console.WindowWidth, Height = Console.WindowHeight };
            for (var i = 0; i < sprite.Size.Width; i++)
            {
                for (var j = 0; j < sprite.Size.Height; j++)
                {
                    var x = offset.Left + i + widthBorderOffset;
                    var y = offset.Top + j + heightBorderOffset;

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

        public void RenderDiff(ISprite oldSprite, ISprite newSprite)
        {
            RenderDiff(oldSprite, newSprite, Offset.Empty);
        }

        public void RenderDiff(ISprite oldSprite, ISprite newSprite, Offset offset)
        {
            if (oldSprite.Size != newSprite.Size)
                throw new InvalidOperationException("cannot draw diff of 2 sprites of different sizes");

            var consoleColorBackup = Console.BackgroundColor;

            var consoleBounds = new { Width = Console.WindowWidth, Height = Console.WindowHeight };
            for (var i = 0; i < newSprite.Size.Width; i++)
            {
                for (var j = 0; j < newSprite.Size.Height; j++)
                {
                    if (oldSprite[i, j] == newSprite[i, j])
                        continue;

                    var x = offset.Left + i + widthBorderOffset;
                    var y = offset.Top + j + heightBorderOffset;

                    var brushBackground = newSprite[i, j];

                    if ((x < 0) || (x >= consoleBounds.Width) || (y < 0) || (y >= consoleBounds.Height))
                        continue;

                    Console.SetCursorPosition(x, y);
                    Console.BackgroundColor = brushBackground.ToConsoleColor();
                    Console.Write(_brushSymbol);
                }
            }

            Console.BackgroundColor = consoleColorBackup;
        }
    }
}
