using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Weiz.TaskManager.TaskUtility;

namespace Weiz.TaskManager.HouTai_New.Controllers
{
    public class CronController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult NextFireTime()
        {
            string cronExpressionString = Request.Params["CronExpression"].ToString();
            try
            {
                var result = QuartzHelper.GetNextFireTime(cronExpressionString, 5);

                JavaScriptSerializer js = new JavaScriptSerializer();

                string msg = js.Serialize(result);

                return Json(new { result = true, msg = msg });
            }
            catch
            {
                return Json(new { result = false, msg = "" });
            }
        }
    }
}