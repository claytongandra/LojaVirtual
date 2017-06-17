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
        public ActionResult FecharPedido(Carrinho carrinho, Pedido pedido)
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