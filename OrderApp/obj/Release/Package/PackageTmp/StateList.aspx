<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StateList.aspx.cs" Inherits="OrderApp.StateList" MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>Territory List</h3>
            </div>
            <div class="form-group pull-right">
                <asp:Button ID="btnAdd" runat="server" Text="Add Territory" OnClick="btnAdd_Click" CssClass="btn btn-info btn-rounded" />
            </div>
        </div>

      

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
                <asp:GridView ID="grdStateList" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                    CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                    OnRowCommand="grdStateList_RowCommand" AllowPaging="true" OnPageIndexChanging="grdStateList_PageIndexChanging" PageSize="10" AllowSorting="true"
                    OnSorting="grdStateList_Sorting">
                    <Columns>
                        <asp:BoundField DataField="country_name" HeaderStyle-ForeColor="White" ItemStyle-Width="40%" HeaderText="Country Name" SortExpression="country_name" />
                        <asp:BoundField DataField="state_name" HeaderStyle-ForeColor="White" ItemStyle-Width="40%" HeaderText="Territory" SortExpression="state_name" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" ItemStyle-Width="10%" CssClass="btn btn-success pull-right" CommandName="EditValue" CommandArgument='<%# Eval("state_id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" ItemStyle-Width="10%" CssClass="btn btn-warning pull-right" CommandName="DeleteValue" CommandArgument='<%# Eval("state_id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>
            </div>
        </div>



    </div>
</asp:Content>
