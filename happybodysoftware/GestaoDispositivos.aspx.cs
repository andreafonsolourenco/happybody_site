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

public partial class GestaoDispositivos : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "GestaoDispositivos"))
            {
                
            }
        }
    }

    [WebMethod]
    public static string loadGrid(string filtro)
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
                                            DECLARE @filtro varchar(max) = {0}
                                        
                                            SELECT 
                                                GESTAO_DISPOSITIVOSID,
                                                disp.NOME,
                                                IP,
                                                ANDROID_ID,
                                                CAST(disp.ATIVO as INT) as ATIVO,
                                                isnull(op.nome, '') as OP,
                                                case when data_ativacao is null then (CONVERT(VARCHAR(10), DATEADD(hh, -1, data_desativacao), 103) + ' ' + CONVERT(VARCHAR(10), DATEADD(hh, -1, data_desativacao), 108))
                                                else (CONVERT(VARCHAR(10), DATEADD(hh, -1, data_ativacao), 103) + ' ' + CONVERT(VARCHAR(10), DATEADD(hh, -1, data_ativacao), 108))
                                                end as data_ativacao_desativacao
                                            FROM GESTAO_DISPOSITIVOS disp
                                            left join operadores op on op.operadoresid = disp.id_op_ativacao
                                            WHERE @filtro is null
                                            or disp.IP like '%' + @filtro + '%'
                                            or disp.NOME like '%' + @filtro + '%'
                                            or op.NOME like '%' + @filtro + '%'
                                            ORDER BY disp.CTRLDATA DESC", string.IsNullOrEmpty(filtro) ? "NULL" : "'" + filtro + "'");

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerColspan' colspan='2'>DISPOSITIVOS</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr style='cursor:pointer;' ondblclick='deleteDispositivo({0});'>
                                            <td style='width:80%;'>{1}<br />ID: {3}{6}</td>
                                            <td style='width:20%;'><input type='checkbox' id='ativo_{5}' class='form-control' {4} onclick='ativar({0}, {5});'/></td>
                                        </tr>",
                                                oDs.Tables[0].Rows[i]["GESTAO_DISPOSITIVOSID"].ToString(),
                                                oDs.Tables[0].Rows[i]["NOME"].ToString(),
                                                oDs.Tables[0].Rows[i]["IP"].ToString(),
                                                oDs.Tables[0].Rows[i]["ANDROID_ID"].ToString(),
                                                oDs.Tables[0].Rows[i]["ATIVO"].ToString() == "1" ? " checked " : "",
                                                i.ToString(),
                                                string.IsNullOrEmpty(oDs.Tables[0].Rows[i]["OP"].ToString()) ? "" : "<br />Ativado/Desativado Por: " + oDs.Tables[0].Rows[i]["OP"].ToString() + "<br />" + oDs.Tables[0].Rows[i]["data_ativacao_desativacao"].ToString());
                }

                table.AppendFormat("</tbody></table>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", oDs.Tables[0].Rows.Count.ToString());
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='tableGrid'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem dispositivos a apresentar</div>");
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='tableGrid'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Erro ao carregar aniversários: {0}</div>", exc.ToString());
        }

        return table.ToString();
    }

    [WebMethod]
    public static string ativarDesativar(string id, string ativo, string operatorID)
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
                                            DECLARE @id int = {0};
                                            DECLARE @id_op int = {1};
                                            DECLARE @ativo bit = {2};
                                            DECLARE @ret int;
                                        
                                            EXEC ATIVA_DESATIVA_DISPOSITIVO @id, @id_op, @ativo, @ret output
                                            SELECT @ret as ret", id, operatorID, ativo);


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
                    table.AppendFormat(@"{0}", oDs.Tables[0].Rows[i]["ret"].ToString());
                }
            }
            else
            {
                table.AppendFormat("-1");
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("-1");
        }

        return table.ToString();
    }

    [WebMethod]
    public static string apagarDispositivo(string id)
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
                                            DECLARE @id int = {0};
                                        
                                            DELETE FROM GESTAO_DISPOSITIVOS WHERE GESTAO_DISPOSITIVOSID = @id
                                            SELECT 0 as ret", id);


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
                    table.AppendFormat(@"Dispositivo apagado com sucesso!");
                }
            }
            else
            {
                table.AppendFormat("Ocorreu um erro ao apagar o dispositivo selecionado!");
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("Ocorreu um erro ao apagar o dispositivo selecionado!");
        }

        return table.ToString();
    }
}
