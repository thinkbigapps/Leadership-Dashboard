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
    public partial class AgentView : System.Web.UI.Page
    {
        private ExEventManager _myExEventManager = new ExEventManager();
        private EmployeeManager _myEmployeeManager = new EmployeeManager();
        
        List<BusinessObjects.ExEvent> _completeSupEventList = new List<BusinessObjects.ExEvent>();
        List<BusinessObjects.ExEvent> _completeLeadEventList = new List<BusinessObjects.ExEvent>();
        List<BusinessObjects.ExEvent> _completeAgentEventList = new List<BusinessObjects.ExEvent>();
        List<BusinessObjects.ExEvent> _myExEventList;

        protected void Page_Load(object sender, EventArgs e)
        {
            //btnViewReport.Click += new EventHandler(this.btnViewReport_Click);
            if (!IsPostBack)
            {
                //Populate start and end date
                txtStartDate.Text = DateTime.Today.ToShortDateString();
                txtEndDate.Text = DateTime.Now.AddDays(1).ToShortDateString();
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
                    //Response.Redirect("AgentView.aspx");
                }
                
                //Check to see if edit/submit pages were interacted with and refresh view page to reflect changes
                if (Request.QueryString["event"] != null)
                {
                    btnViewReport_Click(sender, e);
                }

                //Check to see if page load is from new user creation
                if (Session["newUser"] != null)
                {
                    lblNoData.Visible = true;
                    lblNoData.Text = "Account Successfully Created!";
                    lblNoData.ForeColor = System.Drawing.Color.Black;
                    Session.Remove("newUser");
                }
                else
                {
                    lblNoData.Visible = false;
                }
            }

            //Hide form fields if user is not logged in
            else
            {
                btnViewReport.Visible = false;
                listTopLevel.Items.Clear();
                listDepartment.Items.Clear();
                listRepresentative.Items.Clear();
                listStatus.Items.Clear();
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
                    
                    //populate status list with all statuses
                    List<Status> currentStatuses = _myExEventManager.FetchStatusList();
                    listStatus.DataTextField = "statusName";
                    listStatus.DataValueField = "statusName";
                    listStatus.DataSource = currentStatuses;
                    listStatus.DataBind();
                }
                else if (loggedInEmployee.RoleName == "Manager")
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
                    List<Employee> currentSups = _myEmployeeManager.FindEmployeesToList("Supervisor", loggedInEmployee.DepartmentName);

                    listRepresentative.DataTextField = "FullName";
                    listRepresentative.DataValueField = "employeeID";
                    listRepresentative.DataSource = currentSups;
                    listRepresentative.DataBind();

                    //populate status list with all statuses
                    List<Status> currentStatuses = _myExEventManager.FetchStatusList();
                    listStatus.DataTextField = "statusName";
                    listStatus.DataValueField = "statusName";
                    listStatus.DataSource = currentStatuses;
                    listStatus.DataBind();

                    listTopLevel.SelectedValue = "Supervisor";
                    listDepartment.SelectedValue = loggedInEmployee.DepartmentName;
                }

                else if (loggedInEmployee.RoleName == "Agent")
                {
                    //populate top level list with agent
                    listTopLevel.DataTextField = "roleName";
                    listTopLevel.DataValueField = "roleName";
                    listTopLevel.DataSource = currentAgent;
                    listTopLevel.DataBind();

                    //populate department list with loggedInEmployee.DepartmentID
                    listDepartment.DataTextField = "departmentName";
                    listDepartment.DataValueField = "departmentName";
                    listDepartment.DataSource = currentAgent;
                    listDepartment.DataBind();

                    //populate representative list with loggedInEmployee.EmployeeID
                    listRepresentative.DataTextField = "FullName";
                    listRepresentative.DataValueField = "employeeID";
                    listRepresentative.DataSource = currentAgent;
                    listRepresentative.DataBind();

                    //populate status list with Pending, Completed
                    List<Status> currentStatuses = _myExEventManager.FetchStatusList();
                    listStatus.DataTextField = "statusName";
                    listStatus.DataValueField = "statusName";
                    listStatus.DataSource = currentStatuses;
                    listStatus.DataBind();
                }

                //default select Pending status
                listStatus.SelectedValue = "Pending";

                //verify page load is not from submit/edit pages
                if (Session["resetToken"] == null && loggedInEmployee.RoleName != "Manager")
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
                else if (loggedInEmployee.RoleName != "Manager")
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

            //grab user input data from form and set to var
            DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime endDate = Convert.ToDateTime(txtEndDate.Text);
            string role = listTopLevel.SelectedItem.Value;
            string department = listDepartment.SelectedItem.Value;
            int representative = Convert.ToInt32(listRepresentative.SelectedItem.Value);
            string status = listStatus.SelectedItem.Value;
            lblNoData.Text = "No records to display.";
            lblNoData.ForeColor = System.Drawing.Color.Red;

            //check selected role
            if (role == "Supervisor")
            {
                try
                {
                    //create employee object from supervisor selected
                    BusinessObjects.Employee repSelected = _myEmployeeManager.FindSingleEmployee(representative);

                    //create list of team leaders that report to this supervisor
                    List<BusinessObjects.Employee> _myEmployeeList = _myEmployeeManager.FindSupAgents(repSelected.FirstName, repSelected.LastName);

                    //iterate through and create a new list of all employees that report to each team leader that reports to this supervisor
                    foreach (BusinessObjects.Employee emp in _myEmployeeList)
                    {
                        try
                        {
                            //iterate through and create a new list of all requests submitted by each employee, that reports to each lead, that reports to this supervisor
                            _myExEventList = _myExEventManager.FetchEventsByAgent(emp.EmployeeID, status, startDate, endDate);
                            foreach (BusinessObjects.ExEvent ev in _myExEventList)
                            {
                                _completeSupEventList.Add(ev);
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                catch (Exception)
                {
                    //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('No exceptions to display.');", true);
                }

                if (_completeSupEventList.Count() != 0)
                {
                    //statement added to page load that checks for edit/submit interaction causes gridview to duplicate events due to addl page load from view report button click event firing
                    //***will address logic bug later***
                    //for now, capture duplicate listings and filter
                    //var distinctItems = _completeSupEventList.GroupBy(x => x.eventID).Select(y => y.First());
                    lblNoData.Visible = false;
                    gvExEvent.Visible = true;
                    Session["currentEventList"] = _completeSupEventList;
                    Session["SortDirection"] = "ASC";
                    gvExEvent.DataSource = _completeSupEventList.OrderBy(o => o.agentName).ToList();
                    gvExEvent.DataBind();
                }
                //if event list is empty, hide gridview and display 'no records' message
                else
                {
                    lblNoData.Visible = true;
                    gvExEvent.Visible = false;
                }
            }
            else if (role == "Lead")
            {
                try
                {
                    //find selected lead employee
                    Employee repSelected = _myEmployeeManager.FindSingleEmployee(representative);

                    //create list of employees that report directly to selected lead
                    List<Employee> _myEmployeeList = _myEmployeeManager.FindLeadReports(repSelected.FirstName, repSelected.LastName);

                    //iterate through employee list and create combined list of all events submitted by each employee
                    foreach (Employee emp in _myEmployeeList)
                    {
                        try
                        {
                            _myExEventList = _myExEventManager.FetchEventsByAgent(emp.EmployeeID, status, startDate, endDate);
                            for (int i = 0; i < _myExEventList.Count; i++)
                            {
                                _completeLeadEventList.Add(_myExEventList[i]);
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                }
                catch (Exception)
                {
                    //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('No exceptions to display.');", true);
                }

                //populate gridview with results
                if (_completeLeadEventList.Count() != 0)
                {
                    lblNoData.Visible = false;
                    gvExEvent.Visible = true;
                    Session["currentEventList"] = _completeLeadEventList;
                    Session["SortDirection"] = "ASC";
                    gvExEvent.DataSource = _completeLeadEventList.OrderBy(o => o.agentName).ToList();
                    gvExEvent.DataBind();
                }
                else
                {
                    lblNoData.Visible = true;
                    gvExEvent.Visible = false;
                }
            }
            else if (role == "Agent")
            {
                //retrieve all events submitted by selected agent by status and start/end dates
                try
                {
                    _completeAgentEventList = _myExEventManager.FetchEventsByAgent(representative, status, startDate, endDate);
                }
                catch (Exception)
                {
                    //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('No exceptions to display.');", true);
                }

                //populate gridview with results
                if (_completeAgentEventList.Count != 0)
                {
                    lblNoData.Visible = false;
                    gvExEvent.Visible = true;
                    Session["currentEventList"] = _completeAgentEventList;
                    Session["SortDirection"] = "ASC";
                    gvExEvent.DataSource = _completeAgentEventList;
                    gvExEvent.DataBind();
                }
                else
                {
                    lblNoData.Visible = true;
                    gvExEvent.Visible = false;
                } 
            }
        }

        //capture query string and clear if variables are present
        //***Not working as predicted after last update***
        //***disabling for now, will come back to later***
        private void clearQueryString()
        {
            if (Request.QueryString["employee"] != null)
            {
                PropertyInfo Isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

                Isreadonly.SetValue(Request.QueryString, false, null);

                Request.QueryString.Clear();
            }
        }

        protected void gvExEvent_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //detect if event status is completed
            //if so, remove button to complete event
            //clear session variable for edit/submit changes for next page load
            LinkButton lbComplete = (LinkButton)e.Row.Cells[7].FindControl("lbComplete");

            if(e.Row.Cells[5].Text=="Completed")
            {
                lbComplete.Visible = false;
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
            //add click events for each row to detect selected row and bring up edit page for that event

            ////insert edit and complete buttons into each row
            ////***ADDING 'Edit' button as some users were reporting that they weren't aware they could click the row to edit***
            LinkButton lbComplete = (LinkButton)e.Row.Cells[7].FindControl("lbComplete");
            LinkButton lbEdit = (LinkButton)e.Row.Cells[6].FindControl("lbEdit");

            //check to see if event is already completed or rejected, change edit button text to 'View'
            if (e.Row.Cells[5].Text == "Completed" || e.Row.Cells[5].Text == "Rejected")
            {
                lbComplete.Visible = false;
                lbEdit.Text = "View";
            }

            Employee loggedInAgent = (Employee)Session["loggedInUser"];

            //check to see if the logged in user is an agent, remove ability to complete events
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (loggedInAgent.RoleName == "Agent")
                {
                    LinkButton lbComplete2 = (LinkButton)e.Row.Cells[7].FindControl("lbComplete");
                    lbComplete2.Visible = false;
                }
            }
        }

        protected void gvExEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            //determine the event id of the row selected
            //create session variable to hold event id between pages
            //bring up edit popup window
            int selectedExEvent = (int)gvExEvent.SelectedValue;
            Session["selectedEventID"] = selectedExEvent;
            ScriptManager.RegisterStartupScript(this, GetType(), "showModalPopUp", "showModalPopUp();", true);
        }

        protected void gvExEvent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //determine which button was selected, either bring up edit window or complete the event selected
            Session.Remove("resetToken");
            if (e.CommandName == "EditEvent")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvExEvent.Rows[index];
                int id = (int)gvExEvent.DataKeys[index].Value;
                Session["selectedEventID"] = id;
                ScriptManager.RegisterStartupScript(this, GetType(), "showModalPopUp", "showModalPopUp();", true);
            }
            else if(e.CommandName == "CompleteEvent")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvExEvent.Rows[index];
                int id = (int)gvExEvent.DataKeys[index].Value;
                Session["selectedEventID"] = id;
                updateEvent("Completed");
                
                btnViewReport_Click(sender, e);
            }
        }

        public void updateEvent(string status)
        {
            //retrieve logged in user info
            //create new event
            //update new event to reflect submitted changes
            BusinessObjects.Employee loggedInEmployee = (BusinessObjects.Employee)Session["loggedInUser"];
            try
            {
                int selectedEventID = (int)Session["selectedEventID"];

                BusinessObjects.ExEvent originalExEvent = _myExEventManager.FetchEvent(selectedEventID);
                BusinessObjects.ExEvent updatedExEvent = new BusinessObjects.ExEvent();

                updatedExEvent.eventID = originalExEvent.eventID;
                updatedExEvent.eventDate = originalExEvent.eventDate;
                updatedExEvent.employeeID = originalExEvent.employeeID;
                updatedExEvent.submissionDate = originalExEvent.submissionDate;
                updatedExEvent.activityName = originalExEvent.activityName;
                updatedExEvent.startTime = originalExEvent.startTime;
                updatedExEvent.endTime = originalExEvent.endTime;
                updatedExEvent.statusName = status;
                updatedExEvent.activityNote = originalExEvent.activityNote;

                updatedExEvent.completedBy = loggedInEmployee.FirstName + " " + loggedInEmployee.LastName;
                updatedExEvent.completedDate = DateTime.Now;
                _myExEventManager.UpdateExEvent(originalExEvent, updatedExEvent);  
            }
            catch (Exception)
            {
                throw;
            }
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

        protected void btnMonthToDate_Click(object sender, EventArgs e)
        {
            //database dates are for 12:00AM, set end date to 1 day later to pull through 11:59PM that night
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            txtStartDate.Text = firstDayOfMonth.ToShortDateString();
            txtEndDate.Text = DateTime.Today.AddDays(1).ToShortDateString();
        }

        protected void btnLastMonth_Click(object sender, EventArgs e)
        {
            //populate date fields for start/end last month
            DateTime firstDayOfLastMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime firstDay = firstDayOfLastMonth.AddMonths(-1);
            DateTime lastDayOfLastMonth = firstDay.AddMonths(1).AddDays(-1);
            txtStartDate.Text = firstDay.ToShortDateString();
            txtEndDate.Text = lastDayOfLastMonth.ToShortDateString();
        }

        protected void btnBPToDate_Click(object sender, EventArgs e)
        {
            //previous issues with bptodate not pulling correct start date
            //***updated button to be <72 hours, since that is the threshold where supervisors/leads are able to enter exceptions***
            txtStartDate.Text = DateTime.Today.AddDays(-3).ToShortDateString();
            txtEndDate.Text = DateTime.Today.AddDays(1).ToShortDateString();
        }

        protected void btnPriorBP_Click(object sender, EventArgs e)
        {
            //***disabled bptodate function, updated button to "ALL" to pull all exceptions***
            txtStartDate.Text = "1/1/2000";
            txtEndDate.Text = DateTime.Today.AddDays(-3).ToShortDateString();
        }

        protected void gvExEvent_Sorting(object sender, GridViewSortEventArgs e)
        {
            //***gridview column sort method not working after 
            List<ExceptionDashboard.ExEvent> myGridResults = (List<ExceptionDashboard.ExEvent>)Session["currentEventList"];

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

        protected void btnBPToDate_Click1(object sender, EventArgs e)
        {
            //***disabled bptodate function, updated button to >72 hours to show exceptions that would require manager approval past 72 hours
            txtStartDate.Text = "1/1/2000";
            txtEndDate.Text = DateTime.Today.AddDays(1).ToShortDateString();
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
    }
}