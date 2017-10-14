using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Weiz.TaskManager.Models;
using Weiz.TaskManager.Common;

namespace Weiz.TaskManager.TaskUtility
{
    /// <summary>
    /// 任务帮助类
    /// </summary>
    public class TaskHelper
    {
        /// <summary>
        /// 配置文件地址
        /// </summary>
        private static readonly string TaskPath = FileHelper.GetAbsolutePath("Config/TaskConfig.xml");

        private static BLL.TaskBLL task = new BLL.TaskBLL();

        public static bool AddTask(TaskModel model, string action)
        {
            var result = false;

            if (action == "edit")
            {
                result = task.Edit(model);
            }
            else
            {
                model.TaskID = Guid.NewGuid();
                result = task.Add(model);
            }
            return result;
        }

        /// <summary>
        /// 删除指定id任务
        /// </summary>
        /// <param name="TaskID">任务id</param>
        public static void DeleteById(string taskId)
        {
            //QuartzHelper.DeleteJob(taskId);

            task.DeleteById(taskId);
        }

        /// <summary>
        /// 更新任务运行状态
        /// </summary>
        /// <param name="TaskID">任务id</param>
        /// <param name="Status">任务状态</param>
        public static void UpdateTaskStatus(string taskId, TaskStatus Status)
        {
            //if (Status == TaskStatus.RUN)
            //{
            //    QuartzHelper.ResumeJob(taskId);
            //}
            //else
            //{
            //    QuartzHelper.PauseJob(taskId);
            //}
            task.UpdateTaskStatus(taskId, (int)Status);
        }

        /// <summary>
        /// 更新任务下次运行时间
        /// </summary>
        /// <param name="TaskID">任务id</param>
        /// <param name="NextFireTime">下次运行时间</param>
        public static void UpdateNextFireTime(string taskId, DateTime nextFireTime)
        {
            task.UpdateNextFireTime(taskId, nextFireTime);
        }

        /// <summary>
        /// 任务完成后，更新上次执行时间
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="recentRunTime">上次执行时间</param>
        public static void UpdateRecentRunTime(string taskId, DateTime recentRunTime)
        {
            task.UpdateRecentRunTime(taskId, recentRunTime);
        }

        /// <summary>
        /// 从数据库中读取全部任务列表
        /// </summary>
        /// <returns></returns>
        private static IList<TaskModel> TaskInDB()
        {
            return task.GetAllTaskList();
        }

        /// <summary>
        /// 从配置文件中读取任务列表
        /// </summary>
        /// <returns></returns>
        private static IList<TaskModel> ReadTaskConfig()
        {
            return XmlHelper.XmlToList<TaskModel>(TaskPath, "Task");
        }

        /// <summary>
        /// 获取所有启用的任务
        /// </summary>
        /// <returns>所有启用的任务</returns>
        public static IList<TaskModel> GetAllTaskList()
        {
            if (SysConfig.StorageMode == 1)
            {
                return TaskInDB();
            }
            else
            {
                return ReadTaskConfig();
            }
        }


    }
}
