﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="PendingTally.aspx.cs" Inherits="OrderApp.PendingTally" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Style/datepicker.css" rel="stylesheet" />
    <script src="Script/jquery-1.9.1.js"></script>
    <script src="Script/bootstrap-datepicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>Pending Bill List</h3>
            </div>
        </div>
        <div class="panel-body">
            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>From Date</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>To Date</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-info" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="form-group">
                    <asp:GridView ID="grdPendingBill" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                        CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                        AllowPaging="true" OnPageIndexChanging="grdPendingBill_PageIndexChanging" PageSize="10"
                        OnRowCommand="grdPendingBill_RowCommand">
                        <Columns>
                            
                            <asp:BoundField DataField="OrdSrNo" HeaderText="Order Sr No" />
                            <asp:BoundField DataField="OrderDate" HeaderText="Order Date"  />
                            <asp:BoundField DataField="OrderType" HeaderText="Order Type" />
                            <asp:BoundField DataField="DealerName" HeaderText="Dealer Name" />
                                <asp:BoundField DataField="Area" HeaderText="Area" />
                            <asp:BoundField DataField="TotalKgGm" HeaderText="Total Kg" />
                            <asp:TemplateField HeaderText="View">
                                <ItemTemplate>
                                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info pull-right" CommandName="ViewValue" CommandArgument='<%# Eval("OrderID") + "|" + Eval("OrderType") + "|" + Eval("ParentOrderId") %>' />
                                    <asp:HiddenField ID="hdOrderType" runat="server" Value='<%# Eval("OrderType") %>' />
                                    <asp:HiddenField ID="hdnIsFree" runat="server" Value='<%# Eval("IsFree") %>' />
                                    <asp:HiddenField ID="hdnIsTallyImport" runat="server" Value='<%# Eval("IsTallyImport") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pagination-ys" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </div>
            </div>
        </div>



    </div>

    <script type="text/ecmascript">
        var j = jQuery.noConflict();
        j(function () {
            j('#<%=txtFromDate.ClientID%>, #<%=txtToDate.ClientID%>').datepicker({
                format: 'dd/mm/yyyy'

            }).on('changeDate', function (e) {
                var txtFromDate = j('#<%=txtFromDate.ClientID%>').val();
                var txtToDate = j('#<%=txtToDate.ClientID%>').val();

                if (txtFromDate != '' && txtToDate != '') {
                    var from = txtFromDate.split("/");
                    var f = new Date(from[2], from[1] - 1, from[0]);

                    var to = txtToDate.split("/");
                    var t = new Date(to[2], to[1] - 1, to[0]);

                    if (f.getTime() > t.getTime()) {
                        alert('ToDate must be greater than From Date.');
                        j('#<%=txtFromDate.ClientID%>').val('');
                        j('#<%=txtToDate.ClientID%>').val('');
                    }
                }
            });
        });
    </script>
</asp:Content>

