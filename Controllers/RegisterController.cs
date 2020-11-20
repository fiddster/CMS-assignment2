using Umbraco.Web.Mvc;
using System.Web.Mvc;
using Inlamning2.Models;

namespace Inlamning2.Controllers
{
    public class RegisterController : SurfaceController
    {
        public ActionResult RenderRegister()
        {
            var vm = new RegisterViewModel();
            return PartialView("_Register", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleRegister(RegisterViewModel model)
        {
            if (!ModelState.IsValid) { 
                return CurrentUmbracoPage();
            }

            var existingMember = Services.MemberService.GetByEmail(model.EmailAddress);
            if(existingMember != null)
            {
                ModelState.AddModelError("Account Error", "There's already a user with that email address");
                return CurrentUmbracoPage();

            }

            existingMember = Services.MemberService.GetByUsername(model.Username);
            if(existingMember != null)
            {
                ModelState.AddModelError("Account Error", "There's already a user with that username");
                return CurrentUmbracoPage();
            }

            var newMember = Services.MemberService
                .CreateMember(model.Username, model.EmailAddress, $"{model.FirstName} {model.LastName}", "Member");

            newMember.PasswordQuestion = "";
            newMember.RawPasswordAnswerValue = "";

            Services.MemberService.Save(newMember);
            Services.MemberService.SavePassword(newMember, model.Password);

            Services.MemberService.AssignRole(newMember.Id, "Normal User");

            return CurrentUmbracoPage();
        }
    }

    
}