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
        private Dictionary<ConsoleKey, MovementType> _mapping = new Dictionary<ConsoleKey, MovementType>()
        {
            {ConsoleKey.LeftArrow, MovementType.MoveLeft},
            {ConsoleKey.RightArrow, MovementType.MoveRight},
            {ConsoleKey.DownArrow, MovementType.MoveDown}
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

    public class GameField : IGameField
    {
        private readonly Size _size;
        private readonly Ground _ground;
        private IFigure _currentFigure;

        public GameField(int i, int i1)
        {
            _size = new Size(i, i1);
            _ground = new Ground(_size);
        }

        public ISprite GetCurrentView()
        {
            var sprite = new ModifyableSprite(_ground.GetCurrentView());

            for (int i = 0; i < _currentFigure.Size.Width; i++)
            {
                for (int j = 0; j < _currentFigure.Size.Height; j++)
                {
                    var x = _currentFigure.Placement.Left + i;
                    var y = _currentFigure.Placement.Top + j;

                    if (x < 0 || y < 0 || x > _size.Width - 1 || y > _size.Height - 1)
                        continue;

                    sprite[x, y] = CurrentFigure[i, j];
                }
            }

            return sprite;
        }

        public Size Size
        {
            get { return _size; }
        }

        public IGround Ground
        {
            get { return _ground; }
        }

        public IFigure CurrentFigure
        {
            get { return _currentFigure; }
        }

        public void SetCurrentFigure(IFigure figure)
        {
            _currentFigure = figure;
        }
    }
}
