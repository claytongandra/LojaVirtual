using System.Web.Mvc;
using Clayton.LojaVirtual.Dominio.Repositorio;
using Clayton.LojaVirtual.Web.V2.Models;
using Clayton.LojaVirtual.Web.V2.HtmlHelpers;


namespace Clayton.LojaVirtual.Web.V2.Controllers
{
    public class NavController : Controller
    {
        private ProdutoModeloRepositorio _repositorio;
        private ProdutosViewModel _model;
        private MenuRepositorio _menuRepositorio;

        // GET: Nav
        public ActionResult Index()
        {
            _repositorio = new ProdutoModeloRepositorio();

            var produtosVitrine = _repositorio.ObterProdutosVitrine();

            _model = new ProdutosViewModel { Produtos = produtosVitrine };

            return View(_model);
        }

        [Route("nav/{codigo}/{marca}")]
        public ActionResult ObterProdutosPorMarcas(string codigo, string marca)
        {

            _repositorio = new ProdutoModeloRepositorio();

            var produtosVitrine = _repositorio.ObterProdutosVitrine(marca: codigo);


            _model = new ProdutosViewModel { Produtos = produtosVitrine, Titulo = marca };


            return View("Navegacao", _model);
        }

        [Route("nav/times/{codigo}/{clube}")]
        public ActionResult ObterProdutosPorClubes(string codigo, string clube)
        {

            _repositorio = new ProdutoModeloRepositorio();

            var produtosVitrine = _repositorio.ObterProdutosVitrine(linha: codigo);


            _model = new ProdutosViewModel { Produtos = produtosVitrine, Titulo = clube };


            return View("Navegacao", _model);
        }


        [Route("nav/genero/{codigo}/{genero}")]
        public ActionResult ObterProdutosPorGenero(string codigo, string genero)
        {

            _repositorio = new ProdutoModeloRepositorio();

            var produtosVitrine = _repositorio.ObterProdutosVitrine(genero: codigo);


            _model = new ProdutosViewModel { Produtos = produtosVitrine, Titulo = genero };


            return View("Navegacao", _model);
        }

        #region [Tenis por Categoria]

        /// <summary>
        /// Obtem categoria de tenis exibido no menu
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [OutputCache(Duration = 3600, VaryByParam = "*")]
        public ActionResult TenisCategoria()
        {
            _menuRepositorio = new MenuRepositorio();
            var categorias = _menuRepositorio.ObterTenisCategorias();
            var subgrupo = _menuRepositorio.ObterTenisSubGrupo();

            SubGrupoCategoriasViewModel model = new SubGrupoCategoriasViewModel
            {
                Categorias = categorias,
                SubGrupo = subgrupo
            };

            return PartialView("_TenisCategoria", model);
        }

        /// <summary>
        /// Retorna uma vitrine com tenis por categoria
        /// </summary>
        /// <param name="subGrupoCodigo"></param>
        /// <param name="categoriaCodigo"></param>
        /// <param name="categoriaDescricao"></param>
        /// <returns></returns>
        [Route("calcados/{subGrupoCodigo}/tenis/{categoriaCodigo}/{categoriaDescricao}")]
        public ActionResult ObterTenisPorCategoria(string subGrupoCodigo, string categoriaCodigo, string categoriaDescricao)
        {
            _repositorio = new ProdutoModeloRepositorio();
            var produtos = _repositorio.ObterProdutosVitrine(categoriaCodigo, subgrupo: subGrupoCodigo);
            _model = new ProdutosViewModel { Produtos = produtos, Titulo = categoriaDescricao.UpperCaseFirst() };
            return View("Navegacao", _model);

        }

        #endregion

        #region [Casual]

        /// <summary>
        /// Obtem modalidades de casual exibido no menu
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [OutputCache(Duration = 3600, VaryByParam = "*")]
        public ActionResult CasualSubGrupo()
        {
            _menuRepositorio = new MenuRepositorio();
            var casual = _menuRepositorio.ModalidadeCasual();
            var subGrupo = _menuRepositorio.ObterCasualSubGrupo();

            var model = new ModalidadeSubGrupoViewModel
            {
                Modalidade = casual,
                SubGrupos = subGrupo
            };

            return PartialView("_CasualSubGrupo", model);
        }

        [Route("{modalidadeCodigo}/casual/{subGrupoCodigo}/{subGrupoDescricao}")]
        public ActionResult ObterModalidadeSubGrupo(string modalidadeCodigo, string subGrupoCodigo, string subGrupoDescricao)
        {
            _repositorio = new ProdutoModeloRepositorio();
            var produtos = _repositorio.ObterProdutosVitrine(modalidade: modalidadeCodigo, subgrupo: subGrupoCodigo);
            _model = new ProdutosViewModel { Produtos = produtos, Titulo = subGrupoDescricao.UpperCaseFirst() };
            return View("Navegacao", _model);
        }

        #endregion


        #region [Suplementos]

        /// <summary>
        /// Obtem suplemenos exibido no menu
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [OutputCache(Duration = 3600, VaryByParam = "*")]
        public ActionResult Suplementos()
        {
            _menuRepositorio = new MenuRepositorio();
            var categoria = _menuRepositorio.Suplemento();
            var subGrupos = _menuRepositorio.ObterSuplementos();

            CategoriaSubGruposViewModel model = new CategoriaSubGruposViewModel
            {
                Categoria = categoria,
                SubGrupos = subGrupos
            };

            return PartialView("_Suplementos", model);
        }

        [Route("{categoriaCodigo}/suplementos/{subGrupoCodigo}/{subGrupoDescricao}")]
        public ActionResult ObterCategoriaSubGrupos(string categoriaCodigo, string subGrupoCodigo, string subGrupoDescricao)
        {
            _repositorio = new ProdutoModeloRepositorio();
            var produtos = _repositorio.ObterProdutosVitrine(categoriaCodigo, subgrupo: subGrupoCodigo);
            _model = new ProdutosViewModel { Produtos = produtos, Titulo = subGrupoDescricao.UpperCaseFirst() };
            return View("Navegacao", _model);
        }

        #endregion

        #region [Consulta]

        public ActionResult ConsultarProduto(string termo)
        {
            _repositorio = new ProdutoModeloRepositorio();
            var produtos = _repositorio.ObterProdutosVitrine(busca: termo);

            _model = new ProdutosViewModel { Produtos = produtos, Titulo = termo.UpperCaseFirst() };
            return View("Navegacao", _model);
        }

        #endregion


        //public JsonResult TesteMetedoVitrine()
        //{
        //    ProdutoModeloRepositorio repositorio = new ProdutoModeloRepositorio();

        //    //modalidade: "0051", categoria:"0083",marca: "1106"

        //    var produtos = repositorio.ObterProdutosVitrine(modalidade: "0051");

        //    return Json(produtos, JsonRequestBehavior.AllowGet);
        //}

    }
}