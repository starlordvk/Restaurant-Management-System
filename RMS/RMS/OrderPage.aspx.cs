using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;

public partial class OrderPage : System.Web.UI.Page
{
    Dictionary<string, int> itemOrdered=new Dictionary<string, int>();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["theme"] != null)
        {
            Page.Theme = Session["theme"].ToString();
        }
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
        List<string> item_id_list = new List<string>();
        List<int> item_quantity_list = new List<int>();
        List<int> item_existing_quantity_list = new List<int>();

        int orderAmount = 0;
        foreach(GridViewRow row in GridView1.Rows)
        {
            CheckBox cb = (CheckBox)row.FindControl("SelectItem");
            
            if (cb.Checked)
            {
                
                TextBox tb = (TextBox)row.FindControl("SelectedItemQuantity");
                int quantity;
                int.TryParse(tb.Text, out quantity);

                item_id_list.Add(row.Cells[0].Text);
                item_quantity_list.Add(quantity);
                
                //Adding Itemcode and quantity
                itemOrdered.Add(row.Cells[0].Text, quantity);
                int price;
                int.TryParse(row.Cells[2].Text, out price);
                orderAmount += quantity * price;

            }
        }
        GridView2.DataSource = itemOrdered;
        this.DataBind();

        //Generate random order Id
        string OrderId = 'o'+RandomDigits(4);
        DateTime dt = DateTime.Today;
        int Complete = 1;

        //Connection object
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString);
        
        //Command object for insertion in order table for a newly generated id
        SqlCommand cmd= new SqlCommand("Insert into [Order] (OrderId, Date, Amount, Complete) values(@Id, @date, @amount, @complete)",con);
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

       //Inserting Items wrt and order in the order_items table
        foreach (KeyValuePair<string,int> item in itemOrdered)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString);

            cmd = new SqlCommand("Insert into [Order_Items] (OrderId, ItemCode, Quantity) values (@OrderId, @ItemCode, @Quantity)", conn);
            cmd.Parameters.AddWithValue("@OrderId", OrderId);
            cmd.Parameters.AddWithValue("@ItemCode", item.Key);
            cmd.Parameters.AddWithValue("@Quantity", item.Value);
            try
            {
                using (conn)
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }


        }

        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
        {
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT Quantity FROM Items WHERE ItemCode = @ItemCode", connection))
                {
                    for(int i = 0; i < item_id_list.Count; i++)
                    {
                        command.Parameters.AddWithValue("@ItemCode", item_id_list[i]);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            item_existing_quantity_list.Add(int.Parse(reader["Quantity"].ToString()));
                        }
                        command.Parameters.Clear();
                    }
                }
            }
            catch(Exception ex)
            {
                error_label.Text = ex.Message;
            }
        }

        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
        {
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Items SET Quantity = @Quantity WHERE ItemCode = @ItemCode", connection))
                {
                    for (int i = 0; i < item_id_list.Count; i++)
                    {
                        command.Parameters.AddWithValue("@Quantity",  item_existing_quantity_list[i] + item_quantity_list[i]);
                        command.Parameters.AddWithValue("@ItemCode", item_id_list[i]);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                error_label.Text = ex.Message;
            }
        }

    }
    

    //Generating random OrderId
    public string RandomDigits(int length)
    {
        var random = new Random();
        string s = string.Empty;
        for (int i = 0; i < length; i++)
            s = String.Concat(s, random.Next(10).ToString());
        return s;
    }
}