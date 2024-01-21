<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDealer.aspx.cs" Inherits="OrderApp.AddDealer" MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>Add Dealer</h3>
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
                        <div class="col-md-2">
                            <label>Dealer Code</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDealerCode" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:HiddenField ID="hdDealerId" runat="server" />

                        </div>
                        <div class="col-md-2">
                            <label>Dealer Name</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDealerName" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqDealerName" runat="server" ControlToValidate="txtDealerName" ForeColor="Red" ErrorMessage="Plese Enter Dealer Name." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>Contact Name</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtcontactname" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Phone No</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtPhoneNo" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqPhoneNo" runat="server" ControlToValidate="txtPhoneNo" ForeColor="Red" ErrorMessage="Plese Enter PhoneNo." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">

                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-4">
                                    <label>Address</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtAddress" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqtAddress" runat="server" ControlToValidate="txtAddress" ForeColor="Red" ErrorMessage="Plese Enter address." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-4">
                                    <label>Territory</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="drpStateName" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-md-4">
                                    <label>Area</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtArea" CssClass="form-control" runat="server"></asp:TextBox>

                                </div>
                            </div>
                        </div>

                    </div>




                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">

                        <div class="col-md-2">
                            <label>PinCode</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtpincode" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqpincode" runat="server" ControlToValidate="txtpincode" ForeColor="Red" ErrorMessage="Plese Enter pin code." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>

                        </div>

                        <div class="col-md-2">
                            <label>GST</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtGST" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                    </div>
                </div>
            </div>


            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>GST Photo</label>
                        </div>

                        <div class="col-md-4">
                            <asp:FileUpload ID="FileUploadGST" runat="server" onchange="previewGSTPhoto();" />
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionGSTPhoto" runat="server"
                                ValidationExpression=""
                                ErrorMessage="" 
                                ControlToValidate="FileUploadGST"
                                ValidationGroup="RequireValidation"></asp:RegularExpressionValidator>--%>

                            <asp:Image runat="server" ID="imgGSTPhoto" CssClass="img-responsive" Style="margin-top: 15px;"
                                ImageUrl="~/DealerImage/no-picture-available.png" />
                        </div>

                        <div class="col-md-2">
                            <label>Visit Card Photo</label>
                        </div>

                        <div class="col-md-4">
                            <asp:FileUpload ID="FileUploadVisitCard" runat="server" onchange="previewVisitCard();" />
                            <asp:Image runat="server" ID="imgVisitCard" CssClass="img-responsive" Style="margin-top: 15px;"
                                ImageUrl="~/DealerImage/no-picture-available.png" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel-footer">
            <div class="form-group pull-right">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-success"
                    ValidationGroup="RequireValidation" CausesValidation="true" />

                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-dark" />
            </div>
        </div>
    </div>

    <input type="hidden" id="hdnGSTPhoto" runat="server" />
    <input type="hidden" id="hdnVisitCard" runat="server" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function previewGSTPhoto() {
            var preview = document.querySelector('#<%=imgGSTPhoto.ClientID %>');
            var file = document.querySelector('#<%=FileUploadGST.ClientID %>').files[0];

            var reader = new FileReader();
            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file)
                reader.readAsDataURL(file);
            else
                preview.src = "";
        }

        function previewVisitCard() {
            var preview = document.querySelector('#<%=imgVisitCard.ClientID %>');
            var file = document.querySelector('#<%=FileUploadVisitCard.ClientID %>').files[0];

            var reader = new FileReader();
            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file)
                reader.readAsDataURL(file);
            else
                preview.src = "";
        }
    </script>
</asp:Content>
