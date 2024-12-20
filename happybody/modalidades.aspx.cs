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

public partial class modalidades : Page
{
    static string csName = "connectionString";
    static string connectionstring = ConfigurationManager.ConnectionStrings[csName].ToString();
    StringBuilder modalidadesDescription = new StringBuilder();
    StringBuilder formModalidades = new StringBuilder();
    string pagina = "";
    string lingua = "";
    string sound = "";
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
        getDatePickerValues();
        loadBackgroundImages();
        loadModalidadesData();
        //loadHorarios();
        getLanguage();
        getSound();
        getFooter();
    }

    private void loadModalidadesData()
    {
        loadModalidades();
        divModalidadesDescription.InnerHtml = modalidadesDescription.ToString();
    }

    private void getMenu()
    {
        var table = new StringBuilder();
        var language = new StringBuilder();
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

                                            SET @traducao = 'Modalidades Happy Body®';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @titulo output;

                                            SET @traducao = 'Modalidades';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @modalidades output;

                                            SET @traducao = 'Selecione Língua';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @selectLanguage output;

                                            SET @traducao = 'Marcar Treino Personalizado';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @aulagratuita output;

                                            SET @traducao = 'Recrutamento';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @recrutamento output;

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
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='news.aspx?language={9}&page=news'>{3}</a>
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
                    modalidadesTitle.InnerHtml = oDs.Tables[count].Rows[i]["modalidades"].ToString();
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

    private void loadModalidades()
    {
        var table = new StringBuilder();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_modalidade int
                                            DECLARE @lingua varchar(10) = '{0}'
                                            declare @path varchar(max) = (select concat(site, icons_modalidades) from report_paths());
                                            
                                            SELECT
                                                ID_MODALIDADE,
	                                            TITULO,
	                                            TEXTO,
	                                            VIDEO,
	                                            FOTO,
                                                ICON,
                                                @path as PATH,
                                                concat(@path, icon) as FILE_PATH,
                                                ORDEM
                                            FROM HB_WS_REPORT_MODALIDADES(@id_modalidade, @lingua)
                                            ORDER BY ORDEM", lingua);

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
                    var file = oDs.Tables[0].Rows[i]["icon"].ToString();
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
                                                    <img class='img-fluid' src='{2}' alt='{1}' id='imgModalidades{0}'>
                                                </div>
                                            </div>", (i+1).ToString(),
                                            oDs.Tables[0].Rows[i]["titulo"].ToString(),
                                            file_path);

                    loadModalidadesPhotos(oDs.Tables[0].Rows[i]["ID_MODALIDADE"].ToString(), (i + 1).ToString(), lingua,
                            oDs.Tables[0].Rows[i]["titulo"].ToString(), oDs.Tables[0].Rows[i]["texto"].ToString().Replace("\n", "<br />"),
                            oDs.Tables[0].Rows[i]["video"].ToString(), oDs.Tables[0].Rows[i]["FOTO"].ToString());
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

        modalidadesRow.InnerHtml = table.ToString();
    }

    private void loadModalidadesPhotos(string idModalidade, string count, string language, string titulo, string texto, string video, string foto)
    {
        formModalidades.Clear();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            declare @id_modalidade int = {0};
                                            declare @lingua varchar(10) = '{1}';
                                            declare @path varchar(max) = (select concat(site, modalidades) from report_paths());
                                        
                                            SELECT
	                                            titulo,
	                                            texto,
                                                ficheiro,
                                                @path as PATH,
	                                            CONCAT(@path, ficheiro) AS file_path,
	                                            ordem
                                            from HB_WS_REPORT_GALERIA_MODALIDADES(@id_modalidade, @lingua)
                                            order by ordem", idModalidade, language);

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            modalidadesDescription.AppendFormat(@"  <div class='portfolio-modal modal fade' id='portfolioModal{0}' tabindex='-1' role='dialog' aria-labelledby='portfolioModal{0}Label' aria-hidden='true'>
                                                <div class='modal-dialog modal-xl' role='document'>
                                                    <div class='modal-content' id='divModalidades{0}'>
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
                    var file = oDs.Tables[0].Rows[i]["ficheiro"].ToString();
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

        modalidadesDescription.AppendFormat(@"<p class='mb-5'>{0}</p>
                                            <button class='btn btn-primary' onclick='openFormModalidades({3});'>
                                            <i class='fas fa-plus fa-fw'></i>
                                            {1}
                                         </button>
                                         </div></div></div></div></div>", texto, aulagratuita, titulo, count);

        getModalidadesForm(idModalidade, count, language, titulo);

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

    private void getSound()
    {
        switch (sound.ToUpper())
        {
            case "1":
                fab_sound.Attributes.Add("class", "fab_sound bg-secondary sound");
                break;
            default:
                fab_sound.Attributes.Add("class", "fab_sound bg-secondary mute");
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
        sound = Request.QueryString["sound"];
        separator.InnerHtml = pagina;

        if (String.IsNullOrEmpty(sound))
        {
            sound = "0";
        }

        if (String.IsNullOrEmpty(lingua))
        {
            lingua = String.IsNullOrEmpty(getLanguageCode()) ? "PT" : getLanguageCode();
        }
    }

    private void loadBackgroundImages()
    {
        var table = new StringBuilder();
        string server = "";
        var i = 1;

        SqlConnection connection = new SqlConnection(connectionstring);

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

    //private void loadHorarios()
    //{
    //    string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
    //    SqlConnection connection = new SqlConnection(connectionstring);
    //    string server = "";
    //    string site = "";
    //    var table = new StringBuilder();
    //    var table2 = new StringBuilder();
    //    string fileImg = "";

    //    try
    //    {
    //        connection.Open();

    //        string sql = string.Format(@"   select 
		  //                                      site,
		  //                                      horarios
    //                                        from report_paths()");

    //        SqlCommand myCommand = new SqlCommand(sql, connection);
    //        SqlDataReader myReader = myCommand.ExecuteReader();
    //        SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

    //        if (myReader.HasRows)
    //        {
    //            while (myReader.Read())
    //            {
    //                server = myReader["horarios"].ToString();
    //                site = myReader["site"].ToString();
    //            }

    //            connection.Close();
    //        }
    //        else
    //        {
                
    //        }
    //    }
    //    catch (Exception exc)
    //    {
            
    //    }

        

    //    try
    //    {
    //        if (File.Exists((HttpContext.Current.Server.MapPath("~") + "/" + site + server + "/horario.JPG")))
    //        {
    //            fileImg = server + "horario.JPG";
    //        }

    //        if (File.Exists((HttpContext.Current.Server.MapPath("~") + "/" + site + server + "/horario.jpg")))
    //        {
    //            fileImg = server + "horario.jpg";
    //        }

    //        if (File.Exists((HttpContext.Current.Server.MapPath("~") + "/" + site + server + "/horario.png")))
    //        {
    //            fileImg = server + "horario.png";
    //        }

    //        if (File.Exists((HttpContext.Current.Server.MapPath("~") + "/" + site + server + "/horario.pdf")))
    //        {
    //            table2.AppendFormat(@"  <a class='btn btn-xl btn-outline-light' href='{0}' download>
    //                                        <i class='fas fa-download mr-2'></i>
    //                                        Download!
    //                                    </a>", server + "horario.pdf");
    //        }

    //        table.AppendFormat(@"<img class='img-fluid rounded mb-5 w-100' src='{0}'/>", fileImg);
    //    }
    //    catch (Exception exc)
    //    {

    //    }

    //    horariosDiv.InnerHtml = table.ToString();
    //    divDownloadHorarios.InnerHtml = table2.ToString();
    //}

    private void getModalidadesForm(string id_modalidade, string count, string language, string titulo)
    {
        StringBuilder form = new StringBuilder();
        string nameLabel = "";
        string ageLabel = "";
        string phoneLabel = "";
        string emailLabel = "";
        string dateLabel = "";
        string hourLabel = "";
        string sendLabel = "";
        string validLabel = "";
        string dateName = "";
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        switch(language)
        {
            case "EN":
                dateName = "SET LANGUAGE English;";
                break;
            case "ES":
                dateName = "SET LANGUAGE Spanish;";
                break;
            case "FR":
                dateName = "SET LANGUAGE French;";
                break;
            default:
                dateName = "SET LANGUAGE Portuguese;";
                break;
        }

        try
        {
            string sql = string.Format(@"   {1}
                                            DECLARE @nome varchar(max);
                                            DECLARE @telefone varchar(max);
                                            DECLARE @email varchar(max);
                                            DECLARE @enviar varchar(max);
                                            DECLARE @idade varchar(max);
                                            DECLARE @data varchar(max);
                                            DECLARE @hora varchar(max);
                                            DECLARE @traducao varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';
                                            DECLARE @campovalido varchar(max);
                                            DECLARE @id_modalidade int = {2};
                                            
                                            SET @traducao = 'Nome';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @nome output;

                                            SET @traducao = 'Email';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @email output;

                                            SET @traducao = 'ENVIAR';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @enviar output;
        
                                            SET @traducao = 'Telefone';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @telefone output;

                                            SET @traducao = 'Idade';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @idade output;
        
                                            SET @traducao = 'Data';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @data output;

                                            SET @traducao = 'Hora';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @hora output;

                                            SET @traducao = 'Por favor, insira um(a) [FIELD] válido(a)!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @campovalido output;

                                            SELECT 
                                                @nome as nome,
                                                @telefone as telefone,
                                                @email as email,
                                                @enviar as enviar,
                                                @idade as idade,
                                                @data as data,
                                                @hora as hora,
                                                @campovalido as campovalido", lingua, dateName, id_modalidade);

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if(oDs.Tables != null && oDs.Tables.Count > 0)
            {
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                    {
                        nameLabel = oDs.Tables[0].Rows[i]["nome"].ToString();
                        ageLabel = oDs.Tables[0].Rows[i]["idade"].ToString();
                        phoneLabel = oDs.Tables[0].Rows[i]["telefone"].ToString();
                        emailLabel = oDs.Tables[0].Rows[i]["email"].ToString();
                        dateLabel = oDs.Tables[0].Rows[i]["data"].ToString();
                        hourLabel = oDs.Tables[0].Rows[i]["hora"].ToString();
                        sendLabel = oDs.Tables[0].Rows[i]["enviar"].ToString();
                        validLabel = oDs.Tables[0].Rows[i]["campovalido"].ToString();

                        form.AppendFormat(@"
                                        <div class='modal-content variaveis' id='divFormModalidades{2}'>
                                                        <button type='button' class='close' onclick='closeFormModalidades({2});'>
                                                            <span>
                                                                <i class='fas fa-times'></i>
                                                            </span>
                                                        </button>
                                                        <div class='modal-body text-center'>
                                                            <div class='container'>
                                                                <div class='row justify-content-center'>
                                                                    <div class='col-lg-12 mx-auto text-center'>
                                                                        <h2 class='portfolio-modal-title text-secondary mb-0' id='modalidadesTitle{2}'>{1}</h2>
                                                                        <h4 class='text-secondary mb-0' id='modalidadesSubtitle{2}'><br />{0}</h4>
                                                                        <div class='divider-custom'>
                                                                            <div class='divider-custom-line'></div>
                                                                            <div class='divider-custom-icon'>
                                                                                <i class='fas fa-star'></i>
                                                                            </div>
                                                                            <div class='divider-custom-line'></div>
                                                                        </div>
                                                                        <div class='control-group'>
                                                                            <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                                <label id='nameLabel{2}'>{3}</label>
                                                                                <input class='form-control' id='name{2}' type='text' placeholder='{3}' required='required' data-validation-required-message='{9}'>
                                                                                <p class='help-block text-danger display_none' id='nameDanger{2}'>{9}</p>
                                                                            </div>
                                                                        </div>
                                                                        <div class='control-group'>
                                                                            <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                                <label id='ageLabel{2}'>{4}</label>
                                                                                <input class='form-control' id='age{2}' type='number' placeholder='{4}' required='required' data-validation-required-message='{10}'>
                                                                                <p class='help-block text-danger display_none' id='ageDanger{2}'>{10}</p>
                                                                            </div>
                                                                        </div>
                                                                        <div class='control-group'>
                                                                            <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                                <label id='phoneLabel{2}'>{5}</label>
                                                                                <input class='form-control' id='phone{2}' type='tel' placeholder='{5}' required='required' data-validation-required-message='{11}'>
                                                                                <p class='help-block text-danger display_none' id='phoneDanger{2}'>{11}</p>
                                                                            </div>
                                                                        </div>
                                                                        <div class='control-group'>
                                                                            <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                                <label id='emailLabel{2}'>{6}</label>
                                                                                <input class='form-control' id='email{2}' type='email' placeholder='{6}' required='required' data-validation-required-message='{12}'>
                                                                                <p class='help-block text-danger display_none' id='emailDanger{2}'>{12}</p>
                                                                            </div>
                                                                        </div>
                                                                        <div class='control-group'>
                                                                            <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                               <label id='dataLabel{2}'>{7}</label>
                                                                               <input class='form-control' id='data{2}' type='text' onfocus='onFocus(0, {2});' onfocusout='outFocus(0, {2});' placeholder='{7}' required='required' data-validation-required-message='{13}'>
                                                                               <p class='help-block text-danger display_none' id='dataDanger{2}'>{13}</p>
                                                                            </div>
                                                                        </div>
                                                                        <div id='success{2}' class='text-center'></div>
                                                                        <span id='idModalidade{2}' class='variaveis'>{14}</span>
                                                                        <div class='form-group'>
                                                                            <button type='button' class='btn btn-primary btn-xl mw-100 w-100' id='formModalidadesButton' onclick='sendModalidadesMail({2});'>{8}</button>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>", aulagratuita,
                                                        titulo,
                                                        count,
                                                        nameLabel,
                                                        ageLabel,
                                                        phoneLabel,
                                                        emailLabel,
                                                        dateLabel,
                                                        sendLabel,
                                                        validLabel.Replace("[FIELD]", nameLabel),
                                                        validLabel.Replace("[FIELD]", ageLabel),
                                                        validLabel.Replace("[FIELD]", phoneLabel),
                                                        validLabel.Replace("[FIELD]", emailLabel),
                                                        validLabel.Replace("[FIELD]", dateLabel),
                                                        id_modalidade);
                    }
                }
            }

            formModalidades.AppendFormat(@"{0}", form.ToString());


        }
        catch (Exception exc)
        {
            formModalidades.AppendFormat(@"<div class='modal-content variaveis' id='divFormModalidades{0}'>{1}</div>", count, exc.ToString());
        }
    }

    private string getDayTranslated(string language, string dia)
    {
        switch (language)
        {
            case "EN":
                switch(dia)
                {
                    case "2":
                        return "Monday";
                    case "3":
                        return "Tuesday";
                    case "4":
                        return "Wednesday";
                    case "5":
                        return "Thursday";
                    case "6":
                        return "Friday";
                    case "SAB":
                        return "Saturday";
                }
                break;
            case "ES":
                switch (dia)
                {
                    case "2":
                        return "Lunes";
                    case "3":
                        return "Martes";
                    case "4":
                        return "Miércoles";
                    case "5":
                        return "Jueves";
                    case "6":
                        return "Viernes";
                    case "SAB":
                        return "Sábado";
                }
                break;
            case "FR":
                switch (dia)
                {
                    case "2":
                        return "Lundi";
                    case "3":
                        return "Mardi";
                    case "4":
                        return "Mercredi";
                    case "5":
                        return "Jeudi";
                    case "6":
                        return "Vendredi";
                    case "SAB":
                        return "Samedi";
                }
                break;
            default:
                switch (dia)
                {
                    case "2":
                        return "2ª Feira";
                    case "3":
                        return "3ª Feira";
                    case "4":
                        return "4ª Feira";
                    case "5":
                        return "5ª Feira";
                    case "6":
                        return "6ª Feira";
                    case "SAB":
                        return "Sábado";
                }
                break;
        }

        return "";
    }

    [WebMethod]
    public static string sendEmailFromTemplate(string assunto, string intro, string sendto, string sendcc, string sendbcc, string body, string language)
    {
        string servidor = "";
        string ginasiohappybody = "";
        string rsp = "";
        string _from = "";
        string _emailpwd = "";
        string _smtp = "";
        string _smtpport = "";
        string _emailsend = "";
        int timeout = 50000;

        try
        {
            SqlConnection connection = new SqlConnection(connectionstring);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @ginasiohappybody varchar(max);
                                            DECLARE @rsp varchar(max);
                                            DECLARE @traducao varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';
                                            
                                            SET @traducao = 'Ginásio Happy Body®';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @ginasiohappybody output;

                                            SET @traducao = 'O Ginásio Happy Body® agradece o teu email. O teu email foi enviado com sucesso! Entraremos em contacto contigo o mais brevemente possível. Obrigado!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @rsp output;

                                            SELECT 
                                                @ginasiohappybody as ginasiohappybody,
                                                @rsp as rsp,
                                                CONCAT(SITE, TEMPLATES) as server,
                                                SENDEMAIL_EMAIL,
		                                        SENDEMAIL_PASSWORD,
		                                        SENDEMAIL_SMTP,
		                                        SENDEMAIL_SMTPPORT,
		                                        SENDEMAIL_EMAILSEND
                                            FROM REPORT_PATHS()", language);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    servidor = myReader["server"].ToString();
                    ginasiohappybody = myReader["ginasiohappybody"].ToString();
                    rsp = myReader["rsp"].ToString();
                    _from = myReader["SENDEMAIL_EMAIL"].ToString();
                    _emailpwd = myReader["SENDEMAIL_PASSWORD"].ToString();
                    _smtp = myReader["SENDEMAIL_SMTP"].ToString();
                    _smtpport = myReader["SENDEMAIL_SMTPPORT"].ToString();
                    _emailsend = myReader["SENDEMAIL_EMAILSEND"].ToString();
                }
            }
        }
        catch (Exception exc)
        {

        }

        try
        {
            MailMessage mailMessage = new MailMessage();

            string newsletterText = string.Empty;
            newsletterText = File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "//" + servidor + "template.html");

            intro = intro.Replace("[IMAGEM]", "https://happybody.site//" + servidor + "//img//campanha.jpg");
            newsletterText = newsletterText.Replace("[EMAIL_INTRO]", intro);
            newsletterText = newsletterText.Replace("[EMAIL_TEXTBODY]", body);

            mailMessage.From = new MailAddress(_from, ginasiohappybody);

            mailMessage.To.Add(_emailsend);

            if (sendcc.Trim() != "")
                mailMessage.CC.Add(sendcc);
            if (sendbcc.Trim() != "")
                mailMessage.Bcc.Add(sendbcc);

            mailMessage.Subject = assunto;
            mailMessage.Body = newsletterText;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;

            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.BodyEncoding = Encoding.UTF8;

            SmtpClient smtpClient = new SmtpClient(_smtp);
            NetworkCredential mailAuthentication = new NetworkCredential(_from, _emailpwd);

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = mailAuthentication;
            smtpClient.Timeout = timeout;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpClient.Send(mailMessage);
            smtpClient.Dispose();
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }

        try
        {
            MailMessage mailMessage = new MailMessage();

            string newsletterText = string.Empty;
            newsletterText = File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "//" + servidor + "template.html");

            newsletterText = newsletterText.Replace("[EMAIL_INTRO]", intro);

            body = rsp;

            newsletterText = newsletterText.Replace("[EMAIL_TEXTBODY]", body);

            mailMessage.From = new MailAddress(_from, ginasiohappybody);

            mailMessage.To.Add(sendto);

            if (sendcc.Trim() != "")
                mailMessage.CC.Add(sendcc);
            if (sendbcc.Trim() != "")
                mailMessage.Bcc.Add(sendbcc);

            mailMessage.Subject = assunto;
            mailMessage.Body = newsletterText;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;

            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.BodyEncoding = Encoding.UTF8;

            SmtpClient smtpClient = new SmtpClient(_smtp);
            NetworkCredential mailAuthentication = new NetworkCredential(_from, _emailpwd);

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = mailAuthentication;
            smtpClient.Timeout = timeout;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpClient.Send(mailMessage);
            smtpClient.Dispose();
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }

        return rsp;
    }

    private void getDatePickerValues()
    {
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   DECLARE @limpar varchar(max);
                                            DECLARE @cancelar varchar(max);
                                            DECLARE @confirmar varchar(max);
                                            DECLARE @traducao varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';
                                            
                                            SET @traducao = 'Limpar';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @limpar output;

                                            SET @traducao = 'Cancelar';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @cancelar output;

                                            SET @traducao = 'Confirmar';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @confirmar output;

                                            SELECT 
                                                UPPER(@limpar) as limpar,
                                                UPPER(@cancelar) as cancelar,
                                                UPPER(@confirmar) as confirmar", lingua);

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
                    datepicker_clean.InnerHtml = oDs.Tables[count].Rows[i]["limpar"].ToString();
                    datepicker_cancel.InnerHtml = oDs.Tables[count].Rows[i]["cancelar"].ToString();
                    datepicker_confirm.InnerHtml = oDs.Tables[count].Rows[i]["confirmar"].ToString();
                }
                return;
            }
        }
        catch (Exception exc)
        {
            datepicker_clean.InnerHtml = "LIMPAR";
            datepicker_cancel.InnerHtml = "CANCELAR";
            datepicker_confirm.InnerHtml = "CONFIRMAR";
            return;
        }

        datepicker_clean.InnerHtml = "LIMPAR";
        datepicker_cancel.InnerHtml = "CANCELAR";
        datepicker_confirm.InnerHtml = "CONFIRMAR";
        return;
    }

    private void getFooter()
    {
        var table = new StringBuilder();
        var language = new StringBuilder();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   DECLARE @politicaprivacidade varchar(max);
                                            DECLARE @traducao varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';
                                            
                                            SET @traducao = 'Política de Privacidade';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @politicaprivacidade output;

                                            SELECT 
                                                @politicaprivacidade as politicaprivacidade", lingua);

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
                    table.AppendFormat(@"   <div class='row' style='text-align: center'>
                                                <div class='col-xl-6 col-md-6 col-sm-6 col-xs-6 col-lg-6' style='height: 30px !important; line-height: 30px; text-align: center; margin: auto !important; padding-bottom: 0px !important;'>
                                                    <p class='lead_small'>Copyright &copy;&nbsp;&nbsp;&nbsp;<img src='img/aal.png' style='height: 30px; width: auto; margin: auto;' />&nbsp;&nbsp;&nbsp;2023</p>
                                                </div>
                                                <div class='col-xl-6 col-md-6 col-sm-6 col-xs-6 col-lg-6' style='height: 30px !important; line-height: 30px; cursor: pointer; text-align: center; margin: auto !important; padding-bottom: 0px !important;' onclick='loadPrivacyPolicy();'>
                                                    <p class='lead_small'>{0}</p>
                                                </div>
                                            </div>",
                                            oDs.Tables[count].Rows[i]["politicaprivacidade"].ToString());
                }
            }
            else
            {

            }
        }
        catch (Exception exc)
        {

        }

        footer.InnerHtml = table.ToString();
    }
}
