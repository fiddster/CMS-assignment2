using Umbraco.Web.Mvc;
using System.Web.Mvc;
using System.Net.Mail;
using Inlamning2.Models;
using System.Web;
using Umbraco.Web;

namespace Inlamning2.Controllers
{
    public class ContactSurfaceController : SurfaceController
    {
        [HttpGet]
        public ActionResult RenderForm()
        {
            ContactModel model = new ContactModel() { ContactFormId = CurrentPage.Id};
            return PartialView("_Contact", model);
        }

        [HttpPost]
        public ActionResult RenderForm(ContactModel model)
        {
            return PartialView("_Contact", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ContactModel model)
        {
            bool success = false;
            if (ModelState.IsValid)
            {
                success = SendEmail(model);
            }

            var contactPage = UmbracoContext.Content.GetById(false, model.ContactFormId);

            var successMessage = contactPage.Value<IHtmlString>("successMessage");
            var errorMessage = contactPage.Value<IHtmlString>("errorMessage");

            return PartialView("_ContactResult", success ? successMessage : errorMessage);
        }

        public bool SendEmail(ContactModel model)
        {
            try
            {
                MailMessage message = new MailMessage(model.EmailAddress, model.EmailAddress)
                {
                    Subject = string.Format("Enquiry from {0} {1} - {2}", model.FirstName, model.LastName, model.EmailAddress),

                    Body = model.Message
                };

                SmtpClient client = new SmtpClient("127.0.0.1", 25);

                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

    }
}