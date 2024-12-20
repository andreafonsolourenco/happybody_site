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

public partial class ParceirosWS : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "ParceirosWS"))
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

            string sql = string.Format(@"   DECLARE @id_parceiro int;
                                            DECLARE @FILTRO varchar(MAX) = {0};

                                            SELECT 
                                                id_parceiro,
	                                            nome,
	                                            ordem,
	                                            icon,
                                                facebook,
                                                instagram,
                                                website
                                            FROM REPORT_PARCEIROS(@id_parceiro)
                                            WHERE (@FILTRO IS NULL OR NOME LIKE '%' + @FILTRO + '%')
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
                                                    <th style='text-align: center; width: 100%;'>Parceiro</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick='openParceiroSelected({0}, {2});'>
                                            <td style='display:none;' id='id_{2}'>{0}</td>
                                            <td class='tbodyTrTd'>{1}</td>
                                        </tr>",
                                                myReader["id_parceiro"].ToString(),
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
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem parceiros a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem parceiros a apresentar.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string getParceiro(string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_parceiro int = {0};

                                            SELECT 
                                                id_parceiro,
	                                            nome,
	                                            ordem,
	                                            icon,
                                                facebook,
                                                instagram,
                                                website,
                                                (SELECT SERVIDOR_SITE FROM APPLICATION_CONFIG) AS SERVIDOR
                                            FROM REPORT_PARCEIROS(@id_parceiro)", id);

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

                                                <input id='btnDelete' value='Apagar' runat='server' type='button' onclick='deleteParceiro();' style='background-color: #4282b5; 
                                                width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                                <img src='img/icons/icon_close.png' style='cursor:pointer;' onclick='closeParceiroInfo();'/>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: 'Noto Sans', sans-serif !important; height:100%'>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Nome:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Ordem:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Website:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Facebook:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Instagram:</div>
                                                    <div style='height:200px; width:100%; margin-bottom: 10px'>Imagem</div>
                                                </div>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='nome' name='nome' placeholder='Nome' style='width: 100%; margin: auto; height: 100%;' value='{1}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='number' class='form-control' id='ordem' name='ordem' placeholder='Ordem' style='width: 100%; margin: auto; height: 100%;' value='{2}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='website' name='website' placeholder='Website' style='width: 100%; margin: auto; height: 100%;' value='{5}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='facebook' name='facebook' placeholder='Facebook' style='width: 100%; margin: auto; height: 100%;' value='{6}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='instagram' name='instagram' placeholder='Instagram' style='width: 100%; margin: auto; height: 100%;' value='{7}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px' id='divImages'></div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px; text-align: center;'>
                                                        <img src='../{4}img/parceiros/{3}' style='height: 100%; width: auto' id='imgParceiro'/>
                                                    </div>
                                                    <span class='variaveis' id='imgName'>{3}</span>
                                                </div>
                                            </div>",
                                                myReader["id_parceiro"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["ORDEM"].ToString(),
                                                myReader["icon"].ToString(),
                                                myReader["SERVIDOR"].ToString(),
                                                myReader["website"].ToString(),
                                                myReader["facebook"].ToString(),
                                                myReader["instagram"].ToString());
                }
                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                            <img src='img/icon_close.png' style='cursor:pointer;' onclick='closeParceiroInfo();'/>
                                        </div>
                                        <div style='height:auto' class='panel-heading' id='panel-heading'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para este parceiro.</span>
                                        </div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                            <img src='img/icon_close.png' style='cursor:pointer;' onclick='closeParceiroInfo();'/>
                                        </div>
                                        <div style='height:auto' class='panel-heading' id='panel-heading'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para este parceiro.</span>
                                        </div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string editar(string id_operador, string id_parceiro, string nome, string ordem, string icon, string website, string facebook, string instagram)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @id_parceiro int = {1};
	                                        DECLARE @nome VARCHAR(500) = '{2}';
	                                        DECLARE @ordem INT = {3};
	                                        DECLARE @icon VARCHAR(500) = '{4}';
                                            DECLARE @website varchar(750) = '{5}';
                                            DECLARE @facebook varchar(750) = '{6}';
                                            DECLARE @instagram varchar(750) = '{7}';
	                                        DECLARE @ret int

                                            EXEC UPDATE_PARCEIRO_WS @id_operador, @id_parceiro, @nome, @ordem, @icon, @website, @facebook, @instagram, @ret output

                                            SELECT @ret as res",
                                                id_operador, id_parceiro, nome, ordem, icon, website, facebook, instagram);

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
    public static string criar(string id_operador, string nome, string ordem, string icon, string website, string facebook, string instagram)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @nome VARCHAR(500) = '{1}';
	                                        DECLARE @ordem INT = {2};
	                                        DECLARE @icon VARCHAR(500) = '{3}';
                                            DECLARE @website varchar(750) = '{4}';
                                            DECLARE @facebook varchar(750) = '{5}';
                                            DECLARE @instagram varchar(750) = '{6}';
	                                        DECLARE @id_parceiro int

                                            EXEC CRIA_PARCEIRO_WS @id_operador, @nome, @ordem, @icon, @website, @facebook, @instagram, @id_parceiro output

                                            SELECT @id_parceiro as res",
                                                id_operador, nome, ordem, icon, website, facebook, instagram);

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

            string sql = string.Format(@"   DECLARE @id_parceiro int = {0};
                                            DECLARE @id_operador int = {1};
                                            DECLARE @res int;

                                            EXEC DELETE_PARCEIROS_WS @id_operador, @id_parceiro, @res OUTPUT

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

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server + "img/parceiros/"))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}' {1}>{0}</option>", info.Name, info.Name == imgDefault ? "selected" : "");
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFiles'>{0}</span>", "..//" + server + "img//parceiros//");

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
            table.AppendFormat(@"<option value='0'>Selecione uma imagem</option>");

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + "//" + server + "img/parceiros/"))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}'>{0}</option>", info.Name);
            }

            table.AppendFormat(@"</select><span class='variaveis' id='pathToFilesNew'>{0}</span>", "..//" + server + "img//parceiros//");
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            connection.Close();
        }

        divImagesNew.InnerHtml = table.ToString();
    }
}
