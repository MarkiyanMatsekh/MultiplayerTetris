namespace Tetris.Core.Helpers
{
    public static class MatrixExtensions
    {
        public static T[,] Transpose<T>(this T[,] matrix)
        {
            var width = matrix.GetLength(1);
            var height = matrix.GetLength(0);

            var newMatrix = new T[width, height];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    newMatrix[i, j] = matrix[j, i];

            return newMatrix;
        }

        public static bool[,] ToBoolMatrix(this short[,] matrix)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            var boolMatrix = new bool[width,height];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    boolMatrix[i, j] = matrix[i, j] != 0;

            return boolMatrix;
        }

        public static short[,] ToShortMatrix(this bool[,] matrix)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            var shortMatrix = new short[width,height];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    shortMatrix[i, j] =  (short) (matrix[i, j] ? 1 : 0);

            return shortMatrix;
        }

    }
}