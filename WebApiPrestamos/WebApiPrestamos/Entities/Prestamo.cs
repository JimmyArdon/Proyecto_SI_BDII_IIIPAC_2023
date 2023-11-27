using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiPrestamos.Entities
{
    [Table("prestamos", Schema = "transaccional")]
    public class Prestamo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Cliente")]
        [Column("cliente_id")]
        public int ClienteId { get; set; }

        [Column("fecha_solicitud")]
        public DateTime FechaSolicitud { get; set; }

        [Column("monto")]
        public decimal Monto { get; set; }

        [Column("tasa_interes")]
        public decimal TasaInteres { get; set; }

        [ForeignKey("Usuario")]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [ForeignKey("EstadoSolicitud")]
        [Column("estado_solicitud_id")]
        public int EstadoSolicitudId { get; set; }

        public EstadoSolicitud EstadoSolicitud { get; set; }

        public ICollection<PlanDePago> PlanesDePago { get; set; }

        public Cliente Cliente { get; set; }

        public IdentityUser User { get; set; }
    }
}
