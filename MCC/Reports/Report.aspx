<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="MCC.Reports.Report" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html>
<html>
<head>
    <style>
        body {
            background-color: white;
            padding:0; margin:0;
        }
    </style>
</head>
<body>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote"  Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)"  ShowCredentialPrompts="False" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="1000px" Width="1440px" BackColor="#F7F7F7" >
                <ServerReport ReportPath="/Mycrewcare/1.0/UAT/FeedbackSummaryExternalNew" 
                ReportServerUrl="http://datamartuat.hq.hotelexpress.com/ReportServer" />
</rsweb:ReportViewer>
        </form>
</body>
</html>
