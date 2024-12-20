using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.Net.Mail;
using System.IO;

public partial class Avaliacoes : Page
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
                                                (SELECT TOP 1 CONVERT(VARCHAR(10), DATA_INICIO, 103) FROM CONTRATO cont WHERE soc.sociosid = cont.socio_id order by data_fim desc) as DATA_INICIO_CONTRATO,
						                        (SELECT TOP 1 CONVERT(VARCHAR(10), DATA_FIM, 103) FROM CONTRATO cont WHERE soc.sociosid = cont.socio_id order by data_fim desc) as DATA_FIM_CONTRATO
                                            FROM SOCIOS soc
                                            INNER JOIN OPERADORES op on op.OPERADORESID = soc.ID_COMERCIAL
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
                    table.AppendFormat(@"<tr onclick='selectSocio({0}, {2});' id='ln_{2}'>
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
    public static string loadAvaliacoes(string id_operador, string id_socio, string data)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @id_socio int = {1};
                                            DECLARE @data date = {2};
                                            DECLARE @id_avaliacao int;

                                            SELECT 
                                                ID_SOCIO,
	                                            NOME_SOCIO,
	                                            NR_SOCIO,
	                                            ID_AVALIACAO,
	                                            SEGUIU_PROTOCOLO_AF,
	                                            PATOLOGIAS_ANTECEDENTES_MEDICACAO,
	                                            TREINA_QUANTO_TEMPO,
	                                            O_QUE_FEZ_COMO_TREINO,
	                                            DURANTE_QTO_TEMPO,
	                                            HORAS_DISPONIVEIS_TREINO,
	                                            NR_VEZES_TREINO_SEMANA,
	                                            AULAS_OU_MUSCULACAO,
	                                            OBJETIVO_TREINO,
	                                            PREFERENCIA_DESPORTO,
	                                            AVALIACAO_PRATICA,
	                                            IDADE,
	                                            ALTURA,
	                                            PESO,
	                                            PERC_MG,
	                                            KG_MM,
	                                            KG_OSSO,
	                                            IMC,
	                                            METAB_BASAL,
	                                            IDADE_METAB,
	                                            PERC_H2O,
	                                            GORDURA_VISCERAL,
	                                            PERIM_CINT,
	                                            PERIM_ANCA,
	                                            ICA,
	                                            TA,
	                                            FC_REPOUSO,
	                                            PLANO_TREINO,	
	                                            NOTAS,
	                                            DATA_AVALIACAO,
	                                            OPERADOR_AVALIACAO,
	                                            ID_OPERADOR_AVALIACAO,
	                                            DATA_ULTIMA_ALTERACAO,
	                                            OPERADOR_ULTIMA_ALTERACAO,
	                                            ID_OPERADOR_ULTIMA_ALTERACAO,
                                                DATA_PROXIMA_AVALIACAO,
                                                HORA_PROXIMA_AVALIACAO,
                                                NOME_OPERADOR
                                            FROM REPORT_AVALIACOES(@id_socio, @data, @id_avaliacao)
                                            ORDER BY DATA_AVALIACAO", id_operador, id_socio == "" ? "NULL" : id_socio, data == string.Empty ? "NULL" : string.Format("'{0}'", data));
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
                                                    <th style='-webkit-border-radius: 4px !important; border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important; text-align:center;' colspan='2'>Avaliações</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    table.AppendFormat(@"<tr ondblclick='openAvaliacaoInfo({0});'>
                                            <td style='width: 85%;'>
                                                {1}<br />
                                                {2}
                                            </td>
                                            <td style='width: 15%;'>
                                                <input type='button' onclick='deleteAvaliacao({0});' value='Apagar Avaliação' class='form-control' style='width: 100%;' />
                                            </td>
                                        </tr>",
                                                myReader["ID_AVALIACAO"].ToString(),
                                                myReader["DATA_AVALIACAO"].ToString(),
                                                myReader["OPERADOR_AVALIACAO"].ToString());

                    conta++;
                }

                table.AppendFormat(@"<tr>
                                            <td colspan='2'><input type='button' onclick='novaAvaliacao({0});' value='Nova Avaliação' class='form-control' style='width: 100%; height: 100%' /></td>
                                        </tr>",
                                                id_socio);

                table.AppendFormat("</tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='nrAvaliacoes'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th style='-webkit-border-radius: 4px !important; border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;'>Avaliações</th>
						                        </tr>
						                    </thead>
                                            <tbody>");


                table.AppendFormat(@"<tr>
                                            <td><input type='button' onclick='novaAvaliacao({0});' value='Nova Avaliação' class='form-control' style='width: 100%; height: 100%' /></td>
                                        </tr>",
                                                id_socio);

                table.AppendFormat("</tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='nrAvaliacoes'>{0}</span>", "0");

                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            // Adiciona as linhas
            table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th style='-webkit-border-radius: 4px !important; border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;'>Avaliações</th>
						                        </tr>
						                    </thead>
                                            <tbody>");


            table.AppendFormat(@"<tr>
                                            <td>Por favor, insira uma data válida para pesquisar!</td>
                                        </tr>",
                                            exc.ToString());

            table.AppendFormat("</tbody></table></div>");
            table.AppendFormat("<span class='variaveis' id='nrAvaliacoes'>{0}</span>", "0");

            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string loadInfoAvaliacao(string id_socio, string id_operador)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_socio int = {0};
                                            DECLARE @id_operador int = {1};
                                            DECLARE @data_atual datetime = DATEADD(hh, -1, GETDATE())
                                            DECLARE @data_nascimento datetime = (select DATA_NASCIMENTO from socios where sociosid = @id_socio)
                                            DECLARE @iMonthDayDob int = CAST(datepart (mm, @data_nascimento) * 100 + datepart  (dd, @data_nascimento) AS int)
                                            DECLARE @iMonthDayPassedDate int = CAST(datepart (mm, @data_atual) * 100 + datepart  (dd, @data_atual) AS int)
                                            DECLARE @flag int = (select case when @iMonthDayDob <= @iMonthDayPassedDate THEN 0 ELSE 1 END)

                                            select top 1
                                                soc.NR_SOCIO,
                                                LTRIM(RTRIM(soc.PROFISSAO)) as PROFISSAO,
                                                LTRIM(RTRIM(soc.NOME)) as NOME,
						                        (DATEDIFF(yy, @data_nascimento, @data_atual) - @flag) as IDADE,
                                                tpcont.DESIGNACAO as TIPO_CONTRATO,
                                                CONVERT(VARCHAR(10), cont.DATA_INICIO, 103) as DATA_INICIO_CONTRATO,
                                                DATEDIFF(mm, (DATEADD(hh, -1, cont.DATA_INICIO)), (DATEADD(dd, 1, DATEADD(hh, -1, cont.DATA_FIM)))) as DURACAO_CONTRATO,
                                                (CONVERT(VARCHAR(10), @data_atual, 103) + ' ' + CONVERT(VARCHAR(5), @data_atual, 108)) as DATA_AVALIACAO,
                                                (SELECT LTRIM(RTRIM(NOME)) FROM OPERADORES WHERE OPERADORESID = @id_operador) as OPERADOR 
                                            from socios soc
                                            inner join contrato cont on cont.socio_id = soc.sociosid
                                            inner join tipo_contrato tpcont on tpcont.tipo_contratoid = cont.tipo_contrato_id
                                            where soc.sociosid = @id_socio
                                            order by cont.data_fim desc", id_socio, id_operador);
            //return sql;
            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"<span class='variaveis' id='nrSocio_{5}'>{0}</span>
                                        <span class='variaveis' id='nomeSocio_{5}'>{9}</span>
                                        <span class='variaveis' id='profissao_{5}'>{1}</span>
                                        <span class='variaveis' id='tipoContrato_{5}'>{2}</span>
                                        <span class='variaveis' id='dataInicioContrato_{5}'>{3}</span>
                                        <span class='variaveis' id='duracaoContrato_{5}'>{4}</span>
                                        <span class='variaveis' id='dataAvaliacao_{5}'>{6}</span>
                                        <span class='variaveis' id='idade_{5}'>{7}</span>
                                        <span class='variaveis' id='operador_{5}'>{8}</span>",
                                                myReader["NR_SOCIO"].ToString(),
                                                myReader["PROFISSAO"].ToString(),
                                                myReader["TIPO_CONTRATO"].ToString(),
                                                myReader["DATA_INICIO_CONTRATO"].ToString(),
                                                myReader["DURACAO_CONTRATO"].ToString(),
                                                id_socio,
                                                myReader["DATA_AVALIACAO"].ToString(),
                                                myReader["IDADE"].ToString(),
                                                myReader["OPERADOR"].ToString(),
                                                myReader["NOME"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                if (id_socio == "")
                {
                    table.AppendFormat(@"Por favor, selecione um sócio");

                    connection.Close();
                    return table.ToString();
                }

                table.AppendFormat(@"<span class='variaveis' id='nrSocio_{5}'>{0}</span>
                                        <span class='variaveis' id='profissao_{5}'>{1}</span>
                                        <span class='variaveis' id='tipoContrato_{5}'>{2}</span>
                                        <span class='variaveis' id='dataInicioContrato_{5}'>{3}</span>
                                        <span class='variaveis' id='duracaoContrato_{5}'>{4}</span>",
                                                "0", "", "", "", "0", id_socio);

                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"<span class='variaveis' id='nrSocio_{5}'>{0}</span>
                                        <span class='variaveis' id='profissao_{5}'>{1}</span>
                                        <span class='variaveis' id='tipoContrato_{5}'>{2}</span>
                                        <span class='variaveis' id='dataInicioContrato_{5}'>{3}</span>
                                        <span class='variaveis' id='duracaoContrato_{5}'>{4}</span>",
                                                "0", "", "", "", "0", id_socio);

            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string registaAvaliacao(string id_operador, string id_socio, string seguiuprotocoloaf, string patologiasantecedentesmedicacao, string treinaquantotempo,
        string oquefezcomotreino, string durantequantotempo, string horasdisponiveistreino, string nrvezestreinosemana, string aulasoumusculacao, string objetivotreino,
        string preferenciadesporto, string avaliacaopratica, string idade, string altura, string peso, string percmg, string kgmm, string kgosso, string imc,
        string metabolismobasal, string idademetabolica, string percagua, string gorduravisceral, string perimcintura, string perimanca, string ica, string ta,
        string fcrepouso, string planotreino, string notas, string data, string dataproximaavaliacao, string nomeoperador)
    {
        var table = new StringBuilder();
        string cs = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        notas = notas.Replace("''", "\"");
        notas = notas.Replace("'", "''");

        patologiasantecedentesmedicacao = patologiasantecedentesmedicacao.Replace("''", "\"");
        patologiasantecedentesmedicacao = patologiasantecedentesmedicacao.Replace("'", "''");

        treinaquantotempo = treinaquantotempo.Replace("''", "\"");
        treinaquantotempo = treinaquantotempo.Replace("'", "''");

        oquefezcomotreino = oquefezcomotreino.Replace("''", "\"");
        oquefezcomotreino = oquefezcomotreino.Replace("'", "''");

        durantequantotempo = durantequantotempo.Replace("''", "\"");
        durantequantotempo = durantequantotempo.Replace("'", "''");

        horasdisponiveistreino = horasdisponiveistreino.Replace("''", "\"");
        horasdisponiveistreino = horasdisponiveistreino.Replace("'", "''");

        nrvezestreinosemana = nrvezestreinosemana.Replace("''", "\"");
        nrvezestreinosemana = nrvezestreinosemana.Replace("'", "''");

        aulasoumusculacao = aulasoumusculacao.Replace("''", "\"");
        aulasoumusculacao = aulasoumusculacao.Replace("'", "''");

        objetivotreino = objetivotreino.Replace("''", "\"");
        objetivotreino = objetivotreino.Replace("'", "''");

        preferenciadesporto = preferenciadesporto.Replace("''", "\"");
        preferenciadesporto = preferenciadesporto.Replace("'", "''");

        avaliacaopratica = avaliacaopratica.Replace("''", "\"");
        avaliacaopratica = avaliacaopratica.Replace("'", "''");

        ta = ta.Replace("''", "\"");
        ta = ta.Replace("'", "''");

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   set dateformat dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @id_socio int = {1}
	                                        DECLARE @seguiuprotocoloaf bit = {2}
	                                        DECLARE @patologiasantecedentesmedicacao [VARCHAR](MAX) = {3};
	                                        DECLARE @treinaquantotempo [VARCHAR](MAX) = {4};
	                                        DECLARE @oquefezcomotreino [VARCHAR](MAX) = {5};
	                                        DECLARE @durantequantotempo [VARCHAR](MAX) = {6};
	                                        DECLARE @horasdisponiveistreino [VARCHAR](MAX) = {7};
	                                        DECLARE @nrvezestreinosemana [VARCHAR](MAX) = {8};
	                                        DECLARE @aulasoumusculacao [VARCHAR](MAX) = {9};
	                                        DECLARE @objetivotreino [VARCHAR](MAX) = {10};
	                                        DECLARE @preferenciadesporto [VARCHAR](MAX) = {11};
	                                        DECLARE @avaliacaopratica [VARCHAR](MAX) = {12};
	                                        DECLARE @idade [int] = {13};
	                                        DECLARE @altura [DECIMAL](15,2) = {14};
	                                        DECLARE @peso [DECIMAL](15,3) = {15};
	                                        DECLARE @percmg [DECIMAL](15,3) = {16};
	                                        DECLARE @kgmm [DECIMAL](15,3) = {17};
	                                        DECLARE @kgosso [DECIMAL](15,3) = {18};
	                                        DECLARE @imc [DECIMAL](15,3) = {19};
	                                        DECLARE @metabolismobasal [DECIMAL](15,3) = {20};
	                                        DECLARE @idademetabolica [DECIMAL](15,3) = {21};
	                                        DECLARE @percagua [DECIMAL](15,3) = {22};
	                                        DECLARE @gorduravisceral [DECIMAL](15,3) = {23};
	                                        DECLARE @perimcintura [DECIMAL](15,3) = {24};
	                                        DECLARE @perimanca [DECIMAL](15,3) = {25};
	                                        DECLARE @ica [DECIMAL](15,3) = {26};
	                                        DECLARE @ta [VARCHAR](MAX) = {27};
	                                        DECLARE @fcrepouso [DECIMAL](15,3) = {28};
	                                        DECLARE @plano_treino [BIT] = {29};
	                                        DECLARE @notas [varchar](max) = {30};
	                                        DECLARE @data [datetime] = {31};
	                                        DECLARE @ret int;
	                                        DECLARE @res varchar(max);
                                            DECLARE @dataproximaavaliacao datetime = {32};
                                            DECLARE @nomeoperador varchar(500) = {33};

                                            EXEC REGISTA_AVALIACAO @id_operador, @id_socio, @seguiuprotocoloaf, @patologiasantecedentesmedicacao,
                                                @treinaquantotempo,@oquefezcomotreino, @durantequantotempo, @horasdisponiveistreino, @nrvezestreinosemana,
	                                            @aulasoumusculacao, @objetivotreino, @preferenciadesporto, @avaliacaopratica, @idade, @altura, @peso, @percmg,
	                                            @kgmm, @kgosso, @imc, @metabolismobasal, @idademetabolica, @percagua, @gorduravisceral, @perimcintura,
	                                            @perimanca, @ica, @ta, @fcrepouso, @plano_treino, @notas, @data, @dataproximaavaliacao, @nomeoperador, @ret output, @res output

                                            select @ret as ret, @res as res", id_operador == "" ? "0" : id_operador
                                            , id_socio == "" ? "0" : id_socio
                                            , seguiuprotocoloaf == "" || seguiuprotocoloaf == "undefined" ? "0" : seguiuprotocoloaf
                                            , patologiasantecedentesmedicacao == "" ? "''" : "'" + patologiasantecedentesmedicacao + "'"
                                            , treinaquantotempo == "" ? "''" : "'" + treinaquantotempo + "'"
                                            , oquefezcomotreino == "" ? "''" : "'" + oquefezcomotreino + "'"
                                            , durantequantotempo == "" ? "''" : "'" + durantequantotempo + "'"
                                            , horasdisponiveistreino == "" ? "''" : "'" + horasdisponiveistreino + "'"
                                            , nrvezestreinosemana == "" ? "''" : "'" + nrvezestreinosemana + "'"
                                            , aulasoumusculacao == "" ? "''" : "'" + aulasoumusculacao + "'"
                                            , objetivotreino == "" ? "''" : "'" + objetivotreino + "'"
                                            , preferenciadesporto == "" ? "''" : "'" + preferenciadesporto + "'"
                                            , avaliacaopratica == "" ? "''" : "'" + avaliacaopratica + "'"
                                            , idade == "" ? "0" : idade
                                            , altura == "" ? "0" : altura
                                            , peso == "" ? "0" : peso
                                            , percmg == "" ? "0" : percmg
                                            , kgmm == "" ? "0" : kgmm
                                            , kgosso == "" ? "0" : kgosso
                                            , imc == "" ? "0" : imc
                                            , metabolismobasal == "" ? "0" : metabolismobasal
                                            , idademetabolica == "" ? "0" : idademetabolica
                                            , percagua == "" ? "0" : percagua
                                            , gorduravisceral == "" ? "0" : gorduravisceral
                                            , perimcintura == "" ? "0" : perimcintura
                                            , perimanca == "" ? "0" : perimanca
                                            , ica == "" ? "0" : ica
                                            , ta == "" ? "''" : "'" + ta + "'"
                                            , fcrepouso == "" ? "0" : fcrepouso
                                            , planotreino == "" || planotreino == "undefined" ? "0" : planotreino
                                            , notas == "" ? "''" : "'" + notas + "'"
                                            , data == "" ? "''" : "'" + data + "'"
                                            , dataproximaavaliacao == "" ? "NULL" : "'" + dataproximaavaliacao + "'"
                                            , nomeoperador == "" ? "NULL" : "'" + nomeoperador + "'");
            //return sql;
            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"{0}", myReader["res"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"Ocorreu um erro ao inserir a avaliação");

                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Ocorreu um erro ao inserir a avaliação: {0}", exc.ToString());

            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string alteraAvaliacao(string id_operador, string id_socio, string seguiuprotocoloaf, string patologiasantecedentesmedicacao, string treinaquantotempo,
        string oquefezcomotreino, string durantequantotempo, string horasdisponiveistreino, string nrvezestreinosemana, string aulasoumusculacao, string objetivotreino,
        string preferenciadesporto, string avaliacaopratica, string idade, string altura, string peso, string percmg, string kgmm, string kgosso, string imc,
        string metabolismobasal, string idademetabolica, string percagua, string gorduravisceral, string perimcintura, string perimanca, string ica, string ta,
        string fcrepouso, string planotreino, string notas, string data, string id_avaliacao, string id_operador_avaliacao, string dataproximaavaliacao, string nomeoperador)
    {
        var table = new StringBuilder();
        string cs = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        notas = notas.Replace("''", "\"");
        notas = notas.Replace("'", "''");

        patologiasantecedentesmedicacao = patologiasantecedentesmedicacao.Replace("''", "\"");
        patologiasantecedentesmedicacao = patologiasantecedentesmedicacao.Replace("'", "''");

        treinaquantotempo = treinaquantotempo.Replace("''", "\"");
        treinaquantotempo = treinaquantotempo.Replace("'", "''");

        oquefezcomotreino = oquefezcomotreino.Replace("''", "\"");
        oquefezcomotreino = oquefezcomotreino.Replace("'", "''");

        durantequantotempo = durantequantotempo.Replace("''", "\"");
        durantequantotempo = durantequantotempo.Replace("'", "''");

        horasdisponiveistreino = horasdisponiveistreino.Replace("''", "\"");
        horasdisponiveistreino = horasdisponiveistreino.Replace("'", "''");

        nrvezestreinosemana = nrvezestreinosemana.Replace("''", "\"");
        nrvezestreinosemana = nrvezestreinosemana.Replace("'", "''");

        aulasoumusculacao = aulasoumusculacao.Replace("''", "\"");
        aulasoumusculacao = aulasoumusculacao.Replace("'", "''");

        objetivotreino = objetivotreino.Replace("''", "\"");
        objetivotreino = objetivotreino.Replace("'", "''");

        preferenciadesporto = preferenciadesporto.Replace("''", "\"");
        preferenciadesporto = preferenciadesporto.Replace("'", "''");

        avaliacaopratica = avaliacaopratica.Replace("''", "\"");
        avaliacaopratica = avaliacaopratica.Replace("'", "''");

        ta = ta.Replace("''", "\"");
        ta = ta.Replace("'", "''");

        try
        {
            connection.Open();

            string sql = string.Format(@"   set dateformat dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @id_socio int = {1}
	                                        DECLARE @seguiuprotocoloaf bit = {2}
	                                        DECLARE @patologiasantecedentesmedicacao [VARCHAR](MAX) = {3};
	                                        DECLARE @treinaquantotempo [VARCHAR](MAX) = {4};
	                                        DECLARE @oquefezcomotreino [VARCHAR](MAX) = {5};
	                                        DECLARE @durantequantotempo [VARCHAR](MAX) = {6};
	                                        DECLARE @horasdisponiveistreino [VARCHAR](MAX) = {7};
	                                        DECLARE @nrvezestreinosemana [VARCHAR](MAX) = {8};
	                                        DECLARE @aulasoumusculacao [VARCHAR](MAX) = {9};
	                                        DECLARE @objetivotreino [VARCHAR](MAX) = {10};
	                                        DECLARE @preferenciadesporto [VARCHAR](MAX) = {11};
	                                        DECLARE @avaliacaopratica [VARCHAR](MAX) = {12};
	                                        DECLARE @idade [int] = {13};
	                                        DECLARE @altura [DECIMAL](15,2) = {14};
	                                        DECLARE @peso [DECIMAL](15,3) = {15};
	                                        DECLARE @percmg [DECIMAL](15,3) = {16};
	                                        DECLARE @kgmm [DECIMAL](15,3) = {17};
	                                        DECLARE @kgosso [DECIMAL](15,3) = {18};
	                                        DECLARE @imc [DECIMAL](15,3) = {19};
	                                        DECLARE @metabolismobasal [DECIMAL](15,3) = {20};
	                                        DECLARE @idademetabolica [DECIMAL](15,3) = {21};
	                                        DECLARE @percagua [DECIMAL](15,3) = {22};
	                                        DECLARE @gorduravisceral [DECIMAL](15,3) = {23};
	                                        DECLARE @perimcintura [DECIMAL](15,3) = {24};
	                                        DECLARE @perimanca [DECIMAL](15,3) = {25};
	                                        DECLARE @ica [DECIMAL](15,3) = {26};
	                                        DECLARE @ta [VARCHAR](MAX) = {27};
	                                        DECLARE @fcrepouso [DECIMAL](15,3) = {28};
	                                        DECLARE @plano_treino [BIT] = {29};
	                                        DECLARE @notas [varchar](max) = {30};
	                                        DECLARE @data [datetime] = {31};
	                                        DECLARE @ret int;
	                                        DECLARE @res varchar(max);
                                            DECLARE @id_avaliacao int = {32};
                                            DECLARE @id_operador_avaliacao int = {33};
                                            DECLARE @data_proxima_avaliacao datetime = {34};
                                            DECLARE @nomeoperador varchar(500) = {35};

                                            EXEC ALTERA_AVALIACAO @id_operador, @id_operador_avaliacao, @id_avaliacao, @id_socio, @seguiuprotocoloaf, @patologiasantecedentesmedicacao,
                                                @treinaquantotempo,@oquefezcomotreino, @durantequantotempo, @horasdisponiveistreino, @nrvezestreinosemana,
	                                            @aulasoumusculacao, @objetivotreino, @preferenciadesporto, @avaliacaopratica, @idade, @altura, @peso, @percmg,
	                                            @kgmm, @kgosso, @imc, @metabolismobasal, @idademetabolica, @percagua, @gorduravisceral, @perimcintura,
	                                            @perimanca, @ica, @ta, @fcrepouso, @plano_treino, @notas, @data, @data_proxima_avaliacao, @nomeoperador, @ret output, @res output

                                            select @ret as ret, @res as res", id_operador == "" ? "0" : id_operador
                                                                            , id_socio == "" ? "0" : id_socio
                                            , seguiuprotocoloaf == "" || seguiuprotocoloaf == "undefined" ? "0" : seguiuprotocoloaf
                                            , patologiasantecedentesmedicacao == "" ? "''" : "'" + patologiasantecedentesmedicacao + "'"
                                            , treinaquantotempo == "" ? "''" : "'" + treinaquantotempo + "'"
                                            , oquefezcomotreino == "" ? "''" : "'" + oquefezcomotreino + "'"
                                            , durantequantotempo == "" ? "''" : "'" + durantequantotempo + "'"
                                            , horasdisponiveistreino == "" ? "''" : "'" + horasdisponiveistreino + "'"
                                            , nrvezestreinosemana == "" ? "''" : "'" + nrvezestreinosemana + "'"
                                            , aulasoumusculacao == "" ? "''" : "'" + aulasoumusculacao + "'"
                                            , objetivotreino == "" ? "''" : "'" + objetivotreino + "'"
                                            , preferenciadesporto == "" ? "''" : "'" + preferenciadesporto + "'"
                                            , avaliacaopratica == "" ? "''" : "'" + avaliacaopratica + "'"
                                            , idade == "" ? "0" : idade
                                            , altura == "" ? "0" : altura
                                            , peso == "" ? "0" : peso
                                            , percmg == "" ? "0" : percmg
                                            , kgmm == "" ? "0" : kgmm
                                            , kgosso == "" ? "0" : kgosso
                                            , imc == "" ? "0" : imc
                                            , metabolismobasal == "" ? "0" : metabolismobasal
                                            , idademetabolica == "" ? "0" : idademetabolica
                                            , percagua == "" ? "0" : percagua
                                            , gorduravisceral == "" ? "0" : gorduravisceral
                                            , perimcintura == "" ? "0" : perimcintura
                                            , perimanca == "" ? "0" : perimanca
                                            , ica == "" ? "0" : ica
                                            , ta == "" ? "''" : "'" + ta + "'"
                                            , fcrepouso == "" ? "0" : fcrepouso
                                            , planotreino == "" || planotreino == "undefined" ? "0" : planotreino
                                            , notas == "" ? "''" : "'" + notas + "'"
                                            , data == "" ? "''" : "'" + data + "'"
                                            , id_avaliacao == "" ? "0" : id_avaliacao 
                                            , id_operador_avaliacao == "" ? "0" : id_operador_avaliacao
                                            , dataproximaavaliacao == "" ? "NULL" : "'" + dataproximaavaliacao + "'"
                                            , nomeoperador == "" ? "NULL" : "'" + nomeoperador + "'");
            //return sql;
            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"{0}", myReader["res"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"Ocorreu um erro ao atualizar a avaliação");

                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Ocorreu um erro ao atualizar a avaliação: {0}", exc.ToString());

            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string loadInfoAvaliacaoEdit(string id_avaliacao, string id_socio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_socio int = {0};
                                            DECLARE @id_avaliacao int = {1};
                                            DECLARE @data datetime;

                                            SELECT 
                                                ID_SOCIO,
	                                            NOME_SOCIO,
	                                            NR_SOCIO,
	                                            ID_AVALIACAO,
	                                            SEGUIU_PROTOCOLO_AF,
	                                            PATOLOGIAS_ANTECEDENTES_MEDICACAO,
	                                            TREINA_QUANTO_TEMPO,
	                                            O_QUE_FEZ_COMO_TREINO,
	                                            DURANTE_QTO_TEMPO,
	                                            HORAS_DISPONIVEIS_TREINO,
	                                            NR_VEZES_TREINO_SEMANA,
	                                            AULAS_OU_MUSCULACAO,
	                                            OBJETIVO_TREINO,
	                                            PREFERENCIA_DESPORTO,
	                                            AVALIACAO_PRATICA,
	                                            IDADE,
	                                            ALTURA,
	                                            PESO,
	                                            PERC_MG,
	                                            KG_MM,
	                                            KG_OSSO,
	                                            IMC,
	                                            METAB_BASAL,
	                                            IDADE_METAB,
	                                            PERC_H2O,
	                                            GORDURA_VISCERAL,
	                                            PERIM_CINT,
	                                            PERIM_ANCA,
	                                            ICA,
	                                            TA,
	                                            FC_REPOUSO,
	                                            PLANO_TREINO,	
	                                            NOTAS,
	                                            DATA_AVALIACAO,
	                                            OPERADOR_AVALIACAO,
	                                            ID_OPERADOR_AVALIACAO,
	                                            DATA_ULTIMA_ALTERACAO,
	                                            OPERADOR_ULTIMA_ALTERACAO,
	                                            ID_OPERADOR_ULTIMA_ALTERACAO,
                                                DATA_PROXIMA_AVALIACAO,
                                                HORA_PROXIMA_AVALIACAO,
                                                NOME_OPERADOR
                                            FROM REPORT_AVALIACOES(@id_socio, @data, @id_avaliacao)", id_socio, id_avaliacao);
            //return sql;
            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"   <span id='idsocio_avaliacao{39}' class='variaveis'>{0}</span>
                                            <span id='nomesocio_avaliacao{39}' class='variaveis'>{1}</span>
                                            <span id='nrsocio_avaliacao{39}' class='variaveis'>{2}</span>
                                            <span id='idavaliacao_avaliacao{39}' class='variaveis'>{3}</span>
                                            <span id='seguiuprotocoloaf_avaliacao{39}' class='variaveis'>{4}</span>
                                            <span id='patologias_avaliacao{39}' class='variaveis'>{5}</span>
                                            <span id='treinaqtotempo_avaliacao{39}' class='variaveis'>{6}</span>
                                            <span id='oquefezcomotreino_avaliacao{39}' class='variaveis'>{7}</span>
                                            <span id='duranteqtotempo_avaliacao{39}' class='variaveis'>{8}</span>
                                            <span id='horasdisponiveistreino_avaliacao{39}' class='variaveis'>{9}</span>
                                            <span id='nrvezestreinosemana_avaliacao{39}' class='variaveis'>{10}</span>
                                            <span id='aulasoumusculacao_avaliacao{39}' class='variaveis'>{11}</span>
                                            <span id='objetivotreino_avaliacao{39}' class='variaveis'>{12}</span>
                                            <span id='preferenciadesporto_avaliacao{39}' class='variaveis'>{13}</span>
                                            <span id='avaliacaopratica_avaliacao{39}' class='variaveis'>{14}</span>
                                            <span id='idade_avaliacao{39}' class='variaveis'>{15}</span>
                                            <span id='altura_avaliacao{39}' class='variaveis'>{16}</span>
                                            <span id='peso_avaliacao{39}' class='variaveis'>{17}</span>
                                            <span id='percmg_avaliacao{39}' class='variaveis'>{18}</span>
                                            <span id='kmgg_avaliacao{39}' class='variaveis'>{19}</span>
                                            <span id='kgosso_avaliacao{39}' class='variaveis'>{20}</span>
                                            <span id='imc_avaliacao{39}' class='variaveis'>{21}</span>
                                            <span id='metabolismobasal_avaliacao{39}' class='variaveis'>{22}</span>
                                            <span id='idademetabolica_avaliacao{39}' class='variaveis'>{23}</span>
                                            <span id='percagua_avaliacao{39}' class='variaveis'>{24}</span>
                                            <span id='gorduravisceral_avaliacao{39}' class='variaveis'>{25}</span>
                                            <span id='perimetrocintura_avaliacao{39}' class='variaveis'>{26}</span>
                                            <span id='perimetroanca_avaliacao{39}' class='variaveis'>{27}</span>
                                            <span id='ica_avaliacao{39}' class='variaveis'>{28}</span>
                                            <span id='ta_avaliacao{39}' class='variaveis'>{29}</span>
                                            <span id='fcrepouso_avaliacao{39}' class='variaveis'>{30}</span>
                                            <span id='planotreino_avaliacao{39}' class='variaveis'>{31}</span>
                                            <span id='notas_avaliacao{39}' class='variaveis'>{32}</span>
                                            <span id='dataavaliacao_avaliacao{39}' class='variaveis'>{33}</span>
                                            <span id='opavaliacao_avaliacao{39}' class='variaveis'>{34}</span>
                                            <span id='idopavaliacao_avaliacao{39}' class='variaveis'>{35}</span>
                                            <span id='dataultimaalteracao_avaliacao{39}' class='variaveis'>{36}</span>
                                            <span id='opultimaalteracao_avaliacao{39}' class='variaveis'>{37}</span>
                                            <span id='idopultimaalteracao_avaliacao{39}' class='variaveis'>{38}</span>
                                            <span id='dataproximaavaliacao{39}' class='variaveis'>{40}</span>
                                            <span id='horaproximaavaliacao{39}' class='variaveis'>{41}</span>
                                            <span id='nomeopavaliacao{39}' class='variaveis'>{42}</span>",
                                                myReader["ID_SOCIO"].ToString(),
                                                myReader["NOME_SOCIO"].ToString(),
                                                myReader["NR_SOCIO"].ToString(),
                                                myReader["ID_AVALIACAO"].ToString(),
                                                myReader["SEGUIU_PROTOCOLO_AF"].ToString(),
                                                myReader["PATOLOGIAS_ANTECEDENTES_MEDICACAO"].ToString(),
                                                myReader["TREINA_QUANTO_TEMPO"].ToString(),
                                                myReader["O_QUE_FEZ_COMO_TREINO"].ToString(),
                                                myReader["DURANTE_QTO_TEMPO"].ToString(),
                                                myReader["HORAS_DISPONIVEIS_TREINO"].ToString(),
                                                myReader["NR_VEZES_TREINO_SEMANA"].ToString(),
                                                myReader["AULAS_OU_MUSCULACAO"].ToString(),
                                                myReader["OBJETIVO_TREINO"].ToString(),
                                                myReader["PREFERENCIA_DESPORTO"].ToString(),
                                                myReader["AVALIACAO_PRATICA"].ToString(),
                                                myReader["IDADE"].ToString(),
                                                myReader["ALTURA"].ToString(),
                                                myReader["PESO"].ToString(),
                                                myReader["PERC_MG"].ToString(),
                                                myReader["KG_MM"].ToString(),
                                                myReader["KG_OSSO"].ToString(),
                                                myReader["IMC"].ToString(),
                                                myReader["METAB_BASAL"].ToString(),
                                                myReader["IDADE_METAB"].ToString(),
                                                myReader["PERC_H2O"].ToString(),
                                                myReader["GORDURA_VISCERAL"].ToString(),
                                                myReader["PERIM_CINT"].ToString(),
                                                myReader["PERIM_ANCA"].ToString(),
                                                myReader["ICA"].ToString(),
                                                myReader["TA"].ToString(),
                                                myReader["FC_REPOUSO"].ToString(),
                                                myReader["PLANO_TREINO"].ToString(),
                                                myReader["NOTAS"].ToString(),
                                                myReader["DATA_AVALIACAO"].ToString(),
                                                myReader["OPERADOR_AVALIACAO"].ToString(),
                                                myReader["ID_OPERADOR_AVALIACAO"].ToString(),
                                                myReader["DATA_ULTIMA_ALTERACAO"].ToString(),
                                                myReader["OPERADOR_ULTIMA_ALTERACAO"].ToString(),
                                                myReader["ID_OPERADOR_ULTIMA_ALTERACAO"].ToString(),
                                                id_avaliacao,
                                                myReader["DATA_PROXIMA_AVALIACAO"].ToString(),
                                                myReader["HORA_PROXIMA_AVALIACAO"].ToString(),
                                                myReader["NOME_OPERADOR"].ToString());

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
    public static string loadDataLastAvaliacao(string id_socio, string id_operador)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @id_socio int = {1};
                                            DECLARE @data date;
                                            DECLARE @id_avaliacao int;

                                            select top 1 @id_avaliacao = avaliacoes_fisicasid 
                                            from avaliacoes_fisicas
                                            where id_socio = @id_socio
                                            order by DATA desc

                                            SELECT
	                                            SEGUIU_PROTOCOLO_AF,
	                                            PATOLOGIAS_ANTECEDENTES_MEDICACAO,
	                                            TREINA_QUANTO_TEMPO,
	                                            O_QUE_FEZ_COMO_TREINO,
	                                            DURANTE_QTO_TEMPO,
	                                            HORAS_DISPONIVEIS_TREINO,
	                                            NR_VEZES_TREINO_SEMANA,
	                                            AULAS_OU_MUSCULACAO,
	                                            OBJETIVO_TREINO,
	                                            PREFERENCIA_DESPORTO,
	                                            AVALIACAO_PRATICA
                                            FROM REPORT_AVALIACOES(@id_socio, @data, @id_avaliacao)", id_operador, id_socio == "" ? "NULL" : id_socio);
            //return sql;
            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"   <span class='variaveis' id='ultima_af_seguiuprotocoloaf'>{0}</span>
                                            <span class='variaveis' id='ultima_af_patologias'>{1}</span>
                                            <span class='variaveis' id='ultima_af_treinaquantotempo'>{2}</span>
                                            <span class='variaveis' id='ultima_af_quefezcomotreino'>{3}</span>
                                            <span class='variaveis' id='ultima_af_durantequantotempo'>{4}</span>
                                            <span class='variaveis' id='ultima_af_horasdisponiveis'>{5}</span>
                                            <span class='variaveis' id='ultima_af_nrvezes'>{6}</span>
                                            <span class='variaveis' id='ultima_af_aulasmusculacao'>{7}</span>
                                            <span class='variaveis' id='ultima_af_objetivo'>{8}</span>
                                            <span class='variaveis' id='ultima_af_preferenciadesporto'>{9}</span>
                                            <span class='variaveis' id='ultima_af_avaliacaopratica'>{10}</span>",
                                                myReader["SEGUIU_PROTOCOLO_AF"].ToString(),
                                                myReader["PATOLOGIAS_ANTECEDENTES_MEDICACAO"].ToString(),
                                                myReader["TREINA_QUANTO_TEMPO"].ToString(),
                                                myReader["O_QUE_FEZ_COMO_TREINO"].ToString(),
                                                myReader["DURANTE_QTO_TEMPO"].ToString(),
                                                myReader["HORAS_DISPONIVEIS_TREINO"].ToString(),
                                                myReader["NR_VEZES_TREINO_SEMANA"].ToString(),
                                                myReader["AULAS_OU_MUSCULACAO"].ToString(),
                                                myReader["OBJETIVO_TREINO"].ToString(),
                                                myReader["PREFERENCIA_DESPORTO"].ToString(),
                                                myReader["AVALIACAO_PRATICA"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"   <span class='variaveis' id='ultima_af_seguiuprotocoloaf'></span>
                                        <span class='variaveis' id='ultima_af_patologias'></span>
                                        <span class='variaveis' id='ultima_af_treinaquantotempo'></span>
                                        <span class='variaveis' id='ultima_af_quefezcomotreino'></span>
                                        <span class='variaveis' id='ultima_af_durantequantotempo'></span>
                                        <span class='variaveis' id='ultima_af_horasdisponiveis'></span>
                                        <span class='variaveis' id='ultima_af_nrvezes'></span>
                                        <span class='variaveis' id='ultima_af_aulasmusculacao'></span>
                                        <span class='variaveis' id='ultima_af_objetivo'></span>
                                        <span class='variaveis' id='ultima_af_preferenciadesporto'></span>
                                        <span class='variaveis' id='ultima_af_avaliacaopratica'></span>");

                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"   <span class='variaveis' id='ultima_af_seguiuprotocoloaf'></span>
                                        <span class='variaveis' id='ultima_af_patologias'></span>
                                        <span class='variaveis' id='ultima_af_treinaquantotempo'></span>
                                        <span class='variaveis' id='ultima_af_quefezcomotreino'></span>
                                        <span class='variaveis' id='ultima_af_durantequantotempo'></span>
                                        <span class='variaveis' id='ultima_af_horasdisponiveis'></span>
                                        <span class='variaveis' id='ultima_af_nrvezes'></span>
                                        <span class='variaveis' id='ultima_af_aulasmusculacao'></span>
                                        <span class='variaveis' id='ultima_af_objetivo'></span>
                                        <span class='variaveis' id='ultima_af_preferenciadesporto'></span>
                                        <span class='variaveis' id='ultima_af_avaliacaopratica'></span>");

            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string loadListagemMensal(string id_operador)
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
                                            DECLARE @data datetime = DATEADD(hh, -1, getdate());

                                            select
                                                nr_socio, nome, [data], telemovel, email, enviado
                                            from REPORT_AFS_MES(@data)");

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            // Adiciona as linhas
            table.AppendFormat(@"   <table style='width:100%; height: auto; font-family: 'Noto Sans', sans-serif !important;'>
                                        <thead style='background-color:#000; color: #FFF; font-size: large; font-weight: bold;'>
						                    <tr style='height: 50px;'>
                                                <th style='padding: 5px; width: 50%; border-right: 1px red solid;'>Sócio</th>
                                                <th style='padding: 5px; width: 20%; border-right: 1px red solid; border-left: 1px red solid;'>Contactos</th>
                                                <th style='padding: 5px; width: 20%; border-left: 1px red solid;'>Data AF</th>
                                                <th style='padding: 5px; width: 10%; border-left: 1px red solid;'>Enviado</th>
						                    </tr>
						                </thead>
                                        <tbody style='background-color:#FFF; color:#000; font-size: medium; border: 2px solid black; border-spacing: 1px; line-height: 1.2;'>");

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    string[] words = oDs.Tables[0].Rows[i]["nome"].ToString().Split(' ');

                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr style='border: 2px solid black; border-spacing: 1px;' ondblclick='enviarEmail({0});'>
                                            <td style='border: 2px solid black; border-spacing: 1px; padding: 5px;'>
                                                {0} - {1}
                                            </td>
                                            <td style='border: 2px solid black; border-spacing: 1px; padding: 5px;'>
                                                {2}<br />{3}
                                            </td>
                                            <td style='border: 2px solid black; border-spacing: 1px; padding: 5px;'>
                                                {4}
                                            </td>
                                            <td style='{7}'></td>
                                        </tr>",
                                                oDs.Tables[0].Rows[i]["nr_socio"].ToString(),
                                                oDs.Tables[0].Rows[i]["nome"].ToString(),
                                                oDs.Tables[0].Rows[i]["telemovel"].ToString(),
                                                oDs.Tables[0].Rows[i]["email"].ToString(),
                                                oDs.Tables[0].Rows[i]["data"].ToString(),
                                                words[0].ToString(),
                                                words[words.Length - 1].ToString(),
                                                oDs.Tables[0].Rows[i]["enviado"].ToString() == "1" ? " background-color: green; " : "");
                }

                table.AppendFormat(@"</tbody></table>");

                return table.ToString();
            }
            else
            {
                table.AppendFormat(" < div style='height:auto; background-color: #FFF; color: #000;' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem avaliações a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto; background-color: #FFF; color: #000;' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem avaliações a apresentar.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string sendEmailFromTemplate(string nr_socio)
    {
        var body = "";
        string mes = "";
        string email = "";

        try
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            string sql = string.Format(@"   set language portuguese
                                            declare @mes varchar(100) = (select DATENAME(month, dateadd(hh, -1, getdate())))
                                            declare @nr_socio int = {0};

                                            select 
                                                UPPER(LEFT(@mes,1))+LOWER(SUBSTRING(@mes,2,LEN(@mes))) as mes,
                                                email
                                            from socios
                                            where nr_socio = @nr_socio", nr_socio);

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
                    // Adiciona as linhas com dados
                    mes = oDs.Tables[0].Rows[i]["mes"].ToString();
                    email = oDs.Tables[0].Rows[i]["email"].ToString();
                }

                body = "<h4 style='font-weight:bold'>OLÁ!</h4><br/><br />Está na hora de lembrar e renovar <b>OBJETIVOS!</b><br />";
                body += "A sua Avaliação Física está marcada para " + mes + ". Solicitamos confirmação da data e hora disponiveis.<br /><br />";
                body += "<h4 style='font-weight: bold'>Avaliação Física GRATUITA</h4>";
                body += "<ul><li>Composição Corporal;</li>";
                body += "<li>Peso e Altura;</li>";
                body += "<li>Perímetros de cintura e anca;</li>";
                body += "<li>Anamenese médica e patologias;</li>";
                body += "<li>Anamenese desportiva</li></ul>";
                body += "<table border='0'><tbody>";
                body += "<tr><td style='width:50%; text-align: center;'>";
                body += "<img src='https://happybody.site//happybodysoftware//afs//email_img_1.JPG' style='max-width: 100%; height:auto; border-radius: 40%;' /></td>";
                body += "<td style='width:50%; text-align: center;'><img src='https://happybody.site//happybodysoftware//afs//email_img_2.JPG' style='max-width: 100%; height:auto; border-radius: 40%;' /></td></tr></tbody></table>";
                body += "<br /><br /><br />";
                body += "<h4 style='font-weight: bold'>Serviço Especial em Personal Trainer</h4>";
                body += "<ul><li>Programa BURNING FAT (3 meses);</li>";
                body += "<li>Treino para a 3ª Idade;</li>";
                body += "<li>Avaliação Postural;</li>";
                body += "<li>Avaliação do movimento;</li>";
                body += "<li>Treino Postural;</li>";
                body += "<li>Encurtamentos musculares</li></ul>";
                body += "<table border='0'><tbody>";
                body += "<tr><td style='width:50%; text-align: center;'>";
                body += "<img src='https://happybody.site//happybodysoftware//afs//email_img_3.JPG' style='max-width: 100%; height:auto; border-radius: 40%;' /></td>";
                body += "<td style='width:50%; text-align: center;'><img src='https://happybody.site//happybodysoftware//afs//email_img_4.JPG' style='max-width: 100%; height:auto; border-radius: 40%;' /></td></tr></tbody></table>";
                body += "<br /><br /><br />";
                body += "Saudações Desportivas";
            }

        }
        catch (Exception exc)
        {

        }

        try
        {
            MailMessage mailMessage = new MailMessage();

            string newsletterText = string.Empty;
            newsletterText = File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "\\happybodysoftware//templates//aniversario.html");

            //sendEmailFromTemplate("Avaliação Física " + mes, "Avaliação Física " + mes + " GRATUITA - HAPPY BODY", "happybodyfitcoach@gmail.com", "", "afonsopereira6@gmail.com", body);

            // ------------------------------------
            // Processa o template 
            // ------------------------------------
            newsletterText = newsletterText.Replace("[EMAIL_INTRO]", "Avaliação Física " + mes);
            newsletterText = newsletterText.Replace("[EMAIL_TEXTBODY]", body);
            //newsletterText = newsletterText.Replace("[EMAIL_INTROIMAGE]", "  <img style='width:280px;height:100px' src='http://teu site publico/" + lic_num + @"/logocustomer.png'  alt='Logo'  data-default='placeholder' /> ");
            //newsletterText = newsletterText.Replace("[EMAIL_RODAPEIMAGE]", "  <img style='width:200px;height:50px' src='http:// teu site publico /" + lic_num + @"/logocustomer.png'    alt='Logo'  data-default='placeholder' /> ");
            //newsletterText = newsletterText.Replace("[EMAIL_LICNAME]", lic_name);
            //newsletterText = newsletterText.Replace("[EMAIL_LICEMAIL]", lic_email);


            // ------------------------------------
            string _from = "happybodyfitcoach@gmail.com";
            string _emailpwd = "happysr@1";
            string _smtp = "smtp.gmail.com";
            string _smtpport = "465";

            mailMessage.From = new MailAddress(_from, "Happy Body Fit Coach");

            mailMessage.To.Add(email);

            //if (sendcc.Trim() != "")
            //    mailMessage.CC.Add(sendcc);
            mailMessage.Bcc.Add("happybodyfitcoach@gmail.com");

            mailMessage.Subject = "Avaliação Física " + mes + " GRATUITA - HAPPY BODY";
            mailMessage.Body = newsletterText;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;

            string html = "html";

            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(_smtp);
            System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential(_from, _emailpwd);

            //smtpClient.Port = Convert.ToInt32(_smtpport);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = mailAuthentication;
            smtpClient.Timeout = 50000;
            smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            smtpClient.Send(mailMessage);
            smtpClient.Dispose();
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }

        try
        {
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(cs);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;

                                            DECLARE @nr_socio int = {0};
                                            DECLARE @tipo varchar(500) = '{1}';
                                            DECLARE @dataatual datetime = dateadd(hh, -1, getdate());

                                            UPDATE list
                                            SET enviado = 1
                                            FROM SOCIOS soc
                                            INNER JOIN LISTAGEM_EMAILS_ENVIADOS list on list.id_socio = soc.sociosid
                                            WHERE soc.NR_SOCIO = @nr_socio
                                            and list.tipo = @tipo
                                            and list.mes = MONTH(@dataatual)
                                            and list.ano = YEAR(@dataatual)", nr_socio, "AVALIACOES-MENSAL");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            connection.Close();
        }
        catch (Exception exc)
        {

        }

        return "Email de avaliação física enviado com sucesso!";
    }

    [WebMethod]
    public static string eliminaAvaliacao(string id_operador, string id_avaliacao)
    {
        var table = new StringBuilder();
        string cs = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   set dateformat dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @id_avaliacao int = {1}
                                            DECLARE @ret int;

                                            EXEC DELETE_AVALIACAO @id_avaliacao, @id_operador, @ret output

                                            select @ret as ret", id_operador, id_avaliacao);
            //return sql;
            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

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
                table.AppendFormat(@"-1");

                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"-1");

            connection.Close();
            return table.ToString();
        }
    }
}
