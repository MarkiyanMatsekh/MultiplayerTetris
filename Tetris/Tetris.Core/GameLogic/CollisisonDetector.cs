using System;
using Tetris.Core.GameContracts;
using Tetris.Core.Helpers;

namespace Tetris.Core.GameLogic
{
    public class CollisionDetector
    {
        private readonly GameField _gameField;

        public CollisionDetector(GameField gameField)
        {
            _gameField = gameField;
        }

        public CollisionType EvaluateNextMove(MoveType move, IFigure figure)
        {
            var collision = CollisionType.None;
            IFigure movedFigure;
            switch (move)
            {
                case MoveType.RowAdded:
                    collision = ResolveRowAdded();
                    break;
                case MoveType.MoveRight:
                    movedFigure = figure.MoveRight();
                    collision = ResolveMoveRight(movedFigure);
                    break;
                case MoveType.MoveLeft:
                    movedFigure = figure.MoveLeft();
                    collision = ResolveMoveLeft(movedFigure);
                    break;
                case MoveType.MoveDown:
                    movedFigure = figure.MoveDown();
                    collision = ResolveMoveDown(movedFigure);
                    break;
                case MoveType.TossDown:
                    // for now do nothing
                    break;
                case MoveType.Rotate:
                    var rotatedFigure = figure.RotateClockwise();
                    collision = ResolveRotate(rotatedFigure);
                    break;
                default:
                    throw new NotImplementedException("unknown movement type: " + move);
            }

            return collision;
        }

        private CollisionType ResolveRotate(IFigure figure)
        {
            var collision = CollisionType.None;

            figure.ForEachNonEmptyCell((i, j) =>
            {
                var absoluteLeft = figure.Placement.Left + i;
                var absoluteTop = figure.Placement.Top + j;

                if (absoluteLeft > _gameField.Size.Width - 1)
                {
                    collision = CollisionType.Borders;
                    return false;
                }
                if (absoluteLeft < 0)
                {
                    collision = CollisionType.Borders;
                    return false;
                }
                if (absoluteTop < 0)
                {
                    collision = CollisionType.Borders;
                    return false;
                }
                if (absoluteTop > _gameField.Size.Height - 1)
                {
                    collision = CollisionType.Borders;
                    return false;
                }
                if (!_gameField.GroundView[absoluteLeft, absoluteTop].IsEmptyCell())
                {
                    collision = CollisionType.Ground;
                    return false;
                }
                return true;
            });
            return collision;
        }

        private CollisionType ResolveRowAdded()
        {
            return _gameField.GroundPeak < 1 ? CollisionType.Critical : CollisionType.None;
        }

        private CollisionType ResolveMoveRight(IFigure figure)
        {
            var collision = CollisionType.None;

            figure.ForEachNonEmptyCell((i, j) =>
            {
                var absoluteLeft = figure.Placement.Left + i;
                var absoluteTop = figure.Placement.Top + j;

                if (absoluteLeft > _gameField.Size.Width - 1)
                {
                    collision = CollisionType.Borders;
                    return false;
                }

                if (!_gameField.GroundView[absoluteLeft, absoluteTop].IsEmptyCell())
                {
                    collision = CollisionType.Ground;
                    return false;
                }
                return true;
            });
            return collision;
        }

        private CollisionType ResolveMoveLeft(IFigure figure)
        {
            var collision = CollisionType.None;

            figure.ForEachNonEmptyCell((i, j) =>
            {
                var absoluteLeft = figure.Placement.Left + i;
                var absoluteTop = figure.Placement.Top + j;

                if (absoluteLeft < 0)
                {
                    collision = CollisionType.Borders;
                    return false;
                }

                if (!_gameField.GroundView[absoluteLeft, absoluteTop].IsEmptyCell())
                {
                    collision = CollisionType.Ground;
                    return false;
                }
                return true;
            });
            return collision;
        }

        private CollisionType ResolveMoveDown(IFigure figure)
        {
            var collision = CollisionType.None;

            figure.ForEachNonEmptyCell((i, j) =>
            {
                var absoluteX = figure.Placement.Left + i;
                var absoluteY = figure.Placement.Top + j;

                if (absoluteY > _gameField.Size.Height - 1)
                {
                    collision = CollisionType.Ground;
                    return false;
                }

                if (absoluteY < 0)
                    return true;

                if (_gameField.GroundView[absoluteX, absoluteY].IsEmptyCell())
                    return true;

                collision = CollisionType.Ground;
                if (figure.Placement.Top == 1) // a little bad assumpotion that figures always start at 0, and thatn moves down by 1
                    collision = CollisionType.Critical;
                
                return false;
            });
            return collision;
        }
    }
}
