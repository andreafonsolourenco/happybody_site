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
using System.Security.Cryptography;

public partial class staff : Page
{
    static string csName = "connectionString";
    static string connectionstring = ConfigurationManager.ConnectionStrings[csName].ToString();
    string pagina = "";
    string lingua = "";
    string sound = "";
    string erro_candidatura = "";
    string caminho_cv = "";
    String validfield = "";
    string anyfile = "";
    string nome = "";
    string mail = "";
    string telefone = "";
    string querestrabalhar = "";
    string enviar = "";
    string escrevetexto = "";

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
        getStaff();
        getRecrutamento();
        getLanguage();
        getSound();
        getFooter();
    }

    [WebMethod]
    public static string sendEmailFromTemplate(string assunto, string intro, string sendto, string sendcc, string sendbcc, string body)
    {
        string servidor = "";

        try
        {
            SqlConnection connection = new SqlConnection(connectionstring);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;

                                            SELECT SERVIDOR_SITE FROM APPLICATION_CONFIG");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    servidor = myReader["SERVIDOR_SITE"].ToString();
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
            newsletterText = File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "//" + servidor + "//templates//template.html");

            intro = intro.Replace("[IMAGEM]", "https://happybody.site//" + servidor + "//img//campanha.jpg");
            newsletterText = newsletterText.Replace("[EMAIL_INTRO]", intro);
            newsletterText = newsletterText.Replace("[EMAIL_TEXTBODY]", body);

            string _from = "happybodyfitcoach@gmail.com";
            string _emailpwd = "happysr@1";
            string _smtp = "smtp.gmail.com";
            string _smtpport = "465";

            mailMessage.From = new MailAddress(_from, "Happy Body Fit Coach");

            mailMessage.To.Add("happybodyfitcoach@gmail.com");
            //mailMessage.To.Add("afonsopereira6@gmail.com");

            if (sendcc.Trim() != "")
                mailMessage.CC.Add(sendcc);
            if (sendbcc.Trim() != "")
                mailMessage.Bcc.Add(sendbcc);

            mailMessage.Subject = assunto;
            mailMessage.Body = newsletterText;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;

            string html = "html";

            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(_smtp);
            System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential(_from, _emailpwd);

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = mailAuthentication;
            smtpClient.Timeout = 50000;
            smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

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
            newsletterText = File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "//" + servidor + "//templates//template.html");

            newsletterText = newsletterText.Replace("[EMAIL_INTRO]", intro);

            body = "O seu email foi enviado com sucesso! Entraremos em contacto consigo o mais rapidamente possível. Obrigado!";

            newsletterText = newsletterText.Replace("[EMAIL_TEXTBODY]", body);

            string _from = "happybodyfitcoach@gmail.com";
            string _emailpwd = "happysr@1";
            string _smtp = "smtp.gmail.com";
            string _smtpport = "465";

            mailMessage.From = new MailAddress(_from, "Happy Body Fit Coach");

            mailMessage.To.Add(sendto);

            if (sendcc.Trim() != "")
                mailMessage.CC.Add(sendcc);
            if (sendbcc.Trim() != "")
                mailMessage.Bcc.Add(sendbcc);

            mailMessage.Subject = assunto;
            mailMessage.Body = newsletterText;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;

            string html = "html";

            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(_smtp);
            System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential(_from, _emailpwd);

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = mailAuthentication;
            smtpClient.Timeout = 50000;
            smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            smtpClient.Send(mailMessage);
            smtpClient.Dispose();
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }

        return "Email enviado com sucesso!";
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
                                            DECLARE @recrutamento varchar(max);
                                            DECLARE @erro_candidatura varchar(max);
                                            DECLARE @path_candidatura varchar(max);
                                            DECLARE @uploadfile varchar(max);
                                            DECLARE @anyfile varchar(max);
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

                                            SET @traducao = 'A Nossa Equipa';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @titulo output;

                                            SET @traducao = 'Selecione Língua';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @selectLanguage output;

                                            SET @traducao = 'Recrutamento';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @recrutamento output;

                                            SET @traducao = 'Ocorreu um erro ao efetuar a sua candidatura. Por favor, envie um email para happybodyfitcoach@gmail.com!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @erro_candidatura output;

                                            SET @traducao = 'Carregar Ficheiro';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @uploadfile output;

                                            SET @traducao = 'Nenhum ficheiro selecionado';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @anyfile output;

                                            SELECT @path_candidatura = cv from report_paths();

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
                                                @recrutamento as recrutamento,
                                                @erro_candidatura as erro_candidatura,
                                                @path_candidatura as path_candidatura,
                                                @selectLanguage as selectLanguage,
                                                @uploadfile as uploadfile,
                                                @anyfile as anyfile,
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
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='modalidades.aspx?language={9}&page=modalidades'>{2}</a>
                                                        </li>
                                                        <!--<li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='modalidades.aspx?language={9}&page=horarios'>{5}</a>
                                                        </li>-->
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='#staff'>{6}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='#recrutamento'>{10}</a>
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

                    staffTitle.InnerHtml = oDs.Tables[count].Rows[i]["staff"].ToString();
                    pageTitle.InnerHtml = oDs.Tables[count].Rows[i]["pagetitle"].ToString();
                    selectLanguageTitle.InnerHtml = oDs.Tables[count].Rows[i]["selectLanguage"].ToString();
                    erro_candidatura = oDs.Tables[count].Rows[i]["erro_candidatura"].ToString();
                    caminho_cv = oDs.Tables[count].Rows[i]["path_candidatura"].ToString();
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

    private void getStaff()
    {
        var table = new StringBuilder();
        var table2 = new StringBuilder();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string designacao = "";
            string texto = "";
            string var = "";
            string path1 = "../";
            string path2 = path1;

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            declare @id_staff int;
                                            declare @lingua varchar(10) = '{0}';
                                            declare @path varchar(max) = (select concat(site, staff) from report_paths());
                                            declare @fechar varchar(max);
                                            DECLARE @traducao varchar(max) = 'FECHAR';

                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @fechar output;
                                            
                                            SELECT
	                                            NOME,
	                                            FUNCAO,
                                                TEXTO,
	                                            ORDEM,
	                                            CONCAT(@path, FOTO_1) as PATH1,
	                                            CONCAT(@path, FOTO_2) as PATH2,
                                                @fechar as fechar
                                            FROM HB_WS_REPORT_STAFF(@id_staff, @lingua)
                                            ORDER BY ORDEM ASC", lingua);

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                nrStaff.InnerHtml = oDs.Tables[0].Rows.Count.ToString();

                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    designacao = oDs.Tables[0].Rows[i]["FUNCAO"].ToString().Replace("\n", "<br />");
                    texto = oDs.Tables[0].Rows[i]["TEXTO"].ToString().Replace("\n", "<br />");
                    var = (i + 1).ToString();
                    path1 = "../" + oDs.Tables[0].Rows[i]["PATH1"].ToString();
                    path2 = "../" + oDs.Tables[0].Rows[i]["PATH2"].ToString();

                    table.AppendFormat(@"   <div class='col-md-6 col-lg-4'>
                                                <div class='portfolio-item mx-auto' data-toggle='modal' data-target='#portfolioModal{2}'>
                                                    <div class='portfolio-item-caption d-flex align-items-center justify-content-center h-100 w-100 servicesHeight'>
                                                        <div class='portfolio-item-caption-content text-center text-white'>
                                                            <i class='fas fa-plus fa-3x'></i>
				                                            <br />{0}
                                                        </div>
                                                    </div>
                                                    <img class='img-fluid' src='{1}' alt='{0}' id='imgStaff{2}'>
                                                </div>
                                            </div>", oDs.Tables[0].Rows[i]["NOME"].ToString(), path1, var);

                    table2.AppendFormat(@"  <div class='portfolio-modal modal fade' id='portfolioModal{0}' tabindex='-1' role='dialog' aria-labelledby='portfolioModal{0}Label' aria-hidden='true'>
                                                <div class='modal-dialog modal-xl' role='document'>
                                                    <div class='modal-content'>
                                                        <button type='button' class='close' data-dismiss='modal' aria-label='Close'>
                                                            <span aria-hidden='true'>
                                                                <i class='fas fa-times'></i>
                                                            </span>
                                                        </button>
                                                        <div class='modal-body text-center'>
                                                            <div class='container'>
                                                                <div class='row justify-content-center'>
                                                                    <div class='col-lg-8'>
                                                                        <h2 class='portfolio-modal-title text-secondary text-uppercase mb-0'>{4}</h2>
                                                                        <br />
                                                                        <h5 class='text-secondary mb-0 text-left'>{1}</h2>
                                                                        <div class='divider-custom'>
                                                                            <div class='divider-custom-line'></div>
                                                                            <div class='divider-custom-icon'>
                                                                                <i class='fas fa-star'></i>
                                                                            </div>
                                                                            <div class='divider-custom-line'></div>
                                                                        </div>
				                                                        <img class='img-fluid rounded mb-5' src='{2}' alt='{4}'/>
                                                                        <p class='mb-5 text-left'>{3}</p>
                                                                        <button class='btn btn-primary' href='#' data-dismiss='modal'>
                                                                            <i class='fas fa-times fa-fw'></i>
                                                                            {5}
                                                                        </button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>", var, designacao, path2, texto, oDs.Tables[0].Rows[i]["NOME"].ToString(), oDs.Tables[0].Rows[i]["fechar"].ToString());
                }
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
        }

        staffRow.InnerHtml = table.ToString();
        divStaffDescription.InnerHtml = table2.ToString();
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

    private void getRecrutamento()
    {
        var table = new StringBuilder();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   DECLARE @nome varchar(max);
                                            DECLARE @telefone varchar(max);
                                            DECLARE @escrevaoseutextoaqui varchar(max);
                                            DECLARE @email varchar(max);
                                            DECLARE @enviar varchar(max);
                                            DECLARE @querestrabalhar varchar(max);
                                            DECLARE @comercial varchar(max);
                                            DECLARE @instrutor varchar(max);
                                            DECLARE @pt varchar(max);
                                            DECLARE @nutricionista varchar(max);
                                            DECLARE @uploadfile varchar(max);
                                            DECLARE @anyfile varchar(max);
                                            DECLARE @campovalido varchar(max);
                                            DECLARE @validation varchar(max);
                                            DECLARE @cargo varchar(max);
                                            DECLARE @traducao varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';
                                            
                                            SET @traducao = 'Nome';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @nome output;

                                            SET @traducao = 'Email';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @email output;

                                            SET @traducao = 'ENVIAR';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @enviar output;
        
                                            SET @traducao = 'Telefone';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @telefone output;

                                            SET @traducao = 'Queres Trabalhar no HAPPY BODY®?';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @querestrabalhar output;

                                            SET @traducao = 'Escreve o teu texto aqui...';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @escrevaoseutextoaqui output;

                                            SET @traducao = 'Comercial';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @comercial output;
        
                                            SET @traducao = 'Instrutor de Aulas de Grupo';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @instrutor output;

                                            SET @traducao = 'Personal Trainer';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @pt output;

                                            SET @traducao = 'Nutricionista';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @nutricionista output;
                                            
                                            SET @traducao = 'Carregar Ficheiro';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @uploadfile output;

                                            SET @traducao = 'Nenhum ficheiro selecionado';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @anyfile output;

                                            SET @traducao = 'Apenas são válidos letras, números e caracteres de pontuação!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @validation output;

                                            SET @traducao = 'Por favor, insira um(a) [FIELD] válido(a)!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @campovalido output;

                                            SET @traducao = 'Cargo';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @cargo output;

                                            SELECT 
                                                @nome as nome,
                                                @telefone as telefone,
                                                @querestrabalhar as querestrabalhar,
                                                @email as email,
                                                @enviar as enviar,
                                                @escrevaoseutextoaqui as escrevaoseutextoaqui,
                                                @comercial as comercial,
                                                @instrutor as instrutor,
                                                @pt as pt,
                                                @nutricionista as nutricionista,
                                                @anyfile as anyfile,
                                                @uploadfile as uploadfile,
                                                @campovalido as campovalido,
                                                @validation as validation,
                                                @cargo as cargo", lingua);

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
                    nome = oDs.Tables[count].Rows[i]["nome"].ToString();
                    mail = oDs.Tables[count].Rows[i]["email"].ToString();
                    telefone = oDs.Tables[count].Rows[i]["telefone"].ToString();
                    querestrabalhar = oDs.Tables[count].Rows[i]["querestrabalhar"].ToString();
                    enviar = oDs.Tables[count].Rows[i]["enviar"].ToString();
                    escrevetexto = oDs.Tables[count].Rows[i]["escrevaoseutextoaqui"].ToString();
                    string comercial = oDs.Tables[count].Rows[i]["comercial"].ToString();
                    string instrutor = oDs.Tables[count].Rows[i]["instrutor"].ToString();
                    string pt = oDs.Tables[count].Rows[i]["pt"].ToString();
                    string nutricionista = oDs.Tables[count].Rows[i]["nutricionista"].ToString();
                    anyfile = oDs.Tables[count].Rows[i]["anyfile"].ToString();
                    string uploadfile = oDs.Tables[count].Rows[i]["uploadfile"].ToString();
                    validfield = oDs.Tables[count].Rows[i]["campovalido"].ToString();
                    string validation = oDs.Tables[count].Rows[i]["validation"].ToString();
                    string cargo = oDs.Tables[count].Rows[i]["cargo"].ToString();

                    table.AppendFormat(@"   <div class='col-lg-12 mx-auto placeholdercolor text-center'>
                                                <div class='control-group'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                        <label>{0}</label>
                                                        <select name='tipoRecrutamento' id='tipoRecrutamento' class='form-control placeholdercolor btn btn-primary' required='required' data-validation-required-message='{5}'>
                                                            <option value='{1}'>{1}</option>
                                                            <option value='{2}'>{2}</option>
                                                            <option value='{3}'>{3}</option>
                                                            <option value='{4}'>{4}</option>
                                                        </select>
                                                        <p class='help-block text-danger display_none' id='dangerTipoRecrutamento'>{5}</p>
                                                    </div>
                                                </div>
                                                <div class='control-group'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                        <label>{6}</label>
                                                        <input class='form-control placeholdercolor' id='nomeRecrutamento' type='text' placeholder='{6}' required='required' data-validation-required-message='{7}'>
                                                        <p class='help-block text-danger display_none' id='dangerNomeRecrutamento'>{7}</p>
                                                    </div>
                                                </div>
                                                <div class='control-group'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                        <label>{8}</label>
                                                        <input class='form-control placeholdercolor' id='emailRecrutamento' type='email' placeholder='{8}' required='required' data-validation-required-message='{9}'>
                                                        <p class='help-block text-danger display_none' id='dangerEmailRecrutamento'>{9}</p>
                                                    </div>
                                                </div>
                                                <div class='control-group'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                        <label>{10}</label>
                                                        <input class='form-control placeholdercolor' id='telefoneRecrutamento' type='text' placeholder='{10}' required='required' data-validation-required-message='{11}'>
                                                        <p class='help-block text-danger display_none' id='dangerTelefoneRecrutamento'>{11}</p>
                                                    </div>
                                                </div>
                                                <div class='control-group'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                        <label>{12}</label>
                                                        <textarea class='form-control placeholdercolor' id='textoRecrutamento' rows='10' placeholder='{12}' required='required' data-validation-required-message='{13}'></textarea>
                                                        <p class='help-block text-danger display_none' id='dangerTextoRecrutamento'>{13}</p>
                                                    </div>
                                                </div>
                                                <div class='control-group'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                         <button type='button' class='btn btn-primary btn-xl mw-100 w-100' id='buttonFileUpload' onclick='simulateClickOnFileUploadButton();' runat='server'>{14}</button>
                                                         <p class='text-white lead' id='fileUploadedName' runat='server'><br />{15}</p>
                                                         <p class='help-block text-danger' id='cvDanger' runat='server'></p>
                                                    </div>
                                                </div>
                                                <div class='control-group'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                        <p class='text-white lead recrutamento_messages' id='recrutamentoSuccess' runat='server'><br /></p>
                                                        <p class='help-block text-danger text-success recrutamento_messages' id='recrutamentoError' runat='server'></p>
                                                    </div>
                                                </div>
                                                <div class='form-group'>
                                                    <span class='variaveis' id='errorMsgFile' runat='server'>{15}</span>
                                                    <button type='button' class='btn btn-light btn-xl mw-100 w-100' id='buttonSend' onclick='simulateClickOnSendButton();' runat='server'>{16}</button>
                                                </div>
                                            </div>", cargo, comercial, instrutor, pt, nutricionista, validfield.Replace("[FIELD]", cargo),
                                            nome, validfield.Replace("[FIELD]", nome), mail, validfield.Replace("[FIELD]", mail), 
                                            telefone, validfield.Replace("[FIELD]", telefone), 
                                            escrevetexto, validation, uploadfile, anyfile, enviar);
                }
            }
            else
            {

            }
        }
        catch (Exception exc)
        {

        }

        recrutamentoVisible.InnerHtml = table.ToString();
        recTitle.InnerHtml = querestrabalhar.ToString();
    }

    protected void VemTrabalhar_Click(object sender, EventArgs e)
    {
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        string servidor = "";
        string ddlValue = type.Text.ToString();
        string nomeField = name.Text.ToString();
        string mailField = email.Text.ToString();
        string telefoneField = tlf.Text.ToString();
        string textoField = text.Text.ToString();
        string _from = "";
        string _emailpwd = "";
        string _smtp = "";
        string _smtpport = "";
        string _emailsend = "";
        string ginasiohappybody = "";
        string querestrabalharnohb = "";
        string emailrsp = "";
        string templatePath = "";
        int timeout = 50000;

        if (!FileUploadControl.HasFile)
        {
            error.Text = anyfile;
            success.Text = "";
            return;
        }

        if(nomeField.Length == 0)
        {
            error.Text = validfield.Replace("[FIELD]", nome);
            success.Text = "";
            return;
        }

        byte[] tempBytes;
        tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(nomeField);
        nomeField = Encoding.UTF8.GetString(tempBytes);

        if (!IsValidEmail(mailField))
        {
            error.Text = validfield.Replace("[FIELD]", mail);
            success.Text = "";
            return;
        }

        if (String.IsNullOrEmpty(telefoneField))
        {
            error.Text = validfield.Replace("[FIELD]", telefone);
            success.Text = "";
            return;
        }

        if (textoField.Length == 0 || textoField.Contains("<") || textoField.Contains(">"))
        {
            error.Text = validfield.Replace("[FIELD]", escrevetexto);
            success.Text = "";
            return;
        }


        if (FileUploadControl.HasFile)
        {
            try
            {
                string filename = Path.GetFileName(FileUploadControl.FileName);
                string extension = Path.GetExtension(FileUploadControl.FileName);
                string idToSave = "";
                string nameToSave = name.Text.ToString().Replace(" ", "_");
                tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(nameToSave);
                nameToSave = Encoding.UTF8.GetString(tempBytes).Replace("?", "");

                filename = filename.Replace(" ", "_");
                tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(filename);
                filename = Encoding.UTF8.GetString(tempBytes).Replace("?", "");

                string sql = string.Format(@"   DECLARE @nome varchar(500) = '{0}';
                                                DECLARE @email varchar(500) = '{1}';
                                                DECLARE @tlf varchar(50) = '{2}';
                                                DECLARE @texto varchar(MAX) = '{3}';
                                                DECLARE @extensao varchar(10) = '{4}';
                                                DECLARE @tipo varchar(500) = '{5}';
                                                DECLARE @lingua varchar(2) = '{6}';
                                                DECLARE @id_candidatura int;
                                                DECLARE @ret varchar(max);
                                                DECLARE @traducao varchar(max);
                                                DECLARE @ginasiohappybody varchar(max);
                                                DECLARE @rsp varchar(max);
                                                DECLARE @querestrabalhar varchar(max);
                                                DECLARE @tipo_email varchar(max) = 'RECRUTAMENTO';
                                                DECLARE @retEmail int;
                                                DECLARE @retMsgEmail varchar(max);
                                                DECLARE @zero int = 0;
                                                DECLARE @empty varchar(max) = '';

                                                SET @traducao = 'Ginásio Happy Body®';
                                                EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @ginasiohappybody output;

                                                SET @traducao = 'Obrigado pelo teu contacto. A tua candidatura foi enviada com sucesso e irá ser analisada!';
                                                EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @rsp output;

                                                SET @traducao = 'Queres Trabalhar no HAPPY BODY®?';
                                                EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @querestrabalhar output;

                                                EXEC INSERIR_CANDIDATURA @nome, @email, @tlf, @texto, @extensao, @tipo, @lingua, @id_candidatura output, @ret output;

                                                EXEC insere_novo_email @tipo_email, @zero, @zero, @zero, @id_candidatura, @empty, @empty, @empty, @empty, @empty, @empty, @empty, 
                                                    @empty, @lingua, @retEmail OUTPUT, @retMsgEmail OUTPUT

                                                SELECT 
                                                    @ginasiohappybody as ginasiohappybody,
                                                    @rsp as rsp,
                                                    @querestrabalhar as querestrabalhar,
                                                    CONCAT(SITE, TEMPLATES) as templates,
                                                    SENDEMAIL_EMAIL,
		                                            SENDEMAIL_PASSWORD,
		                                            SENDEMAIL_SMTP,
		                                            SENDEMAIL_SMTPPORT,
		                                            SENDEMAIL_EMAILSEND,
                                                    @id_candidatura as id,
                                                    @ret as ret,
                                                    site,
                                                    cv
                                                FROM REPORT_PATHS()", nomeField, mailField, telefoneField, textoField, extension, ddlValue, lingua);

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
                        string id = oDs.Tables[0].Rows[i]["id"].ToString();
                        success.Text = oDs.Tables[0].Rows[i]["rsp"].ToString();
                        error.Text = "";
                        servidor = oDs.Tables[0].Rows[i]["site"].ToString();
                        string cv = oDs.Tables[0].Rows[i]["cv"].ToString();
                        _from = oDs.Tables[0].Rows[i]["SENDEMAIL_EMAIL"].ToString();
                        _emailpwd = oDs.Tables[0].Rows[i]["SENDEMAIL_PASSWORD"].ToString();
                        _smtp = oDs.Tables[0].Rows[i]["SENDEMAIL_SMTP"].ToString();
                        _smtpport = oDs.Tables[0].Rows[i]["SENDEMAIL_SMTPPORT"].ToString();
                        _emailsend = oDs.Tables[0].Rows[i]["SENDEMAIL_EMAILSEND"].ToString();
                        ginasiohappybody = oDs.Tables[0].Rows[i]["ginasiohappybody"].ToString();
                        querestrabalharnohb = oDs.Tables[0].Rows[i]["querestrabalhar"].ToString();
                        emailrsp = oDs.Tables[0].Rows[i]["rsp"].ToString();
                        templatePath = oDs.Tables[0].Rows[i]["templates"].ToString();
                        idToSave = Server.MapPath("~") + Path.DirectorySeparatorChar + servidor + Path.DirectorySeparatorChar + cv + id + extension;
                        nameToSave = Server.MapPath("~") + Path.DirectorySeparatorChar + servidor + Path.DirectorySeparatorChar + cv + nameToSave + extension;

                        if (Convert.ToInt32(id) > 0)
                        {
                            FileUploadControl.SaveAs(idToSave);

                            name.Text = "";
                            email.Text = "";
                            tlf.Text = "";
                            text.Text = "";
                            idCandidatura.Text = id;
                        }
                        else
                        {
                            FileUploadControl.SaveAs(nameToSave);
                            error.Text = erro_candidatura + ": CV não inserido!";
                            success.Text = "";
                            return;
                        }
                    }
                }
                else
                {
                    nameToSave = Server.MapPath("~") + Path.DirectorySeparatorChar + servidor + Path.DirectorySeparatorChar + caminho_cv + nameToSave + extension;
                    error.Text = erro_candidatura + ": erro ao inserir na base de dados";
                    success.Text = "";
                    FileUploadControl.SaveAs(nameToSave);
                    return;
                }
            }
            catch (Exception ex)
            {
                error.Text = erro_candidatura + ": " + ex.ToString();
                success.Text = "";
                return;
            }

            /*try
            {
                MailMessage mailMessage = new MailMessage();

                string newsletterText = string.Empty;
                newsletterText = File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "//" + templatePath + "template.html");

                // ------------------------------------
                // Processa o template 
                // ------------------------------------
                newsletterText = newsletterText.Replace("[EMAIL_INTRO]", querestrabalharnohb);
                newsletterText = newsletterText.Replace("[EMAIL_TEXTBODY]", emailrsp);

                mailMessage.From = new MailAddress(_from, ginasiohappybody);

                mailMessage.To.Add(email.Text.ToString());

                mailMessage.Bcc.Add(_emailsend);

                mailMessage.Subject = querestrabalharnohb;
                mailMessage.Body = newsletterText;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;

                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.BodyEncoding = Encoding.UTF8;

                SmtpClient smtpClient = new SmtpClient(_smtp);
                System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential(_from, _emailpwd);

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
                return;
            }*/
        }
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
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
