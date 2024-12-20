using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Log : Page
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
        loadOperators();
    }

    [WebMethod]
    public static string loadStats(string id_operador, string data_inicio, string data_fim, string filtro)
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
                                            DECLARE @id_operador int = {0};
                                            DECLARE @id_tipo int;
                                            DECLARE @data_inicio datetime = {1};
                                            DECLARE @data_fim datetime = {2};
                                            DECLARE @filtro varchar(max) = {3};
                                            declare @id_log int
                                            declare @notas varchar(max)
                                            declare @temptable table(id_log int)

                                            IF(@filtro is null)
                                            begin
                                                SELECT 
                                                    operador,
                                                    data,
                                                    hora,
                                                    tipo,
                                                    notas
                                                FROM REPORT_LOG_OPERADORES(@id_operador, @id_tipo, @data_inicio, @data_fim)
                                                where (notas like '%' + @filtro + '%' or @filtro is null)
                                                ORDER BY CAST(data as date) DESC, hora desc
                                            end
                                            else
                                            begin
                                                DECLARE db_cursor CURSOR FOR 
                                                SELECT id_log, notas 
                                                FROM REPORT_LOG_OPERADORES(@id_operador, @id_tipo, @data_inicio, @data_fim) rpt 

                                                OPEN db_cursor  
                                                FETCH NEXT FROM db_cursor INTO @id_log, @notas  

                                                WHILE @@FETCH_STATUS = 0  
                                                BEGIN
                                                    IF(SELECT value FROM STRING_SPLIT(@notas, ' ') WHERE LTRIM(RTRIM(value)) = @filtro) is not null
	                                                insert into @temptable(id_log) values(@id_log)

                                                    FETCH NEXT FROM db_cursor INTO @id_log, @notas 
                                                END 

                                                CLOSE db_cursor  
                                                DEALLOCATE db_cursor 

                                                select
                                                    operador,
                                                    data,
                                                    hora,
                                                    tipo,
                                                    notas
                                                FROM REPORT_LOG_OPERADORES(@id_operador, @id_tipo, @data_inicio, @data_fim) rpt
                                                inner join @temptable tmp on tmp.id_log = rpt.id_log
                                                ORDER BY CAST(data as date) DESC, hora desc
                                            end", id_operador == "0" ? "NULL" : id_operador
                                                , string.IsNullOrEmpty(data_inicio) ? "NULL" : "'" + data_inicio + "'"
                                                , string.IsNullOrEmpty(data_fim) ? "NULL" : "'" + data_fim + "'"
                                                , filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   
                                        <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerTable'>Log</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr>
                                            <td>
                                                {0} - {1}<br />
                                                {2}<br />
                                                {3}<br />
                                                {4}
                                            </td>
                                        </tr>",
                                                oDs.Tables[0].Rows[i]["data"].ToString(),
                                                oDs.Tables[0].Rows[i]["hora"].ToString(),
                                                oDs.Tables[0].Rows[i]["operador"].ToString(),
                                                oDs.Tables[0].Rows[i]["tipo"].ToString(),
                                                oDs.Tables[0].Rows[i]["notas"].ToString());
                }

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                connection.Close();
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem registos a apresentar.</div>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Erro ao carregar registos: {0}</div>", exc.ToString());
        }

        return table.ToString();
    }

    private void loadOperators()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                        
                                            SELECT 
                                                operadoresid,
                                                nome,
                                                CONVERT(VARCHAR(10), DATEADD(hh, -1, GETDATE()), 103) as data_atual
                                            FROM OPERADORES");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            string data_atual = "";

            if (myReader.HasRows)
            {
                table.AppendFormat(@"   <select class='form-control' id='op' style='width:100%; height: 40px; font-size: small;' onchange='loadLog();'>");

                table.AppendFormat(@"   <option value='0'>Todos</option>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <option value='{0}'>{1}</option>", myReader["operadoresid"].ToString(),
                                                                                myReader["nome"].ToString());

                    data_atual = myReader["data_atual"].ToString();
                }

                table.AppendFormat(@"</select");

                connection.Close();
                divOperadores.InnerHtml = table.ToString();
                dataInicio.Value = data_atual;
                dataFim.Value = data_atual;
            }
            else
            {
                table.AppendFormat(@"   <select class='form-control' id='op' style='width:100%; height: 25px; font-size: small;'>");
                table.AppendFormat(@"   <option value='0'>Não existem operadores a apresentar</option>");
                table.AppendFormat(@"   </select>");
                connection.Close();
                divOperadores.InnerHtml = table.ToString();
                dataInicio.Value = "";
                dataFim.Value = "";
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat(@"   <select class='form-control' id='op' style='width:100%; height: 25px; font-size: small;'>");
            table.AppendFormat(@"   <option value='0'>Não existem operadores a apresentar</option>");
            table.AppendFormat(@"   </select>");
            connection.Close();
            divOperadores.InnerHtml = table.ToString();
            dataInicio.Value = "";
            dataFim.Value = "";
        }
    }
}
