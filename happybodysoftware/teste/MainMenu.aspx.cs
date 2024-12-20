using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Text;
using System.Security.Cryptography;
using System.Web.Services;

public partial class MainMenu : Page
{
    string separador = "";
    string id = "";
    Boolean isAdmin = false;
    StringBuilder table = new StringBuilder();
    string[] ids;

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        separador = HttpContext.Current.Request.Url.PathAndQuery;
        id = Request.QueryString["id"];

        checkSessionID();
        string page = loadOperatorName();

        if (id == "-1")
        {
            Response.Redirect(page);
        }

        if (!IsPostBack)
        {
            ClientScriptManager oCsm = this.Page.ClientScript;
            if (!oCsm.IsStartupScriptRegistered(GetType(), "MainMenu"))
            {

            }
        }

        table.AppendFormat(@"{0}", page);
        ids = countMenus().Trim().Split(',');
        table.AppendFormat(@"<span class='variaveis' id='nrMenus'>{0}</span>", ids.Length.ToString());

        //for (int i = 0; i < ids.Length; i++)
        //{
        //    table.AppendFormat(@"{0}{1}", getMenus(ids[i], i), getSubMenus(ids[i], i.ToString()));
        //}

        //menu.InnerHtml = table.ToString();

        getMenus();

        if (isAdmin)
        {
            loadCV();
            loadAvaliacoes();
            //loadAvisos();
        }
        else
        {
            //lblcv.InnerHtml = "0";
            //lblavaliacoestresdias.InnerHtml = "";
            //lblavaliacoeshoje.InnerHtml = "";
        }

        //lbloperatorid.InnerHtml = id;
        //lblip.InnerHtml = Request.ServerVariables["REMOTE_HOST"];
    }

    private void checkSessionID()
    {
        string EncryptionKey = "GinasioHappyBody";
        id = id.Replace(" ", "+");
        byte[] cipherBytes = null;

        try
        {
            cipherBytes = Convert.FromBase64String(id);
        }
        catch (Exception e)
        {
            Response.Redirect("login.aspx?msg=Utilizador%20inválido");
        }
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                id = Encoding.Unicode.GetString(ms.ToArray());
            }
        }

        //lblmacaddress.InnerHtml = id;

        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_sessao INT = {0};
                                        
                                            SELECT ID_OP FROM OPERADORES_SESSAO WHERE OPERADORES_SESSAOID = @id_sessao AND DATA_FIM IS NULL", id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["ID_OP"].ToString()) < 0)
                    {
                        Response.Redirect("login.aspx?msg='Utilizador Inválido!'");
                        return;
                    }

                    id = myReader["ID_OP"].ToString();
                }
                return;
            }
            else
            {
                connection.Close();
                id = "-1";
                Response.Redirect("login.aspx?msg='Utilizador Inválido!'");
                return;
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            id = "-1";
            Response.Redirect("login.aspx?msg='Utilizador Inválido!'");
            return;
        }
    }

    private string loadOperatorName()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id INT = {0};
                                        
                                            SELECT 
                                                LTRIM(RTRIM(NOME)) as NOME,
                                                LTRIM(RTRIM(CODIGO)) as CODIGO,
                                                CAST(ADMINISTRADOR as INT) as ADMIN,
                                                CONVERT(VARCHAR(10), dateadd(hh, -1, getdate()), 103) as CURR_DATE,
                                                DATEPART(dw, DATEADD(hh, -1, GETDATE())) as WEEK_DAY
                                            FROM OPERADORES
                                            WHERE OPERADORESID = @id", id);

            int admin = 0;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    operatorCode.InnerHtml = myReader["CODIGO"].ToString();
                    String[] substrings = myReader["NOME"].ToString().Trim().Split(' ');
                    string name = substrings[0] + " " + substrings[substrings.Length - 1];
                    operatorPhoto.InnerHtml = "img/users/" + myReader["CODIGO"].ToString() + ".jpg";
                    operatorName.InnerHtml = name;
                    string dia = "";

                    switch(myReader["WEEK_DAY"].ToString()) {
                        case "1": dia = "Domingo";
                            break;
                        case "2": dia = "Segunda-Feira";
                            break;
                        case "3": dia = "Terça-Feira";
                            break;
                        case "4": dia = "Quarta-Feira";
                            break;
                        case "5": dia = "Quinta-Feira";
                            break;
                        case "6": dia = "Sexta-Feira";
                            break;
                        case "7": dia = "Sábado";
                            break;
                    }

                    table.AppendFormat(@"   <div class='menuTitles'>{0}</div>
                                            <div class='menuTitles'>{1}</div>", name, (dia + ", " + myReader["CURR_DATE"].ToString()));

                    admin = Convert.ToInt32(myReader["ADMIN"].ToString());

                    isAdmin = Convert.ToBoolean(admin); 
                }
            }
            else
            {
                connection.Close();
                return "login.aspx?msg=Utilizador inválido";
            }

            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            connection.Close();
            return "login.aspx?msg=Utilizador inválido";
        }

        connection.Close();
        return "login.aspx?msg=Utilizador inválido";
    }

    private void loadCV()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                        
                                            SELECT 
                                                COUNT(WS_CANDIDATURASID) as CONTA
                                            FROM WS_CANDIDATURAS
                                            WHERE LIDA = 0", id);

            int admin = 0;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    //lblcv.InnerHtml = myReader["CONTA"].ToString();
                }
            }
            else
            {
                connection.Close();
                //lblcv.InnerHtml = "0";
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            //lblcv.InnerHtml = "0";
        }
    }

    private void loadAvaliacoes()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @data_atual datetime = DATEADD(hh, -1, GETDATE());
					                        DECLARE @data_anterior datetime = DATEADD(dd, 3, @data_atual);
                                        
					                        select *
					                        from (
                                                SELECT 
                                                    soc.NR_SOCIO,
						                            soc.NOME,
						                            CONVERT(VARCHAR(5), av.DATA_PROXIMA_AVALIACAO, 108) as HORA,
						                            0 as HOJE
                                                FROM AVALIACOES_FISICAS av
					                            INNER JOIN SOCIOS soc on soc.sociosid = av.id_socio
					                            WHERE CAST(av.DATA_PROXIMA_AVALIACAO as DATE) = CAST(@data_anterior as date)
					                            UNION
					                            SELECT 
                                                    soc.NR_SOCIO,
						                            soc.NOME,
						                            CONVERT(VARCHAR(5), av.DATA_PROXIMA_AVALIACAO, 108) as HORA,
						                            1 as HOJE
                                                FROM AVALIACOES_FISICAS av
					                            INNER JOIN SOCIOS soc on soc.sociosid = av.id_socio
					                            WHERE CAST(av.DATA_PROXIMA_AVALIACAO as DATE) = CAST(@data_atual as date)
					                            ) as tmp
					                            order by HOJE asc");

            string hoje = "", tresdias = "";

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if(myReader["HOJE"].ToString() == "1")
                        hoje += myReader["NR_SOCIO"].ToString() + ": " + myReader["HORA"].ToString() + "<br />";
                    else
                        tresdias += myReader["NR_SOCIO"].ToString() + ": " + myReader["HORA"].ToString() + "<br />";
                }

                //lblavaliacoestresdias.InnerHtml = tresdias;
                //lblavaliacoeshoje.InnerHtml = hoje;
            }
            else
            {
                connection.Close();
                //lblavaliacoestresdias.InnerHtml = "";
                //lblavaliacoeshoje.InnerHtml = "";
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            //lblavaliacoestresdias.InnerHtml = "";
            //lblavaliacoeshoje.InnerHtml = "";
        }
    }

    private string countMenus()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @admin bit = (SELECT ADMINISTRADOR FROM OPERADORES WHERE OPERADORESID = @id_operador);
                                        
                                            IF(@admin = 0)
                                            BEGIN
                                                SELECT
                                                    distinct MENUSID, men.ADMINISTRADOR, men.ORDEM
                                                FROM MENUS men
						                        left join submenus sub on sub.id_menu = men.menusid
						                        left join permissoes perm on (perm.id_menu = men.menusid or (perm.id_submenu = sub.submenusid and sub.submenusid is not null))
                                                WHERE men.VISIVEL = 1
                                                AND @admin = men.ADMINISTRADOR
						                        and ((perm.leitura = 1 and @id_operador = perm.id_op) or (sub.submenusid is null and men.pagina = ''))
                                                ORDER BY men.ADMINISTRADOR, men.ORDEM, men.menusid
                                            END
                                            ELSE
                                            BEGIN
                                                SELECT
                                                    MENUSID
                                                FROM MENUS men
                                                WHERE VISIVEL = 1
                                                ORDER BY ADMINISTRADOR DESC, ORDEM ASC
                                            END", id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"{0}", conta == 0 ? myReader["MENUSID"].ToString() : ("," + myReader["MENUSID"].ToString()));

                    conta++;
                }
            }
            else
            {
                connection.Close();
                return "0,0";
            }

            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            connection.Close();
            return "0,0";
        }

        connection.Close();
        return "0,0";
    }

    //private string getMenus(string menuID, int conta)
    //{
    //    var table = new StringBuilder();
    //    string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
    //    SqlConnection connection = new SqlConnection(connectionstring);

    //    try
    //    {
    //        connection.Open();

    //        string sql = string.Format(@"   SET DATEFORMAT dmy;
    //                                        DECLARE @id_operador int = {0};
    //                                        DECLARE @admin bit = (SELECT ADMINISTRADOR FROM OPERADORES WHERE OPERADORESID = @id_operador);
    //                                        DECLARE @menuID int = {1};

    //                                        IF(@admin = 0)
    //                                        BEGIN
    //                                            SELECT
    //                                                men.MENUSID,
    //                                                men.TITULO,
    //                                             men.PAGINA,
    //                                                COUNT(sub.SUBMENUSID) as SUBMENUS
    //                                            FROM MENUS men
    //                                            LEFT JOIN SUBMENUS sub on sub.ID_MENU = men.MENUSID
    //                                            left join permissoes perm on (perm.id_menu = men.menusid or (perm.id_submenu = sub.submenusid and sub.submenusid is not null))
    //                                            WHERE men.VISIVEL = 1
    //                                            AND @admin = men.ADMINISTRADOR
    //                                            and ((perm.leitura = 1 and @id_operador = perm.id_op) or (sub.submenusid is null and men.pagina = ''))
    //                                            AND men.MENUSID = @menuID
    //		                        GROUP BY men.ADMINISTRADOR, men.ORDEM, men.MENUSID, men.TITULO, men.PAGINA
    //                                            ORDER BY men.ADMINISTRADOR desc, men.ORDEM asc, men.MENUSID, men.TITULO, men.PAGINA DESC
    //                                        END
    //                                        ELSE
    //                                        BEGIN
    //                                            SELECT
    //                                                menu.MENUSID,
    //                                                menu.TITULO,
    //                                             menu.PAGINA,
    //                                                COUNT(sub.SUBMENUSID) as SUBMENUS
    //                                            FROM MENUS menu
    //                                            LEFT JOIN SUBMENUS sub on sub.ID_MENU = menu.MENUSID
    //                                            WHERE menu.VISIVEL = 1
    //                                            AND menu.MENUSID = @menuID
    //		                        GROUP BY menu.ADMINISTRADOR, menu.ORDEM, menu.MENUSID, menu.TITULO, menu.PAGINA
    //                                            ORDER BY menu.ADMINISTRADOR desc, menu.ORDEM asc, menu.MENUSID, menu.TITULO, menu.PAGINA DESC
    //                                        END", id, menuID);

    //        SqlCommand myCommand = new SqlCommand(sql, connection);
    //        SqlDataReader myReader = myCommand.ExecuteReader();
    //        SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

    //        if (myReader.HasRows)
    //        {
    //            while (myReader.Read())
    //            {
    //                table.AppendFormat(@"   <div class='menuLine clickable' onclick='openPage({1});'>
    //                                            {0}
    //                                            <span class='variaveis' id='menuid_{1}'>{2}</span>
    //                                            <span class='variaveis' id='pagina_{1}'>{3}</span>
    //                                            <span class='variaveis' id='titulo_{1}'>{0}</span>
    //                                            <span class='variaveis' id='submenus_{1}'>{4}</span>
    //                                        </div>", myReader["TITULO"].ToString(),
    //                                               conta.ToString(),
    //                                               myReader["MENUSID"].ToString(),
    //                                               myReader["PAGINA"].ToString(),
    //                                               myReader["SUBMENUS"].ToString());

    //                conta++;
    //            }
    //        }
    //        else
    //        {
    //            connection.Close();
    //            return "";
    //        }

    //        connection.Close();
    //        return table.ToString();
    //    }
    //    catch (Exception exc)
    //    {
    //        connection.Close();
    //        return "";
    //    }

    //    connection.Close();
    //    return "";
    //}

    private void getMenus()
    {
        var table = new StringBuilder();
        var table2 = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @admin bit = (SELECT ADMINISTRADOR FROM OPERADORES WHERE OPERADORESID = @id_operador);
                                            DECLARE @menuID int;
                                            DECLARE @visivel bit = 1;
                                        
                                            SELECT
                                                rpt.MENUSID,
                                                rpt.TITULO,
	                                            rpt.PAGINA,
                                                rpt.ADMINISTRADOR,
                                                rpt.ORDEM,
                                                rpt.HAVE_SUBMENUS
                                            FROM HB_SOFTWARE_REPORT_MENUS(@menuID, @visivel) rpt
                                            WHERE ((@admin = rpt.ADMINISTRADOR and @admin = 0) or (@admin = 1)) 
						                    GROUP BY rpt.ADMINISTRADOR, rpt.ORDEM, rpt.HAVE_SUBMENUS, rpt.MENUSID, rpt.TITULO, rpt.PAGINA
                                            ORDER BY rpt.ADMINISTRADOR desc, rpt.ORDEM asc, rpt.HAVE_SUBMENUS asc, rpt.MENUSID asc, rpt.TITULO asc, rpt.PAGINA asc", id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"<li class='active'><a href='#'>Home</a></li>");
            table2.AppendFormat(@"<li class='nav-item' active><a class='nav-link' href='#'>Home</a></li>");

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if(myReader["HAVE_SUBMENUS"].ToString() == "1")
                    {
                        table.AppendFormat(@"   <li>
                                                    <a href='#submenu{1}' data-toggle='collapse' aria-expanded='false' class='dropdown-toggle'>{0}</a>
                                                    <ul class='collapse list-unstyled' id='submenu{1}'>
                                                        [SUBMENU]
                                                    </ul>
                                                </li>", myReader["TITULO"].ToString(),
                                                   myReader["MENUSID"].ToString(),
                                                   myReader["PAGINA"].ToString());

                        table.Replace("[SUBMENU]", getSubmenu(myReader["MENUSID"].ToString()));

                        table2.AppendFormat(@"   <li class='nav-item'>
                                                    <a class='nav-link' href='#navsubmenu{1}' data-toggle='collapse' aria-expanded='false' class='dropdown-toggle'>{0}</a>
                                                    <ul class='collapse list-unstyled' id='navsubmenu{1}'>
                                                        [SUBMENU]
                                                    </ul>
                                                </li>", myReader["TITULO"].ToString(),
                                                   myReader["MENUSID"].ToString(),
                                                   myReader["PAGINA"].ToString());

                        table2.Replace("[SUBMENU]", getNavbarSubmenu(myReader["MENUSID"].ToString()));
                    }
                    else
                    {
                        table.AppendFormat(@"   <li>
                                                    <a href='#'>{0}</a>
                                                </li>", myReader["TITULO"].ToString(),
                                                   myReader["MENUSID"].ToString(),
                                                   myReader["PAGINA"].ToString());

                        table2.AppendFormat(@"<li class='nav-item'><a class='nav-link' href='#'>{0}</a><li>", myReader["TITULO"].ToString(),
                                                   myReader["MENUSID"].ToString(),
                                                   myReader["PAGINA"].ToString());
                    }
                }
            }
            else
            {
                connection.Close();
            }

            connection.Close();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"<li><a href='#'>{0}</a></li>", exc.ToString());
            connection.Close();
        }

        menu.InnerHtml = table.ToString();
        navbarmenu.InnerHtml = table2.ToString();
    }

    //private string getSubMenus(string menuID, string menuConta)
    //{
    //    var table = new StringBuilder();
    //    string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
    //    SqlConnection connection = new SqlConnection(connectionstring);

    //    try
    //    {
    //        connection.Open();

    //        string sql = string.Format(@"   SET DATEFORMAT dmy;
    //                                        declare @id_operador int = {0};
    //                                        DECLARE @admin bit = (SELECT ADMINISTRADOR FROM OPERADORES WHERE OPERADORESID = @id_operador);
    //                                        DECLARE @menuID int = {1};

    //                                        IF(@admin = 0)
    //                                        BEGIN
    //                                            SELECT
    //                                                SUBMENUSID,
    //                                                TITULO,
    //                                             PAGINA
    //                                            FROM SUBMENUS sub
    //                                            inner join permissoes perm on perm.id_op = @id_operador and sub.submenusid = perm.id_submenu and perm.leitura = 1
    //                                            WHERE VISIVEL = 1
    //                                            AND @admin = ADMINISTRADOR
    //                                            AND sub.ID_MENU = @menuID
    //                                            ORDER BY ADMINISTRADOR DESC
    //                                        END
    //                                        ELSE
    //                                        BEGIN
    //                                            SELECT
    //                                                SUBMENUSID,
    //                                                TITULO,
    //                                             PAGINA
    //                                            FROM SUBMENUS
    //                                            WHERE VISIVEL = 1
    //                                            AND ID_MENU = @menuID
    //                                            ORDER BY ADMINISTRADOR DESC
    //                                        END", id, menuID);

    //        SqlCommand myCommand = new SqlCommand(sql, connection);
    //        SqlDataReader myReader = myCommand.ExecuteReader();
    //        SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
    //        int conta = 0;

    //        if (myReader.HasRows)
    //        {
    //            while (myReader.Read())
    //            {
    //                table.AppendFormat(@"   <div class='menuLine clickable subMenuLine' id='subMenu_{4}_{1}' onclick='openSubMenuPage({4}, {1});'>
    //                                            {0}
    //                                            <span class='variaveis' id='submenuid_{4}_{1}'>{2}</span>
    //                                            <span class='variaveis' id='pagina_{4}_{1}'>{3}</span>
    //                                            <span class='variaveis' id='titulo_{4}_{1}'>{0}</span>
    //                                        </div>", myReader["TITULO"].ToString(),
    //                                               conta.ToString(),
    //                                               myReader["SUBMENUSID"].ToString(),
    //                                               myReader["PAGINA"].ToString(),
    //                                               menuConta.ToString());

    //                conta++;
    //            }
    //        }
    //        else
    //        {
    //            connection.Close();
    //            return "";
    //        }

    //        connection.Close();
    //        return table.ToString();
    //    }
    //    catch (Exception exc)
    //    {
    //        connection.Close();
    //        return "";
    //    }

    //    connection.Close();
    //    return "";
    //}

    private string getSubmenu(string menuID)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            declare @id_operador int = {0};
                                            DECLARE @admin bit = (SELECT ADMINISTRADOR FROM OPERADORES WHERE OPERADORESID = @id_operador);
                                            DECLARE @menuID int = {1};
                                            DECLARE @submenuID int;
                                            DECLARE @visivel bit = 1;

                                            SELECT
                                                rpt.SUBMENUSID,
                                                rpt.TITULO,
                                                rpt.PAGINA,
                                                rpt.ADMINISTRADOR
                                            FROM HB_SOFTWARE_REPORT_SUBMENUS(@menuID, @submenuID, @visivel) rpt
                                            left join HB_SOFTWARE_REPORT_PERMISSOES(@menuID, @submenuID, @id_operador) perm on rpt.submenusid = perm.id_submenu
                                            WHERE ((@admin = ADMINISTRADOR and @admin = 0 and perm.leitura = 1) or (@admin = 1)) 
                                            ORDER BY ADMINISTRADOR DESC, TITULO ASC", id, menuID);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"   <li><a href='#'>{0}</a></li>", myReader["TITULO"].ToString(),
                                                   myReader["SUBMENUSID"].ToString(),
                                                   myReader["PAGINA"].ToString());
                }

                connection.Close();
            }
            else
            {
                connection.Close();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"<li><a href='#'>{0}</a></li>", exc.ToString());
            connection.Close();
        }

        return table.ToString();
    }

    private string getNavbarSubmenu(string menuID)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            declare @id_operador int = {0};
                                            DECLARE @admin bit = (SELECT ADMINISTRADOR FROM OPERADORES WHERE OPERADORESID = @id_operador);
                                            DECLARE @menuID int = {1};
                                            DECLARE @submenuID int;
                                            DECLARE @visivel bit = 1;

                                            SELECT
                                                rpt.SUBMENUSID,
                                                rpt.TITULO,
                                                rpt.PAGINA,
                                                rpt.ADMINISTRADOR
                                            FROM HB_SOFTWARE_REPORT_SUBMENUS(@menuID, @submenuID, @visivel) rpt
                                            left join HB_SOFTWARE_REPORT_PERMISSOES(@menuID, @submenuID, @id_operador) perm on rpt.submenusid = perm.id_submenu
                                            WHERE ((@admin = ADMINISTRADOR and @admin = 0 and perm.leitura = 1) or (@admin = 1)) 
                                            ORDER BY ADMINISTRADOR DESC, TITULO ASC", id, menuID);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"   <li class='nav-link'><a href='#'>{0}</a></li>", myReader["TITULO"].ToString(),
                                                   myReader["SUBMENUSID"].ToString(),
                                                   myReader["PAGINA"].ToString());
                }

                connection.Close();
            }
            else
            {
                connection.Close();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"<li class='nav-link'><a href='#'>{0}</a></li>", exc.ToString());
            connection.Close();
        }

        return table.ToString();
    }


    private void loadAvisos()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @data_atual datetime = DATEADD(hh, -1, GETDATE());
                                        
					                        select
                                                av.NOTAS,
                                                av.VALOR,
                                                soc.NR_SOCIO
                                            FROM AVISOS av
                                            INNER JOIN SOCIOS soc on soc.sociosid = av.id_socio
                                            WHERE (CAST(av.DATA_AVISO as DATE) = CAST(@data_atual as DATE)
                                            OR av.DATA_AVISO IS NULL)
                                            ORDER BY soc.NR_SOCIO");

            string hoje = "";

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    hoje += "Sócio Nº" + myReader["NR_SOCIO"].ToString() + ": " + myReader["NOTAS"].ToString() + " - " + myReader["VALOR"].ToString() + "€<br />";
                }

                //lblavisos.InnerHtml = hoje;
            }
            else
            {
                connection.Close();
                //lblavisos.InnerHtml = "";
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            //lblavisos.InnerHtml = "";
        }
    }

    [WebMethod]
    public static string getOperator(string user, string pass, string idAntigo)
    {
        var table = new StringBuilder();

        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @user char(30) = '{0}';
                                            DECLARE @pass varchar(60) = '{1}';
                                            DECLARE @id_op int;
                                            DECLARE @ret int;
                                            DECLARE @id_sessao_antiga int = {2};

                                            EXEC TERMINA_SESSAO @id_sessao_antiga, @ret output;
                                        
                                            SELECT 
                                                @id_op = OPERADORESID
                                            FROM OPERADORES
                                            WHERE LTRIM(RTRIM(codigo)) = @user
                                            AND LTRIM(RTRIM(password)) = @pass
                                            AND ATIVO = 1

                                            UPDATE OPERADORES_SESSAO
                                            SET DATA_FIM = GETDATE()
                                            WHERE ID_OP = @id_op
                                            AND DATA_INICIO IS NOT NULL
                                            AND DATA_FIM IS NULL

                                            EXEC CRIA_SESSAO @id_op, @ret output

                                            SELECT @ret as ret", user, pass, idAntigo);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["ret"].ToString()) < 0)
                        table.AppendFormat(@"Utilizador não encontrado!");
                    else
                        table.AppendFormat(@"{0}", myReader["ret"].ToString());
                }

                connection.Close();


                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"Utilizador não encontrado!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Utilizador não encontrado!");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string Encrypt(string str)
    {
        string EncryptionKey = "GinasioHappyBody";
        byte[] clearBytes = Encoding.Unicode.GetBytes(str);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                str = Convert.ToBase64String(ms.ToArray());
            }
        }
        return str;
    }

    [WebMethod]
    public static string cancelSession(string sessionID)
    {
        var table = new StringBuilder();

        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @ret int;
                                            DECLARE @id_sessao_antiga int = {0};

                                            EXEC TERMINA_SESSAO @id_sessao_antiga, @ret output;
                                            
                                            SELECT @ret as ret", sessionID);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"{0}", myReader["ret"].ToString());
                }

                connection.Close();


                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"-1");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"-1");
            connection.Close();
            return table.ToString();
        }
    }
}
