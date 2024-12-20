using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class EditCustomer : Page
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

        loadPermissoes();
    }

    [WebMethod]
    public static string load(string id_operador, string filtro, string query)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @FILTRO varchar(MAX) = {1}

                                            SELECT
                                                soc.SOCIOSID, 
                                                soc.NOME,
	                                            soc.MORADA,
	                                            soc.CODPOSTAL,
	                                            soc.LOCALIDADE,
	                                            soc.TLF_EMERGENCIA,
	                                            soc.TELEMOVEL,
	                                            CAST(soc.DATA_NASCIMENTO as DATE) as DATA_NASCIMENTO,
	                                            soc.CC_NR,
	                                            CAST(soc.VALIDADE_CC as DATE) as VALIDADE_CC,
	                                            soc.PROFISSAO,
	                                            op.NOME as COMERCIAL,
                                                soc.SEXO,
	                                            soc.EMAIL,
                                                soc.NR_SOCIO,
                                                CAST(soc.NAO_QUER_RECEBER_PUBLICIDADE as INT) as NAO_QUER_RECEBER_PUBLICIDADE,
						                        CASE 
						                        WHEN rpt.DATA_INICIO is null 
						                        THEN (select top 1 CONVERT(VARCHAR(10), DATA_INICIO, 103) from contrato where socio_id = soc.sociosid order by data_fim desc) 
						                        ELSE CONVERT(VARCHAR(10), rpt.DATA_INICIO, 103) 
						                        END as DATA_INICIO_CONTRATO,
						                        CASE 
						                        WHEN rpt.DATA_FIM is null 
						                        THEN (select top 1 CONVERT(VARCHAR(10), DATA_FIM, 103) from contrato where socio_id = soc.sociosid order by data_fim desc) 
						                        ELSE CONVERT(VARCHAR(10), rpt.DATA_FIM, 103) 
						                        END as DATA_FIM_CONTRATO
                                            FROM SOCIOS soc
                                            INNER JOIN OPERADORES op on op.OPERADORESID = soc.ID_COMERCIAL
					                        LEFT JOIN REPORT_CONTRATOS_DENTRO_DATA(null, null) rpt on rpt.id_socio = soc.sociosid
                                            WHERE (@FILTRO IS NULL OR ({2}))
                                            ORDER BY soc.NR_SOCIO", id_operador, filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro), query);
            //return sql;
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
                                                    <th class='headerLeft'>Nº</th>
                                                    <th class='headerRight'>Nome</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    table.AppendFormat(@"<tr ondblclick='openCustomerInfo({0}, {2});'>
                                            <td style='display:none;' id='id_{2}'>{0}</td>
                                            <td class='tbodyTrTdLeft'>{3}</td>
                                            <td class='tbodyTrTdRight'>{1}</td>
                                        </tr>",
                                                myReader["SOCIOSID"].ToString(),
                                                myReader["NOME"].ToString(),
                                                conta.ToString(),
                                                myReader["NR_SOCIO"].ToString());

                    conta++;
                }

                table.AppendFormat("</tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem sócios a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem sócios a apresentar.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string getCustomerData(string id_operador, string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @id_socio int = {1};

                                            SELECT TOP 1
                                                SOCIOSID, 
                                                NOME,
	                                            MORADA,
	                                            CODPOSTAL,
	                                            LOCALIDADE,
	                                            TLF_EMERGENCIA,
	                                            TELEMOVEL,
	                                            DATA_NASCIMENTO,
	                                            CC_NR,
                                                VALIDADE_CC,
	                                            PROFISSAO,
	                                            COMERCIAL,
                                                COMERCIAL_ID,
                                                SEXO,
	                                            EMAIL,
                                                NR_SOCIO,
                                                CONTRATOID,
	                                            TIPO_CONTRATO_ID,
	                                            ESTADO_CONTRATO_ID,
	                                            NOTAS,
	                                            DATA_INICIO,
	                                            DATA_FIM,
	                                            DEBITO_DIRETO,
                                                VALOR,
                                                NAO_QUER_RECEBER_PUBLICIDADE,
                                                NOTAS_SOCIO
                                            FROM REPORT_SOCIO_ALTERA_CONTRATO(@id_socio)
                                            ORDER BY DATA_FIM DESC", id_operador, id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    string s = "";

                    if (myReader["SEXO"].ToString()=="M"){
                        s = "<label class='radio-inline'><input type='radio' name='optradioEdit' value='M' checked='checked'>Masculino</label><label class='radio-inline'><input type='radio' name='optradioEdit' value='F'>Feminino</label>";
                    }
                    else {
                        s = "<label class='radio-inline'><input type='radio' name='optradioEdit' value='M'>Masculino</label><label class='radio-inline'><input type='radio' name='optradioEdit' value='F' checked='checked'>Feminino</label>";
                    }

                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <div class='col-lg-12 col-nmd-12 col-sm-12 col-xs-12'>
                                                <input id='btnSave' value='Guardar' runat='server' type='button' onclick='edit();' style='background-color: #4282b5; float: left;
                                                        width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                                <input id='btnApagar' value='Apagar' runat='server' type='button' onclick='removeCustomer();' style='background-color: #4282b5; float: left;
                                                        width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                                <img src='img/icons/icon_close.png' style='cursor:pointer; float: right; vertical-align: middle;' onclick='backEdit();'/>
                                            </div>
                                            <div class='col-lg-2 col-md-2 col-sm-2 col-xs-2 line'>
                                                Nº Sócio
                                                <input type='text' class='form-control' id='nrSocioEdit' name='nrSocioEdit' placeholder='Nº' required='required' style='width: 100%; margin: auto;' value='{14}'/>
                                            </div>
                                            <div class='col-lg-10 col-md-10 col-sm-10 col-xs-10 line'>
                                                Nome
                                                <input type='text' class='form-control' id='nameEdit' name='nameEdit' placeholder='Nome' required='required' style='width: 100%; margin: auto;' value='{1}'/>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 line'>
                                                Morada
                                                <input type='text' class='form-control' id='moradaEdit' name='moradaEdit' placeholder='Morada' required='required' style='width: 100%; margin: auto;' value='{2}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                Cod. Postal
                                                <input type='text' class='form-control' id='codpostalEdit' name='codpostalEdit' placeholder='Código Postal: xxxx-xxx' required='required' style='width: 100%; margin: auto;' value='{3}'/>
                                            </div>
                                            <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8 line'>
                                                Localidade
                                                <input type='text' class='form-control' id='localidadeEdit' name='localidadeEdit' placeholder='Localidade' required='required' style='width: 100%; margin: auto; float: left' value='{4}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                Telefone
                                                <input type='text' class='form-control' id='tlfEdit' name='tlfEdit' placeholder='Telefone' required='required' style='width: 100%; margin: auto; float: left' value='{6}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                Tlf Emergência
                                                <input type='text' class='form-control' id='tlfemergenciaEdit' name='tlfemergenciaEdit' placeholder='Telefone de Emergência' required='required' style='width: 100%; margin: auto;' value='{5}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                Email
                                                <input type='text' class='form-control' id='emailEdit' name='emailEdit' placeholder='Email' required='required' style='width: 100%; margin: auto;' value='{12}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                Data de Nascimento
                                                <input type='text' class='form-control' id='datanascEdit' name='datanascEdit' placeholder='Data de Nascimento' required='required' style='width: 100%; margin: auto;' value='{7}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                CC Nº
                                                <input type='text' class='form-control' id='ccEdit' name='ccEdit' placeholder='Nº CC' required='required' style='width: 100%; margin: auto;' value='{8}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                CC Validade
                                                <input type='text' class='form-control' id='validadeccEdit' name='validadeccEdit' placeholder='Validade CC' required='required' style='width: 100%; margin: auto;' value='{9}'/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                                                Profissão
                                                <input type='text' class='form-control' id='profissaoEdit' name='profissao' placeholder='Profissão' required='required' style='width: 100%; margin: auto;' value='{10}'/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line' id='comercialDivEdit' runat='server'></div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                                                Sexo
                                                <form style='width:100%' id='sexoEdit' name='sexoEdit'>{11}</form>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 nopadding'>Não quer receber informação:</div>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8 nopadding'>
                                                    <input type='checkbox' class='form-control' id='publicidadeEdit' name='publicidadeEdit' style='width: 100%; margin: auto; height: 50px;'{23}/>
                                                </div>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 line'>
                                                Notas
                                                <textarea class='form-control' id='notasEdit' name='notasEdit' style='width: 100%; margin: auto; height: auto;' rows='5'>{18}</textarea>
                                            </div>
                                            <span class='variaveis' id='id_comercialedit'>{13}</span>
                                            <span class='variaveis' id='id_contratoedit'>{15}</span>
                                            <span class='variaveis' id='id_estadocontratoedit'>{17}</span>
                                            <span class='variaveis' id='id_tipocontratoedit'>{16}</span>",
                                                myReader["SOCIOSID"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["MORADA"].ToString().Replace("'", "#").Replace("\"", "@"),
                                                myReader["CODPOSTAL"].ToString(),
                                                myReader["LOCALIDADE"].ToString(),
                                                myReader["TLF_EMERGENCIA"].ToString(),
                                                myReader["TELEMOVEL"].ToString(),
                                                myReader["DATA_NASCIMENTO"].ToString(),
                                                myReader["CC_NR"].ToString(),
                                                myReader["VALIDADE_CC"].ToString(),
                                                myReader["PROFISSAO"].ToString(),
                                                s,
                                                myReader["EMAIL"].ToString(),
                                                myReader["COMERCIAL_ID"].ToString(),
                                                myReader["NR_SOCIO"].ToString(),
                                                myReader["CONTRATOID"].ToString(),
                                                myReader["TIPO_CONTRATO_ID"].ToString(),
                                                myReader["ESTADO_CONTRATO_ID"].ToString(),
                                                myReader["NOTAS_SOCIO"].ToString().Replace("'", "#").Replace("\"", "@"),
                                                myReader["DATA_INICIO"].ToString(),
                                                myReader["DATA_FIM"].ToString(),
                                                myReader["DEBITO_DIRETO"].ToString() == "1" ? "checked" : "",
                                                myReader["VALOR"].ToString().Replace(".", ","),
                                                myReader["NAO_QUER_RECEBER_PUBLICIDADE"].ToString() == "1" ? "checked" : "");
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem sócios com o número indicado.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem sócios com o número indicado.</div>");
            table.AppendFormat("{0}", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string loadComerciaisEdit(string id)
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
                                            WHERE LTRIM(RTRIM(CODIGO)) <> 'MCOELHO'
                                            AND LTRIM(RTRIM(CODIGO)) <> 'ALOURENCO'
                                            ORDER BY ADMINISTRADOR DESC");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"Comercial");
                table.AppendFormat(@"<select class='form-control' id='comercialEdit'>");
                while (myReader.Read())
                {
                    table.AppendFormat(@"<option value='{0}' {2}>{1}</option>", 
                        myReader["ID"].ToString(), 
                        myReader["NOME"].ToString(),
                        id == myReader["ID"].ToString() ? "selected" : "");
                }
                table.AppendFormat(@"</select>");

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"Utilizador não encontrado!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            return table.ToString();
        }
    }

    [WebMethod]
    public static string editar(string id_operador, string nome, string morada, string codpostal, string localidade, string tlf_emergencia, string tlm,
        string dataNascimento, string cc, string validadeCC, string profissao, string id_comercial, string sexo, string email, string id_socio, string nrSocio,
        string publicidade, string notas)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        notas = notas.Replace("''", "\"");
        morada = morada.Replace("''", "\"");
        notas = notas.Replace("#", "''");
        morada = morada.Replace("#", "''");

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @nome varchar(max) = '{1}';
	                                        DECLARE @morada varchar(max) = '{2}';
	                                        DECLARE @codpostal varchar(10) = '{3}';
	                                        DECLARE @localidade varchar(max) = '{4}';
	                                        DECLARE @tlf_emergencia varchar(200) = '{5}';
	                                        DECLARE @telemovel varchar(20) = '{6}';
	                                        DECLARE @data_nascimento datetime = '{7}';
	                                        DECLARE @cc_nr varchar(20) = '{8}';
	                                        DECLARE @validade_cc datetime = '{9}';
	                                        DECLARE @profissao varchar(max) = '{10}';
	                                        DECLARE @id_comercial int = {11};
	                                        DECLARE @sexo char(1) = '{12}';
	                                        DECLARE @email varchar(200) = '{13}';
	                                        DECLARE @id_novo_socio int = {14};
                                            DECLARE @nr_socio int = {15};
                                            DECLARE @code_op char(30) = (SELECT LTRIM(RTRIM(CODIGO)) FROM OPERADORES WHERE OPERADORESID = @id_operador);
                                            DECLARE @publicidade bit = {16};
                                            DECLARE @notaslog varchar(max);
                                            DECLARE @reslog int;
                                            DECLARE @notas varchar(max) = '{17}';

                                            IF(@nr_socio is null or @nr_socio = 0)
                                            BEGIN
                                                SET @nr_socio = ((SELECT MAX(NR_SOCIO) FROM SOCIOS)+1);
                                            END

                                            SET @notaslog = 'Foi alterado o sócio ' + ltrim(Rtrim(str(@nr_socio))) + ' pelo operador ' + @code_op;

                                            UPDATE SOCIOS
                                                SET NOME = @nome, 
	                                            MORADA = @morada,
	                                            CODPOSTAL = @codpostal,
	                                            LOCALIDADE = @localidade,
	                                            TLF_EMERGENCIA = @tlf_emergencia,
	                                            TELEMOVEL = @telemovel,
	                                            DATA_NASCIMENTO = @data_nascimento,
	                                            CC_NR = @cc_nr,
	                                            VALIDADE_CC = @validade_cc,
	                                            PROFISSAO = @profissao,
	                                            ID_COMERCIAL = @id_comercial,
	                                            SEXO = @sexo,
	                                            EMAIL = @email,
                                                NR_SOCIO = @nr_socio,
                                                CTRLDATA = getdate(),
	                                            CTRLCODOP = @code_op,
                                                NAO_QUER_RECEBER_PUBLICIDADE = @publicidade,
                                                NOTAS = @notas
                                            WHERE SOCIOSID = @id_novo_socio

                                            EXEC REGISTA_LOG @id_operador, 'ALTERARCLIENTE', @notaslog, @reslog output", id_operador, nome, morada, codpostal, localidade, tlf_emergencia, tlm,
                                                                        dataNascimento, cc, validadeCC, profissao, id_comercial, sexo, email, id_socio, nrSocio,
                                                                        publicidade, notas);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"Sócio {0} atualizado com sucesso!", nrSocio);
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao atualizar sócio {0}!", nrSocio);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao atualizar sócio {0}!", nrSocio);
        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string loadTiposContrato(string id)
    {
        StringBuilder table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_tipocontrato int = {0};
                                            SELECT
                                                TIPO_CONTRATOID,
	                                            LTRIM(RTRIM(CODIGO)) as CODIGO,
	                                            LTRIM(RTRIM(DESIGNACAO)) as DESIGNACAO
                                            from tipo_contrato
                                            order by CODIGO", id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"Tipo de Contrato");
                table.AppendFormat(@"<select class='form-control' id='tipoContratoEdit' onchange='loadFinalDate();'>");
                while (myReader.Read())
                {
                    table.AppendFormat(@"<option value='{0}' {2}>{1}</option>",
                        myReader["TIPO_CONTRATOID"].ToString(),
                        myReader["DESIGNACAO"].ToString(),
                        id == myReader["TIPO_CONTRATOID"].ToString() ? "selected" : "");
                }
                table.AppendFormat(@"</select>");

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"Utilizador não encontrado!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            return table.ToString();
        }
    }

    [WebMethod]
    public static string loadEstadosContrato(string id)
    {
        StringBuilder table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_estadocontrato int = {0};
                                            SELECT
                                                estados_contratoid,
	                                            LTRIM(RTRIM(CODIGO)) as CODIGO,
	                                            LTRIM(RTRIM(DESIGNACAO)) as DESIGNACAO
                                            from estados_contrato
                                            order by ATIVO", id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"Estado do Contrato");
                table.AppendFormat(@"<select class='form-control' id='estadoContratoEdit'>");
                while (myReader.Read())
                {
                    table.AppendFormat(@"<option value='{0}' {2}>{1}</option>",
                        myReader["estados_contratoid"].ToString(),
                        myReader["DESIGNACAO"].ToString(),
                        id == myReader["estados_contratoid"].ToString() ? "selected" : "");
                }
                table.AppendFormat(@"</select>");

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"Utilizador não encontrado!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            return table.ToString();
        }
    }


    [WebMethod]
    public static string editarContrato(string id_operador, string id_contrato, string id_tipocontrato, string id_estadocontrato, string data_inicio, string data_fim, 
        string debito_direto, string notas, string preco)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @id_contrato int = {1};
                                            DECLARE @id_tipo int = {2};
                                            DECLARE @id_estado int = {3};
                                            DECLARE @data_inicio datetime = '{4}';
                                            DECLARE @data_fim datetime = '{5}';
                                            DECLARE @debito_direto bit = {6};
                                            DECLARE @notas varchar(max) = '{7}';
                                            DECLARE @preco decimal(15,2) = {8};
                                            DECLARE @code_op char(30) = (SELECT LTRIM(RTRIM(CODIGO)) FROM OPERADORES WHERE OPERADORESID = @id_operador);
                                            DECLARE @nr_socio int = (select nr_socio from socios soc inner join contrato cont on cont.socio_id = soc.sociosid where contratoid = @id_contrato)
                                            DECLARE @notaslog varchar(max) = 'Foi alterado o contrato de ' + ltrim(rtrim(convert(varchar(10), @data_inicio, 103)))
                                            + ' a ' + ltrim(rtrim(convert(varchar(10), @data_fim, 103))) + ' no sócio ' + ltrim(rtrim(str(@nr_socio)))
                                            + ' pelo operador ' + @code_op;
                                            DECLARE @reslog int;
                                            declare @data_inicio_pagamento datetime
	                                        declare @data_fim_pagamento datetime
	                                        declare @diferenca_meses int
	                                        declare @val decimal(15,2)
	                                        declare @table TABLE (mes int, ano int, id_contrato int, valor decimal(15,2))
	                                        declare @i int = 0;

                                            UPDATE CONTRATO
                                                SET TIPO_CONTRATO_ID = @id_tipo, 
	                                            ESTADO_CONTRATO_ID = @id_estado,
	                                            NOTAS = @notas,
	                                            DATA_INICIO = @data_inicio,
	                                            DATA_FIM = @data_fim,
	                                            DEBITO_DIRETO = @debito_direto,
                                                VALOR = @preco,
	                                            CTRLDATA = GETDATE(),
	                                            CTRLCODOP = @code_op
                                            WHERE CONTRATOID = @id_contrato

                                            select 
						                        @data_inicio_pagamento = '1-' + CONVERT(VARCHAR(2), MONTH(DATA_INICIO)) + '-' + CONVERT(VARCHAR(4), YEAR(DATA_INICIO)) + ' 00:00:00',
						                        @data_fim_pagamento = '1-' + CONVERT(VARCHAR(2), MONTH(DATA_FIM)) + '-' + CONVERT(VARCHAR(4), YEAR(DATA_FIM)) + ' 00:00:00',
						                        @val = VALOR
					                        from contrato where contratoid = @id_contrato
			    
					                        SET @diferenca_meses = (SELECT DATEDIFF(month, @data_inicio_pagamento, @data_fim_pagamento))

                                            WHILE @i <= @diferenca_meses
					                        BEGIN
						                        INSERT INTO @table(mes, ano, id_contrato, valor)
						                        SELECT 
						                            MONTH(DATEADD(month, @i, DATA_INICIO)), 
						                            YEAR(DATEADD(month, @i, DATA_INICIO)), 
						                            CONTRATOID,
						                            @val
						                        from contrato where contratoid = @id_contrato
					       
						                        SET @i = @i + 1;
					                        END;

                                            DELETE FROM PAGAMENTOS
				                            WHERE PAGAMENTOSID IN (
					                        select pag.pagamentosid from pagamentos pag
					                        left join @table novo on novo.mes = pag.mes and novo.ano = pag.ano
					                        where novo.id_contrato is null
					                        and pag.contrato_id IN (SELECT DISTINCT id_contrato from @table)
					                        and pag.id_status is null
				                            )
				    
				                            INSERT INTO PAGAMENTOS(CONTRATO_ID, MES, ANO, VALOR)
				                            select novo.id_contrato, novo.mes, novo.ano, novo.valor 
				                            from @table novo
				                            left join pagamentos pag on novo.mes = pag.mes and novo.ano = pag.ano and pag.contrato_id = novo.id_contrato
				                            where pag.contrato_id is null

                                            EXEC REGISTA_LOG @id_operador, 'ALTERARCLIENTE', @notaslog, @reslog output;", id_operador, id_contrato, id_tipocontrato, id_estadocontrato, data_inicio, data_fim, 
                                                                            debito_direto, notas, preco);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"Contrato atualizado com sucesso!");
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao atualizar contrato!");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao atualizar contrato!");
        connection.Close();
        return table.ToString();
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

    [WebMethod]
    public static string loadPrecoTipoContrato(string id_tipocontrato)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_tipocontrato int = {0};

                                            SELECT PRECO FROM TIPO_CONTRATO WHERE TIPO_CONTRATOID = @id_tipocontrato", id_tipocontrato);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"{0}", myReader["PRECO"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"");
        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string apagaSocio(string id_operador, string id_socio, string id_contrato)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @id_contrato int = {1};
                                            DECLARE @id_socio int = {2};
                                            DECLARE @ret int;

                                            EXEC APAGA_SOCIO @id_operador, @id_socio, @id_contrato, @ret output

                                            SELECT @ret as ret", id_operador, id_contrato, id_socio);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if(Convert.ToInt32(myReader["ret"].ToString()) == 0)
                        table.AppendFormat(@"Sócio removido com sucesso!");
                    else
                        table.AppendFormat(@"Erro ao remover sócio do sistema!");
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"Erro ao remover sócio do sistema!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao remover sócio do sistema!");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao remover sócio do sistema!");
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
                                            DECLARE @subpagina varchar(400) = 'Alterar Cliente';

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
}
