using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class ExEvent
    {
        public int eventID { get; set; }
        public DateTime eventDate { get; set; }
        public int employeeID { get; set; }
        public DateTime submissionDate { get; set; }
        public string activityName { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string duration { get; set; }
        public string statusName { get; set; }
        public string activityNote { get; set; }
        public DateTime completedDate { get; set; }
        public string completedBy { get; set; }
        public string agentName { get; set; }
    }
}
