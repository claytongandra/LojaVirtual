using System.Web.Mvc;
using System.Collections.Generic;
using Clayton.LojaVirtual.Dominio.Dto;
using Clayton.LojaVirtual.Dominio.Entidade;

namespace Clayton.LojaVirtual.Web.V2.Models
{
    public class DetalhesProdutoViewModel
    {
        public ProdutoDetalhes Produto { get; set; }
        public IEnumerable<Cor> Cores { get; set; }
        public IEnumerable<Tamanho> Tamanhos { get; set; }
        public BreadCrumbDto Breadcrumb { get; set; }
        public SelectList CoresList { get; set; }
        public SelectList TamanhoList { get; set; }
    }
}