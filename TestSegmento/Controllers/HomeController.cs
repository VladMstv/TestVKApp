using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestSegmento.BL.API;
using TestSegmento.BL.API.Models;
using TestSegmento.BL.Utils;
using TestSegmento.ViewModels.Home;

namespace TestSegmento.Controllers
{
    [Authorize]
    public class HomeController : CustomController
    {
        public ActionResult Index()
        {
            var model = new ReportViewModel()
            {
                AvatarUrl = AuthenticationManager.User.Claims.FirstOrDefault(x => x.Type == "urn:vkontakte:link")?.Value,
                VkName = AuthenticationManager.User.Claims.FirstOrDefault(x => x.Type == "urn:vkontakte:name")?.Value
            };
            return View(model);
        }

        
        public async Task<JsonResult> GetVKWallPosts(string id)
        {
            var vkPosts = new List<WallPostItem>();
            try
            {
                vkPosts = await VKapi.GetVKWallPostsAsync(id);
            }
            catch(Exception ex)
            {
                return Json(RequestHelper.ProcessException(ex),JsonRequestBehavior.AllowGet);
            }
            return Json(RequestHelper.GenerateSuccessResult(vkPosts), JsonRequestBehavior.AllowGet);
        }


        private VKApiManager _vkApi;
        private VKApiManager VKapi
        {
            get
            {
                if (_vkApi == null)
                {
                    _vkApi = new VKApiManager(AuthenticationManager.User.Claims.FirstOrDefault(x => x.Type == "urn:vkontakte:token")?.Value);
                }
                return _vkApi;
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
    }
}