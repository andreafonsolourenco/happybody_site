using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Pagamentos : Page
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
                                            DECLARE @pagina varchar(400) = 'Pagamentos';
                                            DECLARE @subpagina varchar(400);

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
        string id_contrato_mais_recente = "";

        SqlConnection connection = new SqlConnection(cs);
        SqlConnection connectionLines = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @nr_socio int = {0};

                                            select distinct
	                                            DATA_INICIO,
	                                            DATA_FIM,
	                                            VALOR,
	                                            NOME,
	                                            NR_SOCIO,
                                                ESTADO,
	                                            TIPO,
                                                ID_CONTRATO,
                                                YEAR(DATA_FIM_DATA), 
                                                MONTH(DATA_FIM_DATA), 
                                                DAY(DATA_FIM_DATA)
                                            FROM REPORT_PAGAMENTOS(@nr_socio)
                                            ORDER BY YEAR(DATA_FIM_DATA) desc, MONTH(DATA_FIM_DATA) desc, DAY(DATA_FIM_DATA) desc", nr_socio);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;
            int contaContratos = 0;
            string id_contrato = "";
            string margintop = "";

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    //if (contaContratos != 0)
                    //{
                    //    if (conta < 4)
                    //        margintop = "margin-top: 150px;";
                    //    else if (conta < 8)
                    //        margintop = "margin-top: 300px;";
                    //    else if (conta < 12)
                    //        margintop = "margin-top: 450px;";
                    //    else if (conta < 16)
                    //        margintop = "margin-top: 600px;";
                    //}

                    if (contaContratos == 0 && conta == 0)
                    {
                        table.AppendFormat(@"<input type='button' class='form-control' value='Criar Novo Contrato' style='width: 100%; height: auto; margin-bottom: 10px' onclick='openPopup({0}, {1});' />", myReader["ID_CONTRATO"].ToString(), nr_socio);
                    }

                    conta = 0;

                    table.AppendFormat(@"<div id='divGeralContrato{0}' {1}>", contaContratos.ToString(), contaContratos > 0 ? "style='display:none;'" : "");

                    table.AppendFormat(@"   <div class='headerTitle' id='headerContrato{8}' style='{7}' {7}>
                                                <div class='col-lg-2 col-md-2 col-sm-2 col-xs-2' style='height: 100%; text-align: center; cursor: pointer;' onclick='fadeContracts({8}, -1);'>
                                                    <img src='img/icons/icon_arrow_left.png' onclick='' style='height: 100%' id='icon_left_{8}'/>
                                                </div>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8' style='height: 100%; text-align: center;'>
                                                    {0} - {1}<br />{2} - {3} ({4} €)<br />Contrato: {6} - {5}
                                                </div>
                                                <div class='col-lg-2 col-md-2 col-sm-2 col-xs-2' style='height: 100%; text-align: center; cursor: pointer;' onclick='fadeContracts({8}, 1);'>
                                                    <img src='img/icons/icon_arrow_right.png' onclick='' style='height: 100%' id='icon_right_{8}'/>
                                                </div>
                                            </div>",
                            myReader["NR_SOCIO"].ToString(),
                            myReader["NOME"].ToString(),
                            myReader["DATA_INICIO"].ToString(),
                            myReader["DATA_FIM"].ToString(),
                            myReader["VALOR"].ToString(),
                            myReader["ESTADO"].ToString(),
                            myReader["TIPO"].ToString(),
                            margintop,
                            contaContratos.ToString());

                    id_contrato = myReader["ID_CONTRATO"].ToString();

                    string sqlLinhas = string.Format(@" DECLARE @nr_socio int = {0};
                                                        DECLARE @id_contrato int = {1};

                                                        select
	                                                        MES,
	                                                        ANO,
	                                                        ID_STATUS,
	                                                        CODIGO_STATUS,
	                                                        STATUS,
	                                                        DATA_INICIO,
	                                                        DATA_FIM,
	                                                        VALOR,
	                                                        NOME,
	                                                        NR_SOCIO,
                                                            ID_PAGAMENTO,
                                                            ID_ESTADO,
	                                                        CODIGO_ESTADO,
	                                                        ESTADO,
	                                                        ID_TIPO,
	                                                        CODIGO_TIPO,
	                                                        TIPO,
                                                            ID_CONTRATO,
                                                            VAL_PAGAMENTO
                                                        FROM REPORT_PAGAMENTOS(@nr_socio)
                                                        WHERE ID_CONTRATO = @id_contrato
                                                        ORDER BY ANO asc, MES asc", nr_socio, id_contrato);

                    connectionLines.Open();

                    SqlCommand myCommandLines = new SqlCommand(sqlLinhas, connectionLines);
                    SqlDataReader myReaderLines = myCommandLines.ExecuteReader();
                    SqlDataAdapter adapterLines = new SqlDataAdapter(myCommandLines);

                    if (myReaderLines.HasRows)
                    {
                        table.AppendFormat(@"<div id='pagamentosContrato{0}'>", contaContratos.ToString());

                        while (myReaderLines.Read())
                        {
                            string btn = "";

                            if (contaContratos > 1)
                            {
                                btn = "disabled";
                            }

                            // Adiciona as linhas com dados
                            table.AppendFormat(@"<div class='col-lg-3 col-md-3 col-sm-3 col-xs-3' style='margin-bottom: 10px;'>
                                            <div style='width:100%; height: 25px; line-height: 25px; background-color: #000; color: #FFF; font-size: small; text-align: center; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;'>
                                                {0}/{1}
                                            </div>
                                            <div style='width:100%; height: auto;' id='divStatus_{2}'>
                                                <select class='form-control' style='width:100%; height: 40px; font-size: small;'>
                                                    <option value='{4}'>{6}</option>
                                                </select>
                                            </div>
                                            <div style='width:100%; height: auto;' id='divAlteraValor_{2}' class='input-group'>
                                                <input type='number' value='{9}' class='form-control' id='pagamento_{7}_{8}' onchange='alteraValorPagamento({3}, {7}, {8});' placeholder='Valor' style='margin: auto; height: 28px;' aria-describedby='basic-addon-preco' {5}/>
                                                <span class='input-group-addon addon-euro' id='basic-addon-preco'>€</span>
                                            </div>
                                            <div style='width:100%; height: auto;' id='divRemove_{2}'>
                                                <input id='btnRemove_{7}_{8}' value='Remover' runat='server' type='button' onclick='removerPagamento({3});' style='background-color: #4282b5; width: 100%; height: 25px; font-size: small; text-align: center; line-height: 25px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;' {5}/>
                                            </div>
                                            <span class='variaveis' id='id_pagamento_{2}'>{3}</span>
                                            <span class='variaveis' id='id_status_{2}'>{4}</span>
                                        </div>",
                                                        myReaderLines["MES"].ToString(),
                                                        myReaderLines["ANO"].ToString(),
                                                        contaContratos.ToString() + "_" + conta.ToString(),
                                                        myReaderLines["ID_PAGAMENTO"].ToString(),
                                                        myReaderLines["ID_STATUS"].ToString(),
                                                        btn,
                                                        myReaderLines["STATUS"].ToString(),
                                                        conta.ToString(),
                                                        contaContratos.ToString(),
                                                        myReaderLines["VAL_PAGAMENTO"].ToString());

                            conta++;
                        }
                    }

                    if (contaContratos == 0)
                    {
                        // Adiciona as linhas com dados
                        table.AppendFormat(@"<div class='col-lg-3 col-md-3 col-sm-3 col-xs-3' style='margin-bottom: 10px; text-align: center; cursor: pointer' onclick='criaNovoPagamento({0});' id='divImgPlus_{1}'>
                                            <img src='img/icons/plus.png' id='img_plus_{1}'/>
                                        </div>", id_contrato, contaContratos);

                        id_contrato_mais_recente = id_contrato;
                    }

                    if (contaContratos == 1)
                    {
                        // Adiciona as linhas com dados
                        table.AppendFormat(@"<div class='col-lg-3 col-md-3 col-sm-3 col-xs-3' style='margin-bottom: 10px; text-align: center; cursor: pointer' onclick='criaPagamentoContratoAnterior({0}, {2});' id='divImgPlus_{1}'>
                                            <img src='img/icons/plus.png' id='img_plus_{1}'/>
                                        </div>", id_contrato, contaContratos, id_contrato_mais_recente);
                    }

                    table.AppendFormat("<span class='variaveis' id='countElements_{1}'>{0}</span>", conta.ToString()
                        , contaContratos.ToString());

                    contaContratos++;

                    connectionLines.Close();

                    table.AppendFormat(@"</div></div>");
                }

                table.AppendFormat("<span class='variaveis' id='countContratos'>{0}</span>", contaContratos.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"<input type='button' class='form-control' value='Criar Novo Contrato' style='width: 100%; height: auto; margin-bottom: 10px' onclick='openPopup(0, {0});' />", nr_socio);
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:small;margin: auto;color:#000'>Não existem pagamentos referentes ao número de sócio indicado. Por favor, indique um nº de sócio ou então crie um contrato para o sócio indicado.</div>");
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

                                            IF(@id_status is null or @id_status = 0)
                                            BEGIN
                                                select 
                                                    @notas = ('Contrato do sócio ' + LTRIM(RTRIM(STR(soc.NR_SOCIO))) + ' de ' + CONVERT(VARCHAR(10), cont.DATA_INICIO, 103)
                                                    + ' a ' + CONVERT(VARCHAR(10), cont.DATA_FIM, 103) + ': alterado o status do pagamento de ' + ltrim(rtrim(str(pag.mes)))
                                                    + '/' + ltrim(rtrim(str(pag.ano))) + ' para Sem Estado de Pagamento pelo operador ' + op.codigo)
                                                from operadores op
                                                inner join pagamentos pag on pag.pagamentosid = @id_pagamento
                                                inner join contrato cont on cont.contratoid = pag.contrato_id
                                                inner join socios soc on soc.sociosid = cont.socio_id
                                                where operadoresid = @id_operador
                                            END
                                            ELSE
                                            BEGIN
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
                                            END

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
    public static string removerPagamento(string id_operador, string id_pagamento)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @id_pagamento int = {1};
                                            DECLARE @ret int;
                                            DECLARE @notas varchar(max);
                                            DECLARE @res int;

                                            select 
                                                @notas = ('Contrato do sócio ' + LTRIM(RTRIM(STR(soc.NR_SOCIO))) + ' de ' + CONVERT(VARCHAR(10), cont.DATA_INICIO, 103)
                                                + ' a ' + CONVERT(VARCHAR(10), cont.DATA_FIM, 103) + ': removido pagamento de ' + ltrim(rtrim(str(pag.mes)))
                                                + '/' + ltrim(rtrim(str(pag.ano))) + ' pelo operador ' + op.codigo)
                                            from operadores op
                                            inner join pagamentos pag on pag.pagamentosid = @id_pagamento
                                            inner join contrato cont on cont.contratoid = pag.contrato_id
                                            inner join socios soc on soc.sociosid = cont.socio_id
                                            where operadoresid = @id_operador

                                            EXEC REMOVER_PAGAMENTO @id_operador, @id_pagamento, @ret output

                                            IF(@ret >= 0)
                                            begin
                                                EXEC REGISTA_LOG @id_operador, 'PAGAMENTOS', @notas, @res output;
                                            end

                                            SELECT @ret as ret", id_operador, id_pagamento);

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
    public static string loadAvisos(string nr_socio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        string ret = "";

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @nr_socio int = {0};
                                            DECLARE @data_atual datetime = DATEADD(hh, -1, GETDATE());

                                            SELECT av.NOTAS, av.VALOR
                                            FROM AVISOS av
                                            INNER JOIN SOCIOS soc on soc.SOCIOSID = av.ID_SOCIO
                                            WHERE soc.NR_SOCIO = @nr_socio
                                            AND (CAST(av.DATA_AVISO as DATE) = CAST(@data_atual as DATE)
                                            OR av.DATA_AVISO is null)", nr_socio);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    ret += myReader["NOTAS"].ToString() + ": " + myReader["VALOR"].ToString() + "€<br />";
                }
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
            return "Ocorreu um erro ao verificar os avisos para o sócio indicado: " + exc.ToString();
        }

        connection.Close();
        return ret;
    }
}
