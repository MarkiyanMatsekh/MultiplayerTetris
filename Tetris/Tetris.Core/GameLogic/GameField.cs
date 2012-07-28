using Tetris.Core.GameContracts;
using Tetris.Core.GameObjects;
using Tetris.Core.GameObjects.Figures;

namespace Tetris.Core.GameLogic
{
    public interface IGameField : IUIElement
    {
        Size Size { get; }
        IGround Ground { get; }
        IFigure CurrentFigure { get; }
        void SetCurrentFigure(IFigure figure);
    }

    public class GameField : IGameField
    {
        private readonly Size _size;
        private readonly Ground _ground;
        private IFigure _currentFigure;

        public GameField(int width, int height)
            : this(new Size(width, height))
        {
        }

        public GameField(Size size)
        {
            _size = size;
            _ground = new Ground(_size);
            _currentFigure = new FigureI(3, 0);
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