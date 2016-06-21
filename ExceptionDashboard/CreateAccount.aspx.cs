using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using BusinessObjects;
using System.Globalization;

namespace ExceptionDashboard
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        private ExEventManager _myExEventManager = new ExEventManager();
        private EmployeeManager _myEmployeeManager = new EmployeeManager();
        private ConsultationCardManager _myConsultationCardManager = new ConsultationCardManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            //disable login form for new account creation page
            System.Web.UI.HtmlControls.HtmlGenericControl currdiv = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("pagelogin");
            System.Web.UI.HtmlControls.HtmlGenericControl welcomediv = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("welcome");
            currdiv.Style.Add("display", "none");
            welcomediv.Style.Add("display", "none");
            //retrieve current list of departments and roles and bind to drop down lists
            if (!IsPostBack)
            {
                try
                {
                    List<BusinessObjects.Department> currentDeptList = _myEmployeeManager.FetchDepartmentList();
                    listDepartment.DataTextField = "departmentName";
                    listDepartment.DataValueField = "departmentName";
                    listDepartment.DataSource = currentDeptList;
                    listDepartment.DataBind();

                    List<Role> currentRoles = _myEmployeeManager.FetchRoleList();
                    listRole.DataTextField = "roleName";
                    listRole.DataValueField = "roleName";
                    listRole.DataSource = currentRoles;
                    listRole.DataBind();
                }
                catch (Exception)
                {
                    throw;
                }

                try
                {
                    //retrieve current list of all leads and supervisors to allow user to select direct leader
                    List<BusinessObjects.Employee> currentLeadList = _myEmployeeManager.FetchEmployeesbyRole("Lead");
                    List<BusinessObjects.Employee> currentSupList = _myEmployeeManager.FetchEmployeesbyRole("Supervisor");
                    
                    foreach (Employee sup in currentSupList)
                    {
                        currentLeadList.Add(sup);
                    }
                    //bind list of leaders to drop down list
                    List<BusinessObjects.Employee> sortedLeaderList = currentLeadList.OrderBy(o => o.LastName).ToList();
                    listLeader.DataTextField = "fullName";
                    listLeader.DataValueField = "employeeID";
                    listLeader.DataSource = sortedLeaderList;
                    listLeader.DataBind();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        protected void submitNewUser_Click(object sender, EventArgs e)
        {
            if (listRole.SelectedValue == "Agent")
            {
                createEmployee();
            }
            else if (listRole.SelectedValue == "Supervisor" || listRole.SelectedValue == "Lead" || listRole.SelectedValue == "Manager")
            {
                createSupEmployee();
            }            
        }

        private void createSupEmployee()
        {
            //check for empty form fields
            if (txtFirstName.Text != "" && txtLastName.Text != "" && txtUsername.Text != "" && txtEmailAddress.Text != "")
            {
                //capture form data
                string roleName = listRole.SelectedValue;
                string departmentName = listDepartment.SelectedValue;
                string firstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFirstName.Text);
                string lastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtLastName.Text);
                string username = txtUsername.Text;
                string password1 = txtPassword.Text;
                string password2 = txtPassword2.Text;
                string emailAddress = txtEmailAddress.Text;
                //verify passwords match
                if (password1.Equals(password2))
                {
                    //check to make sure password meets min reqs
                    if (_myEmployeeManager.isValidPassword(password1) == true)
                    {
                        //convert password string to md5
                        string pwdHash = _myEmployeeManager.GeneratePasswordHash(password1);
                        try
                        {
                            try
                            {
                                //check to see if username is currently taken
                                BusinessObjects.Employee checkUsername = _myEmployeeManager.FindEmployee(username);
                                if (checkUsername.Username != null)
                                {
                                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Username already exists');", true);
                                }
                                Response.End();

                            }
                            catch (Exception)
                            {

                            }
                            //create new employee object using form data
                            //since role is supervisor, direct leader is not needed
                            //to eliminate need for managers to create accounts for sups to report to, defaulting supervisor direct leader to 'Hosting' 'Support'
                            BusinessObjects.Employee newEmployee = new BusinessObjects.Employee();
                            newEmployee.RoleName = roleName;
                            newEmployee.DepartmentName = departmentName;
                            newEmployee.SupervisorFirstName = "Hosting";
                            newEmployee.SupervisorLastName = "Support";
                            newEmployee.FirstName = firstName;
                            newEmployee.LastName = lastName;
                            newEmployee.Username = username;
                            newEmployee.Password = pwdHash;
                            newEmployee.EmailAddress = emailAddress;

                            try
                            {
                                //add new employee to db
                                _myEmployeeManager.AddNewEmployee(newEmployee);
                                try
                                {
                                    //attempt to auto log in new user account
                                    Employee loginEmployee = _myEmployeeManager.FindEmployee(username);
                                    Session["loggedInUser"] = loginEmployee;
                                    //set flag for agentview page to identify a new user was created
                                    Session["newUser"] = "yes";
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
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('User Created Successfully.');", true);
                        Employee checkRedirect = (Employee)Session["loggedInUser"];

                        if (checkRedirect.RoleName == "Manager")
                        {
                            Response.Redirect("ManagerView.aspx");
                        }
                        else
                        {
                            Response.Redirect("AgentView.aspx");
                        }
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Password must contain at least one uppercase, one lowercase, one number, and one special character.');", true);
                    }
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Passwords do not match.');", true);
                }
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('All fields required!');", true);
            }
        }

        private void createEmployee()
        {
            //check for empty form fields
            if (txtFirstName.Text != "" && txtLastName.Text != "" && txtUsername.Text != "" && txtEmailAddress.Text != "")
            {
                string roleName = listRole.SelectedValue;
                string departmentName = listDepartment.SelectedValue;
                int supervisorID = Convert.ToInt32(listLeader.SelectedValue);
                string firstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFirstName.Text);
                string lastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtLastName.Text);
                string username = txtUsername.Text;
                string password1 = txtPassword.Text;
                string password2 = txtPassword2.Text;
                string emailAddress = txtEmailAddress.Text;
                //make sure passwords match
                if (password1.Equals(password2))
                {
                    if (_myEmployeeManager.isValidPassword(password1) == true)
                    {
                        string pwdHash = _myEmployeeManager.GeneratePasswordHash(password1);
                        try
                        {
                            //retrieve info for reporting supervisor/lead
                            BusinessObjects.Employee currentSup = _myEmployeeManager.FindSingleEmployee(supervisorID);
                            string supervisorFirstName = currentSup.FirstName;
                            string supervisorLastName = currentSup.LastName;

                            try
                            {
                                //check to see if username is taken
                                BusinessObjects.Employee checkUsername = _myEmployeeManager.FindEmployee(username);
                                if (checkUsername.Username != null)
                                {
                                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Username already exists');", true);
                                }
                                Response.End();

                            }
                            catch (Exception)
                            {

                            }
                            //create new employee
                            BusinessObjects.Employee newEmployee = new BusinessObjects.Employee();
                            newEmployee.RoleName = roleName;
                            newEmployee.DepartmentName = departmentName;
                            newEmployee.SupervisorFirstName = supervisorFirstName;
                            newEmployee.SupervisorLastName = supervisorLastName;
                            newEmployee.FirstName = firstName;
                            newEmployee.LastName = lastName;
                            newEmployee.Username = username;
                            newEmployee.Password = pwdHash;
                            newEmployee.EmailAddress = emailAddress;

                            try
                            {
                                //add new employee to db
                                _myEmployeeManager.AddNewEmployee(newEmployee);
                                Employee newEmp = _myEmployeeManager.FindEmployee(newEmployee.Username);
                                    ConsultationCard newCard = new ConsultationCard();
                                    newCard.EmployeeID = newEmp.EmployeeID;
                                    newCard.CommunicationRequestDate = "1/1/1900 12:00:00";
                                    newCard.CompetitorsRequestDate = "1/1/1900 12:00:00";
                                    newCard.GoalsRequestDate = "1/1/1900 12:00:00";
                                    newCard.GrowthRequestDate = "1/1/1900 12:00:00";
                                    newCard.HeadcountRequestDate = "1/1/1900 12:00:00";
                                    newCard.MarketRequestDate = "1/1/1900 12:00:00";
                                    newCard.RapportRequestDate = "1/1/1900 12:00:00";
                                    newCard.RecommendedRequestDate = "1/1/1900 12:00:00";
                                    newCard.TermRequestDate = "1/1/1900 12:00:00";
                                    newCard.WebsiteRequestDate = "1/1/1900 12:00:00";
                                    _myConsultationCardManager.CreateConsultationCard(newCard);

                                try
                                {
                                    //attempt to auto log in new user account
                                    Employee loginEmployee = _myEmployeeManager.FindEmployee(username);
                                    Session["loggedInUser"] = loginEmployee;
                                    //set flag for agentview page to identify a new user was created.
                                    Session["newUser"] = "yes";
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
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('User Created Successfully.');", true);

                        //Employee redirectCheck = Employee(Session["loggedInUser"]);

                        Employee checkRedirect = (Employee)Session["loggedInUser"];

                        if (checkRedirect.RoleName == "Manager")
                        {
                            Response.Redirect("ManagerView.aspx");
                        }
                        else
                        {
                            Response.Redirect("AgentView.aspx");
                        }
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Password must contain at least one uppercase, one lowercase, one number, and one special character.');", true);
                    }
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Passwords do not match.');", true);
                }
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('All fields required!');", true);
            }
        }

        protected void listRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            //check to see if supervisor was selected as role, remove fields for direct leader selection, as sup accounts are defaulted to 'hosting support' for direct leader
            if (listRole.SelectedValue == "Supervisor" || listRole.SelectedValue == "Lead" || listRole.SelectedValue == "Manager")
            {
                lblListLeader.Visible = false;
                listLeader.Visible = false;
            }
            //if selected role is lead or agent, re-enable direct leader selection
            else if (listRole.SelectedValue == "Agent")
            {
                lblListLeader.Visible = true;
                listLeader.Visible = true;
            }
        }
    }
}