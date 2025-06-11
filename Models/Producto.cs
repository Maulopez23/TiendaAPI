using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaAPI.Models
{
    public class Producto
    {
        public int Id { get; set; } // Identificador �nico del producto

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } // Nombre del producto

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Precio { get; set; } // Precio del producto

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; } // Cantidad disponible en stock

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now; // Fecha de creaci�n del producto

        [Required]
        public int CategoriaId { get; set; } // Identificador de la categor�a a la que pertenece el producto

        public Categoria Categoria { get; set; } // Navegaci�n a la entidad Categoria
    }
}
