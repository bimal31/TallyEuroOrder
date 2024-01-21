<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SchemeList.aspx.cs" Inherits="OrderApp.SchemeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>Scheme List</h3>
            </div>
            <div class="form-group pull-right">
                <asp:Button ID="btnAdd" runat="server" Text="Add Scheme" OnClick="btnAdd_Click" CssClass="btn btn-info btn-rounded" />
            </div>
        </div>
        <%--<asp:TextBox ID="txtSearch" runat="server" />
        <asp:Button Text="Search" runat="server" OnClick="Searchscheme" />--%>
        <center>
        <asp:Label ID="lblErrorMessage"  ForeColor="Red"   runat="server"></asp:Label>
         </center>
        <div class="panel-body">

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-3">
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

                <asp:GridView ID="grdSchemeList" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                    CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                    OnRowCommand="grdSchemeList_RowCommand" AllowPaging="true" OnPageIndexChanging="grdSchemeList_PageIndexChanging" PageSize="10" AllowSorting="true"
                    OnSorting="grdSchemeList_Sorting">
                    <Columns>
                        <asp:BoundField DataField="SchemeName" HeaderStyle-ForeColor="White" ItemStyle-Width="40%" SortExpression="SchemeName" HeaderText="Scheme Name" />
                        <asp:BoundField DataField="Schemedescription" HeaderText="Scheme Description" HeaderStyle-ForeColor="White" ItemStyle-Width="40%" SortExpression="Schemedescription" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-success pull-right" CommandName="EditValue" CommandArgument='<%# Eval("SchemeId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning pull-right" CommandName="DeleteValue" CommandArgument='<%# Eval("SchemeId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>

            </div>
        </div>



    </div>
</asp:Content>
