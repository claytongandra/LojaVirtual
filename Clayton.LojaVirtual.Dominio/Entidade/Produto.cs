using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Clayton.LojaVirtual.Dominio.Entidade
{
    public class Produto
    {
        [HiddenInput(DisplayValue = false)]
        public int ProdutoId {  get; set; }

        public string Nome { get; set; }

        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        public decimal Preco { get; set; }

        public string Categoria { get; set; }
    }
}
