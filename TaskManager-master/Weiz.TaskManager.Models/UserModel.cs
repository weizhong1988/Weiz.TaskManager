using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weiz.TaskManager.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string TrueName { get; set; }

        public string UserEmail { get; set; }

        public string PhoneNum { get; set; }

        public int IsAdmin { get; set; }

        public int Status { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? LastLoginTime { get; set; }
    }
}
