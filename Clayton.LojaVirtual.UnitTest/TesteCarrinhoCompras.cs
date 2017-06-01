using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clayton.LojaVirtual.Dominio.Entidade;

namespace Clayton.LojaVirtual.UnitTest
{
    [TestClass]
    public class TesteCarrinhoCompras
    {
        
        
        // Teste adicionar itens ao Carrinho
        [TestMethod]
        public void AdicionarItemCarrinho()
        {
            Produto produto1 = new Produto{ProdutoId = 1, Nome = "Teste 1"};
            Produto produto2 = new Produto { ProdutoId = 2, Nome = "Teste 2" };
            Produto produto3 = new Produto { ProdutoId = 3, Nome = "Teste 3" };

            //Arrange

            Carrinho carrinho = new Carrinho();

            carrinho.AdicionarItem(produto1);
            carrinho.AdicionarItem(produto2);
            carrinho.AdicionarItem(produto3);

            //Act
            ItemCarrinho[] itens = carrinho.ItensCarrinho.ToArray();

            //Assert
            Assert.AreEqual(itens.Length, 3);

            Assert.AreEqual(itens[0].Produto, produto1);
            Assert.AreEqual(itens[1].Produto, produto2);

        }

        [TestMethod]
        public void AdicionarProdutoExistenteCarrinho()
        {
            Produto produto1 = new Produto { ProdutoId = 1, Nome = "Teste 1" };
            Produto produto2 = new Produto { ProdutoId = 2, Nome = "Teste 2" };
           // Produto produto3 = new Produto { ProdutoId = 3, Nome = "Teste 3" };

            //Arrange

            Carrinho carrinho = new Carrinho();

            carrinho.AdicionarItem(produto1);
            carrinho.AdicionarItem(produto2);
            carrinho.AdicionarItem(produto1);

            //Act
            ItemCarrinho[] resultado = carrinho.ItensCarrinho.OrderBy(c => c.Produto.ProdutoId).ToArray();

            //Assert
            Assert.AreEqual(resultado.Length, 2);

            Assert.AreEqual(resultado[0].Quantidade, 11);
            Assert.AreEqual(resultado[1].Quantidade, 1);
        }


        [TestMethod]
        public void RemoverItensCarrinho()
        {
            Produto produto1 = new Produto { ProdutoId = 1, Nome = "Teste 1" };
            Produto produto2 = new Produto { ProdutoId = 2, Nome = "Teste 2" };
            Produto produto3 = new Produto { ProdutoId = 3, Nome = "Teste 3" };

            //Arrange

            Carrinho carrinho = new Carrinho();

            carrinho.AdicionarItem(produto1);
            carrinho.AdicionarItem(produto2);
            carrinho.AdicionarItem(produto3);
            carrinho.AdicionarItem(produto2);


            carrinho.RemoverItem(produto2, 3);

            //Assert

            Assert.AreEqual(carrinho.ItensCarrinho.Where(c => c.Produto == produto2).Count(),0);

            Assert.AreEqual(carrinho.ItensCarrinho.Count(), 2);
        }

        [TestMethod]
        public void CalcularValorTotalCarrinho()
        {
            Produto produto1 = new Produto { ProdutoId = 1, Nome = "Teste 1", Preco = 100M };
            Produto produto2 = new Produto { ProdutoId = 2, Nome = "Teste 2", Preco = 50M };

            //Arrange

            Carrinho carrinho = new Carrinho();

            carrinho.AdicionarItem(produto1); //100
            carrinho.AdicionarItem(produto2); //50
            carrinho.AdicionarItem(produto1); //300 = 450

            decimal resultado = carrinho.ObterValorTotal();

            //Assert

            Assert.AreEqual(resultado, 450M);
        }


        [TestMethod]
        public void LimparItensCarrinho()
        {
            Produto produto1 = new Produto { ProdutoId = 1, Nome = "Teste 1", Preco = 100M };
            Produto produto2 = new Produto { ProdutoId = 2, Nome = "Teste 2", Preco = 50M };

            //Arrange

            Carrinho carrinho = new Carrinho();

            carrinho.AdicionarItem(produto1); 
            carrinho.AdicionarItem(produto2);

            carrinho.LimparCarrinho();

            //Assert

            Assert.AreEqual(carrinho.ItensCarrinho.Count(), 0);

        }
    }
}
