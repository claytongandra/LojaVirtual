//using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using Clayton.LojaVirtual.Dominio.Entidade;
using Clayton.LojaVirtual.Dominio.Repositorio;



namespace Clayton.LojaVirtual.Web.Controllers
{
    public class AutenticacaoController : Controller
    {
        private AdministradoresRepositorio _repositorio;
        
        // GET: Login
         [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (User.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl ?? "/Administrativo");
            }

            return View(new Administrador());
        }

        // Post: Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Administrador administrador, string returnUrl)
        {
            _repositorio = new AdministradoresRepositorio();

            if(ModelState.IsValid)
            {
                Administrador admin = _repositorio.ObterAdministrador(administrador);

                if(admin != null)
                {
                    if(!Equals(administrador.Senha, admin.Senha))
                    {
                        ModelState.AddModelError("", "Senha inválida");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(admin.Login, false);

                        if (Url.IsLocalUrl(returnUrl)
                            && returnUrl.Length > 1
                            && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//")
                            && !returnUrl.StartsWith("/\\"))
                        {

                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Produto", new { Area = "Administrativo" });
                    }
                }
                else
                {
                    ModelState.AddModelError("","Administrador não localizado");
                }
            }

            return View(new Administrador());
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut();

            FormsAuthentication.SignOut();
            return RedirectToAction("ListaProdutos", "Vitrine");
        }
    }
}