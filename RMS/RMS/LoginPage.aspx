<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="RMS.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- Label and TextBox for Username -->
            <asp:Label runat="server" Text="Username: "></asp:Label>
            <asp:TextBox runat="server" ID="username_tb"></asp:TextBox>
            <br /><br />
            <!-- Label and TextBox for Password-->
            <asp:Label runat="server" Text="Password: "></asp:Label>
            <asp:TextBox runat="server" ID="password_tb" TextMode="Password"></asp:TextBox>
            <br /><br />
            <!-- Login Button -->
            <asp:Button runat="server" ID="login_button" Text="Login" OnClick="ShowHomePage" />
            <br /><br />
            <!-- Validators for ensuring the Username and Password Fields are not empty -->
            <asp:RequiredFieldValidator runat="server" ID="rfv_username" ControlToValidate="username_tb" ForeColor="Red" ErrorMessage="Username is mandatory" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator runat="server" ID="rfv_password" ControlToValidate="password_tb" ForeColor="Red" ErrorMessage="Password is mandatory" Display="None"></asp:RequiredFieldValidator>
            <!-- Validators for ensuring the Username and Password are valid -->
            <asp:CompareValidator runat="server" ID="compv_username" ControlToValidate="username_tb" ValueToCompare="oriental_admin" Operator="Equal" Type="String" ErrorMessage="Invalid Username" ForeColor="Red" Display="None"></asp:CompareValidator>
            <asp:CompareValidator runat="server" ID="compv_password" ControlToValidate="password_tb" ValueToCompare="@dMin123" Operator="Equal" Type="String" ErrorMessage="Invalid Password" ForeColor="Red" Display="None"></asp:CompareValidator>
            <!-- Summary of Validations required -->
            <asp:ValidationSummary runat="server" ShowSummary="true" />
        </div>
    </form>
</body>
</html>
