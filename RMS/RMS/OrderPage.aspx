<%@ Page Title="" Language="C#" MasterPageFile="~/RMSMaster.master" AutoEventWireup="true" Inherits="OrderPage" Codebehind="OrderPage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Welcome to order page"></asp:Label>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ProviderName="System.Data.SqlClient" SelectCommand="Select * from Items" 
        UpdateCommand="Update Items set Price=@Price where ItemCode=@ItemCode"
        ConnectionString="<%$ConnectionStrings:Restaurant %>"></asp:SqlDataSource>

    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" DataKeyNames="ItemCode" EnablePersistedSelection="true" 
        AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" >
        <Columns>
            <asp:BoundField DataField="ItemCode" HeaderText="Code" ReadOnly="true" />
            <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="true" />
            <asp:BoundField DataField="Price" HeaderText="Price" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="true" />
            
            <asp:TemplateField HeaderText="Select" >
                <ItemTemplate>
                    <asp:CheckBox ID="SelectItem" runat="server" AutoPostBack="true" OnCheckedChanged="SelectItem_CheckedChanged"/>
                   
                    <asp:TextBox ID="SelectedItemQuantity" runat="server" Visible="false" Width="30px"></asp:TextBox>
                </ItemTemplate>
                
            </asp:TemplateField>

            <asp:CommandField ShowEditButton="true" EditText="Change Price" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:Button ID="PlaceOrderButton" runat="server" Text="Place Order" OnClick="PlaceOrderButton_Click" />
    <br />
    <asp:GridView ID="GridView2" runat="server"></asp:GridView>
  
    
</asp:Content>

