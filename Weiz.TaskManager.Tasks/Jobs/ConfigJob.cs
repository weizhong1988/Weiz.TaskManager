using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weiz.TaskManager.Common;
using Weiz.TaskManager.Models;
using Weiz.TaskManager.TaskUtility;

namespace Ywdsoft.Task.TaskSet
{
    /// <summary>
    /// 动态读取TaskConfig.xml配置文件，看是否修改了配置文件(新增任务，修改任务，删除任务)
    /// 来动态更改当前运行的任务信息,解决只能停止Windows服务才能添加新任务问题
    /// </summary>
    ///<remarks>DisallowConcurrentExecution属性标记任务不可并行，要是上一任务没运行完即使到了运行时间也不会运行</remarks>
    [DisallowConcurrentExecution]
    public class ConfigJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                LogHelper.WriteLog("Job修改任务开始,当前系统时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                ///获取所有执行中的任务
                List<TaskModel> listTask = TaskHelper.GetAllTaskList().Where(e => e.IsDelete == 0).ToList<TaskModel>();
                //开始对比当前配置文件和上一次配置文件之间的改变
                var currentTaskList = TaskHelper.CurrentTaskList();
                //1.修改的任务
                var UpdateJobList = (from p in listTask
                                     from q in currentTaskList
                                     where p.TaskID == q.TaskID && (p.TaskParam != q.TaskParam || p.AssemblyName != q.AssemblyName || p.ClassName != q.ClassName ||
                                        p.CronExpressionString != q.CronExpressionString
                                     )
                                     select new { NewTaskUtil = p, OriginTaskUtil = q }).ToList();

                foreach (var item in UpdateJobList)
                {
                    try
                    {
                        QuartzHelper.ScheduleJob(item.NewTaskUtil);
                        //修改原有的任务
                        int index = currentTaskList.IndexOf(item.OriginTaskUtil);
                        currentTaskList[index] = item.NewTaskUtil;

                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteLog(string.Format("任务“{0}”配置信息更新失败！", item.NewTaskUtil.TaskName), e);
                    }
                }

                //2.新增的任务(TaskID在原集合不存在)
                var AddJobList = (from p in listTask
                                  where !(from q in currentTaskList select q.TaskID).Contains(p.TaskID)
                                  select p).ToList();

                foreach (var taskUtil in AddJobList)
                {
                    try
                    {
                        QuartzHelper.ScheduleJob(taskUtil);
                        //添加新增的任务
                        currentTaskList.Add(taskUtil);
                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteLog(string.Format("任务“{0}”新增失败！", taskUtil.TaskName), e);
                    }
                }

                //3.删除的任务
                var DeleteJobList = (from p in currentTaskList
                                     where !(from q in listTask select q.TaskID).Contains(p.TaskID)
                                     select p).ToList();
                foreach (var taskUtil in DeleteJobList)
                {
                    try
                    {
                        QuartzHelper.DeleteJob(taskUtil.TaskID.ToString());
                        //添加新增的任务
                        currentTaskList.Remove(taskUtil);
                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteLog(string.Format("任务“{0}”删除失败！", taskUtil.TaskName), e);
                    }
                }
                if (UpdateJobList.Count > 0 || AddJobList.Count > 0 || DeleteJobList.Count > 0)
                {
                    LogHelper.WriteLog("Job修改任务执行完成后,系统当前的所有任务信息:" + JsonHelper.ToJson(currentTaskList));
                }
                else
                {
                    LogHelper.WriteLog("当前没有修改的任务");
                }
            }
            catch (Exception ex)
            {
                JobExecutionException e2 = new JobExecutionException(ex);
                LogHelper.WriteLog("Job修改任务异常", ex);
                //1.立即重新执行任务 
                e2.RefireImmediately = true;
            }
        }
    }
}
