using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Informacoes : Page
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
    public static string load(string data, string filtro)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = "";
            string titulo = "";

            switch (data)
            {
                case "0":
                    sql = string.Format(@"  SET DATEFORMAT dmy;
                                            DECLARE @filtro varchar(max) = {0};
                                        
                                            SELECT distinct 
                                                nr_socio, nome
                                            FROM REPORT_CONTRATOS_DENTRO_DATA(NULL, NULL) rpt
                                            INNER JOIN SOCIOS soc on soc.sociosid = rpt.id_socio
                                            INNER JOIN ESTADOS_CONTRATO st on st.ESTADOS_CONTRATOID = rpt.id_estado
                                            WHERE rpt.id_status_pagamento = 0
                                            and st.codigo not in ('INACTIVO', 'CANC_FALTA_PAG', 'CANC_PAGO')
                                            and (@filtro is null or soc.nr_socio like '%' + @filtro + '%' or soc.nome like '%' + @filtro + '%')
                                            ORDER BY soc.NR_SOCIO", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

                    titulo = "Sem Estado de Pagamento";
                    break;
                case "1":
                    sql = string.Format(@"  SET DATEFORMAT dmy;
                                            DECLARE @filtro varchar(max) = {0};
                                        
                                            SELECT distinct
                                                nr_socio, nome
                                            FROM REPORT_CONTRATOS_DENTRO_DATA(NULL, NULL) rpt
                                            INNER JOIN SOCIOS soc on soc.sociosid = rpt.id_socio
                                            INNER JOIN PAGAMENTOS_STATUS pagst on pagst.PAGAMENTOS_STATUSID = rpt.id_status_pagamento
                                            INNER JOIN ESTADOS_CONTRATO st on st.ESTADOS_CONTRATOID = rpt.id_estado
                                            WHERE pagst.codigo IN ('NAO-PAGO', 'NAO_PAGO_NAORENOV')
                                            and st.codigo not in ('INACTIVO', 'CANC_FALTA_PAG', 'CANC_PAGO')
                                            and (@filtro is null or soc.nr_socio like '%' + @filtro + '%' or soc.nome like '%' + @filtro + '%')
                                            ORDER BY soc.NR_SOCIO", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

                    titulo = "Não Pagos";
                    break;
                case "2":
                    sql = string.Format(@"  SET DATEFORMAT dmy;
                                            DECLARE @filtro varchar(max) = {0};
                                        
                                            SELECT distinct
                                                nr_socio, nome
                                            FROM REPORT_CONTRATOS_DENTRO_DATA(NULL, NULL) rpt
                                            INNER JOIN SOCIOS soc on soc.sociosid = rpt.id_socio
                                            INNER JOIN CONTRATO cont on cont.contratoid = rpt.id_contrato
                                            INNER JOIN ESTADOS_CONTRATO st on st.ESTADOS_CONTRATOID = rpt.id_estado
                                            WHERE cont.debito_direto = 1
                                            and st.codigo not in ('INACTIVO', 'CANC_FALTA_PAG', 'CANC_PAGO')
                                            and (@filtro is null or soc.nr_socio like '%' + @filtro + '%' or soc.nome like '%' + @filtro + '%')
                                            ORDER BY soc.NR_SOCIO", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

                    titulo = "Débitos Diretos";
                    break;
                case "3":
                    sql = string.Format(@"  SET DATEFORMAT dmy;
                                            DECLARE @filtro varchar(max) = {0};
                                        
                                            SELECT distinct
                                                nr_socio, nome
                                            FROM REPORT_CONTRATOS_DENTRO_DATA(NULL, NULL) rpt
                                            INNER JOIN SOCIOS soc on soc.sociosid = rpt.id_socio
                                            INNER JOIN CONTRATO cont on cont.contratoid = rpt.id_contrato
                                            INNER JOIN ESTADOS_CONTRATO st on st.ESTADOS_CONTRATOID = rpt.id_estado
                                            WHERE cont.debito_direto = 0
                                            and st.codigo not in ('INACTIVO', 'CANC_FALTA_PAG', 'CANC_PAGO')
                                            and (@filtro is null or soc.nr_socio like '%' + @filtro + '%' or soc.nome like '%' + @filtro + '%')
                                            ORDER BY soc.NR_SOCIO", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));
                    titulo = "Pagamentos ao Balcão";
                    break;
            }
            //return sql;


            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerTable'>{0}</th>
						                        </tr>
						                    </thead>
                                            <tbody>", titulo);

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr>
                                            <td>
                                                {0} - {1}
                                            </td>
                                        </tr>",
                                                myReader["nr_socio"].ToString(),
                                                myReader["nome"].ToString());
                }

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                connection.Close();
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem dados a apresentar.</div>");
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Erro ao carregar registos: {0}</div>", exc.ToString());
        }

        return table.ToString();
    }
}
