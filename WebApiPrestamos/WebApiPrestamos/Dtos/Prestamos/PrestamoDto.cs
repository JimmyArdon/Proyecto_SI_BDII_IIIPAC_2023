namespace WebApiPrestamos.Dtos.Prestamos
{
    public class PrestamoDto
    {
        public decimal Monto { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public decimal TasaInteres { get; set; }
    }
}
