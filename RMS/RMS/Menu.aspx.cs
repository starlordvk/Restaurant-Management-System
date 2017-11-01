using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
       
    }


    protected void order_cb_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
        int index = row.RowIndex;
        CheckBox cb = (CheckBox)menu_gv.Rows[index].FindControl("order_cb");
        if(cb.Checked)
        {
            menu_gv.Rows[index].FindControl("order_tb").Visible = true;
        }
        else
        {
            menu_gv.Rows[index].FindControl("order_tb").Visible = false;
        }
    }

    protected void menu_order_button_Click(object sender, EventArgs e)
    {
        //Flag to check if order is successful or not
        int order_successful_flag = 1;
        //List to keep tab of dishes ordered and their quantity
        List<string> dish_id_list = new List<String>();
        List<int> dish_quantity_list = new List<int>();
        List<int> dish_price_list = new List<int>();
        //List to keep tab of what items need to be ordered
        List<string> item_id_list = new List<string>();
        List<int> item_quantity_list = new List<int>();
        //Initializing the Confirmation Label Text
        confirmation_label.Text = "";
        //Initializing the total amount for the bill
        int total_amount = 0;
        //Iterating through each row of the gridview
        foreach(GridViewRow row in menu_gv.Rows)
        {
            //Get the checkbox reference in each row
            CheckBox cb = (CheckBox)row.FindControl("order_cb");
            //If checkbox is checked
            if(cb.Checked)
            {
                //Get the corresponding textbox reference
                TextBox tb = (TextBox)row.FindControl("order_tb");
                //Get the quantity entered in the textbox
                int quantity;
                int.TryParse(tb.Text, out quantity);
                //Adding to the total amount
                total_amount += quantity * int.Parse(row.Cells[2].Text);
                //Get the corresponding price of the dish
                dish_price_list.Add(int.Parse(row.Cells[2].Text));
                //Adding the dish_id and the dish_quantity
                dish_id_list.Add(row.Cells[0].Text);
                dish_quantity_list.Add(quantity);
                //Creating the database connection
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT ItemCode, Quantity FROM Dish_Items WHERE DishId = @DishId", con);
                cmd.Parameters.AddWithValue("@DishId", row.Cells[0].Text);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                try
                {
                    using (con)
                    {
                        con.Open();
                        ad.Fill(ds, "Dish_Items");
                    }
                }
                catch(Exception ex)
                {
                    error_label_1.Text = "Error Label 1: " + ex.Message;
                }
                //Decrementing the number of used items from the inventory
                foreach(DataRow row1 in ds.Tables["Dish_Items"].Rows)
                {
                    string item_code = row1["ItemCode"].ToString();
                    int new_quantity;
                    int.TryParse(row1["Quantity"].ToString(), out new_quantity);

                    using (SqlCommand cmd1 = new SqlCommand("SELECT Quantity FROM Items WHERE ItemCode = @ItemCode", con))
                    {
                        cmd1.Parameters.AddWithValue("@ItemCode", item_code);

                        con.ConnectionString = ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString;
                        try
                        {
                            using (con)
                            {
                                con.Open();
                                using (SqlDataReader reader = cmd1.ExecuteReader())
                                {
                                    reader.Read();
                                    int old_quantity;
                                    int.TryParse(reader["Quantity"].ToString(), out old_quantity);
                                    //If available quantity is more than the required quantity
                                    if ((new_quantity * quantity) <= old_quantity)
                                    {
                                        int revised_quantity = old_quantity - (new_quantity * quantity);

                                        using (SqlCommand cmd2 = new SqlCommand("UPDATE Items SET Quantity = @Quantity WHERE ItemCode = @ItemCode1", con))
                                        {
                                            cmd2.Parameters.AddWithValue("@Quantity", revised_quantity);
                                            cmd2.Parameters.AddWithValue("@ItemCode1", item_code);
                                            cmd2.ExecuteNonQuery();
                                        }
                                    }
                                    //If available quantity is less than the required quantity
                                    else if((new_quantity * quantity) > old_quantity)
                                    {
                                        order_successful_flag = 0;
                                        item_id_list.Add(item_code);
                                        item_quantity_list.Add((new_quantity * quantity) - old_quantity);
                                        using (SqlCommand cmd3 = new SqlCommand("SELECT Name FROM Items WHERE ItemCode = @ItemCode", con))
                                        {
                                            cmd3.Parameters.AddWithValue("@ItemCode", item_code);
                                            using (SqlDataReader reader2 = cmd3.ExecuteReader())
                                            {
                                                reader2.Read();
                                                confirmation_label.Text += "Not much " + reader2["Name"].ToString() + " left" + "<br/>";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            error_label_2.Text = "Error Label 2: " + ex.Message;
                        }
                    }
                }
                //Clearing the command object
                cmd.Dispose();
            }
        }
        //Confirmation of Order Placed
        if (order_successful_flag == 1)
        {
            confirmation_label.Text = "<b>Order Placed</b>";

            string bill_id = 'b' + RandomDigits(4);
            DateTime bill_date = DateTime.Now;

            //Enter the details into the Bills table
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString);
            SqlCommand cmd4 = new SqlCommand("INSERT INTO Bills VALUES(@bill_id, @bill_date, @bill_amt)", con1);
            cmd4.Parameters.AddWithValue("bill_id", bill_id);
            cmd4.Parameters.AddWithValue("@bill_date", bill_date);
            cmd4.Parameters.AddWithValue("@bill_amt", total_amount);

            try
            {
                using (con1)
                {
                    con1.Open();
                    cmd4.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                error_label_2.Text = "Inserting into Bills:" + ex.Message;
            }

            //Enter the details into the Bills_Dish table
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString);
            SqlCommand cmd5 = new SqlCommand("INSERT INTO Bills_Dish VALUES(@bill_id, @dish_id, @quantity, @amt)", con2);
            try
            {
                using (con2)
                {
                    con2.Open();
                    for (int x = 0; x < dish_id_list.Count; x++)
                    {
                        cmd5.Parameters.AddWithValue("@bill_id", bill_id);
                        cmd5.Parameters.AddWithValue("@dish_id", dish_id_list[x]);
                        cmd5.Parameters.AddWithValue("@quantity", dish_quantity_list[x]);
                        cmd5.Parameters.AddWithValue("@amt", dish_quantity_list[x] * dish_price_list[x]);
                        cmd5.ExecuteNonQuery();
                        cmd5.Parameters.Clear();
                    }
                }
            }
            catch(Exception ex)
            {
                error_label_2.Text = "Inserting into Bills_Dish: " + ex.Message;
            }
        }
        else if(order_successful_flag == 0)
        {
            List<int> item_price_list = new List<int>();
            string order_id = "o" + RandomDigits(4);

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT Price FROM Items WHERE ItemCode = @ItemCode", con))
                    {
                        foreach(string item_id in item_id_list)
                        {
                            cmd.Parameters.AddWithValue("@ItemCode", item_id);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                reader.Read();
                                item_price_list.Add(int.Parse(reader["Price"].ToString()));
                            }
                            cmd.Parameters.Clear();
                        }
                    }
                }
                catch(Exception ex)
                {
                    error_label_2.Text = ex.Message;
                }
            }

            int total_order_amount = 0;
            for(int i = 0; i < item_id_list.Count; i++)
            {
                total_order_amount += item_quantity_list[i] * item_price_list[i];
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO [Order] VALUES(@OrderId, @Date, @Amount, @Complete)", con))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", order_id);
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Amount", total_order_amount);
                        cmd.Parameters.AddWithValue("@Complete", 0);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch(Exception ex)
                {
                    error_label_2.Text = ex.Message;
                }
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Order_Items VALUES(@OrderId, @ItemCode, @Quantity)", con))
                    {
                        for(int i = 0; i < item_id_list.Count; i++)
                        {
                            cmd.Parameters.AddWithValue("@OrderId", order_id);
                            cmd.Parameters.AddWithValue("@ItemCode", item_id_list[i]);
                            cmd.Parameters.AddWithValue("@Quantity", item_quantity_list[i]);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                    }
                }
                catch(Exception ex)
                {
                    error_label_2.Text = ex.Message;
                }
            }
            confirmation_label.Text += "<b>Orders have been placed for these items</b>";
        }
    }

    //Generating random BillId
    public string RandomDigits(int length)
    {
        var random = new Random();
        string s = string.Empty;
        for (int i = 0; i < length; i++)
            s = String.Concat(s, random.Next(10).ToString());
        return s;
    }
}