using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using Weiz.TaskManager.Common;
using Weiz.TaskManager.Models;
using Weiz.TaskManager.TaskUtility;

namespace Weiz.TaskManager.TaskSet
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
                //获取所有数据库或者配置文件中的任务
                List<TaskModel> listTask = TaskHelper.GetAllTaskList().Where(e => e.IsDelete == 0).ToList<TaskModel>();

                //开始对比当前配置文件和上一次配置文件之间的改变
                // 1. 停用或启用
                //1.修改的任务
                var StatusJobList = (from p in listTask
                                     from q in QuartzHelper.CurrentTaskList
                                     where p.TaskID == q.TaskID && p.Status!= q.Status
                                     
                                     select  p ).ToList();
                foreach (var item in StatusJobList)
                {
                    try
                    {
                        if (item.Status == TaskStatus.RUN)
                        {
                            QuartzHelper.ResumeJob(item.TaskID.ToString());
                        }
                        else
                        {
                            QuartzHelper.PauseJob(item.TaskID.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteLog("自动暂停任务失败，错误：" + e.Message + e.StackTrace);
                    }
                }


                //2.修改的任务
                var UpdateJobList = (from p in listTask
                                     from q in QuartzHelper.CurrentTaskList
                                     where p.TaskID == q.TaskID && (p.TaskParam != q.TaskParam || p.AssemblyName != q.AssemblyName || p.ClassName != q.ClassName ||
                                        p.CronExpressionString != q.CronExpressionString
                                     )
                                     select new { NewTaskModel = p, OriginTaskUtil = q }).ToList();
                foreach (var item in UpdateJobList)
                {
                    try
                    {
                        QuartzHelper.ScheduleJob(item.NewTaskModel, true);
                        //修改原有的任务
                        int index = QuartzHelper.CurrentTaskList.IndexOf(item.OriginTaskUtil);
                        QuartzHelper.CurrentTaskList[index] = item.NewTaskModel;

                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteLog("自动修改任务失败，错误：" + e.Message + e.StackTrace);
                    }
                }

                //3.新增的任务(TaskID在原集合不存在)
                var AddJobList = (from p in listTask
                                  where !(from q in QuartzHelper.CurrentTaskList select q.TaskID).Contains(p.TaskID)
                                  select p).ToList();

                foreach (var taskModel in AddJobList)
                {
                    try
                    {
                        QuartzHelper.ScheduleJob(taskModel);
                        //添加新增的任务
                        QuartzHelper.CurrentTaskList.Add(taskModel);
                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteLog("自动增加任务失败，错误：" + e.Message + e.StackTrace);
                    }
                }

                //4.删除的任务
                var DeleteJobList = (from p in QuartzHelper.CurrentTaskList
                                     where !(from q in listTask select q.TaskID).Contains(p.TaskID)
                                     select p).ToList();
                foreach (var taskModel in DeleteJobList)
                {
                    try
                    {
                        QuartzHelper.DeleteJob(taskModel.TaskID.ToString());
                        //移除删除的任务
                        QuartzHelper.CurrentTaskList.Remove(taskModel);
                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteLog("自动删除任务失败，错误：" + e.Message + e.StackTrace);
                    }
                }
                if (UpdateJobList.Count > 0 || AddJobList.Count > 0 || DeleteJobList.Count > 0)
                {
                    LogHelper.WriteLog("Job修改任务执行完成后,系统当前的所有任务信息:" + JsonHelper.ToJson(QuartzHelper.CurrentTaskList));
                }
                else
                {
                    LogHelper.WriteLog("当前没有修改的任务");
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
