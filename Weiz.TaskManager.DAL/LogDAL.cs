using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Weiz.TaskManager.Models;
using Weiz.TaskManager.Utility;

namespace Weiz.TaskManager.DAL
{
    public class LogDAL
    {
        public void WriteRunInfo(string remark, string taskId, string result)
        {
            var sql = @"INSERT INTO p_RunningLog
                            (TaskID
                            ,Remark
                            ,Description
                            ,CreateTime)
                        VALUES
                            (@TaskID
                            ,@Remark
                            ,@Description
                            ,GETDATE())";

            object param = new { TaskID = taskId, Remark = remark, Description = result };

            SQLHelper.ExecuteNonQuery(sql, param);
        }

        public void WriteErrorInfo(string sLevel , string sMessage, string sException, string sName)
        {
            var sql = @"INSERT INTO p_ErrorLog
                               (dtDate
                               ,sLevel
                               ,sLogger
                               ,sMessage
                               ,sException
                               ,sName)
                         VALUES
                               (GETDATE()
                               ,@sLevel
                               ,@sLogger
                               ,@sMessage
                               ,@sException
                               ,@sName)";

            object param = new { sLevel = sLevel, sLogger = "system", sMessage = sMessage, sException = sException, sName = sName };

            SQLHelper.ExecuteNonQuery(sql, param);
        }

        public PageOf<ErrorLogModel> GetErrorLogList(int pageNo, int pageSize)
        {
            var QUERY_SQL = @"(  select nId,dtDate,sThread,sLevel,sLogger,sMessage,sException,sName
                                 from p_ErrorLog 
                                 where DateDiff(dd,dtDate,getdate())<=30";

            QUERY_SQL += ") pp ";
            string SQL = string.Format(@" select * from (select ROW_NUMBER() OVER(order by pp.dtDate desc) AS RowNum,* from {0}
										) as A where A.RowNum BETWEEN (@PageIndex-1)* @PageSize+1 AND @PageIndex*@PageSize ORDER BY RowNum;",
                                  QUERY_SQL);

            SQL += string.Format(@" SELECT COUNT(1) FROM {0};", QUERY_SQL);

            object param = new { pageIndex = pageNo, pageSize = pageSize };

            DataSet ds = SQLHelper.FillDataSet(SQL, param);
            return new PageOf<ErrorLogModel>()
            {
                PageIndex = pageNo,
                PageSize = pageSize,
                Total = Convert.ToInt32(ds.Tables[1].Rows[0][0]),
                Items = DataMapHelper.DataSetToList<ErrorLogModel>(ds)
            };
        }

        public PageOf<RunLogModel> GetRunLogList(int pageNo, int pageSize)
        {
            var QUERY_SQL = @"( select r.Id,r.Remark,r.Description,r.CreateTime,t.TaskName,t.ClassName 

                                from p_RunningLog(nolock) r inner join p_task(nolock) t on r.TaskID = t.TaskID 
                                where DateDiff(dd,r.CreateTime,getdate())<=30";

            QUERY_SQL += ") pp ";
            string SQL = string.Format(@" select * from (select ROW_NUMBER() OVER(order by pp.CreateTime desc) AS RowNum,* from {0}
										) as A where A.RowNum BETWEEN (@PageIndex-1)* @PageSize+1 AND @PageIndex*@PageSize ORDER BY RowNum;",
                                  QUERY_SQL);

            SQL += string.Format(@" SELECT COUNT(1) FROM {0};", QUERY_SQL);

            object param = new { pageIndex = pageNo, pageSize = pageSize };

            DataSet ds = SQLHelper.FillDataSet(SQL, param);
            return new PageOf<RunLogModel>()
            {
                PageIndex = pageNo,
                PageSize = pageSize,
                Total = Convert.ToInt32(ds.Tables[1].Rows[0][0]),
                Items = DataMapHelper.DataSetToList<RunLogModel>(ds)
            };
        }
    }
}
