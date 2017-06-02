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
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Globalization;

namespace ExceptionDashboard
{
    public partial class AgentCardView : System.Web.UI.Page
    {
        private ExEventManager _myExEventManager = new ExEventManager();
        private EmployeeManager _myEmployeeManager = new EmployeeManager();
        private ConsultationCardManager _myConsultationCardManager = new ConsultationCardManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedInUser"] != null)
            {
                Employee loggedInEmployee = Session["loggedInUser"] as Employee;

                int consultantID = Convert.ToInt32(Request.QueryString["agent"]);
                ConsultationCard currentConsultantCard = _myConsultationCardManager.FindCard(consultantID);


                Employee currentConsultant = _myEmployeeManager.FindSingleEmployee(consultantID);

                if (!IsPostBack)
                {
                    lblConsultantName.Text += (currentConsultant.FirstName + " " + currentConsultant.LastName);
                }

                TableRow tRow1 = new TableRow();
                agentCardViewTable.Rows.Add(tRow1);

                TableCell communication = new TableCell();
                communication.Text = string.Format("<img src=./images/full-communication" + currentConsultantCard.Communication + ".png title='How are they communicating in the office?&#10;How do they communicate with customers?&#10;How do customers communicate with them?'/>");
                tRow1.Controls.Add(communication);

                TableCell competitors = new TableCell();
                competitors.Text = string.Format("<img src=./images/full-competitors" + currentConsultantCard.Competitors + ".png title='Who is the local competition?&#10;Who is the national competition?&#10;What are their competitors doing right?&#10;What makes them better than the competitors?'/>");
                tRow1.Controls.Add(competitors);

                TableCell goals = new TableCell();
                goals.Text = string.Format("<img src=./images/full-goals" + currentConsultantCard.Goals + ".png title='What is their goal for this year?&#10;What is their 5 year goal?&#10;What are they trying to do with the business currently?'/>");
                tRow1.Controls.Add(goals);

                TableCell growth = new TableCell();
                growth.Text = string.Format("<img src=./images/full-growth" + currentConsultantCard.Growth + ".png title='How big would they like the company to be?&#10;Are they currently hiring?&#10;How quickly are they growing?'/>");
                tRow1.Controls.Add(growth);

                TableCell headcount = new TableCell();
                headcount.Text = string.Format("<img src=./images/full-headcount" + currentConsultantCard.Headcount + ".png title='How many employee are their?&#10;How many customers do they work with daily?&#10;How many customers would they like to have?'/>");
                tRow1.Controls.Add(headcount);

                TableRow tRow1c = new TableRow();
                agentCardViewTable.Rows.Add(tRow1c);

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

                TableCell communicationRequestDate = new TableCell();
                if (currentConsultantCard.Communication == 2)
                {
                    communicationRequestDate.Text = currentConsultantCard.CommunicationRequestDate;
                }
                else
                {
                    communicationRequestDate.Text = "";
                }
                tRow1c.Controls.Add(communicationRequestDate);

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

                TableCell competitorsRequestDate = new TableCell();
                if (currentConsultantCard.Competitors == 2)
                {
                    competitorsRequestDate.Text = currentConsultantCard.CompetitorsRequestDate;
                }
                else
                {
                    competitorsRequestDate.Text = "";
                }
                tRow1c.Controls.Add(competitorsRequestDate);


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

                TableCell goalsRequestDate = new TableCell();
                if (currentConsultantCard.Goals == 2)
                {
                    goalsRequestDate.Text = currentConsultantCard.GoalsRequestDate;
                }
                else
                {
                    goalsRequestDate.Text = "";
                }
                tRow1c.Controls.Add(goalsRequestDate);


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

                TableCell growthRequestDate = new TableCell();
                if (currentConsultantCard.Growth == 2)
                {
                    growthRequestDate.Text = currentConsultantCard.GrowthRequestDate;
                }
                else
                {
                    growthRequestDate.Text = "";
                }
                tRow1c.Controls.Add(growthRequestDate);


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

                TableCell headcountRequestDate = new TableCell();
                if (currentConsultantCard.Headcount == 2)
                {
                    headcountRequestDate.Text = currentConsultantCard.HeadcountRequestDate;
                }
                else
                {
                    headcountRequestDate.Text = "";
                }
                tRow1c.Controls.Add(headcountRequestDate);


                TableRow tRow2 = new TableRow();
                agentCardViewTable.Rows.Add(tRow2);

                TableCell market = new TableCell();
                market.Text = string.Format("<img src=./images/full-market" + currentConsultantCard.Market + ".png title='What is the current customer base?&#10;How do they stay in touch with customers?&#10;How do they reach potential new customers?'/>");
                tRow2.Controls.Add(market);

                TableCell rapport = new TableCell();
                rapport.Text = string.Format("<img src=./images/full-rapport" + currentConsultantCard.Rapport + ".png title='How did they get started in their business?&#10;What is their favorite thing about what they do?&#10;Other than [issue that initiated call], how are they doing today?'/>");
                tRow2.Controls.Add(rapport);

                TableCell recommended = new TableCell();
                recommended.Text = string.Format("<img src=./images/full-recommended" + currentConsultantCard.Recommended + ".png title='Based on what you said, I’d recommend…&#10;Because the rest of your account is out [term], I’d recommend doing the same term.&#10;Needs based suggestion'/>");
                tRow2.Controls.Add(recommended);

                TableCell term = new TableCell();
                term.Text = string.Format("<img src=./images/full-term" + currentConsultantCard.Term + ".png title='How long have they been in business?&#10;How long are they planning to be in business?&#10;What are they doing to ensure they stick around?'/>");
                tRow2.Controls.Add(term);

                TableCell website = new TableCell();
                website.Text = string.Format("<img src=./images/full-website" + currentConsultantCard.Website + ".png title='Who built it?&#10;When was it last updated?&#10;Who maintains it?&#10;What are their goals for it?'/>");
                tRow2.Controls.Add(website);

                TableRow tRow2c = new TableRow();
                agentCardViewTable.Rows.Add(tRow2c);

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

                TableCell marketRequestDate = new TableCell();
                if (currentConsultantCard.Market == 2)
                {
                    marketRequestDate.Text = currentConsultantCard.MarketRequestDate;
                }
                else
                {
                    marketRequestDate.Text = "";
                }
                tRow2c.Controls.Add(marketRequestDate);


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

                TableCell rapportRequestDate = new TableCell();
                if (currentConsultantCard.Rapport == 2)
                {
                    rapportRequestDate.Text = currentConsultantCard.RapportRequestDate;
                }
                else
                {
                    rapportRequestDate.Text = "";
                }
                tRow2c.Controls.Add(rapportRequestDate);


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

                TableCell recommendedRequestDate = new TableCell();
                if (currentConsultantCard.Recommended == 2)
                {
                    recommendedRequestDate.Text = currentConsultantCard.RecommendedRequestDate;
                }
                else
                {
                    recommendedRequestDate.Text = "";
                }
                tRow2c.Controls.Add(recommendedRequestDate);


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

                TableCell termRequestDate = new TableCell();
                if (currentConsultantCard.Term == 2)
                {
                    termRequestDate.Text = currentConsultantCard.TermRequestDate;
                }
                else
                {
                    termRequestDate.Text = "";
                }
                tRow2c.Controls.Add(termRequestDate);


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

                TableCell websiteRequestDate = new TableCell();
                if (currentConsultantCard.Website == 2)
                {
                    websiteRequestDate.Text = currentConsultantCard.WebsiteRequestDate;
                }
                else
                {
                    websiteRequestDate.Text = "";
                }
                tRow2c.Controls.Add(websiteRequestDate);

                if (loggedInEmployee.RoleName == "Agent")
                {
                    btnClear.Visible = false;
                    if (loggedInEmployee.EmployeeID != consultantID)
                    {
                        tRow1b.Visible = false;
                        tRow2b.Visible = false;
                        tRow1c.Visible = false;
                        tRow2c.Visible = false;
                    }
                }

                ConsultationSheet currentSheet = _myConsultationCardManager.SelectCurrentConsultationSheet(consultantID);
                int currentMonth = DateTime.Now.Month;
                string[] currentSheetMonthSplit = currentSheet.createdDate.Split('/');
                int currentSheetMonth = Convert.ToInt32(currentSheetMonthSplit[0]);


                if (currentMonth != currentSheetMonth)
                {
                    if (loggedInEmployee.RoleName == "Agent")
                    {
                        lblCardExpired.Text = "This sheet was started last month. Please ask your supervisor to submit your earned entries for last month, and reset your sheet.";
                    }
                    lblCardExpired.Visible = true;
                    communicationButton.Visible = false;
                    communicationRequestButton.Visible = false;

                    competitorsButton.Visible = false;
                    competitorsRequestButton.Visible = false;

                    goalsButton.Visible = false;
                    goalsRequestButton.Visible = false;

                    growthButton.Visible = false;
                    growthRequestButton.Visible = false;

                    headcountButton.Visible = false;
                    headcountRequestButton.Visible = false;

                    marketButton.Visible = false;
                    marketRequestButton.Visible = false;

                    rapportButton.Visible = false;
                    rapportRequestButton.Visible = false;

                    recommendedButton.Visible = false;
                    recommendedRequestButton.Visible = false;

                    termButton.Visible = false;
                    termRequestButton.Visible = false;

                    websiteButton.Visible = false;
                    websiteRequestButton.Visible = false;
                }
            }
            else
            {
                Response.Redirect("AgentView.aspx");
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
            checkLogin();
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Communication";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void competitorsButton_Click(object sender, EventArgs e)
        {
            checkLogin();
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Competitors";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void goalsButton_Click(object sender, EventArgs e)
        {
            checkLogin();
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Goals";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void growthButton_Click(object sender, EventArgs e)
        {
            checkLogin();
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Growth";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void headcountButton_Click(object sender, EventArgs e)
        {
            checkLogin();
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Headcount";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void marketButton_Click(object sender, EventArgs e)
        {
            checkLogin();
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Market";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void rapportButton_Click(object sender, EventArgs e)
        {
            checkLogin();
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Rapport";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void recommendedButton_Click(object sender, EventArgs e)
        {
            checkLogin();
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Recommended";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void termButton_Click(object sender, EventArgs e)
        {
            checkLogin();
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            int empID = Convert.ToInt32(Request.QueryString["agent"]);
            string card = "Term";
            string s = (sender as Button).Text;
            updateCard(s, card, empID);
        }

        protected void websiteButton_Click(object sender, EventArgs e)
        {
            checkLogin();
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
                lblCard.Text = c;
                lblAgent.Text = empID.ToString();
                switch (c)
                {
                    case "Communication":
                        if (oldCard.CommunicationRequestDate != "1/1/1900 12:00:00 PM")
                        {
                            lblRequested.Text = "true";
                        }
                        else
                        {
                            lblRequested.Text = "false";
                        }
                        break;
                    case "Competitors":
                        if (oldCard.CompetitorsRequestDate != "1/1/1900 12:00:00 PM")
                        {
                            lblRequested.Text = "true";
                        }
                        else
                        {
                            lblRequested.Text = "false";
                        }
                        break;
                    case "Goals":
                        if (oldCard.GoalsRequestDate != "1/1/1900 12:00:00 PM")
                        {
                            lblRequested.Text = "true";
                        }
                        else
                        {
                            lblRequested.Text = "false";
                        }
                        break;
                    case "Growth":
                        if (oldCard.GrowthRequestDate != "1/1/1900 12:00:00 PM")
                        {
                            lblRequested.Text = "true";
                        }
                        else
                        {
                            lblRequested.Text = "false";
                        }
                        break;
                    case "Headcount":
                        if (oldCard.HeadcountRequestDate != "1/1/1900 12:00:00 PM")
                        {
                            lblRequested.Text = "true";
                        }
                        else
                        {
                            lblRequested.Text = "false";
                        }
                        break;
                    case "Market":
                        if (oldCard.MarketRequestDate != "1/1/1900 12:00:00 PM")
                        {
                            lblRequested.Text = "true";
                        }
                        else
                        {
                            lblRequested.Text = "false";
                        }
                        break;
                    case "Rapport":
                        if (oldCard.RapportRequestDate != "1/1/1900 12:00:00 PM")
                        {
                            lblRequested.Text = "true";
                        }
                        else
                        {
                            lblRequested.Text = "false";
                        }
                        break;
                    case "Recommended":
                        if (oldCard.RecommendedRequestDate != "1/1/1900 12:00:00 PM")
                        {
                            lblRequested.Text = "true";
                        }
                        else
                        {
                            lblRequested.Text = "false";
                        }
                        break;
                    case "Term":
                        if (oldCard.TermRequestDate != "1/1/1900 12:00:00 PM")
                        {
                            lblRequested.Text = "true";
                        }
                        else
                        {
                            lblRequested.Text = "false";
                        }
                        break;
                    case "Website":
                        if (oldCard.WebsiteRequestDate != "1/1/1900 12:00:00 PM")
                        {
                            lblRequested.Text = "true";
                        }
                        else
                        {
                            lblRequested.Text = "false";
                        }
                        break;
                }
                //updatedCard.RequestDate = "1/1/1900 12:00:00";
                //_myConsultationCardManager.UpdateConsultationCard(oldCard, updatedCard);
                //Employee currentEmp = _myEmployeeManager.FindSingleEmployee(updatedCard.EmployeeID);
                //SendMail(currentEmp.FirstName, c, currentEmp.EmailAddress);
                //if (updatedCard.Communication == 1 && updatedCard.Competitors == 1 && updatedCard.Goals == 1 && updatedCard.Growth == 1 && updatedCard.Headcount == 1 && updatedCard.Market == 1 && updatedCard.Rapport == 1 && updatedCard.Recommended == 1 && updatedCard.Term == 1 && updatedCard.Website == 1)
                //{
                //   int consultantID = Convert.ToInt32(Request.QueryString["agent"]);
                //    addEntry(consultantID);
                //    SendEntryMail(currentEmp.FirstName, currentEmp.EmailAddress);

                //Response.Redirect(Request.RawUrl);
                ScriptManager.RegisterStartupScript(this, GetType(), "showModalPopUp", "showModalPopUp();", true);
            }
            else if (a == "Remove" || a == "Reject")
            {
                switch (c)
                {
                    case "Communication":
                        updatedCard.Communication = 0;
                        updatedCard.CommunicationRequestDate = "1/1/1900 12:00:00";
                        break;
                    case "Competitors":
                        updatedCard.Competitors = 0;
                        updatedCard.CompetitorsRequestDate = "1/1/1900 12:00:00";
                        break;
                    case "Goals":
                        updatedCard.Goals = 0;
                        updatedCard.GoalsRequestDate = "1/1/1900 12:00:00";
                        break;
                    case "Growth":
                        updatedCard.Growth = 0;
                        updatedCard.GrowthRequestDate = "1/1/1900 12:00:00";
                        break;
                    case "Headcount":
                        updatedCard.Headcount = 0;
                        updatedCard.HeadcountRequestDate = "1/1/1900 12:00:00";
                        break;
                    case "Market":
                        updatedCard.Market = 0;
                        updatedCard.MarketRequestDate = "1/1/1900 12:00:00";
                        break;
                    case "Rapport":
                        updatedCard.Rapport = 0;
                        updatedCard.RapportRequestDate = "1/1/1900 12:00:00";
                        break;
                    case "Recommended":
                        updatedCard.Recommended = 0;
                        updatedCard.RecommendedRequestDate = "1/1/1900 12:00:00";
                        break;
                    case "Term":
                        updatedCard.Term = 0;
                        updatedCard.TermRequestDate = "1/1/1900 12:00:00";
                        break;
                    case "Website":
                        updatedCard.Website = 0;
                        updatedCard.WebsiteRequestDate = "1/1/1900 12:00:00";
                        break;
                }
                _myConsultationCardManager.UpdateConsultationCard(oldCard, updatedCard);
                if(a == "Remove")
                {
                    ConsultationSheet currentSheet = _myConsultationCardManager.SelectCurrentConsultationSheet(empID);
                    _myConsultationCardManager.RemoveCard(currentSheet.sheetID, c);
                }
                Response.Redirect(Request.RawUrl);
            }
            else if (a == "Request")
            {
                switch (c)
                {
                    case "Communication":
                        updatedCard.Communication = 2;
                        updatedCard.CommunicationRequestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        break;
                    case "Competitors":
                        updatedCard.Competitors = 2;
                        updatedCard.CompetitorsRequestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        break;
                    case "Goals":
                        updatedCard.Goals = 2;
                        updatedCard.GoalsRequestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        break;
                    case "Growth":
                        updatedCard.Growth = 2;
                        updatedCard.GrowthRequestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        break;
                    case "Headcount":
                        updatedCard.Headcount = 2;
                        updatedCard.HeadcountRequestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        break;
                    case "Market":
                        updatedCard.Market = 2;
                        updatedCard.MarketRequestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        break;
                    case "Rapport":
                        updatedCard.Rapport = 2;
                        updatedCard.RapportRequestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        break;
                    case "Recommended":
                        updatedCard.Recommended = 2;
                        updatedCard.RecommendedRequestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        break;
                    case "Term":
                        updatedCard.Term = 2;
                        updatedCard.TermRequestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        break;
                    case "Website":
                        updatedCard.Website = 2;
                        updatedCard.WebsiteRequestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        break;
                }
                _myConsultationCardManager.UpdateConsultationCard(oldCard, updatedCard);
                Response.Redirect(Request.RawUrl);
            }
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

        protected void SendMail(string fname, string card, string email)
        {
            MailMessage mailMessage = new MailMessage();
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            mailMessage.IsBodyHtml = true;
            mailMessage.From = new MailAddress("donotreply@nimbusguild.com");
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

            mailMessage.From = new MailAddress("donotreply@nimbusguild.com");
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            checkLogin();
            int consultantID = Convert.ToInt32(Request.QueryString["agent"]);
            ConsultationCard oldCard = _myConsultationCardManager.FindCard(consultantID);
            ConsultationCard currentCard = oldCard;
            currentCard.EmployeeID = consultantID;
            currentCard.Communication = 0;
            currentCard.Competitors = 0;
            currentCard.Goals = 0;
            currentCard.Growth = 0;
            currentCard.Headcount = 0;
            currentCard.Market = 0;
            currentCard.Rapport = 0;
            currentCard.Recommended = 0;
            currentCard.Term = 0;
            currentCard.Website = 0;
            currentCard.TotalEntries = 0;
            _myConsultationCardManager.UpdateConsultationCard(oldCard, currentCard);

            ConsultationSheet currentSheet = _myConsultationCardManager.SelectCurrentConsultationSheet(consultantID);
            ConsultationSheet updatedSheet = currentSheet;
            updatedSheet.completedDate = "1/1/2000 12:00:00";
            _myConsultationCardManager.CloseCardSheet(currentSheet, updatedSheet);
            _myConsultationCardManager.CreateNewCardSheet(consultantID);

            Response.Redirect(Request.RawUrl);
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
    }
}