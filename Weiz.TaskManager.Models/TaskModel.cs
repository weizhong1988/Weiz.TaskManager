using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weiz.TaskManager.Models
{
    /// <summary>
    /// 任务实体
    /// </summary>
    public class TaskModel
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public Guid TaskID { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 任务执行参数
        /// </summary>
        public string TaskParam { get; set; }

        /// <summary>
        /// 运行频率设置
        /// </summary>
        public string CronExpressionString { get; set; }

        /// <summary>
        /// 任务运频率中文说明
        /// </summary>
        public string CronRemark { get; set; }

        /// <summary>
        /// 任务所在DLL对应的程序集名称
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// 任务所在类
        /// </summary>
        public string ClassName { get; set; }

        public TaskStatus Status { get; set; }

        /// <summary>
        /// 任务创建时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 任务修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 任务最近运行时间
        /// </summary>
        public DateTime? RecentRunTime { get; set; }

        /// <summary>
        /// 任务下次运行时间
        /// </summary>
        public DateTime? NextFireTime { get; set; }

        /// <summary>
        /// 任务备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDelete { get; set; }
    } 
}
