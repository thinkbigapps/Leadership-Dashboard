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
    public partial class EditSubmission : System.Web.UI.Page
    {
        private ExEventManager _myExEventManager = new ExEventManager();
        private EmployeeManager _myEmployeeManager = new EmployeeManager();
        char[] delimiterChars = { ' ', ':' };
        protected void Page_Load(object sender, EventArgs e)
        {
            checkLogin();
            if (!IsPostBack)
            {
                //find current list of activities and bind to activity drop down list
                List<Activity> activitiesToList = _myExEventManager.FetchActivityList();
                listActivity.DataTextField = "activityName";
                listActivity.DataValueField = "activityName";
                listActivity.DataSource = activitiesToList;
                listActivity.DataBind();
            }
            //disable form fields by default on page load
            lblCompletedOn.Visible = false;
            txtCompletedOn.Visible = false;
            lblCompletedBy.Visible = false;
            txtCompletedBy.Visible = false;
            btnSave.Visible = false;
            btnMarkCompleted.Visible = false;
            btnMarkRejected.Visible = false;
            btnMarkPending.Visible = false;
            btnPurgeEvent.Visible = false;

            if (Session["selectedEventID"] != null)
            {
                if (!IsPostBack)
                {
                    //extract current event id from session variable
                    int selectedEventID = (int)Session["selectedEventID"];
                    txtEventDate.Text = selectedEventID.ToString();

                    try
                    {
                        //search db for event by event id
                        BusinessObjects.ExEvent selectedExEvent = _myExEventManager.FetchEvent(selectedEventID);
                        try
                        {
                            //search db for employee that event belongs to, then populate form field with employee full name
                            BusinessObjects.Employee selectedEmployee = new BusinessObjects.Employee();
                            selectedEmployee = _myEmployeeManager.FindSingleEmployee(selectedExEvent.employeeID);
                            txtAgent.Text = selectedEmployee.FirstName + " " + selectedEmployee.LastName;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        //obtain info for logged in user
                        //populate remaining form fields with user/event info
                        Employee loggedInEmployee = (Employee)Session["loggedInUser"];
                        txtStatus.Text = selectedExEvent.statusName;
                        txtSubmissionDate.Text = selectedExEvent.submissionDate.ToShortDateString();
                        txtEventDate.Text = (selectedExEvent.eventDate).ToShortDateString();
                        listActivity.SelectedValue = selectedExEvent.activityName;
                        string eventStartTime = selectedExEvent.startTime;
                        string[] splitStartTime = eventStartTime.Split(delimiterChars);
                        startHour.SelectedValue = splitStartTime[0];
                        startMinute.SelectedValue = splitStartTime[1];
                        startAMPM.SelectedValue = splitStartTime[2];

                        string eventEndTime = selectedExEvent.endTime;
                        string[] splitEndTime = eventEndTime.Split(delimiterChars);
                        endHour.SelectedValue = splitEndTime[0];
                        endMinute.SelectedValue = splitEndTime[1];
                        endAMPM.SelectedValue = splitEndTime[2];

                        txtActivityNote.Text = selectedExEvent.activityNote;

                        //if event is completed, show info for completed by user and completed on date
                        if (selectedExEvent.statusName == "Completed")
                        {
                            lblCompletedOn.Visible = true;
                            txtCompletedOn.Visible = true;
                            lblCompletedBy.Visible = true;
                            txtCompletedBy.Visible = true;
                            txtCompletedOn.Text = (selectedExEvent.completedDate).ToShortDateString();
                            txtCompletedBy.Text = selectedExEvent.completedBy;
                        }
                        //if the event is pending, and the logged in user is a supervisor or lead, display buttons to complete or reject the event
                        else if (selectedExEvent.statusName == "Pending" && loggedInEmployee.RoleName != "Agent")
                        {
                            btnSave.Visible = true;
                            btnMarkCompleted.Visible = true;
                            btnMarkRejected.Visible = true;
                        }
                        //if the event is rejected, and the logged in user is a supervisor or lead, display buttons to purge the event or revert the status back to pending
                        else if (selectedExEvent.statusName == "Rejected" && loggedInEmployee.RoleName != "Agent")
                        {
                            lblCompletedOn.Visible = true;
                            txtCompletedOn.Visible = true;
                            lblCompletedBy.Visible = true;
                            txtCompletedBy.Visible = true;
                            btnMarkPending.Visible = true;
                            btnPurgeEvent.Visible = true;
                            txtCompletedOn.Text = (selectedExEvent.completedDate).ToShortDateString();
                            txtCompletedBy.Text = selectedExEvent.completedBy;
                        }
                        //if the event is pending, and the user is not a supervisor or lead, only display button to save changes to the event
                        else if (selectedExEvent.statusName == "Pending")
                        {
                            btnSave.Visible = true;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Close();
        }

        protected void btnMarkCompleted_Click(object sender, EventArgs e)
        {
            checkLogin();
            //when marking the event as complete on the popup edit screen, create session variable to let the other pages know that a submission was made to resubmit the gridview on the parent page to reflect changes
            var reset = "reset";
            Session["resetToken"] = reset;
            //update the status of the event to completed
            updateEvent("Completed");
            //close popup window when complete
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myCloseScript", "window.close()", true);
        }

        protected void btnMarkRejected_Click(object sender, EventArgs e)
        {
            checkLogin();
            //create session variable to indicate a change was made on child page
            var reset = "reset";
            Session["resetToken"] = reset;
            //update the status of the event to rejected
            updateEvent("Rejected");
            //close popup window when complete
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myCloseScript", "window.close()", true);
        }

        protected void btnMarkPending_Click(object sender, EventArgs e)
        {
            checkLogin();
            //create session variable to indicate a change was made on child page
            var reset = "reset";
            Session["resetToken"] = reset;
            //obtain info for current event
            //create object to hold updated event
            //change updated event status to pending
            try
            {
                int selectedEventID = (int)Session["selectedEventID"];
                BusinessObjects.ExEvent selectedExEvent = _myExEventManager.FetchEvent(selectedEventID);
                BusinessObjects.ExEvent updatedExEvent = new BusinessObjects.ExEvent();
                updatedExEvent.eventID = selectedExEvent.eventID;
                updatedExEvent.eventDate = Convert.ToDateTime(txtEventDate.Text);
                updatedExEvent.employeeID = selectedExEvent.employeeID;
                updatedExEvent.submissionDate = selectedExEvent.submissionDate;
                updatedExEvent.activityName = listActivity.SelectedItem.Value;
                updatedExEvent.startTime = startHour.SelectedItem.Value + ":" + startMinute.SelectedItem.Value + " " + startAMPM.SelectedItem.Value;
                updatedExEvent.endTime = endHour.SelectedItem.Value + ":" + endMinute.SelectedItem.Value + " " + endAMPM.SelectedItem.Value;
                updatedExEvent.statusName = "Pending";
                updatedExEvent.activityNote = txtActivityNote.Text;

                //remove rejected event
                try
                {
                    _myExEventManager.DeleteExEvent(selectedExEvent);
                }
                catch (Exception)
                {
                    throw;
                }

                //resubmit updated event as a new event
                try
                {
                    _myExEventManager.AddExEvent(updatedExEvent);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myCloseScript", "window.close()", true);
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Exception resubmitted as a new pending event.');", true);
        }

        protected void btnPurgeEvent_Click(object sender, EventArgs e)
        {
            checkLogin();
            //create session variable to indicate a change was made on child page
            var reset = "reset";
            Session["resetToken"] = reset;
            //capture selected event id from session variable
            try
            {
                int selectedEventID = (int)Session["selectedEventID"];
                //retrieve event from db
                BusinessObjects.ExEvent selectedExEvent = _myExEventManager.FetchEvent(selectedEventID);
                try
                {
                    _myExEventManager.DeleteExEvent(selectedExEvent);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myCloseScript", "window.close()", true);
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Event deleted successfully');", true);
        }

        public void updateEvent(string status)
        {
            //create employee object from stored sessoin variable
            BusinessObjects.Employee loggedInEmployee = (BusinessObjects.Employee)Session["loggedInUser"];
            try
            {
                int selectedEventID = (int)Session["selectedEventID"];
                
                BusinessObjects.ExEvent originalExEvent = _myExEventManager.FetchEvent(selectedEventID);
                BusinessObjects.ExEvent updatedExEvent = new BusinessObjects.ExEvent();

                updatedExEvent.eventID = originalExEvent.eventID;
                updatedExEvent.eventDate = Convert.ToDateTime(txtEventDate.Text);
                updatedExEvent.employeeID = originalExEvent.employeeID;
                updatedExEvent.submissionDate = originalExEvent.submissionDate;
                updatedExEvent.activityName = listActivity.SelectedItem.Value;
                updatedExEvent.startTime = startHour.SelectedItem.Value + ":" + startMinute.SelectedItem.Value + " " + startAMPM.SelectedItem.Value;
                updatedExEvent.endTime = endHour.SelectedItem.Value + ":" + endMinute.SelectedItem.Value + " " + endAMPM.SelectedItem.Value;
                updatedExEvent.statusName = status;
                updatedExEvent.activityNote = txtActivityNote.Text;

                //if the new event status is completed or rejected, capture and update info for completed on and completed by
                if (status != "Pending")
                {
                    updatedExEvent.completedBy = loggedInEmployee.FirstName + " " + loggedInEmployee.LastName;
                    updatedExEvent.completedDate = DateTime.Now;
                    _myExEventManager.UpdateExEvent(originalExEvent, updatedExEvent);
                }
                else
                {
                    updatedExEvent.completedDate = originalExEvent.completedDate;
                    updatedExEvent.completedBy = originalExEvent.completedBy;
                    _myExEventManager.UpdateExEventToPending(originalExEvent, updatedExEvent);
                }
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseWindow", "window.close()", true);
                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Submission Updated Successfully');", true);
                //Response.End();   
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            checkLogin();
            //create session variable to indicate a change was made on child page
            var reset = "reset";
            Session["resetToken"] = reset;
            //update event with data in form fields
            try
            {
                int selectedEventID = (int)Session["selectedEventID"];
                BusinessObjects.ExEvent selectedExEvent = _myExEventManager.FetchEvent(selectedEventID);
                updateEvent(selectedExEvent.statusName);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OnClose", "window.close()", true);
                //Response.Close();
            }
            catch (Exception)
            {
                throw;
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