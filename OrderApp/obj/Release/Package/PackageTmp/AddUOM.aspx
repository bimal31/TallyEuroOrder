﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUOM.aspx.cs" Inherits="OrderApp.AddUOM" MasterPageFile="~/MainMaster.Master" EnableEventValidation="false" ViewStateEncryptionMode="Never" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>Add UOM</h3>
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
                            <label>UOM Name</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtUOMName" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:HiddenField ID="hdUOMId" runat="server" />
                            <asp:RequiredFieldValidator ID="reqDealerCode" runat="server" ControlToValidate="txtUOMName" ErrorMessage="Plese Enter UOM Name." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                        </div>



                        <div class="col-md-2">
                            <label>UOM Description</label>
                        </div>
                         <div class="col-md-4">
                            <asp:TextBox ID="txtUOMDescription" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
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
