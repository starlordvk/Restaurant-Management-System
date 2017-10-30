<%@ Page Title="" Language="C#" MasterPageFile="~/RMSMaster.master" AutoEventWireup="true" Inherits="HomePage" Codebehind="HomePage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:label runat="server" text="Available Balance = "></asp:label>
     <br />
    <%--<asp:label runat="server" text="Growth/Profit = "></asp:label>--%>
     <br />
    <asp:label runat="server" text="Miscellaneous = "></asp:label>
     <br />
</asp:Content>

