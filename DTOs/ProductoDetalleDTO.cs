namespace TiendaAPI.DTOs
{
    public class ProductoDetalleDTO
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } // Nombre del producto
        public decimal Precio { get; set; } // Precio del producto
        public int Stock { get; set; } // Cantidad disponible en stock
        public string CategoriaNombre { get; set; } // Nombre de la categoría a la que pertenece el producto
    }
}
