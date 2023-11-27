using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiPrestamos.Entities
{
    [Table("estados_solicitud", Schema = "transaccional")]
    public class EstadoSolicitud
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("nombre")]
        public string Nombre { get; set; }
    }
}
