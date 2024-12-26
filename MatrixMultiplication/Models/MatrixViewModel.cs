namespace MatrixMultiplication.Web.Models
{
    public class MatrixViewModel
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int[][] MatrixA { get; set; }
        public int[][] MatrixB { get; set; }
        public int[][] ResultClassic { get; set; }
        public int[][] ResultBlock { get; set; }
        public long TimeClassic { get; set; }
        public long TimeBlock { get; set; }
    }
}
