using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MatrixMultiplication.Web.Models;
using MatrixMultiplication.Web.Services;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace MatrixMultiplication.Pages
{
    public class MatrixModel : PageModel
    {
        private readonly MatrixMultiplicationService _classicService;
        private readonly BlockMatrixMultiplicationService _blockService;
        private readonly ILogger<MatrixModel> _logger;

        public MatrixViewModel MatrixViewModel { get; set; }

        public MatrixModel(MatrixMultiplicationService classicService,
                           BlockMatrixMultiplicationService blockService,
                           ILogger<MatrixModel> logger)
        {
            if (classicService == null || blockService == null)
            {
                _logger?.LogError("Один из сервисов не зарегистрирован в DI.");
                throw new ArgumentNullException("Сервис не зарегистрирован в DI");
            }

            _classicService = classicService;
            _blockService = blockService;
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("Вызван метод OnGet.");
            MatrixViewModel = new MatrixViewModel();
        }

        public void OnPost()
        {
            try
            {
                _logger.LogInformation("Начало обработки POST-запроса для умножения матриц.");

                int rowsA = int.Parse(Request.Form["RowsA"]);
                int colsA = int.Parse(Request.Form["ColsA"]);
                int rowsB = int.Parse(Request.Form["RowsB"]);
                int colsB = int.Parse(Request.Form["ColsB"]);

                _logger.LogInformation("Параметры матриц получены: RowsA={RowsA}, ColsA={ColsA}, RowsB={RowsB}, ColsB={ColsB}.", rowsA, colsA, rowsB, colsB);

                int[][] A = GenerateMatrix(rowsA, colsA);
                int[][] B = GenerateMatrix(rowsB, colsB);

                var stopwatch = new Stopwatch();

                stopwatch.Start();
                var resultClassic = _classicService.Multiply(A, B);
                stopwatch.Stop();
                long timeClassic = stopwatch.ElapsedMilliseconds;

                _logger.LogInformation("Классическое умножение завершено за {TimeClassic} мс.", timeClassic);

                stopwatch.Restart();
                var resultBlock = _blockService.MultiplyBlock(A, B);
                stopwatch.Stop();
                long timeBlock = stopwatch.ElapsedMilliseconds;

                _logger.LogInformation("Блочное умножение завершено за {TimeBlock} мс.", timeBlock);

                MatrixViewModel = new MatrixViewModel
                {
                    MatrixA = A,
                    MatrixB = B,
                    ResultClassic = resultClassic,
                    ResultBlock = resultBlock,
                    TimeClassic = timeClassic,
                    TimeBlock = timeBlock
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обработке POST-запроса для умножения матриц.");
                throw;
            }
        }

        private int[][] GenerateMatrix(int rows, int cols)
        {
            _logger.LogInformation("Генерация матрицы размером {Rows}x{Cols}.", rows, cols);

            Random rnd = new Random();
            int[][] matrix = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new int[cols];
                for (int j = 0; j < cols; j++)
                {
                    matrix[i][j] = rnd.Next(1, 10);
                }
            }

            _logger.LogInformation("Генерация матрицы завершена.");
            return matrix;
        }
    }
}
