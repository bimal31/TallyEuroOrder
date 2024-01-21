<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealerOrderSchemeList.aspx.cs" Inherits="OrderApp.DealerOrderSchemeList" MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Style/datepicker.css" rel="stylesheet" />
    <script src="Script/jquery-1.9.1.js"></script>
    <script src="Script/bootstrap-datepicker.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>Dealer Order Scheme List</h3>
            </div>
            <div class="form-group pull-right">
                <asp:Button ID="btnAdd" runat="server" Text="Add Dealer Order Scheme" OnClick="btnAdd_Click" CssClass="btn btn-info btn-rounded" />
            </div>
        </div>

        <center>
            <asp:Label ID="lblErrorMessage"  ForeColor="Red"   runat="server"></asp:Label>
         </center>


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
                    <asp:GridView ID="grdOrder" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                        CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                        OnRowCommand="grdOrder_RowCommand" AllowPaging="true" OnPageIndexChanging="grdOrder_PageIndexChanging" PageSize="10"
                        OnRowDataBound="grdOrder_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="OrdSrNo" HeaderText="Order Sr No" />
                            <asp:BoundField DataField="OrderID" HeaderText="Order Id" />
                              <asp:BoundField DataField="OrderType" HeaderText="Order Type" />
                            <asp:BoundField DataField="OrderDate" HeaderText="Order Date"  />
                            <asp:BoundField DataField="DealerName" HeaderText="Dealer Name" />
                                  <asp:BoundField DataField="Area" HeaderText="Area" />
                            <asp:BoundField DataField="TotalKgGm" HeaderText="Total Kg" />
                             <%--<asp:BoundField DataField="OrderStatus" HeaderText="Order Status" />--%>
                          <%--  <asp:BoundField DataField="Other" HeaderText="Other" />
                            <asp:BoundField DataField="POP" HeaderText="POP" />
                            <asp:BoundField DataField="Transport" HeaderText="Transport" />--%>
                            <%-- <asp:BoundField DataField="OrderStatus" HeaderText="OrderStatus" />--%>
                            <asp:TemplateField HeaderText="View">
                                <ItemTemplate>
                                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info pull-right" CommandName="ViewValue" CommandArgument='<%# Eval("OrderID") + "|" + Eval("OrderType") %>' />
                                       <asp:HiddenField ID="hdnIsTallyImport" runat="server" Value='<%# Eval("IsTallyImport") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Print">
                                <ItemTemplate>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary pull-right" CommandName="PrintValue" CommandArgument='<%# Eval("OrderID") + "|" + Eval("OrderType") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Free" Visible="false">
                                <ItemTemplate>
                                    <asp:Button ID="btnFree" runat="server" Text="Free" CssClass="btn btn-danger" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-success pull-right" CommandName="EditValue" CommandArgument='<%# Eval("OrderID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                          <%--  <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning pull-right" CommandName="DeleteValue" CommandArgument='<%# Eval("OrderID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpOrderStatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpOrderStatus_SelectedIndexChanged" AutoPostBack="true">
                                       <%-- <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                        <asp:ListItem Text="Cancel" Value="Cancel"></asp:ListItem>
                                        <asp:ListItem Text="Factory" Value="Factory"></asp:ListItem>
                                        <asp:ListItem Text="Dispatch" Value="Dispatch"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdOrderListId" runat="server" Value='<%# Eval("OrderID") %>' />
                                    <asp:HiddenField ID="hdOrderStatus" runat="server" Value='<%# Eval("OrderStatus") %>' />
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
