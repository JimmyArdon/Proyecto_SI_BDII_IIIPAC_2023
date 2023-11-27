namespace WebApiPrestamos.Dtos.Prestamos
{
    public class CreatePrestamoDto
    {
        public decimal Monto { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaSolicitud { get; set; }
    }
}
