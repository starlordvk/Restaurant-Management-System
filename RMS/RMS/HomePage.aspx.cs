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

        DataTable table = new DataTable();
        table.Columns.Add("Statistics", typeof(string));
        table.Columns.Add("Numbers", typeof(int));

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
        {
            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT SUM(Quantity) AS Total FROM Order_Items", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataRow row = table.NewRow();

                        reader.Read();
                        total_items_ordered = int.Parse(reader["Total"].ToString());

                        row["Statistics"] = "Total Items Ordered";
                        row["Numbers"] = total_items_ordered;
                        table.Rows.Add(row);
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT SUM(Quantity) AS Total FROM Bills_Dish", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataRow row = table.NewRow();

                        reader.Read();
                        total_dishes_ordered = int.Parse(reader["Total"].ToString());

                        row["Statistics"] = "Total Dishes Ordered";
                        row["Numbers"] = total_dishes_ordered;
                        table.Rows.Add(row);
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT SUM(Amount) AS Total FROM [Order]", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataRow row = table.NewRow();

                        reader.Read();
                        total_spendings = int.Parse(reader["Total"].ToString());

                        row["Statistics"] = "Total Spendings";
                        row["Numbers"] = total_spendings;
                        table.Rows.Add(row);
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT SUM(BillTotal) AS Total FROM Bills", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataRow row = table.NewRow();

                        reader.Read();
                        total_earnings = int.Parse(reader["Total"].ToString());

                        row["Statistics"] = "Total Earnings";
                        row["Numbers"] = total_earnings;
                        table.Rows.Add(row);
                    }
                }
            }
            catch(Exception ex)
            {
                error_label.Text = ex.Message;
            }
        }

        statistics_gv.DataSource = table;
        statistics_gv.DataBind();
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["theme"] != null)
        {
            Page.Theme = Session["theme"].ToString();
        }
    }

}