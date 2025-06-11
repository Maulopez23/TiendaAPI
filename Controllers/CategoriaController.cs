using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAPI.Data;
using TiendaAPI.Models;

namespace TiendaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly TiendaContext _context;

        public CategoriasController(TiendaContext context) 
        {
            _context = context;
        }

        [HttpGet] //
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias() // Endpoint para obtener todas las categorías
        {
            return await _context.Categorias.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> CrearCategoria(Categoria categoria) // Endpoint para crear una nueva categoría
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategorias), new { id = categoria.Id }, categoria);
        }


    }
}
