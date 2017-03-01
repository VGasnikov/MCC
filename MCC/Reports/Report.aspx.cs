using System;
using System.Collections.Generic;
using System.Web.UI;
using Microsoft.Reporting.WebForms;

namespace MCC.Reports
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var userName = "mesaadmin";
                var reportName = Request.QueryString["id"];
                var lang = Request.QueryString["l"];
                
                if (lang != "en")
                    reportName += "-" + lang;
                if (!string.IsNullOrEmpty(reportName))
                    ReportViewer1.ServerReport.ReportPath = "/Mycrewcare/1.0/UAT/"+ reportName;

                var reportParameters = new List<ReportParameter>();

                switch(reportName)
                {
                    case "CancelMyRoomExternal":
                    case "FeebackSummaryExternalNew":
                        reportParameters.Add(new ReportParameter("PUserId", userName));
                        break;
                    case "YearToDateComplaints":
                    case "YearToDateCompliments":   
                    case "FeedbackRanking":    
                    case "AnalysisOfHotelFeedbacks":                                                               
                    case "AnalysisOfTransportFeedbacks":       
                        reportParameters.Add(new ReportParameter("UserName", userName));
                        break;

                }

                ReportViewer1.ServerReport.ReportServerCredentials = new DatamartServerCredentials();			
                ReportViewer1.ServerReport.SetParameters(reportParameters);
            }
        }

    }
}