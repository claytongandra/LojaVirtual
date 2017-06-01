using System.Collections.Generic;
using Clayton.LojaVirtual.Dominio.Dto;
using Clayton.LojaVirtual.Dominio.Entidade;

namespace Clayton.LojaVirtual.Web.V2.Models
{
    public class ModalidadeSubGrupoViewModel
    {
        public Modalidade Modalidade { get; set; }
        public IEnumerable<SubGrupoDto> SubGrupos { get; set; }


    }
}