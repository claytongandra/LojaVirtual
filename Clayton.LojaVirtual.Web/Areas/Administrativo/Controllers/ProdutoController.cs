using System.Web.Mvc;
using System.Linq;
using Clayton.LojaVirtual.Dominio.Repositorio;
using Clayton.LojaVirtual.Dominio.Entidade;
using System.Web;

namespace Clayton.LojaVirtual.Web.Areas.Administrativo.Controllers
{
    [Authorize]
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

            ViewBag.NomeProduto = produto.Nome;

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alterar(Produto produto, string hidNomeProduto, HttpPostedFileBase image = null)
        {

            if (ModelState.IsValid)
            {

                _repositorio = new ProdutosRepositorio();

                if (image != null)
                {
                    produto.ImagemMimeType = image.ContentType;
                    produto.Imagem = new byte[image.ContentLength];
                    image.InputStream.Read(produto.Imagem, 0, image.ContentLength);
                }
                else
                {
                    Produto prod = _repositorio.Produtos
                        .FirstOrDefault(p => p.ProdutoId == produto.ProdutoId);

                    if (prod.Imagem != null)
                    {
                        produto.ImagemMimeType = prod.ImagemMimeType;
                        produto.Imagem = prod.Imagem;
                    }
                }
                
                _repositorio.Salvar(produto);

                TempData["mensagem"] = string.Format("<a href='Administrativo/Produto/Alterar?ProdutoId={0}'> {1} foi salvo com sucesso </a>", produto.ProdutoId, produto.Nome);

                return RedirectToAction("Index");
            }

            ViewBag.NomeProduto = hidNomeProduto;

            return View(produto);
        }

        public ViewResult NovoProduto()
        {

            return View("Alterar", new Produto());
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Excluir(int produtoId)
        //{
        //    _repositorio = new ProdutosRepositorio();

        //    Produto prod = _repositorio.Excluir(produtoId);

        //    if (prod != null)
        //    {
        //        TempData["mensagem"] = string.Format("{0} excluído com sucesso", prod.Nome);
        //    }
        //    return RedirectToAction("Index");
        //}


        public JsonResult Excluir(int produtoId)
        {
            string mensagem = string.Empty;
            _repositorio = new ProdutosRepositorio();

            Produto prod = _repositorio.Excluir(produtoId);

            if (prod != null)
            {
                mensagem = string.Format("{0} excluído com sucesso", prod.Nome);
            }
            return Json(mensagem, JsonRequestBehavior.AllowGet);
        }
    }
}

