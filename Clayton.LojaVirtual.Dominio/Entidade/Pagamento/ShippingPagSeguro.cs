using System.Globalization;
using System.Xml.Serialization;

namespace Clayton.LojaVirtual.Dominio.Entidade.Pagamento
{
    public class ShippingPagSeguro
    {

        CultureInfo us = new CultureInfo("en-US");

        [XmlElement(ElementName = "address")]
        public ShippingAddressPagSeguro Address { get; set; }
        [XmlElement(ElementName = "type")]
        public int Type { get; set; } // 1- PAC, 2 - SEDEX, 3 - Não especificado

        private decimal cost;
        [XmlElement(ElementName = "cost")]
        public string Cost
        {


            get { return cost.ToString("n2", us); }
            set { cost = decimal.Parse(value); }
        }
    }
}
