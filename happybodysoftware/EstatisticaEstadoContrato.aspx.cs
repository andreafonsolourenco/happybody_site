using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class EstatisticaEstadoContrato : Page
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
    }

    private void loadStats()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_estado_contrato int;
                                        
                                            SELECT 
                                                STATUS,
                                                NR_CONTRATOS,
                                                VALOR_PERCENTAGEM
                                            FROM REPORT_ESTATISTICA_ESTADOS_CONTRATO(@id_estado_contrato)
                                            ORDER BY VALOR_PERCENTAGEM DESC");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerLeft'>Estado do Contrato</th>
                                                    <th class='headerRight'>Nº de Contratos</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick=''>
                                            <td class='tbodyTrTdLeft'>{0}</td>
                                            <td class='tbodyTrTdRight'>{2}%<br /><span style='font-size: small;'>{1} Contratos</span></td>
                                        </tr>",
                                                myReader["STATUS"].ToString(),
                                                myReader["NR_CONTRATOS"].ToString(),
                                                myReader["VALOR_PERCENTAGEM"].ToString());
                }

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                connection.Close();
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem estatísticas a apresentar para os Tipos de Contrato</div>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Erro ao carregar estatísticas relativas aos Tipos de Contrato: {0}</div>", exc.ToString());
        }

        divTable.InnerHtml = table.ToString();
    }
}
