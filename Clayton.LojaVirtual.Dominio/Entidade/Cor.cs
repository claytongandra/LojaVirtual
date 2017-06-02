using System.ComponentModel.DataAnnotations;

namespace Clayton.LojaVirtual.Dominio.Entidade
{
    public class Cor
    {
        [Key]
        public int CorId { get; set; }
        public string CorDescricao { get; set; }
        public string CorCodigo { get; set; }
        public string Tamanho { get; set; }
    }
}
