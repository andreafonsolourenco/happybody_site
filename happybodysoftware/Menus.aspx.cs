using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.Net.Mail;
using System.IO;

public partial class Menus : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "Menus"))
            {
                
            }
        }
    }

    [WebMethod]
    public static string loadGrid(string filtro)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @filtro varchar(max) = {0};
                                        
                                            SELECT 
                                                MENUSID,
                                                TITULO,
                                                PAGINA,
                                                NOTAS,
                                                CAST(ADMINISTRADOR as INT) as ADMINISTRADOR,
                                                CAST(VISIVEL as INT) as VISIVEL,
                                                ORDEM
                                            FROM MENUS
                                            WHERE TITULO NOT IN ('Sair', 'Mudar de Utilizador')
                                            and (
                                                @filtro is null or (
                                                    TITULO LIKE '%' + @filtro + '%'
                                                    OR PAGINA LIKE '%' + @filtro + '%'
                                                )
                                            )
                                            ORDER BY ORDEM", string.IsNullOrEmpty(filtro) ? "NULL" : "'" + filtro + "'");

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerColspan'>MENUS</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr style='cursor:pointer;' ondblclick='loadEdit({0});'>
                                            <td>{1}</td>
                                        </tr>",
                                                oDs.Tables[0].Rows[i]["MENUSID"].ToString(),
                                                oDs.Tables[0].Rows[i]["TITULO"].ToString(),
                                                oDs.Tables[0].Rows[i]["PAGINA"].ToString(),
                                                oDs.Tables[0].Rows[i]["NOTAS"].ToString(),
                                                oDs.Tables[0].Rows[i]["ADMINISTRADOR"].ToString(),
                                                oDs.Tables[0].Rows[i]["VISIVEL"].ToString(),
                                                oDs.Tables[0].Rows[i]["ORDEM"].ToString());
                }

                table.AppendFormat("</tbody></table>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", oDs.Tables[0].Rows.Count.ToString());
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='tableGrid'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem menus a apresentar</div>");
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='tableGrid'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Erro ao carregar menus: {0}</div>", exc.ToString());
        }

        return table.ToString();
    }

    [WebMethod]
    public static string criarMenu(string operatorID, string titulo, string pagina, string notas, string administrador, string visivel, string ordem)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id int;
                                            DECLARE @titulo varchar(200) = '{0}';
                                            DECLARE @pagina varchar(max) = '{1}';
                                            DECLARE @notas varchar(max) = '{2}';
                                            DECLARE @administrador bit = {3};
                                            DECLARE @visivel bit = {4};
                                            DECLARE @ordem int = {5};
                                            DECLARE @id_op int = {6}
                                            DECLARE @codOp char(30) = (select codigo from operadores where operadoresid = @id_op);
                                        
                                            INSERT INTO MENUS(TITULO, PAGINA, NOTAS, CTRLCODOP, administrador, visivel, ordem)
                                            VALUES(@titulo, @pagina, @notas, @codOp, @administrador, @visivel, @ordem)

                                            SET @id = scope_identity();

                                            INSERT INTO PERMISSOES(ID_OP, ID_MENU, ID_SUBMENU)
					                        SELECT operadoresid, men.MENUSID, NULL
					                        FROM MENUS men
					                        left join submenus sub on sub.id_menu = men.menusid
					                        inner join operadores op on op.operadoresid <> 0
                                            left join permissoes perm on perm.id_menu = men.menusid and perm.id_submenu is null and perm.id_op = op.operadoresid
					                        where sub.submenusid is null
					                        and men.administrador = 0
					                        and men.pagina <> ''
                                            and men.menusid = @id
                                            and perm.permissoesid is null
				    
					                        INSERT INTO PERMISSOES(ID_OP, ID_MENU, ID_SUBMENU)
					                        SELECT operadoresid, men.MENUSID, submenusid
					                        FROM MENUS men
					                        inner join submenus sub on sub.id_menu = men.menusid
					                        inner join operadores op on op.operadoresid <> 0
                                            left join permissoes perm on perm.id_menu = men.menusid and perm.id_submenu = sub.submenusid and perm.id_op = op.operadoresid
					                        where men.administrador = 0
					                        and sub.administrador = 0
                                            and men.menusid = @id
                                            and perm.permissoesid is null
                                            
                                            SELECT @id as ret", titulo, pagina, notas, administrador, visivel, ordem, operatorID);

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"{0}", oDs.Tables[0].Rows[i]["ret"].ToString());
                }
            }
            else
            {
                table.AppendFormat("-1");
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("-1");
        }

        return table.ToString();
    }

    [WebMethod]
    public static string deleteMenu(string id)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id int = {0};
                                            DECLARE @ret int;
                                            EXEC APAGA_MENU @id, @ret output
                                            SELECT @ret as ret", id);


            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"{0}", oDs.Tables[0].Rows[i]["ret"].ToString());
                }
            }
            else
            {
                table.AppendFormat("-1");
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("-1");
        }

        return table.ToString();
    }

    [WebMethod]
    public static string atualizarMenu(string operatorID, string titulo, string pagina, string notas, string administrador, string visivel, string ordem, string menuID)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id int = {7};
                                            DECLARE @titulo varchar(200) = '{0}';
                                            DECLARE @pagina varchar(max) = '{1}';
                                            DECLARE @notas varchar(max) = '{2}';
                                            DECLARE @administrador bit = {3};
                                            DECLARE @visivel bit = {4};
                                            DECLARE @ordem int = {5};
                                            DECLARE @id_op int = {6}
                                            DECLARE @codOp char(30) = (select codigo from operadores where operadoresid = @id_op);
                                        
                                            UPDATE MENUS
                                            SET TITULO = @titulo, PAGINA = @pagina, NOTAS = @notas, CTRLCODOP = @codOp, CTRLDATA = GETDATE(), administrador = @administrador, visivel = @visivel, ordem = @ordem
                                            WHERE MENUSID = @id
                                            
                                            SELECT 0 as ret", titulo, pagina, notas, administrador, visivel, ordem, operatorID, menuID);


            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"{0}", oDs.Tables[0].Rows[i]["ret"].ToString());
                }
            }
            else
            {
                table.AppendFormat("-1");
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("-1");
        }

        return table.ToString();
    }

    [WebMethod]
    public static string loadEditMenu(string id)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id int = {0};
                                        
                                            SELECT DISTINCT
                                                men.MENUSID,
                                                men.TITULO,
                                                men.PAGINA,
                                                men.NOTAS,
                                                CAST(men.ADMINISTRADOR as INT) as ADMINISTRADOR,
                                                CAST(men.VISIVEL as INT) as VISIVEL,
                                                men.ORDEM,
                                                CASE WHEN sub.submenusid is null then 0 else 1 end as TEM_SUBMENUS
                                            FROM MENUS men
                                            LEFT JOIN SUBMENUS sub on sub.ID_MENU = men.MENUSID
                                            where menusid = @id", id);

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>
                                                <input id='btnSave' value='Guardar' runat='server' type='button' onclick='edit();' style='background-color: #4282b5; float: left;
                                                        width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                                <input id='btnApagar' value='Apagar' runat='server' type='button' onclick='remove();' style='background-color: #4282b5; float: left;
                                                        width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                                <img src='img/icons/icon_close.png' style='cursor:pointer; float: right; vertical-align: middle;' onclick='closeEdit();'/>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>
                                                {7}
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                                                Título
                                                <input type='text' class='form-control' id='tituloEdit' placeholder='Título' required='required' style='width: 100%; margin: auto;' value='{1}'/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                                                Página
                                                <input type='text' class='form-control' id='paginaEdit' placeholder='Página' required='required' style='width: 100%; margin: auto;' value='{2}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                Ordem
                                                <input type='number' class='form-control' id='ordemEdit' placeholder='Ordem' required='required' style='width: 100%; margin: auto;' value='{6}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                Administrador
                                                <input type='checkbox' class='form-control' id='administradorEdit' style='width: 100%; margin: auto; height: 50px;' {4}/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                Visivel
                                                <input type='checkbox' class='form-control' id='visivelEdit' style='width: 100%; margin: auto; height: 50px;' {5}/>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 line'>
                                                Notas
                                                <textarea class='form-control' id='notasEdit' name='notasEdit' style='width: 100%; margin: auto; height: auto;' rows='5'>{3}</textarea>
                                            </div>",
                                                oDs.Tables[0].Rows[i]["MENUSID"].ToString(),
                                                oDs.Tables[0].Rows[i]["TITULO"].ToString(),
                                                oDs.Tables[0].Rows[i]["PAGINA"].ToString(),
                                                oDs.Tables[0].Rows[i]["NOTAS"].ToString(),
                                                oDs.Tables[0].Rows[i]["ADMINISTRADOR"].ToString() == "1" ? " checked " : "",
                                                oDs.Tables[0].Rows[i]["VISIVEL"].ToString() == "1" ? " checked " : "",
                                                oDs.Tables[0].Rows[i]["ORDEM"].ToString(),
                                                oDs.Tables[0].Rows[i]["TEM_SUBMENUS"].ToString() == "1" ? "Tem Submenus Associados" : "");
                }

                table.AppendFormat("</tbody></table>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", oDs.Tables[0].Rows.Count.ToString());
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='tableGrid'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem menus a apresentar</div>");
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='tableGrid'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Erro ao carregar menus: {0}</div>", exc.ToString());
        }

        return table.ToString();
    }
}
