using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Contracts;
using Tetris.Implementation.Figures;

namespace Tetris.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var renderer = new ConsoleRenderer();

            var figures = new FigureBase[]
            {
                new FigureI(),
                new FigureJ(),
                new FigureL(),
                new FigureO(),
                new FigureS(),
                new FigureT(),
                new FigureZ()
            };

            foreach (var figure in figures)
            {
                Console.Write(figure.GetType().Name);
                renderer.Render(figure, new Offset(0, Console.CursorTop + 1 ));
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
