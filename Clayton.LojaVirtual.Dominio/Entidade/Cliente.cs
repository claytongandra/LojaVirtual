using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clayton.LojaVirtual.Dominio.Entidade
{
    public class Cliente: IdentityUser
    {
        [NotMapped]
        public string Senha { get; set; }
        [Required]
        public virtual TelefoneCliente Telefone { get; set; }
        [Required]
        public virtual DocumentoCliente Documento { get; set; }
        [Required]
        public virtual EnderecoCliente Endereco { get; set; }
    }
}
