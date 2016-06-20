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
using System.Web.UI.HtmlControls;

namespace ExceptionDashboard
{
    public partial class AgentCardView : System.Web.UI.Page
    {
        private ExEventManager _myExEventManager = new ExEventManager();
        private EmployeeManager _myEmployeeManager = new EmployeeManager();
        private ConsultationCardManager _myConsultationCardManager = new ConsultationCardManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            Employee loggedInEmployee = (Employee)Session["loggedInUser"]; 
            int consultantID = Convert.ToInt32(Request.QueryString["agent"]);
            ConsultationCard currentConsultantCard = _myConsultationCardManager.FindCard(consultantID);

            Employee currentConsultant = _myEmployeeManager.FindSingleEmployee(consultantID);

            if(!IsPostBack)
            {
                lblConsultantName.Text += (currentConsultant.FirstName + " " + currentConsultant.LastName);
            }
            
            TableRow tRow1 = new TableRow();
            agentCardViewTable.Rows.Add(tRow1);

            TableCell communication = new TableCell();
            communication.Text = string.Format("<img src=./images/full-communication" + currentConsultantCard.Communication + ".png />");
            tRow1.Controls.Add(communication);

            TableCell competitors = new TableCell();
            competitors.Text = string.Format("<img src=./images/full-competitors" + currentConsultantCard.Competitors + ".png />");
            tRow1.Controls.Add(competitors);

            TableCell goals = new TableCell();
            goals.Text = string.Format("<img src=./images/full-goals" + currentConsultantCard.Goals + ".png />");
            tRow1.Controls.Add(goals);

            TableCell growth = new TableCell();
            growth.Text = string.Format("<img src=./images/full-growth" + currentConsultantCard.Growth + ".png />");
            tRow1.Controls.Add(growth);

            TableCell headcount = new TableCell();
            headcount.Text = string.Format("<img src=./images/full-headcount" + currentConsultantCard.Headcount + ".png />");
            tRow1.Controls.Add(headcount);

            TableRow tRow1b = new TableRow();
            agentCardViewTable.Rows.Add(tRow1b);



            Button communicationButton = new Button();
            communicationButton.Click += new EventHandler(communicationButton_Click);
            string btnText = checkPropertyValue(currentConsultantCard.Communication, loggedInEmployee.RoleName);
            communicationButton.Text = btnText;
            TableCell communicationButtonCell = new TableCell();
            communicationButtonCell.Controls.Add(communicationButton);

            Button communicationRequestButton = new Button();
            communicationRequestButton.Click += new EventHandler(communicationButton_Click);
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Communication == 1 || currentConsultantCard.Communication == 2) 
                {
                    communicationButtonCell.Controls.Remove(communicationButton);
                }
            }
            else if (loggedInEmployee.RoleName == "Manager" || loggedInEmployee.RoleName == "Supervisor" || loggedInEmployee.RoleName == "Lead")
            {
                if (currentConsultantCard.Communication == 2)
                {
                    communicationRequestButton.Text = "Reject";
                    communicationButtonCell.Controls.Add(communicationRequestButton);
                }
            }
            

            tRow1b.Controls.Add(communicationButtonCell);



            Button competitorsButton = new Button();
            competitorsButton.Click += new EventHandler(competitorsButton_Click);
            btnText = checkPropertyValue(currentConsultantCard.Competitors, loggedInEmployee.RoleName);
            competitorsButton.Text = btnText;
            TableCell competitorsButtonCell = new TableCell();
            competitorsButtonCell.Controls.Add(competitorsButton);

            Button competitorsRequestButton = new Button();
            competitorsRequestButton.Click += new EventHandler(competitorsButton_Click);
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Competitors == 1 || currentConsultantCard.Competitors == 2) 
                {
                    competitorsButtonCell.Controls.Remove(competitorsButton);
                }
            }
            else if (loggedInEmployee.RoleName == "Manager" || loggedInEmployee.RoleName == "Supervisor" || loggedInEmployee.RoleName == "Lead")
            {
                if (currentConsultantCard.Competitors == 2)
                {
                    competitorsRequestButton.Text = "Reject";
                    competitorsButtonCell.Controls.Add(competitorsRequestButton);
                }
            }
            

            tRow1b.Controls.Add(competitorsButtonCell);

            Button goalsButton = new Button();
            goalsButton.Click += new EventHandler(goalsButton_Click);
            btnText = checkPropertyValue(currentConsultantCard.Goals, loggedInEmployee.RoleName);
            goalsButton.Text = btnText;
            TableCell goalsButtonCell = new TableCell();
            goalsButtonCell.Controls.Add(goalsButton);

            Button goalsRequestButton = new Button();
            goalsRequestButton.Click += new EventHandler(goalsButton_Click);
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Goals == 1 || currentConsultantCard.Goals == 2)
                {
                    goalsButtonCell.Controls.Remove(goalsButton);
                }
            }
            else if (loggedInEmployee.RoleName == "Manager" || loggedInEmployee.RoleName == "Supervisor" || loggedInEmployee.RoleName == "Lead")
            {
                if (currentConsultantCard.Goals == 2)
                {
                    goalsRequestButton.Text = "Reject";
                    goalsButtonCell.Controls.Add(goalsRequestButton);
                }
            }
            

            tRow1b.Controls.Add(goalsButtonCell);


            Button growthButton = new Button();
            growthButton.Click += new EventHandler(growthButton_Click);
            btnText = checkPropertyValue(currentConsultantCard.Growth, loggedInEmployee.RoleName);
            growthButton.Text = btnText;
            TableCell growthButtonCell = new TableCell();
            growthButtonCell.Controls.Add(growthButton);

            Button growthRequestButton = new Button();
            growthRequestButton.Click += new EventHandler(growthButton_Click);
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Growth == 1 || currentConsultantCard.Growth == 2)
                {
                    growthButtonCell.Controls.Remove(growthButton);
                }
            }
            else if (loggedInEmployee.RoleName == "Manager" || loggedInEmployee.RoleName == "Supervisor" || loggedInEmployee.RoleName == "Lead")
            {
                if (currentConsultantCard.Growth == 2)
                {
                    growthRequestButton.Text = "Reject";
                    growthButtonCell.Controls.Add(growthRequestButton);
                }
            }
            

            tRow1b.Controls.Add(growthButtonCell);


            Button headcountButton = new Button();
            headcountButton.Click += new EventHandler(headcountButton_Click);
            btnText = checkPropertyValue(currentConsultantCard.Headcount, loggedInEmployee.RoleName);
            headcountButton.Text = btnText;
            TableCell headcountButtonCell = new TableCell();
            headcountButtonCell.Controls.Add(headcountButton);

            Button headcountRequestButton = new Button();
            headcountRequestButton.Click += new EventHandler(headcountButton_Click);
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Headcount == 1 || currentConsultantCard.Headcount == 2)
                {
                    headcountButtonCell.Controls.Remove(headcountButton);
                }
            }
            else if (loggedInEmployee.RoleName == "Manager" || loggedInEmployee.RoleName == "Supervisor" || loggedInEmployee.RoleName == "Lead")
            {
                if (currentConsultantCard.Headcount == 2)
                {
                    headcountRequestButton.Text = "Reject";
                    headcountButtonCell.Controls.Add(headcountRequestButton);
                }
            }
            

            tRow1b.Controls.Add(headcountButtonCell);



            TableRow tRow2 = new TableRow();
            agentCardViewTable.Rows.Add(tRow2);

            TableCell market = new TableCell();
            market.Text = string.Format("<img src=./images/full-market" + currentConsultantCard.Market + ".png />");
            tRow2.Controls.Add(market);

            TableCell rapport = new TableCell();
            rapport.Text = string.Format("<img src=./images/full-rapport" + currentConsultantCard.Rapport + ".png />");
            tRow2.Controls.Add(rapport);

            TableCell recommended = new TableCell();
            recommended.Text = string.Format("<img src=./images/full-recommended" + currentConsultantCard.Recommended + ".png />");
            tRow2.Controls.Add(recommended);

            TableCell term = new TableCell();
            term.Text = string.Format("<img src=./images/full-term" + currentConsultantCard.Term + ".png />");
            tRow2.Controls.Add(term);

            TableCell website = new TableCell();
            website.Text = string.Format("<img src=./images/full-website" + currentConsultantCard.Website + ".png />");
            tRow2.Controls.Add(website);

            TableRow tRow2b = new TableRow();
            agentCardViewTable.Rows.Add(tRow2b);

            Button marketButton = new Button();
            marketButton.Click += new EventHandler(marketButton_Click);
            btnText = checkPropertyValue(currentConsultantCard.Market, loggedInEmployee.RoleName);
            marketButton.Text = btnText;
            TableCell marketButtonCell = new TableCell();
            marketButtonCell.Controls.Add(marketButton);

            Button marketRequestButton = new Button();
            marketRequestButton.Click += new EventHandler(marketButton_Click);
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Market == 1 || currentConsultantCard.Market == 2) 
                {
                    marketButtonCell.Controls.Remove(marketButton);
                }
            }
            else if (loggedInEmployee.RoleName == "Manager" || loggedInEmployee.RoleName == "Supervisor" || loggedInEmployee.RoleName == "Lead")
            {
                if (currentConsultantCard.Market == 2)
                {
                    marketRequestButton.Text = "Reject";
                    marketButtonCell.Controls.Add(marketRequestButton);
                }
            }


            tRow2b.Controls.Add(marketButtonCell);



            Button rapportButton = new Button();
            rapportButton.Click += new EventHandler(rapportButton_Click);
            btnText = checkPropertyValue(currentConsultantCard.Rapport, loggedInEmployee.RoleName);
            rapportButton.Text = btnText;
            TableCell rapportButtonCell = new TableCell();
            rapportButtonCell.Controls.Add(rapportButton);

            Button rapportRequestButton = new Button();
            rapportRequestButton.Click += new EventHandler(rapportButton_Click);
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Rapport == 1 || currentConsultantCard.Rapport == 2) 
                {
                    rapportButtonCell.Controls.Remove(rapportButton);
                }
            }
            else if (loggedInEmployee.RoleName == "Manager" || loggedInEmployee.RoleName == "Supervisor" || loggedInEmployee.RoleName == "Lead")
            {
                if (currentConsultantCard.Rapport == 2)
                {
                    rapportRequestButton.Text = "Reject";
                    rapportButtonCell.Controls.Add(rapportRequestButton);
                }
            }


            tRow2b.Controls.Add(rapportButtonCell);


            Button recommendedButton = new Button();
            recommendedButton.Click += new EventHandler(recommendedButton_Click);
            btnText = checkPropertyValue(currentConsultantCard.Recommended, loggedInEmployee.RoleName);
            recommendedButton.Text = btnText;
            TableCell recommendedButtonCell = new TableCell();
            recommendedButtonCell.Controls.Add(recommendedButton);

            Button recommendedRequestButton = new Button();
            recommendedRequestButton.Click += new EventHandler(recommendedButton_Click);
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Recommended == 1 || currentConsultantCard.Recommended == 2)
                {
                    recommendedButtonCell.Controls.Remove(recommendedButton);
                }
            }
            else if (loggedInEmployee.RoleName == "Manager" || loggedInEmployee.RoleName == "Supervisor" || loggedInEmployee.RoleName == "Lead")
            {
                if (currentConsultantCard.Recommended == 2)
                {
                    recommendedRequestButton.Text = "Reject";
                    recommendedButtonCell.Controls.Add(recommendedRequestButton);
                }
            }


            tRow2b.Controls.Add(recommendedButtonCell);


            Button termButton = new Button();
            termButton.Click += new EventHandler(termButton_Click);
            btnText = checkPropertyValue(currentConsultantCard.Term, loggedInEmployee.RoleName);
            termButton.Text = btnText;
            TableCell termButtonCell = new TableCell();
            termButtonCell.Controls.Add(termButton);

            Button termRequestButton = new Button();
            termRequestButton.Click += new EventHandler(termButton_Click);
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Term == 1 || currentConsultantCard.Term == 2)
                {
                    termButtonCell.Controls.Remove(termButton);
                }
            }
            else if (loggedInEmployee.RoleName == "Manager" || loggedInEmployee.RoleName == "Supervisor" || loggedInEmployee.RoleName == "Lead")
            {
                if (currentConsultantCard.Term == 2)
                {
                    termRequestButton.Text = "Reject";
                    termButtonCell.Controls.Add(termRequestButton);
                }
            }


            tRow2b.Controls.Add(termButtonCell);


            Button websiteButton = new Button();
            websiteButton.Click += new EventHandler(websiteButton_Click);
            btnText = checkPropertyValue(currentConsultantCard.Website, loggedInEmployee.RoleName);
            websiteButton.Text = btnText;
            TableCell websiteButtonCell = new TableCell();
            websiteButtonCell.Controls.Add(websiteButton);

            Button websiteRequestButton = new Button();
            websiteRequestButton.Click += new EventHandler(websiteButton_Click);
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Website == 1 || currentConsultantCard.Website == 2)
                {
                    websiteButtonCell.Controls.Remove(websiteButton);
                }
            }
            else if (loggedInEmployee.RoleName == "Manager" || loggedInEmployee.RoleName == "Supervisor" || loggedInEmployee.RoleName == "Lead")
            {
                if (currentConsultantCard.Website == 2)
                {
                    websiteRequestButton.Text = "Reject";
                    websiteButtonCell.Controls.Add(websiteRequestButton);
                }
            }


            tRow2b.Controls.Add(websiteButtonCell);
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (loggedInEmployee.EmployeeID != consultantID)
                {
                    tRow1b.Visible = false;
                    tRow2b.Visible = false;
                }
            }
        }

        public string checkPropertyValue(int inProp, string RoleName)
        {
            string returnProp = "";

            if (RoleName == "Manager" || RoleName == "Supervisor" || RoleName == "Lead")
            {
                if (inProp == 0 || inProp == 2)
                {
                    returnProp = "Add";
                }
                else if (inProp == 1)
                {
                    returnProp = "Remove";
                }
            }

            if (RoleName == "Agent")
            {
                if (inProp == 0)
                {
                    returnProp = "Request";
                }
            }

            return returnProp;
        }

        protected void communicationButton_Click(object sender, EventArgs e)
        {
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Communication";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void competitorsButton_Click(object sender, EventArgs e)
        {
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Competitors";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void goalsButton_Click(object sender, EventArgs e)
        {
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Goals";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void growthButton_Click(object sender, EventArgs e)
        {
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Growth";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void headcountButton_Click(object sender, EventArgs e)
        {
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Headcount";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void marketButton_Click(object sender, EventArgs e)
        {
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Market";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void rapportButton_Click(object sender, EventArgs e)
        {
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Rapport";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void recommendedButton_Click(object sender, EventArgs e)
        {
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Recommended";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void termButton_Click(object sender, EventArgs e)
        {
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Term";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void websiteButton_Click(object sender, EventArgs e)
        {
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Website";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void communicationRequestButton_Click(object sender, EventArgs e)
        {
            
        }

        protected void competitorsRequestButton_Click(object sender, EventArgs e)
        {

        }

        protected void goalsRequestButton_Click(object sender, EventArgs e)
        {

        }

        protected void growthRequestButton_Click(object sender, EventArgs e)
        {

        }

        protected void headcountRequestButton_Click(object sender, EventArgs e)
        {

        }

        protected void marketRequestButton_Click(object sender, EventArgs e)
        {

        }

        protected void rapportRequestButton_Click(object sender, EventArgs e)
        {

        }

        protected void recommendedRequestButton_Click(object sender, EventArgs e)
        {

        }

        protected void termRequestButton_Click(object sender, EventArgs e)
        {

        }

        protected void websiteRequestButton_Click(object sender, EventArgs e)
        {

        }


        public void updateCard(string a, string c, int empID)
        {
            ConsultationCard oldCard = _myConsultationCardManager.FindCard(empID);
            ConsultationCard updatedCard = oldCard;
            
            if (a == "Add")
            {
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
                _myConsultationCardManager.UpdateConsultationCard(oldCard, updatedCard);
                Response.Redirect(Request.RawUrl);
            }
            else if (a == "Remove" || a == "Reject")
            {
                switch (c)
                {
                    case "Communication":
                        updatedCard.Communication = 0;
                        break;
                    case "Competitors":
                        updatedCard.Competitors = 0;
                        break;
                    case "Goals":
                        updatedCard.Goals = 0;
                        break;
                    case "Growth":
                        updatedCard.Growth = 0;
                        break;
                    case "Headcount":
                        updatedCard.Headcount = 0;
                        break;
                    case "Market":
                        updatedCard.Market = 0;
                        break;
                    case "Rapport":
                        updatedCard.Rapport = 0;
                        break;
                    case "Recommended":
                        updatedCard.Recommended = 0;
                        break;
                    case "Term":
                        updatedCard.Term = 0;
                        break;
                    case "Website":
                        updatedCard.Website = 0;
                        break;
                }
                _myConsultationCardManager.UpdateConsultationCard(oldCard, updatedCard);
                Response.Redirect(Request.RawUrl);
            }
            else if (a == "Request")
            {
                switch (c)
                {
                    case "Communication":
                        updatedCard.Communication = 2;
                        break;
                    case "Competitors":
                        updatedCard.Competitors = 2;
                        break;
                    case "Goals":
                        updatedCard.Goals = 2;
                        break;
                    case "Growth":
                        updatedCard.Growth = 2;
                        break;
                    case "Headcount":
                        updatedCard.Headcount = 2;
                        break;
                    case "Market":
                        updatedCard.Market = 2;
                        break;
                    case "Rapport":
                        updatedCard.Rapport = 2;
                        break;
                    case "Recommended":
                        updatedCard.Recommended = 2;
                        break;
                    case "Term":
                        updatedCard.Term = 2;
                        break;
                    case "Website":
                        updatedCard.Website = 2;
                        break;
                }
                _myConsultationCardManager.UpdateConsultationCard(oldCard, updatedCard);
                Response.Redirect(Request.RawUrl);
            }
        }

    }
}