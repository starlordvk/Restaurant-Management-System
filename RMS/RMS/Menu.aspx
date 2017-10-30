<%@ Page Title="" Language="C#" MasterPageFile="~/RMSMaster.master" AutoEventWireup="true" Inherits="_Default" Codebehind="Menu.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="menu_gv" runat="server" AutoGenerateColumns="false" AllowSorting="true" AllowPaging="true" DataSourceID="MenuSqlDataSource" DataKeyNames="DishId">
        <Columns>
            <asp:BoundField DataField="DishId" HeaderText="DishId" ReadOnly="true"/>
            <asp:BoundField DataField="DishName" HeaderText="Name" ReadOnly="true"/>
            <asp:BoundField DataField="Price" HeaderText="Price"/>
            <asp:TemplateField HeaderText="Order">
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="order_cb" OnCheckedChanged="order_cb_CheckedChanged" AutoPostBack="true" />
                    <asp:TextBox runat="server" ID="order_tb" Width="30px" Visible="false"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please enter a valid quantity" ControlToValidate="order_tb" MaximumValue="10" MinimumValue="1" Type="Integer" ForeColor="Red"></asp:RangeValidator>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="true" EditText="Change Price" />
        </Columns>
    </asp:GridView>

    <br />
    <asp:Button runat="server" ID="menu_order_button" Text="Place Order" OnClick="menu_order_button_Click" />
    <br />
    <asp:Label runat="server" ID="confirmation_label"></asp:Label>
    <br />
    <asp:Label runat="server" ID="error_label_1"></asp:Label>
    <br />
    <asp:Label runat="server" ID="error_label_2"></asp:Label>

    <asp:SqlDataSource ID="MenuSqlDataSource" runat="server" SelectCommand="SELECT * from Dishes" ConnectionString="<%$ConnectionStrings:Restaurant %>" UpdateCommand="UPDATE Dishes SET Price = @Price WHERE DishId = @DishId">
        <UpdateParameters>
            <asp:ControlParameter ControlID="menu_gv" Name="Price" />
            <asp:ControlParameter ControlID="menu_gv" Name="DishId" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
