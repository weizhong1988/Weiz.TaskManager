using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weiz.TaskManager.DAL;
using Weiz.TaskManager.Models;

namespace Weiz.TaskManager.BLL
{
    public class LogBLL
    {
        private readonly LogDAL dal = new LogDAL();

        public void WriteRunInfo(string taskName, string taskId, string result)
        {
            dal.WriteRunInfo(taskName, taskId, result);
        }

        public void WriteErrorInfo(string sLevel, string sMessage, string sException, string sName)
        {
            dal.WriteErrorInfo(sLevel,sMessage, sException, sName);
        }

        public PageOf<ErrorLogModel> GetErrorLogList(int pageNo, int pageSize)
        {
            return dal.GetErrorLogList(pageNo, pageSize);
        }

        public PageOf<RunLogModel> GetRunLogList(int pageNo, int pageSize)
        {
            return dal.GetRunLogList(pageNo, pageSize);
        }
    }
}