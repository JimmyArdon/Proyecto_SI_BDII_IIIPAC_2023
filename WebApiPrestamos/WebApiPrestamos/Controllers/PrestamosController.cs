using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPrestamos.Dtos;
using WebApiPrestamos.Dtos.Prestamos;
using WebApiPrestamos.Entities;

namespace WebApiPrestamos.Controllers
{
    [Route("api/prestamos")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PrestamosController(ApplicationDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            this._mapper = mapper;
        }

        //Buscar prestamo por id
        [HttpGet("{prestamoId}")]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<PrestamoDto>>>> GetPrestamoAsync(int prestamoId)
        {
            
                // Buscar el préstamo en la base de datos
            var prestamoDb = await _dbContext.Prestamos
                    .Include(p => p.PlanesDePago)
                    .FirstOrDefaultAsync(p => p.Id == prestamoId);

            var prestamoDto = _mapper.Map<List<PrestamoDto>>(prestamoDb);

            return new ResponseDto<IReadOnlyList<PrestamoDto>>
            {
                Status = true,
                Data = prestamoDto
            };
        }

        //Crear un prestamo
        [HttpPost("crear-prestamo")]
        public async Task<ActionResult<ResponseDto<PrestamoDto>>> CrearPrestamo(CreatePrestamoDto dto)
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

        //Editar un prestamo
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<PrestamoDto>>> Put(PrestamoUpdateDto dto, int id)
        {
            var prestamoDb = await _dbContext.Prestamos.FirstOrDefaultAsync(x => x.Id == id);

            if (prestamoDb is null)
            {
                return NotFound(new ResponseDto<PrestamoDto>
                {
                    Status = false,
                    Message = $"No existe el préstamo: {id}",
                });
            }

            var clienteExiste = await _dbContext.Clientes.AnyAsync(x => x.Id == dto.ClienteId);

            if (!clienteExiste)
            {
                return NotFound(new ResponseDto<PrestamoDto>
                {
                    Status = false,
                    Message = $"No existe el cliente: {dto.ClienteId}",
                });
            }

            _mapper.Map<PrestamoUpdateDto, Prestamo>(dto, prestamoDb);

            _dbContext.Update(prestamoDb);
            await _dbContext.SaveChangesAsync();

            var prestamoDto = _mapper.Map<PrestamoDto>(prestamoDb);

            return Ok(new ResponseDto<PrestamoDto>
            {
                Status = true,
                Message = "Préstamo editado correctamente",
                Data = prestamoDto
            });
        }

        //Metodos

        //Creacion de un prestamo en base de datos
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

        //Generar el plan de pago para el prestamo
        private PlanDePago GenerarPlanDePago(Prestamo prestamo)
        {
            var cuotas = 12; // Ejemplo: 12 cuotas mensuales
            var tasaInteresMensual = 0.01; // Ejemplo: 1% de tasa de interés mensual
            var montoCuota = CalcularCuotaNivelada(prestamo.Monto, tasaInteresMensual, cuotas);
            var fechaVencimiento = prestamo.FechaSolicitud.AddMonths(1); // Primera cuota vence un mes después de la solicitud

            var planDePago = new PlanDePago
            {
                PrestamoId = prestamo.Id,
                NumeroCuota = cuotas,
                MontoCuota = montoCuota,
                FechaVencimiento = fechaVencimiento,
                EstadoPlanPagoId = 1 // Supongamos que 1 es el ID para "Activo"
            };

            for (int cuotaNumero = 1; cuotaNumero <= cuotas; cuotaNumero++)
            {
                // Crea la cuota y agrégala al plan de pago
                var cuota = new Cuota
                {
                    NumeroCuota = cuotaNumero,
                    Monto = montoCuota,
                    FechaVencimiento = fechaVencimiento,
                    EstadoCuotaId = 1 // Supongamos que 1 es el ID para "Pendiente"
                };

                planDePago.Cuotas.Add(cuota);

                // Actualiza la fecha de vencimiento para la próxima cuota
                fechaVencimiento = fechaVencimiento.AddMonths(1);
            }

            return planDePago;
        }

        //Calcular la cuota nivelada
        private decimal CalcularCuotaNivelada(decimal montoPrestamo, double tasaInteresMensual, int cuotas)
        {
            // Fórmula para calcular la cuota nivelada
            var tasa = Math.Pow(1 + tasaInteresMensual, cuotas);
            var cuota = (decimal)((double)montoPrestamo * tasaInteresMensual * tasa / (tasa - 1));

            return cuota;
        }

        private async Task GuardarPlanDePagoEnBaseDeDatosAsync(PlanDePago planDePago)
        {
            // Lógica para guardar el plan de pago en la base de datos
            _dbContext.PlanDePagos.Add(planDePago);
            await _dbContext.SaveChangesAsync();
        }
    }
}
