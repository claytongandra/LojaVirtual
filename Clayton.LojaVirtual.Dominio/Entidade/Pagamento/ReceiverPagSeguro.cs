
using System.Xml.Serialization;

namespace Clayton.LojaVirtual.Dominio.Entidade.Pagamento
{
    public class ReceiverPagSeguro
    {
        [XmlElement(ElementName = "email")]
        public string Email { get { return "gandra_cp@yahoo.com.br"; } set { } }
    }
}
