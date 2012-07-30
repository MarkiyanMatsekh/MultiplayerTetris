using System;
using System.Collections.Generic;
using Tetris.Core.GameContracts;
using Tetris.Core.GameObjects;
using Tetris.Core.GameObjects.Figures;

namespace Tetris.Core.GameLogic
{
    public class MoveHandler
    {
        private readonly GameField _gameField;
        private readonly CollisionDetector _collisionDetector;
        private readonly IRenderer _renderer;
        private ISprite _lastGameFieldView;

        private static readonly Random _random = new Random();
        private static readonly List<FigureBase> _randomFigures = new List<FigureBase>
        {
            new FigureI(3, 0),
            new FigureJ(3, 0),
            new FigureL(3, 0),
            new FigureO(3, 0),
            new FigureS(3, 0),
            new FigureT(3, 0),
            new FigureZ(3, 0)
        };

        public MoveHandler(GameField gameField, IRenderer renderer)
        {
            _gameField = gameField;
            _collisionDetector = new CollisionDetector(_gameField);
            _renderer = renderer;
        }

        public void HandleMove(MoveType move)
        {
            bool moveHandled = false;

            CollisionType collision = _collisionDetector.EvaluateNextMove(move, _gameField.CurrentFigure);

            // var collisionOutcome = // choose what to do with this combination of collision and move

            switch (collision)
            {
                case CollisionType.Borders:
                    // dont allow this move
                    return;
                case CollisionType.Critical:
                    //TODO: indicate the end of the game!
                    return;
                    break;
                case CollisionType.Ground:
                    if (move == MoveType.MoveDown)
                    {
                        _gameField.AttachFigureToTheGround(_gameField.CurrentFigure);
                        var randomFigure = GetRandomFigure();
                        _gameField.SetCurrentFigure(randomFigure); // todo: this can also cause the end of the game. need to be prepared
                        
                        moveHandled = true;
                    }
                    if (move == MoveType.MoveLeft || move == MoveType.MoveRight || move == MoveType.Rotate)
                        return; // forbid this move
                    break;
            }

            if (!moveHandled)
            {
                switch (move)
                {
                    case MoveType.Rotate:
                        _gameField.SetCurrentFigure(_gameField.CurrentFigure.RotateClockwise());
                        break;
                    case MoveType.MoveLeft:
                        _gameField.SetCurrentFigure(_gameField.CurrentFigure.MoveLeft());
                        break;
                    case MoveType.MoveRight:
                        _gameField.SetCurrentFigure(_gameField.CurrentFigure.MoveRight());
                        break;
                    case MoveType.MoveDown:
                        _gameField.SetCurrentFigure(_gameField.CurrentFigure.MoveDown());
                        break;
                    case MoveType.TossDown:
                        // whether increase the timer or calculate where to put figure
                        break;
                    case MoveType.RowAdded:
                        _gameField.AddRows(1); // todo: change enum to classes and get numebr of rows
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("not supported move: " + move);
                }
            }

            var gameFieldView = _gameField.GetCurrentView();

            if (_lastGameFieldView == null)
                _renderer.Render(gameFieldView);
            else
                _renderer.RenderDiff(_lastGameFieldView, gameFieldView);

            _lastGameFieldView = gameFieldView;
        }

        public static IFigure GetRandomFigure()
        {
            return _randomFigures[_random.Next(_randomFigures.Count)];
        }
    }
}