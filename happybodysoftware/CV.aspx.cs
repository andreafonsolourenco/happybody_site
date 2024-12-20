using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.IO;

public partial class CV : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "ServicosWS"))
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

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @FILTRO varchar(MAX) = {0};
                                            DECLARE @id int;

                                            SELECT 
                                                ID_CANDIDATURA,
                                                NOME,
                                                EMAIL,
                                                TLF,
                                                TEXTO,
                                                EXTENSAO,
                                                TIPO,
                                                DATA_ENVIO,
                                                LIDO
                                            FROM HB_SOFTWARE_REPORT_WS_CANDIDATURAS(@id, @FILTRO)
                                            ORDER BY LIDO asc, DATA_ENVIO desc, TIPO", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

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
                                                    <th style='text-align: center; width: 90%;' colspan='3'>Candidatura</th>
                                                    <th style='text-align: center; width: 10%;'>Visto</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick='openServiceSelected({0}, {2});'>
                                            <td style='display:none;' id='id_{2}'>{0}</td>
                                            <td style='width:40%;'>{1}</td>
                                            <td style='width:25%;'>{3}</td>
                                            <td style='width:25%;'>{4}</td>
                                            <td style='width:10%; vertical-align:middle; text-align:center;'><img id='img_{2}' src='{5}' style='width:auto; height:auto; max-width: 50px; cursor: pointer; display:block; margin: 0 auto;'/></td>
                                        </tr>",
                                                myReader["ID_CANDIDATURA"].ToString(),
                                                myReader["NOME"].ToString(),
                                                conta.ToString(),
                                                myReader["DATA_ENVIO"].ToString(),
                                                myReader["TIPO"].ToString(),
                                                myReader["LIDO"].ToString() == "1" ? "img/icons/on.jpg" : "img/icons/off.jpg");

                    conta++;
                }

                table.AppendFormat("</tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem candidaturas a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem candidaturas a apresentar.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string getServico(string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id int = {0};
                                            DECLARE @filtro varchar(max);
                                            DECLARE @serverpath varchar(max) = (SELECT SERVIDOR_SITE FROM APPLICATION_CONFIG)
                                            
                                            UPDATE WS_CANDIDATURAS
                                            SET LIDA = 1
                                            WHERE WS_CANDIDATURASID = @id

                                            SELECT 
                                                ID_CANDIDATURA,
                                                NOME,
                                                EMAIL,
                                                TLF,
                                                TEXTO,
                                                EXTENSAO,
                                                TIPO,
                                                DATA_ENVIO,
                                                LIDO,
                                                @serverpath as SERVIDOR
                                            FROM HB_SOFTWARE_REPORT_WS_CANDIDATURAS(@id, @filtro)", id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:left; margin-top: 5px; margin-bottom: 10px;'>
                                                <input id='btnApagar' value='Apagar' runat='server' type='button' onclick='deleteCV();' style='background-color: #4282b5; float: left;
                                                        width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; 
                                                        cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; 
                                                        -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                                <img src='img/icons/icon_close.png' style='cursor:pointer;' onclick='closeServiceInfo();'/>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: 'Noto Sans', sans-serif !important; height:100%'>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px; line-height: 50px;'>Nome:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px; line-height: 50px;'>Email:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px; line-height: 50px;'>Telefone:</div>
                                                    <div style='height:200px; width:100%; margin-bottom: 10px; line-height: 200px;'>Texto:</div>
                                                </div>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' style='width: 100%; margin: auto; height: 100%;' value='{0}' readonly/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' style='width: 100%; margin: auto; height: 100%;' value='{1}' readonly/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' style='width: 100%; margin: auto; height: 100%;' value='{2}' readonly/>
                                                    </div>
                                                    <div style='height:200px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' rows='10' style='width: 100%; margin: auto; height: 100%;' readonly >{3}</textarea>
                                                    </div>
                                                </div>
                                                <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>
                                                    <embed src='../{5}/curriculos_enviados/{4}' type='application/pdf' height='1000px' width='100%'>
                                                </div>
                                            </div>",
                                                myReader["NOME"].ToString(),
                                                myReader["EMAIL"].ToString(),
                                                myReader["TLF"].ToString(),
                                                myReader["TEXTO"].ToString(),
                                                myReader["ID_CANDIDATURA"].ToString() + myReader["EXTENSAO"].ToString(),
                                                myReader["SERVIDOR"].ToString());
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
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para esta candidatura.</span>
                                        </div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                            <img src='img/icon_close.png' style='cursor:pointer;' onclick='closeCustomerInfo();'/>
                                        </div>
                                        <div style='height:auto' class='panel-heading' id='panel-heading'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para esta candidatura.</span>
                                        </div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string deleteCV(string id_operador, string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_cv int = {0};
                                            DECLARE @id_operador int = {1};
                                            DECLARE @res int;
                                            DECLARE @path varchar(max);

                                            EXEC DELETE_CANDIDATURA @id_cv, @id_operador, @res output, @path output

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
