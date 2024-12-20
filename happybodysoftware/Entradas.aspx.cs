using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Entradas : Page
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
        loadNrEntradasDiaAnterior();
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

    private void loadNrEntradasDiaAnterior()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   set dateformat dmy
                                            declare @data_atual datetime = DATEADD(hh, -1, GETDATE())

                                            select count(distinct entradasid) as nr_entradas
                                            from entradas
                                            where cast(DATEADD(hh, -1, DATA_ENTRADA) as DATE) = CAST(DATEADD(dd, -1, @data_atual) as DATE)");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    totalDiaAnterior.InnerHtml = "Total Dia Anterior: " + myReader["nr_entradas"].ToString();
                }
            }
            else
            {
                connection.Close();
                totalDiaAnterior.InnerHtml = "Total Dia Anterior: 0";
            }

            connection.Close();
        }
        catch (Exception exc)
        {
            connection.Close();
            totalDiaAnterior.InnerHtml = "Total Dia Anterior: 0";
        }

        connection.Close();
    }

    [WebMethod]
    public static string loadSocios(string id_operador, string filtro, string query)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string declares = "";

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @FILTRO VARCHAR(MAX) = {1};
                                            DECLARE @data_inferior datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 00:00:00';
                                            DECLARE @data_superior datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 23:59:59';
                                            
                                            select 
                                                soc.sociosid as id_socio, soc.nr_socio as nr_socio, ltrim(rtrim(soc.nome)) as nome,
                                                ltrim(rtrim(st.designacao)) as estado, cast(st.inativo as int) as inativo
                                            from socios soc
                                            inner join CONTRATO cont on cont.socio_id = soc.sociosid
					                        inner join estados_contrato st on st.estados_contratoid = cont.estado_contrato_id
                                            where soc.sociosid not in (SELECT id_socio FROM REPORT_ENTRADAS(NULL, @data_inferior, @data_superior))
                                            and (@FILTRO IS NULL OR ({2}))
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
                                                    <th style='width:100%' colspan='2'>Sócio</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr onclick='' ondblclick=''>
                                            <td style='display:none;' id='id_{2}'>{0}</td>
                                            <td style='display:none;' id='estado_{2}'>{4}</td>
                                            <td style='display:none;' id='inativo_{2}'>{5}</td>
                                            <td style='display:none;' id='nrsocio_{2}'>{6}</td>
                                            <td class='tbodyTrTdLeft'>Sócio Nº {3}<br />{1}</td>
                                            <td class='tbodyTrTdRight'>
                                                <input id='btnEntradaSocio_{2}' value='Entrada' runat='server' type='button' onclick='declareEntrada({0}, {2});' style='background-color: #4282b5; width: 100%; height: auto; font-size: 12pt; text-align: center; line-height: auto; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/></td>
                                        </tr>",
                                                myReader["id_socio"].ToString(),
                                                myReader["nome"].ToString(),
                                                conta.ToString(),
                                                myReader["nr_socio"].ToString(),
                                                myReader["estado"].ToString(),
                                                myReader["inativo"].ToString(),
                                                myReader["nr_socio"].ToString());

                    conta++;
                }

                table.AppendFormat("</tbody></table>");
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
    public static string loadEntradas(string id_operador)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int
                                            DECLARE @data_inferior datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 00:00:00';
                                            DECLARE @data_superior datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 23:59:59';
                                            
                                            select 
                                                soc.sociosid as id_socio, soc.nr_socio as nr_socio, ltrim(rtrim(soc.nome)) as nome,
                                                rpt.data_entrada, rpt.hora_entrada, rpt.data_saida, rpt.hora_saida, rpt.id_entrada,
						                        rpt.cacifo, rpt.toalha, rpt.roupao,
						                        tp.CODIGO,
						                        tp.DESIGNACAO,
						                        tp.SEG,
						                        tp.TER,
						                        tp.QUA,
						                        tp.QUI,
						                        tp.SEX,
						                        tp.SAB,
						                        tp.DOM,
						                        tp.HORARIO_LIVRE,
						                        tp.HORA_ENTRADA_SEG,
						                        tp.HORA_SAIDA_SEG,
						                        tp.HORA_ENTRADA_TER,
						                        tp.HORA_SAIDA_TER,
						                        tp.HORA_ENTRADA_QUA,
						                        tp.HORA_SAIDA_QUA,
						                        tp.HORA_ENTRADA_QUI,
						                        tp.HORA_SAIDA_QUI,
						                        tp.HORA_ENTRADA_SEX,
						                        tp.HORA_SAIDA_SEX,
						                        tp.HORA_ENTRADA_SAB,
						                        tp.HORA_SAIDA_SAB,
						                        tp.HORA_ENTRADA_DOM,
						                        tp.HORA_SAIDA_DOM,
                                                CONVERT(VARCHAR(10), soc.DATA_NASCIMENTO, 103) as DATA_NASC,
                                                CONVERT(VARCHAR(10), cont.DATA_FIM, 103) as VALIDADE_CONTRATO,
                                                LTRIM(RTRIM(soc.notas)) as NOTAS,
                                                isnull((select top 1 
							                                convert(varchar(10), data_proxima_avaliacao, 103) + ' ' +
							                                convert(varchar(5), data_proxima_avaliacao, 108)
						                                from avaliacoes_fisicas where id_socio = soc.sociosid
										                order by data_proxima_avaliacao desc), '') as data_prox_av,
                                                case when CAST(@data_inferior AS DATE) <= CAST((isnull((select top 1 
							                                convert(varchar(10), data_proxima_avaliacao, 103) + ' ' +
							                                convert(varchar(5), data_proxima_avaliacao, 108)
						                                from avaliacoes_fisicas where id_socio = soc.sociosid
										                order by data_proxima_avaliacao desc), '')) AS DATE)
                                                then 0 else 1 end as af_fora_data
                                            from socios soc
                                            inner join report_entradas(@id_operador, @data_inferior, @data_superior) rpt on rpt.id_socio = soc.sociosid
					                        inner join CONTRATO cont on cont.SOCIO_ID = soc.sociosid and cont.CONTRATOID in (select distinct top 1 id_contrato from report_pagamentos(null) where nr_socio = soc.nr_socio)
					                        inner join REPORT_TIPOS_CONTRATO(null) tp on tp.TIPO_CONTRATOID = cont.TIPO_CONTRATO_ID
                                            WHERE (rpt.data_saida is null or rpt.data_saida = '')
                                            AND (rpt.hora_saida is null or rpt.hora_saida = '')
                                            ORDER BY rpt.id_entrada desc");

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid' style='width:100%; height: auto;'>
                                            <thead>
						                        <tr style='text-align: center; background-color: #000; color: #FFF;'>
                                                    <th style='width:100%; font-size: 1.5vw; text-align: center;'>ENTRADAS DO DIA</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr onclick='selectRow({2});' class='tableLine' id='ln_{2}' style='font-size: 1.1vw !important;'>
                                            <td style='display:none;' id='nr_socio_{2}'>{0}</td>
                                            <td style='display:none;' id='entrada_{2}'>{3}</td>
                                            <td style='display:none;' id='saida_{2}'>{9}</td>
                                            <td style='display:none;' id='aniversario_{2}'>{4}</td>
                                            <td style='display:none;' id='horario_{2}'>{5}</td>
                                            <td style='display:none;' id='notas_{2}'>{6}</td>
                                            <td style='display:none;' id='tipoContrato_{2}'>{7}</td>
                                            <td style='display:none;' id='validade_{2}'>{8}</td>
                                            <td style='display:none;' id='id_entrada_{2}'>{10}</td>
                                            <td style='display:none;' id='cacifo_{2}'>{11}</td>
                                            <td style='display:none;' id='toalha_{2}'>{12}</td>
                                            <td style='display:none;' id='roupao_{2}'>{13}</td>
                                            <td style='display:none;' id='af_{2}'>{14}</td>
                                            <td style='display:none;' id='afforaprazo_{2}'>{15}</td>
                                            <td class='tbodyTrTdLeft'>
                                                {0} - {1}
                                            </td>
                                        </tr>",
                                                myReader["nr_socio"].ToString(),
                                                myReader["nome"].ToString(),
                                                conta.ToString(),
                                                myReader["hora_entrada"].ToString(),
                                                myReader["DATA_NASC"].ToString(),
                                                myReader["HORARIO_LIVRE"].ToString() == "1" ? "Horário Livre" : "",
                                                myReader["NOTAS"].ToString(),
                                                myReader["DESIGNACAO"].ToString(),
                                                myReader["VALIDADE_CONTRATO"].ToString(),
                                                myReader["hora_saida"].ToString(),
                                                myReader["id_entrada"].ToString(),
                                                myReader["cacifo"].ToString(),
                                                myReader["toalha"].ToString(),
                                                myReader["roupao"].ToString(),
                                                myReader["data_prox_av"].ToString(),
                                                myReader["data_prox_av"].ToString() == "" ? "0" : myReader["af_fora_data"].ToString());

                    conta++;
                }

                table.AppendFormat("</tbody></table>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid' style='width:100%; height: auto;'>
                                            <thead>
						                        <tr style='text-align: center; background-color: #000; color: #FFF;'>
                                                    <th style='width:100%; font-size: 1.5vw; text-align: center; '>ENTRADAS DO DIA</th>
						                        </tr>
						                    </thead>
                                            <tbody>
                                            <tr class='tableLine' style='font-size: 1.1vw !important; text-align: center'>
                                            <td>Não existem entradas para o dia de hoje.</td>
                                            </tr>
                                            </tbody>
                                            </table>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            // Adiciona as linhas
            table.AppendFormat(@"   <td>{0}</td>", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string entrada(string id_operador, string nr_socio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @nr_socio int = {1};
                                            DECLARE @res int;

                                            EXEC DECLARA_ENTRADA_SOCIO @id_operador, @nr_socio, @res output;

                                            SELECT @res as res;", id_operador, nr_socio);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) == 0)
                    {
                        connection.Close();
                        return "Entrada declarada com sucesso!";
                    }
                    else
                    {
                        connection.Close();
                        return "Erro ao declarar entrada!";
                    }
                }
            }
            else
            {
                connection.Close();
                return "Erro ao declarar entrada!";
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            return "Erro ao declarar entrada!";
        }

        connection.Close();
        return "Erro ao declarar saída!";
    }

    [WebMethod]
    public static string saida(string id_operador, string id_entrada)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @id_entrada int = {1};
                                            DECLARE @res int;

                                            EXEC DECLARA_SAIDA_SOCIO @id_operador, @id_entrada, @res output;

                                            SELECT @res as res;", id_operador, id_entrada);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) == 0)
                    {
                        connection.Close();
                        return "Saída declarada com sucesso!";
                    }
                    else
                    {
                        connection.Close();
                        return "Erro ao declarar saída!";
                    }
                }
            }
            else
            {
                connection.Close();
                return "Erro ao declarar saída!";
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            return "Erro ao declarar saída!";
        }

        connection.Close();
        return "Erro ao declarar saída!";
    }

    [WebMethod]
    public static string nrClientesInstalacoes(string id_operador)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @nr_socio int;
                                            DECLARE @data_inferior datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 00:00:00';
                                            DECLARE @data_superior datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 23:59:59';
                                            
                                            select 
                                                count(id_entrada) as nr_clientes
                                            from report_entradas(null, @data_inferior, @data_superior)
                                            where LTRIM(RTRIM(data_saida)) = ''
                                            and LTRIM(RTRIM(hora_saida)) = ''");

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"{0}", myReader["nr_clientes"].ToString());
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
            table.AppendFormat("-1");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string totalClientesDia(string id_operador)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @nr_socio int;
                                            DECLARE @data_inferior datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 00:00:00';
                                            DECLARE @data_superior datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 23:59:59';
                                            
                                            select 
                                                count(id_entrada) as nr_clientes
                                            from report_entradas(null, @data_inferior, @data_superior)");

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"{0}", myReader["nr_clientes"].ToString());
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
            table.AppendFormat("-1");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string removerEntrada(string id_operador, string id_entrada)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @id_entrada int = {1};
                                            DECLARE @notaslog varchar(max);
                                            DECLARE @reslog int;
                                            
                                            select @notaslog = 'Foi removida a entrada do sócio ' + ltrim(rtrim(str(nr_socio))) + ' pelo operador ' + ltrim(rtrim(op.codigo))
                                            from entradas ent
                                            inner join socios soc on soc.sociosid = ent.id_socio
                                            inner join operadores op on op.operadoresid = @id_operador
                                            where ent.entradasid = @id_entrada

                                            DELETE FROM ENTRADAS
                                            WHERE ENTRADASID = @id_entrada

                                            EXEC REGISTA_LOG @id_operador, 'ENTRADAS', @notaslog, @reslog output;

                                            SELECT 0 as res", id_operador, id_entrada);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) == 0)
                    {
                        connection.Close();
                        return "Entrada apagada com sucesso!";
                    }
                    else
                    {
                        connection.Close();
                        return "Erro ao apagar entrada!";
                    }
                }
            }
            else
            {
                connection.Close();
                return "Erro ao apagar entrada!";
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            return "Erro ao declarar entrada!";
        }

        connection.Close();
        return "Erro ao declarar saída!";
    }

    [WebMethod]
    public static string verificaEntradaSaida(string id_operador, string nr_socio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @nr_socio int = {1};
                                            DECLARE @res int;
                                            DECLARE @msg varchar(max);

                                            EXEC DECLARA_ENTRADA_SAIDA_SOCIO @id_operador, @nr_socio, @res output, @msg output;

                                            SELECT @res as res, @msg as msg;", id_operador, nr_socio);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) >= 0)
                    {
                        table.AppendFormat(@"<span class='variaveis' id='resEntradaSaida'>{0}</span>", myReader["res"].ToString());
                        table.AppendFormat(@"<span class='variaveis' id='msgEntradaSaida'>{0}</span>", myReader["msg"].ToString());
                        connection.Close();
                        return table.ToString();
                    }
                    else
                    {
                        table.AppendFormat(@"<span class='variaveis' id='resEntradaSaida'>{0}</span>", myReader["res"].ToString());
                        table.AppendFormat(@"<span class='variaveis' id='msgEntradaSaida'>{0}</span>", myReader["msg"].ToString());
                        connection.Close();
                        return table.ToString();
                    }
                }
            }
            else
            {
                connection.Close();
                table.AppendFormat(@"<span class='variaveis' id='resEntradaSaida'>-999</span>");
                table.AppendFormat(@"<span class='variaveis' id='msgEntradaSaida'>ERRO!</span>");
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat(@"<span class='variaveis' id='resEntradaSaida'>-999</span>");
            table.AppendFormat(@"<span class='variaveis' id='msgEntradaSaida'>ERRO!</span>");
            return table.ToString();
        }

        connection.Close();
        table.AppendFormat(@"<span class='variaveis' id='resEntradaSaida'>-999</span>");
        table.AppendFormat(@"<span class='variaveis' id='msgEntradaSaida'>ERRO!</span>");
        return table.ToString();
    }

    [WebMethod]
    public static string updateCacifo(string id_operador, string id_entrada, string cacifo)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @id_entrada int = {1};
                                            DECLARE @cacifo bit = {2};
                                            DECLARE @res int;
                                            DECLARE @operatorCode char(30) = (SELECT LTRIM(RTRIM(CODIGO)) FROM OPERADORES WHERE OPERADORESID = @id_operador);
                                            DECLARE @notaslog varchar(max);
                                            DECLARE @reslog int;
                                            
                                            select @notaslog = 'Foi declarado cacifo na entrada do sócio ' + ltrim(rtrim(str(nr_socio))) + ' pelo operador ' + ltrim(rtrim(op.codigo))
                                            from entradas ent
                                            inner join socios soc on soc.sociosid = ent.id_socio
                                            inner join operadores op on op.operadoresid = @id_operador
                                            where ent.entradasid = @id_entrada

                                            UPDATE ENTRADAS
                                            SET CTRLDATA = GETDATE(), CTRLCODOP = @operatorCode, CACIFO = @cacifo
                                            WHERE ENTRADASID = @id_entrada

                                            EXEC REGISTA_LOG @id_operador, 'ENTRADAS', @notaslog, @reslog output;", id_operador, id_entrada, cacifo);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            connection.Close();
            return "Cacifo entregue com sucesso!";
        }
        catch (Exception exc)
        {
            connection.Close();
            return "Erro ao entregar cacifo!";
        }

        connection.Close();
        return "Erro ao entregar cacifo!";
    }

    [WebMethod]
    public static string updateToalha(string id_operador, string id_entrada, string toalha)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @id_entrada int = {1};
                                            DECLARE @toalha bit = {2};
                                            DECLARE @res int;
                                            DECLARE @operatorCode char(30) = (SELECT LTRIM(RTRIM(CODIGO)) FROM OPERADORES WHERE OPERADORESID = @id_operador);
                                            DECLARE @notaslog varchar(max);
                                            DECLARE @reslog int;
                                            
                                            select @notaslog = 'Foi declarada toalha na entrada do sócio ' + ltrim(rtrim(str(nr_socio))) + ' pelo operador ' + ltrim(rtrim(op.codigo))
                                            from entradas ent
                                            inner join socios soc on soc.sociosid = ent.id_socio
                                            inner join operadores op on op.operadoresid = @id_operador
                                            where ent.entradasid = @id_entrada

                                            UPDATE ENTRADAS
                                            SET CTRLDATA = GETDATE(), CTRLCODOP = @operatorCode, TOALHA = @toalha
                                            WHERE ENTRADASID = @id_entrada

                                            EXEC REGISTA_LOG @id_operador, 'ENTRADAS', @notaslog, @reslog output;", id_operador, id_entrada, toalha);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            connection.Close();
            return "Toalha entregue com sucesso!";
        }
        catch (Exception exc)
        {
            connection.Close();
            return "Erro ao entregar toalha!";
        }

        connection.Close();
        return "Erro ao entregar toalha!";
    }

    [WebMethod]
    public static string updateRoupao(string id_operador, string id_entrada, string roupao)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @id_entrada int = {1};
                                            DECLARE @roupao bit = {2};
                                            DECLARE @res int;
                                            DECLARE @operatorCode char(30) = (SELECT LTRIM(RTRIM(CODIGO)) FROM OPERADORES WHERE OPERADORESID = @id_operador);
                                            DECLARE @notaslog varchar(max);
                                            DECLARE @reslog int;
                                            
                                            select @notaslog = 'Foi declarado roupão na entrada do sócio ' + ltrim(rtrim(str(nr_socio))) + ' pelo operador ' + ltrim(rtrim(op.codigo))
                                            from entradas ent
                                            inner join socios soc on soc.sociosid = ent.id_socio
                                            inner join operadores op on op.operadoresid = @id_operador
                                            where ent.entradasid = @id_entrada

                                            UPDATE ENTRADAS
                                            SET CTRLDATA = GETDATE(), CTRLCODOP = @operatorCode, ROUPAO = @roupao
                                            WHERE ENTRADASID = @id_entrada

                                            EXEC REGISTA_LOG @id_operador, 'ENTRADAS', @notaslog, @reslog output;", id_operador, id_entrada, roupao);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            connection.Close();
            return "Roupão entregue com sucesso!";
        }
        catch (Exception exc)
        {
            connection.Close();
            return "Erro ao entregar roupão!";
        }

        connection.Close();
        return "Erro ao entregar roupão!";
    }

    [WebMethod]
    public static string declaraSaidaGeral(string id_operador)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int = {0};
                                            declare @operatorcode char(30) = (SELECT LTRIM(RTRIM(CODIGO)) FROM OPERADORES WHERE OPERADORESID = @id_operador);
                                            DECLARE @data_inferior datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 00:00:00';
                                            DECLARE @data_superior datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 23:59:59';
                                            DECLARE @notaslog varchar(max);
                                            DECLARE @reslog int;
                                            
                                            set @notaslog = 'Foi declarada saída geral pelo operador ' + @operatorcode
                                            
                                            UPDATE ENTRADAS
                                            SET CTRLDATA = GETDATE(), CTRLCODOP = @operatorcode, DATA_SAIDA = GETDATE()
                                            WHERE ENTRADASID IN (
                                            select rpt.id_entrada
                                            from socios soc
                                            inner join report_entradas(null, @data_inferior, @data_superior) rpt on rpt.id_socio = soc.sociosid
					                        inner join CONTRATO cont on cont.SOCIO_ID = soc.sociosid
					                        inner join REPORT_TIPOS_CONTRATO(null) tp on tp.TIPO_CONTRATOID = cont.TIPO_CONTRATO_ID
                                            WHERE rpt.data_saida = '' and rpt.hora_saida = '')

                                            EXEC REGISTA_LOG @id_operador, 'ENTRADAS', @notaslog, @reslog output;", id_operador);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            connection.Close();
            return "Saída geral declarada com sucesso!";
        }
        catch (Exception exc)
        {
            connection.Close();
            return "Erro ao declarar saída geral!";
        }

        connection.Close();
        return "Erro ao declarar saída geral!";
    }

    [WebMethod]
    public static string updateNotas(string id_operador, string notas, string nr_socio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int = {0};
                                            declare @notas varchar(max) = '{1}';
                                            declare @nr_socio int = {2};
                                            declare @operatorcode char(30) = (SELECT LTRIM(RTRIM(CODIGO)) FROM OPERADORES WHERE OPERADORESID = @id_operador);
                                            DECLARE @notaslog varchar(max);
                                            DECLARE @reslog int;
                                            
                                            select @notaslog = 'Foram atualizadas as notas do sócio ' + ltrim(rtrim(str(@nr_socio))) + ' pelo operador ' + @operatorcode
                                            
                                            UPDATE SOCIOS
                                            SET CTRLDATA = GETDATE(), CTRLCODOP = @operatorcode, NOTAS = @notas
                                            WHERE NR_SOCIO = @nr_socio

                                            EXEC REGISTA_LOG @id_operador, 'ENTRADAS', @notaslog, @reslog output;", id_operador, notas, nr_socio);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            connection.Close();
            return "Notas alteradas com sucesso!";
        }
        catch (Exception exc)
        {
            connection.Close();
            return "Erro ao alterar notas!";
        }

        connection.Close();
        return "Erro ao alterar notas!";
    }

    [WebMethod]
    public static string loadInfoUltimoRegisto(string id_entrada)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            SET DATEFIRST 1;

                                            DECLARE @id_operador int
                                            DECLARE @data_inferior datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 00:00:00';
                                            DECLARE @data_superior datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 23:59:59';
                                            DECLARE @id_entrada int = {0};
                                            DECLARE @dw int = DATEPART(dw, @data_inferior)
                                            
                                            select
                                                LTRIM(RTRIM(STR(soc.nr_socio))) + '.jpg' as foto, 
                                                rpt.hora_entrada, rpt.hora_saida,
                                                LTRIM(RTRIM(tp.DESIGNACAO)) as TIPO_CONTRATO,
                                                CONVERT(VARCHAR(10), soc.DATA_NASCIMENTO, 103) as DATA_NASC,
                                                CONVERT(VARCHAR(10), cont.DATA_FIM, 103) as VALIDADE_CONTRATO,
                                                LTRIM(RTRIM(soc.notas)) as NOTAS,
                                                CASE WHEN tp.HORARIO_LIVRE = 1 THEN 'Horário Livre'
                                                ELSE (CASE WHEN @dw = 1 THEN CONVERT(VARCHAR(5), tp.HORA_ENTRADA_SEG, 108) + ' - ' + CONVERT(VARCHAR(5), tp.HORA_SAIDA_SEG, 108)
	                                                ELSE (CASE WHEN @dw = 2 THEN CONVERT(VARCHAR(5), tp.HORA_ENTRADA_TER, 108) + ' - ' + CONVERT(VARCHAR(5), tp.HORA_SAIDA_TER, 108)
		                                                ELSE (CASE WHEN @dw = 3 THEN CONVERT(VARCHAR(5), tp.HORA_ENTRADA_QUA, 108) + ' - ' + CONVERT(VARCHAR(5), tp.HORA_SAIDA_QUA, 108)
			                                                ELSE (CASE WHEN @dw = 4 THEN CONVERT(VARCHAR(5), tp.HORA_ENTRADA_QUI, 108) + ' - ' + CONVERT(VARCHAR(5), tp.HORA_SAIDA_QUI, 108)
				                                                ELSE (CASE WHEN @dw = 5 THEN CONVERT(VARCHAR(5), tp.HORA_ENTRADA_SEX, 108) + ' - ' + CONVERT(VARCHAR(5), tp.HORA_SAIDA_SEX, 108)
					                                                ELSE (CASE WHEN @dw = 6 THEN CONVERT(VARCHAR(5), tp.HORA_ENTRADA_SAB, 108) + ' - ' + CONVERT(VARCHAR(5), tp.HORA_SAIDA_SAB, 108)
						                                                ELSE (CONVERT(VARCHAR(5), tp.HORA_ENTRADA_DOM, 108) + ' - ' + CONVERT(VARCHAR(5), tp.HORA_SAIDA_DOM, 108)) END) END) END) END) END) END) END as HORARIO,
                                                rpt.CACIFO, rpt.TOALHA, rpt.ROUPAO,
                                                soc.nr_socio,
                                                isnull((select 
							                                convert(varchar(10), data_proxima_avaliacao, 103) + ' ' +
							                                convert(varchar(5), data_proxima_avaliacao, 108)
						                                from avaliacoes_fisicas where id_socio = soc.sociosid), '') as data_prox_av,
                                                case when CAST(@data_inferior AS DATE) <= CAST((isnull((select 
							                                convert(varchar(10), data_proxima_avaliacao, 103) + ' ' +
							                                convert(varchar(5), data_proxima_avaliacao, 108)
						                                from avaliacoes_fisicas where id_socio = soc.sociosid), '')) AS DATE)
                                                then 0 else 1 end as af_fora_data
                                            from socios soc
					                        inner join report_entradas(@id_operador, @data_inferior, @data_superior) rpt on rpt.id_socio = soc.sociosid
					                        inner join CONTRATO cont on cont.SOCIO_ID = soc.sociosid
					                        inner join REPORT_TIPOS_CONTRATO(null) tp on tp.TIPO_CONTRATOID = cont.TIPO_CONTRATO_ID
                                            WHERE rpt.id_entrada = @id_entrada", id_entrada);

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
                    table.AppendFormat(@"   <span class='variaveis' id='ultimoRegistoHoraEntrada'>{0}</span>
                                            <span class='variaveis' id='ultimoRegistoHoraSaida'>{1}</span>
                                            <span class='variaveis' id='ultimoRegistoAniversario'>{2}</span>
                                            <span class='variaveis' id='ultimoRegistoCacifo'>{3}</span>
                                            <span class='variaveis' id='ultimoRegistoToalha'>{4}</span>
                                            <span class='variaveis' id='ultimoRegistoRoupao'>{5}</span>
                                            <span class='variaveis' id='ultimoRegistoTipoContrato'>{6}</span>
                                            <span class='variaveis' id='ultimoRegistoValidadeContrato'>{7}</span>
                                            <span class='variaveis' id='ultimoRegistoNotas'>{8}</span>
                                            <span class='variaveis' id='ultimoRegistoHorario'>{9}</span>
                                            <span class='variaveis' id='ultimoRegistoFoto'>{10}</span>
                                            <span class='variaveis' id='ultimoRegistoNrSocio'>{11}</span>
                                            <span class='variaveis' id='ultimoRegistoAF'>{12}</span>
                                            <span class='variaveis' id='ultimoRegistoAFForaPrazo'>{13}</span>",
                                                myReader["hora_entrada"].ToString(),
                                                myReader["hora_saida"].ToString(),
                                                myReader["DATA_NASC"].ToString(),
                                                myReader["CACIFO"].ToString(),
                                                myReader["TOALHA"].ToString(),
                                                myReader["ROUPAO"].ToString(),
                                                myReader["TIPO_CONTRATO"].ToString(),
                                                myReader["VALIDADE_CONTRATO"].ToString(),
                                                myReader["NOTAS"].ToString(),
                                                myReader["HORARIO"].ToString(),
                                                myReader["foto"].ToString(),
                                                myReader["nr_socio"].ToString(),
                                                myReader["data_prox_av"].ToString(),
                                                myReader["data_prox_av"].ToString() == "" ? "0" : myReader["af_fora_data"].ToString());
                }

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

    [WebMethod]
    public static string loadGrafico(string id_operador)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int
                                            DECLARE @data_inferior datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 00:00:00';
                                            DECLARE @data_superior datetime = CONVERT(VARCHAR(10), dateadd(hh, -1, GETDATE()), 103) + ' 23:59:59';

                                            select *
                                            from (
                                                select count(id_entrada) as nr_entradas, '07:00' as hora, '7' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('07:00' as time)
                                                and CAST(hora_entrada as time) < CAST('07:30' as time)
						                        UNION
						                        select count(id_entrada) as nr_entradas, '07:30' as hora, '73' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('07:30' as time)
                                                and CAST(hora_entrada as time) < CAST('08:00' as time)
						                        UNION
                                                select count(id_entrada) as nr_entradas, '08:00' as hora, '8' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('08:00' as time)
                                                and CAST(hora_entrada as time) < CAST('08:30' as time)
                                                UNION
						                        select count(id_entrada) as nr_entradas, '08:30' as hora, '83' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('08:30' as time)
                                                and CAST(hora_entrada as time) < CAST('09:00' as time)
						                        UNION
                                                select count(id_entrada) as nr_entradas, '09:00' as hora, '9' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('09:00' as time)
                                                and CAST(hora_entrada as time) < CAST('09:30' as time)
                                                UNION
						                        select count(id_entrada) as nr_entradas, '09:30' as hora, '93' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('09:30' as time)
                                                and CAST(hora_entrada as time) < CAST('10:00' as time)
						                        UNION
                                                select count(id_entrada) as nr_entradas, '10:00' as hora, '10' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('10:00' as time)
                                                and CAST(hora_entrada as time) < CAST('10:30' as time)
                                                UNION
						                        select count(id_entrada) as nr_entradas, '10:30' as hora, '103' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('10:30' as time)
                                                and CAST(hora_entrada as time) < CAST('11:00' as time)
						                        UNION
                                                select count(id_entrada) as nr_entradas, '11:00' as hora, '11' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('11:00' as time)
                                                and CAST(hora_entrada as time) < CAST('11:30' as time)
                                                UNION
						                        select count(id_entrada) as nr_entradas, '11:30' as hora, '113' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('11:30' as time)
                                                and CAST(hora_entrada as time) < CAST('12:00' as time)
						                        UNION
                                                select count(id_entrada) as nr_entradas, '12:00' as hora, '12' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('12:00' as time)
                                                and CAST(hora_entrada as time) < CAST('12:30' as time)
                                                UNION
						                        select count(id_entrada) as nr_entradas, '12:30' as hora, '123' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('12:30' as time)
                                                and CAST(hora_entrada as time) < CAST('13:00' as time)
						                        UNION
                                                select count(id_entrada) as nr_entradas, '13:00' as hora, '13' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('13:00' as time)
                                                and CAST(hora_entrada as time) < CAST('13:30' as time)
                                                UNION
						                        select count(id_entrada) as nr_entradas, '13:30' as hora, '133' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('13:30' as time)
                                                and CAST(hora_entrada as time) < CAST('13:00' as time)
						                        UNION
                                                select count(id_entrada) as nr_entradas, '14:00' as hora, '14' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('14:00' as time)
                                                and CAST(hora_entrada as time) < CAST('14:30' as time)
                                                UNION
						                        select count(id_entrada) as nr_entradas, '14:30' as hora, '143' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('14:30' as time)
                                                and CAST(hora_entrada as time) < CAST('15:00' as time)
						                        UNION
                                                select count(id_entrada) as nr_entradas, '15:00' as hora, '15' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('15:00' as time)
                                                and CAST(hora_entrada as time) < CAST('15:30' as time)
                                                UNION
						                        select count(id_entrada) as nr_entradas, '15:30' as hora, '153' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('15:30' as time)
                                                and CAST(hora_entrada as time) < CAST('16:00' as time)
						                        UNION
                                                select count(id_entrada) as nr_entradas, '16:00' as hora, '16' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('16:00' as time)
                                                and CAST(hora_entrada as time) < CAST('16:30' as time)
                                                UNION
						                        select count(id_entrada) as nr_entradas, '16:30' as hora, '163' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('16:30' as time)
                                                and CAST(hora_entrada as time) < CAST('17:00' as time)
						                        UNION
                                                select count(id_entrada) as nr_entradas, '17:00' as hora, '17' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('17:00' as time)
                                                and CAST(hora_entrada as time) < CAST('17:30' as time)
                                                UNION
						                        select count(id_entrada) as nr_entradas, '17:30' as hora, '173' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('17:30' as time)
                                                and CAST(hora_entrada as time) < CAST('18:00' as time)
						                        UNION
                                                select count(id_entrada) as nr_entradas, '18:00' as hora, '18' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('18:00' as time)
                                                and CAST(hora_entrada as time) < CAST('18:30' as time)
                                                UNION
						                        select count(id_entrada) as nr_entradas, '18:30' as hora, '183' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('18:30' as time)
                                                and CAST(hora_entrada as time) < CAST('19:00' as time)
						                        UNION
                                                select count(id_entrada) as nr_entradas, '19:00' as hora, '19' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('19:00' as time)
                                                and CAST(hora_entrada as time) < CAST('19:30' as time)
                                                UNION
						                        select count(id_entrada) as nr_entradas, '19:30' as hora, '193' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('19:30' as time)
                                                and CAST(hora_entrada as time) < CAST('20:00' as time)
						                        UNION
                                                select count(id_entrada) as nr_entradas, '20:00' as hora, '20' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('20:00' as time)
                                                and CAST(hora_entrada as time) < CAST('20:30' as time)
                                                UNION
						                        select count(id_entrada) as nr_entradas, '20:30' as hora, '203' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('20:30' as time)
                                                and CAST(hora_entrada as time) < CAST('21:00' as time)
						                        UNION
                                                select count(id_entrada) as nr_entradas, '21:00' as hora, '21' as flag
                                                from report_entradas(@id_operador, @data_inferior, @data_superior)
                                                where CAST(hora_entrada as time) >= CAST('21:00' as time)
                                                and CAST(hora_entrada as time) <= CAST('21:30' as time)
                                            ) as tmp
                                            order by CAST(hora as TIME), nr_entradas");

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
                    table.AppendFormat(@"   <span class='variaveis' id='entradaGrafico{2}'>{1}</span>
                                            <span class='variaveis' id='nrEntradas{2}'>{0}</span>",
                                                myReader["nr_entradas"].ToString(),
                                                myReader["hora"].ToString(),
                                                myReader["flag"].ToString());

                    conta++;
                }

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

    [WebMethod]
    public static string loadDatasEntradasSocio(string nr_socio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   set dateformat dmy
                                            declare @nr_socio int = {0};
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


    [WebMethod]
    public static string loadAvisos(string id_entrada)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        string ret = "";

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_entrada int = {0};
                                            DECLARE @data_atual datetime = DATEADD(hh, -1, GETDATE());

                                            SELECT av.NOTAS, av.VALOR
                                            FROM AVISOS av
                                            INNER JOIN ENTRADAS ent on ent.ID_SOCIO = av.ID_SOCIO
                                            WHERE ent.ENTRADASID = @id_entrada
                                            AND (CAST(av.DATA_AVISO as DATE) = CAST(@data_atual as DATE)
                                            OR av.DATA_AVISO is null)", id_entrada);

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


    [WebMethod]
    public static string changeDateAF(string id_operador, string data, string nr_socio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @data datetime = '{1}';
                                            DECLARE @nr_socio int = {2};
                                            DECLARE @id_socio int = (select sociosid from socios where nr_socio = @nr_socio)
                                            DECLARE @id_avaliacao int = (select top 1 avaliacoes_fisicasid from avaliacoes_fisicas where id_socio = @id_socio order by data desc)
                                            DECLARE @notas varchar(max);
                                            DECLARE @reslog int;
                                            UPDATE avaliacoes_fisicas set data_proxima_avaliacao = @data
                                            where avaliacoes_fisicasid = @id_avaliacao

                                            select @notas = 'O operador ' + op.codigo + ' alterou a data da próxima avaliação física do sócio nº '
                                                + ltrim(rtrim(str(@nr_socio))) + ' para o dia ' + convert(varchar(10), @data, 103) + ' '
                                                + convert(varchar(8), @data, 108)                                            
                                            from operadores op
                                            where operadoresid = @id_operador

                                            EXEC REGISTA_LOG @id_operador, 'ENTRADAS', @notas, @reslog output;", id_operador, data, nr_socio);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            connection.Close();

            return "0";
        }
        catch (Exception exc)
        {
            connection.Close();
            return "-999";
        }
    }
}
