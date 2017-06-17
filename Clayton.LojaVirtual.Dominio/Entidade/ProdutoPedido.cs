using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clayton.LojaVirtual.Dominio.Entidade
{
    public class ProdutoPedido
    {
        [Key]
        public int Id { get; set; }

        public int PedidoId { get; set; }

        [ForeignKey("PedidoId")]
        public virtual Pedido Pedido { get; set; }

        public int ProdutoId { get; set; }

        [ForeignKey("ProdutoId")]
        public virtual ProdutoDetalhes Produto { get; set; }

        [Required]
        public int Quantidade { get; set; }

    }   
}
