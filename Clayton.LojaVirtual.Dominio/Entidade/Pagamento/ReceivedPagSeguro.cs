using System.Xml.Serialization;

namespace Clayton.LojaVirtual.Dominio.Entidade.Pagamento
{
    [XmlRoot("checkout")]
    public class ReceivedPagSeguro
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "date")]
        public string Date { get; set; }
        [XmlElement(ElementName = "qrCode")]
        public string QrCode { get; set; }
    }
}
