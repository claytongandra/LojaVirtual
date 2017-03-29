
using System.Web.Mvc;
using System.Linq;
using Clayton.LojaVirtual.Dominio.Repositorio;
using Clayton.LojaVirtual.Dominio.Entidade;
using Clayton.LojaVirtual.Web.Models;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;


namespace Clayton.LojaVirtual.Web.Controllers
{
    public class CarrinhoController : Controller
    {
        private ProdutosRepositorio _repositorio;

        // GET: Index
        public ViewResult Index(Carrinho carrinho, string returnUrl)
        {

            string varReturnUrlLimpa = HttpUtility.UrlDecode(new Regex(@"(?si:(Carrinho\/Index\?returnUrl=(?<Url>[^/]+)))").Match(returnUrl).Groups["Url"].Value);

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
        public ViewResult FecharPedido()
        {
            return View(new Pedido());
        }

        // Post: FecharPedido
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult FecharPedido(Carrinho carrinho, Pedido pedido)
        {
            EmailConfiguracoes email = new EmailConfiguracoes
            {
                EscreverArquivo = bool.Parse(ConfigurationManager.AppSettings["Email.EscreverArquivo"] ?? "false")
            };

            EmailPedido emailPedido = new EmailPedido(email);

            if (!carrinho.ItensCarrinho.Any())
            {
                ModelState.AddModelError("", "Não foi possível concluir o pedido, seu carrinho está vazio!");
            }

            if (ModelState.IsValid)
            {
                emailPedido.ProcessarPedido(carrinho, pedido);
                carrinho.LimparCarrinho();
                return View("PedidoConcluido");
            }
            else
            {
                return View(pedido);
            }

        }

        // GET: Adicionar
        public RedirectToRouteResult Adicionar(Carrinho carrinho, int produtoId, string returnUrl)
        {
            _repositorio = new ProdutosRepositorio();

            Produto produto = _repositorio.Produtos
                .FirstOrDefault(p => p.ProdutoId == produtoId);

            if(produto != null)
            {
                carrinho.AdicionarItem(produto);
               
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        // GET: Remover
        public RedirectToRouteResult Remover(Carrinho carrinho, int produtoId, string returnUrl, int quantidade = 0)
        {
             _repositorio = new ProdutosRepositorio();

            Produto produto = _repositorio.Produtos
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
            
            return RedirectToAction("Index", new { returnUrl });

        }

        // GET: PedidoConcluido
        public ViewResult PedidoConcluido()
        {
            return View();
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