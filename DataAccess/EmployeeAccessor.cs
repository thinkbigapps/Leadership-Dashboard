using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class EmployeeAccessor
    {
        public static Employee SelectEmployee(string username)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT employee_id, role_name, department_name, supervisor_fname, supervisor_lname, first_name, last_name, username, password, email_address, new_passwd_expire, new_passwd_id FROM employee WHERE username = @username";
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            //cmd.Parameters.AddWithValue("@password", password);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new ApplicationException("Employee not found.");
                }

                var newEmployee = new Employee();
                reader.Read();
                newEmployee.EmployeeID = reader.GetInt32(0);
                newEmployee.RoleName = reader.GetString(1);
                newEmployee.DepartmentName = reader.GetString(2);
                newEmployee.SupervisorFirstName = reader.GetString(3);
                newEmployee.SupervisorLastName = reader.GetString(4);
                newEmployee.FirstName = reader.GetString(5);
                newEmployee.LastName = reader.GetString(6);
                newEmployee.Username = reader.GetString(7);
                newEmployee.Password = reader.GetString(8);
                newEmployee.EmailAddress = reader.GetString(9);
                newEmployee.FullName = reader.GetString(6) + ", " + reader.GetString(5);

                if (!reader.IsDBNull(10))
                {
                    newEmployee.NewPassExpire = reader.GetDateTime(10);
                }
                if (!reader.IsDBNull(11))
                {
                    newEmployee.NewPassID = reader.GetString(11);
                }
                
                return newEmployee;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public static Employee SelectSingleEmployeeByName(string fname, string lname)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT employee_id, role_name, department_name, supervisor_fname, supervisor_lname, first_name, last_name, username, password, email_address, new_passwd_expire, new_passwd_id FROM employee WHERE first_name = @first_name AND last_name = @last_name";
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@first_name", fname);
            cmd.Parameters.AddWithValue("@last_name", lname);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new ApplicationException("Employee not found.");
                }

                var newEmployee = new Employee();
                reader.Read();
                newEmployee.EmployeeID = reader.GetInt32(0);
                newEmployee.RoleName = reader.GetString(1);
                newEmployee.DepartmentName = reader.GetString(2);
                newEmployee.SupervisorFirstName = reader.GetString(3);
                newEmployee.SupervisorLastName = reader.GetString(4);
                newEmployee.FirstName = reader.GetString(5);
                newEmployee.LastName = reader.GetString(6);
                newEmployee.Username = reader.GetString(7);
                newEmployee.Password = reader.GetString(8);
                newEmployee.EmailAddress = reader.GetString(9);
                newEmployee.FullName = reader.GetString(6) + ", " + reader.GetString(5);

                if (!reader.IsDBNull(10))
                {
                    newEmployee.NewPassExpire = reader.GetDateTime(10);
                }
                if (!reader.IsDBNull(11))
                {
                    newEmployee.NewPassID = reader.GetString(11);
                }

                return newEmployee;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public static Employee SelectEmployeeByResetID(string resetID)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT employee_id, role_name, department_name, supervisor_fname, supervisor_lname, first_name, last_name, username, password, email_address, new_passwd_expire, new_passwd_id FROM employee WHERE new_passwd_id = @resetID";
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@resetID", resetID);
            //cmd.Parameters.AddWithValue("@password", password);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new ApplicationException("Employee not found!!!");
                }

                var newEmployee = new Employee();
                reader.Read();
                newEmployee.EmployeeID = reader.GetInt32(0);
                newEmployee.RoleName = reader.GetString(1);
                newEmployee.DepartmentName = reader.GetString(2);
                newEmployee.SupervisorFirstName = reader.GetString(3);
                newEmployee.SupervisorLastName = reader.GetString(4);
                newEmployee.FirstName = reader.GetString(5);
                newEmployee.LastName = reader.GetString(6);
                newEmployee.Username = reader.GetString(7);
                newEmployee.Password = reader.GetString(8);
                newEmployee.EmailAddress = reader.GetString(9);
                newEmployee.FullName = reader.GetString(6) + ", " + reader.GetString(5);
                newEmployee.NewPassExpire = reader.GetDateTime(10);
                newEmployee.NewPassID = reader.GetString(11);

                return newEmployee;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public static Employee SelectSingleEmployee(int employeeID)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT employee_id, role_name, department_name, supervisor_fname, supervisor_lname, first_name, last_name, username, password, email_address FROM employee WHERE employee_id = @employeeID";
            var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@employeeID", employeeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new ApplicationException("Employee not found.");
                }

                var newEmployee = new Employee();
                reader.Read();
                newEmployee.EmployeeID = reader.GetInt32(0);
                newEmployee.RoleName = reader.GetString(1);
                newEmployee.DepartmentName = reader.GetString(2);
                newEmployee.SupervisorFirstName = reader.GetString(3);
                newEmployee.SupervisorLastName = reader.GetString(4);
                newEmployee.FirstName = reader.GetString(5);
                newEmployee.LastName = reader.GetString(6);
                newEmployee.Username = reader.GetString(7);
                newEmployee.Password = reader.GetString(8);
                newEmployee.EmailAddress = reader.GetString(9);
                newEmployee.FullName = reader.GetString(6) + ", " + reader.GetString(5);

                return newEmployee;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Employee> SelectEmployeesToList(string role, string department)
        {
            var ListToDisplay = new List<Employee>();

            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT employee_id, role_name, department_name, supervisor_fname, supervisor_lname, first_name, last_name, username, password, email_address FROM employee WHERE role_name = @role AND department_name = @department";

            var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@department", department);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var newEmployee = new Employee();

                        newEmployee.EmployeeID = reader.GetInt32(0);
                        newEmployee.RoleName = reader.GetString(1);
                        newEmployee.DepartmentName = reader.GetString(2);
                        newEmployee.SupervisorFirstName = reader.GetString(3);
                        newEmployee.SupervisorLastName = reader.GetString(4);
                        newEmployee.FirstName = reader.GetString(5);
                        newEmployee.LastName = reader.GetString(6);
                        newEmployee.Username = reader.GetString(7);
                        newEmployee.Password = reader.GetString(8);
                        newEmployee.EmailAddress = reader.GetString(9);
                        newEmployee.FullName = reader.GetString(6) + ", " + reader.GetString(5);

                        ListToDisplay.Add(newEmployee);
                    }
                }
                else
                {
                    var ax = new ApplicationException("No records were found in the database with the information submitted");
                    throw ax;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return ListToDisplay;
        }

        public static List<Employee> SelectDirectReports(string supFirstName, string supLastName)
        {
            var LeadReportList = new List<Employee>();

            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT employee_id, role_name, department_name, supervisor_fname, supervisor_lname, first_name, last_name, username, password, email_address FROM employee WHERE supervisor_fname = @supFirstName AND supervisor_lname = @supLastName";
            var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("supFirstName", supFirstName);
            cmd.Parameters.AddWithValue("supLastName", supLastName);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var newEmployee = new Employee();
                        
                        newEmployee.EmployeeID = reader.GetInt32(0);
                        newEmployee.RoleName = reader.GetString(1);
                        newEmployee.DepartmentName = reader.GetString(2);
                        newEmployee.SupervisorFirstName = reader.GetString(3);
                        newEmployee.SupervisorLastName = reader.GetString(4);
                        newEmployee.FirstName = reader.GetString(5);
                        newEmployee.LastName = reader.GetString(6);
                        newEmployee.Username = reader.GetString(7);
                        newEmployee.Password = reader.GetString(8);
                        newEmployee.EmailAddress = reader.GetString(9);
                        newEmployee.FullName = reader.GetString(6) + ", " + reader.GetString(5);
                        newEmployee.FullSupName = newEmployee.SupervisorLastName + ", " + newEmployee.SupervisorFirstName;
                        LeadReportList.Add(newEmployee);
                    }
                }
                //else
                //{
                //    var ax = new ApplicationException("No employee records were found");
                //    throw ax;
                //}
            }
            //catch (Exception)
            //{
            //    throw;
            //}
            finally
            {
                conn.Close();
            }

            return LeadReportList;
        }

        public static List<Employee> SelectSupAgentReports(string supFirstName, string supLastName)
        {
            List<Employee> ReportingLeadList = SelectAgentToSupEmployees(supFirstName, supLastName, "Lead");
            var SupAgentList = new List<Employee>();
            foreach (Employee lead in ReportingLeadList)
            {
                List<Employee> AgentReportingList = SelectDirectReports(lead.FirstName, lead.LastName);
                if (AgentReportingList.Count != 0)
                {
                    foreach (Employee agent in AgentReportingList)
                    {
                        SupAgentList.Add(agent);
                    }
                }
                else
                {
                    continue;
                }
            }
            List<Employee> CheckedAgentToSupList = new List<Employee>();
            CheckedAgentToSupList = SelectAgentToSupEmployees(supFirstName, supLastName, "Agent");
            if (CheckedAgentToSupList.Count != 0)
            {
                foreach (Employee agentToSup in CheckedAgentToSupList)
                {
                    SupAgentList.Add(agentToSup);
                }
            }

            return SupAgentList;
        }

        public static List<Employee> SelectAgentToSupEmployees(string sup_fname, string sup_lname, string role)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT employee_id, role_name, department_name, supervisor_fname, supervisor_lname, first_name, last_name, username, password, email_address FROM employee WHERE supervisor_fname = @sup_fname AND supervisor_lname = @sup_lname AND role_name = @role";
            var cmd = new SqlCommand(query, conn);
            var ListToDisplay2 = new List<Employee>();

            cmd.Parameters.AddWithValue("@sup_fname", sup_fname);
            cmd.Parameters.AddWithValue("@sup_lname", sup_lname);
            cmd.Parameters.AddWithValue("@role", role);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var newEmployee = new Employee();

                        newEmployee.EmployeeID = reader.GetInt32(0);
                        newEmployee.RoleName = reader.GetString(1);
                        newEmployee.DepartmentName = reader.GetString(2);
                        newEmployee.SupervisorFirstName = reader.GetString(3);
                        newEmployee.SupervisorLastName = reader.GetString(4);
                        newEmployee.FirstName = reader.GetString(5);
                        newEmployee.LastName = reader.GetString(6);
                        newEmployee.Username = reader.GetString(7);
                        newEmployee.Password = reader.GetString(8);
                        newEmployee.EmailAddress = reader.GetString(9);
                        newEmployee.FullName = reader.GetString(6) + ", " + reader.GetString(5);

                        ListToDisplay2.Add(newEmployee);
                    }
                }
                //else
                //{
                //    var ax = new ApplicationException("No records were found in the database with the information submitted");
                //    throw ax;
                //}
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return ListToDisplay2;
        }


        public static List<Role> SelectRoleList()
        {
            var RoleList = new List<Role>();

            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT role_name FROM role";
            var cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var newRole = new Role();
                        
                        newRole.roleName = reader.GetString(0);
                        
                        RoleList.Add(newRole);
                    }
                }
                else
                {
                    var ax = new ApplicationException("No employee records were found");
                    throw ax;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return RoleList;
        }

        public static List<Department> SelectDepartmentList()
        {
            var DepartmentList = new List<Department>();

            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT Department_name FROM Department";
            var cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var newDepartment = new Department();

                        newDepartment.departmentName = reader.GetString(0);

                        DepartmentList.Add(newDepartment);
                    }
                }
                else
                {
                    var ax = new ApplicationException("No employee records were found");
                    throw ax;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return DepartmentList;
        }

        public static List<Employee> SelectEmployeesByRole(string roleName)
        {
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var query = @"SELECT employee_id, role_name, department_name, supervisor_fname, supervisor_lname, first_name, last_name, username, password, email_address FROM employee WHERE role_name = @roleName";
            var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@roleName", roleName);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new ApplicationException("Employee not found.");
                }

                var employeeByRoleList = new List<Employee>();

                while (reader.Read())
                {
                    var newEmployee = new Employee();
                    newEmployee.EmployeeID = reader.GetInt32(0);
                    newEmployee.RoleName = reader.GetString(1);
                    newEmployee.DepartmentName = reader.GetString(2);
                    newEmployee.SupervisorFirstName = reader.GetString(3);
                    newEmployee.SupervisorLastName = reader.GetString(4);
                    newEmployee.FirstName = reader.GetString(5);
                    newEmployee.LastName = reader.GetString(6);
                    newEmployee.Username = reader.GetString(7);
                    newEmployee.Password = reader.GetString(8);
                    newEmployee.EmailAddress = reader.GetString(9);
                    newEmployee.FullName = reader.GetString(6) + ", " + reader.GetString(5);

                    employeeByRoleList.Add(newEmployee);
                }
                return employeeByRoleList;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public static int InsertNewEmployee(Employee e)
        {
            var rowsAffected = 0;

            var conn = DatabaseConnection.GetExEventDatabaseConnection();

            string commandText = "INSERT INTO employee (role_name, department_name, supervisor_fname, supervisor_lname, first_name, last_name, username, password, email_address) VALUES (@role_name, @department_name, @supervisor_fname, @supervisor_lname, @first_name, @last_name, @username, @password, @email_address)";

            var cmd = new SqlCommand(commandText, conn);

            cmd.Parameters.AddWithValue("@role_name", e.RoleName);
            cmd.Parameters.AddWithValue("@department_name", e.DepartmentName);
            cmd.Parameters.AddWithValue("@supervisor_fname", e.SupervisorFirstName);
            cmd.Parameters.AddWithValue("@supervisor_lname", e.SupervisorLastName);
            cmd.Parameters.AddWithValue("@first_name", e.FirstName);
            cmd.Parameters.AddWithValue("@last_name", e.LastName);
            cmd.Parameters.AddWithValue("@username", e.Username);
            cmd.Parameters.AddWithValue("@password", e.Password);
            cmd.Parameters.AddWithValue("@email_address", e.EmailAddress);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    var up = new ApplicationException("User account could not be created");
                    throw up;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        public static int UpdateEmployee(Employee oldE, Employee newE)
        {
            int rowsAffected = 0;
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var cmdText = "spUpdateEmployeeReset";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@employee_id", newE.EmployeeID);
            cmd.Parameters.AddWithValue("@role_name", newE.RoleName);
            cmd.Parameters.AddWithValue("@department_name", newE.DepartmentName);
            cmd.Parameters.AddWithValue("@supervisor_fname", newE.SupervisorFirstName);
            cmd.Parameters.AddWithValue("@supervisor_lname", newE.SupervisorLastName);
            cmd.Parameters.AddWithValue("@first_name", newE.FirstName);
            cmd.Parameters.AddWithValue("@last_name", newE.LastName);
            cmd.Parameters.AddWithValue("@username", newE.Username);
            cmd.Parameters.AddWithValue("@password", newE.Password);
            cmd.Parameters.AddWithValue("@email_address", newE.EmailAddress);
            cmd.Parameters.AddWithValue("@new_passwd_expire", newE.NewPassExpire);
            cmd.Parameters.AddWithValue("@new_passwd_id", newE.NewPassID);

            cmd.Parameters.AddWithValue("@original_employee_id", oldE.EmployeeID);
            cmd.Parameters.AddWithValue("@original_role_name", oldE.RoleName);
            cmd.Parameters.AddWithValue("@original_department_name", oldE.DepartmentName);
            cmd.Parameters.AddWithValue("@original_supervisor_fname", oldE.SupervisorFirstName);
            cmd.Parameters.AddWithValue("@original_supervisor_lname", oldE.SupervisorLastName);
            cmd.Parameters.AddWithValue("@original_first_name", oldE.FirstName);
            cmd.Parameters.AddWithValue("@original_last_name", oldE.LastName);
            cmd.Parameters.AddWithValue("@original_username", oldE.Username);
            cmd.Parameters.AddWithValue("@original_password", oldE.Password);
            cmd.Parameters.AddWithValue("@original_email_address", oldE.EmailAddress);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Concurrency Exception:\\nYour record has been changed by another user.\nPlease refresh and try again.");
                }

            }
            catch (Exception)
            {
                throw;
            }

            return rowsAffected;
        }

        public static int ResetEmployeePass(Employee oldE, Employee newE)
        {
            int rowsAffected = 0;
            var conn = DatabaseConnection.GetExEventDatabaseConnection();
            var cmdText = "spResetEmployeePass";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@employee_id", newE.EmployeeID);
            cmd.Parameters.AddWithValue("@role_name", newE.RoleName);
            cmd.Parameters.AddWithValue("@department_name", newE.DepartmentName);
            cmd.Parameters.AddWithValue("@supervisor_fname", newE.SupervisorFirstName);
            cmd.Parameters.AddWithValue("@supervisor_lname", newE.SupervisorLastName);
            cmd.Parameters.AddWithValue("@first_name", newE.FirstName);
            cmd.Parameters.AddWithValue("@last_name", newE.LastName);
            cmd.Parameters.AddWithValue("@username", newE.Username);
            cmd.Parameters.AddWithValue("@password", newE.Password);
            cmd.Parameters.AddWithValue("@email_address", newE.EmailAddress);
            cmd.Parameters.AddWithValue("@new_passwd_expire", newE.NewPassExpire);
            cmd.Parameters.AddWithValue("@new_passwd_id", newE.NewPassID);

            cmd.Parameters.AddWithValue("@original_employee_id", oldE.EmployeeID);
            cmd.Parameters.AddWithValue("@original_role_name", oldE.RoleName);
            cmd.Parameters.AddWithValue("@original_department_name", oldE.DepartmentName);
            cmd.Parameters.AddWithValue("@original_supervisor_fname", oldE.SupervisorFirstName);
            cmd.Parameters.AddWithValue("@original_supervisor_lname", oldE.SupervisorLastName);
            cmd.Parameters.AddWithValue("@original_first_name", oldE.FirstName);
            cmd.Parameters.AddWithValue("@original_last_name", oldE.LastName);
            cmd.Parameters.AddWithValue("@original_username", oldE.Username);
            cmd.Parameters.AddWithValue("@original_email_address", oldE.EmailAddress);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Concurrency Exception:\\nYour record has been changed by another user.\nPlease refresh and try again.");
                }

            }
            catch (Exception)
            {
                throw;
            }

            return rowsAffected;
        }
    }
}
