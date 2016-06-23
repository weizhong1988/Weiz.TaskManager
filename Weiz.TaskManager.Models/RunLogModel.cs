using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weiz.TaskManager.Models
{
    public class RunLogModel
    {
        public long Id { get; set; }

        public string Remark { get; set; }

        public string Description { get; set; }

        public DateTime? CreateTime { get; set; }

        public string TaskName { get; set; }

        public string ClassName { get; set; }
    }
}
