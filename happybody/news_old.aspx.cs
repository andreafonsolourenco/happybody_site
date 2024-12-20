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

public partial class news_old : Page
{
    int countPortfolio = 1;
    string enviar = "";
    string email = "";
    string subscribe = "";
    StringBuilder eventsDescription = new StringBuilder();

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }

        getMenu();
        getNewsletter();
        loadEventsData();
        loadArtigos();
        getContacts();
    }

    private void loadEventsData()
    {
        loadEventos();
        divEventosDescription.InnerHtml = eventsDescription.ToString();
    }

    [WebMethod]
    public static string sendEmailFromTemplate(string assunto, string intro, string sendto, string sendcc, string sendbcc, string body)
    {
        string servidor = "";

        try
        {
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(cs);

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
                                            DECLARE @lingua varchar(10) = '{0}';
                                            DECLARE @enviar varchar(max);
                                            DECLARE @email varchar(max);
                                            DECLARE @subscribe varchar(max);
                                            DECLARE @artigos varchar(max);
                                            DECLARE @eventos varchar(max);
                                            
                                            SET @traducao = 'Sobre Happy Body®';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @about output;

                                            SET @traducao = 'Serviços';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @servicos output;

                                            SET @traducao = 'Notícias';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @noticias output;

                                            SET @traducao = 'Marcar Treino';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @marcarvisita output;

                                            SET @traducao = 'Horários';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @horarios output;

                                            SET @traducao = 'Staff';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @staff output;

                                            SET @traducao = 'Contactos';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @contactos output;

                                            SET @traducao = 'Modalidades';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @modalidades output;

                                            SET @traducao = 'Obrigado pelo seu contacto. Entraremos em contacto consigo o mais depressa possível!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @rspemail output;

                                            SET @traducao = 'Notícias Happy Body';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @titulo output;
            
                                            SET @traducao = 'Subscreva aqui a nossa Newsletter!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @subscribe output;

                                            SET @traducao = 'Enviar';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @enviar output;

                                            SET @traducao = 'Email';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @email output;

                                            SET @traducao = 'Artigos';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @artigos output;

                                            SET @traducao = 'Eventos';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @eventos output;

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
                                                @enviar as enviar,
                                                @email as email,
                                                @subscribe as subscribe,
                                                @artigos as artigos,
                                                @eventos as eventos,
                                                @rspemail as rspemail", "PT");

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                int count = oDs.Tables.Count - 1;
                
                /*switch("PT")
                {
                    case "EN": language.AppendFormat(@" <a href='javascript:void(0);' onclick='changeLanguage(44);'><img src='img/icons/en.ico' id='en'/></a>
                                                        <a href='javascript:void(0);' onclick='changeLanguage(351);'><img src='img/icons/pt.ico' id='pt'/></a>
                                                        <a href='javascript:void(0);' onclick='changeLanguage(34);'><img src='img/icons/sp.ico' id='es'/></a>
                                                        <a href='javascript:void(0);' onclick='changeLanguage(33);'><img src='img/icons/fr.ico' id='fr'/></a>");
                        break;
                    case "ES":
                        language.AppendFormat(@" <a href='javascript:void(0);' onclick='changeLanguage(34);'><img src='img/icons/sp.ico' id='es'/></a> 
                                            <a href='javascript:void(0);' onclick='changeLanguage(351);'><img src='img/icons/pt.ico' id='pt'/></a>
                                            <a href='javascript:void(0);' onclick='changeLanguage(33);'><img src='img/icons/fr.ico' id='fr'/></a>
                                            <a href='javascript:void(0);' onclick='changeLanguage(44);'><img src='img/icons/en.ico' id='en'/></a>");
                        break;
                    case "FR":
                        language.AppendFormat(@" <a href='javascript:void(0);' onclick='changeLanguage(33);'><img src='img/icons/fr.ico' id='fr'/></a>
                                            <a href='javascript:void(0);' onclick='changeLanguage(351);'><img src='img/icons/pt.ico' id='pt'/></a>
                                            <a href='javascript:void(0);' onclick='changeLanguage(34);'><img src='img/icons/sp.ico' id='es'/></a>
                                            <a href='javascript:void(0);' onclick='changeLanguage(44);'><img src='img/icons/en.ico' id='en'/></a>");
                        break;
                    default:
                        language.AppendFormat(@" <a href='javascript:void(0);' onclick='changeLanguage(351);'><img src='img/icons/pt.ico' id='pt'/></a>
                                            <a href='javascript:void(0);' onclick='changeLanguage(34);'><img src='img/icons/sp.ico' id='es'/></a>
                                            <a href='javascript:void(0);' onclick='changeLanguage(33);'><img src='img/icons/fr.ico' id='fr'/></a>
                                            <a href='javascript:void(0);' onclick='changeLanguage(44);'><img src='img/icons/en.ico' id='en'/></a>");
                        break;
                }*/

                for (int i = 0; i < oDs.Tables[count].Rows.Count; i++)
                {
                    table.AppendFormat(@"   <div class='container'>
                                                <!--<a class='navbar-brand js-scroll-trigger' href='#page-top'>HappyBody Gym</a>-->
                                                <button class='navbar-toggler navbar-toggler-right text-uppercase font-weight-bold bg-primary text-white rounded' type='button' data-toggle='collapse' data-target='#navbarResponsive' aria-controls='navbarResponsive' aria-expanded='false' aria-label='Toggle navigation'>
                                                    Menu
                                                    <i class='fas fa-bars'></i>
                                                </button>
                                                <div class='collapse navbar-collapse' id='navbarResponsive'>
                                                    <ul class='navbar-nav ml-auto'>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='index.aspx'>{0}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='index.aspx'>{1}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='modalidades.aspx'>{2}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='#news'>{3}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='index.aspx'>{4}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='modalidades.aspx'>{5}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='staff.aspx'>{6}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='index.aspx'>{7}</a>
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
                                            language.ToString());

                    pageTitle.InnerHtml = oDs.Tables[count].Rows[i]["pagetitle"].ToString();
                    eventosTitle.InnerHtml = oDs.Tables[count].Rows[i]["eventos"].ToString();
                    artigosTitle.InnerHtml = oDs.Tables[count].Rows[i]["artigos"].ToString();
                    enviar = oDs.Tables[count].Rows[i]["enviar"].ToString();
                    email = oDs.Tables[count].Rows[i]["email"].ToString();
                    subscribe = oDs.Tables[count].Rows[i]["subscribe"].ToString();
                }
            }
            else
            {
                /*table.AppendFormat(@"   <a href='javascript:void(0);' onclick='changeLanguage(351);'><img src='img/icons/pt.ico' id='pt'/></a>
                                            <a href='javascript:void(0);' onclick='changeLanguage(34);'><img src='img/icons/sp.ico' id='es'/></a>
                                            <a href='javascript:void(0);' onclick='changeLanguage(33);'><img src='img/icons/fr.ico' id='fr'/></a>
                                            <a href='javascript:void(0);' onclick='changeLanguage(44);'><img src='img/icons/en.ico' id='en'/></a>
                                            <a href='javascript:void(0);' onclick='openPage(0);'>Sobre Nós</a>
                                            <a href='#services'>Serviços</a>
                                            <a href='javascript:void(0);' onclick='openPage(2);'>Modalidades</a>
                                            <a href='javascript:void(0);' onclick='openPage(3);'>Notícias</a>
                                            <a href='#marcarVisita'>Marcar Visita</a>
                                            <a href='javascript:void(0);' onclick='openPage(5);'>Horários</a>
                                            <a href='javascript:void(0);' onclick='openPage(6);'>Staff</a>
                                            <a href='#contactos'>Contactos</a>
                                            <a href='javascript:void(0);' onclick='mute();' style='float:right;'>
                                                <i class='fa fa-volume-up' id='iconSound' style='height:28px;'></i>
                                            </a>
                                            <a href='javascript:void(0);' class='icon' onclick='myFunction()'>
                                                <i class='fa fa-bars'></i>
                                            </a>");

                tituloServicos = servicesTitle.InnerHtml = "SERVIÇOS";
                lblrspemail.InnerHtml = "Obrigado pelo seu contacto. Entraremos em contacto consigo o mais depressa possível!";*/
            }
        }
        catch (Exception exc)
        {
            /*table.AppendFormat(@"   <a href='javascript:void(0);' onclick='changeLanguage(351);'><img src='img/icons/pt.ico' id='pt'/></a>
                                            <a href='javascript:void(0);' onclick='changeLanguage(34);'><img src='img/icons/sp.ico' id='es'/></a>
                                            <a href='javascript:void(0);' onclick='changeLanguage(33);'><img src='img/icons/fr.ico' id='fr'/></a>
                                            <a href='javascript:void(0);' onclick='changeLanguage(44);'><img src='img/icons/en.ico' id='en'/></a>
                                            <a href='javascript:void(0);' onclick='openPage(0);'>Sobre Nós</a>
                                            <a href='#services'>Serviços</a>
                                            <a href='javascript:void(0);' onclick='openPage(2);'>Modalidades</a>
                                            <a href='javascript:void(0);' onclick='openPage(3);'>Notícias</a>
                                            <a href='#marcarVisita'>Marcar Visita</a>
                                            <a href='javascript:void(0);' onclick='openPage(5);'>Horários</a>
                                            <a href='javascript:void(0);' onclick='openPage(6);'>Staff</a>
                                            <a href='#contactos'>Contactos</a>
                                            <a href='javascript:void(0);' onclick='mute();' style='float:right;'>
                                                <i class='fa fa-volume-up' id='iconSound' style='height:28px;'></i>
                                            </a>
                                            <a href='javascript:void(0);' class='icon' onclick='myFunction()'>
                                                <i class='fa fa-bars'></i>
                                            </a>");

            tituloServicos = servicesTitle.InnerHtml = "SERVIÇOS";
            lblrspemail.InnerHtml = "Obrigado pelo seu contacto. Entraremos em contacto consigo o mais depressa possível!";*/
        }

        mainNav.InnerHtml = table.ToString();
    }

    private void getContacts()
    {
        var table = new StringBuilder();
        var table2 = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @titulo varchar(max);
                                            DECLARE @morada varchar(max);
                                            DECLARE @emails varchar(max);
                                            DECLARE @telefones varchar(max);
                                            DECLARE @vernomapa varchar(max);
                                            DECLARE @saibamaisaqui varchar(max);
                                            DECLARE @nome varchar(max);
                                            DECLARE @email varchar(max);
                                            DECLARE @telefone varchar(max);
                                            DECLARE @assunto varchar(max);
                                            DECLARE @escrevaoseutextoaqui varchar(max);
                                            DECLARE @enviar varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';
                                            EXEC DEVOLVE_TRADUCAO 'Contactos', @lingua, @titulo output;
                                            EXEC DEVOLVE_TRADUCAO 'Morada', @lingua, @morada output;
                                            EXEC DEVOLVE_TRADUCAO 'Emails', @lingua, @emails output;
                                            EXEC DEVOLVE_TRADUCAO 'Telefones', @lingua, @telefones output;
                                            EXEC DEVOLVE_TRADUCAO 'VER NO MAPA', @lingua, @vernomapa output;
                                            EXEC DEVOLVE_TRADUCAO 'Saiba mais aqui!', @lingua, @saibamaisaqui output;
                                            EXEC DEVOLVE_TRADUCAO 'Nome', @lingua, @nome output;
                                            EXEC DEVOLVE_TRADUCAO 'Email', @lingua, @email output;
                                            EXEC DEVOLVE_TRADUCAO 'Telefone', @lingua, @telefone output;
                                            EXEC DEVOLVE_TRADUCAO 'Escreva o seu texto aqui...', @lingua, @escrevaoseutextoaqui output;
                                            EXEC DEVOLVE_TRADUCAO 'ENVIAR', @lingua, @enviar output;
                                            EXEC DEVOLVE_TRADUCAO 'Assunto', @lingua, @assunto output;
                                            
                                            SELECT 
                                                MORADA,
                                                FACEBOOK,
                                                INSTAGRAM,
                                                EMAIL1,
                                                EMAIL2,
                                                EMAIL3,
                                                TLF1,
                                                TLF2,
                                                TLF3,
                                                WHATSAPP,
                                                EMAIL4,
                                                YOUTUBE,
                                                @titulo as titulo,
                                                @emails as emails,
                                                @telefones as telefones,
                                                @vernomapa as vernomapa,
                                                @saibamaisaqui as saibamaisaqui,
                                                @nome as nome,
                                                @email as email,
                                                @telefone as telefone,
                                                @assunto as assunto,
                                                @escrevaoseutextoaqui as escrevaoseutextoaqui,
                                                @enviar as enviar,
                                                @morada as morada
                                            FROM WS_CONTACTOS", "PT");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"   <div class='col-lg-6 mb-5 b-lg-0>
                                                <h4 class='text-uppercase mb-4'>
                                                    {0}<br />
                                                    {9}: {6}/{8}<br />
                                                    {10}: {5}/{7}</h4>
                                            </div>
                                            <div class='col-lg-6 mb-5 mb-lg-0'>
                                                <a class='btn btn-outline-light btn-social mx-1' href='{1}' target='_blank'>
                                                    <i class='fab fa-fw fa-facebook-f'></i>
                                                </a>
		                                        <a class='btn btn-outline-light btn-social mx-1' href='{2}' target='_blank'>
                                                    <i class='fab fa-fw fa-instagram'></i>
                                                </a>
                                                <a class='btn btn-outline-light btn-social mx-1' href='https://wa.me/{3}/?text=happybody.site' target='_blank'>
                                                    <i class='fab fa-fw fa-whatsapp'></i>
                                                </a>
                                                <a class='btn btn-outline-light btn-social mx-1' href='{4}' target='_blank'>
                                                    <i class='fab fa-fw fa-youtube'></i>
                                                </a>
                                                <a class='btn btn-outline-light btn-social mx-1' href='mailto:{5}?Subject=Contacto' target='_blank'>
                                                    <i class='fab fa-fw fa-envelope-open'></i>
                                                </a>
                                                <a class='btn btn-outline-light btn-social mx-1' href='callto:{6}' target='_blank'>
                                                    <i class='fab fa-fw fa-phone'></i>
                                                </a>
                                                <a class='btn btn-outline-light btn-social mx-1' href='mailto:{7}?Subject=Contacto' target='_blank'>
                                                    <i class='fab fa-fw fa-envelope-open'></i>
                                                </a>
                                                <a class='btn btn-outline-light btn-social mx-1' href='callto:{8}' target='_blank'>
                                                    <i class='fab fa-fw fa-phone'></i>
                                                </a>
                                            </div>
                                            ", myReader["MORADA"].ToString(),
                                            myReader["FACEBOOK"].ToString(),
                                            myReader["INSTAGRAM"].ToString(),
                                            myReader["WHATSAPP"].ToString(),
                                            myReader["YOUTUBE"].ToString(),
                                            myReader["EMAIL1"].ToString(),
                                            myReader["TLF1"].ToString(),
                                            myReader["EMAIL2"].ToString(),
                                            myReader["TLF2"].ToString(),
                                            myReader["TELEFONES"].ToString(),
                                            myReader["EMAILS"].ToString());

                    footerDiv.InnerHtml = table.ToString();
                }
            }

            connection.Close();
        }
        catch (Exception exc)
        {
            footerDiv.InnerHtml = exc.ToString();
        }
    }

    private void getNewsletter()
    {
        var table = new StringBuilder();
        var table2 = new StringBuilder();

        table.AppendFormat(@"   <div class='col-md-6 col-lg-6'>
                                    <div class='portfolio-item mx-auto' data-toggle='modal' data-target='#portfolioModal{0}'>
                                        <div class='portfolio-item-caption d-flex align-items-center justify-content-center h-100 w-100'>
                                            <div class='portfolio-item-caption-content text-center text-white'>
                                                <i class='fas fa-plus fa-3x'></i>
                                            </div>
                                        </div>
                                        <img class='img-fluid' src='Newsletter/newsletter_00{0}.jpg' alt='Newsletter Happy Body {0}'>
                                    </div>
                                </div>", countPortfolio.ToString());

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
                                                                    <div class='col-lg-12'>
				                                                        <img class='img-fluid rounded mb-5' src='Newsletter/newsletter_00{0}.jpg' alt='Newsletter Happy Body {0}'/>
                                                                        <button class='btn btn-primary' href='#' data-dismiss='modal'>
                                                                            <i class='fas fa-times fa-fw'></i>
                                                                            FECHAR
                                                                        </button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>", countPortfolio.ToString());

        countPortfolio++;

        table.AppendFormat(@"   <div class='col-md-6 col-lg-6'>
                                    <div class='portfolio-item mx-auto' data-toggle='modal' data-target='#portfolioModal{0}'>
                                        <div class='portfolio-item-caption d-flex align-items-center justify-content-center h-100 w-100'>
                                            <div class='portfolio-item-caption-content text-center text-white'>
                                                <i class='fas fa-plus fa-3x'></i>
                                            </div>
                                        </div>
                                        <img class='img-fluid' src='Newsletter/newsletter_00{0}.jpg' alt='Newsletter Happy Body {0}'>
                                    </div>
                                </div>", countPortfolio.ToString());

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
                                                                    <div class='col-lg-12'>
				                                                        <img class='img-fluid rounded mb-5' src='Newsletter/newsletter_00{0}.jpg' alt='Newsletter Happy Body {0}'/>
                                                                        <button class='btn btn-primary' href='#' data-dismiss='modal'>
                                                                            <i class='fas fa-times fa-fw'></i>
                                                                            FECHAR
                                                                        </button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>", countPortfolio.ToString());

        table.AppendFormat(@"   <div class='col-lg-12 mx-auto'>
                                    <h5 class='text-center text-uppercase text-secondary mb-0'>{2}</h2>
                                    <div class='control-group'>
                                        <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                            <label>{1}</label>
                                            <input class='form-control' id='subscribeEmail' type='email' placeholder='{1}' required='required' data-validation-required-message='Por favor, insira o email'>
                                            <p class='help-block text-danger'></p>
                                        </div>
                                    </div>
                                    <br>
                                    <div id='success'></div>
                                    <div class='form-group'>
                                        <button type='button' class='btn btn-primary btn-xl mw-100 w-100' id='subscribeNewsletterButton' onclick='' runat='server'>{0}</button>
                                    </div>
                                </div>", enviar, email, subscribe);

        countPortfolio++;

        newsletterRow.InnerHtml = table.ToString();
        divNewsletterDescription.InnerHtml = table2.ToString();
    }

    private void loadArtigos()
    {
        var table = new StringBuilder();
        var table2 = new StringBuilder();
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
                                            
                                            SET @traducao = 'Clica aqui para saberes mais!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @sabemais output;
                                            
                                            SELECT titulo, texto, link, data, autor, @sabemais as sabemais
                                            from REPORT_ARTIGOS(@id_artigo, @lingua)", "PT");

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
                    table.AppendFormat(@"   <div class='col-md-6 col-lg-4'>
                                                <div class='portfolio-item mx-auto' data-toggle='modal' data-target='#portfolioModal{0}'>
                                                    <div class='portfolio-item-caption d-flex align-items-center justify-content-center h-100 w-100'>
                                                        <div class='portfolio-item-caption-content text-center text-white'>
                                                            <i class='fas fa-plus fa-3x'></i>
				                                            <br />{1}
                                                        </div>
                                                    </div>
                                                    <img class='img-fluid' src='img/artigos/{2}' alt='{1}' id='imgArtigos{0}'>
                                                </div>
                                            </div>", countPortfolio.ToString(),
                                            oDs.Tables[0].Rows[i]["titulo"].ToString(),
                                            oDs.Tables[0].Rows[i]["link"].ToString());

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
                                                                        <h2 class='portfolio-modal-title text-secondary text-uppercase mb-0'>{1}</h2>
                                                                        <br />
                                                                        <h5 class='text-secondary text-uppercase mb-0'>{5}<br />{4}</h2>
                                                                        <div class='divider-custom'>
                                                                            <div class='divider-custom-line'></div>
                                                                            <div class='divider-custom-icon'>
                                                                                <i class='fas fa-star'></i>
                                                                            </div>
                                                                            <div class='divider-custom-line'></div>
                                                                        </div>
				                                                        <img class='img-fluid rounded mb-5' src='img/artigos/{2}' alt='{1}' id='imgArtigos{0}'/>
                                                                        <p class='mb-5'>{3}</p>
                                                                        <button class='btn btn-primary' href='#' data-dismiss='modal'>
                                                                            <i class='fas fa-plus fa-fw'></i>
                                                                            {6}
                                                                        </button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>", countPortfolio.ToString(),
                                            oDs.Tables[0].Rows[i]["titulo"].ToString(),
                                            oDs.Tables[0].Rows[i]["link"].ToString(),
                                            oDs.Tables[0].Rows[i]["texto"].ToString(),
                                            oDs.Tables[0].Rows[i]["data"].ToString(),
                                            oDs.Tables[0].Rows[i]["autor"].ToString(),
                                            oDs.Tables[0].Rows[i]["sabemais"].ToString());

                    if (i == 0)
                    {
                        table.AppendFormat(@"<span class='variaveis' id='counterArtigosStart'>{0}</span>", countPortfolio.ToString());
                    }
                    else if (i == oDs.Tables[oDs.Tables.Count - 1].Rows.Count - 1)
                    {
                        table.AppendFormat(@"<span class='variaveis' id='counterArtigosFinish'>{0}</span>", countPortfolio.ToString());
                    }

                    countPortfolio++;
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

        artigosRow.InnerHtml = table.ToString();
        divArtigosDescription.InnerHtml = table2.ToString();
    }

    private void loadEventos()
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
                                            
                                            select id_evento, titulo, foto_capa as link
                                            from REPORT_EVENTOS(@lingua)", "PT");

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
                    table.AppendFormat(@"   <div class='col-md-6 col-lg-4' onclick='showDivs(0, {0});'>
                                                 <div class='portfolio-item mx-auto' data-toggle='modal' data-target='#portfolioModal{0}'>
                                                    <div class='portfolio-item-caption d-flex align-items-center justify-content-center h-100 w-100'>
                                                        <div class='portfolio-item-caption-content text-center text-white'>
                                                            <i class='fas fa-plus fa-3x'></i>
				                                            <br />{1}
                                                        </div>
                                                    </div>
                                                    <img class='img-fluid' src='img/eventos/{2}' alt='{1}' id='imgEventos{0}'>
                                                </div>
                                            </div>", countPortfolio.ToString(),
                                            oDs.Tables[0].Rows[i]["titulo"].ToString(),
                                            oDs.Tables[0].Rows[i]["link"].ToString());

                    if(i==0)
                    {
                        table.AppendFormat(@"<span class='variaveis' id='counterEventsStart'>{0}</span>", countPortfolio.ToString());
                    }
                    else if (i == oDs.Tables[oDs.Tables.Count - 1].Rows.Count-1)
                    {
                        table.AppendFormat(@"<span class='variaveis' id='counterEventsFinish'>{0}</span>", countPortfolio.ToString());
                    }

                    loadEventosPhotos(oDs.Tables[0].Rows[i]["id_evento"].ToString(), countPortfolio.ToString(), "PT", oDs.Tables[0].Rows[i]["titulo"].ToString());

                    countPortfolio++;
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

        eventosRow.InnerHtml = table.ToString();
    }

    private void loadEventosPhotos(string idEvento, string count, string language, string titulo)
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            declare @id_evento int = {0};
                                            declare @lingua varchar(10) = '{1}';
                                        
                                            select id_evento, ficheiro, ordem
                                            from REPORT_GALERIA_EVENTOS_WS(@id_evento, @lingua)
                                            ORDER BY ORDEM ASC", idEvento, language);

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            eventsDescription.AppendFormat(@"  <div class='portfolio-modal modal fade' id='portfolioModal{0}' tabindex='-1' role='dialog' aria-labelledby='portfolioModal{0}Label' aria-hidden='true'>
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
                                                                        <h2 class='portfolio-modal-title text-secondary text-uppercase mb-0'>{1}</h2>
                                                                        <div class='divider-custom'>
                                                                            <div class='divider-custom-line'></div>
                                                                            <div class='divider-custom-icon'>
                                                                                <i class='fas fa-star'></i>
                                                                            </div>
                                                                            <div class='divider-custom-line'></div>
                                                                        </div>", count, titulo);

            if (oDs.Tables != null && oDs.Tables.Count > 0)
            {
                eventsDescription.AppendFormat(@"<div class='w3-content w3-display-container'>");

                for (int i = 0; i < oDs.Tables[oDs.Tables.Count - 1].Rows.Count; i++)
                {
                    eventsDescription.AppendFormat(@"<img class='mySlides img-fluid rounded mb-5 slideImgEvents{2}' src='img/eventos/{0}' alt='{1}'>", 
                        String.IsNullOrEmpty(oDs.Tables[0].Rows[i]["ficheiro"].ToString()) ? "no_img.jpg" : oDs.Tables[0].Rows[i]["ficheiro"].ToString(),
                        titulo,
                        count);
                }

                eventsDescription.AppendFormat(@"   <button class='w3-button w3-black w3-display-left' onclick='plusDivs(-1, {0})'>&#10094;</button>
                                                    <button class='w3-button w3-black w3-display-right' onclick='plusDivs(1, {0})'>&#10095;</button>
                                                    </div>", count);
            }
        }
        catch (Exception exc)
        {
            
        }

        eventsDescription.AppendFormat(@"<button class='btn btn-primary' href='#' data-dismiss='modal'>
                                            <i class='fas fa-times fa-fw'></i>
                                            FECHAR
                                         </button>
                                         </div></div></div></div></div></div></div>");
    }
}
