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
            // CONFIGURACI�N DE CATEGOR�A
            // ----------------------------

            // Establece el �ndice �nico para el campo 'Nombre' de la categor�a.
            modelBuilder.Entity<Categoria>()
                .HasIndex(c => c.Nombre)
                .IsUnique();

            // Define longitud m�xima y obligatoriedad del campo 'Nombre'.
            modelBuilder.Entity<Categoria>()
                .Property(c => c.Nombre)
                .HasMaxLength(50)
                .IsRequired();


            // ----------------------------
            // CONFIGURACI�N DE PRODUCTO
            // ----------------------------

            // El campo 'Nombre' del producto es obligatorio y tiene un l�mite de 100 caracteres.
            modelBuilder.Entity<Producto>()
                .Property(p => p.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            // Define la precisi�n del campo decimal 'Precio' (18 d�gitos en total, 2 decimales).
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            // 'Stock' es obligatorio.
            modelBuilder.Entity<Producto>()
                .Property(p => p.Stock)
                .IsRequired();

            // Convierte expl�citamente 'FechaCreacion' para garantizar compatibilidad en la base de datos.
            // No aplica transformaci�n, pero garantiza que EF lo mapea correctamente.
            modelBuilder.Entity<Producto>()
                .Property(p => p.FechaCreacion)
                .HasConversion(v => v, v => v);


            // -------------------------------------
            // RELACI�N ENTRE PRODUCTO Y CATEGOR�A
            // -------------------------------------

            // Configura la relaci�n uno-a-muchos:
            // Un producto pertenece a una categor�a (CategoriaId es clave for�nea),
            // y una categor�a puede tener muchos productos.
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict); // Previene eliminaci�n en cascada accidental
        }

    }
}
