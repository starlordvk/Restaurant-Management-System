using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class OrderPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SelectItem_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
        int index = row.RowIndex;
        CheckBox cb = (CheckBox)GridView1.Rows[index].FindControl("SelectItem");
        if (cb.Checked)

        {
            GridView1.Rows[index].FindControl("SelectedItemQuantity").Visible = true;
        }
        if(!cb.Checked)
        {

            GridView1.Rows[index].FindControl("SelectedItemQuantity").Visible = false;


        }

    }

   

    protected void PlaceOrderButton_Click(object sender, EventArgs e)
    {
        int orderAmount = 0;
        foreach(GridViewRow row in GridView1.Rows)
        {
            CheckBox cb = (CheckBox)row.FindControl("SelectItem");
            if (cb.Checked)
            {

                TextBox tb = (TextBox)row.FindControl("SelectedItemQuantity");
                int quantity;
                int.TryParse(tb.Text, out quantity);
                int price;
                int.TryParse(row.Cells[2].Text, out price);
                orderAmount += quantity * price;

            }
        }

        //Generate random order Id
        string OrderId = 'o'+RandomDigits(4);
        DateTime dt = DateTime.Today;
        int Complete = 1;

        //Connection object
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString);
        
        //Command object for insertion in order table for a newly generated id
        SqlCommand cmd= new SqlCommand("Insert into Order(OrderId, Date, Amount, Complete) values(@Id, @date, @amount, @complete)",con);
        cmd.Parameters.AddWithValue("@Id", OrderId);
        cmd.Parameters.AddWithValue("@date", dt);
        cmd.Parameters.AddWithValue("@amount", orderAmount);
        cmd.Parameters.AddWithValue("@complete", Complete);

        try
        {
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }



    }

    public string RandomDigits(int length)
    {
        var random = new Random();
        string s = string.Empty;
        for (int i = 0; i < length; i++)
            s = String.Concat(s, random.Next(10).ToString());
        return s;
    }
}