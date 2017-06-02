

using System;
using System.ComponentModel.DataAnnotations;

namespace Clayton.LojaVirtual.Dominio.Entidade
{
    public class Estoque
    {
        [Key]
        public int EstoqueId { get; set; }

        public string ProdutoCodigo { get; set; }
        public int Quantidade { get; set; }

        public DateTime DataAtualizacao { get; set; }
    }
}
