<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="RMS.RegisterPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Firstname: <asp:TextBox runat="server" ID="firstname_tb"></asp:TextBox>
            <br /><br />
            Lastname: <asp:TextBox runat="server" ID="lastname_tb"></asp:TextBox>
            <br /><br />
            UserId: <asp:TextBox runat="server" ID="userid_tb"></asp:TextBox>
            <br /><br />
            Password: <asp:TextBox runat="server" ID="password_tb" TextMode="Password"></asp:TextBox>
            <br /><br />
            Confirm Password: <asp:TextBox runat="server" ID="confirm_password_tb" TextMode="Password"></asp:TextBox>
            <br /><br />
            <asp:Button ID="register_button" runat="server" Text="Register" OnClick="register_button_Click"/>
            <br /><br />
            <asp:Label ID="error_label" runat="server"></asp:Label>

            <asp:RequiredFieldValidator ID="rfv_fn" runat="server" ControlToValidate="firstname_tb" ErrorMessage="Firstname is mandatory" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfv_ln" runat="server" ControlToValidate="lastname_tb" ErrorMessage="Lastname is mandatory" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfv_uid" runat="server" ControlToValidate="userid_tb" ErrorMessage="UserId is mandatory" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfv_pw" runat="server" ControlToValidate="password_tb" ErrorMessage="Password is mandatory" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfv_cpw" runat="server" ControlToValidate="confirm_password_tb" ErrorMessage="Please confirm password" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>

            <asp:CompareValidator ID="cv_cpw" runat="server" ControlToValidate="confirm_password_tb" ControlToCompare="password_tb" ErrorMessage="Password does not match" ForeColor="Red" Display="None"></asp:CompareValidator>

            <asp:ValidationSummary ID="vs_register" runat="server" ShowSummary="true" /> 
        </div>
    </form>
</body>
</html>
