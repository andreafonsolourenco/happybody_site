using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class InsertCustomer : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "InsertCustomer"))
            {
                
            }
        }

        loadComerciais();
        loadTiposContrato();
        loadEstadosContrato();
        loadPermissoes();
    }

    private void loadComerciais()
    {
        StringBuilder table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT
                                                OPERADORESID as ID,
                                                LTRIM(RTRIM(NOME)) as NOME
                                            FROM OPERADORES
                                            WHERE VISIVEL = 1
                                            AND ATIVO = 1
                                            ORDER BY ADMINISTRADOR DESC");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"Comercial");
                table.AppendFormat(@"<select class='form-control' id='comercial'>");
                while (myReader.Read())
                {
                    table.AppendFormat(@"<option value='{0}'>{1}</option>", myReader["ID"].ToString(), myReader["NOME"].ToString());
                }
                table.AppendFormat(@"</select>");

                connection.Close();
                comercialDiv.InnerHtml = table.ToString();
            }
            else
            {
                table.AppendFormat(@"Utilizador não encontrado!");
                connection.Close();
                comercialDiv.InnerHtml = table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            comercialDiv.InnerHtml = table.ToString();
        }
    }

    private void loadTiposContrato()
    {
        StringBuilder table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT
                                                TIPO_CONTRATOID,
	                                            LTRIM(RTRIM(CODIGO)) as CODIGO,
	                                            LTRIM(RTRIM(DESIGNACAO)) as DESIGNACAO
                                            from tipo_contrato
                                            order by CODIGO");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"Tipo de Contrato");
                table.AppendFormat(@"<select class='form-control' id='tipoContrato' onchange='loadFinalDate();'>");
                while (myReader.Read())
                {
                    table.AppendFormat(@"<option value='{0}'>{1}</option>",
                        myReader["TIPO_CONTRATOID"].ToString(),
                        myReader["DESIGNACAO"].ToString());
                }
                table.AppendFormat(@"</select>");

                connection.Close();
                tipoContratoDiv.InnerHtml = table.ToString();
            }
            else
            {
                table.AppendFormat(@"Não existem tipos de contrato a apresentar!");
                connection.Close();
                tipoContratoDiv.InnerHtml = table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            tipoContratoDiv.InnerHtml = table.ToString();
        }
    }

    private void loadEstadosContrato()
    {
        StringBuilder table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT
                                                estados_contratoid,
	                                            LTRIM(RTRIM(CODIGO)) as CODIGO,
	                                            LTRIM(RTRIM(DESIGNACAO)) as DESIGNACAO
                                            from estados_contrato
                                            order by ATIVO desc, CODIGO asc");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"Estado do Contrato");
                table.AppendFormat(@"<select class='form-control' id='estadoContrato'>");
                while (myReader.Read())
                {
                    table.AppendFormat(@"<option value='{0}'>{1}</option>",
                        myReader["estados_contratoid"].ToString(),
                        myReader["DESIGNACAO"].ToString());
                }
                table.AppendFormat(@"</select>");

                connection.Close();
                estadoContratoDiv.InnerHtml = table.ToString();
            }
            else
            {
                table.AppendFormat(@"Não existem estados de contrato a apresentar!");
                connection.Close();
                estadoContratoDiv.InnerHtml = table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            estadoContratoDiv.InnerHtml = table.ToString();
        }
    }

    [WebMethod]
    public static string insertNewCustomer(string id_operador, string xml)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        xml = xml.Replace("''", "\"");
        xml = xml.Replace("#", "''");

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @docXml nvarchar(max) = N'{1}';
                                            DECLARE @erro int

                                            EXEC CREATE_NEW_CUSTOMER @id_operador, @docXml, @erro output

                                            SELECT @erro as id_contrato", id_operador, xml);
            //return sql;
            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    string ret = myReader["id_contrato"].ToString();
                    connection.Close();
                    return ret;
                }
            }
            else
            {
                connection.Close();
                return "Erro ao adicionar novo sócio!";
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            return "Erro ao adicionar novo sócio! Por favor, verifique se o nº de CC em questão já se encontra inserido na base de dados!";
        }

        connection.Close();
        return "Erro ao adicionar novo sócio!";
    }

    [WebMethod]
    public static string loadFinalDate(string id_tipocontrato, string data_inicio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_tipocontrato int = {0};
	                                        DECLARE @data_inicio datetime = '{1}';
                                            DECLARE @data_fim datetime;
                                            DECLARE @meses int = (SELECT DURACAO_MINIMA_MESES FROM TIPO_CONTRATO WHERE TIPO_CONTRATOID = @id_tipocontrato);

                                            SET @data_fim = (SELECT DATEADD(MONTH, @meses, @data_inicio));
                                            SET @data_fim = (SELECT DATEADD(DAY, -1, @data_fim))

                                            SELECT CONVERT(VARCHAR(10), CAST(@data_fim as DATE), 103) as data_fim", id_tipocontrato, data_inicio);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"{0}", myReader["data_fim"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"{0}", data_inicio);
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", data_inicio);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"{0}", data_inicio);
        connection.Close();
        return table.ToString();
    }

    private void loadPermissoes()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_op int = {0};
                                            DECLARE @pagina varchar(400) = 'Clientes';
                                            DECLARE @subpagina varchar(400) = 'Inserir Cliente';

                                            IF(SELECT ADMINISTRADOR FROM OPERADORES WHERE OPERADORESID = @id_op) = 1
                                            BEGIN
                                                select 1 as escrita, 1 as leitura
                                            END
                                            ELSE
                                            BEGIN
                                                select 
                                                    escrita, leitura
                                                from REPORT_PERMISSOES(@id_op, @pagina, @subpagina)
                                            END", id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    lblescrita.InnerHtml = myReader["escrita"].ToString();
                    lblleitura.InnerHtml = myReader["leitura"].ToString();
                }
            }
            else
            {
                connection.Close();
            }

            connection.Close();
        }
        catch (Exception exc)
        {
            connection.Close();
        }

        connection.Close();
    }

    [WebMethod]
    public static string loadPreInscricao(string id_operador, string filtro)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @FILTRO varchar(MAX) = {1}
                                            DECLARE @id_preinscricao int;

                                            SELECT
                                                id_preinscricao,
	                                            NOME,
	                                            MORADA,
	                                            CODPOSTAL,
	                                            LOCALIDADE,
	                                            TLF_EMERGENCIA,
	                                            TELEMOVEL,
	                                            data_nascimento,
	                                            CC_NR,
	                                            VALIDADE_CC,
	                                            PROFISSAO,
	                                            EMAIL,
	                                            NAO_QUER_PUBLICIDADE,
	                                            DATA_PRIMEIRO_TREINO,
	                                            DATA_HORA_PRIMEIRA_AF
                                            FROM REPORT_PRE_INSCRICOES(@id_preinscricao)
                                            where @FILTRO IS NULL or NOME like ('%' + @FILTRO + '%') or TELEMOVEL like ('%' + @FILTRO + '%')
                                            or EMAIL like ('%' + @FILTRO + '%') or CC_NR like ('%' + @FILTRO + '%')
                                            order by nome", id_operador, string.IsNullOrEmpty(filtro) ? "NULL" : string.Format("'{0}'", filtro));

            
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
                                                    <th class='headerLeft'>CC Nº</th>
                                                    <th class='headerRight'>Nome</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    table.AppendFormat(@"<tr ondblclick='getPreInscricaoInfo({0});'>
                                            <td style='display:none;' id='id_{2}'>{0}</td>
                                            <td class='tbodyTrTdLeft'>{3}</td>
                                            <td class='tbodyTrTdRight'>{1}</td>
                                        </tr>",
                                                myReader["id_preinscricao"].ToString(),
                                                myReader["NOME"].ToString(),
                                                conta.ToString(),
                                                myReader["CC_NR"].ToString());

                    conta++;
                }

                table.AppendFormat("</tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem pré-inscrições a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem pré-inscrições a apresentar.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string loadData(string id)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   DECLARE @id_preinscricao int = {0};

                                            SELECT
                                                id_preinscricao,
	                                            NOME,
	                                            MORADA,
	                                            CODPOSTAL,
	                                            LOCALIDADE,
	                                            TLF_EMERGENCIA,
	                                            TELEMOVEL,
	                                            data_nascimento,
	                                            CC_NR,
	                                            VALIDADE_CC,
	                                            PROFISSAO,
	                                            EMAIL,
	                                            NAO_QUER_PUBLICIDADE,
	                                            DATA_PRIMEIRO_TREINO,
	                                            DATA_HORA_PRIMEIRA_AF
                                            FROM REPORT_PRE_INSCRICOES(@id_preinscricao)", id);


            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    table.AppendFormat(@"   <span class='variaveis' id='preinscricao_nome'>{0}</span>
                                            <span class='variaveis' id='preinscricao_morada'>{1}</span>
                                            <span class='variaveis' id='preinscricao_codpostal'>{2}</span>
                                            <span class='variaveis' id='preinscricao_localidade'>{3}</span>
                                            <span class='variaveis' id='preinscricao_tlf_emergencia'>{4}</span>
                                            <span class='variaveis' id='preinscricao_telemovel'>{5}</span>
                                            <span class='variaveis' id='preinscricao_data_nascimento'>{6}</span>
                                            <span class='variaveis' id='preinscricao_cc_nr'>{7}</span>
                                            <span class='variaveis' id='preinscricao_validade_cc'>{8}</span>
                                            <span class='variaveis' id='preinscricao_profissao'>{9}</span>
                                            <span class='variaveis' id='preinscricao_email'>{10}</span>
                                            <span class='variaveis' id='preinscricao_nao_quer_publicidade'>{11}</span>
                                            <span class='variaveis' id='preinscricao_data_primeiro_treino'>{12}</span>
                                            <span class='variaveis' id='preinscricao_data_hora_primeira_af'>{13}</span>",
                                                oDs.Tables[0].Rows[i]["NOME"].ToString(),
                                                oDs.Tables[0].Rows[i]["MORADA"].ToString(),
                                                oDs.Tables[0].Rows[i]["CODPOSTAL"].ToString(),
                                                oDs.Tables[0].Rows[i]["LOCALIDADE"].ToString(),
                                                oDs.Tables[0].Rows[i]["TLF_EMERGENCIA"].ToString(),
                                                oDs.Tables[0].Rows[i]["TELEMOVEL"].ToString(),
                                                oDs.Tables[0].Rows[i]["data_nascimento"].ToString(),
                                                oDs.Tables[0].Rows[i]["CC_NR"].ToString(),
                                                oDs.Tables[0].Rows[i]["VALIDADE_CC"].ToString(),
                                                oDs.Tables[0].Rows[i]["PROFISSAO"].ToString(),
                                                oDs.Tables[0].Rows[i]["EMAIL"].ToString(),
                                                oDs.Tables[0].Rows[i]["NAO_QUER_PUBLICIDADE"].ToString(),
                                                oDs.Tables[0].Rows[i]["DATA_PRIMEIRO_TREINO"].ToString(),
                                                oDs.Tables[0].Rows[i]["DATA_HORA_PRIMEIRA_AF"].ToString());
                }
            }
            else
            {
                table.AppendFormat("");
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("");
        }

        return table.ToString();
    }

    [WebMethod]
    public static string deletePreInscricao(string id_operador, string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @id_preinscricao int = {1};
                                            DECLARE @ret int;

                                            EXEC DELETE_PREINSCRICAO @id_operador, @id_preinscricao, @ret output;

                                            select @ret as ret", id_operador, id);


            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"{0}", myReader["ret"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("Ocorreu um erro ao eliminar a Pré-Inscrição!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("Ocorreu um erro ao eliminar a Pré-Inscrição!");
            return table.ToString();
        }
    }
}
