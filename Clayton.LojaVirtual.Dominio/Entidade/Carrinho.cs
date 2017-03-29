using System.Linq;
using System.Collections.Generic;

namespace Clayton.LojaVirtual.Dominio.Entidade
{
    public class Carrinho
    {
        private readonly List<ItemCarrinho> _itensCarrinho = new List<ItemCarrinho>();
        //Adicionar
        public void AdicionarItem(Produto produto)
        {
            ItemCarrinho item = _itensCarrinho.FirstOrDefault(p => p.Produto.ProdutoId == produto.ProdutoId);

            if (item == null)
            {
                _itensCarrinho.Add(new ItemCarrinho
                {
                    Produto = produto,
                    Quantidade = 1
                });
            }
            else
            {
                item.Quantidade += 1;
            }
        }

        //Remover
        public void RemoverItem(Produto produto, int quantidade)
        {
            if (quantidade >= 1)
            {
                ItemCarrinho item = _itensCarrinho.FirstOrDefault(p => p.Produto.ProdutoId == produto.ProdutoId);

                item.Quantidade = quantidade;
            }
            else
            {
                _itensCarrinho.RemoveAll(l => l.Produto.ProdutoId == produto.ProdutoId);
            }

        }

        //Obter valor total
        public decimal ObterValorTotal()
        {
            return _itensCarrinho.Sum(e => e.Produto.Preco * e.Quantidade);
        }

        //Limpar carrinho
        public void LimparCarrinho()
        {
            _itensCarrinho.Clear();
        }

        //Iten carrinho
        public IEnumerable<ItemCarrinho> ItensCarrinho
        {
            get { return _itensCarrinho; }
        }
    }

    public class ItemCarrinho
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
