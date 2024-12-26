using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MatrixMultiplication.Pages
{
    public class AuthorModel : PageModel
    {
        private readonly ILogger<AuthorModel> _logger;

        public AuthorModel(ILogger<AuthorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("Вызван метод OnGet в модели AuthorModel.");
        }
    }
}
