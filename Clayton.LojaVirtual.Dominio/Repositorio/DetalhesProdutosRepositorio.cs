
using System.Linq;
using System.Collections.Generic;
using Clayton.LojaVirtual.Dominio.Entidade;

namespace Clayton.LojaVirtual.Dominio.Repositorio
{
    public class DetalhesProdutosRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();

        public IEnumerable<ProdutoDetalhes> Produtos
        {
            get { return _context.ProdutosDetalhes; }
        }


        public ProdutoDetalhes ObterProduto(string codigo, string corCodigo, string tamanhoCodigo)
        {
            return _context.ProdutosDetalhes.Single(p => p.ProdutoModeloCodigo == codigo
                    && p.CorCodigo == corCodigo && p.TamanhoCodigo == tamanhoCodigo);
        }

        //Salvar Produto - Alterar Produto
        public void Salvar(ProdutoDetalhes produto)
        {
            if (produto.ProdutoId == 0)
            {
                //Salvado
                _context.ProdutosDetalhes.Add(produto);
            }
            else
            {
                //Alteração
                _context.Entry(produto).State = System.Data.Entity.EntityState.Modified;
            }

            _context.SaveChanges();
        }


        //Excluir

        public ProdutoDetalhes Excluir(int produtoId)
        {

            ProdutoDetalhes prod = _context.ProdutosDetalhes.Find(produtoId);

            if (prod != null)
            {
                _context.ProdutosDetalhes.Remove(prod);
                _context.SaveChanges();
            }

            return prod;
        }
    }
}
