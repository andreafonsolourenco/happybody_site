using System;
using System.Web.UI;
using System.Configuration;
using System.Data;
using System.Text;
using System.Data.SqlClient;

public partial class welcome : Page
{
    string language = "";
    static string csName = "connectionString";
    static string connectionstring = ConfigurationManager.ConnectionStrings[csName].ToString();

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime now;

        try
        {
            language = Request.QueryString["language"];
        }
        catch (Exception exc)
        {
            language = "FR";
        }

        getText();
    }

    private void getText()
    {
        var table = new StringBuilder();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   DECLARE @titlePT varchar(max) = 'Carrega Aqui!';
                                            DECLARE @button varchar(max);
                                            DECLARE @language varchar(10) = '{0}';

                                            EXEC DEVOLVE_TRADUCAO @titlePT, @language, @button output;                                      

                                            SELECT 
                                                CONCAT('&nbsp;&nbsp;&nbsp;', @button, '&nbsp;&nbsp;&nbsp;') as button,
                                                SERVIDOR_SITE
                                            FROM APPLICATION_CONFIG", language);

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
                    table.AppendFormat(@"   <div class='form-group col-12 col-md-12 col-xl-12 col-sm-12 col-lg-12 text-center' style='margin: auto; vertical-align: middle !important;'>
                                                <button class='btn btn-primary btn-xl text-uppercase' style='width: 90% !important; max-width: 90% !important;' id='btnGoToPage' onclick='openPage();' runat='server'>
                                                    <i class='fas fa-arrow-right fa-fw'></i>
                                                    {0}
                                                    <i class='fas fa-arrow-left fa-fw'></i>
                                                </button>
                                            </div>
                                            <span id='page' class='variaveis'>/{2}index.aspx</span>
                                            <span id='languageToRemove' class='variaveis'>?language={1}</span>",
                                            oDs.Tables[count].Rows[i]["button"].ToString(),
                                            language,
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

        divText.InnerHtml = table.ToString();
    }
}
