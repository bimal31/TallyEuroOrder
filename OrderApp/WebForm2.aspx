<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="OrderApp.WebForm2" MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Script/jquery-1.9.1.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>
                    <asp:Label ID="lblheading" runat="server"></asp:Label>
                </h3>
            </div>
            <div class="form-group pull-right">
                <asp:Button ID="btnback" runat="server" Text="Back To List" CssClass="btn btn-info btn-rounded" />
            </div>
        </div>
        <center>
            <asp:Label ID="lblErrorMessage"  ForeColor="Red"  runat="server"></asp:Label>
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
                            <asp:Label ID="lbldealercode" runat="server" Text="Dealer Code :"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtDealerCodeSearch" CssClass="form-control" runat="server" AutoPostBack="true" ></asp:TextBox>
                            <a id="lnkdealerInfo" href="#">View Dealer Information</a>
                        </div>
                        <div class="col-md-2">
                            <input type="button" id="btnAddDealer" runat="server" class="btn btn-info btn-rounded" value="Add Dealer" />
                        </div>
                        <div class="col-md-2">
                            <label>Order Date :</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div class="modal fade" id="DealerModal" tabindex="-1" role="dialog" aria-labelledby="ModalTitle"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="ModalTitle"></h4>
                </div>
                <div class="modal-body">

                    <div class="form-group">
                        <div class="row" id="divdealercode">
                            <div class="col-lg-12">
                                <label runat="server">Dealer Code</label>
                                <asp:TextBox ID="txtDealerCode" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hdDealerId" runat="server" />
                                <%--<asp:RequiredFieldValidator ID="reqDealerCode" runat="server" ControlToValidate="txtDealerCode" ErrorMessage="Plese Enter Dealer Code." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <label>Dealer Name</label>
                                <asp:TextBox ID="txtDealerName" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDealerName" ErrorMessage="Plese Enter Dealer Name." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
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
                                <asp:TextBox ID="txtAddress" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>Area</label>
                                <asp:TextBox ID="txtArea" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtArea" ErrorMessage="Plese Enter Area." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
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
                                <asp:TextBox ID="txtPhoneNo" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPhoneNo" ErrorMessage="Plese Enter PhoneNo." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>GST</label>
                                <asp:TextBox ID="txtGST" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtGST" ErrorMessage="Plese Enter GST." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                            </div>
                        </div>


                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveDealer" Text="Save" runat="server" Class="btn btn-primary" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">

        $('#lnkdealerInfo').click(function () {
          
                $('#divdealercode').css("display", "block");
                $('#ModalTitle').text('View Dealer');
                $('#ContentPlaceHolder1_btnSaveDealer').hide()
                $('#ContentPlaceHolder1_txtDealerCode').attr('readonly', true);
                $('#ContentPlaceHolder1_txtDealerName').attr('readonly', true);
                $('#ContentPlaceHolder1_txtAddress').attr('readonly', true);
                $('#ContentPlaceHolder1_txtArea').attr('readonly', true);
                $('#ContentPlaceHolder1_txtPhoneNo').attr('readonly', true);
                $('#ContentPlaceHolder1_txtGST').attr('readonly', true);
                $('#ContentPlaceHolder1_txtTransport').attr('readonly', true);

                $('#ContentPlaceHolder1_txtContactName').attr('readonly', true);
                $('#ContentPlaceHolder1_txtpincode').attr('readonly', true);

                $('#DealerModal').modal('show');
            

        });
    </script>
</asp:Content>
