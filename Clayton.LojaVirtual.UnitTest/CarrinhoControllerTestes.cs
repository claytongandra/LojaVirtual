using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clayton.LojaVirtual.Dominio.Entidade;
using Clayton.LojaVirtual.Web.Controllers;
using System.Web.Mvc;
using Clayton.LojaVirtual.Web.Models;

namespace Clayton.LojaVirtual.UnitTest
{
    [TestClass]
    public class CarrinhoControllerTestes
    {
        [TestMethod]
        public void AdicionarItemCarrinho()
        {
            //Preparação (Arrange ) eo estímulo (Act), das verificações (Asserts) 

            //Arrange
            Produto produto1 = new Produto { 
                ProdutoId= 1,
                Nome = "Teste 1"
            };

            Produto produto2 = new Produto
            {
                ProdutoId = 2,
                Nome = "Teste 2"
            };

            Carrinho carrinho = new Carrinho();

            //carrinho.AdicionarItem(produto1);

            //carrinho.AdicionarItem(produto2);

            CarrinhoController controller = new CarrinhoController();

            //Act

            //controller.Adicionar(carrinho, 2, "");

            //Asserts

            Assert.AreEqual(carrinho.ItensCarrinho.Count(), 2);

            Assert.AreEqual(carrinho.ItensCarrinho.ToArray()[0].Produto.ProdutoId, 1);
        }

        [TestMethod]
        public void AdicionarCarrinhoVoltarCategoria()
        {
            //Arrange
            Carrinho carrinho = new Carrinho();

            CarrinhoController controller = new CarrinhoController();

            //Act

            //////RedirectToRouteResult result = controller.Adicionar(carrinho, 2, "minhaUrl");

            //Asserts

             //////Assert.AreEqual(result.RouteValues["action"],"Index");

             //////Assert.AreEqual(result.RouteValues["returnUrl"], "minhaUrl");
        }

        [TestMethod]
        public void VisualizarConteudoCarrinho()
        {
            //Arrange
            Carrinho carrinho = new Carrinho();

            CarrinhoController controller = new CarrinhoController();

            //Act
            CarrinhoViewModel resultado = (CarrinhoViewModel)controller.Index(carrinho, "minhaUrl").ViewData.Model;

            //Asserts
            Assert.AreSame(resultado.Carrinho, carrinho);

            Assert.AreEqual(resultado.ReturnUrl, "minhaUrl");
        }
    }
}
