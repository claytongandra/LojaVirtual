using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Clayton.LojaVirtual.Dominio.Entidade
{
    public class Produto
    {
        [HiddenInput(DisplayValue = false)]
        public int ProdutoId {  get; set; }

        [Required(ErrorMessage = "Informe o nome do produto")]
        public string Nome { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Informe descição do produto")]
        public string Descricao { get; set; }

          [Required(ErrorMessage = "Informe o valor do produto")]
        [Range(0.01,double.MaxValue,ErrorMessage="Valor inválido")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Informe a categotia do produto")]
        public string Categoria { get; set; }
    }
}
