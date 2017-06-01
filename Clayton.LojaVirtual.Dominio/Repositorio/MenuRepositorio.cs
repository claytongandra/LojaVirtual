using System.Collections.Generic;
using System.Linq;
using FastMapper;
using Clayton.LojaVirtual.Dominio.Entidade;
using Clayton.LojaVirtual.Dominio.Entidade.Vitrine;
using Clayton.LojaVirtual.Dominio.Dto;

namespace Clayton.LojaVirtual.Dominio.Repositorio
{
    public class MenuRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();

        public IEnumerable<Categoria> ObterCategorias()
        {
            return _context.Categorias.OrderBy(c => c.CategoriaDescricao);
        }

        public IEnumerable<MarcaVitrine> ObterMarcas()
        {
            return _context.MarcaVitrine.OrderBy(m => m.MarcaDescricao);
        }

        public IEnumerable<ClubesNacionais> ObterClubesNacionais()
        {
            return _context.ClubesNacionais.OrderBy(c => c.LinhaDescricao);
        }

        public IEnumerable<ClubesInternacionais> ObterClubesInternacionais()
        {
            return _context.ClubesInternacionais.OrderBy(c => c.LinhaDescricao);
        }

        //------------------------------------------------------------------------

        #region [Menu lateral Tênis]

        public IEnumerable<Categoria> ObterTenisCategorias()
        {
            var categorias = new[] { "0005", "0082", "0112", "0010", "0006" };
            return _context.Categorias.Where(c=> categorias.Contains(c.CategoriaCodigo)).OrderBy(c => c.CategoriaDescricao);
        }

        public SubGrupo ObterTenisSubGrupo()
        {
            return _context.SubGrupos.FirstOrDefault(s=> s.SubGrupoCodigo == "0084");
        }

        #endregion
        
        #region [Menu lateral Casual]

        /// <summary>
       /// Retorno modalidade Casual
       /// </summary>
       /// <returns></returns>
        public Modalidade ModalidadeCasual()
        {
            const string CODIGOMODALIDADE = "0001";
            return _context.Modalidades.FirstOrDefault(m => m.ModalidadeCodigo == CODIGOMODALIDADE);
        }

        public IEnumerable<SubGrupoDto> ObterCasualSubGrupo()
        {
            var subGrupos = new[] { "0001", "0102", "0103", "0738", "0084", "0921" };

            var query = from s in _context.SubGrupos
                .Where(s => subGrupos.Contains(s.SubGrupoCodigo))
                .Select(s => new { s.SubGrupoCodigo, s.SubGrupoDescricao })
                .Distinct()
                .OrderBy(s => s.SubGrupoDescricao)
                        select new
                        {
                            s.SubGrupoCodigo,
                            s.SubGrupoDescricao
                        };

            return query.Project().To<SubGrupoDto>().ToList();
        }

        #endregion  

        #region [Menu lateral Suplementos]

        public Categoria Suplemento()
        {
            const string CATEGORIASUPLEMENTOS = "0008";
            return _context.Categorias.FirstOrDefault(s => s.CategoriaCodigo == CATEGORIASUPLEMENTOS);
        }

        public IEnumerable<SubGrupo> ObterSuplementos()
        {
            var subGrupos = new[] { "0162", "0381", "0557", "0564", "0977", "0565", "1082", "1083", "1084", "1085", };

            return _context.SubGrupos
                .Where(s => subGrupos.Contains(s.SubGrupoCodigo) && s.GrupoCodigo == "0012")
                .OrderBy(s => s.SubGrupoDescricao);
        }

        #endregion
    }
}
