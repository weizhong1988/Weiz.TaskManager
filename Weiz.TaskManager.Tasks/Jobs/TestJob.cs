using Weiz.TaskManager.Utility;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Weiz.TaskManager.Models;
using Weiz.TaskManager.TaskUtility;

namespace Weiz.TaskManager.TaskSet
{
    /// <summary>
    /// 测试任务
    /// </summary>
    ///<remarks>DisallowConcurrentExecution属性标记任务不可并行，要是上一任务没运行完即使到了运行时间也不会运行</remarks>
    [DisallowConcurrentExecution]
    public class TestJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                // 1. 获取Task 基本信息及 Task 配置的其他参数,任务启动时会读取配置文件节点的值传递过来
                TaskModel task = QuartzHelper.GetTaskDetail(context);
                // 2. 记录Task 运行状态数据库
                DbLogHelper.WriteRunInfo(task.TaskName + " 开始", task.TaskID.ToString(), "");
                // 3. 开始执行相关任务
                LogHelper.WriteLog(task.TaskName + ",当前系统时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                // 4. 记录Task 运行状态数据库
                DbLogHelper.WriteRunInfo(task.TaskName + " 结束", task.TaskID.ToString(), "成功执行");
            }
            catch (Exception ex)
            {
                JobExecutionException e2 = new JobExecutionException(ex);
                // 记录异常到数据库和 log 文件中。
                DbLogHelper.WriteErrorInfo(ex);
                LogHelper.WriteLog("测试任务异常", ex);

                //true  是立即重新执行任务 
                e2.RefireImmediately = true;
            }
        }
    }
}
