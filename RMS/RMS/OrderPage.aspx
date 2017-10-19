<%@ Page Title="" Language="C#" MasterPageFile="~/RMSMaster.master" AutoEventWireup="true" CodeFile="OrderPage.aspx.cs" Inherits="OrderPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Welcome to order page"></asp:Label>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ProviderName="System.Data.SqlClient" SelectCommand="Select * from Items"
        ConnectionString="<%$ConnectionStrings:Restaurant %>"></asp:SqlDataSource>

    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" DataKeyNames="ItemCode" EnablePersistedSelection="true" 
        AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" >
        <Columns>
            <asp:BoundField DataField="ItemCode" HeaderText="Code" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Price" HeaderText="Price" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
            <asp:TemplateField HeaderText="Select" >
                <ItemTemplate>
                    <asp:CheckBox ID="SelectItem" runat="server" AutoPostBack="true" OnCheckedChanged="SelectItem_CheckedChanged"/>
                    <asp:TextBox ID="SelectedItemQuantity" runat="server" Visible="false" Width="30px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:Button ID="PlaceOrderButton" runat="server" Text="Place Order" OnClick="PlaceOrderButton_Click" />
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    
</asp:Content>

