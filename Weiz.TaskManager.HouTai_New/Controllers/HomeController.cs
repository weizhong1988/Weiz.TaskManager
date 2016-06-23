using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Weiz.TaskManager.HouTai_New.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult LoginOut()
        {
        
            Session["loginuser"] = null;

            string redirectUrl = string.Empty;
            if (Request.UrlReferrer != null)
            {
                redirectUrl = HttpUtility.UrlEncode(Request.UrlReferrer.PathAndQuery);
            }
            string loginUrl = FormsAuthentication.LoginUrl;
            if (Request.HttpMethod == System.Net.WebRequestMethods.Http.Get && !string.IsNullOrEmpty(redirectUrl))
            {
                loginUrl += "?ReturnUrl=" + redirectUrl;
            }
            return Redirect(loginUrl);
        }
    }
}
