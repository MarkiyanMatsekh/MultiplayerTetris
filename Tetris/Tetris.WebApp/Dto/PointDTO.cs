using Tetris.Core.GameObjects;

namespace Tetris.WebApp
{
    public class PointDTO
    {
        public int x { get; set; }
        public int y { get; set; }
        public string color { get; set; }

        public PointDTO() { }
        public PointDTO(int x, int y, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color.ToString().ToLowerInvariant();
        }
    }
}