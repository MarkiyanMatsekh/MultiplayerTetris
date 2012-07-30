using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Core.GameLogic;
using Tetris.Core.GameObjects;

namespace Tetris.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new GameEngine(new Size(10, 10), 
                                        new ConsoleInputListener(), 
                                        new ConsoleRenderer(),
                                        () =>
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Game Over!");
                                            });

            engine.Start();

            var a = 5;
        }
    }
}
