<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealerList.aspx.cs" Inherits="OrderApp.Dealer" 
    MasterPageFile="~/MainMaster.Master" EnableEventValidation="false" %>

<%--<!DOCTYPE html>--%>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dealer List</title>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="Style/Common.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.5/jquery.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function ShowConfirmDelete(DeleteId) {
            $('#DeleteModal').modal('show');
        }
    </script>

    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>Dealer List</h3>
            </div>
            <div class="form-group pull-right">

                <asp:Button ID="btnAdd" runat="server" Text="Add Dealer" OnClick="btnAdd_Click" CssClass="btn btn-info btn-rounded" />
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
                            <asp:CheckBox ID="chkDealerCode" runat="server" Text="Dealer Without Code" OnCheckedChanged="chkDealerCode_CheckedChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-1">
                            <label>Search:</label>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" 
                                OnTextChanged="chkDealerCode_CheckedChanged" AutoPostBack="true"></asp:TextBox>
                        </div>

                         <div class="col-md-1">
                            <asp:Button ID="btnExport" runat="server" CssClass="btn btn-inverse" Text="Export To Excel" OnClick="ExportToExcelDealer" />
                        </div>
                    </div>
                </div>
            </div>
            <%----%>
            <div class="col-md-12">
                <div class="form-group table-responsive">
                    <asp:GridView ID="grdDealerList" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                        CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                         AllowPaging="true" OnRowCommand="grdDealerList_RowCommand" 
                         OnPageIndexChanging="grdDealerList_PageIndexChanging" PageSize="20">

                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Srno" />
                            <asp:BoundField DataField="DealerCode" HeaderText="Code" />
                            <asp:BoundField DataField="DealerName" HeaderText="Name" />
                            <asp:BoundField DataField="ContactName" HeaderText="Contact Name" />
                            <asp:BoundField DataField="Address" HeaderText="Address" />
                            <asp:BoundField DataField="Area" HeaderText="Area" />
                            <asp:BoundField DataField="GST" HeaderText="GST" />
                            <asp:BoundField DataField="Phone" HeaderText="Phone No" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-success pull-right" CommandName="EditValue" CommandArgument='<%# Eval("DealerId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning pull-right" CommandName="DeleteValue" CommandArgument='<%# Eval("DealerId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pagination-ys" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </div>
            </div>
        </div>


    </div>

    <!-- Modal -->
    <div class="modal fade" id="DeleteModal" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="background-color: orange;">
                    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                    <h4 class="modal-title">Delete Dealer</h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete the Record?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnDeleteOk" class="btn btn-success" runat="server" onserverclick="btnDeleteOk_ServerClick">Ok</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <input type="hidden" id="hdDeleteId" runat="server" />
</asp:Content>

<%-- </form>
</body>
</html>--%>
