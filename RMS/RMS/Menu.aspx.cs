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
        //Initializing the Confirmation Label Text
        confirmation_label.Text = "";
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
        }
    }
}