using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using BusinessObjects;
using System.Globalization;
using System.Collections.Specialized;

namespace ExceptionDashboard
{
    public partial class ConsultationCardReport : System.Web.UI.Page
    {
        ConsultationCardManager _myConsultationCardManager = new ConsultationCardManager();
        EmployeeManager _myEmployeeManager = new EmployeeManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblCompletionTime.Text += string.Format("<br />");
            ddlReportMonth.Enabled = false;
            DateTime month = Convert.ToDateTime("1/1/2000");
            for (int i = 0; i < 12; i++)
            {
                DateTime nextMonth = month.AddMonths(i);
                ListItem list = new ListItem();
                list.Text = nextMonth.ToString("MMMM");
                list.Value = nextMonth.Month.ToString();
                ddlReportMonth.Items.Add(list);
            }
            if (!IsPostBack)
            {
                ddlReportMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
            }
            
            int currentReportMonth = Convert.ToInt32(ddlReportMonth.SelectedValue);
            string mostAwardedCard = _myConsultationCardManager.SelectMostAwardedCard(currentReportMonth);

            

            Image myImg = new Image();
            myImg.ImageUrl = "/images/" + mostAwardedCard + "1.png";
            myImg.Visible = true;
            lblMostAwardedImage.Controls.Add(myImg);

            string leastAwardedCard = _myConsultationCardManager.SelectLeastAwardedCard(currentReportMonth);

            Image myImg2 = new Image();
            myImg2.ImageUrl = "/images/" + leastAwardedCard + "1.png";
            myImg2.Visible = true;
            lblLeastAwardedImage.Controls.Add(myImg2);

            List<string> cardList = _myConsultationCardManager.SelectMostTargetedCards(currentReportMonth);

            Image myImg3 = new Image();
            myImg3.ImageUrl = "/images/" + cardList[0] + "1.png";

            Image myImg4 = new Image();
            myImg4.ImageUrl = "/images/" + cardList[1] + "1.png";

            Image myImg5 = new Image();
            myImg5.ImageUrl = "/images/" + cardList[2] + "1.png";

            lblTargetedFirstImages.Controls.Add(myImg3);
            lblTargetedFirstImages.Controls.Add(myImg4);
            lblTargetedFirstImages.Controls.Add(myImg5);

            List<string> cardList2 = _myConsultationCardManager.SelectLeastTargetedCards(currentReportMonth);

            Image myImg6 = new Image();
            myImg6.ImageUrl = "/images/" + cardList2[0] + "1.png";

            Image myImg7 = new Image();
            myImg7.ImageUrl = "/images/" + cardList2[1] + "1.png";

            Image myImg8 = new Image();
            myImg8.ImageUrl = "/images/" + cardList2[2] + "1.png";

            lblTargetedLastImages.Controls.Add(myImg6);
            lblTargetedLastImages.Controls.Add(myImg7);
            lblTargetedLastImages.Controls.Add(myImg8);

            List<ConsultationSheet> sheetDates = _myConsultationCardManager.SelectConsultationSheetDates(currentReportMonth);

            List<int> timeList = new List<int>();
            for (int i = 0; i < sheetDates.Count(); i++)
            {
                DateTime start = DateTime.Parse(sheetDates[i].createdDate);
                DateTime end = DateTime.Parse(sheetDates[i].completedDate);
                var diff = end.Subtract(start);
                int timeDiff = Convert.ToInt32(diff.TotalSeconds);
                timeList.Add(timeDiff);
            }

            int totalTime = 0;
            for (int i = 0; i < timeList.Count(); i++)
            {
                totalTime += timeList[i];
            }

            int averageSeconds = totalTime / timeList.Count();

            TimeSpan t = TimeSpan.FromSeconds(averageSeconds);

            string avgTime = string.Format("{0:D2}h:{1:D2}m:{2:D2}s", 
                t.Hours, 
                t.Minutes, 
                t.Seconds);

            lblCompletionTime.Text += avgTime;


            List<CardMethod> currentMethods = _myConsultationCardManager.SelectCardMethods();
            lblAwardMethod.Text += string.Format("<br />");
            for (int i = 0; i < currentMethods.Count(); i++)
            {
                int currentCount = _myConsultationCardManager.SelectCountByMethod(currentReportMonth, currentMethods[i].methodName);
                string countFormat = string.Format(currentMethods[i].methodName + ": " + currentCount + "<br />");
                lblAwardMethod.Text += countFormat;
            }

            //NameValueCollection countByDept = _myConsultationCardManager.SelectTotalEntriesByDept(currentReportMonth);
            //lblEntriesByDept.Text += string.Format("<br />");
            //for(int i=0; i<countByDept.Count; i++)
            //{
            //    lblEntriesByDept.Text += string.Format(countByDept.GetKey(i) + " " + countByDept.GetValues(i) + "<br />");
            //}
            lblEntriesByDept.Text += string.Format("<br />");
            lblTopTeamsByDept.Text += string.Format("<br />");
            List<Department> deptList = _myEmployeeManager.FetchDepartmentList();
            NameValueCollection deptEntries = new NameValueCollection();

            for(int i=0; i<deptList.Count; i++)
            {
                int deptCount = _myConsultationCardManager.SelectTotalEntriesByDepartment(currentReportMonth, deptList[i].departmentName);

                deptEntries.Add(deptList[i].departmentName, deptCount.ToString());
            }
            var sorted = deptEntries.AllKeys.OrderByDescending(key => deptEntries[key]).Select(key => new KeyValuePair<string, string>(key, deptEntries[key]));

            var charsToRemove = new string[] { "[", "]" };
            for (int i = 0; i < deptList.Count; i++)
            {
                string topTeams = sorted.ElementAt(i).ToString() + "<br />";
                foreach(var c in charsToRemove)
                {
                    topTeams = topTeams.Replace(c, string.Empty);
                    
                }
                topTeams = topTeams.Replace(",", ":");
                topTeams = topTeams.Replace(" Support", string.Empty);
                lblEntriesByDept.Text += string.Format(topTeams);
            }

            


            for(int i=0; i<deptList.Count; i++)
            {
                NameValueCollection supEntries = new NameValueCollection();
                List<Employee> supList = _myEmployeeManager.SelectSupsByDept(deptList[i].departmentName);
                for(int x=0; x<supList.Count; x++)
                {
                    
                        int totalEntries = _myConsultationCardManager.SelectTotalEntriesBySup(currentReportMonth, supList[x]);
                        string supName = supList[x].FirstName + " " + supList[x].LastName;
                        supEntries.Add(supName, totalEntries.ToString());
                    

                }
                var sortedSups = supEntries.AllKeys.OrderByDescending(key => supEntries[key]).Select(key => new KeyValuePair<string, string>(key, supEntries[key]));
                if (sortedSups.Count() > 0)
                {
                    string topSup = sortedSups.ElementAt(0).ToString() + "<br />";
                    foreach (var q in charsToRemove)
                    {
                        topSup = topSup.Replace(q, string.Empty);
                    }
                    if (topSup.Contains("Sup") || topSup.Contains("Supervisor") || topSup.Contains("0"))
                    {
                        lblTopTeamsByDept.Text += deptList[i].departmentName + ": No Entries" + "<br />";
                    }
                    else
                    {
                        topSup = topSup.Replace(",", " -");
                        string dept = deptList[i].departmentName;
                        dept = dept.Replace(" Support", string.Empty);
                        lblTopTeamsByDept.Text += dept + ": ";
                        lblTopTeamsByDept.Text += string.Format(topSup);
                    }
                }
            }
            

        }
    }
}