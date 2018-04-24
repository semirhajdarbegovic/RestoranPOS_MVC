using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using RestoranPOS.Reports;

namespace RestoranPOS.Reports
{
    public partial class Racun_report_WebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var podaciHeader = Racun_report_Model.GetHeader(18);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Header", podaciHeader));

                var podaciBody = Racun_report_Model.GetBody(1);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", podaciBody));

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("") + "/Racun_Report.rdlc";
                ReportViewer1.DataBind();
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}