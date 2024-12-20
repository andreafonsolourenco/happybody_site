using System;
using System.Web.UI;

public partial class index : Page
{

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClientScriptManager oCsm = this.Page.ClientScript;
            if (!oCsm.IsStartupScriptRegistered(GetType(), "gymsummerpack"))
            {
                
            }
        }

        Response.Redirect("https://www.happybody.site/fitnessathome/Treinos_HappyBody.mp4");
    }
}
