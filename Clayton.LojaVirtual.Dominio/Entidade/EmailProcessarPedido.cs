using System.Net;
using System.Net.Mail;
using System.Text;

namespace Clayton.LojaVirtual.Dominio.Entidade
{
    public class EmailPedido
    {
        private readonly EmailConfiguracoes _emailConfiguracoes;

        public EmailPedido(EmailConfiguracoes emailConfiguracoes)
        {
            _emailConfiguracoes = emailConfiguracoes;
        }

        public void ProcessarPedido(Carrinho carrinho, Pedido pedido)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = _emailConfiguracoes.UsarSsl;
                smtpClient.Host = _emailConfiguracoes.ServidorSmtp;
                smtpClient.Port = _emailConfiguracoes.ServidorPorta;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(
                    _emailConfiguracoes.Usuario, 
                    _emailConfiguracoes.ServidorSmtp
                );

                if (_emailConfiguracoes.EscreverArquivo)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = _emailConfiguracoes.PastaArquivo;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Novo Pedido")
                    .AppendLine("-----------")
                    .AppendLine("Itens");

                body.AppendLine("--------------------------------------------");

                foreach (var item in carrinho.ItensCarrinho)
	            {
                    var subtotal = item.Produto.Preco * item.Quantidade;

                    

                    body.AppendFormat("{0} x {1} (subtotal: {2:c})", item.Quantidade, item.Produto.Nome, subtotal);
                    body.AppendLine(".");
                    body.AppendLine("--------------------------------------------");
	            }

                body.AppendLine("--------------------------------------------");
                body.AppendFormat("Valor total do pedido: {0:c}", carrinho.ObterValorTotal());
                body.AppendLine("");
                body.AppendLine("--------------------------------------------")
                    .AppendLine("Enviar para:")
                    .AppendLine(pedido.NomeCliente)
                    .AppendLine(pedido.Email)
                    .AppendLine(pedido.Endereco ?? "")
                    .AppendLine(pedido.Complemento ?? "")
                    .AppendLine(pedido.Bairro ?? "")
                    .AppendLine(pedido.Cidade ?? "")
                    .AppendLine(pedido.Estado ?? "")
                    .AppendLine("--------------------------------------------")
                    .AppendFormat("Para presente?: {0}", pedido.EmbrulhaPresente ? "Sim" : "Não");

                body.AppendLine(".");

                body.AppendLine("--------------------------------------------");
         

                MailMessage mailMessage = new MailMessage(
                    _emailConfiguracoes.De,
                    _emailConfiguracoes.Para,
                    "Novo pedido", body.ToString()
                    //"Novo pedido", WebUtility.HtmlEncode(body.ToString())
                    );

                if(_emailConfiguracoes.EscreverArquivo)
                {
                    mailMessage.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}
