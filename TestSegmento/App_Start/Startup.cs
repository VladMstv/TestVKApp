using System;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using Duke.Owin.VkontakteMiddleware;
using Duke.Owin.VkontakteMiddleware.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(TestSegmento.App_Start.Startup))]

namespace TestSegmento.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType("ApplicationCookie");
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                CookieName = ".AspNet.ApplicationCookie",
                LoginPath = new PathString("/Auth")
            });

            app.UseVkontakteAuthentication(new VkAuthenticationOptions()
            {
                AppId = ConfigurationManager.AppSettings.Get("VKAppId"),
                AppSecret = ConfigurationManager.AppSettings.Get("VKAppSecret"),
                Provider = new VkAuthenticationProvider
                {
                    OnAuthenticated = (context) =>
                    {
                        context.Identity.AddClaim(new Claim("urn:vkontakte:token", context.AccessToken));
                        return Task.FromResult(true);
                    }
                },
                AuthenticationType = "vkontakte"
            });
        }
    }
}
