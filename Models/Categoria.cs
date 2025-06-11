using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; } // Identificador único de la categoría

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } // Nombre de la categoría, requerido y con un máximo de 50 caracteres

        public ICollection<Producto> Productos { get; set; } // Colección de productos asociados a esta categoría
    }
}
