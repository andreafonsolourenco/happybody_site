using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Contratos : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "ContractStatus"))
            {
                
            }
        }

        loadPermissoes();
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
                                            DECLARE @subpagina varchar(400) = 'Contratos';

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
    public static string load(string nr_socio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @nr_socio int = {0};
                                            DECLARE @id_socio int;
                                            DECLARE @id_contrato int;

                                            select distinct
	                                            id_socio,
	                                            nome,
	                                            nr_socio,
	                                            id_contrato,
	                                            notas,
	                                            convert(varchar(10), data_inicio, 103) as data_inicio,
	                                            convert(varchar(10), data_fim, 103) as data_fim,
	                                            debito_direto,
	                                            valor,
	                                            id_estado,
	                                            codigo_estado,
	                                            estado,
	                                            id_tipo,
	                                            codigo_tipo,
	                                            tipo,
                                                YEAR(data_fim), 
                                                MONTH(data_fim), 
                                                DAY(data_fim),
                                                tag_pagamentos,
                                                case when data_agendamento_nr is null then '' else convert(varchar(10), data_agendamento_nr, 103) end as data_agendamento_nr,
                                                case when data_agendamento_fim is null then '' else convert(varchar(10), data_agendamento_fim, 103) end as data_agendamento_fim,
                                                case when data_agendamento_reativacao is null then '' else convert(varchar(10), data_agendamento_reativacao, 103) end as data_agendamento_reativacao,
                                                case when data_agendamento_cancelamento is null then '' else convert(varchar(10), data_agendamento_cancelamento, 103) end as data_agendamento_cancelamento
                                            FROM REPORT_CONTRATO(@id_socio, @id_contrato, @nr_socio)
                                            ORDER BY YEAR(DATA_FIM) desc, MONTH(DATA_FIM) desc, DAY(DATA_FIM) desc", nr_socio);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (conta == 0)
                    {
                        table.AppendFormat(@"<input type='button' class='form-control' value='Criar Novo Contrato' style='width: 100%; height: auto; margin-bottom: 10px' onclick='openPopup({0}, {1});' />", myReader["ID_CONTRATO"].ToString(), nr_socio);
                    }

                    table.AppendFormat(@"<div id='divContrato{0}' {1}>", conta.ToString(), conta > 0 ? "style='display:none;'" : "");
                    table.AppendFormat(@"<span id='id_contrato{0}' style='display:none'>{1}</span>", conta.ToString(), myReader["id_contrato"].ToString());

                    table.AppendFormat(@"   <div class='headerTitle' id='headerContrato{7}'>
                                                <div class='col-lg-2 col-md-2 col-sm-2 col-xs-2' style='height: 100%; text-align: center; cursor: pointer;' onclick='fadeContracts({7}, -1);'>
                                                    <img src='img/icons/icon_arrow_left.png' onclick='' style='height: 100%' id='icon_left_{7}'/>
                                                </div>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8' style='height: 100%; text-align: center;'>
                                                    {0} - {1}<br />{2} - {3} ({4} €)<br />Contrato: {6} - {5}<br />{8}
                                                </div>
                                                <div class='col-lg-2 col-md-2 col-sm-2 col-xs-2' style='height: 100%; text-align: center; cursor: pointer;' onclick='fadeContracts({7}, 1);'>
                                                    <img src='img/icons/icon_arrow_right.png' onclick='' style='height: 100%' id='icon_right_{7}'/>
                                                </div>
                                            </div>",
                            myReader["nr_socio"].ToString(),
                            myReader["nome"].ToString(),
                            myReader["data_inicio"].ToString(),
                            myReader["data_fim"].ToString(),
                            myReader["valor"].ToString(),
                            myReader["estado"].ToString(),
                            myReader["tipo"].ToString(),
                            conta.ToString(),
                            myReader["tag_pagamentos"].ToString());

                    table.AppendFormat(@"   <span id='idTipoContrato{0}' class='variaveis'>{1}</span>
                                            <span id='idEstadoContrato{0}' class='variaveis'>{2}</span>", conta.ToString(), 
                                                                                                        myReader["id_tipo"].ToString(), 
                                                                                                        myReader["id_estado"].ToString());

                    table.AppendFormat(@"   <div class='nopadding col-lg-12 col-md-12 col-sm-12 col-xs-12' id='tipoContratoDiv{0}'></div>", conta.ToString());
                    table.AppendFormat(@"   <div class='nopadding col-lg-12 col-md-12 col-sm-12 col-xs-12' id='estadoContratoDiv{0}'></div>", conta.ToString());
                    table.AppendFormat(@"   <div class='nopadding col-lg-6 col-md-6 col-sm-6 col-xs-6' id='dataInicioDiv{0}'>
                                                Data de Início
                                                <input type='text' class='form-control' placeholder='Data de Início' style='width: 100%; margin-bottom: 5px;' id='dataInicio{0}' value='{1}' />
                                            </div>", conta.ToString(), myReader["data_inicio"].ToString());
                    table.AppendFormat(@"   <div class='nopadding col-lg-6 col-md-6 col-sm-6 col-xs-6' id='dataFimDiv{0}'>
                                                Data de Fim
                                                <input type='text' class='form-control' placeholder='Data de Fim' style='width: 100%; margin-bottom: 5px;' id='dataFim{0}' value='{1}' />
                                            </div>", conta.ToString(), myReader["data_fim"].ToString());
                    table.AppendFormat(@"   <div class='nopadding col-lg-6 col-md-6 col-sm-6 col-xs-6' id='valorDiv{0}'>
                                                Valor
                                                <div style='width:100%;' class='input-group' style='margin-bottom: 5px;'>
                                                    <input type='number' class='form-control' id='valor{0}' placeholder='Valor' style='width: 100%; height: 34px;' aria-describedby='basic-addon-valor' value='{1}'/>
                                                    <span class='input-group-addon addon-euro' id='basic-addon-valor'>€</span>
                                                </div>
                                            </div>", conta.ToString(), myReader["valor"].ToString());
                    table.AppendFormat(@"   <div class='nopadding col-lg-6 col-md-6 col-sm-6 col-xs-6' id='debitoDiretoDiv{0}'>
                                                Débito Direto
                                                <input type='checkbox' class='form-control' style='margin-bottom: 5px;' id='debitoDireto{0}' {1}/>
                                            </div>", conta.ToString(), myReader["debito_direto"].ToString() == "1" ? "checked" : "");
                    table.AppendFormat(@"   <div class='nopadding col-lg-6 col-md-6 col-sm-6 col-xs-6' id='dataagendamentonrDiv{0}'>
                                                Data Agendamento NR
                                                <div style='width:100%;' class='input-group' style='margin-bottom: 5px;'>
                                                    <input type='checkbox' class='form-control' style='margin-bottom: 5px; width: 20%;' id='data_agendamento_nr_checkbox{0}' onclick='changeNR(this, {0});' {2}/>
                                                    <input type='text' class='form-control' id='data_agendamento_nr{0}' name='data_agendamento_nr{0}' placeholder='Data Agendamento NR' style='width: 80%; height: 34px;' {1} {3}/>
                                                </div>
                                            </div>", conta.ToString(), myReader["data_agendamento_nr"].ToString() == "" ? "" : ("value = '" + myReader["data_agendamento_nr"].ToString() + "'"), myReader["data_agendamento_nr"].ToString() == "" ? "" : " checked ", myReader["data_agendamento_nr"].ToString() == "" ? " disabled " : "");
                    table.AppendFormat(@"   <div class='nopadding col-lg-6 col-md-6 col-sm-6 col-xs-6' id='dataagendamentofimDiv{0}'>
                                                Data Agendamento Fim
                                                <div style='width:100%;' class='input-group' style='margin-bottom: 5px;'>
                                                    <input type='checkbox' class='form-control' style='margin-bottom: 5px; width: 20%;' id='data_agendamento_fim_checkbox{0}' onclick='changeFim(this, {0});' {2}/>
                                                    <input type='text' class='form-control' id='data_agendamento_fim{0}' name='data_agendamento_fim{0}' placeholder='Data Agendamento Fim' style='width: 80%; height: 34px;' {1} {3}/>
                                                </div>
                                            </div>", conta.ToString(), myReader["data_agendamento_fim"].ToString() == "" ? "" : ("value = '" + myReader["data_agendamento_fim"].ToString() + "'"), myReader["data_agendamento_fim"].ToString() == "" ? "" : " checked ", myReader["data_agendamento_fim"].ToString() == "" ? " disabled " : "");
                    table.AppendFormat(@"   <div class='nopadding col-lg-6 col-md-6 col-sm-6 col-xs-6' id='dataagendamentoreativacaoDiv{0}'>
                                                Data Agendamento Reativação
                                                <div style='width:100%;' class='input-group' style='margin-bottom: 5px;'>
                                                    <input type='checkbox' class='form-control' style='margin-bottom: 5px; width: 20%;' id='data_agendamento_reativacao_checkbox{0}' onclick='changeReativacao(this, {0});' {2}/>
                                                    <input type='text' class='form-control' id='data_agendamento_reativacao{0}' name='data_agendamento_reativacao{0}' placeholder='Data Agendamento Reativação' style='width: 80%; height: 34px;' {1} {3}/>
                                                </div>
                                            </div>", conta.ToString(), myReader["data_agendamento_reativacao"].ToString() == "" ? "" : ("value = '" + myReader["data_agendamento_reativacao"].ToString() + "'"), myReader["data_agendamento_reativacao"].ToString() == "" ? "" : " checked ", myReader["data_agendamento_reativacao"].ToString() == "" ? " disabled " : "");
                    table.AppendFormat(@"   <div class='nopadding col-lg-6 col-md-6 col-sm-6 col-xs-6' id='dataagendamentocancelamentoDiv{0}'>
                                                Data Agendamento Cancelamento
                                                <div style='width:100%;' class='input-group' style='margin-bottom: 5px;'>
                                                    <input type='checkbox' class='form-control' style='margin-bottom: 5px; width: 20%;' id='data_agendamento_cancelamento_checkbox{0}' onclick='changeCancelamento(this, {0});' {2}/>
                                                    <input type='text' class='form-control' id='data_agendamento_cancelamento{0}' name='data_agendamento_cancelamento{0}' placeholder='Data Agendamento Cancelamento' style='width: 80%; height: 34px;' {1} {3}/>
                                                </div>
                                            </div>", conta.ToString(), myReader["data_agendamento_cancelamento"].ToString() == "" ? "" : ("value = '" + myReader["data_agendamento_cancelamento"].ToString() + "'"), myReader["data_agendamento_cancelamento"].ToString() == "" ? "" : " checked ", myReader["data_agendamento_cancelamento"].ToString() == "" ? " disabled " : "");
                    table.AppendFormat(@"   <div class='nopadding col-lg-12 col-md-12 col-sm-12 col-xs-12' id='notasDiv{0}'>
                                                Notas
                                                <textarea class='form-control' id='notas{0}' style='width: 100%; margin: auto; height: auto; margin-bottom: 5px;' rows='5'>{1}</textarea>
                                            </div>", conta.ToString(), myReader["notas"].ToString());

                    table.AppendFormat(@"<input type='button' class='form-control' value='Guardar Alterações' style='float: right; width: 48%; height: auto; margin-bottom: 10px' onclick='guardarAlteracoesContrato({0}, {1});' />", myReader["ID_CONTRATO"].ToString(), conta.ToString());
                    table.AppendFormat(@"<input type='button' class='form-control' value='Remover Contrato' style='float: left; width: 48%; height: auto; margin-bottom: 10px' onclick='removerContrato({0});' />", myReader["ID_CONTRATO"].ToString());

                    table.AppendFormat(@"</div>");

                    conta++;
                }

                table.AppendFormat("<span class='variaveis' id='countContratos'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"<input type='button' class='form-control' value='Criar Novo Contrato' style='width: 100%; height: auto; margin-bottom: 10px' onclick='openPopup(0, {0});' />", nr_socio);
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:small;margin: auto;color:#000'>Não existem contratos referentes ao número de sócio indicado. Por favor, indique um nº de sócio ou crie um novo contrato para o nº de sócio indicado</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:small;margin: auto;color:#000'>{0}</div>", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string loadStatus(string id_status_default, string nome, string id_pagamento, string nr_contrato, string nr_pagamento)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   select
	                                            PAGAMENTOS_STATUSID,
	                                            LTRIM(RTRIM(CODIGO)) as CODIGO,
	                                            LTRIM(RTRIM(DESIGNACAO)) as DESIGNACAO
                                            FROM PAGAMENTOS_STATUS
                                            ORDER BY CODIGO");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                table.AppendFormat(@"   <select class='form-control' id='{0}' style='width:100%; height: 40px; font-size: small;' onchange='updateStatus({1}, {2}, {3})'>", nome, id_pagamento, nr_contrato, nr_pagamento);

                table.AppendFormat(@"   <option value='0'>Selecione um estado</option>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <option value='{0}' {2}>{1}</option>", myReader["PAGAMENTOS_STATUSID"].ToString(),
                                                                                myReader["DESIGNACAO"].ToString(),
                                                                                myReader["PAGAMENTOS_STATUSID"].ToString() == id_status_default ? "selected" : "");
                }

                table.AppendFormat(@"</select");

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"   <select class='form-control' id='{0}' style='width:100%; height: 25px; font-size: small;'>", nome);
                table.AppendFormat(@"   <option value='0'>Não existem estados a apresentar</option>");
                table.AppendFormat(@"   </select>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"   <select class='form-control' id='{0}' style='width:100%; height: 25px; font-size: small;'>", nome);
            table.AppendFormat(@"   <option value='0'>Não existem estados a apresentar</option>");
            table.AppendFormat(@"   </select>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string updateStatus(string id_operador, string id_pagamento, string id_status)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @code_op char(30) = (SELECT LTRIM(RTRIM(CODIGO)) FROM OPERADORES WHERE OPERADORESID = @id_operador);
                                            DECLARE @id_pagamento int = {1};
                                            DECLARE @id_status int = {2};
                                            DECLARE @notas varchar(max);
                                            DECLARE @ret int;

                                            select 
                                                @notas = ('Contrato do sócio ' + LTRIM(RTRIM(STR(soc.NR_SOCIO))) + ' de ' + CONVERT(VARCHAR(10), cont.DATA_INICIO, 103)
                                                + ' a ' + CONVERT(VARCHAR(10), cont.DATA_FIM, 103) + ': alterado o status do pagamento de ' + ltrim(rtrim(str(pag.mes)))
                                                + '/' + ltrim(rtrim(str(pag.ano))) + ' para ' + pagst.designacao + ' pelo operador ' + op.codigo)
                                            from operadores op
                                            inner join pagamentos_status pagst on pagst.pagamentos_statusid = @id_status
                                            inner join pagamentos pag on pag.pagamentosid = @id_pagamento
                                            inner join contrato cont on cont.contratoid = pag.contrato_id
                                            inner join socios soc on soc.sociosid = cont.socio_id
                                            where operadoresid = @id_operador

                                            UPDATE PAGAMENTOS
                                            SET ID_STATUS = @id_status, CTRLCODOP = @code_op, CTRLDATA = GETDATE()
                                            WHERE PAGAMENTOSID = @id_pagamento

                                            EXEC REGISTA_LOG @id_operador, 'PAGAMENTOS', @notas, @ret output;", id_operador, id_pagamento, id_status == "0" ? "NULL" : id_status);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"Pagamento atualizado com sucesso!");
            connection.Close();
            return table.ToString();            
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Ocorreu um erro ao atualizar o pagamento!");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string insertNovoPagamento(string id_operador, string id_contrato)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @id_contrato int = {1};
                                            DECLARE @ret int;
                                            DECLARE @notas varchar(max);
                                            DECLARE @res int;

                                            EXEC INSERT_NOVO_PAGAMENTO @id_operador, @id_contrato, @ret output

                                            select 
                                                @notas = ('Contrato do sócio ' + LTRIM(RTRIM(STR(soc.NR_SOCIO))) + ' de ' + CONVERT(VARCHAR(10), cont.DATA_INICIO, 103)
                                                + ' a ' + CONVERT(VARCHAR(10), cont.DATA_FIM, 103) + ': inserido novo pagamento de ' + ltrim(rtrim(str(pag.mes)))
                                                + '/' + ltrim(rtrim(str(pag.ano))) + ' pelo operador ' + op.codigo)
                                            from operadores op
                                            inner join contrato cont on cont.contratoid = @id_contrato
                                            inner join socios soc on soc.sociosid = cont.socio_id
                                            inner join pagamentos pag on pag.contrato_id = cont.contratoid and pag.pagamentosid = @ret
                                            where operadoresid = @id_operador

                                            IF(@ret >= 0)
                                            begin
                                                EXEC REGISTA_LOG @id_operador, 'PAGAMENTOS', @notas, @res output;
                                            end

                                            SELECT @ret as ret", id_operador, id_contrato);

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
                // Adiciona as linhas com dados
                table.AppendFormat(@"-999");
                connection.Close();
                return table.ToString();
            }            
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"-999");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string removerContrato(string id_operador, string id_contrato)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @id_contrato int = {1};
                                            DECLARE @ret int;
                                            DECLARE @notas varchar(max);
                                            DECLARE @res int;

                                            select 
                                                @notas = ('Contrato do sócio ' + LTRIM(RTRIM(STR(soc.NR_SOCIO))) + ' de ' + CONVERT(VARCHAR(10), cont.DATA_INICIO, 103)
                                                + ' a ' + CONVERT(VARCHAR(10), cont.DATA_FIM, 103) + ' removido pelo operador ' + op.codigo)
                                            from operadores op
                                            inner join contrato cont on cont.contratoid = @id_contrato
                                            inner join socios soc on soc.sociosid = cont.socio_id
                                            where operadoresid = @id_operador

                                            DELETE FROM PAGAMENTOS WHERE CONTRATO_ID = @id_contrato
                                            DELETE FROM CONTRATO WHERE CONTRATOID = @id_contrato

                                            EXEC REGISTA_LOG @id_operador, 'CONTRATOS', @notas, @res output;

                                            SELECT 0 as ret", id_operador, id_contrato);

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
                // Adiciona as linhas com dados
                table.AppendFormat(@"-999");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"-999");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string criaNovoContrato(string id_operador, string id_tipo, string id_estado, string dataInicio, string dataFim, string valor, string debitoDireto, string notas, string id_contrato, string nr_socio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @id_tipo int = {1};
                                            DECLARE @id_estado int = {2};
                                            DECLARE @dataInicio datetime = '{3}' + ' 12:00:00';
                                            DECLARE @dataFim datetime = '{4}' + ' 12:00:00';
                                            DECLARE @valor DECIMAL(15,2) = {5};
                                            DECLARE @debitoDireto bit = {6};
                                            DECLARE @notas varchar(max) = '{7}';
                                            DECLARE @id_contrato int = {8};
                                            DECLARE @id_socio int = (select socio_id from contrato where contratoid = @id_contrato);
                                            DECLARE @ret int;
                                            DECLARE @res varchar(max);
                                            
                                            IF(@id_socio is null)
                                            BEGIN
                                                set @id_socio = (select sociosid from socios where nr_socio = {9});
                                            END

                                            EXEC CRIA_NOVO_CONTRATO @id_operador, @id_socio, @id_tipo, @id_estado, @dataInicio, @dataFim, @valor, @debitoDireto,
                                                @notas, @ret output, @res output;

                                            SELECT @ret as ret, @res as res", id_operador, id_tipo, id_estado, dataInicio, dataFim, valor, debitoDireto, notas, id_contrato, nr_socio);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["ret"].ToString()) > 0)
                    {
                        table.AppendFormat(@"{0}", myReader["res"].ToString());
                    }
                    else
                    {
                        // Adiciona as linhas com dados
                        table.AppendFormat(@"Erro: {0}", myReader["res"].ToString());
                    }
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                // Adiciona as linhas com dados
                table.AppendFormat(@"Ocorreu um erro ao inserir o novo contrato!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Ocorreu um erro ao inserir o novo contrato!");
            connection.Close();
            return table.ToString();
        }
    }


    [WebMethod]
    public static string inserPagamentoContratoAnterior(string id_operador, string id_contrato, string id_contrato_anterior)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @id_contrato int = {1};
                                            DECLARE @id_contrato_anterior int = {2};
                                            DECLARE @ret int;
                                            DECLARE @notas varchar(max);
                                            DECLARE @res int

                                            EXEC INSERT_PAGAMENTO_CONTRATO_ANTERIOR @id_operador, @id_contrato, @id_contrato_anterior, @ret output

                                            select 
                                                @notas = ('Contrato do sócio ' + LTRIM(RTRIM(STR(soc.NR_SOCIO))) + ' de ' + CONVERT(VARCHAR(10), cont.DATA_INICIO, 103)
                                                + ' a ' + CONVERT(VARCHAR(10), cont.DATA_FIM, 103) + ' removido pois foi criado um novo pagamento no contrato de '
						                        + CONVERT(VARCHAR(10), ca.DATA_INICIO, 103) + ' a ' + CONVERT(VARCHAR(10), ca.DATA_FIM, 103)
						                        + ' pelo operador ' + op.codigo)
                                            from operadores op
                                            inner join contrato cont on cont.contratoid = @id_contrato
                                            inner join contrato ca on ca.contratoid = @id_contrato_anterior
                                            inner join socios soc on soc.sociosid = cont.socio_id
                                            where operadoresid = @id_operador

                                            IF(@ret >= 0)
                                            begin
                                                EXEC REGISTA_LOG @id_operador, 'PAGAMENTOS', @notas, @res output;
                                            end

                                            SELECT @ret as ret", id_operador, id_contrato, id_contrato_anterior);

            //return sql;

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
                // Adiciona as linhas com dados
                table.AppendFormat(@"-999");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"-999");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string updateValorPagamento(string id_operador, string id_pagamento, string valor)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @code_op char(30) = (SELECT LTRIM(RTRIM(CODIGO)) FROM OPERADORES WHERE OPERADORESID = @id_operador);
                                            DECLARE @id_pagamento int = {1};
                                            DECLARE @val decimal(15,2) = {2};
                                            DECLARE @notas varchar(max);
                                            DECLARE @ret int;

                                            select 
                                                @notas = ('Contrato do sócio ' + LTRIM(RTRIM(STR(soc.NR_SOCIO))) + ' de ' + CONVERT(VARCHAR(10), cont.DATA_INICIO, 103)
                                                + ' a ' + CONVERT(VARCHAR(10), cont.DATA_FIM, 103) + ': alterado o valor do pagamento de ' + ltrim(rtrim(str(pag.mes)))
                                                + '/' + ltrim(rtrim(str(pag.ano))) + ' para ' + ltrim(rtrim(convert(varchar, @val, 10))) + ' pelo operador ' + op.codigo)
                                            from operadores op
                                            inner join pagamentos pag on pag.pagamentosid = @id_pagamento
                                            inner join contrato cont on cont.contratoid = pag.contrato_id
                                            inner join socios soc on soc.sociosid = cont.socio_id
                                            where operadoresid = @id_operador

                                            UPDATE PAGAMENTOS
                                            SET VALOR = @val, CTRLCODOP = @code_op, CTRLDATA = GETDATE()
                                            WHERE PAGAMENTOSID = @id_pagamento

                                            EXEC REGISTA_LOG @id_operador, 'PAGAMENTOS', @notas, @ret output;", id_operador, id_pagamento, valor == "" ? "0" : valor);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"Valor do pagamento atualizado com sucesso!");
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Ocorreu um erro ao atualizar o valor do pagamento!");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string loadStatusContrato(string id_status)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   select
	                                            ESTADOS_CONTRATOID,
                                                LTRIM(RTRIM(DESIGNACAO)) as DESIGNACAO
                                            FROM ESTADOS_CONTRATO
                                            ORDER BY CODIGO");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"   <select class='form-control' id='estadosContratoNew' style='width:100%; height: 40px; font-size: small;'>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <option value='{0}' {2}>{1}</option>", myReader["ESTADOS_CONTRATOID"].ToString(),
                                                                                myReader["DESIGNACAO"].ToString(),
                                                                                myReader["ESTADOS_CONTRATOID"].ToString() == id_status ? "selected" : "");
                }

                table.AppendFormat(@"</select");

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
    public static string loadTipoContrato(string id_tipo)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   select
	                                            TIPO_CONTRATOID,
                                                LTRIM(RTRIM(DESIGNACAO)) as DESIGNACAO
                                            FROM TIPO_CONTRATO
                                            ORDER BY CODIGO");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"   <select class='form-control' id='tiposContratoNew' style='width:100%; height: 40px; font-size: small;' onchange='loadFinalDate();'>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <option value='{0}' {2}>{1}</option>", myReader["TIPO_CONTRATOID"].ToString(),
                                                                                myReader["DESIGNACAO"].ToString(),
                                                                                myReader["TIPO_CONTRATOID"].ToString() == id_tipo ? "selected" : "");
                }

                table.AppendFormat(@"</select");

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
    public static string criaNovoContratoDados(string id_contrato)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   declare @id_contrato int = {0}
                                            declare @data_inicio datetime;

                                            IF(@id_contrato >= 0)
                                            BEGIN
                                                set @data_inicio = DATEADD(hh, -1, GETDATE());

                                                set @data_inicio = CAST('01-' 
						                           + (CASE WHEN MONTH(@data_inicio) < 10 THEN '0' + LTRIM(RTRIM(STR(MONTH(@data_inicio)))) 
						                           ELSE LTRIM(RTRIM(STR(MONTH(@data_inicio)))) END) + '-' + 
						                           LTRIM(RTRIM(STR(YEAR(@data_inicio)))) + ' 12:00:00' as datetime);

                                                set @data_inicio = dateadd(mm, 1, @data_inicio);

                                                select top 1
                                                    tipo_contratoid as id_tipo, 
                                                    (select estados_contratoid from estados_contrato where codigo = 'INACTIVO') as id_estado, 
                                                    'Contrato criado manualmente através da plataforma' as notas, 
                                                    CONVERT(VARCHAR(10), @data_inicio, 103) as data_inicio, 
                                                    CONVERT(VARCHAR(10), DATEADD(dd, -1, (DATEADD(mm, tp.DURACAO_MINIMA_MESES, @data_inicio))), 103) as data_fim, 
                                                    0 as dd, 
                                                    CAST(tp.PRECO as decimal(15,2)) as valor 
                                                from tipo_contrato tp
                                            END
                                            ELSE
                                            BEGIN
                                                set @data_inicio = CAST((select top 1
			                                                CONVERT(VARCHAR(10), 
			                                                ('01-' + (CASE WHEN mes < 10 THEN '0' + LTRIM(RTRIM(STR(mes))) ELSE LTRIM(RTRIM(STR(MES))) END)
			                                                + '-' + LTRIM(RTRIM(STR(ANO)))))
			                                                + ' 12:00:00'
			                                            from pagamentos
			                                            where contrato_id = @id_contrato
			                                            order by ano desc, mes desc) as datetime);
    
                                                set @data_inicio = DATEADD(mm, 1, @data_inicio);

                                                select 
                                                    tipo_contrato_id as id_tipo, 
                                                    (select estados_contratoid from estados_contrato where codigo = 'INACTIVO') as id_estado, 
                                                    'Contrato criado manualmente através da plataforma' as notas, 
                                                    CONVERT(VARCHAR(10), @data_inicio, 103) as data_inicio, 
                                                    CONVERT(VARCHAR(10), DATEADD(dd, -1, (DATEADD(mm, tp.DURACAO_MINIMA_MESES, @data_inicio))), 103) as data_fim, 
                                                    CAST(cont.DEBITO_DIRETO as int) as dd, 
                                                    CAST(cont.VALOR as decimal(15,2)) as valor 
                                                from contrato cont 
                                                inner join tipo_contrato tp on tp.tipo_contratoid = cont.tipo_contrato_id
                                                where contratoid = @id_contrato
                                            END", id_contrato);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <span class='variaveis' id='idTipoNovoContrato'>{0}</span>
                                            <span class='variaveis' id='idEstadoNovoContrato'>{1}</span>
                                            <span class='variaveis' id='notasNovoContrato'>{2}</span>
                                            <span class='variaveis' id='dataInicioNovoContrato'>{3}</span>
                                            <span class='variaveis' id='dataFimNovoContrato'>{4}</span>
                                            <span class='variaveis' id='debitoDiretoNovoContrato'>{5}</span>
                                            <span class='variaveis' id='valorNovoContrato'>{6}</span>", myReader["id_tipo"].ToString()
                                                                                                     , myReader["id_estado"].ToString()
                                                                                                     , myReader["notas"].ToString()
                                                                                                     , myReader["data_inicio"].ToString()
                                                                                                     , myReader["data_fim"].ToString()
                                                                                                     , myReader["dd"].ToString()
                                                                                                     , myReader["valor"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                // Adiciona as linhas com dados
                table.AppendFormat(@"Não existem linhas a apresentar", sql);
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
    public static string loadStatusContratoTabela(string id_status, string x)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   select
	                                            ESTADOS_CONTRATOID,
                                                LTRIM(RTRIM(DESIGNACAO)) as DESIGNACAO
                                            FROM ESTADOS_CONTRATO
                                            ORDER BY CODIGO");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"Estado do Contrato<select class='form-control' id='selectEstadoContrato{0}' style='width:100%; height: 40px; font-size: small;'>", x);

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <option value='{0}' {2}>{1}</option>", myReader["ESTADOS_CONTRATOID"].ToString(),
                                                                                myReader["DESIGNACAO"].ToString(),
                                                                                myReader["ESTADOS_CONTRATOID"].ToString() == id_status ? "selected" : "");
                }

                table.AppendFormat(@"</select");

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
    public static string loadTipoContratoTabela(string id_tipo, string x)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   select
	                                            TIPO_CONTRATOID,
                                                LTRIM(RTRIM(DESIGNACAO)) as DESIGNACAO
                                            FROM TIPO_CONTRATO
                                            ORDER BY CODIGO");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"Tipo do Contrato<select class='form-control' id='selectTipoContrato{0}' style='width:100%; height: 40px; font-size: small;'>", x);

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <option value='{0}' {2}>{1}</option>", myReader["TIPO_CONTRATOID"].ToString(),
                                                                                myReader["DESIGNACAO"].ToString(),
                                                                                myReader["TIPO_CONTRATOID"].ToString() == id_tipo ? "selected" : "");
                }

                table.AppendFormat(@"</select");

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
    public static string updateContrato(string id_operador, string id_contrato, string id_tipo, string id_estado, string data_inicio, string data_fim, string valor, string dd, string notas, string data_agendamento_nr, string data_agendamento_fim, string data_agendamento_reativacao, string data_agendamento_cancelamento)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY
                                            DECLARE @id_operador int = {0};
                                            DECLARE @code_op char(30) = (SELECT LTRIM(RTRIM(CODIGO)) FROM OPERADORES WHERE OPERADORESID = @id_operador);
                                            DECLARE @id_contrato int = {1};
                                            DECLARE @id_tipo int = {2};
                                            DECLARE @id_estado int = {3};
                                            DECLARE @data_inicio datetime = '{4}';
                                            DECLARE @data_fim datetime = '{5}';
                                            DECLARE @valor decimal(15,2) = {6};
                                            DECLARE @dd bit = {7};
                                            DECLARE @notas varchar(max) = '{8}';
                                            DECLARE @data_agendamento_nr datetime = {10};
                                            DECLARE @data_agendamento_reativacao datetime = {11};
                                            DECLARE @data_agendamento_fim datetime = {9};
                                            DECLARE @data_agendamento_cancelamento datetime = {12};
                                            DECLARE @ret int;

                                            UPDATE CONTRATO
                                            SET TIPO_CONTRATO_ID = @id_tipo,
                                            ESTADO_CONTRATO_ID = @id_estado,
                                            DATA_INICIO = @data_inicio,
                                            DATA_FIM = @data_fim,
                                            VALOR = @valor,
                                            DEBITO_DIRETO = @dd,
                                            CTRLCODOP = @code_op, 
                                            CTRLDATA = GETDATE(),
                                            NOTAS = @notas,
                                            DATA_AGENDAMENTO_NR = @data_agendamento_nr,
                                            DATA_AGENDAMENTO_FIM = @data_agendamento_fim,
                                            DATA_AGENDAMENTO_REATIVACAO = @data_agendamento_reativacao,
                                            DATA_AGENDAMENTO_CANCELAMENTO = @data_agendamento_cancelamento
                                            WHERE CONTRATOID = @id_contrato

                                            select 
                                                @notas = ('Contrato do sócio ' + LTRIM(RTRIM(STR(soc.NR_SOCIO))) + ' de ' + CONVERT(VARCHAR(10), cont.DATA_INICIO, 103)
                                                + ' a ' + CONVERT(VARCHAR(10), cont.DATA_FIM, 103) + ' alterado pelo operador ' + @code_op)
                                            from socios soc
                                            inner join contrato cont on cont.socio_id = soc.sociosid
                                            where cont.contratoid = @id_contrato

                                            EXEC REGISTA_LOG @id_operador, 'CONTRATOS', @notas, @ret output;", id_operador, 
                                                                                                             id_contrato, 
                                                                                                             id_tipo, 
                                                                                                             id_estado, 
                                                                                                             data_inicio, 
                                                                                                             data_fim, 
                                                                                                             valor, 
                                                                                                             dd, 
                                                                                                             notas,
                                                                                                             data_agendamento_fim == "NULL" ? data_agendamento_fim : "'" + data_agendamento_fim + "'",
                                                                                                             data_agendamento_nr == "NULL" ? data_agendamento_nr : "'" + data_agendamento_nr + "'",
                                                                                                             data_agendamento_reativacao == "NULL" ? data_agendamento_reativacao : "'" + data_agendamento_reativacao + "'",
                                                                                                             data_agendamento_cancelamento == "NULL" ? data_agendamento_cancelamento : "'" + data_agendamento_cancelamento + "'");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"Contrato atualizado com sucesso!");
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Ocorreu um erro ao atualizar o contrato: {0}", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }
}
