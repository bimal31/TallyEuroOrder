<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="OrderApp.AddProduct" MasterPageFile="~/MainMaster.Master" enableeventvalidation="false" viewstateencryptionmode="Never" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default">
         <div class="panel-heading page-titles">
           <div class="form-group pull-left">
                <h3>Add Product</h3>
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
                            <label>Product Name</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtProductName" CssClass="form-control" runat="server"  MaxLength="30" ></asp:TextBox>
                            <asp:HiddenField ID="hdProductId" runat="server" />
                            <asp:RequiredFieldValidator ID="reqDealerCode" runat="server" ControlToValidate="txtProductName" ErrorMessage="Plese Enter Product Name." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-2">
                            <label>Product Description</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtProductDesc" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>      
            
              <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>From Scheme</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtFromScheme" CssClass="form-control" runat="server"  MaxLength="4" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromScheme" ErrorMessage="Plese Enter Product From shceme." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-2">
                            <label>To Scheme</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtToScheme"  MaxLength="4" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToScheme" ErrorMessage="Plese Enter Product to shceme." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
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
