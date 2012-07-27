using Tetris.Contracts;
using Tetris.Implementation.Figures;

namespace Tetris.ConsoleApp.classes
{
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