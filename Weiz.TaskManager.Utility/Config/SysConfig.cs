using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weiz.TaskManager.Utility
{
    /// <summary>
    /// 缓存系统所有配置信息，以键值对形式存在
    /// </summary>
    /// <remarks>
    /// 系统相关配置信息都应该通过此类的静态属性读取
    /// </remarks>
    /// <example>
    /// 获取连接字符串 SysConfig.SqlConnect
    /// </example>
    /// <summary>
    /// 系统的配置
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
