using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Clayton.LojaVirtual.Dominio.Entidade
{
    public class Produto
    {
        [HiddenInput(DisplayValue = false)]
        public int ProdutoId {  get; set; }

        [Required(ErrorMessage = "Informe o nome do produto")]
        [Display(Name = "Nome:")]
        public string Nome { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Informe descição do produto")]
        [Display(Name = "Descrição:")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o valor do produto")]
        [Range(0.01,double.MaxValue,ErrorMessage="Valor inválido")]
        [Display(Name = "Preço:")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Informe a categotia do produto")]
        [Display(Name = "Categoria:")]
        public string Categoria { get; set; }

        [Display(Name = "Imagem:")]
        public byte[] Imagem { get; set; }

        //[ScaffoldColumn(false)]
        public string ImagemMimeType { get; set; }
    }
}
