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

        public PageOf<TaskModel> GetTaskList(int pageIndex, int pageSize)
        {
            return dal.GetTaskList(pageIndex, pageSize);
        }

        public List<TaskModel> GetAllTaskList()
        {
            return dal.GetAllTaskList();
        }

        public TaskModel GetById(string taskId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(string taskId)
        {
            return dal.UpdateTaskStatus(taskId, -1);
        }

        public bool UpdateTaskStatus(string taskId, int status)
        {
            return dal.UpdateTaskStatus(taskId, status);
        }

        public bool UpdateNextFireTime(string taskId, DateTime nextFireTime)
        {
            return dal.UpdateNextFireTime(taskId, nextFireTime);
        }


        public bool UpdateRecentRunTime(string taskId, DateTime recentRunTime)
        {
            return dal.UpdateRecentRunTime(taskId, recentRunTime);
        }

        public TaskModel GetTaskById(string taskId)
        {
            return dal.GetTaskById(taskId);
        }

        public bool Add(TaskModel task)
        {
            return dal.Add(task);
        }

        public bool Edit(TaskModel task)
        {
            return dal.Edit(task);
        }
    }
}