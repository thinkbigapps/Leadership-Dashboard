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
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Communication == 0) 
                {
                    communicationRequestButton.Text = "Request";
                    communicationButtonCell.Controls.Add(communicationRequestButton);
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
            btnText = checkPropertyValue(currentConsultantCard.Competitors, loggedInEmployee.RoleName);
            competitorsButton.Text = btnText;
            TableCell competitorsButtonCell = new TableCell();
            competitorsButtonCell.Controls.Add(competitorsButton);

            Button competitorsRequestButton = new Button();
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Competitors == 0) 
                {
                    competitorsRequestButton.Text = "Request";
                    competitorsButtonCell.Controls.Add(competitorsRequestButton);
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
            btnText = checkPropertyValue(currentConsultantCard.Goals, loggedInEmployee.RoleName);
            goalsButton.Text = btnText;
            TableCell goalsButtonCell = new TableCell();
            goalsButtonCell.Controls.Add(goalsButton);

            Button goalsRequestButton = new Button();
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Goals == 0)
                {
                    goalsRequestButton.Text = "Request";
                    goalsButtonCell.Controls.Add(goalsRequestButton);
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
            btnText = checkPropertyValue(currentConsultantCard.Growth, loggedInEmployee.RoleName);
            growthButton.Text = btnText;
            TableCell growthButtonCell = new TableCell();
            growthButtonCell.Controls.Add(growthButton);

            Button growthRequestButton = new Button();
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Growth == 0)
                {
                    growthRequestButton.Text = "Request";
                    growthButtonCell.Controls.Add(growthRequestButton);
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
            btnText = checkPropertyValue(currentConsultantCard.Headcount, loggedInEmployee.RoleName);
            headcountButton.Text = btnText;
            TableCell headcountButtonCell = new TableCell();
            headcountButtonCell.Controls.Add(headcountButton);

            Button headcountRequestButton = new Button();
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Headcount == 0)
                {
                    headcountRequestButton.Text = "Request";
                    headcountButtonCell.Controls.Add(headcountRequestButton);
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
            btnText = checkPropertyValue(currentConsultantCard.Market, loggedInEmployee.RoleName);
            marketButton.Text = btnText;
            TableCell marketButtonCell = new TableCell();
            marketButtonCell.Controls.Add(marketButton);

            Button marketRequestButton = new Button();
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Market == 0) 
                {
                    marketRequestButton.Text = "Request";
                    marketButtonCell.Controls.Add(marketRequestButton);
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
            btnText = checkPropertyValue(currentConsultantCard.Rapport, loggedInEmployee.RoleName);
            rapportButton.Text = btnText;
            TableCell rapportButtonCell = new TableCell();
            rapportButtonCell.Controls.Add(rapportButton);

            Button rapportRequestButton = new Button();
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Rapport == 0) 
                {
                    rapportRequestButton.Text = "Request";
                    rapportButtonCell.Controls.Add(rapportRequestButton);
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
            btnText = checkPropertyValue(currentConsultantCard.Recommended, loggedInEmployee.RoleName);
            recommendedButton.Text = btnText;
            TableCell recommendedButtonCell = new TableCell();
            recommendedButtonCell.Controls.Add(recommendedButton);

            Button recommendedRequestButton = new Button();
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Recommended == 0)
                {
                    recommendedRequestButton.Text = "Request";
                    recommendedButtonCell.Controls.Add(recommendedRequestButton);
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
            btnText = checkPropertyValue(currentConsultantCard.Term, loggedInEmployee.RoleName);
            termButton.Text = btnText;
            TableCell termButtonCell = new TableCell();
            termButtonCell.Controls.Add(termButton);

            Button termRequestButton = new Button();
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Term == 0)
                {
                    termRequestButton.Text = "Request";
                    termButtonCell.Controls.Add(termRequestButton);
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
            btnText = checkPropertyValue(currentConsultantCard.Website, loggedInEmployee.RoleName);
            websiteButton.Text = btnText;
            TableCell websiteButtonCell = new TableCell();
            websiteButtonCell.Controls.Add(websiteButton);

            Button websiteRequestButton = new Button();
            if (loggedInEmployee.RoleName == "Agent")
            {
                if (currentConsultantCard.Website == 0)
                {
                    websiteRequestButton.Text = "Request";
                    websiteButtonCell.Controls.Add(websiteRequestButton);
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
            else if (a == "Remove")
            {

            }
            else if (a == "Request")
            {

            }
        }

    }
}