using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAPI.Data;
using TiendaAPI.DTOs;
using TiendaAPI.Models;

namespace TiendaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly TiendaContext _context;
        private readonly ILogger<ProductosController> _logger; // Logger para registrar información y errores

        public ProductosController(TiendaContext context, ILogger<ProductosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDetalleDTO>>> GetProductos() // Endpoint para obtener todos los productos
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .Select(p => new ProductoDetalleDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    CategoriaNombre = p.Categoria.Nombre
                })
                .ToListAsync();

            return Ok(productos); // Retornar 200 OK con la lista de productos
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDetalleDTO>> GetProducto(int id) // Endpoint para obtener un producto por ID
        {
            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .Where(p => p.Id == id)
                .Select(p => new ProductoDetalleDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    CategoriaNombre = p.Categoria.Nombre
                })
                .FirstOrDefaultAsync();

            if (producto == null)
                return NotFound();

            return Ok(producto); // Retornar 200 OK con el producto encontrado
        }

        [HttpPost]
        public async Task<ActionResult> CrearProducto(ProductoDTO dto) // Endpoint para crear un nuevo producto
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Stock = dto.Stock,
                CategoriaId = dto.CategoriaId
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, ProductoDTO dto) // Endpoint para actualizar un producto existente
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound(); // Si el producto no existe, retornar 404 Not Found

            producto.Nombre = dto.Nombre;
            producto.Precio = dto.Precio;
            producto.Stock = dto.Stock;
            producto.CategoriaId = dto.CategoriaId;

            await _context.SaveChangesAsync();
            return NoContent(); // Retornar 204 No Content si la actualización fue exitosa
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id) // Endpoint para eliminar un producto
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound(); // Si el producto no existe, retornar 404 Not Found

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent(); // Retornar 204 No Content si la eliminación fue exitosa
        }
    }
}
