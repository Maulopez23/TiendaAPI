using Microsoft.EntityFrameworkCore;
using TiendaAPI.Models;

namespace TiendaAPI.Data
{
    public class TiendaContext(DbContextOptions<TiendaContext> options) : DbContext(options)
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ----------------------------
            // CONFIGURACIÓN DE CATEGORÍA
            // ----------------------------

            // Establece el índice único para el campo 'Nombre' de la categoría.
            modelBuilder.Entity<Categoria>()
                .HasIndex(c => c.Nombre)
                .IsUnique();

            // Define longitud máxima y obligatoriedad del campo 'Nombre'.
            modelBuilder.Entity<Categoria>()
                .Property(c => c.Nombre)
                .HasMaxLength(50)
                .IsRequired();


            // ----------------------------
            // CONFIGURACIÓN DE PRODUCTO
            // ----------------------------

            // El campo 'Nombre' del producto es obligatorio y tiene un límite de 100 caracteres.
            modelBuilder.Entity<Producto>()
                .Property(p => p.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            // Define la precisión del campo decimal 'Precio' (18 dígitos en total, 2 decimales).
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            // 'Stock' es obligatorio.
            modelBuilder.Entity<Producto>()
                .Property(p => p.Stock)
                .IsRequired();

            // Convierte explícitamente 'FechaCreacion' para garantizar compatibilidad en la base de datos.
            // No aplica transformación, pero garantiza que EF lo mapea correctamente.
            modelBuilder.Entity<Producto>()
                .Property(p => p.FechaCreacion)
                .HasConversion(v => v, v => v);


            // -------------------------------------
            // RELACIÓN ENTRE PRODUCTO Y CATEGORÍA
            // -------------------------------------

            // Configura la relación uno-a-muchos:
            // Un producto pertenece a una categoría (CategoriaId es clave foránea),
            // y una categoría puede tener muchos productos.
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict); // Previene eliminación en cascada accidental
        }

    }
}
