using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiPrestamos.Entities
{
    [Table("clientes", Schema = "transaccional")]
    public class Cliente
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        [Column("apellido")]
        public string Apellido { get; set; }

        [Column("numero_telefono")]
        public string NumeroTelefono { get; set; }

        [Column("departamento")]
        public string Departamento { get; set; }

        [Column("ciudad")]
        public string Ciudad { get; set; }

        [Column("direccion")]
        public string Direccion { get; set; }

        public ICollection<Prestamo> Préstamos { get; set; }

        [ForeignKey("usuario_id")]
        public IdentityUser User { get; set; }
    }
}

