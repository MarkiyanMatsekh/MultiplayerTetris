using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Contracts;

namespace Tetris.ConsoleApp.classes
{
    public enum CollisionType { Critical, Borders, Ground, None }
    public enum MovementType { Rotate, MoveLeft, MoveRight, MoveDown, RowAdded }

    public class CollisionDetector
    {
        private readonly IGround _ground;
        private readonly IGameField _gameField;

        public CollisionDetector(IGameField gameField)
        {
            _gameField = gameField;
            _ground = gameField.Ground;
        }

        public CollisionType EvaluateNextMove(MovementType move, IFigure figure)
        {
            var collision = CollisionType.None;
            IFigure movedFigure;
            switch (move)
            {
                case MovementType.RowAdded:
                    collision = ResolveRowAdded();
                    break;
                case MovementType.MoveRight:
                    movedFigure = figure.MoveRight();
                    collision = ResolveMoveRight(movedFigure);
                    break;
                case MovementType.MoveLeft:
                    movedFigure = figure.MoveLeft();
                    collision = ResolveMoveLeft(movedFigure);
                    break;
                case MovementType.MoveDown:
                    movedFigure = figure.MoveDown();
                    collision = ResolveMoveDown(movedFigure);
                    break;
                case MovementType.Rotate:
                    //var rotatedFigure = figure.RotateClockwise();
                    //collision = ResolveRotate(figure);
                    break;
                default:
                    throw new NotImplementedException("unknown movement type: " + move);
            }

            return collision;
        }

        private CollisionType ResolveRowAdded()
        {
            return _ground.Peak < 1 ? CollisionType.Critical : CollisionType.None;
        }

        private CollisionType ResolveMoveRight(IFigure figure)
        {
            for (int i = 0; i < figure.Size.Width; i++)
            {
                for (int j = 0; j < figure.Size.Height; j++)
                {
                    if (figure[i,j].IsEmptyCell())
                        continue;

                    var absoluteLeft = figure.Placement.Left + i;
                    var absoluteTop = figure.Placement.Top + j;

                    if (absoluteLeft > _gameField.Size.Width - 1)
                        return CollisionType.Borders;

                    if (!_ground[absoluteLeft,absoluteTop].IsEmptyCell())
                        return CollisionType.Ground;
                }
            }
            return CollisionType.None;
        }

        private CollisionType ResolveMoveLeft(IFigure figure)
        {
            for (int i = 0; i < figure.Size.Width; i++)
            {
                for (int j = 0; j < figure.Size.Height; j++)
                {
                    if (figure[i,j].IsEmptyCell())
                        continue;

                    var absoluteLeft = figure.Placement.Left + i;
                    var absoluteTop = figure.Placement.Top + j;

                    if (absoluteLeft < 0)
                        return CollisionType.Borders;

                    if (!_ground[absoluteLeft,absoluteTop].IsEmptyCell())
                        return CollisionType.Ground;
                }
            }
            return CollisionType.None;
        }

        private CollisionType ResolveMoveDown(IFigure figure)
        {
            for (int i = 0; i < figure.Size.Width; i++)
            {
                for (int j = 0; j < figure.Size.Height; j++)
                {
                    if (figure[i,j].IsEmptyCell())
                        continue;

                    // exact coordinates of cell
                    var absoluteX = figure.Placement.Left + i;
                    var absoluteY = figure.Placement.Top + j;
                    if (absoluteY > _gameField.Size.Height - 1)
                        return CollisionType.Borders; // maybe return Ground?
                    
                    if (absoluteY < 0)
                        continue;

                    if (_ground[absoluteX, absoluteY].IsEmptyCell()) 
                        continue;

                    return absoluteX < 0 ? CollisionType.Critical : CollisionType.Ground;
                }
            }
            return CollisionType.None;
        }
    }
}
