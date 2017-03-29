using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using Weiz.TaskManager.Common;
using Weiz.TaskManager.Models;
using Weiz.TaskManager.TaskUtility;

namespace Weiz.TaskManager.TaskSet.Jobs
{

    /// <summary>
    /// 动态读取TaskConfig.xml配置文件，看是否修改了配置文件(新增任务，修改任务，删除任务)
    /// 来动态更改当前运行的任务信息,解决只能停止Windows服务才能添加新任务问题
    /// </summary>
    ///<remarks>DisallowConcurrentExecution属性标记任务不可并行，要是上一任务没运行完即使到了运行时间也不会运行</remarks>
    [DisallowConcurrentExecution]
    public class AutoAddJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                ///获取所有执行中的任务
                List<TaskModel> listTask = TaskHelper.GetAllTaskList().Where(e => e.IsDelete == 0).ToList<TaskModel>();
                //开始对比当前配置文件和上一次配置文件之间的改变

                //2.新增的任务(TaskID在原集合不存在)
                var addJobList = (from p in listTask
                                  where !(from q in QuartzHelper.CurrentTaskList select q.TaskID).Contains(p.TaskID)
                                  select p).ToList();

                foreach (var taskUtil in addJobList)
                {
                    try
                    {
                        QuartzHelper.ScheduleJob(taskUtil);
                        //添加新增的任务
                        QuartzHelper.CurrentTaskList.Add(taskUtil);
                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteLog("自动增加任务失败，错误：" + e.Message + e.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                JobExecutionException e2 = new JobExecutionException(ex);
                //1.立即重新执行任务 
                e2.RefireImmediately = true;
            }
        }
    }

}
