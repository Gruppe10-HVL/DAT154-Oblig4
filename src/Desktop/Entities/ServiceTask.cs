using Desktop.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Entities
{
    public class ServiceTask
    {
        public int roomId { get; set; }
        public string description { get; set; }
        public ServiceTaskType taskType { get; set; }
        public ServiceTaskStatus taskStatus { get; set; }
        public ServiceTaskPriority priority { get; set; }
        public string notes { get; set; }
    }
}
