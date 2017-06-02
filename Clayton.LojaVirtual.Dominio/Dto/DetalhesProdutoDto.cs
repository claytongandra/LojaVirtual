using System.Collections.Generic;
using Clayton.LojaVirtual.Dominio.Entidade;


namespace Clayton.LojaVirtual.Dominio.Dto
{
    public class DetalhesProdutoDto
    {
        public ProdutoDetalhes Produto { get; set; }
        public IEnumerable<Cor> Cores { get; set; }
        public IEnumerable<Tamanho> Tamanhos { get; set; }
    }
}
