﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BillsPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if(Session["theme"] != null)
        {
            Page.Theme = Session["theme"].ToString();
        }
    }


    protected void SqlDataSource2_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        Console.WriteLine("Selecting");
    }

    protected void SqlDataSource2_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        Console.WriteLine("Selected");
    }
}