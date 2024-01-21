<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Addstate.aspx.cs" Inherits="OrderApp.Addstate" 
    MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>Add Territory</h3>
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
                        <div class="col-md-2" style="display:none">
                            <label>Country Name</label>
                        </div>
                        <div class="col-md-4" style="display:none">
                            <asp:DropDownList ID="drpCountryName" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                           
                        </div>
                        <div class="col-md-2">
                            <label>Territory Name</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtstatename" CssClass="form-control" MaxLength="30" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqstatename" runat="server" ControlToValidate="txtstatename" ErrorMessage="Plese Enter State." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                            <asp:HiddenField ID="hdstateid" runat="server" />
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
