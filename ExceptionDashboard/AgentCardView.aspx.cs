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
            int consultantID = Convert.ToInt32(Request.QueryString["agent"]);
            ConsultationCard currentConsultantCard = _myConsultationCardManager.FindCard(consultantID);

            Employee currentConsultant = _myEmployeeManager.FindSingleEmployee(consultantID);

            if(!IsPostBack)
            {
                lblConsultantName.Text += currentConsultant.FirstName + " " + currentConsultant.LastName;
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
            if(currentConsultantCard.Communication == 0)
            {
                communicationButton.Text = "Add";
            }
            else
            {
                communicationButton.Text = "Remove";
            }

            TableCell communicationButtonCell = new TableCell();
            communicationButtonCell.Controls.Add(communicationButton);
            tRow1b.Controls.Add(communicationButtonCell);



            Button competitorsButton = new Button();
            if (currentConsultantCard.Competitors == 0)
            {
                competitorsButton.Text = "Add";
            }
            else
            {
                competitorsButton.Text = "Remove";
            }

            TableCell competitorsButtonCell = new TableCell();
            competitorsButtonCell.Controls.Add(competitorsButton);
            tRow1b.Controls.Add(competitorsButtonCell);


            Button goalsButton = new Button();
            if (currentConsultantCard.Goals == 0)
            {
                goalsButton.Text = "Add";
            }
            else
            {
                goalsButton.Text = "Remove";
            }

            TableCell goalsButtonCell = new TableCell();
            goalsButtonCell.Controls.Add(goalsButton);
            tRow1b.Controls.Add(goalsButtonCell);


            Button growthButton = new Button();
            if (currentConsultantCard.Growth == 0)
            {
                growthButton.Text = "Add";
            }
            else
            {
                growthButton.Text = "Remove";
            }

            TableCell growthButtonCell = new TableCell();
            growthButtonCell.Controls.Add(growthButton);
            tRow1b.Controls.Add(growthButtonCell);


            Button headcountButton = new Button();
            if (currentConsultantCard.Headcount == 0)
            {
                headcountButton.Text = "Add";
            }
            else
            {
                headcountButton.Text = "Remove";
            }

            TableCell headcountButtonCell = new TableCell();
            headcountButtonCell.Controls.Add(headcountButton);
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
            if (currentConsultantCard.Market == 0)
            {
                marketButton.Text = "Add";
            }
            else
            {
                marketButton.Text = "Remove";
            }

            TableCell marketButtonCell = new TableCell();
            marketButtonCell.Controls.Add(marketButton);
            tRow2b.Controls.Add(marketButtonCell);



            Button rapportButton = new Button();
            if (currentConsultantCard.Rapport == 0)
            {
                rapportButton.Text = "Add";
            }
            else
            {
                rapportButton.Text = "Remove";
            }

            TableCell rapportButtonCell = new TableCell();
            rapportButtonCell.Controls.Add(rapportButton);
            tRow2b.Controls.Add(rapportButtonCell);


            Button recommendedButton = new Button();
            if (currentConsultantCard.Recommended == 0)
            {
                recommendedButton.Text = "Add";
            }
            else
            {
                recommendedButton.Text = "Remove";
            }

            TableCell recommendedButtonCell = new TableCell();
            recommendedButtonCell.Controls.Add(recommendedButton);
            tRow2b.Controls.Add(recommendedButtonCell);


            Button termButton = new Button();
            if (currentConsultantCard.Term == 0)
            {
                termButton.Text = "Add";
            }
            else
            {
                termButton.Text = "Remove";
            }

            TableCell termButtonCell = new TableCell();
            termButtonCell.Controls.Add(termButton);
            tRow2b.Controls.Add(termButtonCell);


            Button websiteButton = new Button();
            if (currentConsultantCard.Website == 0)
            {
                websiteButton.Text = "Add";
            }
            else
            {
                websiteButton.Text = "Remove";
            }

            TableCell websiteButtonCell = new TableCell();
            websiteButtonCell.Controls.Add(websiteButton);
            tRow2b.Controls.Add(websiteButtonCell);
        }
    }
}