using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class ContractStatus : Page
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

        loadStatusPagamento();
    }

    [WebMethod]
    public static string load(string filtro)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_estado int;
                                            DECLARE @FILTRO varchar(MAX) = {0};

                                            SELECT 
                                                ESTADOS_CONTRATOID,
	                                            CODIGO,
	                                            DESIGNACAO,
	                                            NOTAS,
	                                            DATA_ULTIMA_ALTERACAO,
	                                            OP_ULTIMA_ALTERACAO,
	                                            ATIVO,
                                                INATIVO,
                                                NAO_PAGANTE
                                            FROM REPORT_ESTADOS_CONTRATO(@id_estado)
                                            WHERE (@FILTRO IS NULL OR (CODIGO LIKE '%' + @FILTRO + '%' OR DESIGNACAO LIKE '%' + @FILTRO + '%'))
                                            ORDER BY ATIVO desc, CODIGO asc", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

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
                                                    <th style='text-align: center; width: 100%;'>Estados de Contrato</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick='openTypeContractInfo({0}, {2});'>
                                            <td style='display:none;' id='id_{2}'>{0}</td>
                                            <td class='tbodyTrTd'>{1}</td>
                                        </tr>",
                                                myReader["ESTADOS_CONTRATOID"].ToString(),
                                                myReader["DESIGNACAO"].ToString(),
                                                conta.ToString());

                    conta++;
                }

                table.AppendFormat("</tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem estados de contrato a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem estados de contrato a apresentar.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string getStatusContractData(string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_estado int = {0};

                                            SELECT 
                                                ESTADOS_CONTRATOID,
	                                            CODIGO,
	                                            DESIGNACAO,
	                                            NOTAS,
	                                            DATA_ULTIMA_ALTERACAO,
	                                            OP_ULTIMA_ALTERACAO,
	                                            ATIVO,
                                                INATIVO,
                                                NAO_PAGANTE
                                            FROM REPORT_ESTADOS_CONTRATO(@id_estado)", id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:left; margin-top: 5px; margin-bottom: 10px;'>
                                                <input id='btnSave' value='Guardar' runat='server' type='button' onclick='updateInfo();' style='background-color: #4282b5; 
                                                    width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                                <input id='btnApagar' value='Apagar' runat='server' type='button' onclick='deleteContractStatus();' style='background-color: #4282b5; 
                                                    width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                                <img src='img/icons/icon_close.png' style='cursor:pointer;' onclick='closeCustomerInfo();'/>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: 'Noto Sans', sans-serif !important; height:100%'>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Código:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Designação:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Última Modificação:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Realizada por:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Ativo:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Inativo:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Não Pagante:</div>
                                                    <div style='width: 100%; margin-bottom: 10px;' id='tagEstadosPagamentosDiv'>Estados de Pagamentos:</div>
                                                    <div style='height:200px; width:100%; margin-bottom: 10px'>Notas:</div>
                                                </div>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='codigo' name='codigo' placeholder='Código' style='width: 100%; margin: auto; height: 100%;' value='{1}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='designacao' name='designacao' placeholder='Designação' style='width: 100%; margin: auto; height: 100%;' value='{2}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='ultimamodificacao' name='ultimamodificacao' placeholder='Última Modificação' style='width: 100%; margin: auto; height: 100%;' value='{4}' disabled/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='realizadapor' name='realizadapor' placeholder='Realizada por' style='width: 100%; margin: auto; height: 100%;' value='{5}' disabled/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='ativo' name='ativo' style='width: 100%; margin: auto; height: 100%;'{6}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='inativo' name='inativo' style='width: 100%; margin: auto; height: 100%;'{7}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='naopagante' name='naopagante' style='width: 100%; margin: auto; height: 100%;'{8}/>
                                                    </div>
                                                    <div style='width:100%; margin-bottom: 10px' id='divEstadosPagamentos'>
                                                        
                                                    </div>
                                                    <div style='height:200px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='notas' name='notas' style='width: 100%; margin: auto; height: 100%;' rows='5'>{3}</textarea>
                                                    </div>
                                                </div>
                                            </div>",
                                                myReader["ESTADOS_CONTRATOID"].ToString(),
                                                myReader["CODIGO"].ToString(),
                                                myReader["DESIGNACAO"].ToString(),
                                                myReader["NOTAS"].ToString().Trim(),
                                                myReader["DATA_ULTIMA_ALTERACAO"].ToString(),
                                                myReader["OP_ULTIMA_ALTERACAO"].ToString(),
                                                myReader["ATIVO"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["INATIVO"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["NAO_PAGANTE"].ToString().Trim() == "1" ? "checked" : "");
                }
                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                            <img src='img/icon_close.png' style='cursor:pointer;' onclick='closeCustomerInfo();'/>
                                        </div>
                                        <div style='height:auto' class='panel-heading' id='panel-heading'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para este estado de contrato.</span>
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
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para este tipo de contrato.</span>
                                        </div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string editar(string id_operador, string id_estado_contrato, string codigo, string designacao, string ativo, string inativo, string notas, string nao_pagante)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @id_estado_contrato int = {1};
	                                        DECLARE @codigo char(30) = '{2}';
	                                        DECLARE @designacao varchar(100) = '{3}';
	                                        DECLARE @notas varchar(max) = '{4}';
	                                        DECLARE @ativo bit = {5};
                                            DECLARE @inativo bit = {6};
                                            DECLARE @nao_pagante bit = {7};
                                            DECLARE @res int;

                                            EXEC ALTERA_ESTADO_CONTRATO @id_estado_contrato, @id_operador, @codigo, @designacao, @notas, @ativo, @inativo, @nao_pagante, 
                                            @res output",
                                                id_operador, id_estado_contrato, codigo, designacao, notas, ativo, inativo, nao_pagante);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"Estado do Contrato '{0}' atualizado com sucesso!", codigo);
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao atualizar estado do contrato '{0}'!", codigo);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao atualizar estado do contrato '{0}'!", codigo);
        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string criar(string id_operador, string codigo, string designacao, string ativo, string inativo, string notas, string nao_pagante)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @codigo char(30) = '{1}';
	                                        DECLARE @designacao varchar(100) = '{2}';
	                                        DECLARE @notas varchar(max) = '{3}';
	                                        DECLARE @ativo bit = {4};
                                            DECLARE @inativo bit = {5};
                                            DECLARE @nao_pagante bit = {6};
                                            DECLARE @res int;

                                            EXEC CRIA_ESTADO_CONTRATO @id_operador, @codigo, @designacao, @notas, @ativo, @inativo, @nao_pagante, @res output

                                            select @res as res",
                                                id_operador, codigo, designacao, notas, ativo, inativo, nao_pagante);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"{0}", myReader["res"].ToString());
                }
            }

            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"-1");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"-999");
        connection.Close();
        return table.ToString();
    }


    [WebMethod]
    public static string apagar(string id_operador, string id_estado_contrato)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @id_estado_contrato int = {1};
                                            DECLARE @res int;

                                            EXEC APAGA_ESTADO_CONTRATO @id_estado_contrato, @id_operador, @res output

                                            select @res as res",
                                                id_operador, id_estado_contrato);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if(Int32.Parse(myReader["res"].ToString()) >= 0)
                    {
                        table.AppendFormat(@"Estado do Contrato apagado com sucesso!");
                    }
                    else
                    {
                        table.AppendFormat(@"Erro ao apagar estado do contrato!");
                    }
                }
            }

            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao apagar estado do contrato!");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao apagar estado do contrato!");
        connection.Close();
        return table.ToString();
    }


    [WebMethod]
    public static string getStatusPagamento(string id_estado_contrato)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_estado_contrato int = {0};

                                            ; with pag_cont_st as (
                                                select
	                                            pagamentos_statusid as id_estado,
	                                            ltrim(rtrim(codigo)) as cod_estado,
	                                            ltrim(rtrim(designacao)) as estado,
	                                            1 as presente
                                                from pagamentos_status
                                                where id_estado_contrato = @id_estado_contrato
                                            )

                                            select
                                                id_estado,
                                                cod_estado,
                                                estado,
                                                presente
                                            from (
                                                select *
                                                from pag_cont_st
                                                union
                                                select
	                                            pagamentos_statusid as id_estado,
	                                            ltrim(rtrim(codigo)) as cod_estado,
	                                            ltrim(rtrim(designacao)) as estado,
	                                            0 as presente
                                                from pagamentos_status
                                                where pagamentos_statusid not in (select id_estado from pag_cont_st)
                                            ) as tmp
                                            order by cod_estado", id_estado_contrato);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <div class='col-lg-10 col-md-10 col-sm-10 col-xs-10 nopadding' style='text-align:left;' id='divEstado_{0}'>
                                                <span class='variaveis' id='idEstadoPagamento_{0}'>{1}</span>
                                                {2}
                                            </div>
                                            <div class='col-lg-2 col-md-2 col-sm-2 col-xs-2 nopadding' style='text-align:right;' id='divEstadoCheckbox_{0}'>
                                                <input type='checkbox' class='form-control' id='estadoPagamentoCheckbox_{0}' name='estadoPagamentoCheckbox_{0}' style='margin: auto; width: 100%; cursor: pointer;' {3}/>
                                            </div>",
                                                   conta.ToString(),
                                                   myReader["id_estado"].ToString(),
                                                   myReader["estado"].ToString(),
                                                   myReader["presente"].ToString().Trim() == "1" ? "checked" : "");

                    conta++;
                }

                table.AppendFormat(@"<span class='variaveis' id='countEstadosPagamento'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"<span class='variaveis' id='countEstadosPagamento'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());

            connection.Close();
            return table.ToString();
        }
    }

    private void loadStatusPagamento()
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   select
	                                            pagamentos_statusid as id_estado,
	                                            ltrim(rtrim(codigo)) as cod_estado,
	                                            ltrim(rtrim(designacao)) as estado,
	                                            0 as presente
                                            from pagamentos_status
                                            order by codigo");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <div class='col-lg-10 col-md-10 col-sm-10 col-xs-10 nopadding' style='text-align:left;' id='divEstadoNew_{0}'>
                                                <span class='variaveis' id='idEstadoPagamentoNew_{0}'>{1}</span>
                                                {2}
                                            </div>
                                            <div class='col-lg-2 col-md-2 col-sm-2 col-xs-2 nopadding' style='text-align:right;' id='divEstadoCheckboxNew_{0}'>
                                                <input type='checkbox' class='form-control' id='estadoPagamentoCheckboxNew_{0}' name='estadoPagamentoCheckboxNew_{0}' style='margin: auto; width: 100%; cursor: pointer' {3}/>
                                            </div>",
                                                   conta.ToString(),
                                                   myReader["id_estado"].ToString(),
                                                   myReader["estado"].ToString(),
                                                   myReader["presente"].ToString().Trim() == "1" ? "checked" : "");

                    conta++;
                }

                table.AppendFormat(@"<span class='variaveis' id='countEstadosPagamentoNew'>{0}</span>", conta.ToString());

                connection.Close();
                divEstadosPagamentosNew.InnerHtml = table.ToString();
            }
            else
            {
                table.AppendFormat(@"<span class='variaveis' id='countEstadosPagamentoNew'>{0}</span>", conta.ToString());

                connection.Close();
                divEstadosPagamentosNew.InnerHtml = table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());

            connection.Close();
            divEstadosPagamentosNew.InnerHtml = table.ToString();
        }
    }

    [WebMethod]
    public static string alteraEstadoPagamento(string id_operador, string id_estado_contrato, string id_estado_pagamento)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @codop char(30) = (SELECT LTRIM(RTRIM(CODIGO)) FROM OPERADORES WHERE OPERADORESID = @id_operador)
	                                        DECLARE @id_estado_contrato int = {1};
                                            DECLARE @id_estado_pagamento int = {2};

                                            UPDATE PAGAMENTOS_STATUS
                                                SET CTRLDATA = GETDATE(),
                                                CTRLCODOP = @codop,
                                                ID_ESTADO_CONTRATO = @id_estado_contrato
                                            WHERE PAGAMENTOS_STATUSID = @id_estado_pagamento", id_operador, id_estado_contrato, id_estado_pagamento);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"0");
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"-999");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao apagar estado do contrato!");
        connection.Close();
        return table.ToString();
    }
}
