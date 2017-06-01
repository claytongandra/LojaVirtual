
using System.Collections.Generic;
using Clayton.LojaVirtual.Dominio.Entidade.Vitrine;

namespace Clayton.LojaVirtual.Web.V2.Models
{
    public class ProdutosViewModel
    {
        public List<ProdutoVitrine> Produtos { get; set; }

        public string Titulo { get; set; }
    }

   
}