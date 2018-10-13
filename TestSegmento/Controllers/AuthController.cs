using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestSegmento.BL.Auth;
using TestSegmento.ViewModels.Auth;

namespace TestSegmento.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        public ActionResult Index(AuthViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult AuthenticateViaVk()
        {
            return new ChallengeResult("vkontakte", Url.Action("ExternalAuthCallback","Auth"));
        }

        public async Task<ActionResult> ExternalAuthCallback()
        {
            var authResult = await AuthenticationManager.AuthenticateAsync("ApplicationCookie");
            if (authResult != null)
            {
                var token = authResult.Identity.Claims.FirstOrDefault(x => x.Type == "urn:vkontakte:token")?.Value;
                if (!String.IsNullOrEmpty(token))
                {
                    var identity = CreateIdentity(authResult.Identity.Claims);
                    AuthenticationManager.SignIn(new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.Now.AddMinutes(60)
                    }, identity);

                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", new AuthViewModel() { Errors = new string[] { "Something went wrong. Please, try again." } });
        }


        private ClaimsIdentity CreateIdentity(IEnumerable<Claim> claims = null)
        {
            List<Claim> claimsList = new List<Claim>();
            if (claims != null)
            {
                claimsList.AddRange(claims);
            }
            
            var identity = new ClaimsIdentity(claims, "ApplicationCookie");
            return identity;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

    }
}