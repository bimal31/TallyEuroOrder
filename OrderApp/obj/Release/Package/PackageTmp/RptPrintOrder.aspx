<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptPrintOrder.aspx.cs" Inherits="OrderApp.RptPrintOrder" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
    <title>Order Print</title>

    <script src="Script/jquery-1.9.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Script/ReportViewer.js" />
           </Scripts>
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="600px" Width="100%"></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
