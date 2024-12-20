using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Text;
using System.Web;
using System.Data.SqlClient;

public partial class EmailsRecebidosSite : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "EmailsRecebidosSite"))
            {
                
            }
        }
    }

    [WebMethod]
    public static string load(string filtro)
    {
        var table = new StringBuilder();
        string cs = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_email_send_temp int;
                                            DECLARE @tipo_email varchar(max);
                                            DECLARE @lido bit;
                                            DECLARE @FILTRO varchar(MAX) = {0};

                                            SELECT 
                                                id_email,
		                                        TIPO_EMAIL,
		                                        id_externo,
		                                        titulo_externo,
		                                        NOME,
		                                        EMAIL,
		                                        TELEFONE,
		                                        IDADE,
		                                        [DATA],
		                                        HORA,
		                                        ASSUNTO,
		                                        TEXTO,
		                                        LIDO,
		                                        LINGUA,
		                                        CTRLDATA,
		                                        RESPONDIDO
                                            FROM REPORT_EMAIL_SEND_TEMP(@id_email_send_temp, @tipo_email, @lido)
                                            WHERE (@FILTRO IS NULL OR (TIPO_EMAIL LIKE '%' + @FILTRO + '%') OR (titulo_externo LIKE '%' + @FILTRO + '%')
                                                OR (CTRLDATA LIKE '%' + @FILTRO + '%'))
                                            ORDER BY lido asc, ctrldata desc, respondido asc, tipo_email asc", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th style='text-align: center; width: 100%;' colspan='6'>Emails Recebidos</th>
						                        </tr>
                                                <tr>
                                                    <th style='text-align: center; width: 20%;'>Data</th>
                                                    <th style='text-align: center; width: 20%;'>Tipo de Email</th>
                                                    <th style='text-align: center; width: 30%;'>Remetente</th>
                                                    <th style='text-align: center; width: 10%;'>Lido</th>
                                                    <th style='text-align: center; width: 10%;'>Respondido</th>
                                                    <th style='text-align: center; width: 10%;'>Apagar</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    string remetente = "";
                    string on = "<img src='img/icons/on.jpg' />";
                    string off = "<img src='img/icons/off.jpg' />";

                    if (myReader["NOME"].ToString() != "" && myReader["EMAIL"].ToString() != "")
                    {
                        remetente = myReader["NOME"].ToString() + " - " + myReader["EMAIL"].ToString();
                    }
                    else
                    {
                        if (myReader["NOME"].ToString() != "")
                        {
                            remetente = myReader["NOME"].ToString();
                        }
                        else if (myReader["EMAIL"].ToString() != "")
                        {
                            remetente = myReader["EMAIL"].ToString();
                        }
                    }
                    
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick='showEmailInfo({0});'>
                                            <td class='tbodyTrTd' style='width:20%; text-align:center;'>{1}</td>
                                            <td class='tbodyTrTd' style='width:20%; text-align:center;'>{2}</td>
                                            <td class='tbodyTrTd' style='width:30%; text-align:center;'>{3}</td>
                                            <td class='tbodyTrTd' style='width:10%; text-align:center;'>{4}</td>
                                            <td class='tbodyTrTd' style='width:10%; text-align:center;'>{5}</td>
                                            <td class='tbodyTrTd' style='width:10%; text-align:center;'>
                                                <input type='button' value='APAGAR' onclick='askDeleteEmail({0});' style='background-color: #4282b5; width: auto; height: 50px; font-size: 12pt; text-align: center; line-height: 50px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; padding: 0 5px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;' />
                                            </td>
                                        </tr>",
                                                myReader["id_email"].ToString(),
                                                myReader["CTRLDATA"].ToString(),
                                                myReader["TIPO_EMAIL"].ToString(),
                                                myReader["NOME"].ToString() + " - " + myReader["EMAIL"].ToString(),
                                                Convert.ToBoolean(myReader["LIDO"].ToString()) ? on : off,
                                                Convert.ToBoolean(myReader["RESPONDIDO"].ToString()) ? on : off);

                    conta++;
                }

                table.AppendFormat("</tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem eventos a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem eventos a apresentar. {0}</div>", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string getEmailData(string id, string idUser)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id int = {0};
                                            DECLARE @id_user int = {1};
                                            DECLARE @ret int;
                                            DECLARE @retMsg varchar(max);

                                            EXEC ler_email @id_user, @id, @ret output, @retMsg output

                                            SELECT 
                                                id_email,
		                                        title,
		                                        language_text,
		                                        date_email,
		                                        email_text,
		                                        respondido
                                            FROM REPORT_EMAIL_SEND_TEMP_DATA(@id)", id, idUser);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"{0}{5}{1}{5}{2}{5}{3}{5}{4}",
                                                myReader["title"].ToString(),
                                                myReader["language_text"].ToString(),
                                                myReader["date_email"].ToString(),
                                                myReader["email_text"].ToString(),
                                                Convert.ToBoolean(myReader["respondido"].ToString()) ? "1" : "0",
                                                "<#SEP#>");
                }
                connection.Close();
                return table.ToString();
            }
            else
            {
                connection.Close();
                return "";
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            return "";
        }
    }

    [WebMethod]
    public static string answerEmail(string id, string idUser)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id int = {0};
                                            DECLARE @id_user int = {1};
                                            DECLARE @ret int;
                                            DECLARE @retMsg varchar(max);

                                            EXEC responder_email @id_user, @id, @ret output, @retMsg output

                                            SELECT  @ret as ret, @retMsg as retMsg", id, idUser);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"{0}{2}{1}",
                                                myReader["ret"].ToString(),
                                                myReader["retMsg"].ToString(),
                                                "<#SEP#>");
                }
                connection.Close();
                return table.ToString();
            }
            else
            {
                connection.Close();
                return "-1<#SEP#>Ocorreu um erro";
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            return "-999<#SEP#>Ocorreu um erro";
        }
    }

    [WebMethod]
    public static string deleteEmail(string id, string idUser)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id int = {0};
                                            DECLARE @id_user int = {1};
                                            DECLARE @ret int;
                                            DECLARE @retMsg varchar(max);

                                            EXEC delete_email @id_user, @id, @ret output, @retMsg output

                                            SELECT @ret as ret, @retMsg as retMsg", id, idUser);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"{0}{2}{1}",
                                                myReader["ret"].ToString(),
                                                myReader["retMsg"].ToString(),
                                                "<#SEP#>");
                }
                connection.Close();
                return table.ToString();
            }
            else
            {
                connection.Close();
                return "-1<#SEP#>Ocorreu um erro";
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            return "-999<#SEP#>Ocorreu um erro";
        }
    }
}
