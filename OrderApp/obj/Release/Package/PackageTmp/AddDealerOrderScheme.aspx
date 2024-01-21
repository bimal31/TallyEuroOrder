<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDealerOrderScheme.aspx.cs" Inherits="OrderApp.AddDealerOrderScheme" MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Style/datepicker.css" rel="stylesheet" />
    <script src="Script/jquery-1.9.1.js"></script>
    <script src="Script/bootstrap-datepicker.js"></script>

    <%--<script src="Script/jquery-1.11.1.min.js"></script>--%>
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
                            <a id="lnkdealerInfo" href="#">View Dealer Information</a>
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

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>From Date:</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>To Date:</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row" style="display: none">
                        <div class="col-md-2">
                            <label>From Scheme:</label>
                        </div>
                        <div class="col-md-4">
                            <%--onchange="TotalFreeKg();"--%>
                            <asp:TextBox ID="txtFromScheme" runat="server" CssClass="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>To Scheme:</label>
                        </div>
                        <div class="col-md-4">
                            <%--onchange="TotalFreeKg();"--%>
                            <asp:TextBox ID="txtToScheme" runat="server" CssClass="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>



            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>Purchase Kg:</label>
                        </div>
                        <div class="col-md-4">
                            <%--onchange="TotalFreeKg();"--%>
                            <asp:TextBox ID="txtPurchaseKg" runat="server" CssClass="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                        </div>
                        <div class="col-md-2" style="display: none">
                            <label>Total Free Kg:</label>
                        </div>
                        <div class="col-md-4" style="display: none">
                            <asp:TextBox ID="txtTotalFreeKg" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

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
                            <td style="width: 200px">Purchase Kg</td>
                            <td style="width: 200px">From Scheme</td>
                            <td style="width: 200px">To Scheme</td>
                            <td style="width: 200px">Total Free Kg</td>
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
                            <asp:TextBox ID="lblTotal" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-8">
                        </div>
                        <div class="col-md-1 pull-right;">
                            <%--OnClick="btnSubmitOrder_Click"--%>
                            <input id="btnSubmitOrder" type="button" value="Sent" class="btn btn-success" />
                            <%--<asp:Button ID="btnSubmitOrder" runat="server" Text="Send" CssClass="btn btn-success"  OnClientClick="return Check();" />--%>
                            <%--<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-dark" OnClick="btnClear_Click" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdDelaerId" runat="server" />
    <asp:HiddenField ID="hdOrderId" runat="server" />
    <asp:HiddenField ID="hdEditOrderId" runat="server" />
    <asp:HiddenField ID="hdTotalKgCount" runat="server" />
    <asp:HiddenField ID="hdstate" runat="server" />
    


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
    <div id="modelfreeitem" class="modal" tabindex="-1" data-keyboard="false" data-backdrop="static" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 700px !important;">
                <div class="modal-header">
                    <h4 class="modal-title"><span id="modeltitle"></span></h4>
                    <button type="button" class="close" onclick="prddtlmodelclose()">×</button>

                </div>
                <div class="modal-body">
                    <input type="text" id="srnoProd" class="form-control srnoProd" style="display: none" />
                    <input type="text" id="Productid" class="form-control Productid" style="display: none" />
                    <div class="col-12 row">

                        <div class="col-6">
                            <div class="form-group">
                                <label class="control-label">Item:</label>
                                <b>
                                    <label class="control-label" id="itemname"></label>
                                </b>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label class="control-label">Free Kg:</label>
                                &nbsp
                                <b>
                                    <label class="control-label Producttotalfreekg" id="Producttotalfreekg"></label>
                                </b>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 row">
                        <div class="col-5">
                            <div class="form-group">
                                <label class="control-label">PKG :</label>
                                <select id="drppkg" class="form-control drpProductPacking" onchange="selectionitemFreeitem(this)">
                                    <option>select</option>
                                </select>

                            </div>
                        </div>
                        <div class="col-1">
                            <label id="package" class="package" style="padding-top: 38px;"></label>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label class="control-label">QTY :</label>
                                <input type="text" id="qty" class="form-control" maxlength="4" onkeypress="return isNumber(event)" />

                            </div>
                        </div>
                        <div class="col-2">
                            <button type="button" class="btn btn-primary waves-effect waves-light SaveOrderProductDetail" style="margin-top: 31px;" id="SaveOrderProductDetail">ADD</button>

                            <button type="button" class="btn btn-default waves-effect " onclick="prddtlmodelclose()" style="display: none">Close</button>
                        </div>
                    </div>
                    <div class="form-group isschemedisplay" id="isschemedisplay">
                        <label class="control-label">Scheme :</label>
                        <select id="dropschemeId" class="form-control drpscheme">
                        </select>
                        <textarea id="Schemetext" class="form-control Schemetext" rows="3" cols="30"></textarea>
                    </div>

                </div>
                <hr />

                <table id="tblOrderProductDetail" class="tblOrderProductDetail table">
                    <thead>
                        <tr>
                            <th>PKG</th>
                            <th>QTY</th>
                            <th></th>
                            <th>Total Kg</th>
                            <th style="display: none">srno</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div class="col-12 row" style="margin-left: 10px; padding-bottom: 20px;">
                    <div class="col-2">

                        <label class="control-label">QTY :</label>

                    </div>
                    <div class="col-2">

                        <input type="text" id="popuplistfreekg" class="form-control disabled" maxlength="4" />

                    </div>
                </div>
                <%-- <button type="button" class="btn btn-default waves-effect " id="savechange" onclick="SaveClose()">Save & Close</button>--%>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <script src="Script/OrderScheme.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=txtFromDate.ClientID%>').attr('readonly', 'readonly');
            $('#<%=txtToDate.ClientID%>').attr('readonly', 'readonly');

            $('#lnkdealerInfo').click(function () {
                $('#ViewDealerModal').modal('show');
            });

        });


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
            $('#<%=lblTotal.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find('input:hidden').val($(this1).val());
        }



    </script>

    <script type="text/ecmascript">

        function TotalFreeKg() {
            <%--var txtFromScheme = $('#<%=txtFromScheme.ClientID%>').val();
            var txtToScheme = $('#<%=txtToScheme.ClientID%>').val();--%>
            var txtPurchaseKg = $('#<%=txtPurchaseKg.ClientID%>').val();

            var Purchase = 0;
            if (txtFromScheme != '' && txtToScheme != '' && txtPurchaseKg != '') {
                Purchase = Math.ceil(((parseFloat(txtPurchaseKg) * parseFloat(txtToScheme)) / parseFloat(txtFromScheme)));

                $('#<%=txtTotalFreeKg.ClientID%>').val(Purchase);
            }
            else {
                $('#<%=txtTotalFreeKg.ClientID%>').val(0);
            }
        }

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
