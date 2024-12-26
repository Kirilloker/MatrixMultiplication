namespace MatrixMultiplication.Web.Services
{
    public class MatrixMultiplicationService
    {
        public int[][] Multiply(int[][] A, int[][] B)
        {
            int n = A.Length;
            int m = B[0].Length;
            int p = B.Length;

            int[][] result = new int[n][];

            for (int i = 0; i < n; i++)
                result[i] = new int[m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int sum = 0;

                    for (int k = 0; k < p; k++)
                        sum += A[i][k] * B[k][j];

                    result[i][j] = sum;
                }
            }

            return result;
        }
    }
}
