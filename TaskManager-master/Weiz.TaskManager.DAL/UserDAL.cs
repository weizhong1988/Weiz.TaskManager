using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Weiz.TaskManager.Models;
using Weiz.TaskManager.Common;
using Weiz.TaskManager.DBUtility;

namespace Weiz.TaskManager.DAL
{
    public class UserDAL
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageOf<UserModel> GetUserList(int pageNo, int pageSize)
        {
            var QUERY_SQL = @"( select UserId,UserName,PassWord,TrueName,UserEmail,PhoneNum,IsAdmin,Status,CreateTime,LastLoginTime
                                from  p_User";

            QUERY_SQL += ") pp ";
            string SQL = string.Format(@" select * from (select ROW_NUMBER() OVER(order by pp.CreateTime desc) AS RowNum,* from {0}
										) as A where A.RowNum BETWEEN (@PageIndex-1)* @PageSize+1 AND @PageIndex*@PageSize ORDER BY RowNum;",
                                  QUERY_SQL);

            SQL += string.Format(@" SELECT COUNT(1) FROM {0};", QUERY_SQL);

            object param = new { pageIndex = pageNo, pageSize = pageSize };

            DataSet ds = SQLHelper.FillDataSet(SQL, param);
            return new PageOf<UserModel>()
            {
                PageIndex = pageNo,
                PageSize = pageSize,
                Total = Convert.ToInt32(ds.Tables[1].Rows[0][0]),
                Items = DataMapHelper.DataSetToList<UserModel>(ds)
            };
        }

        /// <summary>
        /// 根据用户名和密码获取管理员用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public UserModel GetUserModel(string userName, string pwd)
        {
            var sql = @"  select UserId,UserName,PassWord,TrueName,UserEmail,PhoneNum,IsAdmin,Status,CreateTime,LastLoginTime
                          from  p_User
                          where UserName=@UserName and PassWord = @PassWord";

            object param = new { UserName = userName, PassWord = pwd };

            return SQLHelper.Single<UserModel>(sql, param);
           
        }
    }
}
