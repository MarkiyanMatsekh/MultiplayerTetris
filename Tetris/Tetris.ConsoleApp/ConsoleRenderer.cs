using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Contracts;

namespace Tetris.ConsoleApp
{


    public class ConsoleRenderer
    {
        private readonly char _brushSymbol;

        public ConsoleRenderer(char brushSymbol = ' ')
        {
            _brushSymbol = brushSymbol;
        }

        public void Render(ISprite sprite, Offset offset)
        {
            if (sprite == null)
                throw new ArgumentNullException("sprite");
            if (offset == null)
                throw new ArgumentNullException("offset");

            var consoleColorBackup = Console.BackgroundColor;

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

        public void Render(IFigure figure)
        {
            Render(figure, figure.Placement);
        }
    }

    public static class MatrixExtensions
    {
        public static T[,] Transpose<T>(this T[,] matrix)
        {
            var width = matrix.GetLength(1);
            var height = matrix.GetLength(0);

            var newMatrix = new T[width, height];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    newMatrix[i, j] = matrix[j, i];

            return newMatrix;
        }

        public static bool[,] ToBoolMatrix(this short[,] matrix)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            var boolMatrix = new bool[width,height];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    boolMatrix[i, j] = matrix[i, j] != 0;

            return boolMatrix;
        }

        public static short[,] ToShortMatrix(this bool[,] matrix)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            var shortMatrix = new short[width,height];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    shortMatrix[i, j] =  (short) (matrix[i, j] ? 1 : 0);

            return shortMatrix;
        }

    }
}
