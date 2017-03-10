using System;
using System.Text;
using System.Web.Mvc;
using Clayton.LojaVirtual.Web.Models;

namespace Clayton.LojaVirtual.Web.HtmlHelpers
{
    public static class PaginacaoHelpers
    {
        //Extenção da classe HtmlHelper retornando um MvcHtmlString
        public static MvcHtmlString PageLinks(this HtmlHelper html, Paginacao paginacao, Func<int,string> paginaUrl )
        {
            StringBuilder resultado = new StringBuilder();

            for (int i = 1; i <= paginacao.TotalPagina; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", paginaUrl(i));
                tag.InnerHtml = i.ToString();

                if(i == paginacao.PaginaAtual)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                resultado.Append(tag);
            }
            return MvcHtmlString.Create(resultado.ToString());
        }
    }
}