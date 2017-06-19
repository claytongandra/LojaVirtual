﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Clayton.LojaVirtual.Dominio.Repositorio;
using Clayton.LojaVirtual.Web.V2.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartupAttribute(typeof(Clayton.LojaVirtual.Web.V2.Startup))]
namespace Clayton.LojaVirtual.Web.V2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(EfDbContext.Create);
            app.CreatePerOwinContext<ClaytSportUserManager>(ClaytSportUserManager.Create);
            app.CreatePerOwinContext<ClaytSportSignInManager>(ClaytSportSignInManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/cliente/login"),
                CookieName = "clienteLogin",
                CookiePath = "/",
                ExpireTimeSpan = TimeSpan.FromHours(12)
            });
        }
    }
}