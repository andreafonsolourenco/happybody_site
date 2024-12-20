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

public partial class EventosWS : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "EventosWS"))
            {
                
            }
        }

        loadImg();
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

            string sql = string.Format(@"   DECLARE @id_evento int;
                                            DECLARE @FILTRO varchar(MAX) = {0};

                                            SELECT 
                                                ID_EVENTO,
	                                            TITULO,
	                                            TITULO_EN,
	                                            TITULO_ES,
	                                            TITULO_FR,
	                                            FOTO_CAPA,
	                                            ATIVO
                                            FROM REPORT_EVENTOS_WS(@id_evento)
                                            WHERE (@FILTRO IS NULL OR (TITULO LIKE '%' + @FILTRO + '%'))
                                            ORDER BY ID_EVENTO, TITULO", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

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
                                                    <th style='text-align: center; width: 100%;'>Eventos</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick='openServiceSelected({0}, {2});'>
                                            <td style='display:none;' id='id_{2}'>{0}</td>
                                            <td class='tbodyTrTd'>{1}</td>
                                        </tr>",
                                                myReader["ID_EVENTO"].ToString(),
                                                myReader["TITULO"].ToString(),
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
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem eventos a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem eventos a apresentar. {0}</div>", exc.ToString());
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

            string sql = string.Format(@"   DECLARE @id_evento int = {0};

                                            SELECT 
                                                ID_EVENTO,
	                                            TITULO,
	                                            TITULO_EN,
	                                            TITULO_ES,
	                                            TITULO_FR,
	                                            FOTO_CAPA,
	                                            CAST(ATIVO as INT) as ATIVO,
                                                (SELECT SERVIDOR_SITE FROM APPLICATION_CONFIG) AS SERVIDOR
                                            FROM REPORT_EVENTOS_WS(@id_evento)", id);

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

                                                <input id='btnDelete' value='Apagar' runat='server' type='button' onclick='deleteEvento();' style='background-color: #4282b5; 
                                                width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                                <img src='img/icons/icon_close.png' style='cursor:pointer;' onclick='closeServiceInfo();'/>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: 'Noto Sans', sans-serif !important; height:100%'>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Título:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Tradução Inglês:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Tradução Espanhol:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Tradução Francês:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Ativo:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Imagem:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'></div>
                                                </div>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='titulo' name='titulo' placeholder='Título' style='width: 100%; margin: auto; height: 100%;' value='{1}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='titulo_en' name='titulo_en' placeholder='Título Inglês' style='width: 100%; margin: auto; height: 100%;' value='{2}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='titulo_es' name='titulo_es' placeholder='Título Espanhol' style='width: 100%; margin: auto; height: 100%;' value='{3}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='titulo_fr' name='titulo_fr' placeholder='Título Francês' style='width: 100%; margin: auto; height: 100%;' value='{4}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='ativo' name='ativo' style='width: 100%; margin: auto; height: 100%;' {6}/>
                                                    </div>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px' id='divImages'></div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px; text-align: center;'>
                                                        <img src='../{7}img/eventos/{5}' style='height: 100%; width: auto' id='imgEvento'/>
                                                    </div>
                                                    <span class='variaveis' id='imgName'>{5}</span>
                                                </div>
                                            </div>",
                                                myReader["ID_EVENTO"].ToString(),
                                                myReader["TITULO"].ToString(),
                                                myReader["TITULO_EN"].ToString(),
                                                myReader["TITULO_ES"].ToString(),
                                                myReader["TITULO_FR"].ToString(),
                                                myReader["FOTO_CAPA"].ToString(),
                                                myReader["ATIVO"].ToString() == "1" ? " checked " : "",
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
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para este evento.</span>
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
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para este evento. {0}</span>
                                        </div>", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string editar(string id_operador, string id_evento, string titulo, string titulo_en, string titulo_es, string titulo_fr, string ativo, string imagem)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        titulo = titulo.Replace("''", "\"");
        titulo = titulo.Replace("'", "''");

        titulo_en = titulo_en.Replace("''", "\"");
        titulo_en = titulo_en.Replace("'", "''");

        titulo_es = titulo_es.Replace("''", "\"");
        titulo_es = titulo_es.Replace("'", "''");

        titulo_fr = titulo_fr.Replace("''", "\"");
        titulo_fr = titulo_fr.Replace("'", "''");

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @id_evento int = {1};
	                                        DECLARE @designacao varchar(500) = '{2}';
	                                        DECLARE @designacao_en varchar(500) = '{3}';
                                            DECLARE @designacao_es varchar(500) = '{4}';
                                            DECLARE @designacao_fr varchar(500) = '{5}';
                                            DECLARE @ativo bit = {6};
                                            DECLARE @imagem varchar(max) = '{7}';
                                            DECLARE @res int;

                                            EXEC ALTERA_EVENTO @id_operador, @id_evento, @designacao, @designacao_en, @designacao_es, @designacao_fr, @ativo, @imagem, @res output

                                            SELECT @res as res",
                                                id_operador, id_evento, titulo, titulo_en, titulo_es, titulo_fr, ativo, imagem);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) < 0)
                    {
                        table.AppendFormat(@"Erro ao atualizar evento {0}!", titulo);
                    }
                    else
                    {
                        table.AppendFormat(@"Evento {0} atualizado com sucesso!", titulo);
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao atualizar evento {0}!", titulo);
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao atualizar artigo {0}!", titulo);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao atualizar artigo {0}!", titulo);
        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string criar(string id_operador, string titulo, string titulo_en, string titulo_es, string titulo_fr, string ativo, string imagem)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        titulo = titulo.Replace("''", "\"");
        titulo = titulo.Replace("'", "''");

        titulo_en = titulo_en.Replace("''", "\"");
        titulo_en = titulo_en.Replace("'", "''");

        titulo_es = titulo_es.Replace("''", "\"");
        titulo_es = titulo_es.Replace("'", "''");

        titulo_fr = titulo_fr.Replace("''", "\"");
        titulo_fr = titulo_fr.Replace("'", "''");

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @designacao varchar(500) = '{1}';
	                                        DECLARE @designacao_en varchar(500) = '{2}';
                                            DECLARE @designacao_es varchar(500) = '{3}';
                                            DECLARE @designacao_fr varchar(500) = '{4}';
                                            DECLARE @ativo bit = {5};
                                            DECLARE @imagem varchar(max) = '{6}';
                                            DECLARE @id_evento int;

                                            EXEC CRIA_EVENTO @id_operador, @designacao, @designacao_en, @designacao_es, @designacao_fr, @ativo, @imagem, @id_evento output

                                            SELECT @id_evento as res",
                                                id_operador, titulo, titulo_en, titulo_es, titulo_fr, ativo, imagem);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) < 0)
                    {
                        table.AppendFormat(@"Erro ao criar evento {0}!", titulo);
                    }
                    else
                    {
                        table.AppendFormat(@"Evento {0} criado com sucesso!", titulo);
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao criar evento {0}!", titulo);
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao criar evento {0}!", titulo);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao criar evento {0}!", titulo);
        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string DeleteEvento(string id_operador, string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_evento int = {0};
                                            DECLARE @id_operador int = {1};
                                            DECLARE @res int;

                                            EXEC APAGA_EVENTO @id_evento, @id_operador, @res OUTPUT

                                            SELECT @res as res", id, id_operador);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) < 0)
                    {
                        table.AppendFormat(@"Erro ao apagar evento!");
                    }
                    else
                    {
                        table.AppendFormat(@"Evento apagado com sucesso!");
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao apagar evento!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao apagar evento!");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao apagar evento!");
        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string loadImages(string imgDefault)
    {
        var table = new StringBuilder();
        string server = "";
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT 
                                                SERVIDOR_SITE
                                            FROM APPLICATION_CONFIG");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    server = myReader["SERVIDOR_SITE"].ToString();
                }
                connection.Close();
            }
            else
            {
                server = "";
                connection.Close();
            }

            table.AppendFormat(@"<select class='form-control' id='selectImg' style='width:100%; height: 40px; font-size: small; float: left;' onchange='changeImg();'>");

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server + "img/eventos/"))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}' {1}>{0}</option>", info.Name, info.Name == imgDefault ? "selected" : "");
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFiles'>{0}</span>", "..//" + server + "img//eventos//");

            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            connection.Close();
        }

        return table.ToString();
    }

    private void loadImg()
    {
        var table = new StringBuilder();
        string server = "";
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT 
                                                SERVIDOR_SITE
                                            FROM APPLICATION_CONFIG");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    server = myReader["SERVIDOR_SITE"].ToString();
                }
                connection.Close();
            }
            else
            {
                server = "";
                connection.Close();
            }

            table.AppendFormat(@"<select class='form-control' id='selectImgNew' style='width:100%; height: 40px; font-size: small; float: left;' onchange='changeImgNew();'>");
            table.AppendFormat(@"<option value=''>Selecione uma imagem</option>");

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server + "img/eventos/"))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}'>{0}</option>", info.Name);
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFilesNew'>{0}</span>", "..//" + server + "img//eventos//");
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            connection.Close();
        }

        divImgNew.InnerHtml = table.ToString();
    }
}
