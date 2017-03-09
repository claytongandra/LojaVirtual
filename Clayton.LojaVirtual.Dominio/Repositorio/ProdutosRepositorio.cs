using System.Collections.Generic;
using Clayton.LojaVirtual.Dominio.Entidade;

namespace Clayton.LojaVirtual.Dominio.Repositorio
{
    public class ProdutosRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();

        public IEnumerable<Produto> Produtos
        {
            get { return _context.Produtos; }
        }
    }
}
