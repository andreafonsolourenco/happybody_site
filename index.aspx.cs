using System;
using System.Web.UI;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

public partial class index : Page
{
    string data = "";

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime now;

        try
        {
            data = Request.QueryString["data"];

            if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data))
            {
                now = DateTime.Now;
            }
            else
            {
                now = Convert.ToDateTime(data);
            }
        }
        catch (Exception exc)
        {
            now = DateTime.Now;
        }

        if (now.Year >= 2023)
        {
            divText.InnerHtml = getText();
        }
        else
        {
            divText.InnerHtml = getWebSiteInConstructionMessage();
        }
    }

    private string getText()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   DECLARE @titlePT varchar(max) = 'Juntos Conseguimos';
                                            DECLARE @textPT varchar(max) = 'Carrega Aqui!';
                                            DECLARE @titleEN varchar(max);
                                            DECLARE @textEN varchar(max);
                                            DECLARE @titleFR varchar(max);
                                            DECLARE @textFR varchar(max);

                                            EXEC DEVOLVE_TRADUCAO @titlePT, 'EN', @titleEN output;
                                            EXEC DEVOLVE_TRADUCAO @textPT, 'EN', @textEN output;
                                            EXEC DEVOLVE_TRADUCAO @titlePT, 'FR', @titleFR output;
                                            EXEC DEVOLVE_TRADUCAO @textPT, 'FR', @textFR output;                                            

                                            SELECT 
                                                @titlePT as titlePT,
                                                @textPT as textPT,
                                                @titleEN as titleEN,
                                                @textEN as textEN,
                                                @titleFR as titleFR,
                                                @textFR as textFR,
                                                SERVIDOR_SITE
                                            FROM APPLICATION_CONFIG");

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                int count = oDs.Tables.Count - 1;

                for (int i = 0; i < oDs.Tables[count].Rows.Count; i++)
                {
                    table.AppendFormat(@"   <div class='form-group col-6 col-md-6 col-xl-6 col-sm-6 col-lg-6 text-center' style='vertical-align: middle !important;'>
                                                <h5 class='page-section-heading text-center text-uppercase text-secondary mb-0 cursor text-white'>{0}</h5>
                                                <button class='btn btn-primary text-uppercase' style='max-width: 100% !important' onclick='openPagePT();' runat='server'>
                                                    {1}
                                                </button>
                                            </div>
                                            <div class='form-group col-6 col-md-6 col-xl-6 col-sm-6 col-lg-6 text-center' style='vertical-align: middle !important;'>
                                                <h5 class='page-section-heading text-center text-uppercase text-secondary mb-0 cursor text-white'>{2}</h5>
                                                <button class='btn btn-primary text-uppercase' style='max-width: 100% !important; margin-bottom: 5px !important;' onclick='openPageEN();' runat='server'>
                                                    {3}
                                                </button>
                                                <button class='btn btn-primary text-uppercase' style='max-width: 100% !important; margin-bottom: 5px !important;' onclick='openPageFR();' runat='server'>
                                                    {4}
                                                </button>
                                            </div>
                                            <span id='pagePT' class='variaveis'>/{5}welcome.aspx?language=PT</span>
                                            <span id='pageEN' class='variaveis'>/{5}welcome.aspx?language=EN</span>
                                            <span id='pageFR' class='variaveis'>/{5}welcome.aspx?language=FR</span>",
                                            oDs.Tables[count].Rows[i]["titlePT"].ToString(),
                                            oDs.Tables[count].Rows[i]["textPT"].ToString(),
                                            oDs.Tables[count].Rows[i]["titleEN"].ToString(),
                                            oDs.Tables[count].Rows[i]["textEN"].ToString(),
                                            oDs.Tables[count].Rows[i]["textFR"].ToString(),
                                            oDs.Tables[count].Rows[i]["SERVIDOR_SITE"].ToString());
                }
            }
            else
            {

            }
        }
        catch (Exception exc)
        {

        }

        return table.ToString();
    }

    private string getWebSiteInConstructionMessage()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        DataSet dts = new DataSet();

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @pt varchar(max) = 'Brevemente...';
                                            DECLARE @en varchar(max);
                                            DECLARE @fr varchar(max);
                                            DECLARE @es varchar(max);

                                            EXEC DEVOLVE_TRADUCAO @pt, 'EN', @en output;
                                            EXEC DEVOLVE_TRADUCAO @pt, 'ES', @es output;
                                            EXEC DEVOLVE_TRADUCAO @pt, 'FR', @fr output;
                                            
                                            SELECT
                                                @pt as pt,
                                                @en as en,
                                                @fr as fr,
                                                @es as es");

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

                table.AppendFormat(@"   <div class='col-12 col-md-12 col-xl-12 col-sm-12 col-lg-12 text-center' style='margin: auto !important; vertical-align: middle !important;'>
                                                <marquee behavior='scroll' direction='left' scrollamount='10'>");

                for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
                {
                    table.AppendFormat(@"<h3 class='page-section-heading text-center text-uppercase text-secondary mb-0 text-white'>{0}{1}{2}{1}{3}{1}{4}</h3>",
                                            dts.Tables[0].Rows[i]["pt"].ToString(),
                                            space,
                                            dts.Tables[0].Rows[i]["en"].ToString(),
                                            dts.Tables[0].Rows[i]["fr"].ToString(),
                                            dts.Tables[0].Rows[i]["es"].ToString());
                }

                table.AppendFormat(@"</marquee>");
            }

            connection.Close();
        }
        catch (Exception exc)
        {

        }

        return table.ToString();
    }
}