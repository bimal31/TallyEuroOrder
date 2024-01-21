<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="OrderApp.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Style/datepicker.css" rel="stylesheet" />
    <script src="Script/jquery-1.9.1.js"></script>
    <script src="Script/bootstrap-datepicker.js"></script>

    <style type="text/css">
        b {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>Dashboard </h3>
            </div>
        </div>
        <div class="panel-body">
            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblerror" runat="server" ForeColor="Red" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1">
                            <label>From Date</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <label>To Date</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <label>Sales Executive</label>
                        </div>
                        &nbsp;
                        <div class="col-md-3">
                            <asp:DropDownList ID="drpSalesExecutive" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-info" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="container">
        <div class="card-columns">
            <div class="card bg-light">
                <div class="card-body text-center">
                    <h3 style="color: green;">Order</h3>
                    <p class="card-text">Total Order: <b id="MainTotalOrder" runat="server"></b></p>
                    <p class="card-text">Total Kg: <b id="MainTotalKgs" runat="server"></b></p>
                    <p class="card-text">Pending Order: <b id="MainPending" runat="server"></b></p>
                    <p class="card-text">Factory Order: <b id="MainFactory" runat="server"></b></p>
                    <p class="card-text">Dispatch Department: <b id="MainDispatchdept" runat="server"></b></p>
                    <p class="card-text">Dispatched Order: <b id="MainDispatch" runat="server"></b></p>
                    
                </div>
            </div>

            <div>
            </div>

            <div class="card bg-light">
                <div class="card-body text-center">
                    <h3 style="color: orange;">With Bill Free Scheme</h3>
                    <p class="card-text">Total Order: <b id="FreeTotalOrder" runat="server"></b></p>
                    <p class="card-text">Total Kg: <b id="FreeTotalKgs" runat="server"></b></p>
                    <p class="card-text">Pending Order: <b id="FreePending" runat="server"></b></p>
                    <p class="card-text">Factory Order: <b id="FreeFactory" runat="server"></b></p>
                     <p class="card-text">Dispatch Department: <b id="FreeDispatchdept" runat="server"></b></p>
                    <p class="card-text">Dispatched Order: <b id="FreeDispatch" runat="server"></b></p>
                    
                </div>
            </div>

            <div>
            </div>

            <div class="card bg-light">
                <div class="card-body text-center">
                    <h3 style="color: blue;">Free Scheme</h3>
                    <p class="card-text">Total Order: <b id="DealerTotalOrder" runat="server"></b></p>
                    <p class="card-text">Total Kg: <b id="DealerTotalKgs" runat="server"></b></p>
                     <p class="card-text">Pending Order: <b id="DealerPending" runat="server"></b></p>
                    <p class="card-text">Factory Order: <b id="DealerFactory" runat="server"></b></p>
                     <p class="card-text">Dispatch Department: <b id="DealerDispatchdept" runat="server"></b></p>
                    <p class="card-text">Dispatched Order: <b id="DealerDispatch" runat="server"></b></p>
                </div>
            </div>
        </div>

        <ul class="nav nav-tabs">
            <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#MainOrder">Order</a></li>
            <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#FreeOrder">With Bill Free Scheme</a></li>
            <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#DealerOrder">Free Scheme</a></li>
        </ul>

        <div class="tab-content">
            <div id="MainOrder" class="tab-pane fade show active">
                <asp:GridView ID="grdMainOrder" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                    CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                    AllowPaging="true" OnPageIndexChanging="grdMainOrder_PageIndexChanging" PageSize="10" OnRowCommand="grdMainOrder_RowCommand" OnRowDataBound="grdMainOrder_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="OrdSrNo" HeaderText="Order Sr No" />
                        <asp:BoundField DataField="OrderID" HeaderText="Order Id" />
                        <asp:BoundField DataField="OrderType" HeaderText="Order Type" />
                        <asp:BoundField DataField="OrderDate" HeaderText="Order Date" /> <%--DataFormatString="{0:dd/MM/yyyy}" --%>
                        <asp:BoundField DataField="DealerName" HeaderText="Dealer Name" />
                        <asp:BoundField DataField="Area" HeaderText="Area" />
                        <asp:BoundField DataField="TotalKgGm" HeaderText="Total Kg" />
                        <asp:BoundField DataField="OrderStatus" HeaderText="Order Status" />
                        <%-- <asp:BoundField DataField="Other" HeaderText="Other" />
                        <asp:BoundField DataField="POP" HeaderText="POP" />--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info pull-right" CommandName="ViewValue" CommandArgument='<%# Eval("OrderID") + "|" + Eval("OrderType") + "|" + Eval("ParentOrderId") %>' />
                                <asp:HiddenField ID="hdOrderType" runat="server" Value='<%# Eval("OrderType") %>' />
                                 <asp:HiddenField ID="hdnIsTallyImport" runat="server" Value='<%# Eval("IsTallyImport") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary pull-right" CommandName="PrintValue" CommandArgument='<%# Eval("OrderID") + "|" + Eval("OrderType") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div id="FreeOrder" class="tab-pane fade">
                <asp:GridView ID="grdFreeOrder" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                    CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                    AllowPaging="true" OnPageIndexChanging="grdFreeOrder_PageIndexChanging" PageSize="10" OnRowCommand="grdFreeOrder_RowCommand" OnRowDataBound="grdFreeOrder_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="OrdSrNo" HeaderText="Order Sr No" />
                        <asp:BoundField DataField="OrderID" HeaderText="Order Id" />
                        <asp:BoundField DataField="OrderType" HeaderText="Order Type" />
                        <asp:BoundField DataField="OrderDate" HeaderText="Order Date" />
                        <asp:BoundField DataField="DealerName" HeaderText="Dealer Name" />
                        <asp:BoundField DataField="TotalKgGm" HeaderText="Total Kg" />
                        <asp:BoundField DataField="OrderStatus" HeaderText="Order Status" />
                        <%--  <asp:BoundField DataField="Other" HeaderText="Other" />
                        <asp:BoundField DataField="POP" HeaderText="POP" />--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info pull-right" CommandName="ViewValue" CommandArgument='<%# Eval("OrderID") + "|" + Eval("OrderType") + "|" + Eval("ParentOrderId") %>' />
                                <asp:HiddenField ID="hdOrderType" runat="server" Value='<%# Eval("OrderType") %>' />
                                 <asp:HiddenField ID="hdnIsTallyImport" runat="server" Value='<%# Eval("IsTallyImport") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary pull-right" CommandName="PrintValue" CommandArgument='<%# Eval("OrderID") + "|" + Eval("OrderType") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div id="DealerOrder" class="tab-pane fade">
                <asp:GridView ID="grdDealerOrder" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                    CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                    AllowPaging="true" OnPageIndexChanging="grdDealerOrder_PageIndexChanging" PageSize="10" OnRowCommand="grdDealerOrder_RowCommand" OnRowDataBound="grdDealerOrder_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="OrdSrNo" HeaderText="Order Sr No" />
                        <asp:BoundField DataField="OrderID" HeaderText="Order Id" />
                        <asp:BoundField DataField="OrderType" HeaderText="Order Type" />
                        <asp:BoundField DataField="OrderDate" HeaderText="Order Date" />
                        <asp:BoundField DataField="DealerName" HeaderText="Dealer Name" />
                        <asp:BoundField DataField="TotalKgGm" HeaderText="Total Kg" />
                        <asp:BoundField DataField="OrderStatus" HeaderText="Order Status" />
                        <%-- <asp:BoundField DataField="Other" HeaderText="Other" />
                        <asp:BoundField DataField="POP" HeaderText="POP" />--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info pull-right" CommandName="ViewValue" CommandArgument='<%# Eval("OrderID") + "|" + Eval("OrderType") + "|" + Eval("ParentOrderId") %>' />
                                <asp:HiddenField ID="hdOrderType" runat="server" Value='<%# Eval("OrderType") %>' />
                                 <asp:HiddenField ID="hdnIsTallyImport" runat="server" Value='<%# Eval("IsTallyImport") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary pull-right" CommandName="PrintValue" CommandArgument='<%# Eval("OrderID") + "|" + Eval("OrderType") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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
