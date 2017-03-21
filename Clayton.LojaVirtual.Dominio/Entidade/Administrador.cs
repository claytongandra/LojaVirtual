using System;
using System.ComponentModel.DataAnnotations;

namespace Clayton.LojaVirtual.Dominio.Entidade
{
    public class Administrador
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o Login")]
        [Display(Name = "Login:")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [Display(Name = "Senha:")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public DateTime UltimoAcesso { get; set; }
    }
}
