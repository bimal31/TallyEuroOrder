<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="OrderReport.aspx.cs" 
    Inherits="OrderApp.OrderReport" EnableEventValidation="false" %>

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
                <h3>Order Report </h3>
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
                            <label>Order Status</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="drpOrderStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                             <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-info" />
                             
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnExport" runat="server" CssClass="btn btn-inverse" Text="Export To Excel" OnClick="ExportToExcel" />
                            </div>


                    </div>
                    <div class="row">

                        <div class="col-md-8">
                        </div>

                        <div class="col-md-2">
                           
                        </div>

                        <div class="col-md-2">
                           
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="container">
        <div class="tab-content">
            <div id="MainOrder" class="tab-pane fade show active">
                <asp:GridView ID="grdMainOrder" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                    CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                    AllowPaging="true" OnPageIndexChanging="grdMainOrder_PageIndexChanging" PageSize="10">
                    <Columns>

                          <asp:BoundField DataField="row_num" HeaderText="Sr. No." />
                        <asp:BoundField DataField="OrderDate" HeaderText="Order Date" />
                        <asp:BoundField DataField="OrderTime" HeaderText="Order Time" />
                        <asp:BoundField DataField="OrderSrNo" HeaderText="Order Sr No" />
                        <asp:BoundField DataField="OrderType" HeaderText="Order Type" />

                        <asp:BoundField DataField="Dealercode" HeaderText="Dealer Code" />
                        <asp:BoundField DataField="DealerName" HeaderText="Dealer Name" />
                        <asp:BoundField DataField="Area" HeaderText="Area" />
                        <asp:BoundField DataField="TotalKgGm" HeaderText="Total Kg" />
                        <asp:BoundField DataField="OrderStatus" HeaderText="Order Status" />
                        <asp:BoundField DataField="Other" HeaderText="Remark" />
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
