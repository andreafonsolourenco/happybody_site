using System;
using System.Web.UI;
using System.Web.Services;
using System.Text;
using System.Web;
using System.Data.SqlClient;

public partial class Seguros : Page
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
    public static string load(string id_operador, string filtro, string query, string dentroPrazo, string foraPrazo, string pago, string naoPago, string acabarMesCorrente)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = "";
            
            if(filtro == "") {
                sql = string.Format(@"  SET DATEFORMAT DMY;
                                        DECLARE @dentroPrazo bit = {0};
                                        DECLARE @foraPrazo bit = {1};
                                        DECLARE @pago bit = {2};
                                        DECLARE @naoPago bit = {3};
                                        DECLARE @acabaEsteMes bit = {4};
                                        DECLARE @nr_socio int;
                                        DECLARE @dataatual datetime = dateadd(hh, -1, getdate());

                                        SELECT 
                                            id_seguro,
	                                        id_socio,
	                                        data_inicio,
	                                        data_fim,
	                                        pago,
	                                        notas,
	                                        op_ultima_modificacao,
	                                        data_ultima_modificao,
	                                        nr_socio,
	                                        nome,
                                            renovacao_paga
                                        FROM REPORT_SEGUROS(@nr_socio)
                                        WHERE (@pago is null or pago = 1)
                                        AND (@naoPago is null or pago = 0)
                                        AND (@dentroPrazo is null or (CAST(data_inicio as DATE) <= CAST((DATEADD(hh, -1, GETDATE())) as DATE) AND CAST(data_fim as DATE) >= CAST((DATEADD(hh, -1, GETDATE())) as DATE)))
                                        AND (@foraPrazo is null or (CAST(data_inicio as DATE) > CAST((DATEADD(hh, -1, GETDATE())) as DATE) OR CAST(data_fim as DATE) < CAST((DATEADD(hh, -1, GETDATE())) as DATE)))
                                        AND (@acabaEsteMes is null or 
						                        (   MONTH(CAST(data_fim as DATE)) = MONTH(CAST(@dataatual as DATE)) 
						                            and 
						                            YEAR(CAST(data_fim as DATE)) = YEAR(CAST(@dataatual as DATE))
						                        )
					                        )
                                        AND ativo = 1
                                        ORDER BY nr_socio", dentroPrazo == "" ? "NULL" : dentroPrazo
                                                          , foraPrazo == "" ? "NULL" : foraPrazo
                                                          , pago == "" ? "NULL" : pago
                                                          , naoPago == "" ? "NULL" : naoPago
                                                          , acabarMesCorrente == "" ? "NULL" : acabarMesCorrente);
            }
            else {
              sql = string.Format(@"   SET DATEFORMAT DMY;
                                       DECLARE @id_operador int = {0};
                                       DECLARE @FILTRO varchar(MAX) = {1};
                                       DECLARE @nr_socio int;

                                       SELECT 
                                           id_seguro,
	                                       id_socio,
	                                       data_inicio,
	                                       data_fim,
	                                       pago,
	                                       notas,
	                                       op_ultima_modificacao,
	                                       data_ultima_modificao,
	                                       nr_socio,
	                                       nome,
                                           renovacao_paga
                                       FROM REPORT_SEGUROS(@nr_socio)
                                       WHERE (@FILTRO IS NULL OR ({2}))
                                       ORDER BY nr_socio", id_operador, filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro), query);
            }

            //return sql;
            
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
                                                    <th class='headerLeft'>Sócio</th>
                                                    <th class='headerRight'>Pago</th>
                                                    <th class='variaveis'>ID</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    table.AppendFormat(@"<tr ondblclick='openCustomerInfo({0}, {3});'>
                                            <td class='tbodyTrTdLeft'>
                                                {3} - {1}<br />
                                                <span style='font-size: small'>{5} - {6}</span>
                                            </td>
                                            <td class='tbodyTrTdRight'>
                                                <input type='checkbox' class='form-control' style='width: 100%; height: auto; margin: auto;' {4} readonly/>
                                            </td>
                                            <td style='display:none;' id='id_{2}'>{0}</td>
                                        </tr>",
                                                myReader["id_socio"].ToString(),
                                                myReader["nome"].ToString(),
                                                conta.ToString(),
                                                myReader["nr_socio"].ToString(),
                                                myReader["pago"].ToString() == "1" ? "checked" : "",
                                                myReader["data_inicio"].ToString(),
                                                myReader["data_fim"].ToString());

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
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>{0}</div>", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string getCustomerData(string nr_socio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @nr_socio int = {0};

                                            SELECT 
                                                id_seguro,
	                                            id_socio,
	                                            data_inicio,
	                                            data_fim,
	                                            pago,
	                                            notas,
	                                            op_ultima_modificacao,
	                                            data_ultima_modificao,
	                                            nr_socio,
	                                            nome,
                                                renovacao_paga
                                            FROM REPORT_SEGUROS(@nr_socio)", nr_socio);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <div class='col-lg-12 col-nmd-12 col-sm-12 col-xs-12'>
                                                <input id='btnSave' value='Guardar' runat='server' type='button' onclick='updateInfo();' style='background-color: #4282b5; float: left;
                                                        width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                                <img src='img/icons/icon_close.png' style='cursor:pointer; float: right; vertical-align: middle;' onclick='back();'/>
                                            </div>
                                            <div class='col-lg-2 col-md-2 col-sm-2 col-xs-2 line'>
                                                Nº Sócio
                                                <input type='text' class='form-control' id='nrSocioEdit' name='nrSocio' placeholder='Nº' required='required' style='width: 100%; margin: auto;' value='{8}' readonly/>
                                            </div>
                                            <div class='col-lg-10 col-md-10 col-sm-10 col-xs-10 line'>
                                                Nome
                                                <input type='text' class='form-control' id='nameEdit' name='nameEdit' placeholder='Nome' required='required' style='width: 100%; margin: auto;' value='{9}' readonly/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                                                Data Início
                                                <input type='text' class='form-control' id='dataInicioEdit' name='dataInicio' placeholder='Data Início' required='required' style='width: 100%; margin: auto;' value='{2}'/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                                                Data Fim
                                                <input type='text' class='form-control' id='dataFimEdit' name='dataFim' placeholder='Data Fim' required='required' style='width: 100%; margin: auto;' value='{3}'/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                                                Pago
                                                <input type='checkbox' class='form-control' id='pagoEdit' name='pago' style='width: 100%; margin: auto;' {4}/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                                                Renovação Paga
                                                <input type='checkbox' class='form-control' id='renovacao_pagaEdit' name='renovacao_paga' style='width: 100%; margin: auto;' {10}/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                                                Op. Última Alteração
                                                <input type='text' class='form-control' id='opUltimaAlteracaoEdit' name='opUltimaAlteracao' placeholder='Op. Última Alteração' required='required' style='width: 100%; margin: auto;' value='{6}' readonly/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                                                Data Última Alteração
                                                <input type='text' class='form-control' id='dataUltimaAlteracaoEdit' name='dataUltimaAlteracao' placeholder='Data Última Alteração' required='required' style='width: 100%; margin: auto;' value='{7}' readonly/>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 line'>
                                                Notas
                                                <textarea class='form-control' id='notasEdit' name='notas' style='width: 100%; margin: auto; height: auto;' rows='5'>{5}</textarea>
                                            </div>
                                            <span class='variaveis' id='id_seguro'>{0}</span>
                                            <span class='variaveis' id='id_socio'>{1}</span>",
                                                myReader["id_seguro"].ToString(),
                                                myReader["id_socio"].ToString(),
                                                myReader["data_inicio"].ToString(),
                                                myReader["data_fim"].ToString(),
                                                myReader["pago"].ToString() == "1" ? "checked" : "",
                                                myReader["notas"].ToString(),
                                                myReader["op_ultima_modificacao"].ToString(),
                                                myReader["data_ultima_modificao"].ToString(),
                                                myReader["nr_socio"].ToString(),
                                                myReader["nome"].ToString(),
                                                myReader["renovacao_paga"].ToString() == "1" ? "checked" : "");
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe seguro para o sócio indicado.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe seguro para o sócio indicado.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string loadFinalDate(string data_inicio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
	                                        DECLARE @data_inicio datetime = '{0}';
                                            SET @data_inicio = DATEADD(hh, 12, @data_inicio);
                                            DECLARE @data_fim datetime = DATEADD(dd, -1, (DATEADD(yy, 1, @data_inicio)));

                                            SELECT CONVERT(VARCHAR(10), CAST(@data_fim as DATE), 103) as data_fim", data_inicio);

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
    public static string updateSeguro(string id_operador, string id_seguro, string id_socio, string data_inicio, string data_fim, string pago, string notas, string renovacao_paga)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @id_seguro int = {1};
                                            DECLARE @id_socio int = {2};
	                                        DECLARE @data_inicio datetime = '{3}';
                                            DECLARE @data_fim datetime = '{4}';
                                            DECLARE @pago bit = {5};
                                            DECLARE @notas varchar(max) = '{6}';
                                            DECLARE @renovacao_paga bit = {7};
                                            DECLARE @ret int

                                            EXEC UPDATE_SEGURO @id_operador, @id_seguro, @id_socio, @data_inicio, @data_fim, @pago, @notas, @renovacao_paga, @ret output
                                            SELECT @ret as ret", id_operador, id_seguro, id_socio, data_inicio, data_fim, pago, notas, renovacao_paga);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if(Convert.ToInt32(myReader["ret"].ToString()) == 0)
                        table.AppendFormat(@"Seguro alterado com sucesso!");
                    else
                        table.AppendFormat(@"Ocorreu um erro ao alterar seguro do sócio!");
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"Ocorreu um erro ao alterar seguro do sócio!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Ocorreu um erro ao alterar seguro do sócio!");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Ocorreu um erro ao alterar seguro do sócio!");
        connection.Close();
        return table.ToString();
    }
}
