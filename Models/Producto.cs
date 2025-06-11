using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaAPI.Models
{
    public class Producto
    {
        public int Id { get; set; } // Identificador único del producto

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } // Nombre del producto (requerido y con un máximo de 100 caracteres)

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Precio { get; set; } // Precio del producto (requerido y debe ser mayor que 0)

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; } // Cantidad disponible en stock (requerido y no puede ser negativo)

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now; // Fecha de creación del producto (con valor por defecto de la fecha actual)

        [Required]
        public int CategoriaId { get; set; } // Identificador de la categoría a la que pertenece el producto (requerido)

        public Categoria Categoria { get; set; } // Navegación a la entidad Categoria 
    }
}
