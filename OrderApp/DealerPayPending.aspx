<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealerPayPending.aspx.cs" Inherits="OrderApp.DealerPayPending"
    MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>Product packing List</h3>
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
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"  OnTextChanged="txtSearch_CheckedChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <asp:GridView ID="grdDealerPayPendingList" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                    CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                    OnRowCommand="grdDealerPayPendingList_RowCommand" AllowPaging="true" OnPageIndexChanging="grdDealerPayPendingList_PageIndexChanging" PageSize="10"
                    AllowSorting="true"
                    OnSorting="grdDealerPayPendingList_Sorting">
                    <Columns>
                        <asp:BoundField DataField="DealerCode" HeaderText="Dealer Code" HeaderStyle-ForeColor="White" ItemStyle-Width="20%" SortExpression="DealerCode" />
                        <asp:BoundField DataField="DealerName" HeaderText="Dealer Name" HeaderStyle-ForeColor="White" ItemStyle-Width="20%" SortExpression="DealerName" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="Amount" />
                        <asp:BoundField DataField="IsAllowOrder" HeaderText="Is Allow Order" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="IsAllowOrder" />
                        <asp:BoundField DataField="CreateDate" HeaderText="Create Date" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="CreateDate" />
                        <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date" HeaderStyle-ForeColor="White" ItemStyle-Width="10%" SortExpression="UpdatedDate" />
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnAllowOrder" runat="server" Text="Edit" ItemStyle-Width="10%" CssClass="btn btn-success pull-right"
                                    CommandName="EditValue" CommandArgument='<%# Eval("DealerCode") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <PagerStyle CssClass="pagination-ys" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>
            </div>
        </div>



    </div>
</asp:Content>
