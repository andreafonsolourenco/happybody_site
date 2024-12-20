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

public partial class Staff : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "ContractType"))
            {
                
            }
        }

        loadOperatorData();
    }

    private void loadOperatorData()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            SELECT 
                                                ADMINISTRADOR
                                            FROM REPORT_OPERADORES(@id_operador)", id);

            int admin = 0;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    lbladmin.InnerHtml = myReader["ADMINISTRADOR"].ToString();
                    if (myReader["ADMINISTRADOR"].ToString() == "0")
                        btnNewOperator.Visible = false;
                }
            }
            else
            {
                connection.Close();
                lbladmin.InnerHtml = "0";
                btnNewOperator.Visible = false;
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            lbladmin.InnerHtml = "0";
            btnNewOperator.Visible = false;
        }
    }

    [WebMethod]
    public static string load(string filtro, string administrador)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        string adminFilter = "";

        if (administrador == "0")
        {
            adminFilter = " AND ATIVO = 1 AND VISIVEL = 1 ";
        }

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador INT;
                                            DECLARE @FILTRO varchar(MAX) = {0};

                                            SELECT 
                                                OPERADORESID,
	                                            CODIGO,
	                                            NOME,
	                                            EMAIL,
	                                            PASSWORD,
	                                            NOTAS,
	                                            LASTLOGIN,
	                                            TELEFONE,
	                                            TELEMOVEL,
	                                            ADMINISTRADOR,
	                                            ESCRITA,
	                                            LEITURA,
	                                            ATIVO,
	                                            VISIVEL
                                            FROM REPORT_OPERADORES(@id_operador)
                                            WHERE (@FILTRO IS NULL OR (CODIGO LIKE '%' + @FILTRO + '%' OR NOME LIKE '%' + @FILTRO + '%')) {1}
                                            ORDER BY ADMINISTRADOR DESC, NOME ASC", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro)
                                                                                  , adminFilter);
            //return sql;

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
                                                    <th style='text-align: center; width: 100%;'>Operadores</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick='openOperatorInfo({0}, {2});'>
                                            <td style='display:none;' id='id_{2}'>{0}</td>
                                            <td class='tbodyTrTd'>{1}</td>
                                        </tr>",
                                                myReader["OPERADORESID"].ToString(),
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
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem operadores a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem operadores a apresentar.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string getOperatorData(string id, string meuID, string administrador)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador INT = {0};

                                            SELECT 
                                                OPERADORESID,
	                                            CODIGO,
	                                            NOME,
	                                            EMAIL,
	                                            PASSWORD,
	                                            NOTAS,
	                                            LASTLOGIN,
	                                            TELEFONE,
	                                            TELEMOVEL,
	                                            ADMINISTRADOR,
	                                            ESCRITA,
	                                            LEITURA,
	                                            ATIVO,
	                                            VISIVEL
                                            FROM REPORT_OPERADORES(@id_operador)", id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:left; margin-top: 5px; margin-bottom: 10px;'>");

                    if(administrador=="1") {
                        table.AppendFormat(@" <input id='btnSave' value='Guardar' runat='server' type='button' onclick='updateInfo();' style='background-color: #4282b5; 
                                                    width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                                <input id='btnApagar' value='Apagar' runat='server' type='button' onclick='deleteOperator();' style='background-color: #4282b5; 
                                                    width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>");
                    }

                    if (administrador == "0" && id == meuID)
                    {
                        table.AppendFormat(@" <input id='btnSave' value='Guardar' runat='server' type='button' onclick='updateInfo();' style='background-color: #4282b5; 
                                                    width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>");
                    }



                    table.AppendFormat(@"   </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                                <img src='img/icons/icon_close.png' style='cursor:pointer;' onclick='closeOperatorInfo();'/>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: 'Noto Sans', sans-serif !important; height:100%'>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Nome:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Código:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Email:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Password:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Telefone:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Telemovel:</div>");

                    if(administrador == "1") {
                        table.AppendFormat(@"<div style='height:50px; width:100%; margin-bottom: 10px'>Administrador:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Escrita:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Leitura:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Ativo:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Visivel:</div>");
                    }

                    table.AppendFormat(@"
                                                    
                                                    <div style='height:200px; width:100%; margin-bottom: 10px'>Notas:</div>
                                                </div>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='nome' name='nome' placeholder='Nome' style='width: 100%; margin: auto; height: 100%;' value='{2}' {15}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='codigo' name='codigo' placeholder='Código' style='width: 100%; margin: auto; height: 100%;' value='{1}' readonly/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='email' name='email' placeholder='Email' style='width: 100%; margin: auto; height: 100%;' value='{4}' {15}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='password' class='form-control' id='password' name='password' placeholder='Password' style='width: 100%; margin: auto; height: 100%;' value='{5}' {15}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='telefone' name='telefone' placeholder='Telefone' style='width: 100%; margin: auto; height: 100%;' value='{8}' {15}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='telemovel' name='telemovel' placeholder='Telemóvel' style='width: 100%; margin: auto; height: 100%;' value='{9}' {15}/>
                                                    </div>",
                                                myReader["OPERADORESID"].ToString(),
                                                myReader["CODIGO"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["NOTAS"].ToString().Trim(),
                                                myReader["EMAIL"].ToString(),
                                                myReader["PASSWORD"].ToString(),
                                                myReader["LASTLOGIN"].ToString(),
                                                myReader["LASTLOGIN"].ToString(),
                                                myReader["TELEFONE"].ToString(),
                                                myReader["TELEMOVEL"].ToString(),
                                                myReader["ADMINISTRADOR"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["ESCRITA"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["LEITURA"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["ATIVO"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["VISIVEL"].ToString().Trim() == "1" ? "checked" : "",
                                                administrador == "0" ? (id != meuID ? "readonly" : "") : "");

                    if(administrador == "1") {
                        table.AppendFormat(@"<div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='administrador' name='administrador' style='width: 100%; margin: auto; height: 100%;' {10}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='escrita' name='escrita' style='width: 100%; margin: auto; height: 100%;' {11}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='leitura' name='leitura' style='width: 100%; margin: auto; height: 100%;' {12}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='ativo' name='ativo' style='width: 100%; margin: auto; height: 100%;' {13}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='visivel' name='visivel' style='width: 100%; margin: auto; height: 100%;' {14}/>
                                                    </div>",
                                                myReader["OPERADORESID"].ToString(),
                                                myReader["CODIGO"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["NOTAS"].ToString().Trim(),
                                                myReader["EMAIL"].ToString(),
                                                myReader["PASSWORD"].ToString(),
                                                myReader["LASTLOGIN"].ToString(),
                                                myReader["LASTLOGIN"].ToString(),
                                                myReader["TELEFONE"].ToString(),
                                                myReader["TELEMOVEL"].ToString(),
                                                myReader["ADMINISTRADOR"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["ESCRITA"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["LEITURA"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["ATIVO"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["VISIVEL"].ToString().Trim() == "1" ? "checked" : "",
                                                administrador == "0" ? (id != meuID ? "readonly" : "") : "");
                    }

                    table.AppendFormat(@"           <div style='height:200px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='notas' name='notas' style='width: 100%; margin: auto; height: 100%;' rows='5' {15}>{3}</textarea>
                                                    </div>
                                                </div>
                                            </div>",
                                                myReader["OPERADORESID"].ToString(),
                                                myReader["CODIGO"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["NOTAS"].ToString().Trim(),
                                                myReader["EMAIL"].ToString(),
                                                myReader["PASSWORD"].ToString(),
                                                myReader["LASTLOGIN"].ToString(),
                                                myReader["LASTLOGIN"].ToString(),
                                                myReader["TELEFONE"].ToString(),
                                                myReader["TELEMOVEL"].ToString(),
                                                myReader["ADMINISTRADOR"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["ESCRITA"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["LEITURA"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["ATIVO"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["VISIVEL"].ToString().Trim() == "1" ? "checked" : "",
                                                administrador == "0" ? (id != meuID ? "readonly" : "") : "");
                }
                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                            <img src='img/icon_close.png' style='cursor:pointer;' onclick='closeOperatorInfo();'/>
                                        </div>
                                        <div style='height:auto' class='panel-heading' id='panel-heading'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem operadores a apresentar.</span>
                                        </div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                            <img src='img/icon_close.png' style='cursor:pointer;' onclick='closeOperatorInfo();'/>
                                        </div>
                                        <div style='height:auto' class='panel-heading' id='panel-heading'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem operadores a apresentar.</span>
                                        </div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string editar(string id_operador, string id_op, string codigo, string nome, string email, string password, string notas, string telefone,
        string telemovel, string administrador, string escrita, string leitura, string ativo, string visivel)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @id_op int = {13};
                                            DECLARE @codigo char(30) = '{1}';
                                            DECLARE @nome varchar(60) = '{2}';
                                            DECLARE @email varchar(60) = '{3}';
                                            DECLARE @password varchar(60) = '{4}';
                                            DECLARE @notas varchar(max) = '{5}';
                                            DECLARE @telefone varchar(20) = '{6}';
                                            DECLARE @telemovel varchar(20) = '{7}';
                                            DECLARE @administrador bit = {8};
                                            DECLARE @escrita bit = {9};
                                            DECLARE @leitura bit = {10};
                                            DECLARE @ativo bit = {11};
                                            DECLARE @visivel bit = {12};
                                            DECLARE @res int;
                                            DECLARE @codigoAntigo varchar(30) = (select ltrim(rtrim(codigo)) from operadores where @id_op = operadoresid);
                                            

                                            EXEC ALTERA_OPERADOR @id_operador, @id_op, @codigo, @nome, @email, @password, @notas, @telefone,
                                                @telemovel, @administrador, @escrita, @leitura, @ativo, @visivel, @res output

                                            SELECT @codigoAntigo as codigoAntigo",
                                                id_operador, codigo, nome, email, password, notas, telefone,
                                                telemovel, 
                                                administrador == "" ? "0" : administrador,
                                                escrita == "" ? "0" : escrita,
                                                leitura == "" ? "0" : leitura,
                                                ativo == "" ? "0" : ativo,
                                                visivel == "" ? "0" : visivel, 
                                                id_op);
            //return sql;
            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"Operador '{0}' atualizado com sucesso!", codigo);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    string codigoAntigo = myReader["codigoAntigo"].ToString();

                    try {
                        System.IO.File.Copy("img//users//" + codigoAntigo + ".jpg", "img//users//" + codigo + ".jpg", true);
                        System.IO.File.Delete(@"img//users//" + codigoAntigo + ".jpg");
                    } catch(Exception err) {

                    }
                }
            }

            connection.Close();

            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao atualizar Operador '{0}'!", codigo);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao atualizar operador '{0}'!", codigo);
        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string criar(string id_operador, string codigo, string nome, string email, string password, string notas, string telefone,
        string telemovel, string administrador, string escrita, string leitura, string ativo, string visivel)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @codigo char(30) = '{1}';
                                            DECLARE @nome varchar(60) = '{2}';
                                            DECLARE @email varchar(60) = '{3}';
                                            DECLARE @password varchar(60) = '{4}';
                                            DECLARE @notas varchar(max) = '{5}';
                                            DECLARE @telefone varchar(20) = '{6}';
                                            DECLARE @telemovel varchar(20) = '{7}';
                                            DECLARE @administrador bit = {8};
                                            DECLARE @escrita bit = {9};
                                            DECLARE @leitura bit = {10};
                                            DECLARE @ativo bit = {11};
                                            DECLARE @visivel bit = {12};
                                            DECLARE @res int;

                                            EXEC CRIA_OPERADOR @id_operador, @codigo, @nome, @email, @password, @notas, @telefone,
                                                @telemovel, @administrador, @escrita, @leitura, @ativo, @visivel, @res output",
                                                id_operador, codigo, nome, email, password, notas, telefone,
                                                telemovel, administrador, escrita, leitura, ativo, visivel);
            //return sql;
            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"Operador '{0}' criado com sucesso!", codigo);
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao criar operador '{0}'!", codigo);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao criar operador '{0}'!", codigo);
        connection.Close();
        return table.ToString();
    }



    [WebMethod]
    public static string apagar(string id_operador, string id_op)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @id_op int = {1};
                                            DECLARE @res int;

                                            EXEC APAGA_OPERADOR @id_operador, @id_op, @res output",
                                                id_operador, id_op);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"Operador apagado com sucesso!");
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao apagar operador!");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao apagar operador!");
        connection.Close();
        return table.ToString();
    }
}
