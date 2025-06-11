using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; } // Identificador �nico de la categor�a

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } // Nombre de la categor�a, requerido y con un m�ximo de 50 caracteres

        public ICollection<Producto> Productos { get; set; } // Colecci�n de productos asociados a esta categor�a
    }
}
