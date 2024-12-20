using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class OutrasPermissoes : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "OutrasPermissoes"))
            {
                
            }
        }

        loadOperadores();
    }

    private void loadOperadores()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                        
                                            SELECT 
                                                OPERADORESID as ID,
                                                LTRIM(RTRIM(NOME)) as NOME,
                                                LTRIM(RTRIM(CODIGO)) as CODIGO
                                            FROM OPERADORES
                                            WHERE ADMINISTRADOR = 0");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"<select class='form-control' id='selectOperadores' onchange='load();'>");

                while (myReader.Read())
                {
                    table.AppendFormat(@"<option value='{0}'>{1}</option>", myReader["ID"].ToString(),
                                                                                myReader["NOME"].ToString());
                }

                table.AppendFormat(@"</select>");

                connection.Close();
                divSelectOperadores.InnerHtml = table.ToString();
            }
            else
            {
                connection.Close();
                table.AppendFormat(@"<select class='form-control' id='selectOperadores'>");
                table.AppendFormat(@"<option value='0'>Não existem operadores a apresentar.</option>");
                table.AppendFormat(@"</select>");
                divSelectOperadores.InnerHtml = table.ToString();
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            table.AppendFormat(@"<select class='form-control' id='selectOperadores'>");
            table.AppendFormat(@"<option value='0'>Não existem operadores a apresentar.</option>");
            table.AppendFormat(@"</select>");
            divSelectOperadores.InnerHtml = table.ToString();
        }
    }

    [WebMethod]
    public static string loadPermissoes(string id_operador)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_op int = {0};

                                            select 
                                                id_permissao as id, id_op, op, id_menu, menu, id_submenu, submenu, tipo, escrita, leitura
                                            from REPORT_OUTRAS_PERMISSOES(@id_op, null, null, null)
                                            order by id_menu, id_submenu, tipo", id_operador);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                table.AppendFormat(@"<input id='btnSave' type='button' class='form-control' value='Guardar' style='width: 100%; height: 40px; font-size: small' onclick='guardaPermissoes();' />");

                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerLeft'>Pagina</th>
                                                    <th class='headerCenter'>Leitura</th>
                                                    <th class='headerRight'>Escrita</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr>
                                            <td class='variaveis' id='id_{0}'>{1}</td>
                                            <td>{2}{3} - {6}</td>
                                            <td><input type='checkbox' class='form-control' id='leitura_{0}' style='width: 100%; margin: auto;' {5} /></td>
                                            <td><input type='checkbox' class='form-control' id='escrita_{0}' style='width: 100%; margin: auto;' {4} /></td>
                                        </tr>", conta.ToString(),
                                                myReader["id"].ToString(),
                                                myReader["menu"].ToString(),
                                                myReader["submenu"].ToString() != "" ? "<br /><span style='font-size: small'>" + myReader["submenu"].ToString() + "</span>" : "",
                                                myReader["escrita"].ToString() == "1" ? "checked" : "",
                                                myReader["leitura"].ToString() == "1" ? "checked" : "",
                                                myReader["tipo"].ToString());

                    conta++;
                }

                table.AppendFormat("</tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='nrpermissoes'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                // Adiciona as linhas
                table.AppendFormat(@"Não existem permissões a apresentar para este Operador");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            // Adiciona as linhas
            table.AppendFormat(@"Não existem permissões a apresentar para este Operador");
            connection.Close();
            return table.ToString();
        }
    }


    [WebMethod]
    public static string alteraPermissao(string id_operador, string id_permissao, string escrita, string leitura)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @id_permissao int = {1};
                                            DECLARE @escrita bit = {2};
                                            DECLARE @leitura bit = {3};
                                            DECLARE @res int;
                                            DECLARE @msg varchar(max);

                                            EXEC ALTERA_OUTRAS_PERMISSOES @id_operador, @id_permissao, @escrita, @leitura, @res output, @msg output

                                            SELECT @res as res, @msg as msg", id_operador, id_permissao, escrita == "" ? "NULL" : escrita, leitura == "" ? "NULL" : leitura);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["res"].ToString()) < 0)
                        table.AppendFormat(@"Erro: {0}", myReader["msg"].ToString());
                    else
                        table.AppendFormat(@"{0}", myReader["msg"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                // Adiciona as linhas
                table.AppendFormat(@"Erro: ocorreu um erro ao alterar a permissão!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro: ocorreu um erro ao alterar a permissão!");
            connection.Close();
            return table.ToString();
        }
    }
}
