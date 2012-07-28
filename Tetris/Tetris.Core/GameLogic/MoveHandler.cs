using System;
using Tetris.Core.GameContracts;
using Tetris.Core.GameObjects;

namespace Tetris.Core.GameLogic
{
    public class MoveHandler
    {
        private readonly IGameField _gameField;
        private readonly CollisionDetector _collisionDetector;
        private readonly IInputSerializer _inputSerializer;
        private readonly IRenderer _renderer;

        public MoveHandler(IGameField gameField, IInputSerializer inputSerializer, IRenderer renderer)
        {
            _gameField = gameField;
            _collisionDetector = new CollisionDetector(_gameField);
            _inputSerializer = inputSerializer;
            _renderer = renderer;
        }

        public void HandleMove(MoveType move)
        {
            var collision = _collisionDetector.EvaluateNextMove(move, _gameField.CurrentFigure);

            switch (collision)
            {
                case CollisionType.Borders:
                    // dont allow this move
                    return;
                case CollisionType.Critical:
                    // indicate the end of the game
                    break;
                case CollisionType.Ground:
                    // attach to the ground
                    break;
            }

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

            _renderer.Render(_gameField.GetCurrentView(),Offset.Empty);
        }
    }
}