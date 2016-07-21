using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using BusinessObjects;
using System.Linq;

namespace ExceptionDashboard
{
    public partial class EditEmployee : System.Web.UI.Page
    {
        private ExEventManager _myExEventManager = new ExEventManager();
        private EmployeeManager _myEmployeeManager = new EmployeeManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            //checkLogin();
            if (!IsPostBack)
            {
                
            }

            if (Session["selectedEmployeeID"] != null)
            {
                if (!IsPostBack)
                {
                    //extract current event id from session variable
                    int selectedEmployeeID = (int)Session["selectedEmployeeID"];
                    //lblName.Text = selectedEmployeeID.ToString();

                    BusinessObjects.Employee selectedEmployee = _myEmployeeManager.FindSingleEmployee(selectedEmployeeID);



                    List<Department> deptList = _myEmployeeManager.FetchDepartmentList();
                    ddlDepartment.DataTextField = "departmentName";
                    ddlDepartment.DataValueField = "departmentName";
                    ddlDepartment.DataSource = deptList;
                    ddlDepartment.DataBind();

                    ddlDepartment.SelectedValue = selectedEmployee.DepartmentName;

                    //search db for event by event id
                    
                    lblNameField.Text = selectedEmployee.FirstName + " " + selectedEmployee.LastName;
                    lblRoleField.Text = selectedEmployee.RoleName;

                    List<Employee> supList = _myEmployeeManager.SelectSupsByDept(ddlDepartment.SelectedValue);
                    List<Employee> sortedSupList = supList.OrderBy(o => o.LastName).ToList();
                    ddlSupervisor.DataTextField = "FullName";
                    ddlSupervisor.DataValueField = "FullName";
                    ddlSupervisor.DataSource = sortedSupList;
                    ddlSupervisor.DataBind();
                    string supName = selectedEmployee.SupervisorLastName + ", " + selectedEmployee.SupervisorFirstName;
                    selectedEmployee.FullSupName = supName;
                    ddlSupervisor.SelectedValue = selectedEmployee.FullSupName;
                    //lblSupervisor.Text = selectedEmployee.FullSupName;
                }
            }
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedDepartment = ddlDepartment.SelectedValue;
            List<Employee> newSupList = _myEmployeeManager.SelectSupsByDept(selectedDepartment);
            List<Employee> sortedSupList = newSupList.OrderBy(o => o.LastName).ToList();
            ddlSupervisor.DataTextField = "FullName";
            ddlSupervisor.DataValueField = "FullName";
            ddlSupervisor.DataSource = sortedSupList;
            ddlSupervisor.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int selectedEmployeeID = (int)Session["selectedEmployeeID"];
            Employee currentEmployee = _myEmployeeManager.FindSingleEmployee(selectedEmployeeID);
            Employee newEmployee = currentEmployee;
            newEmployee.DepartmentName = ddlDepartment.SelectedValue;

            string fullSupName = ddlSupervisor.SelectedValue;

            string[] splitSupName = fullSupName.Split(',');
            for (int i = 0; i < splitSupName.Length; i++)
            {
                splitSupName[i] = splitSupName[i].Trim();
            }
            string supLastName = splitSupName[0].ToString();
            string supFirstName = splitSupName[1].ToString();

            newEmployee.SupervisorFirstName = supFirstName;
            newEmployee.SupervisorLastName = supLastName;
            if (ckDeactivate.Checked == true)
            {
                newEmployee.SupervisorFirstName = "Hosting";
                newEmployee.SupervisorLastName = "Support";
            }
            newEmployee.NewPassExpire = Convert.ToDateTime("1/1/1900");
            newEmployee.NewPassID = "00000000";
            _myEmployeeManager.UpdateEmployee(currentEmployee, newEmployee);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseWindow", "window.close()", true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseWindow", "window.close()", true);
        }
    }
}