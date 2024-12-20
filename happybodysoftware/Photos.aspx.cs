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
using System.IO;

public partial class Photos : Page
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

                                            SELECT LINK, CAST(APROVADO as INT) as APROVADO, WS_FOTOSID as ID, (SELECT SERVIDOR_SITE FROM APPLICATION_CONFIG) as CONFIG
                                            FROM WS_FOTOS", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

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
                                                    <th style='text-align: center; width: 75%;'>Foto</th>
                                                    <th style='text-align: center; width: 15%;'>Aprovado</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr onclick='changeTo({3});' >
                                            <td style='width:75%'><img id='photo_{3}' src='../{5}/img/fotos_carregadas/{1}' style='width:50%; height:auto; display:block; margin: 0 auto;'/></td>
                                            <td style='display:none' id='id_{3}'>{0}</td>
                                            <td style='display:none' id='aprovado_{3}'>{4}</td>
                                            <td style='width:15%; vertical-align:middle; text-align:center;'><img id='img_{3}' src='{2}' style='width:auto; height:auto; max-width: 50px; cursor: pointer; display:block; margin: 0 auto;'/></td>
                                            <td style='width: 10%; text-align: center;'>
                                                <input id='btnApagar' value='Apagar' runat='server' type='button' onclick='deletePhoto({0}, {3});' style='background-color: #4282b5; float: left;
                                                        width: 100%; height: auto; font-size: 12pt; text-align: center; color: #FFFFFF; height: 30px; line-height: 30px; 
                                                        cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; 
                                                        -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                            </td>
                                        </tr>",
                                                myReader["ID"].ToString(),
                                                myReader["LINK"].ToString(),
                                                myReader["APROVADO"].ToString() == "1" ? "img/icons/on.jpg" : "img/icons/off.jpg",
                                                conta.ToString(),
                                                myReader["APROVADO"].ToString(),
                                                myReader["CONFIG"].ToString());

                    conta++;
                }

                table.AppendFormat("</tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem fotografias a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem fotografias a apresentar.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string update(string id_operador, string id, string aprovado)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id int = {0};
                                            DECLARE @aprovado bit = {1};
                                            DECLARE @id_operador int = {2};
                                            DECLARE @codop char(30) = (select ltrim(rtrim(codigo)) from operadores where operadoresid = @id_operador)

                                            UPDATE WS_FOTOS
                                            SET APROVADO = @aprovado, ctrldata = getdate(), ctrlcodop = @codop
                                            WHERE WS_FOTOSID = @id", id, aprovado, id_operador);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                connection.Close();
                return "";
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
            return "";
        }
    }

    [WebMethod]
    public static string deletePhoto(string id_operador, string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_foto int = {0};
                                            DECLARE @id_operador int = {1};
                                            DECLARE @res int;
                                            DECLARE @path varchar(max);

                                            EXEC DELETE_FOTO @id_foto, @id_operador, @res output, @path output

                                            SELECT @res as res, @path as path", id, id_operador);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) < 0)
                    {
                        table.AppendFormat(@"Erro ao apagar CV!");
                    }
                    else
                    {
                        if (File.Exists((HttpContext.Current.Server.MapPath("~") + myReader["path"].ToString())))
                            File.Delete((HttpContext.Current.Server.MapPath("~") + myReader["path"].ToString()));

                        table.AppendFormat(@"CV apagado com sucesso!");
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao apagar CV!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao apagar CV!");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao apagar CV!");
        connection.Close();
        return table.ToString();
    }
}
