<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductPacking.aspx.cs" Inherits="OrderApp.ProductPacking"
    MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>Product packing List</h3>
            </div>

            <div class="form-group pull-right">
                <asp:Button ID="btnAdd" runat="server" Text="Add Product Packing" OnClick="btnAdd_Click"
                    CssClass="btn btn-info btn-rounded" />
            </div>
        </div>
        

        <center>
            <asp:Label ID="lblErrorMessage" ForeColor="Red" runat="server"></asp:Label>
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
                <asp:GridView ID="grdProductPackingList" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                    CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                    OnRowCommand="grdProductPackingList_RowCommand" AllowPaging="true" OnPageIndexChanging="grdProductPackingList_PageIndexChanging" PageSize="10"
                    AllowSorting="true"
                    OnSorting="grdProductPackingList_Sorting">
                    <Columns>
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" HeaderStyle-ForeColor="White" ItemStyle-Width="20%" SortExpression="ProductName" />
                        <asp:BoundField DataField="ProductPckDetails" HeaderText="Product Packing Details" HeaderStyle-ForeColor="White" ItemStyle-Width="20%" SortExpression="ProductPckDetails" />
                        <asp:BoundField DataField="UOM" HeaderText="UOM" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="UOM" />
                         <asp:BoundField DataField="ProductPck" HeaderText="Product Packing" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="ProductPck" />
                        <asp:BoundField DataField="PackingType" HeaderText="Packing Type" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="PackingType" />
                        <asp:BoundField DataField="PackingNos" HeaderText="Product Number" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="PackingNos" />
                        <asp:BoundField DataField="TotalKG" HeaderText="Total KG" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="TotalKG" />
                        <asp:BoundField DataField="IsScheme" HeaderText="IsScheme" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="IsScheme"  />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" ItemStyle-Width="10%"  CssClass="btn btn-success pull-right"
                                    CommandName="EditValue" CommandArgument='<%# Eval("ProductPckID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" ItemStyle-Width="10%" CssClass="btn btn-warning pull-right"
                                    CommandName="DeleteValue" CommandArgument='<%# Eval("ProductPckID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <PagerStyle CssClass="pagination-ys" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>
            </div>
        </div>



    </div>
</asp:Content>
