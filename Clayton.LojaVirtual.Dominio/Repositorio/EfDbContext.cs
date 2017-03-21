using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Clayton.LojaVirtual.Dominio.Entidade;

namespace Clayton.LojaVirtual.Dominio.Repositorio
{
    public class EfDbContext: DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Administrador> Administradores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<Administrador>().ToTable("Administradores");
        }
    }
}
