using Tetris.Core.GameObjects;

namespace Tetris.WebApp.Dto
{
    public class SizeDto
    {
        public int width { get; set; }
        public int height { get; set; }

        public SizeDto(Size size)
        {
            width = size.Width;
            height = size.Height;
        }
    }
}