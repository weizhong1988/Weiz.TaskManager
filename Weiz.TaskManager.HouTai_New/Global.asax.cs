using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Weiz.TaskManager.TaskUtility;
using Weiz.TaskManager.Utility;

namespace Weiz.TaskManager.HouTai_New
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // 配置信息读取
            ConfigInit.InitConfig();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // 获取远程任务服务器上的 Scheduler
            QuartzHelper.InitRemoteScheduler();
        }
    }
}
