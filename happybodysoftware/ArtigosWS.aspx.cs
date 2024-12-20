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

public partial class ArtigosWS : Page
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

            string sql = string.Format(@"   DECLARE @id_artigo int;
                                            DECLARE @FILTRO varchar(MAX) = {0};

                                            SELECT 
                                                ID_ARTIGO,
	                                            TITULO,
	                                            TITULO_EN,
	                                            TITULO_ES,
	                                            TITULO_FR,
	                                            TEXTO,
	                                            TEXTO_EN,
	                                            TEXTO_ES,
	                                            TEXTO_FR,
	                                            ORDEM,
	                                            LINK,
	                                            DATA,
	                                            AUTOR
                                            FROM REPORT_ARTIGOS_WS(@id_artigo)
                                            WHERE (@FILTRO IS NULL OR (TITULO LIKE '%' + @FILTRO + '%'))
                                            ORDER BY ID_ARTIGO asc", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

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
                                                    <th style='text-align: center; width: 100%;'>Artigos</th>
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
                                                myReader["ID_ARTIGO"].ToString(),
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
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem artigos a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem artigos a apresentar.</div>");
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

            string sql = string.Format(@"   DECLARE @id_artigo int = {0};

                                            SELECT 
                                                ID_ARTIGO,
	                                            TITULO,
	                                            TITULO_EN,
	                                            TITULO_ES,
	                                            TITULO_FR,
	                                            TEXTO,
	                                            TEXTO_EN,
	                                            TEXTO_ES,
	                                            TEXTO_FR,
	                                            ORDEM,
	                                            LINK,
	                                            DATA,
	                                            AUTOR,
                                                (SELECT SERVIDOR_SITE FROM APPLICATION_CONFIG) AS SERVIDOR
                                            FROM REPORT_ARTIGOS_WS(@id_artigo)", id);

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

                                                <input id='btnDelete' value='Apagar' runat='server' type='button' onclick='deleteService();' style='background-color: #4282b5; 
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
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Texto:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Inglês:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Espanhol:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Francês:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Ordem:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Data:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Autor:</div>
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
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto' name='texto' placeholder='Texto' style='width: 100%; margin: auto; height: 100%;' rows='5'>{5}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto_en' name='texto_en' placeholder='Texto Inglês' style='width: 100%; margin: auto; height: 100%;' rows='5'>{6}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto_es' name='texto_es' placeholder='Texto Espanhol' style='width: 100%; margin: auto; height: 100%;' rows='5'>{7}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto_fr' name='texto_fr' placeholder='Texto Francês' style='width: 100%; margin: auto; height: 100%;' rows='5'>{8}</textarea>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='number' class='form-control' id='ordem' name='ordem' placeholder='Ordem' style='width: 100%; margin: auto; height: 100%;' value='{9}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='data' name='data' placeholder='Data' style='width: 100%; margin: auto; height: 100%;' value='{11}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='autor' name='autor' placeholder='Autor' style='width: 100%; margin: auto; height: 100%;' value='{12}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px' id='divImages'></div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px; text-align: center;'>
                                                        <img src='../{13}img/artigos/{10}' style='height: 100%; width: auto' id='imgArtigo'/>
                                                    </div>
                                                    <span class='variaveis' id='imgName'>{10}</span>
                                                </div>
                                            </div>",
                                                myReader["ID_ARTIGO"].ToString(),
                                                myReader["TITULO"].ToString(),
                                                myReader["TITULO_EN"].ToString(),
                                                myReader["TITULO_ES"].ToString(),
                                                myReader["TITULO_FR"].ToString(),
                                                myReader["TEXTO"].ToString(),
                                                myReader["TEXTO_EN"].ToString(),
                                                myReader["TEXTO_ES"].ToString(),
                                                myReader["TEXTO_FR"].ToString(),
                                                myReader["ORDEM"].ToString(),
                                                myReader["LINK"].ToString(),
                                                myReader["DATA"].ToString(),
                                                myReader["AUTOR"].ToString(),
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
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para este artigo.</span>
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
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para este artigo.</span>
                                        </div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string editar(string id_operador, string id_artigo, string titulo, string titulo_en, string titulo_es, string titulo_fr,
        string texto, string texto_en, string texto_es, string texto_fr, string imagem, string ordem, string data, string autor)
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

        texto = texto.Replace("''", "\"");
        texto = texto.Replace("'", "''");

        texto_en = texto_en.Replace("''", "\"");
        texto_en = texto_en.Replace("'", "''");

        texto_es = texto_es.Replace("''", "\"");
        texto_es = texto_es.Replace("'", "''");

        texto_fr = texto_fr.Replace("''", "\"");
        texto_fr = texto_fr.Replace("'", "''");

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @id_artigo int = {1};
	                                        DECLARE @designacao varchar(500) = '{2}';
	                                        DECLARE @designacao_en varchar(500) = '{3}';
                                            DECLARE @designacao_es varchar(500) = '{4}';
                                            DECLARE @designacao_fr varchar(500) = '{5}';
                                            DECLARE @texto varchar(max) = '{6}';
                                            DECLARE @texto_en varchar(max) = '{7}';
                                            DECLARE @texto_es varchar(max) = '{8}';
                                            DECLARE @texto_fr varchar(max) = '{9}';
                                            DECLARE @imagem varchar(max) = '{10}';
                                            DECLARE @ordem int = {11};
                                            DECLARE @data varchar(100) = '{12}';
                                            DECLARE @autor varchar(500) = '{13}';
                                            DECLARE @res int;

                                            EXEC ALTERA_ARTIGO @id_operador, @id_artigo, @designacao, @designacao_en, @designacao_es, @designacao_fr, 
                                            @texto, @texto_en, @texto_es, @texto_fr, @ordem, @imagem, @data, @autor, @res output

                                            SELECT @res as res",
                                                id_operador, id_artigo, titulo, titulo_en, titulo_es, titulo_fr, texto, texto_en, texto_es, texto_fr, imagem, ordem, data, autor);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) < 0)
                    {
                        table.AppendFormat(@"Erro ao atualizar artigo {0}!", titulo);
                    }
                    else
                    {
                        table.AppendFormat(@"Artigo {0} atualizado com sucesso!", titulo);
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao atualizar artigo {0}!", titulo);
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
    public static string criar(string id_operador, string titulo, string titulo_en, string titulo_es, string titulo_fr,
        string texto, string texto_en, string texto_es, string texto_fr, string imagem, string ordem, string data, string autor)
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

        texto = texto.Replace("''", "\"");
        texto = texto.Replace("'", "''");

        texto_en = texto_en.Replace("''", "\"");
        texto_en = texto_en.Replace("'", "''");

        texto_es = texto_es.Replace("''", "\"");
        texto_es = texto_es.Replace("'", "''");

        texto_fr = texto_fr.Replace("''", "\"");
        texto_fr = texto_fr.Replace("'", "''");

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @designacao varchar(500) = '{1}';
	                                        DECLARE @designacao_en varchar(500) = '{2}';
                                            DECLARE @designacao_es varchar(500) = '{3}';
                                            DECLARE @designacao_fr varchar(500) = '{4}';
                                            DECLARE @texto varchar(max) = '{5}';
                                            DECLARE @texto_en varchar(max) = '{6}';
                                            DECLARE @texto_es varchar(max) = '{7}';
                                            DECLARE @texto_fr varchar(max) = '{8}';
                                            DECLARE @imagem varchar(max) = '{9}';
                                            DECLARE @ordem int = {10};
                                            DECLARE @data varchar(100) = '{11}';
                                            DECLARE @autor varchar(500) = '{12}';
                                            DECLARE @id_artigo int;

                                            EXEC CRIA_ARTIGO @id_operador, @designacao, @designacao_en, @designacao_es, @designacao_fr, 
                                            @texto, @texto_en, @texto_es, @texto_fr, @ordem, @imagem, @data, @autor, @id_artigo output

                                            SELECT @id_artigo as res",
                                                id_operador, titulo, titulo_en, titulo_es, titulo_fr, texto, texto_en, texto_es, texto_fr, imagem, ordem, data, autor);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) < 0)
                    {
                        table.AppendFormat(@"Erro ao criar artigo {0}!", titulo);
                    }
                    else
                    {
                        table.AppendFormat(@"Artigo {0} criado com sucesso!", titulo);
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao criar artigo {0}!", titulo);
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao criar artigo {0}!", titulo);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao criar artigo {0}!", titulo);
        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string DeleteArtigo(string id_operador, string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_artigo int = {0};
                                            DECLARE @id_operador int = {1};
                                            DECLARE @res int;

                                            EXEC APAGA_ARTIGO @id_artigo, @id_operador, @res OUTPUT

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
                        table.AppendFormat(@"Erro ao apagar artigo!");
                    }
                    else
                    {
                        table.AppendFormat(@"Artigo apagado com sucesso!");
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao apagar artigo!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao apagar artigo!");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao apagar artigo!");
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

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server + "img/artigos/"))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}' {1}>{0}</option>", info.Name, info.Name == imgDefault ? "selected" : "");
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFiles'>{0}</span>", "..//" + server + "img//artigos//");

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

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server + "img/artigos/"))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}'>{0}</option>", info.Name);
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFilesNew'>{0}</span>", "..//" + server + "img//artigos//");
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            connection.Close();
        }

        divImgNew.InnerHtml = table.ToString();
    }
}
