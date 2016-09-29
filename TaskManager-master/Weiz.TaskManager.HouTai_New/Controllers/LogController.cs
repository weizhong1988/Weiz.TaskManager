using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Weiz.TaskManager.BLL;
using Weiz.TaskManager.TaskUtility;

namespace Weiz.TaskManager.HouTai_New.Controllers
{
    public class LogController : BaseController
    {
        LogBLL logBll = new LogBLL();

        public ActionResult RunLog()
        {

            var result = logBll.GetRunLogList(PageNo, PageSize);

            return View(result);
        }

        public ActionResult ErrorLog()
        {
            var result = logBll.GetErrorLogList(PageNo, PageSize);

            return View(result);
        }
       
    }
}