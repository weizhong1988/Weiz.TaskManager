using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weiz.TaskManager.DAL;
using Weiz.TaskManager.Models;

namespace Weiz.TaskManager.BLL
{
    public class TaskBLL
    {
        private readonly TaskDAL dal = new TaskDAL();

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageOf<TaskModel> GetTaskList(int pageIndex, int pageSize)
        {
            return dal.GetTaskList(pageIndex, pageSize);
        }

        /// <summary>
        /// 读取数据库中全部的任务
        /// </summary>
        /// <returns></returns>
        public List<TaskModel> GetAllTaskList()
        {
            return dal.GetAllTaskList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public TaskModel GetById(string taskId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public bool DeleteById(string taskId)
        {
            return dal.UpdateTaskStatus(taskId, -1);
        }

        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateTaskStatus(string taskId, int status)
        {
            return dal.UpdateTaskStatus(taskId, status);
        }

        /// <summary>
        /// 修改任务的下次启动时间
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="nextFireTime"></param>
        /// <returns></returns>
        public bool UpdateNextFireTime(string taskId, DateTime nextFireTime)
        {
            return dal.UpdateNextFireTime(taskId, nextFireTime);
        }

        /// <summary>
        /// 根据任务Id 修改 上次运行时间
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="recentRunTime"></param>
        /// <returns></returns>
        public bool UpdateRecentRunTime(string taskId, DateTime recentRunTime)
        {
            return dal.UpdateRecentRunTime(taskId, recentRunTime);
        }

        /// <summary>
        /// 根据任务Id 获取任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public TaskModel GetTaskById(string taskId)
        {
            return dal.GetTaskById(taskId);
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public bool Add(TaskModel task)
        {
            return dal.Add(task);
        }

        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public bool Edit(TaskModel task)
        {
            return dal.Edit(task);
        }
    }
}