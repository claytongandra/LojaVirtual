using System.Collections.Generic;
using Clayton.LojaVirtual.Dominio.Entidade;

namespace Clayton.LojaVirtual.Web.Models
{
    public class ProdutosViewModel
    {
        public IEnumerable<Produto> Produtos { get; set; }
        public Paginacao Paginacao { get; set; }
        public string CategoriaAtual { get; set; }
        
    }
}