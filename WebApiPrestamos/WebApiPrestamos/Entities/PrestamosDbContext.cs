using Microsoft.EntityFrameworkCore;

namespace WebApiPrestamos.Entities
{
    public class PrestamosDbContext : DbContext
    {
        public PrestamosDbContext(DbContextOptions options) : base(options)
        {

        }

        //public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<PlanDePago> PlanDePagos { get; set; }
    }
}
