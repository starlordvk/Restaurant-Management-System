using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RMS
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Function to Handle Button Click
        protected void ShowHomePage(object sender, EventArgs e)
        {
            //Transfers to Home Page if all the Login Fields are valid
            if(Page.IsValid)
            {
                Response.Redirect("HomePage.aspx");
            }
        }

        //Custom Password Validation Function
        /*protected void ValidatePassword(object sender, ServerValidateEventArgs e)
        {
            string password = e.Value;
            if(password.Length >= 6)
            {
                int digit_flag = 0, cap_flag = 0, specchar_flag = 0, validate_flag = 0;
                for(int x = 0; x < password.Length; x++)
                {
                    if(password[x] == '@' || password[x] == '_')
                    {
                        specchar_flag = 1;
                    }
                    if(password[x] >= 'A' && password[x] <= 'Z')
                    {
                        cap_flag = 1;
                    }
                    if(password[x] >= '0' && password[x] <= '9')
                    {
                        digit_flag = 1;
                    }
                    if(digit_flag == 1 && cap_flag == 1 && specchar_flag == 1)
                    {
                        validate_flag = 1;
                        break;
                    }
                }

                if(validate_flag == 1)
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                    cv_password.ErrorMessage = "Password is missing a special character, digit, or an uppercase letter";
                }
            }
            else
            {
                e.IsValid = false;
                cv_password.ErrorMessage = "Password Length should be minimum of 6 characters";
            }
        }*/
    }
}