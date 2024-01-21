<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FreeOrder.aspx.cs" Inherits="OrderApp.FreeOrder" MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Style/datepicker.css" rel="stylesheet" />
    <script src="Script/jquery-1.9.1.js"></script>
    <script src="Script/bootstrap-datepicker.js"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="editorderid" Style="display: none" runat="server"></asp:TextBox>

    <asp:TextBox ID="isview" Style="display: none" runat="server"></asp:TextBox>

    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>
                    <asp:Label ID="lblheading" runat="server"></asp:Label>
                </h3>
            </div>
            <div class="form-group pull-right">
                <asp:Button ID="btnback" runat="server" Text="Back To List" CssClass="btn btn-info btn-rounded" OnClick="btnback_Click" />
            </div>
        </div>

        <center>
            <asp:Label ID="lblErrorMessage" ForeColor="Red" runat="server"></asp:Label>
        </center>

        <div class="panel-body">
            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lbldealercode" runat="server" Text="Dealer Code"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDealerCodeSearch" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtDealerCodeSearch_TextChanged"></asp:TextBox>

                        </div>
                        <div class="col-md-2">
                            <label>Order Date:</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">

                        <div class="col-md-2">
                            <label>Dealer Name:</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtdealernamesearch" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>

                        <div class="col-md-2">
                            <label>Sales Executive:</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="drpsSalesExe" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">


                        <div class="col-md-2">
                            <label>Transport:</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txttransport" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Order Status :</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="drpOrderStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                <asp:ListItem Text="Factory" Value="Factory"></asp:ListItem>
                                <asp:ListItem Text="Dispatch Department" Value="Dispatch Department"></asp:ListItem>
                                <asp:ListItem Text="Dispatched" Value="Dispatched"></asp:ListItem>
                                <asp:ListItem Text="Cancel" Value="Cancel"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                </div>
            </div>

            <%-- <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            From Scheme:
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtFromScheme" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            To Scheme:
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtToScheme" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>--%>


            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="grdProductFree" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                                CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" FooterStyle-CssClass="lastrow"
                                RowStyle-CssClass="rows" AutoGenerateColumns="false"
                                ShowFooter="true" OnRowDataBound="grdProductFree_RowDataBound" DataKeyNames="ProductId">
                                <Columns>


                                    <asp:TemplateField HeaderText="Product Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalkg" BackColor="Transparent" runat="server" Text='<%#Eval("productName") %>' />
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lbltotaltext" BackColor="Transparent" runat="server" Text="Total" />
                                        </FooterTemplate>
                                    </asp:TemplateField>



                                    <%-- <asp:BoundField DataField="Totalkg" HeaderText="Total kg" />--%>

                                    <asp:TemplateField HeaderText="Total kg">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalkg" BackColor="Transparent" runat="server" Text='<%#Eval("Totalkg") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbltotal" BackColor="Transparent" runat="server" Text="Total Amount" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <%-- <asp:BoundField DataField="Toscheme" HeaderText="To scheme" />--%>
                                    <asp:BoundField DataField="Fromscheme" HeaderText="From scheme" />
                                     <asp:BoundField DataField="Toscheme" HeaderText="To scheme" />
                                    

                                    <%--<asp:BoundField DataField="FreeScheme" HeaderText="Free Scheme" />--%>

                                    <asp:TemplateField HeaderText="Free Scheme">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFreeScheme" BackColor="Transparent" name="freescheme" CssClass="freescheme" runat="server" Text='<%# Eval("FreeScheme") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalFree" BackColor="Transparent" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>


            <%--<div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            Total Kgs:
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtTotalKgsF" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Free Total Kgs:
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtFreetotalkg" runat="server" Text="0" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>--%>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="col-12" style="text-align: right;">
                    <a class="btn btn-primary" id="addrow" style="color: white"><i class="fa fa-plus"></i>&nbsp Add Row</a>
                </div>
                <table id="tblitemScheme" class="table order-list">
                    <thead>
                        <tr>
                            <td style="width: 200px">Product item</td>
                            <td style="width: 200px">Package</td>
                            <td style="width: 200px">Qty</td>
                            <td style="width: 200px">Scheme</td>
                            <td style="width: 200px">Total Kg</td>
                            <td style="display: none;">sr.no</td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>

            <div class="col-md-12" id="divOtherDetails" runat="server">
                <div class="form-group">
                    <div class="row">

                        <div class="col-md-1">Site Delivery</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtsitedelivery" runat="server" TextMode="MultiLine" Style="width: 100%; height: 100px;">
                            </asp:TextBox>
                        </div>

                        <div class="col-md-1">Other :</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtOther" runat="server" TextMode="MultiLine" Style="width: 100%; height: 100px;">
                            </asp:TextBox>
                        </div>

                        <div class="col-md-1">Pop :</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtPOP" runat="server" TextMode="MultiLine" Style="width: 100%; height: 100px;">
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            <label>Total :</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtFreeKg" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-8">
                        </div>
                        <div class="col-md-1 pull-right;">
                            <input id="btnSubmitOrder" type="button" value="Sent" class="btn btn-success" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdDelaerId" runat="server" />
    <asp:HiddenField ID="hdFreeOrderId" runat="server" />
    <asp:HiddenField ID="hdTotalFreeKgCount" runat="server" />
    <asp:HiddenField ID="hdTotalKgCount" runat="server" />
    <asp:HiddenField ID="hdEditOrderId" runat="server" />
    <asp:HiddenField ID="hdstate" runat="server" />

    <asp:HiddenField ID="hdOrdertype" runat="server" />




    <div class="modal fade" id="ViewDealerModal" tabindex="-1" role="dialog" aria-labelledby="ModalTitle"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="ModalTitle">View Dealer</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-lg-12">
                                <label>Dealer Code</label>
                                <asp:TextBox ID="txtDealerCode" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <label>Dealer Name</label>
                                <asp:TextBox ID="txtDealerName" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>ContactName</label>
                                <asp:TextBox ID="txtContactName" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>Address</label>
                                <asp:TextBox ID="txtAddress" TextMode="MultiLine" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>Area</label>
                                <asp:TextBox ID="txtArea" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>Pincode</label>
                                <asp:TextBox ID="txtpincode" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>Phone No</label>
                                <asp:TextBox ID="txtPhoneNo" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>GST</label>
                                <asp:TextBox ID="txtGST" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal -->
    <script src="Script/withbillFreeScheme.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#lnkdealerInfo').click(function () {
                $('#ViewDealerModal').modal('show');
            });

        });

<%--        $('#ContentPlaceHolder1_txtFromScheme').change(function () {

            if ($('#ContentPlaceHolder1_txtFromScheme').val() == "" || $('#ContentPlaceHolder1_txtToScheme').val() == "") {

                $('#ContentPlaceHolder1_txtFreetotalkg').val(0);
            }
            else {


                $('#ContentPlaceHolder1_txtFreetotalkg').val((($('#ContentPlaceHolder1_txtTotalKgsF').val() * $('#ContentPlaceHolder1_txtToScheme').val()) / $('#ContentPlaceHolder1_txtFromScheme').val()));
                var FreeTotalKg = Math.ceil($('#ContentPlaceHolder1_txtFreetotalkg').val());
                $('#ContentPlaceHolder1_txtFreetotalkg').val(FreeTotalKg);

                //var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
                var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();

            }

        });

        $('#ContentPlaceHolder1_txtToScheme').change(function () {

            if ($('#ContentPlaceHolder1_txtFromScheme').val() == "" || $('#ContentPlaceHolder1_txtToScheme').val() == "") {

                $('#ContentPlaceHolder1_txtFreetotalkg').val(0);
            }
            else {

                $('#ContentPlaceHolder1_txtFreetotalkg').val((($('#ContentPlaceHolder1_txtTotalKgsF').val() * $('#ContentPlaceHolder1_txtToScheme').val()) / $('#ContentPlaceHolder1_txtFromScheme').val()));
                var FreetotalKg = Math.ceil($('#ContentPlaceHolder1_txtFreetotalkg').val());
                $('#ContentPlaceHolder1_txtFreetotalkg').val(FreetotalKg);

                //var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
                var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();

                //if (parseFloat(txtFreeKg) > parseFloat(txtFreetotalkg)) {
                //    alert("You can't add more Free kg than allowed kg.");
                //}
            }
        });--%>


        function round(value, exp) {
            if (typeof exp === 'undefined' || +exp === 0)
                return Math.round(value);

            value = +value;
            exp = +exp;

            if (isNaN(value) || !(typeof exp === 'number' && exp % 1 === 0))
                return NaN;

            // Shift
            value = value.toString().split('e');
            value = Math.round(+(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp)));

            // Shift back
            value = value.toString().split('e');
            return +(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp));
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function TotalNumber(TotalKG, PackingType, this1) {
            // Box/Qty Value
            var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }

            // Last Box/Qty Value
            var HdnLastQty = $(this1).parent().find('input:hidden').val();
            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }

            // Total Value
            var HdnTotalKgCount = $('#<%=hdTotalKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }
            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);

            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);

            if (PackingType.toLowerCase() == 'gm') {
                TotalKgCount = TotalKgCount / 1000;
                HdnLastQty = HdnLastQty / 1000;
            }

            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            //if (TotalKgCount == 0) {
            //    FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            //}
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=txtFreeKg.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find('input:hidden').val($(this1).val());
        }



    </script>

</asp:Content>
