using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiPrestamos.Filters
{
    public class MiFiltro : IActionFilter
    {
        private readonly ILogger<MiFiltro> _logger;

        public MiFiltro(ILogger<MiFiltro> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Antes de ejecutar el metodo o acccion");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Despues de ejecutar el metodo o acccion");
        }
    }
}
