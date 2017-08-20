using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using BusinessObjects;

namespace ExceptionDashboard
{
    public partial class AgentSubmit : System.Web.UI.Page
    {
        private ExEventManager _myExEventManager = new ExEventManager();
        private EmployeeManager _myEmployeeManager = new EmployeeManager();
        public BusinessObjects.Employee loggedInEmployee;

        protected void Page_Load(object sender, EventArgs e)
        {
            checkLogin();
            //retrieve current activity list from db
            //bind activity list to drop down list
            List<Activity> activitiesToList = _myExEventManager.FetchActivityList();
            if (!IsPostBack)
            {
                listActivity.DataTextField = "activityName";
                listActivity.DataValueField = "activityName";
                listActivity.DataSource = activitiesToList;
                listActivity.DataBind();
                listActivity.SelectedValue = "Huddle";
                txtEventDate.Text = DateTime.Today.Date.ToShortDateString();
            }
            //assign loggedInEmployee to session variable
            if (Session["loggedInUser"] != null)
            {
                loggedInEmployee = (BusinessObjects.Employee)Session["loggedInUser"]; 
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            checkLogin();
            //capture form fields
            DateTime eventDate = Convert.ToDateTime(txtEventDate.Text);
            int EmployeeID = loggedInEmployee.EmployeeID;
            DateTime submissionDate = DateTime.Now;
            string activity = listActivity.SelectedItem.Value;
            string startTime = startHour.Text + ":" + startMinute.Text + " " + startAMPM.Text;
            string endTime = endHour.Text + ":" + endMinute.Text + " " + endAMPM.Text;
            string statusName = "Pending";
            string note = txtActivityNote.Text;
            //create new event object with form data
            BusinessObjects.ExEvent eventToAdd = new BusinessObjects.ExEvent();
            eventToAdd.eventDate = eventDate;
            eventToAdd.employeeID = EmployeeID;
            eventToAdd.submissionDate = submissionDate;
            eventToAdd.activityName = activity;
            eventToAdd.startTime = startTime;
            eventToAdd.endTime = endTime;
            eventToAdd.statusName = statusName;
            eventToAdd.activityNote = note;
            //add event to db
            try
            {
                _myExEventManager.AddExEvent(eventToAdd);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myCloseScript", "window.close()", true);
                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Submission Received');", true);
                

            }
            catch (Exception)
            {
                
            }
        }

        public void checkLogin()
        {
            if (Session["loggedInUser"] == null)
            {
                Response.Redirect("AgentView.aspx");
            }
        }
    }
}