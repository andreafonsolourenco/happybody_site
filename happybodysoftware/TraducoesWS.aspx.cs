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

public partial class TraducoesWS : Page
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
    }

    [WebMethod]
    public static string getTraducoes(string operatorID)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                        
                                            SELECT 
                                                TEXTO_PT,
                                                TEXTO_EN,
                                                TEXTO_ES,
                                                TEXTO_FR,
                                                CAST(CAMPANHA_INICIAL as int) as CAMPANHA_INICIAL
                                            FROM WS_TRADUCOES
                                            ORDER BY CAMPANHA_INICIAL DESC");

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
                                                    <th class='headerColspan' colspan='4'>TRADUÇÕES</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                string style = "style='background-color: #87CEFA';";

                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr {5}>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='PT' style='width: 100%' id='pt{4}'>{0}</textarea>
                                            </td>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='ES' style='width: 100%' id='es{4}'>{3}</textarea>
                                            </td>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='FR' style='width: 100%' id='fr{4}'>{1}</textarea>
                                            </td>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='EN' style='width: 100%' id='en{4}'>{2}</textarea>
                                            </td>
                                            <span class='variaveis' id='campanha{4}'>{6}</span>
                                        </tr>",
                                                oDs.Tables[0].Rows[i]["texto_pt"].ToString().Replace("<br />", "\n").Replace("´", "'"),
                                                oDs.Tables[0].Rows[i]["texto_fr"].ToString().Replace("<br />", "\n").Replace("´", "'"),
                                                oDs.Tables[0].Rows[i]["texto_en"].ToString().Replace("<br />", "\n").Replace("´", "'"),
                                                oDs.Tables[0].Rows[i]["texto_es"].ToString().Replace("<br />", "\n").Replace("´", "'"),
                                                i.ToString(),
                                                oDs.Tables[0].Rows[i]["CAMPANHA_INICIAL"].ToString() == "0" ? "" : style,
                                                oDs.Tables[0].Rows[i]["CAMPANHA_INICIAL"].ToString());
                }

                // Adiciona as linhas com dados
                table.AppendFormat(@"<tr>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='PT' style='width: 100%' id='ptNew' />
                                            </td>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='ES' style='width: 100%' id='esNew' />
                                            </td>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='FR' style='width: 100%' id='frNew' />
                                            </td>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='EN' style='width: 100%' id='enNew' />
                                            </td>
                                            <span class='variaveis' id='campanhaNew'>0</span>
                                        </tr>");

                table.AppendFormat("</tbody></table>");
                table.AppendFormat("<span id='countElements' style='display:none'>{0}</span>", oDs.Tables[0].Rows.Count.ToString());
            }
            else
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerColspan' colspan='4'>TRADUÇÕES</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                // Adiciona as linhas com dados
                table.AppendFormat(@"<tr>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='PT' style='width: 100%' id='ptNew' />
                                            </td>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='ES' style='width: 100%' id='esNew' />
                                            </td>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='FR' style='width: 100%' id='frNew' />
                                            </td>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='EN' style='width: 100%' id='enNew' />
                                            </td>
                                            <span class='variaveis' id='campanhaNew'>0</span>
                                        </tr>");

                table.AppendFormat("</tbody></table>");
            }
        }
        catch (Exception exc)
        {
            // Adiciona as linhas
            table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerColspan' colspan='4'>TRADUÇÕES</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

            // Adiciona as linhas com dados
            table.AppendFormat(@"<tr>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='PT' style='width: 100%' id='ptNew' />
                                            </td>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='ES' style='width: 100%' id='esNew' />
                                            </td>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='FR' style='width: 100%' id='frNew' />
                                            </td>
                                            <td style='width:25%;'>
                                                <textarea rows='3' class='form-control' placeholder='EN' style='width: 100%' id='enNew' />
                                            </td>
                                        </tr>");

            table.AppendFormat("</tbody></table>");
        }

        return table.ToString();
    }

    [WebMethod]
    public static string updateTraducoes(string id_operador, string xml)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        xml = xml.Replace("''", "\"");

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_op int = {0};
                                            DECLARE @xml nvarchar(max) = '{1}';
                                            DECLARE @ret int;

                                            EXEC UPDATE_TRADUCOES @id_op, @xml, @ret output

                                            SELECT @ret as ret", id_operador, xml);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"{0}", myReader["ret"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("-1");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }
}
