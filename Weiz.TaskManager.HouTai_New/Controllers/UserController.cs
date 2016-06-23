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
    public class UserController : BaseController
    {
        public ActionResult List()
        {
            UserBLL userBll = new UserBLL();
            var users = userBll.GetUserList(PageNo, PageSize);
            return View(users);
        }
    }
}