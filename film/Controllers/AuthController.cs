using film.Infrastructure.Repository;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;

namespace film.Controllers
{
    public class AuthController:Controller
    {
        public AuthController()
        {
        }

        public ActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Auth(string Login, string Password)
        {
            var role = UsersService.GetUserRole(Login, Password);
            if(role != null)
            {
                Session["Login"] = Login;
                Session["Role"] = role;
                HttpContext.User = new User(role, Login);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Incorrect login or password";
            }
            return View();
        }
    }

    public class User : IPrincipal
    {
        public IIdentity Identity => new Identity(_login);

        string _login;
        string _role;

        public User(string role, string login)
        {
            _role = role;
            _login = login;
        }

        public bool IsInRole(string role)
        {
            return _role == role;
        }
    }

    public class Identity : IIdentity
    {
        string _login;

        public Identity(string login)
        {
            _login = login;
        }

        public string Name => _login;

        public string AuthenticationType => "Admin";

        public bool IsAuthenticated => true;
    }
}