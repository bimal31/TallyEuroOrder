<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" MasterPageFile="~/MainMaster.Master" Inherits="OrderApp.UserList" %>

<%--<!DOCTYPE html>--%>
<%--html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User List</title>

   
   
</head>
<body>--%>
<%--<form id="form1" runat="server">--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />--%>

    <%-- <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>User List</h3>

            </div>
            <div class="form-group pull-right">
                <asp:Button ID="btnAdd" runat="server" Text="Add User" OnClick="btnAdd_Click" CssClass="btn btn-info btn-rounded" />
            </div>
        </div>

        <center>
        <asp:Label ID="lblErrorMessage"  ForeColor="Red"   runat="server"></asp:Label>
         </center>


        <div class="panel-body">
            <div class="col-md-12" id="divUserTypeList" runat="server">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-3">
                            <label>User Type:</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlUserType1" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUserType1_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="" Text="All"></asp:ListItem>
                                <asp:ListItem Value="Admin" Text="Admin"></asp:ListItem>
                                <asp:ListItem Value="Clerk" Text="Clerk"></asp:ListItem>
                                <asp:ListItem Value="Salesman" Text="Salesman"></asp:ListItem>
                                <asp:ListItem Value="Factory" Text="Factory"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1">
                           <label>Search:</label>
                        </div>
                        <div class="col-md-5">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" OnTextChanged="txtSearch_CheckedChanged" AutoPostBack="true"></asp:TextBox>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="form-group">
                    <asp:GridView ID="grdUserList" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                        CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                        OnRowCommand="grdUserList_RowCommand" OnRowDataBound="grdUserList_RowDataBound" AllowPaging="true" OnPageIndexChanging="grdUserList_PageIndexChanging"
                        PageSize="10" AllowSorting="true" OnSorting="grdUserList_Sorting">
                        <Columns>
                            <asp:BoundField DataField="UserName" HeaderText="User Name" HeaderStyle-ForeColor="White" ItemStyle-Width="20%" SortExpression="UserName" />
                            <asp:BoundField DataField="FirstName" HeaderText="First Name" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="FirstName" />
                            <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="MiddleName" />
                            <asp:BoundField DataField="LastName" HeaderText="Last Name" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="LastName" />
                            <asp:BoundField DataField="UserType" HeaderText="UserType" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="UserType" />
                            <asp:BoundField DataField="PhoneNo" HeaderText="Phone No" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="PhoneNo" />
                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="MobileNo" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-success pull-right" ItemStyle-Width="10%" CommandName="EditValue" CommandArgument='<%# Eval("UserId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning pull-right" ItemStyle-Width="10%" CommandName="DeleteValue" CommandArgument='<%# Eval("UserId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pagination-ys" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </div>
            </div>
        </div>


    </div>
</asp:Content>

<%--</form>
</body>
</html>--%>
