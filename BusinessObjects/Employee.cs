using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string RoleName { get; set; }
        public string DepartmentName { get; set; }
        public string SupervisorFirstName { get; set; }
        public string SupervisorLastName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public DateTime NewPassExpire { get; set; }
        public string NewPassID { get; set; }
    }
}
