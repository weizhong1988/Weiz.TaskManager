using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weiz.TaskManager.Models
{
    public class ErrorLogModel
    {
        public long nId { get; set; }

        public DateTime? dtDate { get; set; }

        public string sThread { get; set; }

        public string sLevel { get; set; }

        public string sLogger { get; set; }

        public string sMessage { get; set; }

        public string sException { get; set; }

        public string sName { get; set; }
    }
}
