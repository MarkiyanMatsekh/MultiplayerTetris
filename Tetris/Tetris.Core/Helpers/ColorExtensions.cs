using System;
using System.Collections.Generic;
using Tetris.Core.GameObjects;

namespace Tetris.Core.Helpers
{
    public static class ColorExtensions
    {
        private static readonly Dictionary<Color, ConsoleColor> _colorMapping = new Dictionary<Color, ConsoleColor>()
        {
            {Color.Azure, ConsoleColor.Cyan},
            {Color.Black, ConsoleColor.Black},
            {Color.Blue, ConsoleColor.Blue},
            {Color.Gray, ConsoleColor.Gray},
            {Color.Green, ConsoleColor.Green},
            {Color.Orange, ConsoleColor.DarkYellow},
            {Color.Red, ConsoleColor.Red},
            {Color.Transparent, ConsoleColor.Black},
            {Color.White, ConsoleColor.White},
            {Color.Violet, ConsoleColor.Magenta},
            {Color.Yellow, ConsoleColor.Yellow}
        };

        public static ConsoleColor ToConsoleColor(this Color color)
        {
            if (!_colorMapping.ContainsKey(color))
                throw new InvalidOperationException("not supported color: " + color);
            return _colorMapping[color];
        }

        public static bool IsEmptyCell(this Color color)
        {
            return color == Color.Transparent;
        }
    }
}