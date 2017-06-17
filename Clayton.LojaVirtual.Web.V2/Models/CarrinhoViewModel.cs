using Clayton.LojaVirtual.Dominio.Entidade;

namespace Clayton.LojaVirtual.Web.V2.Models
{
    public class CarrinhoViewModel
    {
        public Carrinho Carrinho { get; set; }
        public string ReturnUrl { get; set; }
    }
}