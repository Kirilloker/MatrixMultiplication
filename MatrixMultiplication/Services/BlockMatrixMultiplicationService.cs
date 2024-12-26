namespace MatrixMultiplication.Web.Services
{
    public class BlockMatrixMultiplicationService
    {
        public int[][] MultiplyBlock(int[][] A, int[][] B)
        {
            int n = A.Length;    
            int p = A[0].Length;    
            int m = B[0].Length;    

            int blockSize = AutoComputeBlockSize(n, p, m);

            int[][] result = new int[n][];

            for (int i = 0; i < n; i++)
                result[i] = new int[m];

            for (int iBlock = 0; iBlock < n; iBlock += blockSize)
            {
                for (int kBlock = 0; kBlock < p; kBlock += blockSize)
                {
                    for (int jBlock = 0; jBlock < m; jBlock += blockSize)
                    {
                        int iMax = Math.Min(iBlock + blockSize, n);
                        int kMax = Math.Min(kBlock + blockSize, p);
                        int jMax = Math.Min(jBlock + blockSize, m);

                        for (int i = iBlock; i < iMax; i++)
                        {
                            for (int k = kBlock; k < kMax; k++)
                            {
                                int aVal = A[i][k]; 

                                for (int j = jBlock; j < jMax; j++)
                                    result[i][j] += aVal * B[k][j];
                            }
                        }
                    }
                }
            }

            return result;
        }

        private static int AutoComputeBlockSize(int n, int p, int m)
        {
            int maxDim = Math.Max(n, Math.Max(p, m));

            if (maxDim < 64)
                return 8;
            else if (maxDim < 256)
                return 16;
            else if (maxDim < 512)
                return 32;
            else if (maxDim < 1024)
                return 64;
            else
                return 128;
        }
    }
}
