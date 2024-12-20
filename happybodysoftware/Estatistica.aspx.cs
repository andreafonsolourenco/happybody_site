using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Estatistica : Page
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

        //loadFiltrosEntradas();
        loadDataFiltroContratos();
        loadDDL();
    }

    private void loadDDL()
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
                                            DECLARE @id_op int = {0};
                                            DECLARE @menu varchar(500) = 'Estatísticas';
                                            DECLARE @admin bit = (select administrador from operadores where operadoresid = @id_op)
                                            
                                            IF(@admin = 0)
                                            begin
                                            SELECT 
                                                VALOR_DDL, TIPO
                                            FROM [REPORT_OUTRAS_PERMISSOES](@id_op, @menu, null, null)
                                            WHERE LEITURA = 1
                                            order by valor_ddl
                                            end
                                            else
                                            begin
                                                select distinct tipo, valor_ddl
                                                from outras_permissoes perm
                                                inner join menus men on men.menusid = perm.id_menu
                                                where men.titulo = @menu
                                                order by valor_ddl
                                            end", id);

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                table.AppendFormat(@"<select class='form-control' id='statsType' style='width:80%; height: 40px; font-size: small; float: left;' onchange='loadStats();'>");

                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    table.AppendFormat(@"<option value='{0}'>{1}</option>", oDs.Tables[0].Rows[i]["VALOR_DDL"].ToString(),
                                                                            oDs.Tables[0].Rows[i]["TIPO"].ToString());
                }

                table.AppendFormat(@"</select>");
            }
            else
            {
                connection.Close();
                table.AppendFormat(@"<select class='form-control' id='statsType' style='width:80%; height: 40px; font-size: small; float: left;' onchange='loadStats();'>");
                table.AppendFormat(@"<option value='-1'>Não existem estatísticas a apresentar.</option>");
                table.AppendFormat(@"</select>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat(@"<select class='form-control' id='statsType' style='width:80%; height: 40px; font-size: small; float: left;' onchange='loadStats();'>");
            table.AppendFormat(@"<option value='-1'>Não existem estatísticas a apresentar.</option>");
            table.AppendFormat(@"</select>");

            divTable.InnerHtml = exc.ToString();
        }

        divSelectStatsType.InnerHtml = table.ToString();
    }

    [WebMethod]
    public static string loadStats(string flag)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = "";

            if (flag == "0")
            {
                sql = string.Format(@"   SET DATEFORMAT dmy;
                                         DECLARE @id_tipo_contrato int;
                                        
                                         SELECT 
                                             TIPO,
                                             TOTAL_CONTRATOS,
                                             NR_ATIVOS,
                                             VALOR_PERCENTAGEM_ATIVOS,
                                             NR_DESATIVOS,
                                             VALOR_PERCENTAGEM_DESATIVOS,
                                             ID_TIPO
                                         FROM REPORT_ESTATISTICA_TIPOS_CONTRATO(@id_tipo_contrato)
                                         ORDER BY TOTAL_CONTRATOS DESC");
            }
            else if (flag == "1")
            {
                sql = string.Format(@"  SET DATEFORMAT dmy;
                                        DECLARE @id_estado int;

                                        SELECT
                                            STATUS,
                                            NR_CONTRATOS,
                                            VALOR_PERCENTAGEM,
                                            ID_ESTADO,
                                            CONG, CONG_NR
                                        FROM REPORT_ESTATISTICA_ESTADO_PAGAMENTO(@id_estado)
                                        order by VALOR_PERCENTAGEM DESC");
            }
            else if (flag == "7")
            {
                sql = string.Format(@"  SET DATEFORMAT dmy;
                                        DECLARE @id_estado int;

                                        SELECT
                                            STATUS,
                                            NR_CONTRATOS,
                                            VALOR_PERCENTAGEM,
                                            ID_ESTADO
                                        FROM REPORT_ESTATISTICA_ESTADO_PAGAMENTO_MES_SEGUINTE(@id_estado)
                                        order by VALOR_PERCENTAGEM DESC");
            }
            else if (flag == "2")
            {
                sql = string.Format(@"   SET DATEFORMAT dmy;
                                        
                                         SELECT 
                                             PROFISSAO,
                                             TOTAL_CONTRATOS,
                                             NR_ATIVOS,
                                             VALOR_PERCENTAGEM_ATIVOS,
                                             NR_DESATIVOS,
                                             VALOR_PERCENTAGEM_DESATIVOS,
                                            RANK_VALUE
                                         FROM REPORT_ESTATISTICA_PROFISSOES()
                                         ORDER BY PROFISSAO ASC, TOTAL_CONTRATOS DESC, NR_ATIVOS DESC, NR_DESATIVOS DESC");
            }
            else if (flag == "3")
            {
                sql = string.Format(@"   SET DATEFORMAT dmy;
                                        
                                         SELECT 
                                             TITULO,
                                             ORDEM,
                                             TOTAL_CONTRATOS,
                                             NR_ATIVOS,
                                             VALOR_PERCENTAGEM_ATIVOS,
                                             NR_DESATIVOS,
                                             VALOR_PERCENTAGEM_DESATIVOS,
                                             MARGEM_SUPERIOR,
                                             MARGEM_INFERIOR
                                         FROM REPORT_ESTATISTICA_IDADES()
                                         ORDER BY ORDEM ASC");
            }
            else if (flag == "6")
            {
                sql = string.Format(@"  select *
                                        from (
                                            select 
                                                'ATIVOS' as TAG,
                                                NR_CONTRATOS,
                                                STATUS,
                                                VALOR_PERCENTAGEM,
                                                ID_ESTADO,
                                                valor,
                                                pagante,
                                                ATIVO,
                                                0 as ESTADO_PAGAMENTO
                                            from [REPORT_ESTATISTICA_PAGANTES_NAOPAGANTES](null)
                                            where ativo = 1
                                            UNION
                                            select 
                                                'ATIVOS PAGANTES' as TAG,
                                                NR_CONTRATOS,
                                                STATUS,
                                                VALOR_PERCENTAGEM,
                                                ID_ESTADO,
                                                valor,
                                                pagante,
                                                ATIVO,
                                                0 as ESTADO_PAGAMENTO
                                            from [REPORT_ESTATISTICA_PAGANTES_NAOPAGANTES](null)
                                            where ativo = 1
                                            union
                                            select
                                                TAG, SUM(NR_CONTRATOS) as NR_CONTRATOS, STATUS, SUM(VALOR_PERCENTAGEM) as VALOR_PERCENTAGEM,
                                                ID_ESTADO, SUM(VALOR) as VALOR, PAGANTE, ATIVO, ESTADO_PAGAMENTO
                                            from (
                                                select 
                                                    'ATIVOS NÃO PAGANTES' as TAG,
                                                    NR_CONTRATOS,
                                                    STATUS,
                                                    VALOR_PERCENTAGEM,
                                                    ID_ESTADO,
                                                    valor,
                                                    pagante,
                                                    ATIVO,
                                                    0 as ESTADO_PAGAMENTO
                                                from [REPORT_ESTATISTICA_PAGANTES_NAOPAGANTES](null)
					                            where ativo = 0
					                            union
					                            select distinct
		                                            'ATIVOS NÃO PAGANTES' as TAG,
		                                            count(distinct cont.contratoid) as NR_CONTRATOS,
		                                            case when rpt.id_status_pagamento = 0 then 'Sem Estado de Pagamento'
							                        else 'Não Pago' end as STATUS,
		                                            0 as VALOR_PERCENTAGEM,
		                                            0 as ID_ESTADO,
		                                            case when pag.valor is null or pag.valor = 0 then cont.valor else pag.valor end as valor,
		                                            0 as pagante,
		                                            1 as ATIVO,
                                                    rpt.id_status_pagamento as ESTADO_PAGAMENTO
	                                            from report_contratos_dentro_data(null, null) rpt
	                                            inner join socios soc on soc.sociosid = rpt.id_socio
	                                            left join estados_contrato stpag on stpag.estados_contratoid = rpt.id_estado_contrato_status_pagamento
	                                            inner join estados_contrato st on st.estados_contratoid = rpt.id_estado
	                                            inner join pagamentos pag on pag.pagamentosid = rpt.id_pagamento_mes_corrente
	                                            inner join contrato cont on cont.contratoid = rpt.id_contrato
	                                            left join pagamentos_status pagst on pagst.pagamentos_statusid = rpt.id_status_pagamento
	                                            where isnull(stpag.codigo, st.codigo) not in ('NAO_RENOV', 'INACTIVO', 'CANC_PAGO', 'CANC_FALTA_PAG')
	                                            and (rpt.id_status_pagamento = 0 or pagst.codigo = 'NAO-PAGO')
	                                            group by pag.valor, cont.valor, rpt.id_status_pagamento
                                            ) as tmp_inativos
                                            group by TAG, STATUS, ID_ESTADO, PAGANTE, ATIVO, ESTADO_PAGAMENTO
                                        ) as tmp
                                        ORDER BY TAG, NR_CONTRATOS desc");
            }
            else if (flag == "8")
            {
                sql = string.Format(@"   SET DATEFORMAT dmy;
                                         declare @pagante bit;
                                        
                                         SELECT 
                                             TITULO,
                                             PAGANTE,
                                             NR_CONTRATOS,
                                             VALOR_PERCENTAGEM,
                                             VALOR
                                         FROM REPORT_ESTATISTICA_PAGANTES_NAOPAGANTES_MES_SEGUINTE(@pagante)
                                         ORDER BY PAGANTE DESC");
            }
            else if (flag == "9")
            {
                sql = string.Format(@"   SET DATEFORMAT dmy;
                                         DECLARE @sexo varchar(1);
                                        
                                         SELECT 
                                             SEXO,
                                             RANK_VALUE,
                                             total_contratos,
                                             nr_ativos,
                                             nr_desativos,
                                             VALOR_PERCENTAGEM_ATIVOS,
                                             VALOR_PERCENTAGEM_DESATIVOS
                                         FROM REPORT_ESTATISTICA_SEXO(@sexo)
                                         ORDER BY nr_ativos DESC, nr_desativos desc");
            }

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                if (flag == "0")
                {
                    // Adiciona as linhas
                    table.AppendFormat(@"   <table id='tableGrid' style='border: none !important;'>
                                            <thead>
						                        <tr>
                                                    <th class='headerLeft' rowspan='2'>Tipo de Contrato</th>
                                                    <th class='headerRight' colspan='2'>Nº de Contratos</th>
						                        </tr>
                                                <tr>
                                                    <th style='width: 15%; text-align: center'>Ativos</th>
                                                    <th style='width: 15%; text-align: center; -webkit-border-bottom-right-radius: 4px !important; border-bottom-right-radius: 4px !important;'>Inativos</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                    for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                    {
                        // Adiciona as linhas com dados
                        table.AppendFormat(@"<tr ondblclick='openPopupTipos({6});'>
                                            <td>{0}<br /><span style='font-size: small;'>{1} Contratos</span></td>
                                            <td style='width:15%; text-align: left'>{3}%<br /><span style='font-size: small;'>{2} Contratos</span></td>
                                            <td style='width:15%; text-align: right; color: red;'>{5}%<br /><span style='font-size: small;'>{4} Contratos</span></td>
                                        </tr>",
                                                    oDs.Tables[0].Rows[i]["TIPO"].ToString(),
                                                    oDs.Tables[0].Rows[i]["TOTAL_CONTRATOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["NR_ATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM_ATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["NR_DESATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM_DESATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["ID_TIPO"].ToString());
                    }

                    table.AppendFormat("</tbody></table>");
                }
                else if (flag == "1")
                {
                    // Adiciona as linhas
                    table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr style='border: none !important;'>
                                                    <th class='headerLeftOneLine'>Estado do Contrato</th>
                                                    <th class='headerRightOneLine'>Nº de Contratos</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                    string color = "";

                    for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                    {
                        switch (oDs.Tables[0].Rows[i]["STATUS"].ToString())
                        {
                            case "Activo":
                            case "Congelado":
                            case "Suspenso":
                            case "Oferta":
                                color = " style='background-color: #C0C0C0' ";
                                break;
                            default:
                                color = "";
                                break;
                        }

                        // Adiciona as linhas com dados
                        table.AppendFormat(@"<tr ondblclick='openPopupEstados({3});' {4}>
                                            <td style='width:15%; text-align: left'>{0}{5}</td>
                                            <td style='width:15%; text-align: center'>{2}%<br /><span style='font-size: small;'>{1} Contratos{6}</span></td>
                                        </tr>",
                                                    oDs.Tables[0].Rows[i]["STATUS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString(),
                                                    Convert.ToDouble(oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM"].ToString()).ToString("0.0"),
                                                    oDs.Tables[0].Rows[i]["ID_ESTADO"].ToString(),
                                                    color,
                                                    oDs.Tables[0].Rows[i]["STATUS"].ToString() == "Activo" ? " (Sócios a entrar no ginásio)" : "",
                                                    oDs.Tables[0].Rows[i]["STATUS"].ToString() == "Congelado" ? "<br /><span style='font-weight: bold'>" + oDs.Tables[0].Rows[i]["CONG"].ToString() + " Ativos</span>" : "");
                    }

                    table.AppendFormat("</tbody></table>");
                }
                else if (flag == "7")
                {
                    // Adiciona as linhas
                    table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr style='border: none !important;'>
                                                    <th class='headerLeftOneLine'>Estado do Contrato</th>
                                                    <th class='headerRightOneLine'>Nº de Contratos</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                    for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                    {
                        // Adiciona as linhas com dados
                        table.AppendFormat(@"<tr ondblclick='openPopupEstadosMesSeguinte({3});'>
                                            <td style='width:15%; text-align: left'>{0}</td>
                                            <td style='width:15%; text-align: center'>{2}%<br /><span style='font-size: small;'>{1} Contratos</span></td>
                                        </tr>",
                                                    oDs.Tables[0].Rows[i]["STATUS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM"].ToString(),
                                                    oDs.Tables[0].Rows[i]["ID_ESTADO"].ToString());
                    }

                    table.AppendFormat("</tbody></table>");
                }
                else if (flag == "2")
                {
                    // Adiciona as linhas
                    table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerLeft' rowspan='2'>Profissões</th>
                                                    <th class='headerRight' colspan='2'>Nº de Contratos</th>
						                        </tr>
                                                <tr>
                                                    <th style='width: 15%; text-align: center'>Ativos</th>
                                                    <th style='width: 15%; text-align: center; -webkit-border-bottom-right-radius: 4px !important; border-bottom-right-radius: 4px !important;''>Inativos</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                    for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                    {
                        // Adiciona as linhas com dados
                        table.AppendFormat(@"<tr ondblclick='openPopupProfissoes({7});'>
                                            <td>{0}<br /><span style='font-size: small;'>{1} Contratos</span></td>
                                            <td style='width:15%; text-align: left'>{3}%<br /><span style='font-size: small;'>{2} Contratos</span></td>
                                            <td style='width:15%; text-align: right; color: red;'>{5}%<br /><span style='font-size: small;'>{4} Contratos</span></td>
                                        </tr>",
                                                    oDs.Tables[0].Rows[i]["PROFISSAO"].ToString(),
                                                    oDs.Tables[0].Rows[i]["TOTAL_CONTRATOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["NR_ATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM_ATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["NR_DESATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM_DESATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["PROFISSAO"].ToString(),
                                                    oDs.Tables[0].Rows[i]["RANK_VALUE"].ToString());
                    }

                    table.AppendFormat("</tbody></table>");
                }
                else if (flag == "3")
                {
                    // Adiciona as linhas
                    table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerLeft' rowspan='2'>Margem de Idades</th>
                                                    <th class='headerRight' colspan='2'>Nº de Contratos</th>
						                        </tr>
                                                <tr>
                                                    <th style='width: 15%; text-align: center'>Ativos</th>
                                                    <th style='width: 15%; text-align: center; -webkit-border-bottom-right-radius: 4px !important; border-bottom-right-radius: 4px !important;'>Inativos</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                    for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                    {
                        // Adiciona as linhas com dados
                        table.AppendFormat(@"<tr ondblclick='openPopupIdades({7}, {6});'>
                                            <td>{0}<br /><span style='font-size: small;'>{1} Contratos</span></td>
                                            <td style='width:15%; text-align: left'>{3}%<br /><span style='font-size: small;'>{2} Contratos</span></td>
                                            <td style='width:15%; text-align: right; color: red;'>{5}%<br /><span style='font-size: small;'>{4} Contratos</span></td>
                                        </tr>",
                                                    oDs.Tables[0].Rows[i]["TITULO"].ToString(),
                                                    oDs.Tables[0].Rows[i]["TOTAL_CONTRATOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["NR_ATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM_ATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["NR_DESATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM_DESATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["MARGEM_SUPERIOR"].ToString(),
                                                    oDs.Tables[0].Rows[i]["MARGEM_INFERIOR"].ToString());
                    }

                    table.AppendFormat("</tbody></table>");
                }
                if (flag == "6")
                {
                    // Adiciona as linhas
                    table.AppendFormat(@"   <table id='tableGrid' style='border: none !important;'>
                                            <thead>
						                        <tr>
                                                    <th colspan='3' style='text-align: center; -webkit-border-radius: 4px !important; border-radius: 4px !important;'>Pagantes e Não Pagantes</th>
						                        </tr>
						                    </thead>
                                            <tbody>");
                    int contratosAtivos = 0;
                    Double valorAtivos = 0;
                    Double percentagemAtivos = 0;
                    int contratosAtivosPagantes = 0;
                    Double valorAtivosPagantes = 0;
                    Double percentagemAtivosPagantes = 0;
                    int contratosAtivosNaoPagantes = 0;
                    Double valorAtivosNaoPagantes = 0;
                    Double percentagemAtivosNaoPagantes = 0;
                    var tableAtivos = new StringBuilder();
                    var tableAtivosPagantes = new StringBuilder();
                    var tableAtivosNaoPagantes = new StringBuilder();

                    // Adiciona as linhas
                    tableAtivos.AppendFormat(@" <tr>
                                                    <td colspan='3' style='text-align: center; background-color: #000; color: #FFF;'>
                                                        Ativos
                                                    </td>
                                                </tr>");
                    tableAtivosPagantes.AppendFormat(@" <tr>
                                                            <td colspan='3' style='text-align: center; background-color: #000; color: #FFF;'>
                                                                Ativos Pagantes
                                                            </td>
                                                        </tr>");
                    tableAtivosNaoPagantes.AppendFormat(@" <tr>
                                                                <td colspan='3' style='text-align: center; background-color: #000; color: #FFF;'>
                                                                    Ativos Não Pagantes
                                                                </td>
                                                           </tr>");

                    int contaAtivos = 0;
                    int contaAtivosPagantes = 0;
                    int contaAtivosNaoPagantes = 0;

                    for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                    {
                        if (oDs.Tables[0].Rows[i]["TAG"].ToString() == "ATIVOS")
                        {
                            contaAtivos++;
                        }

                        if (oDs.Tables[0].Rows[i]["TAG"].ToString() == "ATIVOS PAGANTES")
                        {
                            contaAtivosPagantes++;
                        }

                        if (oDs.Tables[0].Rows[i]["TAG"].ToString() == "ATIVOS NÃO PAGANTES")
                        {
                            contaAtivosNaoPagantes++;
                        }
                    }

                    for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                    {
                        if (oDs.Tables[0].Rows[i]["TAG"].ToString() == "ATIVOS")
                        {
                            contratosAtivos += Convert.ToInt32(oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString());
                            percentagemAtivos += Convert.ToDouble(oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM"].ToString());
                            valorAtivos += Convert.ToDouble(oDs.Tables[0].Rows[i]["VALOR"].ToString());
                        }

                        if (oDs.Tables[0].Rows[i]["TAG"].ToString() == "ATIVOS PAGANTES" && oDs.Tables[0].Rows[i]["STATUS"].ToString() != "Activo")
                        {
                            contratosAtivosPagantes += Convert.ToInt32(oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString());
                            percentagemAtivosPagantes += Convert.ToDouble(oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM"].ToString());
                            valorAtivosPagantes += Convert.ToDouble(oDs.Tables[0].Rows[i]["VALOR"].ToString());
                        }

                        if (oDs.Tables[0].Rows[i]["TAG"].ToString() == "ATIVOS NÃO PAGANTES" && oDs.Tables[0].Rows[i]["ATIVO"].ToString() == "1")
                        {
                            contratosAtivosNaoPagantes += Convert.ToInt32(oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString());
                            //percentagemAtivosNaoPagantes += Convert.ToDouble(oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM"].ToString());
                            valorAtivosNaoPagantes += Convert.ToDouble(oDs.Tables[0].Rows[i]["VALOR"].ToString());
                        }
                    }

                    percentagemAtivosNaoPagantes = Convert.ToDouble(Convert.ToDouble(contratosAtivosNaoPagantes) / Convert.ToDouble(contratosAtivos));

                    int nrAtivos = 0;
                    int nrAtivosPagantes = 0;
                    int nrAtivosNaoPagantes = 0;

                    var totalAtivosString = string.Format(@"<span style='font-weight:bold'>TOTAL ATIVOS<br />{0}<br />{1} Euros<br />{2} %</span>", 
                        contratosAtivos.ToString(), 
                        valorAtivos.ToString().Replace(".", ","),
                        percentagemAtivos.ToString().Replace(".", ","));

                    var totalAtivosPagantesString = string.Format(@"<span style='font-weight:bold'>TOTAL ATIVOS PAGANTES<br />{0}<br />{1} Euros<br />{2} %</span>", 
                        (contratosAtivos - contratosAtivosPagantes).ToString(),
                        (valorAtivos - valorAtivosPagantes).ToString().Replace(".", ","),
                        (percentagemAtivos - percentagemAtivosPagantes).ToString().Replace(".", ","));

                    var totalAtivosNaoPagantesString = string.Format(@"<span style='font-weight:bold'>TOTAL ATIVOS NÃO PAGANTES<br />{0}<br />{1} Euros<br />{2} %</span>",
                        contratosAtivosNaoPagantes.ToString(),
                        valorAtivosNaoPagantes.ToString().Replace(".", ","),
                        percentagemAtivosNaoPagantes.ToString("0.00").Replace(".", ","));

                    for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                    {
                        if (oDs.Tables[0].Rows[i]["TAG"].ToString() == "ATIVOS")
                        {
                            if(nrAtivos==0)
                            {
                                tableAtivos.AppendFormat(@"<tr ondblclick='openPopupPagantesNaoPagantes(0, {3}, 0);'>
                                                        <td style='width:60%'>{0}</td>
                                                        <td style='width:20%;'>
                                                            {1}<br /><span style='font-size: small;'>{2}%</span>
                                                        </td>
                                                        <td rowspan='{4}' style='width:20%; text-align: center;'>{5}</td>
                                                    </tr>",
                                                        oDs.Tables[0].Rows[i]["STATUS"].ToString(),
                                                        oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString(),
                                                        Convert.ToDouble(oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM"].ToString()).ToString("0.0"),
                                                        oDs.Tables[0].Rows[i]["ID_ESTADO"].ToString(),
                                                        contaAtivos.ToString(),
                                                        totalAtivosString.ToString());

                                nrAtivos++;
                            }
                            else
                            {
                                tableAtivos.AppendFormat(@"<tr ondblclick='openPopupPagantesNaoPagantes(0, {3}, 0);'>
                                                        <td style='width:60%'>{0}</td>
                                                        <td style='width:20%;'>
                                                            {1}<br /><span style='font-size: small;'>{2}%</span>
                                                        </td>
                                                    </tr>",
                                                        oDs.Tables[0].Rows[i]["STATUS"].ToString(),
                                                        oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString(),
                                                        Convert.ToDouble(oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM"].ToString()).ToString("0.0"),
                                                        oDs.Tables[0].Rows[i]["ID_ESTADO"].ToString(),
                                                        contaAtivos.ToString());

                                nrAtivos++;
                            }
                        }

                        if (oDs.Tables[0].Rows[i]["TAG"].ToString() == "ATIVOS PAGANTES")
                        {
                            if (nrAtivosPagantes == 0)
                            {
                                if (oDs.Tables[0].Rows[i]["STATUS"].ToString() == "Activo")
                                {
                                    tableAtivosPagantes.AppendFormat(@"<tr ondblclick='openPopupPagantesNaoPagantes(1, {4}, 0);'>
                                                        <td style='width:60%'>{0}</td>
                                                        <td style='width:20%;'>
                                                            {1}<br />{3} Euros<br /><span style='font-size: small;'>{2}%</span></span>
                                                        </td>
                                                        <td rowspan='{5}' style='width:20%; text-align: center;'>{6}</td>
                                                    </tr>",
                                                        oDs.Tables[0].Rows[i]["STATUS"].ToString(),
                                                                contratosAtivos.ToString(),
                                                                percentagemAtivos.ToString("0.0"),
                                                                valorAtivos.ToString(),
                                                                oDs.Tables[0].Rows[i]["ID_ESTADO"].ToString(),
                                                                contaAtivosPagantes.ToString(),
                                                                totalAtivosPagantesString.ToString());
                                }
                                else
                                {
                                    tableAtivosPagantes.AppendFormat(@"<tr ondblclick='openPopupPagantesNaoPagantes(1, {4}, 0);'>
                                                        <td style='width:60%'>{0}</td>
                                                        <td style='width:20%;'>
                                                            {1}<br />{3} Euros<br /><span style='font-size: small;'>{2}%</span></span>
                                                        </td>
                                                        <td rowspan='{5}' style='width:20%; text-align: center;'>{6}</td>
                                                    </tr>",
                                                        oDs.Tables[0].Rows[i]["STATUS"].ToString(),
                                                                oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString(),
                                                                Convert.ToDouble(oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM"].ToString()).ToString("0.0"),
                                                                oDs.Tables[0].Rows[i]["VALOR"].ToString(),
                                                                oDs.Tables[0].Rows[i]["ID_ESTADO"].ToString(),
                                                                contaAtivosPagantes.ToString(),
                                                                totalAtivosPagantesString.ToString());
                                }

                                nrAtivosPagantes++;
                            }
                            else
                            {
                                if (oDs.Tables[0].Rows[i]["STATUS"].ToString() == "Activo")
                                {
                                    tableAtivosPagantes.AppendFormat(@"<tr ondblclick='openPopupPagantesNaoPagantes(1, {4}, 0);'>
                                                        <td style='width:60%'>{0}</td>
                                                        <td style='width:20%;'>
                                                            {1}<br />{3} Euros<br /><span style='font-size: small;'>{2}%</span></span>
                                                        </td>
                                                    </tr>",
                                                        oDs.Tables[0].Rows[i]["STATUS"].ToString(),
                                                                contratosAtivos.ToString(),
                                                                percentagemAtivos.ToString("0.0"),
                                                                valorAtivos.ToString(),
                                                                oDs.Tables[0].Rows[i]["ID_ESTADO"].ToString());
                                }
                                else
                                {
                                    tableAtivosPagantes.AppendFormat(@"<tr ondblclick='openPopupPagantesNaoPagantes(1, {4}, 0);'>
                                                        <td style='width:60%'>{0}</td>
                                                        <td style='width:20%;'>
                                                            {1}<br />{3} Euros<br /><span style='font-size: small;'>{2}%</span></span>
                                                        </td>
                                                    </tr>",
                                                        oDs.Tables[0].Rows[i]["STATUS"].ToString(),
                                                                oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString(),
                                                                Convert.ToDouble(oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM"].ToString()).ToString("0.0"),
                                                                oDs.Tables[0].Rows[i]["VALOR"].ToString(),
                                                                oDs.Tables[0].Rows[i]["ID_ESTADO"].ToString());
                                }

                                nrAtivosPagantes++;
                            }
                        }

                        if (oDs.Tables[0].Rows[i]["TAG"].ToString() == "ATIVOS NÃO PAGANTES")
                        {
                            Double valorPercentagem = 0.00;

                            if (oDs.Tables[0].Rows[i]["STATUS"].ToString() != "Inactivo")
                            {
                                valorPercentagem = Convert.ToDouble(oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString()) / contratosAtivos;
                            }

                            if (nrAtivosNaoPagantes == 0)
                            {
                                tableAtivosNaoPagantes.AppendFormat(@"<tr ondblclick='openPopupPagantesNaoPagantes(2, {4}, {5});'>
                                                        <td style='width:60%'>{0}</td>
                                                        <td style='width:20%;'>
                                                            {1}<br />{3} Euros<br /><span style='font-size: small;'>{2}%</span></span>
                                                        </td>
                                                        <td rowspan='{6}' style='width:20%; text-align: center;'>{7}</td>
                                                    </tr>",
                                                        oDs.Tables[0].Rows[i]["STATUS"].ToString(),
                                                            oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString(),
                                                            valorPercentagem.ToString("0.00"),
                                                            oDs.Tables[0].Rows[i]["VALOR"].ToString(),
                                                            oDs.Tables[0].Rows[i]["ID_ESTADO"].ToString(),
                                                            oDs.Tables[0].Rows[i]["ESTADO_PAGAMENTO"].ToString(),
                                                            contaAtivosNaoPagantes.ToString(),
                                                            totalAtivosNaoPagantesString.ToString());

                                nrAtivosNaoPagantes++;
                            }
                            else
                            {
                                tableAtivosNaoPagantes.AppendFormat(@"<tr ondblclick='openPopupPagantesNaoPagantes(2, {4}, {5});'>
                                                        <td style='width:60%'>{0}</td>
                                                        <td style='width:20%;'>
                                                            {1}<br />{3} Euros<br /><span style='font-size: small;'>{2}%</span></span>
                                                        </td>
                                                    </tr>",
                                                        oDs.Tables[0].Rows[i]["STATUS"].ToString(),
                                                            oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString(),
                                                            valorPercentagem.ToString("0.00"),
                                                            oDs.Tables[0].Rows[i]["VALOR"].ToString(),
                                                            oDs.Tables[0].Rows[i]["ID_ESTADO"].ToString(),
                                                            oDs.Tables[0].Rows[i]["ESTADO_PAGAMENTO"].ToString(),
                                                            contaAtivosNaoPagantes.ToString());

                                nrAtivosNaoPagantes++;
                            }
                        }
                    }

                    //tableAtivos.AppendFormat(@"</table>");
                    //tableAtivosPagantes.AppendFormat(@"</table>");
                    //tableAtivosNaoPagantes.AppendFormat(@"</table>");

                    // Adiciona as linhas com dados
                    table.AppendFormat(@"{0}{1}{2}", tableAtivos.ToString(), tableAtivosPagantes.ToString(), tableAtivosNaoPagantes.ToString());
                    //table.AppendFormat(@"{0}<tr ondblclick=''>
                    //                        <td style='width:70%'>{0}</td>
                    //                        <td style='width:30%; text-align: center;'>
                    //                            <span style='font-weight:bold'>
                    //                                TOTAL ATIVOS<br />
                    //                                {1}<br />
                    //                                {2} €<br />
                    //                                {3} %
                    //                            </span>
                    //                        </td>
                    //                    </tr>",
                    //                            tableAtivos.ToString(),
                    //                            contratosAtivos.ToString(),
                    //                            valorAtivos.ToString().Replace(".", ","),
                    //                            percentagemAtivos.ToString().Replace(".", ","));

                    //table.AppendFormat(@"<tr ondblclick=''>
                    //                        <td style='width:70%'>{0}</td>
                    //                        <td style='width:30%; text-align: center;'>
                    //                            <span style='font-weight:bold'>
                    //                                TOTAL ATIVOS PAGANTES<br />
                    //                                {1}<br />
                    //                                {2} €<br />
                    //                                {3} %
                    //                            </span>
                    //                        </td>
                    //                    </tr>",
                    //                            tableAtivosPagantes.ToString(),
                    //                            (contratosAtivos - contratosAtivosPagantes).ToString(),
                    //                            (valorAtivos - valorAtivosPagantes).ToString().Replace(".", ","),
                    //                            (percentagemAtivos - percentagemAtivosPagantes).ToString().Replace(".", ","));

                    //table.AppendFormat(@"<tr ondblclick=''>
                    //                        <td style='width:70%'>{0}</td>
                    //                        <td style='width:30%; text-align: center;'>
                    //                            <span style='font-weight:bold'>
                    //                                TOTAL ATIVOS NÃO PAGANTES<br />
                    //                                {1}<br />
                    //                                {2} €<br />
                    //                                {3} %
                    //                            </span>
                    //                        </td>
                    //                    </tr>",
                    //                            tableAtivosNaoPagantes.ToString(),
                    //                            contratosAtivosNaoPagantes.ToString(),
                    //                            valorAtivosNaoPagantes.ToString().Replace(".", ","),
                    //                            percentagemAtivosNaoPagantes.ToString("0.00").Replace(".", ","));

                    table.AppendFormat("</tbody></table>");
                }
                if (flag == "8")
                {
                    // Adiciona as linhas
                    table.AppendFormat(@"   <table id='tableGrid' style='border: none !important;'>
                                            <thead>
						                        <tr>
                                                    <th colspan='2' style='text-align: center; -webkit-border-radius: 4px !important; border-radius: 4px !important;'>Pagantes / Não Pagantes Mês Seguinte</th>
						                        </tr>
						                    </thead>
                                            <tbody>");
                    string pagante = "";
                    string naopagante = "";
                    int sumPagante = 0;
                    int sumNaoPagante = 0;
                    double sumPercPagante = 0.00;
                    double sumPercNaoPagante = 0.00;
                    double sumValorPagante = 0.00;
                    double sumValorNaoPagante = 0.00;

                    for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                    {
                        if (oDs.Tables[0].Rows[i]["PAGANTE"].ToString() == "1")
                        {
                            pagante += "<span style='font-size: small;'>" + oDs.Tables[0].Rows[i]["TITULO"].ToString() + ": " + oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString() + " contratos (" + oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM"].ToString().Replace(".", ",") + "%) (" + oDs.Tables[0].Rows[i]["VALOR"].ToString().Replace(".", ",") + "€) </span><br />";
                            sumPagante += Convert.ToInt32(oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString());
                            sumPercPagante += Convert.ToDouble(oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM"].ToString());
                            sumValorPagante += Convert.ToDouble(oDs.Tables[0].Rows[i]["VALOR"].ToString());
                        }
                        else
                        {
                            naopagante += "<span style='font-size: small;'>" + oDs.Tables[0].Rows[i]["TITULO"].ToString() + ": " + oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString() + " contratos (" + oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM"].ToString().Replace(".", ",") + "%) (" + oDs.Tables[0].Rows[i]["VALOR"].ToString().Replace(".", ",") + "€) </span><br />";
                            sumNaoPagante += Convert.ToInt32(oDs.Tables[0].Rows[i]["NR_CONTRATOS"].ToString());
                            sumPercNaoPagante += Convert.ToDouble(oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM"].ToString());
                            sumValorNaoPagante += Convert.ToDouble(oDs.Tables[0].Rows[i]["VALOR"].ToString());
                        }
                    }

                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick='openPopupPagantesMesSeguinte(1);'>
                                            <td style='width:65%'>Pagantes<br />{0}</td>
                                            <td style='width:35%; text-align: left'>{1}%<br /><span style='font-size: small;'>{2} Contratos<br />{3} €</span></td>
                                        </tr>",
                                                pagante,
                                                sumPercPagante.ToString().Replace(".", ","),
                                                sumPagante,
                                                sumValorPagante.ToString().Replace(".", ","));
                    table.AppendFormat(@"<tr ondblclick='openPopupPagantesMesSeguinte(0);'>
                                            <td style='width:65%'>Não Pagantes<br />{0}</td>
                                            <td style='width:35%; text-align: left'>{1}%<br /><span style='font-size: small;'>{2} Contratos<br />{3} €</span></td>
                                        </tr>",
                                                naopagante,
                                                sumPercNaoPagante.ToString().Replace(".", ","),
                                                sumNaoPagante,
                                                sumValorNaoPagante.ToString().Replace(".", ","));

                    table.AppendFormat("</tbody></table>");
                }
                else if (flag == "9")
                {
                    // Adiciona as linhas
                    table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerLeft' rowspan='2'>Sexo</th>
                                                    <th class='headerRight' colspan='2'>Nº de Contratos</th>
						                        </tr>
                                                <tr>
                                                    <th style='width: 15%; text-align: center'>Ativos</th>
                                                    <th style='width: 15%; text-align: center; -webkit-border-bottom-right-radius: 4px !important; border-bottom-right-radius: 4px !important;''>Inativos</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                    for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                    {
                        // Adiciona as linhas com dados
                        table.AppendFormat(@"<tr ondblclick='openPopupSexo({7});'>
                                            <td>{0}<br /><span style='font-size: small;'>{1} Contratos</span></td>
                                            <td style='width:15%; text-align: left'>{3}%<br /><span style='font-size: small;'>{2} Contratos</span></td>
                                            <td style='width:15%; text-align: right; color: red;'>{5}%<br /><span style='font-size: small;'>{4} Contratos</span></td>
                                        </tr>",
                                                    oDs.Tables[0].Rows[i]["SEXO"].ToString(),
                                                    oDs.Tables[0].Rows[i]["TOTAL_CONTRATOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["NR_ATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM_ATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["NR_DESATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["VALOR_PERCENTAGEM_DESATIVOS"].ToString(),
                                                    oDs.Tables[0].Rows[i]["SEXO"].ToString(),
                                                    oDs.Tables[0].Rows[i]["RANK_VALUE"].ToString());
                    }

                    table.AppendFormat("</tbody></table>");
                }
            }
            else
            {
                if (flag == "0")
                {
                    table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem estatísticas a apresentar para os Tipos de Contrato</div>");
                }
                else if (flag == "1")
                {
                    table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem estatísticas a apresentar para os Estados de Contrato</div>");
                }
                if (flag == "2")
                {
                    table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem estatísticas a apresentar para as Profissões</div>");
                }
                else if (flag == "3")
                {
                    table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem estatísticas a apresentar para as Margens de Idades</div>");
                }
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto; background-color: #FFF' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem estatísticas a apresentar: {0}</div>", exc.ToString());
            return table.ToString();
        }

        return table.ToString();
    }

    private void loadFiltrosEntradas()
    {
        var table = new StringBuilder();
        var table2 = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        int conta = 0;
        string ano = "";
        string mesTexto = "";

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT DISTINCT YEAR(DATA_ENTRADA) ANO
                                            FROM ENTRADAS
                                            ORDER BY YEAR(DATA_ENTRADA) DESC");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"<span style='width:50%; float: left;'>ANO:</span>");
                table.AppendFormat(@"   <select class='form-control' id='entradasAnoSelect' style='width:50%; height: 40px; font-size: small; float: right;' onchange='loadStats();'>");

                while (myReader.Read())
                {
                    if (conta == 0)
                        ano = myReader["ANO"].ToString();

                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<option value='{0}' {1}>{0}</option>",
                                                myReader["ANO"].ToString(),
                                                conta == 0 ? "selected" : "");

                    conta++;
                }

                table.AppendFormat("</select>");
            }
            else
            {
                connection.Close();
                table.AppendFormat(@"<span style='width:50%; float: left;'>ANO:</span>");
                table.AppendFormat(@"   <select class='form-control' id='entradasAnoSelect' style='width:50%; height: 40px; font-size: small; float: right;' onchange='loadStats();'>
                                            <option value='0'>Não existem entradas em nenhum ano</option>
                                        </select>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat(@"<span style='width:50%; float: left;'>ANO:</span>");
            table.AppendFormat(@"   <select class='form-control' id='entradasAnoSelect' style='width:50%; height: 40px; font-size: small; float: right;' onchange='loadStats();'>
                                        <option value='0'>{0}</option>
                                    </select>", exc.ToString());
        }

        connection.Close();
        divEntradasAno.InnerHtml = table.ToString();

        conta = 0;

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET LANGUAGE 'Portuguese';
                                            SELECT DISTINCT MONTH(DATA_ENTRADA) MES
                                            FROM ENTRADAS
                                            WHERE YEAR(DATA_ENTRADA) = {0}
                                            ORDER BY MONTH(DATA_ENTRADA) DESC", ano);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table2.AppendFormat(@"<span style='width:45%; float: left;'>MÊS:</span>");
                table2.AppendFormat(@"   <select class='form-control' id='entradasMesSelect' style='width:45%; height: 40px; font-size: small; float: right;' onchange='loadStats();'>");

                while (myReader.Read())
                {
                    switch (myReader["MES"].ToString())
                    {
                        case "1":
                            mesTexto = "Janeiro";
                            break;
                        case "2":
                            mesTexto = "Fevereiro";
                            break;
                        case "3":
                            mesTexto = "Março";
                            break;
                        case "4":
                            mesTexto = "Abril";
                            break;
                        case "5":
                            mesTexto = "Maio";
                            break;
                        case "6":
                            mesTexto = "Junho";
                            break;
                        case "7":
                            mesTexto = "Julho";
                            break;
                        case "8":
                            mesTexto = "Agosto";
                            break;
                        case "9":
                            mesTexto = "Setembro";
                            break;
                        case "10":
                            mesTexto = "Outubro";
                            break;
                        case "11":
                            mesTexto = "Novembro";
                            break;
                        case "12":
                            mesTexto = "Dezembro";
                            break;
                    }

                    // Adiciona as linhas com dados
                    table2.AppendFormat(@"<option value='{0}' {1}>{2}</option>",
                                                myReader["MES"].ToString(),
                                                conta == 0 ? "selected" : "",
                                                mesTexto);

                    conta++;
                }

                table2.AppendFormat("</select>");
            }
            else
            {
                connection.Close();
                table2.AppendFormat(@"<span style='width:50%'>MÊS:</span>");
                table2.AppendFormat(@"   <select class='form-control' id='entradasMesSelect' style='width:50%; height: 40px; font-size: small; float: right;' onchange='loadStats();'>
                                            <option value='0'>Não existem entradas em nenhum ano</option>
                                        </select>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table2.AppendFormat(@"<span style='width:50%'>MÊS:</span>");
            table2.AppendFormat(@"   <select class='form-control' id='entradasMesSelect' style='width:50%; height: 40px; font-size: small; float: right;' onchange='loadStats();'>
                                        <option value='0'>{0}</option>
                                    </select>", exc.ToString());
        }

        connection.Close();
        divEntradasMes.InnerHtml = table2.ToString();
    }

    [WebMethod]
    public static string loadStatsEntradas(string ano, string mes)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY
                                            SET LANGUAGE 'Portuguese'
                                            DECLARE @data datetime;
                                            DECLARE @ano int = {0};
                                            DECLARE @mes int = {1};

                                            select top 1 @data = DATEADD(hh, -1, DATA_ENTRADA)
                                            from entradas
                                            where YEAR(DATA_ENTRADA) = @ano
                                            and MONTH(DATA_ENTRADA) = @mes

                                            select *
                                            from (

                                            select ('Total Entradas ' + 
                                                (UPPER(LEFT(DATENAME(MONTH, @data),1))+LOWER(SUBSTRING(DATENAME(MONTH, @data),2,LEN(DATENAME(MONTH, @data))))) + 
                                                ' ' + LTRIM(RTRIM(STR(@ano)))) as texto, 
                                                LTRIM(RTRIM(STR(count(distinct entradasid)))) + ' entradas' as valor,
                                                count(distinct entradasid) as nr_entradas,
                                                1 as ordem
                                            from entradas
                                            where YEAR(DATA_ENTRADA) = @ano
                                            and MONTH(DATA_ENTRADA) = @mes
                                            UNION
                                            select top 1 ('Sócio com Mais Entradas ' + 
                                                (UPPER(LEFT(DATENAME(MONTH, @data),1))+LOWER(SUBSTRING(DATENAME(MONTH, @data),2,LEN(DATENAME(MONTH, @data))))) + 
                                                ' ' + LTRIM(RTRIM(STR(@ano)))) as texto, 
                                                LTRIM(RTRIM(STR(soc.nr_socio))) + ' - ' + LTRIM(RTRIM(soc.nome)) + '<br/>' + LTRIM(RTRIM(STR(count(distinct entradasid)))) + ' entradas' as valor,
                                                count(distinct entradasid) as nr_entradas,
                                                2 as ordem
                                            from entradas ent
                                            inner join socios soc on soc.sociosid = ent.id_socio
                                            where YEAR(DATA_ENTRADA) = @ano
                                            and MONTH(DATA_ENTRADA) = @mes
                                            group by soc.nr_socio, soc.nome
                                            order by nr_entradas desc, soc.nr_socio, soc.nome
                                            UNION
                                            select ('Total Entradas ' + LTRIM(RTRIM(STR(@ano)))) as texto, 
                                                LTRIM(RTRIM(STR(count(distinct entradasid)))) + ' entradas' as valor,
                                                count(distinct entradasid) as nr_entradas,
                                                3 as ordem
                                            from entradas
                                            where YEAR(DATA_ENTRADA) = @ano
                                            UNION
                                            select top 1 ('Sócio com Mais Entradas ' + LTRIM(RTRIM(STR(@ano)))) as texto, 
                                                LTRIM(RTRIM(STR(soc.nr_socio))) + ' - ' + LTRIM(RTRIM(soc.nome)) + '<br/>' + LTRIM(RTRIM(STR(count(distinct entradasid)))) + ' entradas' as valor,
                                                count(distinct entradasid) as nr_entradas,
                                                4 as ordem
                                            from entradas ent
                                            inner join socios soc on soc.sociosid = ent.id_socio
                                            where YEAR(DATA_ENTRADA) = @ano
                                            group by soc.nr_socio, soc.nome
                                            order by nr_entradas desc, soc.nr_socio, soc.nome
                                            ) as tmp
                                            order by ordem

                                            --select count(distinct entradasid) as nr_entradas, CONVERT(VARCHAR(10), DATA_ENTRADA, 103) as data, (UPPER(LEFT(DATENAME(MONTH, @data),1))+LOWER(SUBSTRING(DATENAME(MONTH, @data),2,LEN(DATENAME(MONTH, @data))))) as MES
                                            --from entradas ent
                                            --inner join socios soc on soc.sociosid = ent.id_socio
                                            --where YEAR(DATA_ENTRADA) = 2018
                                            --and MONTH(DATA_ENTRADA) = 2
                                            --group by CONVERT(VARCHAR(10), DATA_ENTRADA, 103)
                                            --order by CONVERT(VARCHAR(10), DATA_ENTRADA, 103)", ano, mes);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid' style='border: none !important;'>
                                            <thead>
						                        <tr>
                                                    <th colspan='2' style='text-align: center; -webkit-border-radius: 4px !important; border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;'>Entradas</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr>
                                            <td style='width: 50%'>{0}</td>
                                            <td style='width: 50%'>{1}</td>
                                        </tr>",
                                                myReader["texto"].ToString(),
                                                myReader["valor"].ToString());
                }

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                connection.Close();
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem estatísticas a apresentar para as entradas</div>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem estatísticas a apresentar para as entradas: {0}</div>", exc.ToString());
        }

        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string loadFiltroAno(string operatorID)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        int conta = 0;
        string ano = "";
        string mesTexto = "";

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT DISTINCT YEAR(DATA_ENTRADA) ANO
                                            FROM ENTRADAS
                                            ORDER BY YEAR(DATA_ENTRADA) DESC");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"<span style='width:50%; float: left;'>ANO:</span>");
                table.AppendFormat(@"   <select class='form-control' id='entradasAnoSelect' style='width:50%; height: 40px; font-size: small; float: right;' onchange='loadMes();'>");

                while (myReader.Read())
                {
                    if (conta == 0)
                        ano = myReader["ANO"].ToString();

                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<option value='{0}' {1}>{0}</option>",
                                                myReader["ANO"].ToString(),
                                                conta == 0 ? "selected" : "");

                    conta++;
                }

                table.AppendFormat("</select>");
            }
            else
            {
                connection.Close();
                table.AppendFormat(@"<span style='width:50%; float: left;'>ANO:</span>");
                table.AppendFormat(@"   <select class='form-control' id='entradasAnoSelect' style='width:50%; height: 40px; font-size: small; float: right;' onchange='loadMes();'>
                                            <option value='0'>Não existem entradas em nenhum ano</option>
                                        </select>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat(@"<span style='width:50%; float: left;'>ANO:</span>");
            table.AppendFormat(@"   <select class='form-control' id='entradasAnoSelect' style='width:50%; height: 40px; font-size: small; float: right;' onchange='loadMes();'>
                                        <option value='0'>{0}</option>
                                    </select>", exc.ToString());
        }

        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string loadFiltroMes(string ano)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        int conta = 0;
        string mesTexto = "";
        try
        {
            connection.Open();

            string sql = string.Format(@"   SET LANGUAGE 'Portuguese';
                                            SELECT DISTINCT MONTH(DATA_ENTRADA) MES
                                            FROM ENTRADAS
                                            WHERE YEAR(DATA_ENTRADA) = {0}
                                            ORDER BY MONTH(DATA_ENTRADA) DESC", ano);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"<span style='width:45%; float: left;'>MÊS:</span>");
                table.AppendFormat(@"   <select class='form-control' id='entradasMesSelect' style='width:45%; height: 40px; font-size: small; float: right;' onchange='loadEntradasTable();'>");

                while (myReader.Read())
                {
                    switch (myReader["MES"].ToString())
                    {
                        case "1":
                            mesTexto = "Janeiro";
                            break;
                        case "2":
                            mesTexto = "Fevereiro";
                            break;
                        case "3":
                            mesTexto = "Março";
                            break;
                        case "4":
                            mesTexto = "Abril";
                            break;
                        case "5":
                            mesTexto = "Maio";
                            break;
                        case "6":
                            mesTexto = "Junho";
                            break;
                        case "7":
                            mesTexto = "Julho";
                            break;
                        case "8":
                            mesTexto = "Agosto";
                            break;
                        case "9":
                            mesTexto = "Setembro";
                            break;
                        case "10":
                            mesTexto = "Outubro";
                            break;
                        case "11":
                            mesTexto = "Novembro";
                            break;
                        case "12":
                            mesTexto = "Dezembro";
                            break;
                    }

                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<option value='{0}' {1}>{2}</option>",
                                                myReader["MES"].ToString(),
                                                conta == 0 ? "selected" : "",
                                                mesTexto);

                    conta++;
                }

                table.AppendFormat("</select>");
            }
            else
            {
                connection.Close();
                table.AppendFormat(@"<span style='width:50%'>MÊS:</span>");
                table.AppendFormat(@"   <select class='form-control' id='entradasMesSelect' style='width:50%; height: 40px; font-size: small; float: right;' onchange='loadEntradasTable();'>
                                            <option value='0'>Não existem entradas em nenhum ano</option>
                                        </select>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat(@"<span style='width:50%'>MÊS:</span>");
            table.AppendFormat(@"   <select class='form-control' id='entradasMesSelect' style='width:50%; height: 40px; font-size: small; float: right;' onchange='loadEntradasTable();'>
                                        <option value='0'>{0}</option>
                                    </select>", exc.ToString());
        }

        connection.Close();
        return table.ToString();
    }

    private void loadDataFiltroContratos()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   declare @data_atual datetime = DATEADD(hh, -1, getdate())
                                            declare @dia int = DAY(@data_atual)
                                            declare @data_inicio_filtro datetime = DATEADD(dd, ((@dia - 1) * -1), @data_atual)
                                            declare @data_fim_filtro datetime = DATEADD(dd, -1, DATEADD(mm, 1, @data_inicio_filtro))

                                            select
                                                convert(varchar(10), @data_inicio_filtro, 103) as data_inicio,
                                                convert(varchar(10), @data_fim_filtro, 103) as data_fim");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    lbldatainicio.InnerHtml = myReader["data_inicio"].ToString();
                    lbldatafim.InnerHtml = myReader["data_fim"].ToString();
                }
            }

            connection.Close();
        }
        catch (Exception exc)
        {
            connection.Close();
        }
    }

    [WebMethod]
    public static string loadStatsContratos(string data_inicio, string data_fim)
    {
        var table = new StringBuilder();
        var tableTemp = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY
                                            declare @data_inicio date = {0};
                                            declare @data_fim date = {1};

                                            select 
                                                CONVERT(VARCHAR(10), rpt.data_inicio, 103) as data_inicio,
                                                CONVERT(VARCHAR(10), rpt.data_fim, 103) as data_fim,
                                                soc.NR_SOCIO,
                                                LTRIM(RTRIM(soc.NOME)) as NOME
                                            from REPORT_CONTRATOS_DENTRO_DATA(null, null) rpt
                                            inner join socios soc on soc.sociosid = rpt.id_socio
                                            where (@data_inicio is null or cast(rpt.data_fim as date) >= @data_inicio)
                                            and 
                                                (
                                                    @data_fim is null 
                                                    or 
                                                    (cast(rpt.data_fim as date) <= @data_fim and @data_inicio is not null) 
                                                    or 
                                                    (@data_inicio is null and cast(rpt.data_fim as date) = @data_fim)
                                                )
                                            and ativo = 1
                                            order by soc.NR_SOCIO", string.IsNullOrEmpty(data_inicio) ? "NULL" : "'" + data_inicio + "'"
                                                                  , string.IsNullOrEmpty(data_fim) ? "NULL" : "'" + data_fim + "'");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    tableTemp.AppendFormat(@"<tr>
                                            <td style='width: 60%'>{0} - {1}</td>
                                            <td style='width: 40%'>{2} - {3}</td>
                                        </tr>",
                                                myReader["NR_SOCIO"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["data_inicio"].ToString(),
                                                myReader["data_fim"].ToString());

                    conta++;
                }

                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid' style='border: none !important; margin-bottom: 10px;'>
                                            <thead>
						                        <tr>
                                                    <th colspan='2' style='text-align: center; -webkit-border-radius: 4px !important; border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;'>{3}</th>
						                        </tr>
						                    </thead>
                                            <tbody>", conta.ToString()
                                                    , data_inicio
                                                    , data_fim
                                                    , string.IsNullOrEmpty(data_inicio) ? "Contratos a terminar a " + data_fim : "Contratos a terminar entre " + data_inicio + " e " + data_fim);

                table.AppendFormat(@"{0}", tableTemp.ToString());

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                connection.Close();
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem contratos a terminar no período indicado.</div>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem contratos a terminar no período indicado: {0}</div>", exc.ToString());
        }

        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string loadDadosGraficoEntradas(string ano, string mes)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY
                                            SET LANGUAGE 'Portuguese'
                                            DECLARE @data datetime;
                                            DECLARE @ano int = {0};
                                            DECLARE @mes int = {1};
                                            DECLARE @mesNome varchar(50);
                                            DECLARE @dataatual datetime = DATEADD(hh, -1, GETDATE())
                                            DECLARE @datainicial datetime = '01-' + LTRIM(RTRIM(STR(@mes))) + '-' + LTRIM(RTRIM(STR(@ano)));
                                            DECLARE @datafinal datetime = DATEADD(dd, -1, DATEADD(mm, 1, @datainicial));
                                            DECLARE @tableData table(dia int not null, mes int not null, ano int not null);

                                            select top 1 @data = DATEADD(hh, -1, DATA_ENTRADA)
                                            from entradas
                                            where YEAR(DATA_ENTRADA) = @ano
                                            and MONTH(DATA_ENTRADA) = @mes

                                            select @mesNome = (UPPER(LEFT(DATENAME(MONTH, @data),1))+LOWER(SUBSTRING(DATENAME(MONTH, @data),2,LEN(DATENAME(MONTH, @data)))))

                                            IF(@mes = MONTH(@dataatual) and @ano = YEAR(@dataatual))
                                            BEGIN
                                                While @datainicial <= @dataatual
                                                Begin
	                                            INSERT INTO @tableData(dia, mes, ano)
	                                            VALUES(DAY(@datainicial), MONTH(@datainicial), YEAR(@datainicial))
	
	                                            SET @datainicial = DATEADD(dd, 1, @datainicial);
                                                End
                                            END
                                            ELSE
                                            BEGIN
                                                While @datainicial <= @datafinal
                                                Begin
	                                            INSERT INTO @tableData(dia, mes, ano)
	                                            VALUES(DAY(@datainicial), MONTH(@datainicial), YEAR(@datainicial))
	
	                                            SET @datainicial = DATEADD(dd, 1, @datainicial);
                                                End
                                            END

                                            select count(entradasid) as nrEntradas, td.dia as DIA, @mesNome as mesNome
                                            from @tableData td
                                            left join entradas ent 
                                            on DAY(DATEADD(hh, -1, ent.DATA_ENTRADA)) = td.dia
                                            and MONTH(DATEADD(hh, -1, ent.DATA_ENTRADA)) = td.mes
                                            and YEAR(DATEADD(hh, -1, ent.DATA_ENTRADA)) = td.ano
                                            group by td.dia
                                            order by td.dia", ano, mes);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <span class='variaveis' id='diaGrafico_{1}'>{0}</span>
                                            <span class='variaveis' id='mesGrafico_{1}'>{3}</span>
                                            <span class='variaveis' id='nrEntradasGrafico_{1}'>{2}</span>",
                                                myReader["DIA"].ToString(),
                                                conta.ToString(),
                                                myReader["nrEntradas"].ToString(),
                                                myReader["mesNome"].ToString());

                    conta++;
                }

                // Adiciona as linhas com dados
                table.AppendFormat(@"   <span class='variaveis' id='nrdias'>{0}</span>",
                                            conta.ToString());
            }
            else
            {
                // Adiciona as linhas com dados
                table.AppendFormat(@"   <span class='variaveis' id='nrdias'>0</span>");
            }
        }
        catch (Exception exc)
        {
            // Adiciona as linhas com dados
            table.AppendFormat(@"   <span class='variaveis' id='nrdias'>0</span>");
        }

        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string loadSociosTipoContrato(string id_tipo)
    {
        var table = new StringBuilder();
        var tableTemp = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY
                                            declare @id_tipo_contrato int = {0};
                                            declare @data_atual datetime = DATEADD(hh, -1, GETDATE());
                                            declare @tipo varchar(100) = (SELECT LTRIM(RTRIM(DESIGNACAO)) FROM TIPO_CONTRATO WHERE TIPO_CONTRATOID = @id_tipo_contrato);

                                            select 
                                                soc.NR_SOCIO,
                                                LTRIM(RTRIM(soc.NOME)) as NOME,
                                                CAST(st.ATIVO as INT) as ATIVO,
                                                @tipo as TIPO
                                            from contrato cont
                                            inner join socios soc on soc.sociosid = cont.socio_id
                                            inner join estados_contrato st on st.estados_contratoid = cont.estado_contrato_id
                                            where cast(cont.data_fim as date) >= CAST(@data_atual as date)
                                            and cast(cont.data_inicio as date) <= CAST(@data_atual as date)
                                            and cont.tipo_contrato_id = @id_tipo_contrato
                                            order by st.ativo desc, soc.NR_SOCIO", id_tipo);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;
            string tipo = "";

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    tableTemp.AppendFormat(@"   <tr>
                                                    <td style='width: 100%; {2}'>{0} - {1}</td>
                                                </tr>",
                                                myReader["NR_SOCIO"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["ATIVO"].ToString() == "1" ? "color: #000;" : "color: red;");

                    tipo = myReader["TIPO"].ToString();

                    conta++;
                }

                // Adiciona as linhas
                table.AppendFormat(@"   <input type='button' class='form-control' value='Exportar para Excel' style='width: 100%; height: auto; margin-bottom: 10px;' onclick='exportPopupTable();' />
                                        <table id='tablePopup' style='border: none !important; margin-bottom: 10px;'>
                                            <thead>
						                        <tr>
                                                    <th style='text-align: center; -webkit-border-radius: 4px !important; border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;'>Contratos {0} ({1})</th>
						                        </tr>
						                    </thead>
                                            <tbody>", tipo
                                                    , conta.ToString());

                table.AppendFormat(@"{0}", tableTemp.ToString());

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                connection.Close();
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem contratos a apresentar.</div>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem contratos a apresentar: {0}</div>", exc.ToString());
        }

        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string loadSociosEstadoContrato(string id_estado)
    {
        var table = new StringBuilder();
        var tableTemp = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY
                                            DECLARE @id_estado int = {0};
                                            SELECT
                                                NR_SOCIO,
                                                NOME,
                                                ESTADO,
                                                MES_ATUAL,
                                                ID_ESTADO,
                                                FLAG_CONGELADOS
                                            FROM REPORT_ESTATISTICA_SOCIOS_ESTADO_PAGAMENTO(@id_estado)
                                            order by flag_congelados asc, nr_socio asc", id_estado);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;
            string estado = "";
            int contaMesAtual = 0;
            Boolean flag = false;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    tableTemp.AppendFormat(@"   <tr>
                                                    <td style='width: 100%; {2}'>{0} - {1}</td>
                                                </tr>",
                                                myReader["NR_SOCIO"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["FLAG_CONGELADOS"].ToString() == "0" && myReader["ESTADO"].ToString() == "Congelado" ? "color: blue;" : "color: #000;");

                    if (myReader["FLAG_CONGELADOS"].ToString() == "0" && myReader["ESTADO"].ToString() == "Congelado")
                        contaMesAtual++;
                    else if (myReader["MES_ATUAL"].ToString() == "-1")
                        flag = true;

                    estado = myReader["ESTADO"].ToString();

                    conta++;
                }

                // Adiciona as linhas
                table.AppendFormat(@"   <input type='button' class='form-control' value='Exportar para Excel' style='width: 100%; height: auto; margin-bottom: 10px;' onclick='exportPopupTable();' />
                                        <table id='tablePopup' style='border: none !important; margin-bottom: 10px;'>
                                            <thead>
						                        <tr>
                                                    <th style='text-align: center; -webkit-border-radius: 4px !important; border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;'>Contratos {0} ({2}{1}){3}</th>
						                        </tr>
						                    </thead>
                                            <tbody>", estado
                                                    , conta.ToString()
                                                    , flag == false ? contaMesAtual.ToString() + "/" : ""
                                                    , contaMesAtual != 0 ? " (" + contaMesAtual.ToString() + " Ativos)" : "");

                table.AppendFormat(@"{0}", tableTemp.ToString());

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                connection.Close();
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem contratos a apresentar.</div>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem contratos a apresentar: {0}</div>", exc.ToString());
        }

        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string loadSociosIdades(string margem_inferior, string margem_superior)
    {
        var table = new StringBuilder();
        var tableTemp = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY
                                            declare @margem_inferior int = {0};
                                            declare @margem_superior int = {1};

                                            with idades_ativos as (
	                                            select 
							                        soc.NR_SOCIO, LTRIM(RTRIM(soc.NOME)) as NOME,
							                        idade.IDADE, st.ATIVO 
	                                            from socios soc
	                                            inner JOIN report_contratos_dentro_data(null, null) rpt on rpt.id_socio = soc.SOCIOSID
	                                            inner JOIN ESTADOS_CONTRATO st on st.ESTADOS_CONTRATOID = rpt.id_estado
	                                            cross apply REPORT_IDADE(CAST(soc.DATA_NASCIMENTO as DATE)) idade
	                                            where st.ATIVO = 1
						                        and idade.IDADE >= @margem_inferior
						                        and idade.IDADE <= @margem_superior
                                            ),
    
                                            idades_inativos as (
	                                            select
							                        soc.NR_SOCIO, LTRIM(RTRIM(soc.NOME)) as NOME,
							                        idade.IDADE, st.ATIVO 
	                                            from socios soc
	                                            inner JOIN report_contratos_dentro_data(null, null) rpt on rpt.id_socio = soc.SOCIOSID
	                                            inner JOIN ESTADOS_CONTRATO st on st.ESTADOS_CONTRATOID = rpt.id_estado
	                                            cross apply REPORT_IDADE(CAST(soc.DATA_NASCIMENTO as DATE)) idade
	                                            where st.ATIVO = 0
						                        and idade.IDADE >= @margem_inferior
						                        and idade.IDADE <= @margem_superior
	                                            UNION
	                                            select
							                        soc.NR_SOCIO, LTRIM(RTRIM(soc.NOME)) as NOME,
							                        idade.IDADE, 0 as ATIVO 
	                                            from socios soc
	                                            cross apply REPORT_IDADE(CAST(soc.DATA_NASCIMENTO as DATE)) idade
	                                            WHERE soc.sociosid not in (select id_socio from report_contratos_dentro_data(null, null))
						                        and idade.IDADE >= @margem_inferior
						                        and idade.IDADE <= @margem_superior
                                            )
					    
					                        select NR_SOCIO, NOME, ATIVO, IDADE
					                        from (
						                        select NR_SOCIO, NOME, ATIVO, IDADE
						                        from idades_ativos
						                        UNION
						                        select NR_SOCIO, NOME, ATIVO, IDADE
						                        from idades_inativos
					                        ) as tmp
					                        order by ATIVO DESC, NR_SOCIO ASC", margem_inferior, margem_superior);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    tableTemp.AppendFormat(@"   <tr>
                                                    <td style='width: 100%; {2}'>
                                                        {0} - {1}<br />
                                                        <span style='font-size: small:'>{3} anos</span>
                                                    </td>
                                                </tr>",
                                                myReader["NR_SOCIO"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["ATIVO"].ToString() == "1" ? "color: #000;" : "color: red;",
                                                myReader["IDADE"].ToString());

                    conta++;
                }

                // Adiciona as linhas
                table.AppendFormat(@"   <input type='button' class='form-control' value='Exportar para Excel' style='width: 100%; height: auto; margin-bottom: 10px;' onclick='exportPopupTable();' />
                                        <table id='tablePopup' style='border: none !important; margin-bottom: 10px;'>
                                            <thead>
						                        <tr>
                                                    <th style='text-align: center; -webkit-border-radius: 4px !important; border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;'>Sócios entre {0} e os {1} anos ({2})</th>
						                        </tr>
						                    </thead>
                                            <tbody>", margem_inferior
                                                    , margem_superior
                                                    , conta.ToString());

                table.AppendFormat(@"{0}", tableTemp.ToString());

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                connection.Close();
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem sócios a apresentar.</div>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem sócios a apresentar: {0}</div>", exc.ToString());
        }

        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string loadSociosPagantes(string tabela, string id_estado, string id_estado_pagamento)
    {
        var table = new StringBuilder();
        var tableTemp = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = "";

            switch (tabela)
            {
                case "0":
                    sql = string.Format(@"  declare @id_estado int = {0};
                                            select nr_socio, nome, estado, valor
                                            from REPORT_ESTATISTICA_SOCIOS_PAGANTES_NAOPAGANTES()
                                            where ativo = 1
                                            and id_estado = @id_estado
                                            order by estado, nr_socio", id_estado);
                    break;

                case "1":
                    sql = string.Format(@"  declare @id_estado int = {0};

                                            IF(select codigo from estados_contrato where estados_contratoid = @id_estado) = 'ACTIVO'
                                            begin
                                                select nr_socio, nome, estado, valor
                                                from REPORT_ESTATISTICA_SOCIOS_PAGANTES_NAOPAGANTES()
                                                where ativo = 1
                                                order by estado, nr_socio
                                            end
                                            else
                                            begin
                                                select nr_socio, nome, estado, valor
                                                from REPORT_ESTATISTICA_SOCIOS_PAGANTES_NAOPAGANTES()
                                                where ativo = 1
                                                and id_estado = @id_estado
                                                order by estado, nr_socio
                                            end", id_estado);
                    break;

                case "2":
                    sql = string.Format(@"  declare @id_estado int = {0};
                                            declare @id_estado_pagamento int = {1};

                                            IF(select codigo from estados_contrato where estados_contratoid = @id_estado) = 'INACTIVO'
                                            begin
                                                select nr_socio, nome, estado, valor
                                                from REPORT_ESTATISTICA_SOCIOS_PAGANTES_NAOPAGANTES()
                                                where ativo = 0
                                                order by estado, nr_socio
                                            end
                                            else
                                            begin
                                                select distinct
		                                            soc.nr_socio, soc.nome, 
                                                    case when pag.valor is null or pag.valor = 0 then cont.valor else pag.valor end as valor,
		                                            case when pagst.pagamentos_statusid is null then 'SEM ESTADO DE PAGAMENTO'
                                                    else pagst.designacao
                                                    end as estado
	                                            from report_contratos_dentro_data(null, null) rpt
	                                            inner join socios soc on soc.sociosid = rpt.id_socio
	                                            left join estados_contrato stpag on stpag.estados_contratoid = rpt.id_estado_contrato_status_pagamento
	                                            inner join estados_contrato st on st.estados_contratoid = rpt.id_estado
	                                            inner join pagamentos pag on pag.pagamentosid = rpt.id_pagamento_mes_corrente
	                                            inner join contrato cont on cont.contratoid = rpt.id_contrato
	                                            left join pagamentos_status pagst on pagst.pagamentos_statusid = rpt.id_status_pagamento
	                                            where isnull(stpag.codigo, st.codigo) not in ('NAO_RENOV', 'INACTIVO', 'CANC_PAGO', 'CANC_FALTA_PAG')
	                                            and (rpt.id_status_pagamento = 0 or pagst.codigo = 'NAO-PAGO')
                                            end", id_estado, id_estado_pagamento);
                    break;
            }

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    tableTemp.AppendFormat(@"   <tr>
                                                    <td style='width: 100%; {2}'>{0} - {1}<br /><span style='font-size:small'>{3} ({4} Euros)</span></td>
                                                </tr>",
                                                myReader["NR_SOCIO"].ToString(),
                                                myReader["NOME"].ToString(),
                                                "color: #000;",
                                                myReader["ESTADO"].ToString(),
                                                myReader["VALOR"].ToString());

                    conta++;
                }

                // Adiciona as linhas
                table.AppendFormat(@"   <input type='button' class='form-control' value='Exportar para Excel' style='width: 100%; height: auto; margin-bottom: 10px;' onclick='exportPopupTable();' />
                                        <table id='tablePopup' style='border: none !important; margin-bottom: 10px;'>
                                            <thead>
						                        <tr>
                                                    <th style='text-align: center; -webkit-border-radius: 4px !important; border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;'>Sócios {0} ({1})</th>
						                        </tr>
						                    </thead>
                                            <tbody>", tabela == "0" ? "Ativos" : (tabela == "1" ? "Ativos Pagantes" : "Ativos Não Pagantes")
                                                    , conta.ToString());

                table.AppendFormat(@"{0}", tableTemp.ToString());

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                connection.Close();
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem sócios a apresentar.</div>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem sócios a apresentar: {0}</div>", exc.ToString());
        }

        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string loadSociosPagantesMesSeguinte(string pagante)
    {
        var table = new StringBuilder();
        var tableTemp = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"SET DATEFORMAT DMY
                                        declare @pagante bit = {0};
                                        select
                                            NR_SOCIO,
						                    NOME,
						                    ESTADO,
						                    VALOR
	                                    from REPORT_ESTATISTICA_SOCIOS_PAGANTES_NAOPAGANTES_MES_SEGUINTE(@pagante)
                                        order by estado, nr_socio", pagante);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    tableTemp.AppendFormat(@"   <tr>
                                                    <td style='width: 100%; {2}'>{0} - {1}<br /><span style='font-size:small'>{3} ({4} €)</span></td>
                                                </tr>",
                                                myReader["NR_SOCIO"].ToString(),
                                                myReader["NOME"].ToString(),
                                                pagante == "0" ? "color: red;" : "color: #000;",
                                                myReader["ESTADO"].ToString(),
                                                myReader["VALOR"].ToString());

                    conta++;
                }

                // Adiciona as linhas
                table.AppendFormat(@"   <input type='button' class='form-control' value='Exportar para Excel' style='width: 100%; height: auto; margin-bottom: 10px;' onclick='exportPopupTable();' />
                                        <table id='tablePopup' style='border: none !important; margin-bottom: 10px;'>
                                            <thead>
						                        <tr>
                                                    <th style='text-align: center; -webkit-border-radius: 4px !important; border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;'>Sócios {0} ({1})</th>
						                        </tr>
						                    </thead>
                                            <tbody>", pagante == "1" ? "Pagantes" : "Não Pagantes"
                                                    , conta.ToString());

                table.AppendFormat(@"{0}", tableTemp.ToString());

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                connection.Close();
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem sócios a apresentar.</div>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem sócios a apresentar: {0}</div>", exc.ToString());
        }

        connection.Close();
        return table.ToString();
    }


    [WebMethod]
    public static string loadSociosEstadoContratoMesSeguinte(string id_estado)
    {
        var table = new StringBuilder();
        var tableTemp = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY
                                            DECLARE @id_estado int = {0};
                                            SELECT
                                                NR_SOCIO,
                                                NOME,
                                                ESTADO,
                                                MES_ATUAL,
                                                ID_ESTADO
                                            FROM REPORT_ESTATISTICA_SOCIOS_ESTADO_PAGAMENTO_MES_SEGUINTE(@id_estado)", id_estado);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;
            string estado = "";
            int contaMesAtual = 0;
            Boolean flag = false;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    tableTemp.AppendFormat(@"   <tr>
                                                    <td style='width: 100%; {2}'>{0} - {1}</td>
                                                </tr>",
                                                myReader["NR_SOCIO"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["MES_ATUAL"].ToString() == "1" ? "color: blue;" : "color: #000;");

                    if (myReader["MES_ATUAL"].ToString() == "1")
                        contaMesAtual++;
                    else if (myReader["MES_ATUAL"].ToString() == "-1")
                        flag = true;

                    estado = myReader["ESTADO"].ToString();

                    conta++;
                }

                // Adiciona as linhas
                table.AppendFormat(@"   <input type='button' class='form-control' value='Exportar para Excel' style='width: 100%; height: auto; margin-bottom: 10px;' onclick='exportPopupTable();' />
                                        <table id='tablePopup' style='border: none !important; margin-bottom: 10px;'>
                                            <thead>
						                        <tr>
                                                    <th style='text-align: center; -webkit-border-radius: 4px !important; border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;'>Contratos {0} ({2}{1})</th>
						                        </tr>
						                    </thead>
                                            <tbody>", estado
                                                    , conta.ToString()
                                                    , flag == false ? contaMesAtual.ToString() + "/" : "");

                table.AppendFormat(@"{0}", tableTemp.ToString());

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                connection.Close();
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem contratos a apresentar.</div>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat("<div style='height:auto; background-color: #FFF;' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem contratos a apresentar: {0}</div>", exc.ToString());
        }

        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string loadSociosProfissoes(string rank)
    {
        var table = new StringBuilder();
        var tableTemp = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY
                                            declare @rank_value int = {0};

                                            select
						                        profissao,
	                                            rank_value,
	                                            NR_SOCIO,
	                                            ATIVO, 
	                                            NOME
	                                        from REPORT_ESTATISTICA_SOCIOS_PROFISSOES(@rank_value) rpt
                                            order by ativo desc, NR_SOCIO asc", rank);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;
            string profissao = "";

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    tableTemp.AppendFormat(@"   <tr>
                                                    <td style='width: 100%; {2}'>{0} - {1}<br /></td>
                                                </tr>",
                                                myReader["NR_SOCIO"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["ATIVO"].ToString() == "0" ? "color: red;" : "color: #000;");

                    conta++;

                    profissao = myReader["profissao"].ToString();
                }

                // Adiciona as linhas
                table.AppendFormat(@"   <input type='button' class='form-control' value='Exportar para Excel' style='width: 100%; height: auto; margin-bottom: 10px;' onclick='exportPopupTable();' />
                                        <table id='tablePopup' style='border: none !important; margin-bottom: 10px;'>
                                            <thead>
						                        <tr>
                                                    <th style='text-align: center; -webkit-border-radius: 4px !important; border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;'>Sócios {0} ({1})</th>
						                        </tr>
						                    </thead>
                                            <tbody>", profissao
                                                    , conta.ToString());

                table.AppendFormat(@"{0}", tableTemp.ToString());

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                connection.Close();
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem sócios a apresentar.</div>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem sócios a apresentar: {0}</div>", exc.ToString());
        }

        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string loadSociosSexo(string rank)
    {
        var table = new StringBuilder();
        var tableTemp = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY
                                            declare @rank_value int = {0};

                                            select
						                        SEXO,
	                                            NR_SOCIO,
	                                            ATIVO, 
	                                            NOME
	                                        from REPORT_ESTATISTICA_SOCIOS_SEXO(@rank_value) rpt
                                            order by ativo desc, NR_SOCIO asc", rank);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;
            string profissao = "";

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    tableTemp.AppendFormat(@"   <tr>
                                                    <td style='width: 100%; {2}'>{0} - {1}<br /></td>
                                                </tr>",
                                                myReader["NR_SOCIO"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["ATIVO"].ToString() == "0" ? "color: red;" : "color: #000;");

                    conta++;

                    profissao = myReader["SEXO"].ToString();
                }

                // Adiciona as linhas
                table.AppendFormat(@"   <input type='button' class='form-control' value='Exportar para Excel' style='width: 100%; height: auto; margin-bottom: 10px;' onclick='exportPopupTable();' />
                                        <table id='tablePopup' style='border: none !important; margin-bottom: 10px;'>
                                            <thead>
						                        <tr>
                                                    <th style='text-align: center; -webkit-border-radius: 4px !important; border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;'>Sócios {0} ({1})</th>
						                        </tr>
						                    </thead>
                                            <tbody>", profissao
                                                    , conta.ToString());

                table.AppendFormat(@"{0}", tableTemp.ToString());

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                connection.Close();
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem sócios a apresentar.</div>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span style='font-size:medium;margin: auto;color:#000'>Não existem sócios a apresentar: {0}</div>", exc.ToString());
        }

        connection.Close();
        return table.ToString();
    }
}