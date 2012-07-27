using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Contracts;

namespace Tetris.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var rend = new ConsoleRenderer();
            var sprite = new Sprite(new Dictionary<byte, Color> {{0, Color.Transparent}, {1, Color.Red}},
                                    new byte[,]{
                                                   {0, 1, 0},
                                                   {0, 1, 0},
                                                   {0, 1, 0},
                                                   {0, 0, 0},
                                                   {0, 1, 0}
                                               }.Transpose());
            rend.Render(sprite, new Offset(3,3));
        }
    }
}
