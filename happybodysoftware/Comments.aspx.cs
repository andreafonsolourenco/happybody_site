using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
//using System.Web.Mail;
using System.Net.Mail;

public partial class Comments : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "Comments"))
            {
                
            }
        }
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

            string sql = string.Format(@"   DECLARE @FILTRO varchar(MAX) = {0};

                                            SELECT COMENTARIO, APROVADO, LOCALIZACAO, ART, ID, DATA
                                            FROM REPORT_COMMENTS()
                                            WHERE (@FILTRO IS NULL OR (COMENTARIO LIKE '%' + @FILTRO + '%' OR LOCALIZACAO LIKE '%' + @FILTRO + '%'))
                                            ORDER BY ART DESC, LOCALIZACAO, DATA ASC", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

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
                                                    <th style='text-align: center; width: 75%;'>Comentário</th>
                                                    <th style='text-align: center; width: 15%;'>Aprovado</th>
                                                    <th style='text-align: center; width: 15%;'>Apagar</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr>
                                            <td style='width:75%'>{2}</td>
                                            <td style='display:none' id='id_{5}'>{0}</td>
                                            <td style='display:none' id='aprovado_{5}'>{6}</td>
                                            <td style='display:none' id='art_{5}'>{1}</td>
                                            <td style='width:15%; vertical-align:middle; text-align:center;'>
                                                <img id='approveComment{5}' src='img/icons/{4}' style='width:20px; height:20px; max-width: 20px; cursor: pointer; display:block; margin: 0 auto;' onclick='changeApproval({0},{5});'/>
                                            </td>
                                            <td style='width:15%; vertical-align:middle; text-align:center;' onclick='selectToDelete({5});'>
                                                <img id='delete_img_{5}' src='img/icons/not_approved.png' style='width:20px; height:20px; max-width: 20px; cursor: pointer; display:block; margin: 0 auto;'/>
                                            </td>
                                        </tr>",
                                                myReader["ID"].ToString(),
                                                myReader["ART"].ToString(),
                                                myReader["COMENTARIO"].ToString(),
                                                myReader["LOCALIZACAO"].ToString(),
                                                Convert.ToBoolean(myReader["APROVADO"].ToString()) == true ? "approved.png" : "not_approved.png",
                                                conta.ToString(),
                                                Convert.ToBoolean(myReader["APROVADO"].ToString()) == true ? "1" : "0");

                    conta++;
                }

                table.AppendFormat("</tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem comentários a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem comentários a apresentar.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string update(string artigo, string id, string aprovado)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @artigo bit = {0};
                                            DECLARE @id int = {1};
                                            DECLARE @aprovado bit = {2};
                                            DECLARE @error int;
                                            DECLARE @errorMsg varchar(max);

                                            EXEC HB_APPROVE_COMMENTS @id, @aprovado, @artigo, @error OUTPUT, @errorMsg OUTPUT

                                            select @error as error, @errorMsg as errorMsg", artigo, id, aprovado);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"{0}<#SEP#>{1}",
                                                myReader["error"].ToString(),
                                                myReader["errorMsg"].ToString());
                }
            }
            else
            {
                connection.Close();
                table.AppendFormat(@"{0}<#SEP#>{1}", "-1", "Ocorreu um erro ao atualizar a aprovação do comentário");
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}<#SEP#>{1}", "-999", exc.ToString());
        }

        return table.ToString();
    }

    [WebMethod]
    public static string deleteComentario(string id_operador, string id, string artigo)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_comentario int = {0};
                                            DECLARE @id_operador int = {1};
                                            DECLARE @artigo bit = {2};
                                            DECLARE @res int;
                                            DECLARE @path varchar(max);

                                            EXEC DELETE_COMENTARIO @id_comentario, @id_operador, @artigo, @res output

                                            SELECT @res as res", id, id_operador, artigo);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) < 0)
                    {
                        table.AppendFormat(@"Erro ao apagar comentário!");
                    }
                    else
                    {
                        table.AppendFormat(@"Comentário apagado com sucesso!");
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao apagar comentário!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao apagar comentário!");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao apagar comentário!");
        connection.Close();
        return table.ToString();
    }
}
