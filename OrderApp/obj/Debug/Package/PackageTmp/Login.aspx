<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OrderApp.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link rel="stylesheet" href="Style/Login.css" />
    <script src="../assets/plugins/jquery/jquery.min.js"></script>
    <script src="Script/Login.js" type="text/javascript"></script>
</head>
<body>

    <hgroup>
        <h1>Login</h1>       
         <center>
            <asp:Label ID="lblErrorMessage"  ForeColor="Red"   runat="server"></asp:Label>
         </center>
    </hgroup>
    <form runat="server">
        <div class="group">
            <input type="text" id="txtUserName"  runat="server" maxlength="20"  /><span class="highlight"></span><span class="bar"></span>
            <label>User Name</label>
        </div>
        <div class="group">
            <input type="password" id="txtPassword"  runat="server" maxlength="20" /><span class="highlight"></span><span class="bar"></span>
            <label>Password</label>
        </div>
        <button type="button" class="button buttonBlue" id="btnSubmit" runat="server" onserverclick="btnSubmit_Click">Login</button>
    </form>
    <footer>
        
    </footer>

</body>
</html>
