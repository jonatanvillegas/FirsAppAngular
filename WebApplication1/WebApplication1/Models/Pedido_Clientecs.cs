namespace WebApplication1.Models
{
    public class Pedido_Clientecs
    {
        public int Id { get; set; }
        public int Cliente_id { get; set; }
        public int Producto_id { get; set; }
        public int Cantidad_Pro { get; set; }
        public string Fecha { get; set; }
        public int Empleado_Id { get; set; }
    }
}
