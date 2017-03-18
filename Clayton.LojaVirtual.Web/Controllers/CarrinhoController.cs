﻿using System.Web.Mvc;
using System.Linq;
using Clayton.LojaVirtual.Dominio.Repositorio;
using Clayton.LojaVirtual.Dominio.Entidade;
using Clayton.LojaVirtual.Web.Models;
using System.Configuration;


namespace Clayton.LojaVirtual.Web.Controllers
{
    public class CarrinhoController : Controller
    {
        private ProdutosRepositorio _repositorio;

        public ViewResult Index(string returnUrl)
        {
                      
            return View(new CarrinhoViewModel
            {
                Carrinho = ObterCarrinho(),
                ReturnUrl = returnUrl
            });
        }
        
        // GET: Carrinho
        public RedirectToRouteResult Adicionar(int produtoId, string returnUrl)
        {
            _repositorio = new ProdutosRepositorio();

            Produto produto = _repositorio.Produtos
                .FirstOrDefault(p => p.ProdutoId == produtoId);

            if(produto != null)
            {
                ObterCarrinho().AdicionarItem(produto, 1);
               
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        private Carrinho ObterCarrinho()
        {
            Carrinho carrinho = (Carrinho)Session["Carrinho"];

            if(carrinho == null)
            {
                carrinho = new Carrinho();
                Session["Carrinho"] = carrinho;
            }
            return carrinho;
        }

        public RedirectToRouteResult Remover(int produtoId, string returnUrl)
        {
             _repositorio = new ProdutosRepositorio();

            Produto produto = _repositorio.Produtos
                .FirstOrDefault(p => p.ProdutoId == produtoId);

            if (produto != null)
            {
                ObterCarrinho().RemoverItem(produto);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Resumo()
        {
            Carrinho carrinho = ObterCarrinho();

            return PartialView(carrinho);
        }

        public ViewResult FecharPedido()
        {
            return View(new Pedido());
        }

        [HttpPost]
        public ViewResult FecharPedido(Pedido pedido)
        {
            Carrinho carrinho = ObterCarrinho();

            EmailConfiguracoes email = new EmailConfiguracoes
            {
                EscreverArquivo = bool.Parse(ConfigurationManager.AppSettings["Email.EscreverArquivo"] ?? "false")
            };

            EmailPedido emailPedido = new EmailPedido(email);

            if(!carrinho.ItensCarrinho.Any())
            {
                ModelState.AddModelError("", "Não foi possível concluir o pedido, seu carrinho está vazio!");
            }

            if (ModelState.IsValid)
            {
                emailPedido.ProcessarPedido(carrinho, pedido);
                carrinho.LimparCarrinho();
                return View("PedidoConcluido");
            }
            else
            {
                return View(pedido);
            }
            
        }

        public ViewResult PedidoConcluido()
        {
            return View();
        }
    }
}