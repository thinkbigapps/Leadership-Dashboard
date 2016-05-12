using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using BusinessObjects;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Specialized;
using System.Web.Util;

namespace ExceptionDashboard
{
    public partial class ManageEmployees : System.Web.UI.Page
    {
        private ExEventManager _myExEventManager = new ExEventManager();
        private EmployeeManager _myEmployeeManager = new EmployeeManager();

        List<Employee> _employeeList = new List<Employee>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

            //Check to see if user is logged in
            if (Session["loggedInUser"] != null)
            {
                try
                {
                    //Create session variable to store logged in user's employee object
                    Employee loggedInEmployee = (Employee)Session["loggedInUser"];

                    //Build page based on user attributes
                    populateLoggedIn(loggedInEmployee.RoleName, loggedInEmployee.DepartmentName);
                }
                catch (Exception)
                {
                    Response.Redirect("AgentView.aspx");
                }
            }

            //Hide form fields if user is not logged in
            else
            {
                btnViewReport.Visible = false;
                listTopLevel.Items.Clear();
                listDepartment.Items.Clear();
                listRepresentative.Items.Clear();
                lblNoData.Text = "Please log in to continue";
                searchTable.Visible = false;
                gvExEvent.Visible = false;
            }
        }

        //populate fields based on user login
        private void populateLoggedIn(string role, string departmentID)
        {
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            List<Employee> currentAgent = new List<Employee>();
            currentAgent.Add(loggedInEmployee);
            if (!IsPostBack) 
            { 
                if ((loggedInEmployee.RoleName == "Supervisor") || (loggedInEmployee.RoleName == "Lead"))
                {
                    //populate top level list with sup/lead/agent
                    List<Role> currentRoles = _myEmployeeManager.FetchRoleList();
                    listTopLevel.DataTextField = "roleName";
                    listTopLevel.DataValueField = "roleName";
                    listTopLevel.DataSource = currentRoles;
                    listTopLevel.DataBind();
                    
                    //populate department list with all departments
                    List<Department> currentDepartments = _myEmployeeManager.FetchDepartmentList();
                    listDepartment.DataTextField = "departmentName";
                    listDepartment.DataValueField = "departmentName";
                    listDepartment.DataSource = currentDepartments;
                    listDepartment.DataBind();

                    //populate sup list will all employees that have role of sup in employee's department
                    List<Employee> currentSups = _myEmployeeManager.FindEmployeesToList(loggedInEmployee.RoleName, loggedInEmployee.DepartmentName).OrderBy(o => o.FullName).ToList();

                    listRepresentative.DataTextField = "FullName";
                    listRepresentative.DataValueField = "employeeID";
                    listRepresentative.DataSource = currentSups;
                    listRepresentative.DataBind();
                }

                //verify page load is not from submit/edit pages
                if (Session["resetToken"] == null)
                {
                    //select previously selected top level to bind employee drop down with list of employees with that role
                    if (Session["selectedTopLevel"] != null)
                    {
                        listTopLevel.SelectedValue = Session["selectedTopLevel"].ToString();
                    }
                    if (Session["selectedDepartment"] != null)
                    {
                        listDepartment.SelectedValue = Session["selectedDepartment"].ToString();
                    }
                    else
                    {
                        listTopLevel.SelectedValue = loggedInEmployee.RoleName;
                        listRepresentative.SelectedValue = loggedInEmployee.EmployeeID.ToString();
                    }
                    
                }
                //if selectedTopLevel session var is set, select that top level from drop down, otherwise page is from submit/edit page - grab top level from query string
                else
                {
                    if (Session["selectedTopLevel"] != null)
                    {
                        listTopLevel.SelectedValue = Session["selectedTopLevel"].ToString();
                    }
                    if (Session["selectedDepartment"] != null)
                    {
                        listDepartment.SelectedValue = Session["selectedDepartment"].ToString();
                    }
                    else
                    {
                        listTopLevel.SelectedValue = Request.QueryString["topLevel"].Replace("\"", "");
                    }
                }
                listDepartment.SelectedValue = loggedInEmployee.DepartmentName;
 
            }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            }
            catch (Exception)
            {
                Response.Redirect("AgentView.aspx");
            }

            if (listTopLevel.SelectedValue == "Supervisor")
            {
                string fullSupName = listRepresentative.SelectedItem.Text;
                string[] splitSupName = fullSupName.Split(',');
                for (int i = 0; i < splitSupName.Length; i++)
                {
                    splitSupName[i] = splitSupName[i].Trim();
                }
                string supLastName = splitSupName[0].ToString();
                string supFirstName = splitSupName[1].ToString();

                _employeeList = _myEmployeeManager.FindLeadReports(supFirstName, supLastName);
            }

            if (_employeeList.Count() != 0)
            {
                lblNoData.Visible = false;
                gvExEvent.Visible = true;
                Session["currentEmployeeList"] = _employeeList;
                Session["SortDirection"] = "ASC";
                gvExEvent.DataSource = _employeeList.OrderBy(o => o.FullName).ToList();
                gvExEvent.DataBind();
            }
            //if event list is empty, hide gridview and display 'no records' message
            else
            {
                lblNoData.Visible = true;
                gvExEvent.Visible = false;
            }
        }

        protected void gvExEvent_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            ///**Start Test Code**/
            var ddl = e.Row.FindControl("ddlActivity") as DropDownList;
            if (ddl != null)
            {
                List<BusinessObjects.Activity> actList = _myExEventManager.FetchActivityList();
                ddl.DataTextField = "ActivityName";
                ddl.DataValueField = "ActivityName";
                ddl.DataSource = actList;
                //ddl.DataSource = new List<string>() { "0", "1" };
                ddl.DataBind();
            }
        }

        protected void gvExEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            //determine the event id of the row selected
            //create session variable to hold event id between pages
            //bring up edit popup window
            int selectedExEvent = (int)gvExEvent.SelectedValue;
            Session["selectedEventID"] = selectedExEvent;
        }

        protected void gvExEvent_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvExEvent_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void listTopLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //when selected top level is changed, determine what was selected and rebind 
            var selectedTopLevel = listTopLevel.SelectedValue;
            Session["selectedTopLevel"] = selectedTopLevel;
            Session.Remove("resetToken");
            populateRepList();
        }

        public void populateRepList()
        {
            List<BusinessObjects.Employee> repList = new List<BusinessObjects.Employee>();
            string toplevel = listTopLevel.SelectedItem.Value;
            string department = listDepartment.SelectedItem.Value;
            //find list of employees based on selected role and department
            try
            {
                repList = _myEmployeeManager.FindEmployeesToList(toplevel, department).OrderBy(o => o.FullName).ToList(); 
            }
            catch (Exception)
            {
                throw;
            }
            //bind employe drop down list from list created
            listRepresentative.DataTextField = "FullName";
            listRepresentative.DataValueField = "employeeID";
            listRepresentative.DataSource = repList;
            listRepresentative.DataBind();
        }

        protected void listDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedDepartment = listDepartment.SelectedValue;
            Session["selectedDepartment"] = selectedDepartment;
            populateRepList();
        }


        protected void gvExEvent_Sorting(object sender, GridViewSortEventArgs e)
        {
            //***gridview column sort method not working after 
            List<ExEvent> myGridResults = (List<ExEvent>)Session["currentEventList"];

            var param = Expression.Parameter(typeof(ExEvent), e.SortExpression);
            var sortExpression = Expression.Lambda<Func<ExEvent, object>>(
                Expression.Convert(Expression.Property(param, e.SortExpression), typeof(object)), param);

            //Check sort direction and revert it for next call ASC to DESC and DESC to ASC
            if (Session["SortDirection"] != null)
            {
                if (Session["SortDirection"].ToString() == "ASC")
                {
                    var selectedNew = myGridResults.AsQueryable<ExEvent>().OrderBy(sortExpression);

                    gvExEvent.DataSource = selectedNew;
                    gvExEvent.DataBind();
                
                    Session["SortDirection"] = "DESC";
                }
                else if (Session["SortDirection"].ToString() == "DESC")
                {
                    var selectedNew = myGridResults.AsQueryable<ExEvent>().OrderByDescending(sortExpression);

                    gvExEvent.DataSource = selectedNew;
                    gvExEvent.DataBind();
               
                    Session["SortDirection"] = "ASC";
                }
            }
        }

        protected void gvExEvent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void listRepresentative_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvExEvent_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvExEvent.EditIndex = e.NewEditIndex;
            gvExEvent.DataBind();
        }


        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}