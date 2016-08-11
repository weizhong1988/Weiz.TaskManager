using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weiz.TaskManager.DAL;
using Weiz.TaskManager.Models;

namespace Weiz.TaskManager.BLL
{
    public class UserBLL
    {
        private UserDAL dal = new UserDAL();

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageOf<UserModel> GetUserList(int pageNo, int pageSize)
        {
            return dal.GetUserList(pageNo, pageSize);
        }

        /// <summary>
        /// 根据用户名和密码获取管理员用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public UserModel GetUserModel(string userName, string pwd)
        {
            return dal.GetUserModel(userName, pwd);
        }
    }
}
