using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiPrestamos.Entities
{
    [Table("solicitudes_credito", Schema = "transaccional")]
    public class SolicitudDeCredito
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Cliente")]
        [Column("cliente_id")]
        public int ClienteId { get; set; }

        [Column("monto_solicitado")]
        public decimal MontoSolicitado { get; set; }

        [ForeignKey("EstadoSolicitud")]
        [Column("estado_solicitud_id")]
        public int EstadoSolicitudId { get; set; }

        public Cliente Cliente { get; set; }

        public EstadoSolicitud EstadoSolicitud { get; set; }
    }
}
