using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiPrestamos.Entities
{
    [Table("cuotas", Schema = "transaccional")]
    public class Cuota
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("PlanDePago")]
        [Column("plan_pago_id")]
        public int PlanDePagoId { get; set; }

        [Column("numero_cuota")]
        public int NumeroCuota { get; set; }

        [Column("monto")]
        public decimal Monto { get; set; }

        [Column("fecha_vencimiento")]
        public DateTime FechaVencimiento { get; set; }

        [ForeignKey("EstadoCuota")]
        [Column("estado_cuota_id")]
        public int EstadoCuotaId { get; set; }

        public PlanDePago PlanDePago { get; set; }

        public EstadoCuota EstadoCuota { get; set; }
    }

}
