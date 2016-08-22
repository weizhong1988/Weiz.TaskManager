using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weiz.TaskManager.Common
{
    /// <summary>
    /// 系统的配置
    /// 获取连接字符串 SysConfig.SqlConnect
    /// </summary>
    public class SysConfig
    {
        /// <summary>
        /// 数据库连接字符串信息
        /// </summary>
        [PathMap(Key = "SqlConnect")]
        public static string SqlConnect { get; set; }

        #region  quartz 服务器的地址和端口

        [PathMap(Key = "QuartzServer")]
        public static string QuartzServer { get; set; }

        [PathMap(Key = "QuartzPort")]
        public static string QuartzPort { get; set; }

        #endregion

        /// <summary>
        /// 任务配置的存储方式
        /// </summary>
        [PathMap(Key = "StorageMode")]
        public static int StorageMode { get; set; }

    }
}
