using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExceptionDashboard
{
    public partial class ConsultationCardReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime month = Convert.ToDateTime("1/1/2000");
            for (int i = 0; i < 12; i++)
            {
                DateTime nextMonth = month.AddMonths(i);
                ListItem list = new ListItem();
                list.Text = nextMonth.ToString("MMMM");
                list.Value = nextMonth.Month.ToString();
                ddlReportMonth.Items.Add(list);
            }
            ddlReportMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
        }
    }
}