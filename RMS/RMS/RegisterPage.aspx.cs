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
    public partial class RegisterPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void register_button_Click(object sender, EventArgs e)
        {
            int flag = 0;
            string temp_userid = userid_tb.Text;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString);

            try
            {
                using (con)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT UserId FROM Users", con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (temp_userid == reader["UserId"].ToString())
                                {
                                    flag = 1;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                error_label.Text = ex.Message;
            }

            if (flag == 0)
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString;
                try
                {
                    using (con)
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Users Values(@UserId, @Password, @FirstName, @LastName)", con))
                        {
                            cmd.Parameters.AddWithValue("@UserId", userid_tb.Text);
                            cmd.Parameters.AddWithValue("@Password", password_tb.Text);
                            cmd.Parameters.AddWithValue("@FirstName", firstname_tb.Text);
                            cmd.Parameters.AddWithValue("@LastName", lastname_tb.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    error_label.Text = ex.Message;
                }
                error_label.Text = "User successfully registered";

                Response.Redirect("LoginPage.aspx");
            }
            else if(flag == 1)
            {
                error_label.Text = "This UserId already exists, try another one";
            }
        }
    }
}