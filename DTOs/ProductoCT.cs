namespace TiendaAPI.DTOs 
{
    public class ProductoDTO
    {
        public string Nombre { get; set; } // Nombre del producto
        public decimal Precio { get; set; } // Precio del producto
        public int Stock { get; set; } // Cantidad disponible en stock
        public int CategoriaId { get; set; } // Identificador de la categoría a la que pertenece el producto    
    }
}
