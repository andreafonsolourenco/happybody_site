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

public partial class ServicosWS : Page
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

            string sql = string.Format(@"   DECLARE @id_servico int;
                                            DECLARE @FILTRO varchar(MAX) = {0};

                                            SELECT 
                                                ID_SERVICO,
	                                            CODIGO,
	                                            DESIGNACAO,
	                                            DESIGNACAO_EN,
	                                            DESIGNACAO_ES,
	                                            DESIGNACAO_FR,
	                                            TEXTO,
	                                            TEXTO_EN,
	                                            TEXTO_ES,
	                                            TEXTO_FR,
	                                            IMAGEM
                                            FROM REPORT_SERVICOS_WS(@id_servico)
                                            WHERE (@FILTRO IS NULL OR (CODIGO LIKE '%' + @FILTRO + '%' OR DESIGNACAO LIKE '%' + @FILTRO + '%'))
                                            ORDER BY ID_SERVICO asc", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

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
                                                    <th style='text-align: center; width: 100%;'>Serviços</th>
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
                                                myReader["ID_SERVICO"].ToString(),
                                                myReader["DESIGNACAO"].ToString(),
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
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem serviços a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem serviços a apresentar.</div>");
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

            string sql = string.Format(@"   DECLARE @id_servico int = {0};

                                            SELECT 
                                                ID_SERVICO,
	                                            CODIGO,
	                                            DESIGNACAO,
	                                            DESIGNACAO_EN,
	                                            DESIGNACAO_ES,
	                                            DESIGNACAO_FR,
	                                            TEXTO,
	                                            TEXTO_EN,
	                                            TEXTO_ES,
	                                            TEXTO_FR,
	                                            IMAGEM,
                                                (SELECT SERVIDOR_SITE FROM APPLICATION_CONFIG) AS SERVIDOR
                                            FROM REPORT_SERVICOS_WS(@id_servico)", id);

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
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Código:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Designação:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Tradução Inglês:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Tradução Espanhol:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Tradução Francês:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Texto:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Inglês:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Espanhol:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Francês:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Imagem:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'></div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'></div>
                                                </div>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='codigo' name='codigo' placeholder='Código' style='width: 100%; margin: auto; height: 100%;' value='{1}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='designacao' name='designacao' placeholder='Designação' style='width: 100%; margin: auto; height: 100%;' value='{2}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='designacao_en' name='designacao_en' placeholder='Designação Inglês' style='width: 100%; margin: auto; height: 100%;' value='{3}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='designacao_es' name='designacao_es' placeholder='Designação Espanhol' style='width: 100%; margin: auto; height: 100%;' value='{4}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='designacao_fr' name='designacao_fr' placeholder='Designação Francês' style='width: 100%; margin: auto; height: 100%;' value='{5}'/>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto' name='texto' placeholder='Texto' style='width: 100%; margin: auto; height: 100%;' rows='5'>{6}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto_en' name='texto_en' placeholder='Texto Inglês' style='width: 100%; margin: auto; height: 100%;' rows='5'>{7}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto_es' name='texto_es' placeholder='Texto Espanhol' style='width: 100%; margin: auto; height: 100%;' rows='5'>{8}</textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto_fr' name='texto_fr' placeholder='Texto Francês' style='width: 100%; margin: auto; height: 100%;' rows='5'>{9}</textarea>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px' id='divImages'></div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px; text-align: center;'>
                                                        <img src='../{11}img/servicos/{10}' style='height: 100%; width: auto' id='imgServico'/>
                                                    </div>
                                                    <span class='variaveis' id='imgName'>{10}</span>
                                                </div>
                                            </div>",
                                                myReader["ID_SERVICO"].ToString(),
                                                myReader["CODIGO"].ToString(),
                                                myReader["DESIGNACAO"].ToString(),
                                                myReader["DESIGNACAO_EN"].ToString().Trim(),
                                                myReader["DESIGNACAO_ES"].ToString(),
                                                myReader["DESIGNACAO_FR"].ToString(),
                                                myReader["TEXTO"].ToString(),
                                                myReader["TEXTO_EN"].ToString().Trim(),
                                                myReader["TEXTO_ES"].ToString(),
                                                myReader["TEXTO_FR"].ToString(),
                                                myReader["IMAGEM"].ToString(),
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
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para este serviço.</span>
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
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para este serviço.</span>
                                        </div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string editar(string id_operador, string id_servico, string codigo, string designacao, string designacao_en, string designacao_es, string designacao_fr,
        string texto, string texto_en, string texto_es, string texto_fr, string imagem)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        designacao = designacao.Replace("''", "\"");
        designacao = designacao.Replace("'", "''");

        designacao_en = designacao_en.Replace("''", "\"");
        designacao_en = designacao_en.Replace("'", "''");

        designacao_es = designacao_es.Replace("''", "\"");
        designacao_es = designacao_es.Replace("'", "''");

        designacao_fr = designacao_fr.Replace("''", "\"");
        designacao_fr = designacao_fr.Replace("'", "''");

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
	                                        DECLARE @id_servico int = {1};
	                                        DECLARE @codigo char(30) = '{2}';
	                                        DECLARE @designacao varchar(500) = '{3}';
	                                        DECLARE @designacao_en varchar(500) = '{4}';
                                            DECLARE @designacao_es varchar(500) = '{5}';
                                            DECLARE @designacao_fr varchar(500) = '{6}';
                                            DECLARE @texto varchar(max) = '{7}';
                                            DECLARE @texto_en varchar(max) = '{8}';
                                            DECLARE @texto_es varchar(max) = '{9}';
                                            DECLARE @texto_fr varchar(max) = '{10}';
                                            DECLARE @imagem varchar(max) = '{11}';
                                            DECLARE @res int;

                                            EXEC ALTERA_SERVICO @id_operador, @id_servico, @codigo, @designacao, @designacao_en, @designacao_es, @designacao_fr, @texto, @texto_en, @texto_es, @texto_fr, @imagem, @res output

                                            SELECT @res as res",
                                                id_operador, id_servico, codigo, designacao, designacao_en, designacao_es, designacao_fr, texto, texto_en, texto_es, texto_fr, imagem);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) < 0)
                    {
                        table.AppendFormat(@"Erro ao atualizar serviço {0}!", codigo);
                    }
                    else
                    {
                        table.AppendFormat(@"Serviço {0} atualizado com sucesso!", codigo);
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao atualizar serviço '{0}'!", codigo);
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao atualizar serviço '{0}'!", codigo);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao atualizar serviço '{0}'!", codigo);
        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string criar(string id_operador, string codigo, string designacao, string designacao_en, string designacao_es, string designacao_fr,
        string texto, string texto_en, string texto_es, string texto_fr, string imagem)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        designacao = designacao.Replace("''", "\"");
        designacao = designacao.Replace("'", "''");

        designacao_en = designacao_en.Replace("''", "\"");
        designacao_en = designacao_en.Replace("'", "''");

        designacao_es = designacao_es.Replace("''", "\"");
        designacao_es = designacao_es.Replace("'", "''");

        designacao_fr = designacao_fr.Replace("''", "\"");
        designacao_fr = designacao_fr.Replace("'", "''");

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
	                                        DECLARE @id_servico int;
	                                        DECLARE @codigo char(30) = '{1}';
	                                        DECLARE @designacao varchar(500) = '{2}';
	                                        DECLARE @designacao_en varchar(500) = '{3}';
                                            DECLARE @designacao_es varchar(500) = '{4}';
                                            DECLARE @designacao_fr varchar(500) = '{5}';
                                            DECLARE @texto varchar(max) = '{6}';
                                            DECLARE @texto_en varchar(max) = '{7}';
                                            DECLARE @texto_es varchar(max) = '{8}';
                                            DECLARE @texto_fr varchar(max) = '{9}';
                                            DECLARE @imagem varchar(max) = '{10}';

                                            EXEC CRIA_SERVICO @id_operador, @codigo, @designacao, @designacao_en, @designacao_es, @designacao_fr, @texto, @texto_en, @texto_es, @texto_fr, @imagem, @id_servico output",
                                                id_operador, codigo, designacao, designacao_en, designacao_es, designacao_fr, texto, texto_en, texto_es, texto_fr, imagem);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"Serviço '{0}' criado com sucesso!", codigo);
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao criar serviço '{0}'!", codigo);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao criar serviço '{0}'!", codigo);
        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string DeleteService(string id_operador, string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_servico int = {0};
                                            DECLARE @id_operador int = {1};
                                            DECLARE @res int;

                                            EXEC APAGA_SERVICO @id_servico, @id_operador, @res OUTPUT

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
                        table.AppendFormat(@"Erro ao apagar serviço!");
                    }
                    else
                    {
                        table.AppendFormat(@"Serviço apagado com sucesso!");
                    }

                    connection.Close();
                    return table.ToString();
                }

            }
            else
            {
                table.AppendFormat(@"Erro ao apagar serviço!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao apagar serviço!");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao apagar serviço!");
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

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server + "img/servicos/"))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}' {1}>{0}</option>", info.Name, info.Name == imgDefault ? "selected" : "");
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFiles'>{0}</span>", "..//" + server + "img//servicos//");

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

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server + "img/servicos/"))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}'>{0}</option>", info.Name);
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFilesNew'>{0}</span>", "..//" + server + "img//servicos//");
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            connection.Close();
        }

        divImgNew.InnerHtml = table.ToString();
    }
}
