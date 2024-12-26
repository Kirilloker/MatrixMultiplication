using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MatrixMultiplication.Pages
{
    public class TheoryModel : PageModel
    {
        private readonly ILogger<TheoryModel> _logger;

        public TheoryModel(ILogger<TheoryModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("Вызван метод OnGet в модели TheoryModel.");
        }
    }
}
