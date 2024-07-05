<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AddBatch.aspx.cs" Inherits="OrderApp.AddBatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Style/datepicker.css" rel="stylesheet" />
    <script src="Script/jquery-1.9.1.js"></script>
    <script src="Script/bootstrap-datepicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>Order List</h3>
            </div>
          
            <div class="form-group pull-right">
                <asp:Button ID="btnSaveBatch" runat="server" Text="Save Batch" OnClick="btnSaveBatch_Click" CssClass="btn btn-info btn-rounded" />
            </div>

        </div>
        <center>
            <asp:Label ID="lblErrorMessage" ForeColor="Red" runat="server"></asp:Label>
        </center>
        <div class="panel-body">

            <div class="col-md-12">
                <div class="form-group">
                    <asp:GridView ID="grdOrder" DataKeyNames="OrderID" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                        CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%--<asp:CheckBox ID="CheckBox1" runat="server" />--%>
                                    <asp:CheckBox Text="  " ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="OrdSrNo" HeaderText="Order Sr No" />
                            <asp:BoundField DataField="OrderType" HeaderText="Order Type" />
                            <asp:BoundField DataField="OrderDate" HeaderText="Order Date" />
                            <asp:BoundField DataField="DealerName" HeaderText="Dealer Name" />
                            <asp:BoundField DataField="Area" HeaderText="Area" />
                            <asp:BoundField DataField="TotalKgGm" HeaderText="Total Kg" />
                            
                        </Columns>
                        <PagerStyle CssClass="pagination-ys" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
