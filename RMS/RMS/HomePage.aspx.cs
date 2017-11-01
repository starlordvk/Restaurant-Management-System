using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int total_items_ordered = 0;
        int total_dishes_ordered = 0;
        int total_spendings = 0;
        int total_earnings = 0;

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
        {
            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT SUM(Quantity) AS Total FROM Order_Items", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        total_items_ordered = int.Parse(reader["Total"].ToString());
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT SUM(Quantity) AS Total FROM Bills_Dish", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        total_dishes_ordered = int.Parse(reader["Total"].ToString());
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT SUM(Amount) AS Total FROM [Order]", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        total_spendings = int.Parse(reader["Total"].ToString());
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT SUM(BillTotal) AS Total FROM Bills", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        total_earnings = int.Parse(reader["Total"].ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                error_label.Text = ex.Message;
            }
        }

        total_dishes_ordered_label.Text = "Total Dishes Ordered: " + total_dishes_ordered.ToString();
        total_items_ordered_label.Text = "Total Items Ordered: " + total_items_ordered.ToString();
        total_earnings_label.Text = "Total Earnings: " + total_earnings.ToString();
        total_spendings_label.Text = "Total Spendings: " + total_spendings.ToString(); 
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["theme"] != null)
        {
            Page.Theme = Session["theme"].ToString();
        }
    }

}