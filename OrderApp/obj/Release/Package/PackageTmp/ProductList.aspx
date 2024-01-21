<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="OrderApp.ProductList" MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>Product List</h3>
            </div>
            <div class="form-group pull-right">
                <asp:Button ID="btnAdd" runat="server" Text="Add Product" OnClick="btnAdd_Click" CssClass="btn btn-info btn-rounded" />
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
                <asp:GridView ID="grdProductList" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                    CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                    OnRowCommand="grdProduct_RowCommand" AllowPaging="true" OnPageIndexChanging="grdProductList_PageIndexChanging" PageSize="10" AllowSorting="true"
                    OnSorting="grdProductList_Sorting">
                    <Columns>
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" HeaderStyle-ForeColor="White" ItemStyle-Width="23%" SortExpression="ProductName" />
                        <asp:BoundField DataField="ProductDesc" HeaderText="Product Description" HeaderStyle-ForeColor="White" ItemStyle-Width="23%" SortExpression="ProductDesc" />
                        <asp:BoundField DataField="FromScheme" HeaderText="From Scheme" HeaderStyle-ForeColor="White" ItemStyle-Width="23%" SortExpression="ProductDesc" />
                        <asp:BoundField DataField="ToScheme" HeaderText="To Scheme" HeaderStyle-ForeColor="White" ItemStyle-Width="23%" SortExpression="ProductDesc" />
                        
                        <asp:TemplateField Visible="true">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-success pull-right" ItemStyle-Width="10%" CommandName="EditValue" CommandArgument='<%# Eval("ProductId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning pull-right" ItemStyle-Width="10%" CommandName="DeleteValue" CommandArgument='<%# Eval("ProductId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
