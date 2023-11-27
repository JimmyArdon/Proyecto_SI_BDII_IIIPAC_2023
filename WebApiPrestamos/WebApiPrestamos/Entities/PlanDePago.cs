using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiPrestamos.Entities
{
    [Table("planes_pago", Schema = "transaccional")]
    public class PlanDePago
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Prestamo")]
        [Column("prestamo_id")]
        public int PrestamoId { get; set; }

        [Column("numero_cuota")]
        public int NumeroCuota { get; set; }

        [Column("monto_cuota")]
        public decimal MontoCuota { get; set; }

        [Column("fecha_vencimiento")]
        public DateTime FechaVencimiento { get; set; }

        [ForeignKey("EstadoPlanPago")]
        [Column("estado_plan_pago_id")]
        public int EstadoPlanPagoId { get; set; }

        public Prestamo Préstamo { get; set; }

        public EstadoPlanPago EstadoPlanPago { get; set; }

        public List<Cuota> Cuotas { get; set; } = new List<Cuota>();
    }
}
