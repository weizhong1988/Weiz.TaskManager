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

        public PageOf<UserModel> GetUserList(int pageNo, int pageSize)
        {
            return dal.GetUserList(pageNo, pageSize);
        }

        public UserModel GetUserModel(string userName, string pwd)
        {
            return dal.GetUserModel(userName, pwd);
        }
    }
}
