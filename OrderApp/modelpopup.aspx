<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nmodelpopup.aspx.cs" Inherits="OrderApp.modelpopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="Script/jquery-1.9.1.js"></script>
   <%-- <script src="assets/plugins/bootstrap/js/bootstrap.js"></script> 
    <link href="assets/plugins/bootstrap/css/bootstrap.css" rel="stylesheet" />--%>
    
    
    <script src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    
</head>
<body>
  

    <form class='cntt-wrapper' id="form1" runat="server">

          <input type="button" id="btnShowLogin" class="btn btn-primary" value="Login" />
        <div class="modal fade" id="LoginModal" tabindex="-1" role="dialog" aria-labelledby="ModalTitle"
            aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title" id="ModalTitle">Login</h4>
                    </div>
                    <div class="modal-body">
                        <label for="txtUsername">
                            Username</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter Username"
                            required />
                        <br />
                        <label for="txtPassword">
                            Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"
                            placeholder="Enter Password" required />
                        <div class="checkbox">
                            <asp:CheckBox ID="chkRememberMe" Text="Remember Me" runat="server" />
                        </div>
                        <div id="dvMessage" runat="server" visible="false" class="alert alert-danger">
                            <strong>Error!</strong>
                            <asp:Label ID="lblMessage" runat="server" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnLogin" Text="Login" runat="server" OnClick="ValidateUser" Class="btn btn-primary" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
    </form>

</body>
</html>


<script type="text/javascript">
    $(function () {
        $("#btnShowLogin").click(function () {
            $('#LoginModal').modal('show');
        });
    });
</script>


