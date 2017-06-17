using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Clayton.LojaVirtual.Dominio.Entidade;
using Clayton.LojaVirtual.Dominio.Entidade.Vitrine;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Validation;
using System;


namespace Clayton.LojaVirtual.Dominio.Repositorio
{
    public class EfDbContext: IdentityDbContext<Cliente>
    {
        public EfDbContext() : base("EFDbContext") { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Administrador> Administradores { get; set; }

        public static EfDbContext Create()
        {
            return new EfDbContext();
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<MarcaVitrine> MarcaVitrine { get; set; }
        public DbSet<ClubesNacionais> ClubesNacionais { get; set; }
        public DbSet<ClubesInternacionais> ClubesInternacionais { get; set; }
        public DbSet<FaixaEtaria> FaixasEtarias { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Modalidade> Modalidades { get; set; }
        public DbSet<SubGrupo> SubGrupos { get; set; }
        public DbSet<ProdutoVitrine> ProdutosVitrine { get; set; }
        public DbSet<ProdutoDetalhes> ProdutosDetalhes { get; set; }
        public DbSet<Cor> Cores { get; set; }
        public DbSet<Tamanho> Tamanhos { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<ProdutoModelo> ProdutoModelo { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ProdutoPedido> ProdutosPedido { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<Administrador>().ToTable("Administradores");

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entidade do tipo \"{0}\" no estado \"{1}\" tem os seguintes erros de validação:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
