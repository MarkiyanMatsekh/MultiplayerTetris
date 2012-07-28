using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Core.GameContracts;

namespace Tetris.Core.Helpers
{
    public static class SpriteExtensions
    {
        public static void ForEachCell(this ISprite figure, Func<int, int, bool> action)
        {
            for (int i = 0; i < figure.Size.Width; i++)
            {
                for (int j = 0; j < figure.Size.Height; j++)
                {
                    if (!action(i, j))
                        return;
                }
            }
        }

        public static void ForEachNonEmptyCell(this ISprite figure, Func<int, int, bool> action)
        {
            figure.ForEachCell((i, j) => figure[i, j].IsEmptyCell() || action(i, j));
        }

        public static void ForEachNonEmptyCell(this ISprite figure, Action<int, int> action)
        {
            figure.ForEachCell((i, j) =>
            {
                if (!figure[i, j].IsEmptyCell())
                    action(i, j);
            });
        }

        public static void ForEachCell(this ISprite figure, Action<int, int> action)
        {
            for (int i = 0; i < figure.Size.Width; i++)
            {
                for (int j = 0; j < figure.Size.Height; j++)
                {
                    action(i, j);
                }
            }
        }

    }
}
