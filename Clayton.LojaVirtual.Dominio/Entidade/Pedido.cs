using System.ComponentModel.DataAnnotations;

namespace Clayton.LojaVirtual.Dominio.Entidade
{
    public class Pedido
    {
        [Required(ErrorMessage="Informe seu nome")]
        [Display(Name = "Nome Completo:")]
        public string NomeCliente { get; set; }

        [Display(Name="Cep:")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Informe o endereço")]
        [Display(Name = "Endereço:")]
        public string Endereco { get; set; }

        [Display(Name = "Complemento:")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Informe o bairro")]
        [Display(Name = "Bairro:")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Informe a cidade")]
        [Display(Name = "Cidade:")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe o estado")]
        [Display(Name = "Estado:")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Informe o bairro")]
        [Display(Name = "E-mail:")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

         //[Display(Name = "Embrulhar para presente?:")]
        public bool EmbrulhaPresente { get; set; }
    }
}
