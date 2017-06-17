

namespace Clayton.LojaVirtual.Dominio.Entidade
{
    public class EmailConfiguracoes
    {

        public bool UsarSsl = false;

        public string ServidorSmtp = "smtp.clayton.com.br";

        public int ServidorPorta = 587;

        public string Usuario = "clayton";

        public bool EscreverArquivo  = true;

        public string PastaArquivo = @"C:\Exercicios DevMedia\03 - Série Programação .net Web\01 - Curso de ASP.NET MVC - Criando Loja Virtual\Clayton.LojaVirtual\www.EmailPedidosEnviados";

        public string De = "pedidos@claytonlojavirtual.com.br";

        public string Para = "pedidos.fechamento@claytonlojavirtual.com.br";
    }
}
