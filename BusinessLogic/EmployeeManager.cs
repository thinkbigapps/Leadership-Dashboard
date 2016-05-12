using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace BusinessLogic
{
    public class EmployeeManager
    {
        public Employee FindEmployee(string username)
        {
            try
            {
                var employeeToFind = EmployeeAccessor.SelectEmployee(username);
                return employeeToFind;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem accessing the database.", ex);
            }
        }

        public Employee FindEmployeeByResetID(string resetID)
        {
            try
            {
                var employeeToFind = EmployeeAccessor.SelectEmployeeByResetID(resetID);
                return employeeToFind;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem accessing the database.", ex);
            }
        }

        public Employee FindSingleEmployee(int employeeID)
        {
            try
            {
                var employeeToFind = EmployeeAccessor.SelectSingleEmployee(employeeID);
                return employeeToFind;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem accessing the database.", ex);
            }
        }

        public List<Employee> FindEmployeesToList(string role, string department)
        {
            try
            {
                return EmployeeAccessor.SelectEmployeesToList(role, department);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Employee> FindLeadReports(string supFirstName, string supLastName)
        {
            try
            {
                return EmployeeAccessor.SelectDirectReports(supFirstName, supLastName);
            }
            finally
            {

            }
        }

        public List<Employee> FindSupAgents(string supFirstName, string supLastName)
        {
            try
            {
                return EmployeeAccessor.SelectSupAgentReports(supFirstName, supLastName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Role> FetchRoleList()
        {
            try
            {
                return EmployeeAccessor.SelectRoleList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Department> FetchDepartmentList()
        {
            try
            {
                return EmployeeAccessor.SelectDepartmentList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Employee> FetchEmployeesbyRole(string roleName)
        {
            try
            {
                return EmployeeAccessor.SelectEmployeesByRole(roleName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddNewEmployee(Employee newEmployee)
        {
            int rows;
            try
            {
                rows = EmployeeAccessor.InsertNewEmployee(newEmployee);
            }
            catch (Exception)
            {
                throw;
            }

            if (rows == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GeneratePasswordHash(string thisPassword)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] tmpSource;
            byte[] tmpHash;
    
            tmpSource = ASCIIEncoding.ASCII.GetBytes(thisPassword); // Turn password into byte array
            tmpHash = md5.ComputeHash(tmpSource);

            StringBuilder sOutput = new StringBuilder(tmpHash.Length);
            for (int i = 0; i < tmpHash.Length; i++)
            {
                sOutput.Append(tmpHash[i].ToString("X2"));  // X2 formats to hexadecimal
            }
            return sOutput.ToString();
        }

        public Boolean VerifyHashPassword(string thisPassword, string thisHash)
        {
            Boolean IsValid = false;
            string tmpHash = GeneratePasswordHash(thisPassword); // Call the routine on user input
            if (tmpHash == thisHash) IsValid = true;  // Compare to previously generated hash
            return IsValid;
        }

        public Boolean isValidPassword(string thisPassword)
        {
            string newPassword = sanitizeInput(thisPassword);
            Regex regX = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
            return regX.Match(newPassword).Success;
        }

        public string sanitizeInput(string thisInput)
        {
            Regex regX = new Regex(@"([<>""'%;()&])");
            return regX.Replace(thisInput, "");
        }

        public void UpdateEmployee(Employee oldE, Employee newE)
        {
            try
            {
                EmployeeAccessor.UpdateEmployee(oldE, newE);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ResetEmployeePass(Employee oldE, Employee newE)
        {
            try
            {
                EmployeeAccessor.ResetEmployeePass(oldE, newE);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
