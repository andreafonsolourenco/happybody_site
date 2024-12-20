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

public partial class ModalidadesWS : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "ModalidadesWS"))
            {
                
            }
        }

        loadImg1();
        loadImg2();
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

            string sql = string.Format(@"   DECLARE @id_modalidade int;
                                            DECLARE @FILTRO varchar(MAX) = {0};

                                            SELECT 
                                                ID_MODALIDADE,
	                                            CODIGO,
	                                            TITULO,
	                                            TITULO_EN,
	                                            TITULO_ES,
	                                            TITULO_FR,
	                                            TEXTO,
	                                            TEXTO_EN,
	                                            TEXTO_ES,
	                                            TEXTO_FR,
	                                            VIDEO,
	                                            FOTO,
                                                ICON,
                                                ICON_FADE,
                                                ORDEM,
                                                VISIVEL
                                            FROM REPORT_MODALIDADES_WS(@id_modalidade)
                                            WHERE (@FILTRO IS NULL OR CODIGO LIKE '%' + @FILTRO + '%' OR TITULO LIKE '%' + @FILTRO + '%')
                                            ORDER BY CODIGO asc, TITULO ASC", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

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
                                                    <th style='text-align: center; width: 100%;'>Modalidades</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick='openModalidadeSelected({0}, {2});'>
                                            <td style='display:none;' id='id_{2}'>{0}</td>
                                            <td class='tbodyTrTd'>{1}</td>
                                        </tr>",
                                                myReader["ID_MODALIDADE"].ToString(),
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
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem modalidades a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem modalidades a apresentar.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string getModalidade(string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_modalidade int = {0};
                                            DECLARE @servidor varchar(500) = (select concat(site, icons_modalidades) from report_paths());

                                            SELECT 
                                                ID_MODALIDADE,
	                                            CODIGO,
	                                            TITULO,
	                                            TITULO_EN,
	                                            TITULO_ES,
	                                            TITULO_FR,
	                                            TEXTO,
	                                            TEXTO_EN,
	                                            TEXTO_ES,
	                                            TEXTO_FR,
	                                            VIDEO,
	                                            FOTO,
                                                ICON,
                                                ICON_FADE,
                                                ORDEM,
                                                VISIVEL,
                                                @servidor AS SERVIDOR
                                            FROM REPORT_MODALIDADES_WS(@id_modalidade)", id);

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

                                                <input id='btnDelete' value='Apagar' runat='server' type='button' onclick='deleteModalidade();' style='background-color: #4282b5; 
                                                width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                                <img src='img/icons/icon_close.png' style='cursor:pointer;' onclick='closeModalidadeInfo();'/>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: 'Noto Sans', sans-serif !important; height:100%'>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Código:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Nome:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Tradução Inglês:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Tradução Espanhol:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Tradução Francês:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Foto / Video:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Visivel:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Ordem:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Texto:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Inglês:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Espanhol:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Francês:</div>
                                                    <div style='height:200px; width:100%; margin-bottom: 10px'>Icon:</div>
                                                    <div style='height:200px; width:100%; margin-bottom: 10px'>Icon Hover:</div>
                                                </div>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='codigo' placeholder='Código' style='width: 100%; margin: auto; height: 100%;' value='{1}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='nome' placeholder='Nome' style='width: 100%; margin: auto; height: 100%;' value='{2}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='nomeEn' placeholder='Nome EN' style='width: 100%; margin: auto; height: 100%;' value='{3}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='nomeEs' placeholder='Nome ES' style='width: 100%; margin: auto; height: 100%;' value='{4}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='nomeFr' placeholder='Nome FR' style='width: 100%; margin: auto; height: 100%;' value='{5}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <label class='radio-inline'><input type='radio' name='fotovideo' value='foto'{10}>Foto</label>
                                                        <label class='radio-inline'><input type='radio' name='fotovideo' value='video'{11}>Video</label>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='visivel' name='visivel' style='width: 100%; margin: auto; height: 100%;' {16}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='number' class='form-control' id='ordem' placeholder='Ordem' style='width: 100%; margin: auto; height: 100%;' value='{13}'/>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto' placeholder='Texto' style='width: 100%; margin: auto; height: 100%;'>{6}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto_en' placeholder='Texto EN' style='width: 100%; margin: auto; height: 100%;'>{7}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto_es' placeholder='Texto ES' style='width: 100%; margin: auto; height: 100%;'>{8}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto_fr' placeholder='Texto FR' style='width: 100%; margin: auto; height: 100%;'>{9}</textarea>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px' id='divImages1'></div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px; text-align: center;background-color: #D3D3D3'>
                                                        <img src='../{12}{14}' style='height: 100%; width: auto' id='imgModalidades1'/>
                                                    </div>
                                                    <span class='variaveis' id='imgName1'>{14}</span>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px' id='divImages2'></div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px; text-align: center;background-color: #D3D3D3'>
                                                        <img src='../{12}{15}' style='height: 100%; width: auto' id='imgModalidades2'/>
                                                    </div>
                                                    <span class='variaveis' id='imgName2'>{15}</span>
                                                </div>
                                            </div>
                                            </div>",
                                                myReader["ID_MODALIDADE"].ToString(),
                                                myReader["CODIGO"].ToString(),
                                                myReader["TITULO"].ToString(),
                                                myReader["TITULO_EN"].ToString(),
                                                myReader["TITULO_ES"].ToString(),
                                                myReader["TITULO_FR"].ToString(),
                                                myReader["TEXTO"].ToString(),
                                                myReader["TEXTO_EN"].ToString(),
                                                myReader["TEXTO_ES"].ToString(),
                                                myReader["TEXTO_FR"].ToString(),
                                                myReader["FOTO"].ToString() == "1" ? " checked" : "",
                                                myReader["VIDEO"].ToString() == "1" ? " checked" : "",
                                                myReader["SERVIDOR"].ToString(),
                                                myReader["ORDEM"].ToString(),
                                                myReader["ICON"].ToString(),
                                                myReader["ICON_FADE"].ToString(),
                                                myReader["VISIVEL"].ToString() == "1" ? " checked" : "");
                }
                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                            <img src='img/icon_close.png' style='cursor:pointer;' onclick='closeModalidadeInfo();'/>
                                        </div>
                                        <div style='height:auto' class='panel-heading' id='panel-heading'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para esta modalidade.</span>
                                        </div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                            <img src='img/icon_close.png' style='cursor:pointer;' onclick='closeModalidadeInfo();'/>
                                        </div>
                                        <div style='height:auto' class='panel-heading' id='panel-heading'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para esta modalidade.</span>
                                        </div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string editar(string id_operador, string id_modalidade, string codigo, string titulo, string titulo_en, string titulo_es, string titulo_fr,
        string texto, string texto_en, string texto_es, string texto_fr, string foto, string video, string ordem, string icon, string icon_hover, string visivel)
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
	                                        DECLARE @id_modalidade int = {1};
	                                        DECLARE @titulo VARCHAR(1000) = '{2}';
	                                        DECLARE @titulo_en VARCHAR(1000) = '{3}';
	                                        DECLARE @titulo_es VARCHAR(1000) = '{4}';
	                                        DECLARE @titulo_fr VARCHAR(1000) = '{5}';
	                                        DECLARE @texto VARCHAR(MAX) = '{6}';
	                                        DECLARE @texto_en VARCHAR(MAX) = '{7}';
	                                        DECLARE @texto_es VARCHAR(MAX) = '{8}';
	                                        DECLARE @texto_fr VARCHAR(MAX) = '{9}';
	                                        DECLARE @foto bit = {10};
	                                        DECLARE @video bit = {11};
                                            DECLARE @codigo varchar(50) = '{12}';
                                            DECLARE @ordem int = {13};
                                            DECLARE @icon varchar(500) = '{14}';
                                            DECLARE @icon_hover varchar(500) = '{15}';
                                            DECLARE @visivel bit = {16};
	                                        DECLARE @ret int

                                            EXEC ALTERA_MODALIDADE @id_operador, @id_modalidade, @codigo, @titulo, @titulo_en, @titulo_es, @titulo_fr, 
                                                @texto, @texto_en, @texto_es, @texto_fr, @foto, @video, @ordem, @icon, @icon_hover, @visivel, @ret output

                                            SELECT @ret as res",
                                                id_operador, id_modalidade, titulo, titulo_en, titulo_es, titulo_fr, texto, texto_en, texto_es, texto_fr, foto, video, codigo, ordem, icon, icon_hover, visivel);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) < 0)
                    {
                        table.AppendFormat(@"Erro ao atualizar {0}!", titulo);
                    }
                    else
                    {
                        table.AppendFormat(@"{0} atualizado com sucesso!", titulo);
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao atualizar {0}!", titulo);
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao atualizar {0}!", titulo);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao atualizar {0}!", titulo);
        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string criar(string id_operador, string codigo, string titulo, string titulo_en, string titulo_es, string titulo_fr,
        string texto, string texto_en, string texto_es, string texto_fr, string foto, string video, string ordem, string icon, string icon_hover, string visivel)
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
	                                        DECLARE @codigo VARCHAR(50) = '{1}';
	                                        DECLARE @titulo VARCHAR(500) = '{2}';
	                                        DECLARE @titulo_en VARCHAR(500) = '{3}';
	                                        DECLARE @titulo_es VARCHAR(500) = '{4}';
	                                        DECLARE @titulo_fr VARCHAR(500) = '{5}';
	                                        DECLARE @texto VARCHAR(MAX) = '{6}';
	                                        DECLARE @texto_en VARCHAR(MAX) = '{7}';
	                                        DECLARE @texto_es VARCHAR(MAX) = '{8}';
	                                        DECLARE @texto_fr VARCHAR(MAX) = '{9}';
	                                        DECLARE @foto bit = {10};
	                                        DECLARE @video bit = {11};
                                            DECLARE @ordem int = {12};
                                            DECLARE @icon varchar(500) = '{13}';
                                            DECLARE @icon_hover varchar(500) = '{14}';
                                            DECLARE @visivel bit = {15};
	                                        DECLARE @id_modalidade int

                                            EXEC CRIA_MODALIDADE @id_operador, @codigo, @titulo, @titulo_en, @titulo_es, @titulo_fr, 
                                                @texto, @texto_en, @texto_es, @texto_fr, @foto, @video, @ordem, @icon, @icon_hover, @visivel, @id_modalidade output

                                            SELECT @id_modalidade as res",
                                                id_operador, codigo, titulo, titulo_en, titulo_es, titulo_fr, texto, texto_en, texto_es, texto_fr, foto, video, ordem, icon, icon_hover, visivel);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) < 0)
                    {
                        table.AppendFormat(@"Erro ao criar {0}!", titulo);
                    }
                    else
                    {
                        table.AppendFormat(@"{0} criado com sucesso!", titulo);
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao criar {0}!", titulo);
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao criar {0}!", titulo);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao criar {0}!", titulo);
        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string delete(string id_operador, string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_modalidade int = {0};
                                            DECLARE @id_operador int = {1};
                                            DECLARE @res int;

                                            EXEC APAGA_MODALIDADES @id_modalidade, @id_operador, @res OUTPUT

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
                        table.AppendFormat(@"Erro ao apagar modalidade!");
                    }
                    else
                    {
                        table.AppendFormat(@"Modalidade apagada com sucesso!");
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao apagar modalidade!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao apagar modalidade!");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao apagar modalidade!");
        connection.Close();
        return table.ToString();
    }


    private void loadImg1()
    {
        var table = new StringBuilder();
        string server = "";
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT CONCAT(SITE, ICONS_MODALIDADES) AS PATH FROM REPORT_PATHS()");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    server = myReader["PATH"].ToString();
                }
                connection.Close();
            }
            else
            {
                server = "";
                connection.Close();
            }

            table.AppendFormat(@"<select class='form-control' id='selectImgNew1' style='width:100%; height: 40px; font-size: small; float: left;' onchange='changeImgNew1();'>");
            table.AppendFormat(@"<option value='0'>Selecione uma imagem</option>");

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}'>{0}</option>", info.Name);
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFilesNew1'>{0}</span>", "..//" + server);
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            connection.Close();
        }

        divImagesNew1.InnerHtml = table.ToString();
    }

    private void loadImg2()
    {
        var table = new StringBuilder();
        string server = "";
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT CONCAT(SITE, ICONS_MODALIDADES) AS PATH FROM REPORT_PATHS()");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    server = myReader["PATH"].ToString();
                }
                connection.Close();
            }
            else
            {
                server = "";
                connection.Close();
            }

            table.AppendFormat(@"<select class='form-control' id='selectImgNew2' style='width:100%; height: 40px; font-size: small; float: left;' onchange='changeImgNew2();'>");
            table.AppendFormat(@"<option value='0'>Selecione uma imagem</option>");

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}'>{0}</option>", info.Name);
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFilesNew2'>{0}</span>", "..//" + server);
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            connection.Close();
        }

        divImagesNew2.InnerHtml = table.ToString();
    }

    [WebMethod]
    public static string loadImages1(string imgDefault)
    {
        var table = new StringBuilder();
        string server = "";
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT CONCAT(SITE, ICONS_MODALIDADES) AS PATH FROM REPORT_PATHS()");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    server = myReader["PATH"].ToString();
                }
                connection.Close();
            }
            else
            {
                server = "";
                connection.Close();
            }

            table.AppendFormat(@"<select class='form-control' id='selectImg1' style='width:100%; height: 40px; font-size: small; float: left;' onchange='changeImg1();'>");

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}' {1}>{0}</option>", info.Name, info.Name == imgDefault ? "selected" : "");
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFiles1'>{0}</span>", "..//" + server);

            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            connection.Close();
        }

        return table.ToString();
    }

    [WebMethod]
    public static string loadImages2(string imgDefault)
    {
        var table = new StringBuilder();
        string server = "";
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT CONCAT(SITE, ICONS_MODALIDADES) AS PATH FROM REPORT_PATHS()");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    server = myReader["PATH"].ToString();
                }
                connection.Close();
            }
            else
            {
                server = "";
                connection.Close();
            }

            table.AppendFormat(@"<select class='form-control' id='selectImg2' style='width:100%; height: 40px; font-size: small; float: left;' onchange='changeImg2();'>");

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}' {1}>{0}</option>", info.Name, info.Name == imgDefault ? "selected" : "");
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFiles2'>{0}</span>", "..//" + server);

            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            connection.Close();
        }

        return table.ToString();
    }
}
