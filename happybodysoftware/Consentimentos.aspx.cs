using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.Net.Mail;
using System.IO;

public partial class Consentimentos : Page
{
    string separador = "";
    string id = "";

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        separador = HttpContext.Current.Request.Url.PathAndQuery;
        id = Request.QueryString["id"];
        lbloperatorid.InnerHtml = id;

        if (!IsPostBack)
        {
            ClientScriptManager oCsm = this.Page.ClientScript;
            if (!oCsm.IsStartupScriptRegistered(GetType(), "MainMenu"))
            {
                
            }
        }

        loadStats();
        //loadAniversariosMensal();
    }

    private void loadStats()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        var link = "";

        try
        {
            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                        
                                            SELECT 
                                                NOME, EMAIL, TELEFONE, CAST(AUTORIZO as INT) as AUTORIZO, TREINO_OFERECIDO_POR, 
                                                CONVERT(VARCHAR(10), CTRLDATA, 103) + ' ' + CONVERT(VARCHAR(5), CTRLDATA, 108) AS DATA,
                                                CONVERT(VARCHAR(10), TREINOEXPERIMENTAL, 103) + ' ' + CONVERT(VARCHAR(5), TREINOEXPERIMENTAL, 108) AS TREINO_EXPERIMENTAL,
                                                LINK,
                                                IP
                                            FROM RGPD
                                            ORDER BY LINK, CTRLDATA DESC");

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                                    <thead>
						                                <tr>
                                                            <th class='headerTable'>Consentimentos</th>
						                                </tr>
						                            </thead>
                                                    <tbody>");

                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr>
                                            <td>
                                                <span style='font-weight: bold'>{7}</span><br />
                                                {0}<br />
                                                {1}<br />
                                                {2}<br />
                                                {3}<br />
                                                IP: {6}
                                                {4}
                                                {5}
                                            </td>
                                        </tr>",
                                                oDs.Tables[0].Rows[i]["NOME"].ToString(),
                                                oDs.Tables[0].Rows[i]["EMAIL"].ToString(),
                                                oDs.Tables[0].Rows[i]["TELEFONE"].ToString(),
                                                oDs.Tables[0].Rows[i]["DATA"].ToString(),
                                                string.IsNullOrEmpty(oDs.Tables[0].Rows[i]["TREINO_OFERECIDO_POR"].ToString()) ? "" : "<br />Treino Oferecido Por: " + oDs.Tables[0].Rows[i]["TREINO_OFERECIDO_POR"].ToString(),
                                                string.IsNullOrEmpty(oDs.Tables[0].Rows[i]["TREINO_EXPERIMENTAL"].ToString()) ? "" : "<br />Treino Experimental: " + oDs.Tables[0].Rows[i]["TREINO_EXPERIMENTAL"].ToString(),
                                                oDs.Tables[0].Rows[i]["IP"].ToString(),
                                                oDs.Tables[0].Rows[i]["LINK"].ToString());
                }

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem consentimentos a apresentar.</div>");
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Erro ao carregar consentimentos: {0}</div>", exc.ToString());
        }

        divTable.InnerHtml = table.ToString();
    }
}
