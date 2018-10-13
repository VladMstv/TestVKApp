using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestSegmento.BL.Auth
{
    public class ChallengeResult: HttpUnauthorizedResult
    {
        public string LoginProvider { get; set; }
        public string Redirect { get; set; }

        public ChallengeResult(string providerName, string redirectUri)
        {
            LoginProvider = providerName;
            Redirect = redirectUri;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var props = new AuthenticationProperties { RedirectUri = Redirect };
            context.HttpContext.GetOwinContext().Authentication.Challenge(props, LoginProvider);
        }
    }
}