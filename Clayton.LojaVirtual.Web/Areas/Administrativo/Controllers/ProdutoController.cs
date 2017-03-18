using System.Web.Mvc;
using System.Linq;
using Clayton.LojaVirtual.Dominio.Repositorio;
using Clayton.LojaVirtual.Dominio.Entidade;


namespace Clayton.LojaVirtual.Web.Areas.Administrativo.Controllers
{
    public class ProdutoController : Controller
    {
      
        private ProdutosRepositorio _repositorio;

        // GET: Administrativo/Produto
        public ActionResult Index()
        {
            _repositorio = new ProdutosRepositorio();

            var produtos = _repositorio.Produtos;

            return View(produtos);
        }

        public ViewResult Alterar(int produtoId)
        {
            _repositorio = new ProdutosRepositorio();

            Produto produto = _repositorio.Produtos
               .FirstOrDefault(p => p.ProdutoId == produtoId);

            return View(produto);
        }
    }
}