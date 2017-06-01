using System.Collections.Generic;
using Clayton.LojaVirtual.Dominio.Entidade;


namespace Clayton.LojaVirtual.Web.V2.Models
{
    public class SubGrupoCategoriasViewModel
    {

        public SubGrupo SubGrupo { get; set; }
        public IEnumerable<Categoria> Categorias { get; set; }
    }
}