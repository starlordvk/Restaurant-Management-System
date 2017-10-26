<%@ Page Title="" Language="C#" MasterPageFile="~/RMSMaster.master" AutoEventWireup="true" CodeBehind="Receipts.aspx.cs" Inherits="RMS.Receipts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ConnectionStrings:Restaurant %>" SelectCommand="SELECT * FROM Bills"></asp:SqlDataSource>
    <asp:GridView ID="receipts_gv" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="false" PageSize="5" AllowPaging="true" AllowSorting="true" DataKeyNames="BillId">
        <Columns>
            <asp:BoundField DataField="BillId" HeaderText="Bill_Id" />
            <asp:BoundField DataField="BillDate" HeaderText="Date" />
            <asp:BoundField DataField="BillTotal" HeaderText="Total Amount" />
            <asp:TemplateField HeaderText="View Details">
                <ItemTemplate>
                    <asp:CheckBox ID="select_receipt_cb" runat="server" AutoPostBack="true" OnCheckedChanged="select_receipt_cb_CheckedChanged" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br /><br />
    <asp:GridView ID="receipts_dishes_gv" runat="server" AutoGenerateColumns="false" PageSize="5" AllowPaging="true">
        <Columns>
            <asp:BoundField DataField="DishId" HeaderText="Dish_Id" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
            <asp:BoundField DataField="Amount" HeaderText="Price" />
        </Columns>
    </asp:GridView> 

    <br /><br />
    <asp:Label ID="error_label" runat="server"></asp:Label>
</asp:Content>
