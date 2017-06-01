using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Clayton.LojaVirtual.Dominio.Repositorio;
using Clayton.LojaVirtual.Web.V2.HtmlHelpers;


namespace Clayton.LojaVirtual.Web.V2.Controllers
{
    public class CategoriaController : Controller
    {
        private CategoriaRepositorio _repositorio;

        // ObterEsportes
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Server, VaryByParam = "none")]
        public JsonResult ObterEsportes()
        {
            _repositorio = new CategoriaRepositorio();

            var cat = _repositorio.ObterCategorias();

            var categoria = from c in cat
                             select new
                             {
                                 c.CategoriaDescricao,
                                 CategoriaDescricaoSeo = c.CategoriaDescricao.ToSeoUrl(),
                                 c.CategoriaCodigo
                             };

            return Json(categoria, JsonRequestBehavior.AllowGet);
        }
    }
}