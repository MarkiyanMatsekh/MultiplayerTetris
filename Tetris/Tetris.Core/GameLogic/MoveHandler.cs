using System;
using Tetris.Core.GameContracts;
using Tetris.Core.GameObjects;
using Tetris.Core.GameObjects.Figures;

namespace Tetris.Core.GameLogic
{
    public class MoveHandler
    {
        private readonly GameField _gameField;
        private readonly CollisionDetector _collisionDetector;
        private readonly IInputSerializer _inputSerializer;
        private readonly IRenderer _renderer;

        public MoveHandler(GameField gameField, IInputSerializer inputSerializer, IRenderer renderer)
        {
            _gameField = gameField;
            _collisionDetector = new CollisionDetector(_gameField);
            _inputSerializer = inputSerializer;
            _renderer = renderer;
        }

        public void HandleMove(MoveType move)
        {
            bool moveHandled = false;

            var collision = _collisionDetector.EvaluateNextMove(move, _gameField.CurrentFigure);

            switch (collision)
            {
                case CollisionType.Borders:
                    // dont allow this move
                    return;
                case CollisionType.Critical:
                    // todo: indicate the end of the game
                    return;
                    break;
                case CollisionType.Ground:
                    if (move == MoveType.MoveDown)
                    {
                        _gameField.Ground.AttachFigure(_gameField.CurrentFigure);
                        _gameField.SetCurrentFigure(new FigureJ(2,0)); // get from generator

                        moveHandled = true;
                    }
                    if (move == MoveType.MoveLeft || move == MoveType.MoveRight)
                        return;
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
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("move");
                }
            }

            _renderer.Render(_gameField.GetCurrentView());
        }
    }
}