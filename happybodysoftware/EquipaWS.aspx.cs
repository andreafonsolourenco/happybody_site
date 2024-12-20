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
using System.Xml;

public partial class EquipaWS : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "EquipaWS"))
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

            string sql = string.Format(@"   DECLARE @id_staff int;
                                            DECLARE @FILTRO varchar(MAX) = {0};

                                            SELECT 
                                                id_staff,
	                                            NOME,
	                                            FUNCAO,
	                                            FUNCAO_EN,
	                                            FUNCAO_ES,
	                                            FUNCAO_FR,
	                                            TEXTO,
	                                            TEXTO_EN,
	                                            TEXTO_ES,
	                                            TEXTO_FR,
	                                            ORDEM,
                                                FOTO_1,
	                                            FOTO_2
                                            FROM REPORT_STAFF_WS(@id_staff)
                                            WHERE (@FILTRO IS NULL OR FUNCAO LIKE '%' + @FILTRO + '%' OR NOME LIKE '%' + @FILTRO + '%')
                                            ORDER BY ORDEM asc, NOME asc", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

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
                                                    <th style='text-align: center; width: 100%;'>Equipa</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick='openStaffSelected({0}, {2});'>
                                            <td style='display:none;' id='id_{2}'>{0}</td>
                                            <td class='tbodyTrTd'>{1}</td>
                                        </tr>",
                                                myReader["ID_STAFF"].ToString(),
                                                myReader["NOME"].ToString(),
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
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem elementos da equipa a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem elementos da equipa a apresentar.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string getStaff(string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_staff int = {0};

                                            SELECT 
                                                id_staff,
	                                            NOME,
	                                            FUNCAO,
	                                            FUNCAO_EN,
	                                            FUNCAO_ES,
	                                            FUNCAO_FR,
	                                            TEXTO,
	                                            TEXTO_EN,
	                                            TEXTO_ES,
	                                            TEXTO_FR,
	                                            ORDEM,
                                                FOTO_1,
	                                            FOTO_2,
                                                (SELECT SERVIDOR_SITE FROM APPLICATION_CONFIG) AS SERVIDOR
                                            FROM REPORT_STAFF_WS(@id_staff)", id);

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

                                                <input id='btnDelete' value='Apagar' runat='server' type='button' onclick='deleteStaff();' style='background-color: #4282b5; 
                                                width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                                <img src='img/icons/icon_close.png' style='cursor:pointer;' onclick='closeStaffInfo();'/>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: 'Noto Sans', sans-serif !important; height:100%'>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Nome:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Ordem:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Função:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Inglês:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Espanhol:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Francês:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Texto:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Inglês:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Espanhol:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Francês:</div>
                                                    <div style='height:200px; width:100%; margin-bottom: 10px'>Imagem 1</div>
                                                    <div style='height:200px; width:100%; margin-bottom: 10px'>Imagem 2</div>
                                                </div>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='nome' name='nome' placeholder='Nome' style='width: 100%; margin: auto; height: 100%;' value='{1}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='number' class='form-control' id='ordem' name='ordem' placeholder='Ordem' style='width: 100%; margin: auto; height: 100%;' value='{10}'/>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='funcao' name='funcao' placeholder='Função' style='width: 100%; margin: auto; height: 100%;'>{2}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='funcao_en' name='funcao_en' placeholder='Função EN' style='width: 100%; margin: auto; height: 100%;'>{3}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='funcao_es' name='funcao_es' placeholder='Função ES' style='width: 100%; margin: auto; height: 100%;'>{4}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='funcao_fr' name='funcao_fr' placeholder='Função FR' style='width: 100%; margin: auto; height: 100%;'>{5}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto' name='texto' placeholder='Texto' style='width: 100%; margin: auto; height: 100%;'>{6}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto_en' name='texto_en' placeholder='Texto EN' style='width: 100%; margin: auto; height: 100%;'>{7}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto_es' name='texto_es' placeholder='Texto ES' style='width: 100%; margin: auto; height: 100%;'>{8}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto_fr' name='texto_fr' placeholder='Texto FR' style='width: 100%; margin: auto; height: 100%;'>{9}</textarea>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px' id='divImages1'></div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px; text-align: center;'>
                                                        <img src='../{13}img/equipa/{11}' style='height: 100%; width: auto' id='imgEquipa1'/>
                                                    </div>
                                                    <span class='variaveis' id='imgName1'>{11}</span>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px' id='divImages2'></div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px; text-align: center;'>
                                                        <img src='../{13}img/equipa/{12}' style='height: 100%; width: auto' id='imgEquipa2'/>
                                                    </div>
                                                    <span class='variaveis' id='imgName2'>{12}</span>
                                                </div>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: 'Noto Sans', sans-serif !important; height:100%' id='divModalidadesStaff'>

                                            </div>",
                                                myReader["ID_STAFF"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["FUNCAO"].ToString().Replace("<br />", "\n"),
                                                myReader["FUNCAO_EN"].ToString().Replace("<br />", "\n"),
                                                myReader["FUNCAO_ES"].ToString().Replace("<br />", "\n"),
                                                myReader["FUNCAO_FR"].ToString().Replace("<br />", "\n"),
                                                myReader["TEXTO"].ToString().Replace("<br />", "\n"),
                                                myReader["TEXTO_EN"].ToString().Replace("<br />", "\n"),
                                                myReader["TEXTO_ES"].ToString().Replace("<br />", "\n"),
                                                myReader["TEXTO_FR"].ToString().Replace("<br />", "\n"),
                                                myReader["ORDEM"].ToString(),
                                                myReader["FOTO_1"].ToString(),
                                                myReader["FOTO_2"].ToString(),
                                                myReader["SERVIDOR"].ToString());
                }
                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                            <img src='img/icon_close.png' style='cursor:pointer;' onclick='closeStaffInfo();'/>
                                        </div>
                                        <div style='height:auto' class='panel-heading' id='panel-heading'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para este elemento.</span>
                                        </div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                            <img src='img/icon_close.png' style='cursor:pointer;' onclick='closeStaffInfo();'/>
                                        </div>
                                        <div style='height:auto' class='panel-heading' id='panel-heading'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para este elemento.</span>
                                        </div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string editar(string id_operador, string id_staff, string nome, string funcao, string funcao_en, string funcao_es, string funcao_fr,
        string texto, string texto_en, string texto_es, string texto_fr, string ordem, string foto_1, string foto_2)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        funcao = funcao.Replace("''", "\"");
        funcao = funcao.Replace("'", "''");

        funcao_en = funcao_en.Replace("''", "\"");
        funcao_en = funcao_en.Replace("'", "''");

        funcao_es = funcao_es.Replace("''", "\"");
        funcao_es = funcao_es.Replace("'", "''");

        funcao_fr = funcao_fr.Replace("''", "\"");
        funcao_fr = funcao_fr.Replace("'", "''");

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
	                                        DECLARE @id_staff int = {1};
	                                        DECLARE @nome VARCHAR(500) = '{2}';
	                                        DECLARE @funcao VARCHAR(1000) = '{3}';
	                                        DECLARE @funcao_en VARCHAR(1000) = '{4}';
	                                        DECLARE @funcao_es VARCHAR(1000) = '{5}';
	                                        DECLARE @funcao_fr VARCHAR(1000) = '{6}';
	                                        DECLARE @texto VARCHAR(MAX) = '{7}';
	                                        DECLARE @texto_en VARCHAR(MAX) = '{8}';
	                                        DECLARE @texto_es VARCHAR(MAX) = '{9}';
	                                        DECLARE @texto_fr VARCHAR(MAX) = '{10}';
	                                        DECLARE @ordem INT = {11};
	                                        DECLARE @foto_1 VARCHAR(500) = '{12}';
	                                        DECLARE @foto_2 VARCHAR(500) = '{13}';
	                                        DECLARE @ret int

                                            EXEC UPDATE_STAFF_WS @id_operador, @id_staff, @nome, @funcao, @funcao_en, @funcao_es, @funcao_fr, 
                                                @texto, @texto_en, @texto_es, @texto_fr, @ordem, @foto_1, @foto_2, @ret output

                                            SELECT @ret as res",
                                                id_operador, id_staff, nome, 
                                                funcao.Replace("\n", "' + char(13) + char(10) + '"), 
                                                funcao_en.Replace("\n", "' + char(13) + char(10) + '"), 
                                                funcao_es.Replace("\n", "' + char(13) + char(10) + '"), 
                                                funcao_fr.Replace("\n", "' + char(13) + char(10) + '"), 
                                                texto.Replace("\n", "' + char(13) + char(10) + '"), 
                                                texto_en.Replace("\n", "' + char(13) + char(10) + '"), 
                                                texto_es.Replace("\n", "' + char(13) + char(10) + '"), 
                                                texto_fr.Replace("\n", "' + char(13) + char(10) + '"), ordem, foto_1, foto_2);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) < 0)
                    {
                        table.AppendFormat(@"Erro ao atualizar {0}!", nome);
                    }
                    else
                    {
                        table.AppendFormat(@"{0} atualizado com sucesso!", nome);
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao atualizar {0}!", nome);
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao atualizar {0}!", nome);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao atualizar {0}!", nome);
        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string criar(string id_operador, string nome, string funcao, string funcao_en, string funcao_es, string funcao_fr,
        string texto, string texto_en, string texto_es, string texto_fr, string ordem, string foto_1, string foto_2)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        funcao = funcao.Replace("''", "\"");
        funcao = funcao.Replace("'", "''");

        funcao_en = funcao_en.Replace("''", "\"");
        funcao_en = funcao_en.Replace("'", "''");

        funcao_es = funcao_es.Replace("''", "\"");
        funcao_es = funcao_es.Replace("'", "''");

        funcao_fr = funcao_fr.Replace("''", "\"");
        funcao_fr = funcao_fr.Replace("'", "''");

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
	                                        DECLARE @nome VARCHAR(500) = '{1}';
	                                        DECLARE @funcao VARCHAR(1000) = '{2}';
	                                        DECLARE @funcao_en VARCHAR(1000) = '{3}';
	                                        DECLARE @funcao_es VARCHAR(1000) = '{4}';
	                                        DECLARE @funcao_fr VARCHAR(1000) = '{5}';
	                                        DECLARE @texto VARCHAR(MAX) = '{6}';
	                                        DECLARE @texto_en VARCHAR(MAX) = '{7}';
	                                        DECLARE @texto_es VARCHAR(MAX) = '{8}';
	                                        DECLARE @texto_fr VARCHAR(MAX) = '{9}';
	                                        DECLARE @ordem INT = {10};
	                                        DECLARE @foto_1 VARCHAR(500) = '{11}';
	                                        DECLARE @foto_2 VARCHAR(500) = '{12}';
	                                        DECLARE @id_staff int

                                            EXEC CRIA_STAFF_WS @id_operador, @nome, @funcao, @funcao_en, @funcao_es, @funcao_fr, 
                                                @texto, @texto_en, @texto_es, @texto_fr, @ordem, @foto_1, @foto_2, @id_staff output

                                            SELECT @id_staff as res",
                                                id_operador, nome, funcao, funcao_en, funcao_es, funcao_fr, texto, texto_en, texto_es, texto_fr, ordem, foto_1, foto_2);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) < 0)
                    {
                        table.AppendFormat(@"Erro ao criar {0}!", nome);
                    }
                    else
                    {
                        table.AppendFormat(@"{0} criado com sucesso!", nome);
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao criar {0}!", nome);
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao criar {0}!", nome);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao criar {0}!", nome);
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

            string sql = string.Format(@"   DECLARE @id_staff int = {0};
                                            DECLARE @id_operador int = {1};
                                            DECLARE @res int;

                                            EXEC DELETE_STAFF_WS @id_operador, @id_staff, @res OUTPUT

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
                        table.AppendFormat(@"Erro ao apagar elemento do staff!");
                    }
                    else
                    {
                        table.AppendFormat(@"Elemento do staff apagado com sucesso!");
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao apagar elemento do staff!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao apagar elemento do staff!");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao apagar elemento do staff!");
        connection.Close();
        return table.ToString();
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

            table.AppendFormat(@"<select class='form-control' id='selectImg1' style='width:100%; height: 40px; font-size: small; float: left;' onchange='changeImg1();'>");
            table.AppendFormat(@"<option value='no_img.jpg'>Sem Imagem</option>");

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server + "img/equipa/"))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}' {1}>{0}</option>", info.Name, info.Name == imgDefault ? "selected" : "");
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFiles1'>{0}</span>", "..//" + server + "img//equipa//");

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

            table.AppendFormat(@"<select class='form-control' id='selectImg2' style='width:100%; height: 40px; font-size: small; float: left;' onchange='changeImg2();'>");
            table.AppendFormat(@"<option value='no_img.jpg'>Sem Imagem</option>");

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server + "img/equipa/"))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}' {1}>{0}</option>", info.Name, info.Name == imgDefault ? "selected" : "");
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFiles2'>{0}</span>", "..//" + server + "img//equipa//");

            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            connection.Close();
        }

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

            table.AppendFormat(@"<select class='form-control' id='selectImgNew1' style='width:100%; height: 40px; font-size: small; float: left;' onchange='changeImgNew1();'>");
            table.AppendFormat(@"<option value='no_img.jpg'>Selecione uma Imagem</option>");

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server + "img/equipa/"))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}'>{0}</option>", info.Name);
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFilesNew1'>{0}</span>", "..//" + server + "img//equipa//");
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

            table.AppendFormat(@"<select class='form-control' id='selectImgNew2' style='width:100%; height: 40px; font-size: small; float: left;' onchange='changeImgNew2();'>");
            table.AppendFormat(@"<option value='no_img.jpg'>Selecione uma Imagem</option>");

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server + "img/equipa/"))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}'>{0}</option>", info.Name);
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFilesNew2'>{0}</span>", "..//" + server + "img//equipa//");
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            connection.Close();
        }

        divImagesNew2.InnerHtml = table.ToString();
    }

    [WebMethod]
    public static string loadModalidadeStaff(string id_staff)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_staff int = {0};
                                            DECLARE @id_modalidade int;

                                            SELECT 
                                                id_modalidade, modalidade, id_staff, nome, presente
                                            FROM REPORT_STAFF_MODALIDADES(@id_staff, @id_modalidade)
                                            ORDER BY modalidade asc", id_staff);

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
                                                    <th style='text-align: center; width: 100%;' colspan='2'>Modalidades Lecionadas</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr>
                                            <td style='display:none;' id='id_modalidade_{2}'>{0}</td>
                                            <td>{1}</td>
                                            <td><input type='checkbox' class='form-control' id='presente_{2}' {3}/></td>
                                        </tr>",
                                                myReader["id_modalidade"].ToString(),
                                                myReader["modalidade"].ToString(),
                                                conta.ToString(),
                                                myReader["presente"].ToString() == "1" ? " checked " : "");

                    conta++;
                }

                table.AppendFormat("</tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='countElementsModalidades'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem elementos da equipa a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem elementos da equipa a apresentar.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string updateModalidadesStaff(string id_operador, string xml)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);
      
        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_op int = {0};
                                            DECLARE @xml nvarchar(max) = '{1}';
                                            DECLARE @ret int;

                                            EXEC UPDATE_MODALIDADES_STAFF @id_op, @xml, @ret output

                                            SELECT @ret as ret", id_operador, xml);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"{0}", myReader["ret"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("-1");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("-1");
            connection.Close();
            return table.ToString();
        }
    }
}
