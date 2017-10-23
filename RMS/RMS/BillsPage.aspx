<%@ Page Title="" Language="C#" MasterPageFile="~/RMSMaster.master" AutoEventWireup="true" Inherits="BillsPage" Codebehind="BillsPage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Welcome to Bills page"></asp:Label>
     <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ConnectionStrings:Restaurant %>" SelectCommand="Select * from [Order]"></asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataSourceID="SqlDataSource1" AllowPaging="true" EnablePersistedSelection="true" DataKeyNames="OrderId" AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="OrderId" HeaderText="Id" />
            <asp:BoundField DataField="Date" HeaderText="Date" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" />
            <asp:CommandField ShowSelectButton="true" SelectText="Expand" />
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ConnectionStrings:Restaurant %>" SelectCommand="Select ItemCode, Quantity from Order_Items where OrderId=@OrderId" OnSelecting="SqlDataSource2_Selecting" OnSelected="SqlDataSource2_Selected" >
        <SelectParameters>
            <asp:ControlParameter ControlID="GridView1" Name="OrderId" PropertyName="SelectedDataKey.Value" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="GridView2" runat="server" DataSourceID="SqlDataSource2" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
        </Columns>
    </asp:GridView>

</asp:Content>

