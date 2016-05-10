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
    public partial class ManagerView : System.Web.UI.Page
    {
        private ExEventManager _myExEventManager = new ExEventManager();
        private EmployeeManager _myEmployeeManager = new EmployeeManager();

        List<BusinessObjects.ExEvent> _completeSupEventList = new List<BusinessObjects.ExEvent>();
        List<BusinessObjects.ExEvent> _completeLeadEventList = new List<BusinessObjects.ExEvent>();
        List<BusinessObjects.ExEvent> _completeAgentEventList = new List<BusinessObjects.ExEvent>();

        protected void Page_Load(object sender, EventArgs e)
        {
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
                listDepartment.Items.Clear();
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
                if (loggedInEmployee.RoleName == "Manager")
                {
                    //populate department list with all departments
                    List<Department> currentDepartments = _myEmployeeManager.FetchDepartmentList();
                    listDepartment.DataTextField = "departmentName";
                    listDepartment.DataValueField = "departmentName";
                    listDepartment.DataSource = currentDepartments;
                    listDepartment.DataBind();

                }

                //verify page load is not from submit/edit pages

                //if selectedTopLevel session var is set, select that top level from drop down, otherwise page is from submit/edit page - grab top level from query string
                else
                {
                    if (Session["selectedDepartment"] != null)
                    {
                        listDepartment.SelectedValue = Session["selectedDepartment"].ToString();
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
            string department = listDepartment.SelectedItem.Value;
            lblNoData.Text = "No records to display.";
            lblNoData.ForeColor = System.Drawing.Color.Red;

            List<ManagerExEvent> _managerEvents = new List<ManagerExEvent>();
            try
            {
                //capture input data and grab report
                _managerEvents = _myExEventManager.FetchManagerEvent(department);

            }
            catch (Exception)
            {
                    
            }

            if (_managerEvents.Count() != 0)
            {
                //statement added to page load that checks for edit/submit interaction causes gridview to duplicate events due to addl page load from view report button click event firing
                //***will address logic bug later***
                //for now, capture duplicate listings and filter
                //var distinctItems = _managerEvents.GroupBy(x => x.eventID).Select(y => y.First());
                lblNoData.Visible = false;
                gvExEvent.Visible = true;
                Session["currentEventList"] = _managerEvents;
                Session["SortDirection"] = "ASC";
                gvExEvent.DataSource = _managerEvents.OrderBy(o => o.supLastName).ToList();
                //gvExEvent.DataSource = _managerEvents;
                gvExEvent.DataBind();
            }
            //if event list is empty, hide gridview and display 'no records' message
            else
            {
                lblNoData.Visible = true;
                gvExEvent.Visible = false;
            }
        }


        protected void gvExEvent_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //detect if event status is completed
            //if so, remove button to complete event
            //clear session variable for edit/submit changes for next page load
            //LinkButton lbComplete = (LinkButton)e.Row.Cells[7].FindControl("lbComplete");

            //if (e.Row.Cells[5].Text == "Completed")
            //{
            //    lbComplete.Visible = false;
            //}
        }

        protected void gvExEvent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //add click events for each row to detect selected row and bring up edit page for that event

            ////insert edit and complete buttons into each row
            ////***ADDING 'Edit' button as some users were reporting that they weren't aware they could click the row to edit***
            //LinkButton lbComplete = (LinkButton)e.Row.Cells[7].FindControl("lbComplete");
            //LinkButton lbEdit = (LinkButton)e.Row.Cells[6].FindControl("lbEdit");

            ////check to see if event is already completed or rejected, change edit button text to 'View'
            //if (e.Row.Cells[5].Text == "Completed" || e.Row.Cells[5].Text == "Rejected")
            //{
            //    lbComplete.Visible = false;
            //    lbEdit.Text = "View";
            //}

            //Employee loggedInAgent = (Employee)Session["loggedInUser"];

            ////check to see if the logged in user is an agent, remove ability to complete events
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (loggedInAgent.RoleName == "Agent")
            //    {
            //        LinkButton lbComplete2 = (LinkButton)e.Row.Cells[7].FindControl("lbComplete");
            //        lbComplete2.Visible = false;
            //    }
            //}
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
            else if (e.CommandName == "CompleteEvent")
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


        protected void listDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedDepartment = listDepartment.SelectedValue;
            Session["selectedDepartment"] = selectedDepartment;
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
    }
}