using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.ConsoleApp.classes;
using Tetris.Contracts;
using Tetris.Implementation.Figures;

namespace Tetris.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var listener = new UserInputListener();

            listener.Start();
        }
    }
}
