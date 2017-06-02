using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clayton.LojaVirtual.Dominio.Entidade
{
    public class TelefoneCliente
    {
        [Key, ForeignKey("Id")]
        public virtual Cliente Cliente { get; set; }

        public string Id { get; set; }
        public string CodigoArea { get; set; }
        public string Numero { get; set; }
    }
}
