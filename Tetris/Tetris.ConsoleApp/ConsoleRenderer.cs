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

            for (int i = 0; i < sprite.Size.Width; i++)
            {
                for (int j = 0; j < sprite.Size.Height; j++)
                {
                    Console.SetCursorPosition(offset.Left + i,offset.Top + j );
                    Console.BackgroundColor = sprite[i, j].ToConsoleColor();
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
