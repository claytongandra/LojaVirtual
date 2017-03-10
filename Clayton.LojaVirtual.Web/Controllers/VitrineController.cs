using System.Linq;
using System.Web.Mvc;
using Clayton.LojaVirtual.Dominio.Repositorio;

namespace Clayton.LojaVirtual.Web.Controllers
{
    public class VitrineController : Controller
    {
       
        private ProdutosRepositorio _repositorio;
        public int ProdutosPorPagina = 10;
        // GET: Vitrine
        public ActionResult ListaProdutos( int pagina = 1)
        {
            _repositorio = new ProdutosRepositorio();
            var produtos = _repositorio.Produtos
                            .OrderBy(p => p.Descricao)
                            .Skip((pagina - 1) * ProdutosPorPagina)
                            .Take(ProdutosPorPagina);

            return View(produtos);
        }
    }
}