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
    public partial class ConsultationView : System.Web.UI.Page
    {
        private ExEventManager _myExEventManager = new ExEventManager();
        private EmployeeManager _myEmployeeManager = new EmployeeManager();
        private ConsultationCardManager _myConsultationCardManager = new ConsultationCardManager();

        List<Employee> _employeeList = new List<Employee>();

        protected void Page_Load(object sender, EventArgs e)
        {
            

            //Check to see if user is logged in
            if (Session["loggedInUser"] != null)
            {
                try
                {
                    //Create session variable to store logged in user's employee object
                    Employee loggedInEmployee = (Employee)Session["loggedInUser"];

                    //Build page based on user attributes
                    populateLoggedIn(loggedInEmployee.RoleName, loggedInEmployee.DepartmentName);
                }
                catch (Exception)
                {
                    //Response.Redirect("AgentView.aspx");
                }
            }

            //Hide form fields if user is not logged in
            else
            {
                btnViewReport.Visible = false;
                listTopLevel.Items.Clear();
                listDepartment.Items.Clear();
                listRepresentative.Items.Clear();
                searchTable.Visible = false;
            }
            if (!IsPostBack)
            {
                btnViewReport_Click(sender, null);
            }
        }

        //populate fields based on user login
        private void populateLoggedIn(string role, string departmentID)
        {
            Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            List<Employee> currentAgent = new List<Employee>();
            currentAgent.Add(loggedInEmployee);
            if (!IsPostBack)
            {
                if ((loggedInEmployee.RoleName == "Supervisor") || (loggedInEmployee.RoleName == "Lead"))
                {
                    //populate top level list with sup/lead/agent
                    List<Role> currentRoles = _myEmployeeManager.FetchRoleList();
                    listTopLevel.DataTextField = "roleName";
                    listTopLevel.DataValueField = "roleName";
                    listTopLevel.DataSource = currentRoles;
                    listTopLevel.DataBind();

                    //populate department list with all departments
                    List<Department> currentDepartments = _myEmployeeManager.FetchDepartmentList();
                    listDepartment.DataTextField = "departmentName";
                    listDepartment.DataValueField = "departmentName";
                    listDepartment.DataSource = currentDepartments;
                    listDepartment.DataBind();

                    //populate sup list will all employees that have role of sup in employee's department
                    List<Employee> currentSups = _myEmployeeManager.FindEmployeesToList(loggedInEmployee.RoleName, loggedInEmployee.DepartmentName).OrderBy(o => o.FullName).ToList();

                    listRepresentative.DataTextField = "FullName";
                    listRepresentative.DataValueField = "employeeID";
                    listRepresentative.DataSource = currentSups;
                    listRepresentative.DataBind();
                }
                else if (loggedInEmployee.RoleName == "Manager")
                {
                    //populate top level list with sup/lead/agent
                    List<Role> currentRoles = _myEmployeeManager.FetchRoleList();
                    listTopLevel.DataTextField = "roleName";
                    listTopLevel.DataValueField = "roleName";
                    listTopLevel.DataSource = currentRoles;
                    listTopLevel.DataBind();

                    //populate department list with all departments
                    List<Department> currentDepartments = _myEmployeeManager.FetchDepartmentList();
                    listDepartment.DataTextField = "departmentName";
                    listDepartment.DataValueField = "departmentName";
                    listDepartment.DataSource = currentDepartments;
                    listDepartment.DataBind();

                    //populate sup list will all employees that have role of sup in employee's department
                    List<Employee> currentSups = _myEmployeeManager.FindEmployeesToList("Supervisor", loggedInEmployee.DepartmentName);

                    listRepresentative.DataTextField = "FullName";
                    listRepresentative.DataValueField = "employeeID";
                    listRepresentative.DataSource = currentSups;
                    listRepresentative.DataBind();

                    listTopLevel.SelectedValue = "Supervisor";
                    listDepartment.SelectedValue = loggedInEmployee.DepartmentName;
                }

                else if (loggedInEmployee.RoleName == "Agent")
                {
                    Employee sup = _myEmployeeManager.FindSingleEmployeeByName(loggedInEmployee.SupervisorFirstName, loggedInEmployee.SupervisorLastName);
                    //populate top level list with sup/lead/agent
                    List<Role> currentRoles = _myEmployeeManager.FetchRoleList();
                    listTopLevel.DataTextField = "roleName";
                    listTopLevel.DataValueField = "roleName";
                    listTopLevel.DataSource = currentRoles;
                    listTopLevel.DataBind();

                    //populate department list with all departments
                    List<Department> currentDepartments = _myEmployeeManager.FetchDepartmentList();
                    listDepartment.DataTextField = "departmentName";
                    listDepartment.DataValueField = "departmentName";
                    listDepartment.DataSource = currentDepartments;
                    listDepartment.DataBind();

                    //populate sup list will all employees that have role of sup in employee's department
                    List<Employee> currentSups = _myEmployeeManager.FindEmployeesToList(sup.RoleName, sup.DepartmentName).OrderBy(o => o.FullName).ToList();

                    listRepresentative.DataTextField = "FullName";
                    listRepresentative.DataValueField = "employeeID";
                    listRepresentative.DataSource = currentSups;
                    listRepresentative.DataBind();

                    
                    listTopLevel.SelectedValue = sup.RoleName;
                    listDepartment.SelectedValue = sup.DepartmentName;
                    listRepresentative.SelectedValue = sup.EmployeeID.ToString();
                    
                }

                //verify page load is not from submit/edit pages
                if (loggedInEmployee.RoleName != "Agent")
                {

                    if (Session["resetToken"] == null)
                    {
                        //select previously selected top level to bind employee drop down with list of employees with that role
                        if (Session["selectedTopLevel"] != null)
                        {
                            listTopLevel.SelectedValue = Session["selectedTopLevel"].ToString();
                        }
                        if (Session["selectedDepartment"] != null)
                        {
                            listDepartment.SelectedValue = Session["selectedDepartment"].ToString();
                        }
                        else
                        {
                            listTopLevel.SelectedValue = loggedInEmployee.RoleName;
                            listRepresentative.SelectedValue = loggedInEmployee.EmployeeID.ToString();
                        }

                    }
                    //if selectedTopLevel session var is set, select that top level from drop down, otherwise page is from submit/edit page - grab top level from query string
                    else
                    {
                        if (Session["selectedTopLevel"] != null)
                        {
                            listTopLevel.SelectedValue = Session["selectedTopLevel"].ToString();
                        }
                        if (Session["selectedDepartment"] != null)
                        {
                            listDepartment.SelectedValue = Session["selectedDepartment"].ToString();
                        }
                        else
                        {
                            listTopLevel.SelectedValue = Request.QueryString["topLevel"].Replace("\"", "");
                        }
                    }
                    listDepartment.SelectedValue = loggedInEmployee.DepartmentName;
                }
            }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                Employee loggedInEmployee = (Employee)Session["loggedInUser"];
            }
            catch (Exception)
            {
                Response.Redirect("AgentView.aspx");
            }

            if (listTopLevel.SelectedValue == "Supervisor" || listTopLevel.SelectedValue == "Lead")
            {
                string fullSupName = listRepresentative.SelectedItem.Text;
                string[] splitSupName = fullSupName.Split(',');
                for (int i = 0; i < splitSupName.Length; i++)
                {
                    splitSupName[i] = splitSupName[i].Trim();
                }
                string supLastName = splitSupName[0].ToString();
                string supFirstName = splitSupName[1].ToString();

                _employeeList = _myEmployeeManager.FindLeadReports(supFirstName, supLastName);
                List<ConsultationCard> empList = new List<ConsultationCard>();

                if (_employeeList.Count() != 0)
                {
                    Session["currentEmployeeList"] = _employeeList;
                    Session["SortDirection"] = "ASC";
                    for (int x = 0; x < _employeeList.Count; x++)
                    {
                        ConsultationCard currentCard = _myConsultationCardManager.FindCard(_employeeList[x].EmployeeID);
                        empList.Add(currentCard);
                    }
                    List<ConsultationCard> SortedList = empList.OrderBy(o => o.NumEarned).Reverse().ToList();
                    for (int x=0; x< SortedList.Count; x++)
                    {
                        
                        TableRow tRow = new TableRow();
                        consultTable.Rows.Add(tRow);

                        TableCell agent = new TableCell();
                        agent.Text = (_myEmployeeManager.FindSingleEmployee(SortedList[x].EmployeeID)).FullName;
                        tRow.Cells.Add(agent);

                        TableCell cards = new TableCell();
                        cards.Text = string.Format(
                              "<img src='./images/communication" + SortedList[x].Communication.ToString() + ".png' title='Ask about TYPE OF COMMUNICATION they use in their business' />" + "&nbsp;"
                            + "<img src='./images/competitors" + SortedList[x].Competitors.ToString() + ".png' title='Ask about COMPETITORS in their market' />" + "&nbsp;"
                            + "<img src='./images/goals" + SortedList[x].Goals.ToString() + ".png' title='Ask about PRESENT AND FUTURE GOALS of their business' />" + "&nbsp;"
                            + "<img src='./images/growth" + SortedList[x].Growth.ToString() + ".png' title='Gauge GROWTH of the business' />" + "&nbsp;"
                            + "<img src='./images/headcount" + SortedList[x].Headcount.ToString() + ".png' title='Gauge HEADCOUNT for their business' />" + "&nbsp;"
                            + "<img src='./images/market" + SortedList[x].Market.ToString() + ".png' title='Gauge HOW THEY MARKET' />" + "&nbsp;"
                            + "<img src='./images/rapport" + SortedList[x].Rapport.ToString() + ".png' title='Ask RAPPORT BUILDING questions' />" + "&nbsp;"
                            + "<img src='./images/recommended" + SortedList[x].Recommended.ToString() + ".png' title='RECOMMEND the right products' />" + "&nbsp;"
                            + "<img src='./images/term" + SortedList[x].Term.ToString() + ".png' title='Gauge TERM of the business' />" + "&nbsp;"
                            + "<img src='./images/website" + SortedList[x].Website.ToString() + ".png' title='ASK ABOUT THEIR WEBSITE for their business' />" + "&nbsp;"
                        );
                        tRow.Cells.Add(cards);

                        TableCell numEarned = new TableCell();

                        int numCards = 0;

                        if (SortedList[x].Communication == 1)
                        {
                            numCards += 1;
                        }
                        if (SortedList[x].Competitors == 1)
                        {
                            numCards += 1;
                        }
                        if (SortedList[x].Goals == 1)
                        {
                            numCards += 1;
                        }
                        if (SortedList[x].Growth == 1)
                        {
                            numCards += 1;
                        }
                        if (SortedList[x].Headcount == 1)
                        {
                            numCards += 1;
                        }
                        if (SortedList[x].Market == 1)
                        {
                            numCards += 1;
                        }
                        if (SortedList[x].Rapport == 1)
                        {
                            numCards += 1;
                        }
                        if (SortedList[x].Recommended == 1)
                        {
                            numCards += 1;
                        }
                        if (SortedList[x].Term == 1)
                        {
                            numCards += 1;
                        }
                        if (SortedList[x].Website == 1)
                        {
                            numCards += 1;
                        }

                        numEarned.Text = numCards + " / 10";
                        
                        tRow.Cells.Add(numEarned);

                        TableCell entries = new TableCell();
                        entries.Text = SortedList[x].TotalEntries.ToString();
                        tRow.Cells.Add(entries);

                        Employee loggedInEmployee = (Employee)Session["loggedInUser"];
                        if (loggedInEmployee.RoleName == "Manager" || loggedInEmployee.RoleName == "Supervisor" || loggedInEmployee.RoleName == "Lead")
                        {
                            Button editCardsButton = new Button();
                            editCardsButton.Text = "Edit Cards";
                            editCardsButton.PostBackUrl = ("./AgentCardView.aspx?agent=" + SortedList[x].EmployeeID);
                            TableCell editCards = new TableCell();
                            editCards.Controls.Add(editCardsButton);
                            tRow.Controls.Add(editCards);
                        }
                        else if (loggedInEmployee.RoleName == "Agent")
                        {
                            Button viewCardsButton = new Button();
                            viewCardsButton.Text = "View Cards";
                            viewCardsButton.PostBackUrl = ("./AgentCardView.aspx?agent=" + SortedList[x].EmployeeID);
                            TableCell viewCards = new TableCell();
                            viewCards.Controls.Add(viewCardsButton);
                            tRow.Controls.Add(viewCards);
                        }
                    }
                
                }
            }
            //if event list is empty, hide gridview and display 'no records' message
            else if (listTopLevel.SelectedValue == "Agent")
            {
                int representative = Convert.ToInt32(listRepresentative.SelectedItem.Value);
                Employee employee = _myEmployeeManager.FindSingleEmployee(representative);
                ConsultationCard currentCard = _myConsultationCardManager.FindCard(employee.EmployeeID);
                TableRow tRow = new TableRow();
                consultTable.Rows.Add(tRow);

                TableCell agent = new TableCell();
                agent.Text = employee.FullName;
                tRow.Cells.Add(agent);

                TableCell cards = new TableCell();
                cards.Text = string.Format(
                      "<img src='./images/communication" + currentCard.Communication.ToString() + ".png' title='Ask about TYPE OF COMMUNICATION they use in their business' />" + "&nbsp;"
                    + "<img src='./images/competitors" + currentCard.Competitors.ToString() + ".png' title='Ask about COMPETITORS in their market' />" + "&nbsp;"
                    + "<img src='./images/goals" + currentCard.Goals.ToString() + ".png' title='Ask about PRESENT AND FUTURE GOALS of their business' />" + "&nbsp;"
                    + "<img src='./images/growth" + currentCard.Growth.ToString() + ".png' title='Gauge GROWTH of the business' />" + "&nbsp;"
                    + "<img src='./images/headcount" + currentCard.Headcount.ToString() + ".png' title='Gauge HEADCOUNT for their business' />" + "&nbsp;"
                    + "<img src='./images/market" + currentCard.Market.ToString() + ".png' title='Gauge HOW THEY MARKET' />" + "&nbsp;"
                    + "<img src='./images/rapport" + currentCard.Rapport.ToString() + ".png' title='Ask RAPPORT BUILDING questions' />" + "&nbsp;"
                    + "<img src='./images/recommended" + currentCard.Recommended.ToString() + ".png' title='RECOMMEND the right products' />" + "&nbsp;"
                    + "<img src='./images/term" + currentCard.Term.ToString() + ".png' title='Gauge TERM of the business' />" + "&nbsp;"
                    + "<img src='./images/website" + currentCard.Website.ToString() + ".png' title='ASK ABOUT THEIR WEBSITE for their business' />" + "&nbsp;"
                );
                tRow.Cells.Add(cards);

                TableCell numEarned = new TableCell();

                int numCards = 0;

                if (currentCard.Communication == 1)
                {
                    numCards += 1;
                }
                if (currentCard.Competitors == 1)
                {
                    numCards += 1;
                }
                if (currentCard.Goals == 1)
                {
                    numCards += 1;
                }
                if (currentCard.Growth == 1)
                {
                    numCards += 1;
                }
                if (currentCard.Headcount == 1)
                {
                    numCards += 1;
                }
                if (currentCard.Market == 1)
                {
                    numCards += 1;
                }
                if (currentCard.Rapport == 1)
                {
                    numCards += 1;
                }
                if (currentCard.Recommended == 1)
                {
                    numCards += 1;
                }
                if (currentCard.Term == 1)
                {
                    numCards += 1;
                }
                if (currentCard.Website == 1)
                {
                    numCards += 1;
                }

                numEarned.Text = numCards + " / 10";
                tRow.Cells.Add(numEarned);

                TableCell entries = new TableCell();
                entries.Text = currentCard.TotalEntries.ToString();
                tRow.Cells.Add(entries);

                Employee loggedInEmployee = (Employee)Session["loggedInUser"];
                if (loggedInEmployee.RoleName == "Manager" || loggedInEmployee.RoleName == "Supervisor" || loggedInEmployee.RoleName == "Lead")
                {
                    Button editCardsButton = new Button();
                    editCardsButton.Text = "Edit Cards";
                    editCardsButton.PostBackUrl = ("./AgentCardView.aspx?agent=" + currentCard.EmployeeID);
                    TableCell editCards = new TableCell();
                    editCards.Controls.Add(editCardsButton);
                    tRow.Controls.Add(editCards);
                }
                else if (loggedInEmployee.RoleName == "Agent")
                {
                    Button viewCardsButton = new Button();
                    viewCardsButton.Text = "View Cards";
                    viewCardsButton.PostBackUrl = ("./AgentCardView.aspx?agent=" + currentCard.EmployeeID);
                    TableCell viewCards = new TableCell();
                    viewCards.Controls.Add(viewCardsButton);
                    tRow.Controls.Add(viewCards);
                }
            }
        }

        protected void listTopLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //when selected top level is changed, determine what was selected and rebind 
            var selectedTopLevel = listTopLevel.SelectedValue;
            Session["selectedTopLevel"] = selectedTopLevel;
            Session.Remove("resetToken");
            populateRepList();
        }

        public void populateRepList()
        {
            List<BusinessObjects.Employee> repList = new List<BusinessObjects.Employee>();
            string toplevel = listTopLevel.SelectedItem.Value;
            string department = listDepartment.SelectedItem.Value;
            //find list of employees based on selected role and department
            try
            {
                repList = _myEmployeeManager.FindEmployeesToList(toplevel, department).OrderBy(o => o.FullName).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            //bind employe drop down list from list created
            listRepresentative.DataTextField = "FullName";
            listRepresentative.DataValueField = "employeeID";
            listRepresentative.DataSource = repList;
            listRepresentative.DataBind();
        }

        protected void listDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedDepartment = listDepartment.SelectedValue;
            Session["selectedDepartment"] = selectedDepartment;
            populateRepList();
        }

        protected void listRepresentative_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void viewCardsButton_Click(object sender, EventArgs e)
        {

        }
    }
}