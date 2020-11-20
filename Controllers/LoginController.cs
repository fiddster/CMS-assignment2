using Inlamning2.Models;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace Inlamning2.Controllers
{
    public class LoginController : SurfaceController
    {
        public ActionResult RenderLogin()
        {
            var model = new LoginViewModel();
            model.RedirectUrl = HttpContext.Request.Url.AbsolutePath;
            return PartialView("_Login", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleLogin(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            var member = Services.MemberService.GetByUsername(model.Username);

            if(member == null)
            {
                ModelState.AddModelError("Login", "Cannot find that username in the system");
                return CurrentUmbracoPage();
            }

            if(!Members.Login(model.Username, model.Password))
            {
                ModelState.AddModelError("Login", "Username or password you entered is not correct");
                return CurrentUmbracoPage();
            }

            if (!string.IsNullOrEmpty(model.RedirectUrl))
            {
                return Redirect(model.RedirectUrl);
            }

            return RedirectToCurrentUmbracoPage();
        }
    }
}