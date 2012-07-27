using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Contracts;
using Tetris.Implementation.Figures;

namespace Tetris.ConsoleApp.classes
{
    public class UserInputListener
    {
        private readonly Dictionary<ConsoleKey, MovementType> _mapping = new Dictionary<ConsoleKey, MovementType>()
        {
            {ConsoleKey.LeftArrow, MovementType.MoveLeft},
            {ConsoleKey.RightArrow, MovementType.MoveRight},
            {ConsoleKey.DownArrow, MovementType.MoveDown},
            {ConsoleKey.Spacebar, MovementType.Rotate}
        };

        public void Start()
        {
            var gameField = new GameField(10, 10);
            var collisionDetector = new CollisionDetector(gameField);
            var renderer = new ConsoleRenderer();

            gameField.SetCurrentFigure(new FigureJ(0, 3));
            renderer.Render(gameField.GetCurrentView(), Offset.Empty);

            while (true)
            {
                try
                {
                    var key = Console.ReadKey();
                    var move = _mapping[key.Key];
                    var collision = collisionDetector.EvaluateNextMove(move, gameField.CurrentFigure);

                    if (collision == CollisionType.Borders)
                        continue;

                    switch (key.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            gameField.SetCurrentFigure(gameField.CurrentFigure.MoveLeft());
                            break;
                        case ConsoleKey.RightArrow:
                            gameField.SetCurrentFigure(gameField.CurrentFigure.MoveRight());
                            break;
                        case ConsoleKey.DownArrow:
                            gameField.SetCurrentFigure(gameField.CurrentFigure.MoveDown());
                            break;
                        case ConsoleKey.Spacebar:
                            gameField.SetCurrentFigure(gameField.CurrentFigure.RotateClockwise());
                            break;

                    }

                    renderer.Clear();
                    renderer.Render(gameField.GetCurrentView(), Offset.Empty);
                }
                catch (Exception ex)
                {
                    renderer.Clear();
                    Console.WriteLine("error occured: {0}", ex.Message);
                    continue;
                }
            }
        }
    }
}
