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
        label2.Text = DateTime.Today.ToString("dd/MM/yyyy")+" "+DateTime.Now.ToString("h:mm:ss tt");
    }
}
