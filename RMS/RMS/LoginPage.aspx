<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="LoginPage.aspx.cs" Inherits="RMS.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/StyleSheets/LoginPageStyleSheet.css" rel="stylesheet" type="text/css"  />
</head>
<body>
    <form id="form1" runat="server" class="LoginClass">
        <div>
            <!--Label for Administrator Login -->
            <asp:Label runat="server" Text="Adminstrator Login"></asp:Label>
            <br /><br />
            <!-- Label and TextBox for Username -->
            <asp:Label runat="server" Text="Username: "></asp:Label>
            <asp:TextBox runat="server" ID="username_tb"></asp:TextBox>
            <br /><br />
            <!-- Label and TextBox for Password-->
            <asp:Label runat="server" Text="Password: "></asp:Label>
            <asp:TextBox runat="server" ID="password_tb" TextMode="Password"></asp:TextBox>
            <br /><br />
            <!-- Login Button -->
            <asp:Button runat="server" ID="login_button" Text="Login" OnClick="ShowHomePage" style="padding-right: 20px"/>
            <asp:Button runat="server" ID="register_user_button" Text="Register User" OnClick="register_user_button_Click" CausesValidation="false"/>
            <br /><br />
            <asp:Label runat="server" ID="error_label"></asp:Label>
            <!-- Validators for ensuring the Username and Password Fields are not empty -->
            <asp:RequiredFieldValidator runat="server" ID="rfv_username" ControlToValidate="username_tb" ForeColor="Red" ErrorMessage="Username is mandatory" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator runat="server" ID="rfv_password" ControlToValidate="password_tb" ForeColor="Red" ErrorMessage="Password is mandatory" Display="None"></asp:RequiredFieldValidator>
      
            <asp:ValidationSummary runat="server" ShowSummary="true" />
        </div>
    </form>
</body>
</html>
