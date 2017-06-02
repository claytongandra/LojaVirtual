using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Clayton.LojaVirtual.Dominio.Entidade;
using Clayton.LojaVirtual.Dominio.Repositorio;

namespace Clayton.LojaVirtual.Web.V2.Models
{
    public class ClaytSportUserManager: UserManager<Cliente>
    {
        public ClaytSportUserManager(IUserStore<Cliente> store) : base(store) { }

        public static ClaytSportUserManager Create(IdentityFactoryOptions<ClaytSportUserManager> options, IOwinContext context)
        {
            var userManager = new ClaytSportUserManager(new UserStore<Cliente>(new EfDbContext()));

            return userManager;
        }
    }
}