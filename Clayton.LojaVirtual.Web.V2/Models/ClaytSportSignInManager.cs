using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Clayton.LojaVirtual.Dominio.Entidade;

namespace Clayton.LojaVirtual.Web.V2.Models
{
    public class ClaytSportSignInManager : SignInManager<Cliente, string>
    {
        public ClaytSportSignInManager(ClaytSportUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager) { }

        public static ClaytSportSignInManager Create(IdentityFactoryOptions<ClaytSportSignInManager> option, IOwinContext context)
        {
            var manager = context.GetUserManager<ClaytSportUserManager>();
            var sign = new ClaytSportSignInManager(manager, context.Authentication);
            return sign;
        }
    }
}