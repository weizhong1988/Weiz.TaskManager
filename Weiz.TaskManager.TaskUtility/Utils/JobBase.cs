using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Weiz.TaskManager.Common;
using Weiz.TaskManager.Models;

namespace Weiz.TaskManager.TaskUtility
{
    public class JobBase
    {
        /// <summary>
        /// 执行指定任务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="action"></param>
        public void ExecuteJob(IJobExecutionContext context, Action action)
        {
            try
            {
                // 1. 获取Task 基本信息及 Task 配置的其他参数,任务启动时会读取配置文件节点的值传递过来
                TaskModel task = QuartzHelper.GetTaskDetail(context);
                // 2. 记录Task 运行状态数据库
                DbLogHelper.WriteRunInfo(task.TaskName + " 开始", task.TaskID.ToString(), "");

                // 3. 开始执行相关任务
                PerformanceTracer.Invoke(action, task.TaskName);//监控并执行任务

                // 4. 记录Task 运行状态数据库
                DbLogHelper.WriteRunInfo(task.TaskName + " 结束", task.TaskID.ToString(), "成功执行");
            }
            catch (Exception ex)
            {
                JobExecutionException e2 = new JobExecutionException(ex);
                //true  是立即重新执行任务 
                e2.RefireImmediately = true;

                // 记录异常到数据库和 log 文件中。
                DbLogHelper.WriteErrorInfo(ex);

            }
        }
    }
}
