using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using Weiz.TaskManager.TaskUtility;
using Weiz.TaskManager.Utility;
namespace Weiz.TaskManager.ServiceBus
{
    public class TaskManagerServiceBus
    {
        public void Start()
        {
            //配置信息读取
            ConfigInit.InitConfig();
            QuartzHelper.InitScheduler();
            QuartzHelper.StartScheduler();
        }
         
        public void Stop()
        {
            QuartzHelper.StopSchedule();

            System.Environment.Exit(0);
        }
    }
}
