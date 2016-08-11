using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weiz.TaskManager.BLL;

namespace Weiz.TaskManager.TaskUtility
{
    public class DbLogHelper
    {
        public static LogBLL log = new LogBLL();

        /// <summary>
        /// 记录task 的运行日志
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="taskId"></param>
        /// <param name="result"></param>
        public static void WriteRunInfo(string taskName, string taskId, string result)
        {
            var task = new Task(() => log.WriteRunInfo(taskName, taskId, result));
            task.Start();
        }

        /// <summary>
        /// 记录task 的错误日志
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteErrorInfo(Exception ex)
        {
            var task = new Task(() => log.WriteErrorInfo("ERROR", ex.Message, ex.StackTrace, "Weiz.TaskManager.Tasks"));
            task.Start();
        }
    }
}
