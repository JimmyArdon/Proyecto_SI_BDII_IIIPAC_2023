using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPrestamos.Dtos.Prestamos;
using WebApiPrestamos.Entities;

namespace WebApiPrestamos.Controllers
{
    [Route("api/prestamos")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly PrestamosDbContext _dbContext;
        public PrestamosController(PrestamosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("crear-prestamo")]
        public async Task<IActionResult> CrearPrestamo(CreatePrestamoDto dto)
        {
            try
            {
                // Lógica para crear el préstamo en la base de datos
                var prestamo = await CrearPrestamoEnBaseDeDatosAsync(dto);

                // Genera el plan de pago
                var planDePago = GenerarPlanDePago(prestamo);

                // Guarda el plan de pago en la base de datos
                await GuardarPlanDePagoEnBaseDeDatosAsync(planDePago);

                return Ok(new
                {
                    Prestamo = prestamo,
                    PlanDePago = planDePago
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        private async Task<Prestamo> CrearPrestamoEnBaseDeDatosAsync(CreatePrestamoDto dto)
        {
            // Lógica para crear el préstamo en la base de datos
            var nuevoPrestamo = new Prestamo
            {

            };

            _dbContext.Prestamos.Add(nuevoPrestamo);
            await _dbContext.SaveChangesAsync();

            return nuevoPrestamo;
        }

        private PlanDePago GenerarPlanDePago(Prestamo prestamo)
        {
            // Lógica para generar el plan de pago (como se explicó anteriormente)
            // ...

            return null;
        }

        private async Task GuardarPlanDePagoEnBaseDeDatosAsync(PlanDePago planDePago)
        {
            // Lógica para guardar el plan de pago en la base de datos
            _dbContext.PlanDePagos.Add(planDePago);
            await _dbContext.SaveChangesAsync();
        }
    }
}
