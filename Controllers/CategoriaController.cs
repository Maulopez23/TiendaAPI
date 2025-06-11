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
            return await _context.Categorias.ToListAsync(); // Retorna una lista de categorías
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> CrearCategoria(Categoria categoria) // Endpoint para crear una nueva categoría
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategorias), new { id = categoria.Id }, categoria); // Retorna 201 Created con la ubicación del nuevo recurso
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id) // Endpoint para eliminar una categoría por ID
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return NoContent(); // Retorna 204 No Content si la eliminación fue exitosa

        }
}
