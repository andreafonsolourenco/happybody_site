using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.IO;

public partial class CustomerInfo : Page
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

        loadMesLimiteCalendario();
        loadPermissoes();
        loadListagemSociosEmail();
    }

    private void loadMesLimiteCalendario()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            declare @data_atual datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 23:59:59';
                                            declare @data_inicio_mes datetime = DATEADD(dd, ((DAY(@data_atual) - 1) * (-1)), @data_atual);
                                            declare @data2anterior datetime = CONVERT(VARCHAR(10), DATEADD(mm, -2, @data_inicio_mes), 103) + ' 00:00:00';

                                            select MONTH(@data_inicio_mes) as mes_limite, YEAR(@data_inicio_mes) as ano_limite");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    lblmeslimitecalendario.InnerHtml = myReader["mes_limite"].ToString();
                    lblanolimitecalendario.InnerHtml = myReader["ano_limite"].ToString();
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
    public static string load(string id_operador, string filtro, string query)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string declares = "";

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @FILTRO VARCHAR(MAX) = {1};

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
                                            ORDER BY soc.NR_SOCIO", id_operador, 
                                                                  filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro), 
                                                                  query,
                                                                  declares);

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
                    // Adiciona as linhas com dados
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
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem sócios a apresentar. {0}</div>", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string getCustomerData(string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_socio int = {0};

                                            SELECT 
                                                soc.SOCIOSID, 
                                                soc.NOME,
	                                            soc.MORADA,
	                                            soc.CODPOSTAL,
	                                            soc.LOCALIDADE,
	                                            soc.TLF_EMERGENCIA,
	                                            soc.TELEMOVEL,
	                                            CONVERT(VARCHAR(10), CAST(soc.DATA_NASCIMENTO as DATE), 103) as DATA_NASCIMENTO,
	                                            soc.CC_NR,
                                                CONVERT(VARCHAR(10), CAST(soc.VALIDADE_CC as DATE), 103) as VALIDADE_CC,
	                                            soc.PROFISSAO,
	                                            op.NOME as COMERCIAL,
                                                op.OPERADORESID as COMERCIAL_ID,
                                                soc.SEXO,
	                                            soc.EMAIL,
                                                soc.NR_SOCIO,
                                                CAST(soc.NAO_QUER_RECEBER_PUBLICIDADE as INT) as NAO_QUER_RECEBER_PUBLICIDADE,

                                                CASE WHEN rpt.id_contrato is null
                                                THEN (select top 1 contratoid from contrato where socio_id = soc.sociosid order by data_fim desc)
                                                ELSE rpt.id_contrato
                                                END as CONTRATOID,
                                                soc.NOTAS,
                                                CASE WHEN rpt.id_contrato is null
                                                THEN (select top 1 CAST(DEBITO_DIRETO as INT) from contrato where socio_id = soc.sociosid order by data_fim desc)
                                                ELSE CAST(rpt.DEBITO_DIRETO as INT)
                                                END as DEBITO_DIRETO,
                                                CASE 
						                        WHEN rpt.DATA_INICIO is null 
						                        THEN (select top 1 CONVERT(VARCHAR(10), DATA_INICIO, 103) from contrato where socio_id = soc.sociosid order by data_fim desc) 
						                        ELSE CONVERT(VARCHAR(10), rpt.DATA_INICIO, 103) 
						                        END as DATA_INICIO,
						                        CASE 
						                        WHEN rpt.DATA_FIM is null 
						                        THEN (select top 1 CONVERT(VARCHAR(10), DATA_FIM, 103) from contrato where socio_id = soc.sociosid order by data_fim desc) 
						                        ELSE CONVERT(VARCHAR(10), rpt.DATA_FIM, 103) 
						                        END as DATA_FIM,
                                                CASE WHEN rpt.id_contrato is null
                                                THEN (select top 1 designacao from contrato inner join tipo_contrato on tipo_contratoid = tipo_contrato_id where socio_id = soc.sociosid order by data_fim desc)
                                                ELSE tp.DESIGNACAO
                                                END as TIPO_CONTRATO,
                                                CASE WHEN rpt.id_contrato is null
                                                THEN (select top 1 designacao from contrato inner join estados_contrato on estados_contratoid = estado_contrato_id where socio_id = soc.sociosid order by data_fim desc)
                                                ELSE tp.DESIGNACAO
                                                END as ESTADO_CONTRATO
                                            FROM SOCIOS soc
                                            INNER JOIN OPERADORES op on op.OPERADORESID = soc.ID_COMERCIAL
					                        LEFT JOIN REPORT_CONTRATOS_DENTRO_DATA(null, null) rpt on rpt.id_socio = soc.sociosid
                                            LEFT JOIN TIPO_CONTRATO tp on tp.tipo_contratoid = rpt.id_tipo
                                            LEFT JOIN ESTADOS_CONTRATO st on st.estados_contratoid = rpt.id_estado
                                            WHERE @id_socio = soc.SOCIOSID", id);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    string s = "";

                    if (myReader["SEXO"].ToString() == "M")
                    {
                        s = "<label class='radio-inline'><input type='radio' name='optradioEdit' value='M' checked='checked' disabled>Masculino</label><label class='radio-inline'><input type='radio' name='optradioEdit' value='F' disabled>Feminino</label>";
                    }
                    else
                    {
                        s = "<label class='radio-inline'><input type='radio' name='optradioEdit' value='M' disabled>Masculino</label><label class='radio-inline'><input type='radio' name='optradioEdit' value='F' checked='checked' disabled>Feminino</label>";
                    }

                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:left; margin-top: 5px;'>
                                                <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>
                                                    <input id='btnPrevious' value='Anterior' runat='server' type='button' onclick='previous();' class='btnTop'/>
                                                    <input id='btnNext' value='Seguinte' runat='server' type='button' onclick='next();' class='btnTop'/>
                                                </div>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:right; margin-top: 5px;'>
                                                <img src='img/icons/icon_close.png' style='cursor:pointer;' onclick='closeCustomerInfo();'/>
                                            </div>
                                            <div class='col-lg-10 col-md-10 col-sm-10 col-xs-10' style='text-align:left; font-family: 'Noto Sans', sans-serif !important; height:100%'>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8' id='divDados'>
                                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 line row row-no-padding'>
                                                        <span style='font-weight: bold'>Sócio Nº {14}:</span>
                                                        <input type='text' class='form-control' id='nameEdit' name='nameEdit' placeholder='Nome' required='required' style='width: 100%; margin: auto;' value='{1}' readonly/>
                                                    </div>
                                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 line row row-no-padding'>
                                                        Morada:
                                                        <input type='text' class='form-control' id='moradaEdit' name='moradaEdit' placeholder='Morada' required='required' style='width: 100%; margin: auto;' value='{2}' readonly/>
                                                    </div>
                                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 line row row-no-padding'>
                                                        Localidade:
                                                        <input type='text' class='form-control' id='codpostalEdit' name='codpostalEdit' placeholder='Código Postal: xxxx-xxx' required='required' style='width: 100%; margin: auto;' value='{3} {4}' readonly/>
                                                    </div>
                                                </div>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4' id='divFotoSocio' style='-moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important; text-align: center; background-repeat: no-repeat; background-size: 100% 100%;'>
                                                    <img src='img/socios/{14}.jpg' id='img_socio' style='width:100%' alt='Não existe fotografia associada ao Sócio {14}'/>
                                                </div>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                    Telefone:
                                                    <input type='text' class='form-control' id='tlfEdit' name='tlfEdit' placeholder='Telefone' required='required' style='width: 100%; margin: auto; float: left' value='{6}' readonly/>
                                                </div>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                    Tlf Emergência:
                                                    <input type='text' class='form-control' id='tlfemergenciaEdit' name='tlfemergenciaEdit' placeholder='Telefone de Emergência' required='required' style='width: 100%; margin: auto;' value='{5}' readonly/>
                                                </div>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                    Email:
                                                    <input type='text' class='form-control' id='emailEdit' name='emailEdit' placeholder='Email' required='required' style='width: 100%; margin: auto;' value='{12}' readonly/>
                                                </div>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                    Data Nasc:
                                                    <input type='text' class='form-control' id='datanascEdit' name='datanascEdit' placeholder='Data de Nascimento' required='required' style='width: 100%; margin: auto;' value='{7}' readonly/>
                                                </div>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                    Nr CC:
                                                    <input type='text' class='form-control' id='ccEdit' name='ccEdit' placeholder='Nº CC' required='required' style='width: 100%; margin: auto;' value='{8}' readonly/>
                                                </div>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                    Validade CC:
                                                    <input type='text' class='form-control' id='validadeccEdit' name='validadeccEdit' placeholder='Validade CC' required='required' style='width: 100%; margin: auto;' value='{9}' readonly/>
                                                </div>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                    Profissão:
                                                    <input type='text' class='form-control' id='profissaoEdit' name='profissao' placeholder='Profissão' required='required' style='width: 100%; margin: auto;' value='{10}' readonly/>
                                                </div>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line' id='comercialDivEdit' runat='server'>
                                                    Comercial:
                                                    <select class='form-control' id='comercial' style='width:100%'>
                                                        <option value='{22}'>{22}</option>
                                                    </select>
                                                </div>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                    Sexo:
                                                    <form style='width:100%' id='sexoEdit' name='sexoEdit'>{11}</form>
                                                </div>
                                                <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line' id='tipoContratoDivEdit' runat='server'>
                                                    Tipo de Contrato:
                                                    <select class='form-control' id='tipoContrato' style='width:100%'>
                                                        <option value='{16}'>{16}</option>
                                                    </select>
                                                </div>
                                                <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line' id='estadoContratoDivEdit' runat='server'>
                                                    Estado do Contrato:
                                                    <select class='form-control' id='estadoContrato' style='width:100%'>
                                                        <option value='{17}'>{17}</option>
                                                    </select>
                                                </div>
                                                <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                                                    <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 nopadding'>Data Início:</div>
                                                    <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8 nopadding'>
                                                        <input type='text' class='form-control' id='dataInícioEdit' name='dataInícioEdit' placeholder='Data Início' value='{19}' readonly/>
                                                    </div>
                                                </div>
                                                <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                                                    <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 nopadding'>Data Fim:</div>
                                                    <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8 nopadding'>
                                                        <input type='text' class='form-control' id='dataFimEdit' name='dataFimEdit' placeholder='Data Fim' value='{20}'readonly/>
                                                    </div>
                                                </div>
                                                <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                                                    <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 nopadding'>Débito Direto:</div>
                                                    <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8 nopadding'>
                                                        <input type='checkbox' class='form-control' id='debitoDiretoEdit' name='debitoDiretoEdit' style='width: 100%; margin: auto; height: 50px;'{21} readonly disabled/>
                                                    </div>
                                                </div>
                                                <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                                                    <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 nopadding'>Não deseja receber informação:</div>
                                                    <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8 nopadding'>
                                                        <input type='checkbox' class='form-control' id='naodesejareceberpublicidadeedit' name='naodesejareceberpublicidadeedit' style='width: 100%; margin: auto; height: 50px;'{23} readonly disabled/>
                                                    </div>
                                                </div>
                                                <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 line'>
                                                    Notas:
                                                    <textarea class='form-control' id='notasEdit' name='notasEdit' style='width: 100%; margin: auto; height: auto;' rows='5' readonly>{18}</textarea>
                                                </div>
                                            </div>
                                            <div class='col-lg-2 col-md-2 col-sm-2 col-xs-2 nopadding' style='text-align:center; height:100%' id='divCalendar'>
                                                
                                            </div>
                                            <span id='fotoSocio' class='variaveis'>img/socios/{14}.jpg</span>
                                            <span id='nr_socio' class='variaveis'>{14}</span>",
                                                myReader["SOCIOSID"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["MORADA"].ToString(),
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
                                                myReader["TIPO_CONTRATO"].ToString(),
                                                myReader["ESTADO_CONTRATO"].ToString(),
                                                myReader["NOTAS"].ToString(),
                                                myReader["DATA_INICIO"].ToString(),
                                                myReader["DATA_FIM"].ToString(),
                                                myReader["DEBITO_DIRETO"].ToString() == "1" ? "checked" : "",
                                                myReader["COMERCIAL"].ToString(),
                                                myReader["NAO_QUER_RECEBER_PUBLICIDADE"].ToString() == "1" ? "checked" : "");
                }
                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:right; margin-top: 5px;'>
                                            <img src='img/icon_close.png' style='cursor:pointer;' onclick='closeCustomerInfo();'/>
                                        </div>
                                        <div style='height:auto' class='panel-heading' id='panel-heading'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem sócios a apresentar.</span>
                                        </div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:right; margin-top: 5px;'>
                                            <img src='img/iconsicon_close.png' style='cursor:pointer;' onclick='closeCustomerInfo();'/>
                                        </div>
                                        <div style='height:auto' class='panel-heading' id='panel-heading'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem sócios a apresentar. {0}</span>
                                        </div>", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static void saveNotes(string id, string notas)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id int = {0};
                                            DECLARE @notas varchar(MAX) = '{1}';

                                            UPDATE SOCIOS
                                            SET NOTAS = @notas
                                            WHERE @id = SOCIOSID", id, notas);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
        }
        catch (Exception exc)
        {
            
        }
    }

    [WebMethod]
    public static string loadDatasEntradasSocio(string nr_socio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   declare @nr_socio int = {0};
                                            declare @data_atual datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 23:59:59';
                                            declare @data_inicio_mes datetime = DATEADD(dd, ((DAY(@data_atual) - 1) * (-1)), @data_atual);
                                            declare @data2anterior datetime = CONVERT(VARCHAR(10), DATEADD(mm, -2, @data_inicio_mes), 103) + ' 00:00:00';

                                            select distinct 
                                                convert(varchar(10), dateadd(hh, -1, data_entrada), 120) as data_entrada,
                                                YEAR(dateadd(hh, -1, data_entrada)) ano, 
                                                MONTH(dateadd(hh, -1, data_entrada)) mes, 
                                                DAY(dateadd(hh, -1, data_entrada)) dia
                                            from socios soc
                                            inner join entradas ent on ent.id_socio = soc.sociosid
                                            where @nr_socio = soc.nr_socio
                                            and data_entrada >= @data2anterior
                                            and data_entrada <= @data_atual
                                            order by YEAR(dateadd(hh, -1, data_entrada)) desc, MONTH(dateadd(hh, -1, data_entrada)) desc, DAY(dateadd(hh, -1, data_entrada)) desc", nr_socio);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <span class='variaveis' id='dataEntrada_{0}'>{1}</span>
                                            <span class='variaveis' id='anoEntrada_{0}'>{2}</span>
                                            <span class='variaveis' id='mesEntrada_{0}'>{3}</span>
                                            <span class='variaveis' id='diaEntrada_{0}'>{4}</span>", conta.ToString()
                                                                                                  , myReader["data_entrada"].ToString()
                                                                                                  , myReader["ano"].ToString()
                                                                                                  , myReader["mes"].ToString()
                                                                                                  , myReader["dia"].ToString());

                    conta++;
                }

                // Adiciona as linhas com dados
                table.AppendFormat(@"   <span class='variaveis' id='nrTotalEntradas'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                // Adiciona as linhas
                table.AppendFormat(@"");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            // Adiciona as linhas
            table.AppendFormat(@"");
            connection.Close();
            return table.ToString();
        }
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
                                            DECLARE @subpagina varchar(400) = 'Ficha de Cliente';

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

    private void loadListagemSociosEmail()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT 
                                                soc.SOCIOSID, 
                                                soc.NOME,
                                                soc.NR_SOCIO,
                                                soc.EMAIL,
                                                soc.TELEMOVEL
                                            FROM SOCIOS soc
                                            INNER JOIN OPERADORES op on op.OPERADORESID = soc.ID_COMERCIAL
					                        INNER JOIN REPORT_CONTRATOS_DENTRO_DATA(null, null) rpt on rpt.id_socio = soc.sociosid
                                            ORDER BY soc.NR_SOCIO");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGridListagem'>
                                            <thead>
						                        <tr>
                                                    <th class='headerLeft'>Sócio</th>
                                                    <th class='headerRight'>Email</th>
                                                    <th class='headerRight'>Telefone</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr>
                                            <td class='tbodyTrTdLeft'>{2} - {1}</td>
                                            <td class='tbodyTrTdRight'>{3}</td>
                                            <td class='tbodyTrTdRight'>{4}</td>
                                        </tr>",
                                                myReader["SOCIOSID"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["NR_SOCIO"].ToString(),
                                                myReader["EMAIL"].ToString(),
                                                myReader["TELEMOVEL"].ToString());
                }

                table.AppendFormat("</tbody></table></div>");

                connection.Close();
                listagemSociosComEmail.InnerHtml = table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem sócios a apresentar.</div>");
                connection.Close();
                listagemSociosComEmail.InnerHtml = table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem sócios a apresentar. {0}</div>", exc.ToString());
            connection.Close();
            listagemSociosComEmail.InnerHtml = table.ToString();
        }
    }
}
