<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Racun_report_WebForm.aspx.cs" Inherits="RestoranPOS.Reports.Racun_report_WebForm" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        
        
   
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Width="100%" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="521px">
            <LocalReport ReportPath="Reports\Racun_Report.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        
   
    </form>
</body>
</html>
