using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clayton.LojaVirtual.Dominio.Entidade
{
    public class ProdutoDetalhes
    {
        [Key]
        public int ProdutoId { get; set; }

        public string ProdutoCodigo { get; set; }

        public string ProdutoModeloCodigo { get; set; }

        public string CorCodigo { get; set; }

        public string TamanhoCodigo { get; set; }

        public decimal Preco { get; set; }

        public string ProdutoDescricao { get; set; }

        public string ProdutoDescricaoResumida { get; set; }

        public string MarcaDescricao { get; set; }

        public string ModeloDescricao { get; set; }

        public string UnidadeVenda { get; set; }


        public string Imagem
        {
            get { return ProdutoCodigo.Substring(0, 8) + ".jpg"; }

        }

        public virtual ICollection<ProdutoPedido> ProdutosPedido { get; set; }
    }
}
