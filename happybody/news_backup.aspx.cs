using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Net.Mail;
using System.IO;
using System.Globalization;
using System.Net;

public partial class news_backup : Page
{
    StringBuilder modalidadesDescription = new StringBuilder();
    StringBuilder formModalidades = new StringBuilder();
    string pagina = "";
    string lingua = "";
    string aulagratuita = "";

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
        
        getLinkValues();
        getMenu();
        loadBackgroundImages();
        loadArtigosData();
        getCommentsForm();
        getLanguage();
    }

    private void loadArtigosData()
    {
        loadArtigos();
        divArtigosDescription.InnerHtml = modalidadesDescription.ToString();
    }

    private void getMenu()
    {
        var table = new StringBuilder();
        var language = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   DECLARE @about varchar(max);
                                            DECLARE @servicos varchar(max);
                                            DECLARE @modalidades varchar(max);
                                            DECLARE @noticias varchar(max);
                                            DECLARE @marcarvisita varchar(max);
                                            DECLARE @horarios varchar(max);
                                            DECLARE @staff varchar(max);
                                            DECLARE @contactos varchar(max);
                                            DECLARE @rspemail varchar(max);
                                            DECLARE @traducao varchar(max);
                                            DECLARE @titulo varchar(max);
                                            DECLARE @selectLanguage varchar(max);
                                            DECLARE @aulagratuita varchar(max);
                                            DECLARE @recrutamento varchar(max);
                                            DECLARE @artigos varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';
                                            
                                            SET @traducao = 'Sobre Happy Body®';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @about output;

                                            SET @traducao = 'Serviços';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @servicos output;

                                            SET @traducao = 'Notícias';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @noticias output;

                                            SET @traducao = 'Marcar Treino';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @marcarvisita output;

                                            SET @traducao = 'Horário de Aulas de Grupo';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @horarios output;

                                            SET @traducao = 'Staff';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @staff output;

                                            SET @traducao = 'Contactos';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @contactos output;

                                            SET @traducao = 'Modalidades';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @modalidades output;

                                            SET @traducao = 'Obrigado pelo seu contacto. Entraremos em contacto consigo o mais depressa possível!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @rspemail output;

                                            SET @traducao = 'Notícias';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @titulo output;

                                            SET @traducao = 'Modalidades';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @modalidades output;

                                            SET @traducao = 'Selecione Língua';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @selectLanguage output;

                                            SET @traducao = 'Marcar Treino Personalizado';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @aulagratuita output;

                                            SET @traducao = 'Recrutamento';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @recrutamento output;

                                            SET @traducao = 'Artigos';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @artigos output;

                                            SELECT 
                                                @about as about,
                                                @servicos as servicos,
                                                @modalidades as modalidades,
                                                @noticias as noticias,
                                                @marcarvisita as marcarvisita,
                                                @horarios as horarios,
                                                @staff as staff,
                                                @contactos as contactos,
                                                @titulo as pagetitle,
                                                @rspemail as rspemail,
                                                @selectLanguage as selectLanguage,
                                                @aulagratuita as aulagratuita,
                                                @recrutamento as recrutamento,
                                                @artigos as artigos,
                                                SERVIDOR_SITE
                                             FROM APPLICATION_CONFIG", lingua);

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                int count = oDs.Tables.Count - 1;

                for (int i = 0; i < oDs.Tables[count].Rows.Count; i++)
                {
                    table.AppendFormat(@"   <div class='container' id='containerMenu'>
                                                <!--<a class='navbar-brand js-scroll-trigger' href='#page-top'>HappyBody Gym</a>-->
                                                <button class='navbar-toggler navbar-toggler-right text-uppercase font-weight-bold bg-primary text-white rounded' type='button' data-toggle='collapse' data-target='#navbarResponsive' aria-controls='navbarResponsive' aria-expanded='false' aria-label='Toggle navigation'>
                                                    Menu
                                                    <i class='fas fa-bars'></i>
                                                </button>
                                                <div class='collapse navbar-collapse' id='navbarResponsive'>
                                                    <ul class='navbar-nav ml-auto'>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='index.aspx?language={9}&page=about'>{0}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='index.aspx?language={9}&page=services'>{1}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='index.aspx?language={9}&page=marcarVisita'>{4}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='index.aspx?language={9}&page=contact'>{7}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='#modalidades'>{2}</a>
                                                        </li>
                                                        <!--<li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='#horarios'>{5}</a>
                                                        </li>-->
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='staff.aspx?language={9}&page=staff'>{6}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='staff.aspx?language={9}&page=recrutamento'>{10}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='#news'>{3}</a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>",
                                            oDs.Tables[count].Rows[i]["about"].ToString(),
                                            oDs.Tables[count].Rows[i]["servicos"].ToString(),
                                            oDs.Tables[count].Rows[i]["modalidades"].ToString(),
                                            oDs.Tables[count].Rows[i]["noticias"].ToString(),
                                            oDs.Tables[count].Rows[i]["marcarvisita"].ToString(),
                                            oDs.Tables[count].Rows[i]["horarios"].ToString(),
                                            oDs.Tables[count].Rows[i]["staff"].ToString(),
                                            oDs.Tables[count].Rows[i]["contactos"].ToString(),
                                            language.ToString(),
                                            lingua,
                                            oDs.Tables[count].Rows[i]["recrutamento"].ToString());

                    pageTitle.InnerHtml = oDs.Tables[count].Rows[i]["pagetitle"].ToString();
                    newsTitle.InnerHtml = oDs.Tables[count].Rows[i]["artigos"].ToString();
                    selectLanguageTitle.InnerHtml = oDs.Tables[count].Rows[i]["selectLanguage"].ToString();
                    aulagratuita = oDs.Tables[count].Rows[i]["aulagratuita"].ToString();
                    servidor_site.InnerHtml = oDs.Tables[count].Rows[i]["SERVIDOR_SITE"].ToString();
                }
            }
            else
            {
                servidor_site.InnerHtml = "happybody/";
            }
        }
        catch (Exception exc)
        {
            servidor_site.InnerHtml = "happybody/";
        }

        mainNav.InnerHtml = table.ToString();
    }

    private void loadArtigos()
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
                                            DECLARE @lingua varchar(10) = '{0}';
                                            DECLARE @id_artigo int;
                                            DECLARE @sabemais varchar(max);
                                            DECLARE @traducao varchar(max);
                                            declare @path varchar(max) = (select concat(site, artigos) from report_paths());

                                            SET @traducao = 'Clica aqui para saberes mais!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @sabemais output;

                                            SELECT id_artigo, titulo, texto, link, data, autor, @sabemais as sabemais, @path as path, concat(@path, link) as file_path
                                            from REPORT_ARTIGOS(@id_artigo, @lingua)", lingua);

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0)
            {
                for (int i = 0; i < oDs.Tables[oDs.Tables.Count - 1].Rows.Count; i++)
                {
                    var path = oDs.Tables[0].Rows[i]["path"].ToString();
                    var file = oDs.Tables[0].Rows[i]["link"].ToString();
                    var file_path = oDs.Tables[0].Rows[i]["file_path"].ToString();
                    if (String.IsNullOrEmpty(file)
                        || !File.Exists((HttpContext.Current.Server.MapPath("~") + "/" + file_path)))
                    {
                        file_path = "../" + path + "no_img.jpg";
                    }
                    else
                    {
                        file_path = "../" + file_path;
                    }

                    table.AppendFormat(@"   <div class='col-md-6 col-lg-4' onclick='showDivs(0, {0});'>
                                                 <div class='portfolio-item mx-auto text-center' data-toggle='modal' data-target='#portfolioModal{0}'>
                                                    <div class='portfolio-item-caption d-flex align-items-center justify-content-center h-100 w-100'>
                                                        <div class='portfolio-item-caption-content text-center text-white'>
                                                            <i class='fas fa-plus fa-3x'></i>
				                                            <br />{1}
                                                        </div>
                                                    </div>
                                                    <img class='img-fluid' src='{2}' alt='{1}' id='imgArtigos{0}'>
                                                </div>
                                            </div>", (i + 1).ToString(),
                                            oDs.Tables[0].Rows[i]["titulo"].ToString(),
                                            file_path);

                    loadArtigosData(oDs.Tables[0].Rows[i]["id_artigo"].ToString(), (i + 1).ToString(), lingua,
                            oDs.Tables[0].Rows[i]["titulo"].ToString(), oDs.Tables[0].Rows[i]["texto"].ToString().Replace("\n", "<br />"),
                            oDs.Tables[0].Rows[i]["file_path"].ToString(), oDs.Tables[0].Rows[i]["data"].ToString(), 
                            oDs.Tables[0].Rows[i]["autor"].ToString(), oDs.Tables[0].Rows[i]["sabemais"].ToString());
                }
            }
            else
            {
                table.AppendFormat(@"");
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"");
        }

        newsRow.InnerHtml = table.ToString();
    }

    private void loadArtigosData(string idArtigo, string count, string language, string titulo, string texto, string foto, string data, string autor, string sabemais)
    {
        formModalidades.Clear();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @lingua varchar(10) = '{1}';
                                            DECLARE @id_artigo int = {0};
                                            DECLARE @sabemais varchar(max);
                                            DECLARE @traducao varchar(max);
                                            declare @path varchar(max) = (select concat(site, artigos) from report_paths());

                                            SET @traducao = 'Clica aqui para saberes mais!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @sabemais output;

                                            SELECT titulo, texto, link, data, autor, @sabemais as sabemais, @path as path, concat(@path, link) as file_path
                                            from REPORT_ARTIGOS(@id_artigo, @lingua)", idArtigo, language);

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            modalidadesDescription.AppendFormat(@"  <div class='portfolio-modal modal fade' id='portfolioModal{0}' tabindex='-1' role='dialog' aria-labelledby='portfolioModal{0}Label' aria-hidden='true'>
                                                <div class='modal-dialog modal-xl' role='document'>
                                                    <div class='modal-content' id='divArtigos{0}'>
                                                        <button type='button' class='close' data-dismiss='modal' aria-label='Close'>
                                                            <span aria-hidden='true'>
                                                                <i class='fas fa-times'></i>
                                                            </span>
                                                        </button>
                                                        <div class='modal-body text-center'>
                                                            <div class='container'>
                                                                <div class='row justify-content-center'>
                                                                    <div class='col-lg-8'>
                                                                        <h2 class='portfolio-modal-title text-secondary mb-0'>{1}</h2>
                                                                        <div class='divider-custom'>
                                                                            <div class='divider-custom-line'></div>
                                                                            <div class='divider-custom-icon'>
                                                                                <i class='fas fa-star'></i>
                                                                            </div>
                                                                            <div class='divider-custom-line'></div>
                                                                        </div>", count, titulo);

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[oDs.Tables.Count - 1].Rows.Count > 0)
            {
                modalidadesDescription.AppendFormat(@"<div class='w3-content w3-display-container'>");

                for (int i = 0; i < oDs.Tables[oDs.Tables.Count - 1].Rows.Count; i++)
                {
                    var path = oDs.Tables[0].Rows[i]["path"].ToString();
                    var file = oDs.Tables[0].Rows[i]["link"].ToString();
                    var file_path = oDs.Tables[0].Rows[i]["file_path"].ToString();
                    if (String.IsNullOrEmpty(file)
                        || !File.Exists((HttpContext.Current.Server.MapPath("~") + "/" + file_path)))
                    {
                        file_path = "../" + path + "no_img.jpg";
                    }
                    else
                    {
                        file_path = "../" + file_path;
                    }

                    modalidadesDescription.AppendFormat(@"<img class='mySlides img-fluid rounded mb-5 slideImgModalidades{2}' src='{0}' alt='{1}'>",
                        file_path, titulo, count);
                }

                modalidadesDescription.AppendFormat(@"   <button class='w3-button w3-black w3-display-left {1}' onclick='plusDivs(-1, {0})'>&#10094;</button>
                                                    <button class='w3-button w3-black w3-display-right {1}' onclick='plusDivs(1, {0})'>&#10095;</button>
                                                    </div>", count, oDs.Tables[oDs.Tables.Count - 1].Rows.Count == 1 ? "variaveis" : "");
            }
        }
        catch (Exception exc)
        {
            
        }

        modalidadesDescription.AppendFormat(@"  <p class='mb-5'>{0}<br /><br />{5} - {4}</p>
                                                </div></div></div></div></div>", texto, sabemais, titulo, count, data, autor);

        modalidadesDescription.AppendFormat(@"{0}</div></div>", formModalidades.ToString());
    }

    private void getLanguage()
    {
        switch (lingua.ToUpper())
        {
            case "EN":
                fab.Attributes.Add("class", "fab_language bg-secondary en");
                break;
            case "FR":
                fab.Attributes.Add("class", "fab_language bg-secondary fr");
                break;
            case "ES":
                fab.Attributes.Add("class", "fab_language bg-secondary es");
                break;
            default:
                fab.Attributes.Add("class", "fab_language bg-secondary pt");
                break;
        }
    }

    private static string getUserCountry()
    {
        return CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToUpper();
    }

    private static string getLanguageCode()
    {
        string language = getUserCountry();

        if (language == "PT" || language == "ES" || language == "FR")
        {
            return language;
        }

        return "EN";
    }

    private void getLinkValues()
    {
        lingua = Request.QueryString["language"];
        pagina = Request.QueryString["page"];
        separator.InnerHtml = pagina;

        if (String.IsNullOrEmpty(lingua))
        {
            lingua = String.IsNullOrEmpty(getLanguageCode()) ? "PT" : getLanguageCode();
        }
    }

    private void loadBackgroundImages()
    {
        var table = new StringBuilder();
        string server = "";
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        var i = 1;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT CONCAT(SITE, BANNERS) as PATH FROM REPORT_PATHS()");

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

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + server, "*"))
            {
                FileInfo info = new FileInfo(name);
                string filePath = "../" + server + info.Name;
                table.AppendFormat(@"<img class='variaveis' id='imgBackground{1}' src='{0}' />", filePath, i.ToString());
                i++;
            }

            i--;
            table.AppendFormat(@"<span class='variaveis' id='totalBackgroundImages'>{0}</span>", i.ToString());
        }
        catch (Exception exc)
        {
            connection.Close();
        }

        divBackgroundImages.InnerHtml = table.ToString();
    }

    private void getCommentsForm()
    {
        var table = new StringBuilder();
        var table2 = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        DataSet dts = new DataSet();

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @titulo varchar(max);
                                            DECLARE @comentario varchar(max);
                                            DECLARE @commentText varchar(max);
                                            DECLARE @commentSentSuccess varchar(max);
                                            DECLARE @commentSentError varchar(max);
                                            DECLARE @invalidComment varchar(max);
                                            DECLARE @enviar varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';
                                            EXEC DEVOLVE_TRADUCAO 'Comentários', @lingua, @titulo output;
                                            EXEC DEVOLVE_TRADUCAO 'Comentário', @lingua, @comentario output;
                                            EXEC DEVOLVE_TRADUCAO 'A tua opinião é importante para nós!<br />Deixa-nos aqui o teu comentário!', @lingua, @commentText output;
                                            EXEC DEVOLVE_TRADUCAO 'Ocorreu um erro ao enviar o teu comentário!<br />Por favor, tenta novamente mais tarde!', @lingua, @commentSentSuccess output;
                                            EXEC DEVOLVE_TRADUCAO 'O teu comentário foi enviado com sucesso!<br />Irá ser apresentado aqui após ser aprovado pelo administrador!', @lingua, @commentSentError output;
                                            EXEC DEVOLVE_TRADUCAO 'Por favor, insira um comentário válido!', @lingua, @invalidComment output;
                                            EXEC DEVOLVE_TRADUCAO 'ENVIAR', @lingua, @enviar output;
                                            
                                            SELECT
                                                @titulo as titulo,
                                                @comentario as comentario,
                                                @enviar as enviar,
                                                @commentText as commentText,
                                                @commentSentSuccess as commentSentSuccess,
                                                @commentSentError as commentSentError,
                                                @invalidComment as invalidComment

                                            select comentario from HB_WS_REPORT_COMENTARIOS()", lingua);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            adapter.Fill(dts);

            if (dts.Tables.Count > 0)
            {
                for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
                {
                    table.AppendFormat(@"  
                                            <div class='col-lg-12 mx-auto text-center' id='commentsForm'>
                                                <h3 class='text-uppercase mb-4'><br /><br />{0}</h3>
                                                <div class='control-group' id='formComment'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                        <label id='labelname'>{2}</label>
                                                        <input class='form-control' id='comment' type='text' placeholder='{2}' required='required' data-validation-required-message='{6}'>
                                                        <p class='help-block text-danger display_none' id='nameDanger'>{2}</p>
                                                    </div>
                                                </div>
                                                <br>
                                                <div id='success' class='text-center' style='display:none;'>{4}</div>
                                                <div id='error' class='text-center' style='display:none;'>{5}</div>
                                                <div id='insertValidMessage' class='text-center' style='display:none;'>{6}</div>
                                                <div class='form-group'>
                                                    <button type='button' class='btn btn-primary btn-xl mw-100 w-100' id='sendCommentButton' onclick='sendComment();'>{3}</button>
                                                </div>
                                            </div>", dts.Tables[0].Rows[i]["commentText"].ToString().Trim(),
                                            dts.Tables[0].Rows[i]["titulo"].ToString().Trim(),
                                            dts.Tables[0].Rows[i]["comentario"].ToString().Trim(),
                                            dts.Tables[0].Rows[i]["enviar"].ToString().Trim(),
                                            dts.Tables[0].Rows[i]["commentSentSuccess"].ToString().Trim(),
                                            dts.Tables[0].Rows[i]["commentSentError"].ToString().Trim(),
                                            dts.Tables[0].Rows[i]["invalidComment"].ToString().Trim());

                    commentsTitle.InnerHtml = dts.Tables[0].Rows[i]["titulo"].ToString().Trim();
                }

                if(dts.Tables.Count == 2)
                {
                    table2.AppendFormat(@"<br /><br /><marquee behavior='scroll' direction='left' scrollamount='10' style='font-size: 2rem;'>");

                    for (int i = 0; i < dts.Tables[1].Rows.Count; i++)
                    {
                        if(i > 0)
                        {
                            for(int j = 0; j < 100; j++)
                            {
                                table2.AppendFormat(@"&nbsp");
                            }
                        }

                        table2.AppendFormat(@"{0}", dts.Tables[1].Rows[i]["comentario"].ToString().Trim());
                    }

                    table2.AppendFormat(@"</marquee>");
                }
            }

            connection.Close();
        }
        catch (Exception exc)
        {

        }

        divCommentsForm.InnerHtml = table.ToString();
        divScrollComments.InnerHtml = table2.ToString(); 
    }

    [WebMethod]
    public static string saveComment(string comment)
    {
        string conta = "";

        try
        {
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(cs);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @comment varchar(max) = '{0}';

                                            INSERT INTO WS_COMENTARIOS(comentario)
                                            select @comment

                                            select count(comentario) as conta from WS_COMENTARIOS where comentario = @comment", comment);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    conta = myReader["conta"].ToString();
                }
            }

            return conta;
        }
        catch (Exception exc)
        {
            return "0";
        }
    }
}
