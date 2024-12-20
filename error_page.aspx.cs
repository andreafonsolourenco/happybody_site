using System;
using System.Web.UI;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

public partial class error_page : Page
{
    string error = "";

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            error = Request.QueryString["error"];

            if (string.IsNullOrEmpty(error) || string.IsNullOrWhiteSpace(error))
            {
                error = "404";
            }
        }
        catch (Exception exc)
        {
            error = "404";
        }

        divText.InnerHtml = getErrorTextMessage();
    }

    private string getErrorTextMessage()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        DataSet dts = new DataSet();
        string pt = String.Empty;

        switch(error)
        {
            default: 
                pt = "Página não encontrada!";
                break;
        }

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @pt varchar(max) = '{0}';
                                            DECLARE @en varchar(max);
                                            DECLARE @fr varchar(max);
                                            DECLARE @es varchar(max);
                                            DECLARE @textPT varchar(max) = 'Carrega Aqui!';
                                            DECLARE @textEN varchar(max);
                                            DECLARE @textFR varchar(max);
                                            DECLARE @textES varchar(max);
                                            
                                            EXEC DEVOLVE_TRADUCAO @textPT, 'EN', @textEN output;
                                            EXEC DEVOLVE_TRADUCAO @textPT, 'FR', @textFR output; 
                                            EXEC DEVOLVE_TRADUCAO @textPT, 'ES', @textES output;
                                            EXEC DEVOLVE_TRADUCAO @pt, 'EN', @en output;
                                            EXEC DEVOLVE_TRADUCAO @pt, 'ES', @es output;
                                            EXEC DEVOLVE_TRADUCAO @pt, 'FR', @fr output;
                                            
                                            SELECT
                                                @pt as pt,
                                                @en as en,
                                                @fr as fr,
                                                @es as es,
                                                @textPT as textPT,
                                                @textEN as textEN,
                                                @textFR as textFR,
                                                @textES as textES,
                                                SERVIDOR_SITE
                                            FROM APPLICATION_CONFIG", pt);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            adapter.Fill(dts);

            if (dts.Tables.Count > 0)
            {
                string space = string.Empty;

                for (int j = 0; j < 50; j++)
                {
                    space += "&nbsp";
                }

                for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
                {
                    table.AppendFormat(@"   <div class='col-12 col-md-12 col-xl-12 col-sm-12 col-lg-12 text-center' style='margin: auto !important; vertical-align: middle !important;'>
                                                <marquee behavior='scroll' direction='left' scrollamount='10'>
                                                    <h3 class='page-section-heading text-center text-uppercase text-secondary mb-0 text-white'>{0}{1}{2}{1}{3}{1}{4}</h3>
                                                </marquee>
                                                <button class='btn btn-primary text-uppercase' style='max-width: 25% !important' onclick='openPagePT();' runat='server'>
                                                    {5}
                                                </button>
                                                <button class='btn btn-primary text-uppercase' style='max-width: 25% !important;' onclick='openPageEN();' runat='server'>
                                                    {6}
                                                </button>
                                                <button class='btn btn-primary text-uppercase' style='max-width: 25% !important;' onclick='openPageFR();' runat='server'>
                                                    {7}
                                                </button>
                                                <button class='btn btn-primary text-uppercase' style='max-width: 25% !important;' onclick='openPageES();' runat='server'>
                                                    {9}
                                                </button>
                                            </div>
                                            <span id='pagePT' class='variaveis'>/{8}index.aspx?language=PT</span>
                                            <span id='pageEN' class='variaveis'>/{8}index.aspx?language=EN</span>
                                            <span id='pageFR' class='variaveis'>/{8}index.aspx?language=FR</span>
                                            <span id='pageES' class='variaveis'>/{8}index.aspx?language=ES</span>",
                                            dts.Tables[0].Rows[i]["pt"].ToString(),
                                            space,
                                            dts.Tables[0].Rows[i]["en"].ToString(),
                                            dts.Tables[0].Rows[i]["fr"].ToString(),
                                            dts.Tables[0].Rows[i]["es"].ToString(),
                                            dts.Tables[0].Rows[i]["textPT"].ToString(),
                                            dts.Tables[0].Rows[i]["textEN"].ToString(),
                                            dts.Tables[0].Rows[i]["textFR"].ToString(),
                                            dts.Tables[0].Rows[i]["SERVIDOR_SITE"].ToString(),
                                            dts.Tables[0].Rows[i]["textES"].ToString());
                }
            }

            connection.Close();
        }
        catch (Exception exc)
        {

        }

        return table.ToString();
    }
}