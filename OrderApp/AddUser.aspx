<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="OrderApp.AddUser" MasterPageFile="~/MainMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
           <div class="form-group pull-left">
                <h3>Add User </h3>
            </div>
          <div class="form-group pull-right">
                <asp:Button ID="btnback" runat="server" Text="Back To List"  CssClass="btn btn-info btn-rounded" OnClick="btnback_Click" />
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
                            <label>First Name</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Middle Name</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtMiddleName" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>Last Name</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>User Type</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlUserType1" runat="server" CssClass="form-control">
                                <asp:ListItem Value="Admin" Text="Admin"></asp:ListItem>
                                <asp:ListItem Value="Clerk" Text="Clerk"></asp:ListItem>
                                <asp:ListItem Value="Salesman" Text="Salesman"></asp:ListItem>
                                <asp:ListItem Value="Factory" Text="Factory"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>Phone No</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtPhoneNo" MaxLength="22" CssClass="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Mobile No</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtMobileNo" MaxLength="22" CssClass="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>User Name</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="Plese Enter User Name." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-2">
                            <label>Password</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" runat="server" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Plese Enter Password." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>Confirm Password</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtConfirmPassword" CssClass="form-control" TextMode="Password" runat="server" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Plese Enter Confirm Password." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="compConfirmPassword" runat="server" ControlToValidate="txtPassword" ControlToCompare="txtConfirmPassword" ErrorMessage="Password and Confirm Password should same." ValidationGroup="RequireValidation"></asp:CompareValidator>
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

    <asp:HiddenField ID="hdUserId" runat="server" />

    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;            
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 40 && charCode != 41 && charCode != 45) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<%--</form>
</body>
</html>--%>
