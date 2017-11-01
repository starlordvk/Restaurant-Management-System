using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RMS
{
    public partial class Receipts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            receipts_dishes_gv.Visible = false;
            
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
           
        }


        protected void select_receipt_cb_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((CheckBox)sender).NamingContainer;
            CheckBox cb = (CheckBox)row.FindControl("select_receipt_cb");

            if(cb.Checked)
            {
                receipts_dishes_gv.Visible = true;
                try
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT DishId, Quantity, Amount FROM Bills_Dish WHERE BillId = @BillId", con))
                        {
                            cmd.Parameters.AddWithValue("@BillId", row.Cells[0].Text);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                DataTable table = new DataTable();
                                table.Load(reader);
                                receipts_dishes_gv.DataSource = table;
                                receipts_dishes_gv.DataBind();
                            }
                        }
                        
                    }
                }
                catch(Exception ex)
                {
                    error_label.Text = ex.Message;
                }
            }
            else
            {
                receipts_dishes_gv.Visible = false;
            }
        }
    }
}