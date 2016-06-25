using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class ConsultationSheet
    {
        public int sheetID { get; set; }
        public int employeeID { get; set; }
        public string createdDate { get; set; }
        public string completedDate { get; set; }
    }
}
