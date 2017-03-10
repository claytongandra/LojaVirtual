using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clayton.LojaVirtual.Web.Models;
using Clayton.LojaVirtual.Web.HtmlHelpers;

namespace Clayton.LojaVirtual.UnitTest
{
    [TestClass]
    public class UnitTestLojaVirtual
    {
        [TestMethod]
        public void TestarGeracaoPaginacao()
        {
            //Arrange
            HtmlHelper htmlHelper = null;
            Paginacao paginacao = new Paginacao
            {
                PaginaAtual = 2,
                ItensPorPagina = 10,
                ItensTotal = 28
                
            };

            Func<int, string> paginaUrl = i => "Pagina" + i;

            //Act
            MvcHtmlString resultado = htmlHelper.PageLinks(paginacao, paginaUrl);

            //Assert

            Assert.AreEqual(
                 @"<a class=""btn btn-default"" href=""Pagina1"">1</a>"
                 + @"<a class=""btn btn-default btn-primary selected"" href=""Pagina2"">2</a>"
                 + @"<a class=""btn btn-default"" href=""Pagina3"">3</a>", resultado.ToString()
                 );
        }
    }
}
