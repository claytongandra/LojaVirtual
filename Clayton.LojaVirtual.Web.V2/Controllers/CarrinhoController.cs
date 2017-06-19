using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Configuration;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Identity;
using Clayton.LojaVirtual.Dominio.Repositorio;
using Clayton.LojaVirtual.Dominio.Entidade;
using Clayton.LojaVirtual.Web.V2.Models;
using System.Collections.Generic;
using System.Net.Http;
using Clayton.LojaVirtual.Dominio.Entidade.Pagamento;
using System.Xml.Serialization;
using System.IO;
using System.Xml;


namespace Clayton.LojaVirtual.Web.V2.Controllers
{
    public class CarrinhoController : Controller
    {
        private DetalhesProdutosRepositorio _repositorio = new DetalhesProdutosRepositorio();
        private ClientesRepositorio _clientesRepositorio = new ClientesRepositorio();
        private PedidosRepositorio _pedidosRepositorio = new PedidosRepositorio();

        // GET: Index
        public ViewResult Index(Carrinho carrinho, string returnUrl)
        {

            string varReturnUrlLimpa = ""; //HttpUtility.UrlDecode(new Regex(@"(?si:(Carrinho\/Index\?returnUrl=(?<Url>[^/]+)))").Match(returnUrl).Groups["Url"].Value);

            if (string.IsNullOrEmpty(varReturnUrlLimpa))
            {
                varReturnUrlLimpa = HttpUtility.UrlDecode(returnUrl);
            }

            return View(new CarrinhoViewModel
            {
                Carrinho = carrinho,
                ReturnUrl = varReturnUrlLimpa
            });
        }

        // GET: Resumo
        public PartialViewResult Resumo(Carrinho carrinho)
        {
            return PartialView(carrinho);
        }

        // GET: FecharPedido
        [Authorize]
        public ViewResult FecharPedido()
        {
            Pedido novoPedido = new Pedido();
            novoPedido.ClienteId = User.Identity.GetUserId();
            novoPedido.Cliente = _clientesRepositorio.ObterCliente(User.Identity.GetUserId());


            return View(novoPedido);
        }

        // Post: FecharPedido
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> FecharPedido(Carrinho carrinho, Pedido pedido)
        {
           
            if (!carrinho.ItensCarrinho.Any())
            {
                ModelState.AddModelError("", "Não foi possível concluir o pedido, seu carrinho está vazio!");
            }

            if (ModelState.IsValid)
            {
                pedido.ProdutosPedido = new List<ProdutoPedido>();
                foreach (var item in carrinho.ItensCarrinho)
                {
                    pedido.ProdutosPedido.Add(new ProdutoPedido()
                    {
                        Quantidade = item.Quantidade,
                        ProdutoId = item.Produto.ProdutoId
                    });
                }
                pedido.Pago = false;
                pedido = _pedidosRepositorio.SalvarPedido(pedido);
                pedido.Cliente = _clientesRepositorio.ObterCliente(pedido.ClienteId);
                foreach (var produto in pedido.ProdutosPedido)
                {
                    produto.Produto = _repositorio.Produtos
                        .Where(p => p.ProdutoId == produto.ProdutoId)
                        .FirstOrDefault();
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new System.Uri("https://ws.sandbox.pagseguro.uol.com.br");
                    client.DefaultRequestHeaders.Clear();

                    var pedidoPagSeguro = new PagamentoPagSeguro(
                       pedido,
                       "http://localhost:2686/Carrinho/PedidoConcluido?pedidoId=" + pedido.Id,
                       Request.UserHostAddress);
                    XmlSerializer serializer = new XmlSerializer(typeof(PagamentoPagSeguro));

                    StreamContent content;

                    using (var stream = new MemoryStream())
                    {
                        using (XmlWriter textWriter = XmlWriter.Create(stream))
                        {
                            serializer.Serialize(textWriter, pedidoPagSeguro);
                        }

                        stream.Seek(0, SeekOrigin.Begin);
                        content = new StreamContent(stream);
                        var test = await content.ReadAsStringAsync();

                        content.Headers.Add("Content-Type", "application/xml");

                        var response = await client.PostAsync(
                            "v2/checkouts-qrcode/?email=gandra_cp@yahoo.com.br&token=0836A26ED0EE4265A4612CDEC07A8F08",
                            content);
                        if (response.IsSuccessStatusCode)
                        {
                            string resultContent = await response.Content.ReadAsStringAsync();
                            XmlSerializer returnSerializer = new XmlSerializer(typeof(ReceivedPagSeguro));
                            using (TextReader reader = new StringReader(resultContent))
                            {
                                var retorno = (ReceivedPagSeguro)returnSerializer.Deserialize(reader);
                                return Redirect("https://sandbox.pagseguro.uol.com.br/v2/checkout/payment.html?code=" + retorno.Code);
                            }
                        }
                    }
                }

                return RedirectToAction("PedidoConcluido", new { pedidoId = pedido.Id });
            }
            else
            {
                return View(pedido);
            }

        }

        // GET: Adicionar //int produtoId
        public RedirectToRouteResult Adicionar(Carrinho carrinho, string produto, string Cor, string Tamanho, string returnUrl)
        {


            ProdutoDetalhes prod =  _repositorio.ObterProduto(produto, Cor, Tamanho);
              

            if(prod != null)
            {
                carrinho.AdicionarItem(prod);
               
            }

            return RedirectToAction("Index", "Nav");
        }

        // GET: Remover
        public RedirectToRouteResult Remover(Carrinho carrinho, int produtoId, string returnUrl, int quantidade = 0)
        {
            

             ProdutoDetalhes produto = _repositorio.Produtos
                .FirstOrDefault(p => p.ProdutoId == produtoId);

            if (produto != null)
            {
                carrinho.RemoverItem(produto, quantidade);
            }

            //if (carrinho.ItensCarrinho.Count() <= 0)
            //{
            //    return RedirectToRoute(new
            //    {
            //        controller = "Vitrine",
            //        action = "ListaProdutos"
            //        //categoria = returnUrl.Replace(@"/", "")  
            //    });
            //}

            return RedirectToAction("Index", "Nav");

        }

        // GET: PedidoConcluido
        public ViewResult PedidoConcluido(Carrinho carrinho, int pedidoId)
        {
            EmailConfiguracoes email = new EmailConfiguracoes
            {
    //            EscreverArquivo = bool.Parse(ConfigurationManager.AppSettings["Email.EscreverArquivo"] ?? "false")
            };

            EmailPedido emailPedido = new EmailPedido(email);

            var pedido = _pedidosRepositorio.ObterPedido(pedidoId);
            pedido.Pago = true;
         //   _pedidosRepositorio.SalvarPedido(pedido);

            emailPedido.ProcessarPedido(carrinho, pedido);
            carrinho.LimparCarrinho();

            return View(pedido);
        }

        //private Carrinho ObterCarrinho()
        //{
        //    Carrinho carrinho = (Carrinho)Session["Carrinho"];

        //    if (carrinho == null)
        //    {
        //        carrinho = new Carrinho();
        //        Session["Carrinho"] = carrinho;
        //    }
        //    return carrinho;
        //}
    }
}