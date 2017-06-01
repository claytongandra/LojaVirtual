using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Clayton.LojaVirtual.Dominio.Repositorio;
using Clayton.LojaVirtual.Web.V2.HtmlHelpers;

namespace Clayton.LojaVirtual.Web.V2.Controllers
{
    public class MenuController : Controller
    {
        private MenuRepositorio _repositorio;

        // ObterEsportes
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Server, VaryByParam = "none")]
        public JsonResult ObterEsportes()
        {
            _repositorio = new MenuRepositorio();

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

        // ObterMarcas
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Server, VaryByParam = "none")]
        public JsonResult ObterMarcas()
        {
            _repositorio = new MenuRepositorio();

            var listaMarcas = _repositorio.ObterMarcas();

            var marcas = from marca in listaMarcas
                         select new
                         {
                             
                            Codigo =  marca.MarcaCodigo,
                            UrlSeo =  marca.MarcaDescricao.ToSeoUrl(),
                            Descricao = marca.MarcaDescricao
                            
                             
                         };

            return Json(marcas, JsonRequestBehavior.AllowGet);

        }


        // ObterClubesNacionais
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Server, VaryByParam = "none")]
        public JsonResult ObterClubesNacionais()
        {
            _repositorio = new MenuRepositorio();

            var clubesRepositorio = _repositorio.ObterClubesNacionais();

            var clubes = from clube in clubesRepositorio
                         select new
                         {
                             Codigo = clube.LinhaCodigo,
                             UrlSeo = clube.LinhaDescricao.ToSeoUrl(),
                             Descricao = clube.LinhaDescricao
                         };

            return Json(clubes, JsonRequestBehavior.AllowGet);

        }

        // ObterClubesInternacionais
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Server, VaryByParam = "none")]
        public JsonResult ObterClubesInternacionais()
        {
            _repositorio = new MenuRepositorio();

            var clubesRepositorio = _repositorio.ObterClubesInternacionais();

            var clubes = from clube in clubesRepositorio
                         select new
                         {
                             Codigo = clube.LinhaCodigo,
                             UrlSeo = clube.LinhaDescricao.ToSeoUrl(),
                             Descricao = clube.LinhaDescricao
                         };

            return Json(clubes, JsonRequestBehavior.AllowGet);

        }
    }
}