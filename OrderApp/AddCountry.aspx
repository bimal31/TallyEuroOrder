<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCountry.aspx.cs" Inherits="OrderApp.AddCountry"
    MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>Add Country</h3>
            </div>
            <div class="form-group pull-right">
                <asp:Button ID="btnback" runat="server" Text="Back To List" CssClass="btn btn-info btn-rounded" OnClick="btnback_Click" />
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
                            <label>Country Name</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCountryName" CssClass="form-control" runat="server" MaxLength="30" ></asp:TextBox>
                            <asp:HiddenField ID="hdCountryId" runat="server" />
                            <asp:RequiredFieldValidator ID="reqDealerCode" runat="server" ControlToValidate="txtCountryName" ErrorMessage="Plese Enter Country Name." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="panel-footer">
            <div class="form-group pull-right">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-success" ValidationGroup="RequireValidation" CausesValidation="true" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-dark" />
            </div>
        </div>
    </div>
</asp:Content>
