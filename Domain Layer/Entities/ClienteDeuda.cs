namespace Domain_Layer.Entities
{
    public class ClienteDeuda
    {
        public decimal Deuda { get; set; }

        public Venta Venta { get; set; }
    }
}
