using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using AdminUI.Helpers;
using AdminUI.Models;
using Microsoft.AspNet.Identity;
using Moodroon.DataLayer.Core;
using Moodroon.DataLayer.Core.Services;

namespace AdminUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController()
        {
            this._userService = new UserService(UnityFactory.Instance.Container);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                var claim = await this._userService.AuthenticateApi(model.Email,model.Password);
                if (claim == null)
                {
                    this.ModelState.AddModelError("", "Invalid login attempt.");
                }
                else
                {
                    var user = this._userService.GetUserById(Convert.ToInt32(claim.GetUserId()));
                    var serializeModel = new CustomPrincipalSerializeModel
                    {
                        Id = Convert.ToInt32(claim.GetUserId()),
                        Roles = await this._userService.GetUserRoles(Convert.ToInt32(claim.GetUserId()))
                    };
                    var userData = new JavaScriptSerializer().Serialize(serializeModel);
                    var authTicket = new FormsAuthenticationTicket(1, model.Email, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData);
                    var encTicket = FormsAuthentication.Encrypt(authTicket);
                    var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    this.Response.Cookies.Add(faCookie);
                    return this.RedirectToAction("Index", "Hotel");
                }
            }
            return this.View(model);
        }
    }
}