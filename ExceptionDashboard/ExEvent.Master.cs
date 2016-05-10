//Exception Dashboard
//Created by Mason A.
//Updated 5/8/16

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
    public partial class ExEvent : System.Web.UI.MasterPage
    {
        private ExEventManager _myExEventManager = new ExEventManager();
        private EmployeeManager _myEmployeeManager = new EmployeeManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            //check to see if user is logged in
            if (Session["loggedInUser"] != null)
            {
                //if logged in, disable login fields
                //update welcome message to reflect user's name
                //change log in button text to logout
                try
                {
                    Employee loggedInEmployee = (Employee)Session["loggedInUser"];
                    lblWelcome.Text = "Welcome " + loggedInEmployee.FirstName + " " + loggedInEmployee.LastName;
                    lblUsername.Visible = false;
                    lblPassword.Visible = false;
                    txtUsername.Visible = false;
                    txtPassword.Visible = false;
                    btnLogin.Text = "Logout";
                    lblNewUser.Visible = false;
                    lblReset.Visible = false;

                    //check user's role, disable request exception for supervisor/lead & enable manage employees button
                    //if user's role is agent, disable manage employees button and add request new exception button
                    var menu = Page.Master.FindControl("Menu1") as Menu;

                    if (loggedInEmployee.RoleName == "Manager")
                    {
                        if (menu.FindItem("btnRequestNewException") != null)
                        {
                            menu.Items.Remove(menu.FindItem("btnRequestNewException"));
                        }
                        if (menu.FindItem("btnViewExceptions") != null)
                        {
                            menu.Items.Remove(menu.FindItem("btnViewExceptions"));
                        }
                    }
                    else if (loggedInEmployee.RoleName == "Supervisor" || loggedInEmployee.RoleName == "Lead")
                    {
                        if (menu.FindItem("btnRequestNewException") != null)
                        {
                            menu.Items.Remove(menu.FindItem("btnRequestNewException"));
                        }
                        if (menu.FindItem("btnManagerView") != null)
                        {
                            menu.Items.Remove(menu.FindItem("btnManagerView"));
                        }
                    }
                    else if (loggedInEmployee.RoleName == "Agent")
                    {
                        if (menu.FindItem("btnManageEmployees") != null)
                        {
                            menu.Items.Remove(menu.FindItem("btnManageEmployees"));
                        }
                        if (menu.FindItem("btnManagerView") != null)
                        {
                            menu.Items.Remove(menu.FindItem("btnManagerView"));
                        }
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("AgentView.aspx");
                }

            }
            else
            {
                //if user is not logged in, disable nav menu and enable login fields
                Menu1.Visible = false;
                lblWelcome.Text = "Welcome, please log in.";
                lblUsername.Visible = true;
                lblPassword.Visible = true;
                txtUsername.Visible = true;
                txtPassword.Visible = true;
                btnLogin.Text = "Login";
                
                //lblNewUser.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //check to see if button was clicked while user was logged in or logged out
            if (Session["loggedInUser"] == null)
            {
                //attempt to log in user with creds provided
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                try
                {
                    Employee loginEmployee = _myEmployeeManager.FindEmployee(username);

                    if (_myEmployeeManager.VerifyHashPassword(password, loginEmployee.Password))
                    {
                        Session["loggedInUser"] = loginEmployee;
                        if (loginEmployee.RoleName == "Manager") 
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
                        lblNewUser.Text = "Login Failed!";
                        lblNewUser.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception)
                {
                    lblNewUser.Text = "Login Failed!";
                    lblNewUser.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblWelcome.Text = "Welcome, please log in.";
                lblUsername.Visible = true;
                lblPassword.Visible = true;
                txtUsername.Visible = true;
                txtPassword.Visible = true;
                txtUsername.Text = "";
                txtPassword.Text = "";
                Session.Abandon();
                btnLogin.Text = "Login";
                lblNewUser.Visible = true;
                lblReset.Visible = true;
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showSubmitPopUp", "showSubmitPopUp();", true);
        }
    }
}