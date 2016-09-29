using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Weiz.TaskManager.Models
{

    public class PageOf<T>
    {
        /// <summary>
        /// 信息对象列表
        /// </summary>
        public IList<T> Items { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int RowCount { get; set; }
    }
}
