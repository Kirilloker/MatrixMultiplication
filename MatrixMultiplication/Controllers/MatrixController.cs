using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MatrixMultiplication.Web.Models;
using MatrixMultiplication.Web.Services;

namespace MatrixMultiplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatrixController : ControllerBase
    {
        private readonly MatrixMultiplicationService _classicService;
        private readonly BlockMatrixMultiplicationService _blockService;
        private readonly ILogger<MatrixController> _logger;

        public MatrixController(
            MatrixMultiplicationService classicService,
            BlockMatrixMultiplicationService blockService,
            ILogger<MatrixController> logger)
        {
            _classicService = classicService;
            _blockService = blockService;
            _logger = logger;
        }

        [HttpPost("multiply")]
        public IActionResult Multiply([FromBody] MatrixRequest request)
        {
            _logger.LogInformation("Запрос на умножение матриц получен.");

            int[][] matrixA = request.MatrixA;
            int[][] matrixB = request.MatrixB;

            var resultClassic = _classicService.Multiply(matrixA, matrixB);

            var resultBlock = _blockService.MultiplyBlock(matrixA, matrixB);

            return Ok(new
            {
                ClassicResult = resultClassic,
                BlockResult = resultBlock
            });
        }

        [HttpGet("/")]
        [HttpGet("/index")]
        public IActionResult Index()
        {
            _logger.LogInformation("Запрос на умножение матриц получен.");

            return Ok(new
            {
                ClassicResult = 1,
                BlockResult = 2
            });
        }
    }


    public class MatrixRequest
    {
        public int[][] MatrixA { get; set; }
        public int[][] MatrixB { get; set; }
    }
}
