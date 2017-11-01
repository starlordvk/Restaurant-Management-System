using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RMSMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {   
        if(!this.IsPostBack)
        {
            theme_ddl.Items.Add("Dark");
            theme_ddl.Items.Add("Light");

            //Session["theme"] = "Light";
            if(Session["theme"]!=null)
            {
                theme_ddl.Items.FindByText(Session["theme"].ToString()).Selected = true;
            }
            
        }
        
        
        label2.Text = DateTime.Today.ToString("dd/MM/yyyy")+" "+DateTime.Now.ToString("h:mm:ss tt");
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
      
    }
    protected void ChangeTheme(object sender, EventArgs e)
    {
      
            Session["theme"] = theme_ddl.SelectedItem.Text;
            Response.Redirect(Request.Path);
    }

    protected void Logout(object sender, EventArgs e)
    {

    }
}
