using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using BusinessLogic;
using System.Web.Util;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;

namespace ExceptionDashboard
{
    public partial class AddCard : System.Web.UI.Page
    {
        ConsultationCardManager _myConsultationCardManager = new ConsultationCardManager();
        EmployeeManager _myEmployeeManager = new EmployeeManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblNote.Visible = false;

            int consultantID = Convert.ToInt32(Request.QueryString["agent"]);
            string cardName = Convert.ToString(Request.QueryString["cardName"]);
            string requested = Convert.ToString(Request.QueryString["requested"]);
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            Employee currentEmployee = _myEmployeeManager.FindSingleEmployee(consultantID);
            List<CardMethod> currentMethods = _myConsultationCardManager.SelectCardMethods();
            if (!IsPostBack)
            {
                ddlMethod.DataTextField = "methodName";
                ddlMethod.DataValueField = "methodName";
                ddlMethod.DataSource = currentMethods;
                ddlMethod.DataBind();
                lblCongrats.Text += currentEmployee.FirstName + "!";
                lblAwarded.Text = "You have just been awarded a new consultation card by " + loggedInEmployee.FirstName + " " + loggedInEmployee.LastName + ".";
                if(requested == "true")
                {
                    ddlMethod.SelectedValue = "Requested";
                    ddlMethod.Enabled = false;
                }
            }
            Image myImg = new Image();
            myImg.ImageUrl = "/images/full-" + cardName + "1.png";
            myImg.Visible = true;
            imgCard.Controls.Add(myImg);
        }

        protected void SendMail(string fname, string card, string email)
        {
            MailMessage mailMessage = new MailMessage();
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            mailMessage.IsBodyHtml = true;
            mailMessage.From = new MailAddress("donotreply@dev.domainmason.com");
            mailMessage.To.Add(email);
            mailMessage.Subject = "You have been awarded a new consultation card!";
            string imageCard = card.ToLower();
            string fileName = "full-" + imageCard + "1.png";
            string path = "~/images/full-" + imageCard + "1.png";
            string sImage = System.Web.HttpContext.Current.Server.MapPath(path);

            //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
            //     "<html><body>Congratulations " + fname + "!" + "<br/><br/>"
            //     + "You have just been awarded a consultation card by " + loggedInEmployee.FirstName + " " + loggedInEmployee.LastName + ".<br /><br />" + @"<img src='cid:" + inline.ContentId + @"'/>" + " />"
            //     + "<br /><br />"
            //     + "This puts you one step closer to earning an entry into the monthly custom swag drawing.<br /><br />Keep up the great work!</body></html>", null, MediaTypeNames.Text.Html);
            mailMessage.AlternateViews.Add(getEmbeddedImage(sImage, fname, loggedInEmployee));

            //FileStream fileToStream = new FileStream(System.Web.HttpContext.Current.Server.MapPath(path), FileMode.Open, FileAccess.Read);
            //Attachment att = new Attachment(fileToStream, fileName, MediaTypeNames.Image.Jpeg);
            //att.ContentDisposition.Inline = true;
            //mailMessage.Attachments.Add(att);

            SmtpClient smtpClient = new SmtpClient("relay-hosting.secureserver.net", 25);
            smtpClient.Send(mailMessage);
        }
        protected void SendEntryMail(string fname, string email)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress("donotreply@dev.domainmason.com");
            mailMessage.To.Add(email);
            mailMessage.Subject = "You have just completed a full consultation card sheet!";

            mailMessage.Body = "Hey " + fname + "!" + "<br/><br/>"
                 + "Congratulations!<br/>"
                 + "You have just completed a full consultation card sheet, and earned an entry into the monthly custom swag drawing.<br /><br />"
                 + "Keep up the great work!";
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("relay-hosting.secureserver.net", 25);
            smtpClient.Send(mailMessage);
        }

        public void checkLogin()
        {
            if (Session["loggedInUser"] == null)
            {
                Response.Redirect("AgentView.aspx");
            }
        }

        private AlternateView getEmbeddedImage(String filePath, string fname, Employee loggedInEmployee)
        {
            LinkedResource inline = new LinkedResource(filePath);
            inline.ContentId = Guid.NewGuid().ToString();
            //string htmlBody = @"<img src='cid:" + inline.ContentId + @"'/>";
            string htmlBody = "<html><body>Congratulations " + fname + "!" + "<br/><br/>"
                 + "You have just been awarded a consultation card by " + loggedInEmployee.FirstName + " " + loggedInEmployee.LastName + ".<br /><br />" + @"<img src='cid:" + inline.ContentId + @"'/>"
                 + "<br /><br />"
                 + "This puts you one step closer to earning an entry into the monthly custom swag drawing.<br /><br />Keep up the great work!</body></html>";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(inline);
            return alternateView;
        }

        protected void emailNote_CheckedChanged(object sender, EventArgs e)
        {
            if (emailNote.Checked == true)
            {
                lblNote.Visible = true;
            }
            else
            {
                lblNote.Visible = false;
            }
        }

        protected void txtNote_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnAddCard_Click(object sender, EventArgs e)
        {
            int consultantID = Convert.ToInt32(Request.QueryString["agent"]);
            string cardName = Convert.ToString(Request.QueryString["cardName"]);
            updateCard(cardName, consultantID);

            //updatedCard.RequestDate = "1/1/1900 12:00:00";
            //_myConsultationCardManager.UpdateConsultationCard(oldCard, updatedCard);
            //Employee currentEmp = _myEmployeeManager.FindSingleEmployee(updatedCard.EmployeeID);
            //SendMail(currentEmp.FirstName, c, currentEmp.EmailAddress);
            //if (updatedCard.Communication == 1 && updatedCard.Competitors == 1 && updatedCard.Goals == 1 && updatedCard.Growth == 1 && updatedCard.Headcount == 1 && updatedCard.Market == 1 && updatedCard.Rapport == 1 && updatedCard.Recommended == 1 && updatedCard.Term == 1 && updatedCard.Website == 1)
            //{
            //   int consultantID = Convert.ToInt32(Request.QueryString["agent"]);
            //    addEntry(consultantID);
            //    SendEntryMail(currentEmp.FirstName, currentEmp.EmailAddress);
        }

        public void updateCard(string c, int empID)
        {
            ConsultationCard oldCard = _myConsultationCardManager.FindCard(empID);
            ConsultationCard updatedCard = oldCard;

            switch (c)
            {
                case "Communication":
                    updatedCard.Communication = 1;
                    break;
                case "Competitors":
                    updatedCard.Competitors = 1;
                    break;
                case "Goals":
                    updatedCard.Goals = 1;
                    break;
                case "Growth":
                    updatedCard.Growth = 1;
                    break;
                case "Headcount":
                    updatedCard.Headcount = 1;
                    break;
                case "Market":
                    updatedCard.Market = 1;
                    break;
                case "Rapport":
                    updatedCard.Rapport = 1;
                    break;
                case "Recommended":
                    updatedCard.Recommended = 1;
                    break;
                case "Term":
                    updatedCard.Term = 1;
                    break;
                case "Website":
                    updatedCard.Website = 1;
                    break;
            }

            //updatedCard.RequestDate = "1/1/1900 12:00:00";
            _myConsultationCardManager.UpdateConsultationCard(oldCard, updatedCard);
            Employee currentEmp = _myEmployeeManager.FindSingleEmployee(updatedCard.EmployeeID);
            SendMail(currentEmp.FirstName, c, currentEmp.EmailAddress);
            if (updatedCard.Communication == 1 && updatedCard.Competitors == 1 && updatedCard.Goals == 1 && updatedCard.Growth == 1 && updatedCard.Headcount == 1 && updatedCard.Market == 1 && updatedCard.Rapport == 1 && updatedCard.Recommended == 1 && updatedCard.Term == 1 && updatedCard.Website == 1)
            {
                int consultantID = Convert.ToInt32(Request.QueryString["agent"]);
                addEntry(consultantID);
                SendEntryMail(currentEmp.FirstName, currentEmp.EmailAddress);
            }
            Response.Redirect(Request.RawUrl);
        }

        public void addEntry(int empID)
        {
            ConsultationCard currentCard = _myConsultationCardManager.FindCard(empID);
            ConsultationCard newCard = currentCard;

            newCard.TotalEntries += 1;
            newCard.LifetimeEntries += 1;
            newCard.Communication = 0;
            newCard.Competitors = 0;
            newCard.Goals = 0;
            newCard.Growth = 0;
            newCard.Headcount = 0;
            newCard.Market = 0;
            newCard.Rapport = 0;
            newCard.Recommended = 0;
            newCard.Term = 0;
            newCard.Website = 0;

            _myConsultationCardManager.UpdateConsultationCard(currentCard, newCard);
        }
    }
}