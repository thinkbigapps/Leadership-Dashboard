using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using BusinessLogic;
using BusinessObjects;
using System.Security.Cryptography;

namespace ExceptionDashboard
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        private ExEventManager _myExEventManager = new ExEventManager();
        private EmployeeManager _myEmployeeManager = new EmployeeManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            //hide form fields on page load
            trPassword.Visible = false;
            trConfirmPass.Visible = false;
            lblResetSubmitted.Visible = false;
            submitConfirmReset.Visible = false;
            
            //check to see if the page load is from the initial reset password link, or the hashed link sent via email
            if (Request.QueryString["resetid"] != null)
            {
                //capture hash value to compare to db value
                string resetID = Request.QueryString["resetid"].Substring(0, 20);
                try
                {
                    //retrieve employee with resetID
                    //retrieve exp date for password reset
                    Employee resetEmployee = _myEmployeeManager.FindEmployeeByResetID(resetID);
                    DateTime passExpire = Convert.ToDateTime(resetEmployee.NewPassExpire);
                    //check to see if password reset link is expired
                    if (DateTime.Compare(DateTime.Now, passExpire) < 0)
                    {
                        //re-enable form fields to enter new password
                        trEmail.Visible = false;
                        trUsername.Visible = true;
                        trPassword.Visible = true;
                        trConfirmPass.Visible = true;
                        submitResetPassword.Visible = false;
                        submitConfirmReset.Visible = true;
                    }
                    else
                    {
                        tbResetPassword.Visible = false;
                        lblResetSubmitted.Text = "The password reset token has expired.  Please request a new password reset.";
                        lblResetSubmitted.Visible = true;
                        tbResetPassword.Visible = true;
                    }
                }
                catch (Exception)
                {
                    lblResetSubmitted.Text = "The password reset token is either invalid or has expired.  Please request a new password reset.";
                    lblResetSubmitted.Visible = true;
                }

            }
        }

        protected void submitResetPassword_Click(object sender, EventArgs e)
        {
            tbResetPassword.Visible = false;
            lblResetSubmitted.Visible = true;

            string username = txtUsername.Text;

            try
            {
                //retrieve emmployee account info
                BusinessObjects.Employee origEmployee = _myEmployeeManager.FindEmployee(username);
                BusinessObjects.Employee checkUsername = _myEmployeeManager.FindEmployee(username);
                if (checkUsername.Username != null)
                {
                    //confirm that email address matches address entered into form
                    if (checkUsername.EmailAddress == txtEmailAddress.Text)
                    {
                        //generate ranndom hash for password reset
                        string resetLinkHash = createResetLinkHash(username);
                        //generate new expiration date for reset request
                        DateTime passExpire = DateTime.Now.AddDays(3);

                        checkUsername.NewPassExpire = passExpire;
                        checkUsername.NewPassID = resetLinkHash;

                        try
                        {
                            //update employee with new reset hash and exp date
                            _myEmployeeManager.UpdateEmployee(origEmployee, checkUsername);
                        }
                        catch (Exception)
                        {
                            
                        }
                        //create reset password with random hash included in query string
                        string resetLink = "http://nimbusguild.com/ResetPassword.aspx?resetid=" + resetLinkHash;
                        //send reset email
                        SendMail(checkUsername.FirstName, checkUsername.LastName, resetLink);
                    }
                }
            }
            catch (Exception) 
            {
                
            }
        }

        protected void SendMail(string fname, string lname, string resetLink)
        {
            MailMessage mailMessage = new MailMessage();
            
            mailMessage.From = new MailAddress("donotreply@nimbusguild.com");
            mailMessage.To.Add(txtEmailAddress.Text);
            mailMessage.Subject = "Exception Dashboard Password Reset Request";

            mailMessage.Body = "Dear " + fname + " " + lname + ",<br/><br/>"
                 + "A password reset for the C3 Exception Dashboard has been requested for user account " + txtUsername.Text + ".<br/><br/>"
                 + "If you have requested this password reset, follow this link:<br/><a href='" + resetLink + "'>" + resetLink + "</a><br/><br/>"
                 + "If you did not request a password reset, please ignore this email.<br/><br/>Thank you.";
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("relay-hosting.secureserver.net", 25);
            smtpClient.Send(mailMessage);
        }

        protected string createResetLinkHash(string username)
        {
            var bytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            string resetLinkHash = BitConverter.ToString(bytes).Replace("-", "").ToLower();

            return resetLinkHash;
        }

        protected void submitConfirmReset_Click(object sender, EventArgs e)
        {
            string resetRequestUsername = txtUsername.Text;
            string password1 = txtPassword.Text;
            string password2 = txtPassword2.Text;
            //confirm username field is not blank
            if (txtUsername.Text != "")
            {
                //confirm passwords match
                if (password1.Equals(password2))
                {
                    //confirm password meets min reqs
                    if (_myEmployeeManager.isValidPassword(password1) == true)
                    {
                        //generate md5 hash
                        string pwdHash = _myEmployeeManager.GeneratePasswordHash(password1);
                        //update user with password reset info
                        try
                        {
                            Employee resetRequestUser = _myEmployeeManager.FindEmployee(resetRequestUsername);
                            Employee origRequestUser = _myEmployeeManager.FindEmployee(resetRequestUsername);
                            resetRequestUser.NewPassExpire = Convert.ToDateTime("1/1/1753 12:00:00 AM");
                            //resetRequestUser.NewPassID. = DBNull.Value;
                            resetRequestUser.Password = pwdHash;
                            _myEmployeeManager.ResetEmployeePass(origRequestUser, resetRequestUser);
                            lblResetSubmitted.Text = "Password successfully reset.  Please log in to continue.";
                            lblResetSubmitted.Visible = true;
                            tbResetPassword.Visible = false;
                        }
                        catch (Exception)
                        {
                            
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
        }
    }
}