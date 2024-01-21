<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorLog.aspx.cs" Inherits="OrderApp.ErrorLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- Tell the browser to be responsive to screen width -->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
   
    <!-- Favicon icon -->
    <%--  <link rel="icon" type="image/png" sizes="16x16" href="../assets/images/favicon.png">--%>
    <title>Euro 7000</title>
    <!-- Bootstrap Core CSS -->
    <link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- chartist CSS -->
    <%--<link href="../assets/plugins/chartist-js/dist/chartist.min.css" rel="stylesheet">
    <link href="../assets/plugins/chartist-js/dist/chartist-init.css" rel="stylesheet">
    <link href="../assets/plugins/chartist-plugin-tooltip-master/dist/chartist-plugin-tooltip.css" rel="stylesheet">--%>
    <!--This page css - Morris CSS -->
    <link href="../assets/plugins/c3-master/c3.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="lite/css/style.css" rel="stylesheet" />
    <link href="Style/ErrorMsg.css" rel="stylesheet" />
    <link rel="stylesheet" href="Style/Common.css" />
    <!-- You can change the theme colors from here -->
    <link href="lite/css/colors/blue.css" id="theme" rel="stylesheet" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js" /></script>
    <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
     <script src="Script/bootstrap-datepicker.js"></script>
    
</head>

<body>
    <form id="form1" runat="server">
        <div class="col-md-12">
            <div class="form-group">
                <asp:GridView ID="grdError" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                    CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                     AllowPaging="true" OnPageIndexChanging="grdError_PageIndexChanging" PageSize="20"
                    >
                    <Columns>
                        <asp:BoundField DataField="ERROR_LOG_ID" HeaderText="ERROR LOG ID" />
                        <asp:BoundField DataField="ERROR_NO" HeaderText="ERROR NO" />
                        <asp:BoundField DataField="MESSAGE" HeaderText="MESSAGE" />
                        <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" />
                        <asp:BoundField DataField="URL" HeaderText="URL" />
                        <asp:BoundField DataField="STACK_TRACE" HeaderText="STACK TRACE" />
                        <asp:BoundField DataField="DATE_ADDED" HeaderText="DATE ADDED" />
                        <asp:BoundField DataField="LAST_UPD_DATE" HeaderText="UPDATE DATE" />
                        <asp:BoundField DataField="IP_ADDED" HeaderText="ADDED IP" />
                        <asp:BoundField DataField="LAST_IP_UPDATED" HeaderText="UPDATE IP" />

                    </Columns>
                    <PagerStyle CssClass="pagination-ys" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>
            </div>
        </div>
        </div>
    </form>
</body>
</html>
