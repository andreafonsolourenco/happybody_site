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
using System.Net;
using System.Globalization;

public partial class index : Page
{
    static string csName = "connectionString";
    static string connectionstring = ConfigurationManager.ConnectionStrings[csName].ToString();
    string pagina = "";
    string lingua = "";
    string sound = "";
    string campaign = "";
    string saibaMais = "";
    string titleParceiros = "";
    StringBuilder formPreInscricao = new StringBuilder();

    string nameLabel = "";
    string ageLabel = "";
    string phoneLabel = "";
    string emailLabel = "";
    string dateLabel = "";
    string hourLabel = "";
    string sendLabel = "";
    string validLabel = "";

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
        getMarcarVisita();
        getServices();
        loadAbout();
        getContacts();
        getLanguage();
        getSound();
        loadParceiros();
        getFooter();
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
                                            DECLARE @aboutWithLineBreak varchar(max);
                                            DECLARE @servicos varchar(max);
                                            DECLARE @modalidades varchar(max);
                                            DECLARE @noticias varchar(max);
                                            DECLARE @marcarvisita varchar(max);
                                            DECLARE @horarios varchar(max);
                                            DECLARE @staff varchar(max);
                                            DECLARE @contactos varchar(max);
                                            DECLARE @selectLanguage varchar(max);
                                            DECLARE @saibamais varchar(max);
                                            DECLARE @titleParceiros varchar(max);
                                            DECLARE @traducao varchar(max);
                                            DECLARE @recrutamento varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';
                                            
                                            SET @traducao = 'Sobre Happy Body®';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @about output;

                                            SET @traducao = 'Sobre<br />Happy Body®';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @aboutWithLineBreak output;

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

                                            SET @traducao = 'Selecione Língua';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @selectLanguage output;

                                            SET @traducao = 'Sabe Mais Aqui!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @saibamais output;

                                            SET @traducao = 'Vê aqui os parceiros Happy Body®';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @titleParceiros output;

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
                                                @selectLanguage as selectLanguage,
                                                @saibamais as saibamais,
                                                @titleParceiros as titleParceiros,
                                                @recrutamento as recrutamento,
                                                @aboutWithLineBreak as aboutWithLineBreak", lingua);

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
                                                <button class='navbar-toggler navbar-toggler-right text-uppercase font-weight-bold bg-primary text-white rounded' type='button' data-toggle='collapse' data-target='#navbarResponsive' aria-controls='navbarResponsive' aria-expanded='false' aria-label='Toggle navigation' id='buttonNavbar'>
                                                    Menu
                                                    <i class='fas fa-bars'></i>
                                                </button>
                                                <div class='collapse navbar-collapse' id='navbarResponsive'>
                                                    <ul class='navbar-nav ml-auto'>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='#about'>{0}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='#services'>{1}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='#marcarVisita'>{4}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='#contact'>{7}</a>
                                                        </li>
                                                        <li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='modalidades.aspx?language={9}&page=modalidades'>{2}</a>
                                                        </li>
                                                        <!--<li class='nav-item mx-0 mx-lg-1 text-center'>
                                                            <a class='nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger' href='modalidades.aspx?language={9}&page=horarios'>{5}</a>
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

                    servicesTitle.InnerHtml = oDs.Tables[count].Rows[i]["servicos"].ToString();
                    aboutTitle.InnerHtml = oDs.Tables[count].Rows[i]["aboutWithLineBreak"].ToString();
                    marcarVisitaTitle.InnerHtml = oDs.Tables[count].Rows[i]["marcarvisita"].ToString().Replace(" (", "<br />(");
                    selectLanguageTitle.InnerHtml = oDs.Tables[count].Rows[i]["selectLanguage"].ToString();
                    saibaMais = oDs.Tables[count].Rows[i]["saibamais"].ToString();
                    titleParceiros = oDs.Tables[count].Rows[i]["titleParceiros"].ToString();
                }
            }
            else
            {
                
            }
        }
        catch (Exception exc)
        {
            
        }

        mainNav.InnerHtml = table.ToString();
    }

    private void getServices()
    {
        var table = new StringBuilder();
        var table2 = new StringBuilder();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_servico int;
                                            DECLARE @lingua varchar(10) = '{0}';
                                            declare @path varchar(max) = (select concat(site, servicos) from report_paths());
                                            
                                            SELECT 
                                                DESIGNACAO, TEXTO, CONCAT(@path, IMAGEM) as IMAGEM, ID_SERVICO
                                            FROM HB_WS_REPORT_SERVICOS(@id_servico, @lingua)", lingua);

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                nrServices.InnerHtml = oDs.Tables[0].Rows.Count.ToString();

                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    table.AppendFormat(@"   <div class='col-md-6 col-lg-6' onclick='closeFormServices({3});'>
                                                <div class='portfolio-item mx-auto' data-toggle='modal' data-target='#portfolioModal{3}'>
                                                    <div class='portfolio-item-caption d-flex align-items-center justify-content-center h-100 w-100'>
                                                        <div class='portfolio-item-caption-content text-center text-white'>
                                                            <i class='fas fa-plus fa-3x'></i>
				                                            <br />{1}
                                                        </div>
                                                    </div>
                                                    <img class='img-fluid' src='../{0}' alt='{1}' id='imgServicos{3}'>
                                                </div>
                                            </div>", oDs.Tables[0].Rows[i]["IMAGEM"].ToString(),
                                            oDs.Tables[0].Rows[i]["DESIGNACAO"].ToString().Replace("\n", "<br />"),
                                            oDs.Tables[0].Rows[i]["TEXTO"].ToString().Replace("\n", "<br />"),
                                            (i+1).ToString());

                    table2.AppendFormat(@"  <div class='portfolio-modal modal fade' id='portfolioModal{0}' tabindex='-1' role='dialog' aria-labelledby='portfolioModal{0}Label' aria-hidden='true'>
                                                <div class='modal-dialog modal-xl' role='document'>
                                                    <div class='modal-content' id='divServices{0}'>
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
                                                                        </div>
				                                                        <img class='img-fluid rounded mb-5' src='../{2}' alt='{1}'/>
                                                                        <div class='text-left'><p class='mb-5'>{3}</p></div>
                                                                        <button class='btn btn-primary' onclick='openFormServices({0});'>
                                                                            <i class='fas fa-plus fa-fw'></i>
                                                                            {4}
                                                                        </button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class='modal-content variaveis' id='divFormServices{0}'>
                                                        <button type='button' class='close' onclick='closeFormServices({0});'>
                                                            <span>
                                                                <i class='fas fa-times'></i>
                                                            </span>
                                                        </button>
                                                        <div class='modal-body text-center'>
                                                            <div class='container'>
                                                                <div class='row justify-content-center'>
                                                                    <div class='col-lg-12 mx-auto text-center'>
                                                                        <h2 class='portfolio-modal-title text-secondary text-uppercase mb-0' id='titleServices{0}'>{1}</h2>
                                                                        <div class='divider-custom'>
                                                                            <div class='divider-custom-line'></div>
                                                                            <div class='divider-custom-icon'>
                                                                                <i class='fas fa-star'></i>
                                                                            </div>
                                                                            <div class='divider-custom-line'></div>
                                                                        </div>
                                                                        <div class='control-group'>
                                                                            <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                                <label id='nameFormLabel{0}'>{5}</label>
                                                                                <input class='form-control' id='nameFormService{0}' type='text' placeholder='{5}' required='required' data-validation-required-message='{12}'>
                                                                                <p class='help-block text-danger display_none' id='nameFormDanger{0}'>{12}</p>
                                                                            </div>
                                                                        </div>
                                                                        <div class='control-group'>
                                                                            <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                                <label id='ageFormLabel{0}'>{6}</label>
                                                                                <input class='form-control' id='ageFormService{0}' type='number' placeholder='{6}' required='required' data-validation-required-message='{13}'>
                                                                                <p class='help-block text-danger display_none' id='ageFormDanger{0}'>{13}</p>
                                                                            </div>
                                                                        </div>
                                                                        <div class='control-group'>
                                                                            <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                                <label id='phoneFormLabel{0}'>{7}</label>
                                                                                <input class='form-control' id='phoneFormService{0}' type='tel' placeholder='{7}' required='required' data-validation-required-message='{14}'>
                                                                                <p class='help-block text-danger display_none' id='phoneFormDanger{0}'>{14}</p>
                                                                            </div>
                                                                        </div>
                                                                        <div class='control-group'>
                                                                            <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                                <label id='emailFormLabel{0}'>{8}</label>
                                                                                <input class='form-control' id='emailFormService{0}' type='email' placeholder='{8}' required='required' data-validation-required-message='{15}'>
                                                                                <p class='help-block text-danger display_none' id='emailFormDanger{0}'>{15}</p>
                                                                            </div>
                                                                        </div>
                                                                        <div class='control-group'>
                                                                            <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                                <label id='dataFormLabel{0}'>{9}</label>
                                                                                <input class='form-control' id='dataFormService{0}' type='text' onfocus='onFocus(5, {0});' onfocusout='outFocus(5, {0})' placeholder='{9}' required='required' data-validation-required-message='{16}'>
                                                                                <p class='help-block text-danger display_none' id='dateFormDanger{0}'>{16}</p>
                                                                            </div>
                                                                        </div>
                                                                        <div class='control-group'>
                                                                            <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                                <label id='horaFormLabel{0}'>{10}</label>
                                                                                <input class='form-control' id='horaFormService{0}' type='text' onfocus='onFocus(6, {0});' onfocusout='outFocus(6, {0})' placeholder='{10}' required='required' data-validation-required-message='{17}'>
                                                                                <p class='help-block text-danger display_none' id='horaFormDanger{0}'>{17}</p>
                                                                            </div>
                                                                        </div>
                                                                        <div id='successFormServices{0}' class='text-center'></div>
                                                                        <span id='idServiceDivFormServices{0}' class='variaveis'>{18}</span>
                                                                        <div class='form-group'>
                                                                            <button type='button' class='btn btn-primary btn-xl mw-100 w-100' id='formServicesButton' onclick='sendServicesMail({0});'>{11}</button>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>", (i+1).ToString(), 
                                            oDs.Tables[0].Rows[i]["DESIGNACAO"].ToString().Replace("\n", "<br />"), 
                                            oDs.Tables[0].Rows[i]["IMAGEM"].ToString(),
                                            oDs.Tables[0].Rows[i]["TEXTO"].ToString().Replace("\n", "<br />"),
                                            saibaMais,
                                            nameLabel,
                                            ageLabel,
                                            phoneLabel,
                                            emailLabel,
                                            dateLabel,
                                            hourLabel,
                                            sendLabel,
                                            validLabel.Replace("[FIELD]", nameLabel),
                                            validLabel.Replace("[FIELD]", ageLabel),
                                            validLabel.Replace("[FIELD]", phoneLabel),
                                            validLabel.Replace("[FIELD]", emailLabel),
                                            validLabel.Replace("[FIELD]", dateLabel),
                                            validLabel.Replace("[FIELD]", hourLabel),
                                            oDs.Tables[0].Rows[i]["ID_SERVICO"].ToString());
                }
            }
        }
        catch (Exception exc)
        {
            
        }

        servicesRow.InnerHtml = table.ToString();
        divServicesDescriptions.InnerHtml = table2.ToString();
    }

    private void loadAbout()
    {
        var table = new StringBuilder();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        var isMobile = Request.Browser.IsMobileDevice;

        try
        {
            string designacao = "";
            string texto = "";

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_about int;
                                            DECLARE @lingua varchar(10) = '{0}';

                                            select
                                                TITULO, TEXTO, ORDEM
                                            from HB_WS_REPORT_ABOUT(@id_about, @lingua)
                                            order by ordem asc", lingua);

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
                    designacao = oDs.Tables[0].Rows[i]["TITULO"].ToString().Replace("\n", "<br />");
                    texto = oDs.Tables[0].Rows[i]["TEXTO"].ToString().Replace("\n", "<br />");

                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <div class='col-lg-4 ml-auto text-left'>
                                                <h5 class='text-uppercase text-white'>{0}</h5>
                                                <p class='lead_small'>{1}</p>
                                                <br /><br />
                                            </div>", designacao, texto);
                }

                loadFormPreInscricao();
                table.AppendFormat(@"{0}", formPreInscricao.ToString());
                divAbout.InnerHtml = table.ToString();
            }
        }
        catch (Exception exc)
        {
            
        }
    }

    private void getContacts()
    {
        var table = new StringBuilder();
        var table2 = new StringBuilder();
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
                                            DECLARE @marcarvisita varchar(max);
                                            DECLARE @enviar varchar(max);
                                            DECLARE @chamadaredemovelinternacional varchar(max);
                                            DECLARE @chamadaredemovelnacional varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';
                                            EXEC DEVOLVE_TRADUCAO 'Contactos', @lingua, @titulo output;
                                            EXEC DEVOLVE_TRADUCAO 'Morada', @lingua, @morada output;
                                            EXEC DEVOLVE_TRADUCAO 'Email', @lingua, @emails output;
                                            EXEC DEVOLVE_TRADUCAO 'Telefones', @lingua, @telefones output;
                                            EXEC DEVOLVE_TRADUCAO 'VER NO MAPA', @lingua, @vernomapa output;
                                            EXEC DEVOLVE_TRADUCAO 'Sabe mais aqui!', @lingua, @saibamaisaqui output;
                                            EXEC DEVOLVE_TRADUCAO 'Nome', @lingua, @nome output;
                                            EXEC DEVOLVE_TRADUCAO 'Email', @lingua, @email output;
                                            EXEC DEVOLVE_TRADUCAO 'Telefone', @lingua, @telefone output;
                                            EXEC DEVOLVE_TRADUCAO 'Escreve o teu texto aqui...', @lingua, @escrevaoseutextoaqui output;
                                            EXEC DEVOLVE_TRADUCAO 'ENVIAR', @lingua, @enviar output;
                                            EXEC DEVOLVE_TRADUCAO 'Assunto', @lingua, @assunto output;
                                            EXEC DEVOLVE_TRADUCAO 'Marcar Visita', @lingua, @marcarvisita output;
                                            EXEC DEVOLVE_TRADUCAO 'Chamada para a Rede Móvel Suiça', @lingua, @chamadaredemovelinternacional output;
                                            EXEC DEVOLVE_TRADUCAO 'Chamada para a Rede Móvel Portuguesa', @lingua, @chamadaredemovelnacional output;
                                            
                                            SELECT 
                                                MORADA,
                                                FACEBOOK,
                                                INSTAGRAM,
                                                EMAIL1,
                                                EMAIL2,
                                                TLF1,
                                                TLF2,
                                                WHATSAPP,
                                                YOUTUBE,
                                                linkedin,
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
                                                @morada as morada,
                                                @marcarvisita as marcarvisita,
                                                @chamadaredemovelinternacional as chamadainternacional,
                                                @chamadaredemovelnacional as chamadanacional
                                            FROM HB_WS_REPORT_CONTACTOS()", lingua);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    string breakLine = isMobileBrowser() ? "<br />" : " / ";
                    string tlf1 = myReader["TLF1"].ToString();
                    string tlf2 = myReader["TLF2"].ToString();
                    string chamadaInternacional = myReader["chamadainternacional"].ToString();
                    string chamadaNacional = myReader["chamadanacional"].ToString();
                    string formatTlf2 = String.IsNullOrEmpty(tlf2) ? ""
                        : String.Format(@"{0}<a href='callto:{1}' target='_blank'>{1}</a>{2}", breakLine, tlf2, "");//tlf2.Contains("+351") ? " (" + chamadaNacional + ")" : " (" + chamadaInternacional + ")");
                    string email1 = myReader["EMAIL1"].ToString();
                    string email2 = myReader["EMAIL2"].ToString();
                    string formatEmail2 = !String.IsNullOrEmpty(email2) ? (breakLine + "<a href='mailto:" + email2 + "?Subject=Contacto através do Site' target='_blank'>" + email2 + "</a>") : "";
                    string showTlf = String.Format(@"<a href='callto:{0}' target='_blank'>{0}</a>{2}{1}", tlf1, formatTlf2, "");//tlf1.Contains("+351") ? " (" + chamadaNacional + ")" : " (" + chamadaInternacional + ")");
                    string showEmail = String.Format(@"<a href='mailto:{0}?Subject=Contacto através do Site' target='_blank'>{0}</a>", email1, formatEmail2);
                    string linkedin = myReader["linkedin"].ToString();
                    string showLinkedin = String.IsNullOrEmpty(linkedin) ? "" : String.Format(@"<a class='btn btn-outline-dark btn-social mx-1' href='{0}' target='_blank'><i class='fab fa-fw fa-linkedin'></i></a>", linkedin);

                    table.AppendFormat(@"   <div class='col-lg-12 mx-auto text-center'>
                                                <p class='mb-4 lead'>
                                                    {0}<br />
                                                    {9}: {11}<br />
                                                    {10}: {12}
                                                </p>
                                            </div>
                                            <div class='col-lg-12 mx-auto text-center'>
                                                <a class='btn btn-outline-dark btn-social mx-1' href='{1}' target='_blank'>
                                                    <i class='fab fa-fw fa-facebook-f'></i>
                                                </a>
		                                        <a class='btn btn-outline-dark btn-social mx-1' href='{2}' target='_blank'>
                                                    <i class='fab fa-fw fa-instagram'></i>
                                                </a>
                                                <a class='btn btn-outline-dark btn-social mx-1' href='https://wa.me/{3}/?text=happybody.site' target='_blank'>
                                                    <i class='fab fa-fw fa-whatsapp'></i>
                                                </a>
                                                <a class='btn btn-outline-dark btn-social mx-1' href='{4}' target='_blank'>
                                                    <i class='fab fa-fw fa-youtube'></i>
                                                </a>
                                                {13}
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
                                            myReader["EMAILS"].ToString(),
                                            showTlf,
                                            showEmail,
                                            showLinkedin);

                    table.AppendFormat(@"  
                                            <div class='col-lg-12 mx-auto text-center' id='contactsForm'>
                                                <h3 class='text-uppercase mb-4'><br /><br />{9}</h3>
                                                <div class='control-group' id='formGroupName'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                        <label id='labelname'>{0}</label>
                                                        <input class='form-control' id='name' type='text' placeholder='{0}' required='required' data-validation-required-message='{10}'>
                                                        <p class='help-block text-danger display_none' id='nameDanger'>{10}</p>
                                                    </div>
                                                </div>
                                                <div class='control-group' id='formGroupEmail'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                        <label id='labelemail'>{1}</label>
                                                        <input class='form-control' id='email' type='email' placeholder='{1}' required='required' data-validation-required-message='{11}'>
                                                        <p class='help-block text-danger display_none' id='emailDanger'>{11}</p>
                                                    </div>
                                                </div>
                                                <div class='control-group' id='formGroupTlf'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                        <label id='labeltlf'>{2}</label>
                                                        <input class='form-control' id='tlf' type='tel' placeholder='{2}' required='required' data-validation-required-message='{12}'>
                                                        <p class='help-block text-danger display_none' id='tlfDanger'>{12}</p>
                                                    </div>
                                                </div>
                                                <div class='control-group' id='formGroupSubject'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                        <label id='labelsubject'>{3}</label>
                                                        <input class='form-control' id='subject' type='text' placeholder='{3}' required='required' data-validation-required-message='{13}'>
                                                        <p class='help-block text-danger display_none' id='subjectDanger'>{13}</p>
                                                    </div>
                                                </div>
                                                <div class='control-group' id='formGroupMsg'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                        <label id='labeltextarea'>{4}</label>
                                                        <textarea class='form-control' id='textarea' rows='5' placeholder='{4}' required='required' data-validation-required-message='{14}'></textarea>
                                                        <p class='help-block text-danger display_none' id='textareaDanger'>{14}</p>
                                                    </div>
                                                </div>
                                                <br>
                                                <div id='success' class='text-center'></div>
                                                <div class='form-group'>
                                                    <button type='button' class='btn btn-primary btn-xl mw-100 w-100' id='sendMessageButton' onclick='sendContactosMail();'>{5}</button>
                                                </div>
                                            </div>", myReader["nome"].ToString(),
                                            myReader["email"].ToString(),
                                            myReader["telefone"].ToString(),
                                            myReader["assunto"].ToString(),
                                            myReader["escrevaoseutextoaqui"].ToString(),
                                            myReader["enviar"].ToString(),
                                            myReader["vernomapa"].ToString(),
                                            myReader["titulo"].ToString(),
                                            myReader["marcarvisita"].ToString(),
                                            myReader["saibamaisaqui"].ToString(),
                                            validLabel.Replace("[FIELD]", myReader["nome"].ToString()),
                                            validLabel.Replace("[FIELD]", myReader["email"].ToString()),
                                            validLabel.Replace("[FIELD]", myReader["telefone"].ToString()),
                                            validLabel.Replace("[FIELD]", myReader["assunto"].ToString()),
                                            validLabel.Replace("[FIELD]", myReader["escrevaoseutextoaqui"].ToString()));

                    divContactsForm.InnerHtml = table.ToString();
                    contactsTitle.InnerHtml = myReader["titulo"].ToString();
                }
            }

            connection.Close();
        }
        catch (Exception exc)
        {
            
        }
    }

    private void getMarcarVisita()
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
                                            DECLARE @marcarvisita varchar(max);
                                            DECLARE @email varchar(max);
                                            DECLARE @enviar varchar(max);
                                            DECLARE @idade varchar(max);
                                            DECLARE @data varchar(max);
                                            DECLARE @hora varchar(max);
                                            DECLARE @traducao varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';
                                            DECLARE @campovalido varchar(max);
                                            
                                            SET @traducao = 'Nome';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @nome output;

                                            SET @traducao = 'Email';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @email output;

                                            SET @traducao = 'ENVIAR';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @enviar output;
        
                                            SET @traducao = 'Telefone';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @telefone output;

                                            SET @traducao = 'Marcar Visita';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @marcarvisita output;

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
                                                @marcarvisita as marcarvisita,
                                                @email as email,
                                                @enviar as enviar,
                                                @idade as idade,
                                                @data as data,
                                                @hora as hora,
                                                @campovalido as campovalido", lingua);

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
                    nameLabel = oDs.Tables[count].Rows[i]["nome"].ToString();
                    ageLabel = oDs.Tables[count].Rows[i]["idade"].ToString();
                    phoneLabel = oDs.Tables[count].Rows[i]["telefone"].ToString();
                    emailLabel = oDs.Tables[count].Rows[i]["email"].ToString();
                    dateLabel = oDs.Tables[count].Rows[i]["data"].ToString();
                    hourLabel = oDs.Tables[count].Rows[i]["hora"].ToString();
                    sendLabel = oDs.Tables[count].Rows[i]["enviar"].ToString();
                    validLabel = oDs.Tables[count].Rows[i]["campovalido"].ToString();

                    table.AppendFormat(@"   <div class='col-lg-12 mx-auto placeholdercolor'>
                                                <div class='control-group'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                        <label id='labelmvname'>{0}</label>
                                                        <input class='form-control placeholdercolor' id='mvname' type='text' placeholder='{0}' required='required' data-validation-required-message='{5}'>
                                                        <p class='help-block text-danger display_none' id='mvnameDanger'>{5}</p>
                                                    </div>
                                                </div>
                                                <div class='control-group'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                        <label id='labelmvemail'>{1}</label>
                                                        <input class='form-control placeholdercolor' id='mvemail' type='email' placeholder='{1}' required='required' data-validation-required-message='{6}'>
                                                        <p class='help-block text-danger display_none' id='mvemailDanger'>{6}</p>
                                                    </div>
                                                </div>
                                                <div class='control-group'>
                                                    <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                        <label id='labelmvtlf'>{2}</label>
                                                        <input class='form-control placeholdercolor' id='mvtlf' type='tel' placeholder='{2}' required='required' data-validation-required-message='{7}'>
                                                        <p class='help-block text-danger display_none' id='mvtlfDanger'>{7}</p>
                                                    </div>
                                                </div>
                                                <br>
                                                <div id='successFormMarcarVisita' class='text-center'></div>
                                                <div class='form-group'>
                                                    <button type='button' class='btn btn-light btn-xl mw-100 w-100' id='marcarVisitaButton' onclick='sendMarcarVisitaMail();'>{4}</button>
                                                </div>
                                            </div>", nameLabel, emailLabel, phoneLabel, oDs.Tables[count].Rows[i]["marcarvisita"].ToString(), sendLabel,
                                            validLabel.Replace("[FIELD]", nameLabel), validLabel.Replace("[FIELD]", emailLabel), validLabel.Replace("[FIELD]", phoneLabel));
                }
            }
            else
            {
                
            }
        }
        catch (Exception exc)
        {

        }

        marcarVisitaFormDiv.InnerHtml = table.ToString();
    }

    private void loadFormPreInscricao()
    {
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        string fieldNome = "";
        string fieldMorada = "";
        string fieldCodPostal = "";
        string fieldLocalidade = "";
        string fieldTelemovel = "";
        string fieldProfissao = "";
        string fieldCCNr = "";
        string fieldValidade = "";
        string fieldDataNascimento = "";
        string fieldEmail = "";
        string fieldContactoEmergencia = "";
        string fieldGrauContactoEmergencia = "";
        string fieldDataAF = "";
        string fieldHoraAF = "";
        string fieldDataPrimeiroTreino = "";
        string titulo = "";
        string variaveis = lingua == "FR" ? " variaveis " : "";
        const string FIELD = "[FIELD]";

        try
        {
            string sql = string.Format(@"   DECLARE @nome varchar(max);
                                            DECLARE @morada varchar(max);
                                            DECLARE @codpostal varchar(max);
                                            DECLARE @localidade varchar(max);
                                            DECLARE @telemovel varchar(max);
                                            DECLARE @profissao varchar(max);
                                            DECLARE @ccnr varchar(max);
                                            DECLARE @validadecc varchar(max);
                                            DECLARE @datanascimento varchar(max);
                                            DECLARE @email varchar(max);
                                            DECLARE @contactoemergencia varchar(max);
                                            DECLARE @graucontactoemergencia varchar(max);
                                            DECLARE @consentimento varchar(max);
                                            DECLARE @tituloconsentimento varchar(max);
                                            DECLARE @checkboxconsentimento varchar(max);
                                            DECLARE @titulo varchar(max);
                                            DECLARE @enviar varchar(max);
                                            DECLARE @dataaf varchar(max);
                                            DECLARE @horaaf varchar(max);
                                            DECLARE @dataprimeirotreino varchar(max);
                                            DECLARE @traducao varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';
                                            
                                            SET @traducao = 'Nome';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @nome output;

                                            SET @traducao = 'Morada';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @morada output;

                                            SET @traducao = 'Código Postal';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @codpostal output;

                                            SET @traducao = 'Localidade';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @localidade output;
        
                                            SET @traducao = 'Telemóvel';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @telemovel output;

                                            SET @traducao = 'Profissão';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @profissao output;

                                            SET @traducao = 'Nº CC';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @ccnr output;

                                            SET @traducao = 'Validade CC';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @validadecc output;

                                            SET @traducao = 'Data de Nascimento';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @datanascimento output;

                                            SET @traducao = 'Email';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @email output;

                                            SET @traducao = 'Contacto de Emergência';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @contactoemergencia output;

                                            SET @traducao = 'Nome / Grau Parentesco';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @graucontactoemergencia output;

                                            SET @traducao = 'CONSENTIMENTO DO TITULAR DE DADOS INFORMADO';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @tituloconsentimento output;

                                            SET @traducao = 'Nos termos do RGPD, consinto que os meus dados pessoais (nome, email e telefone ou telemóvel) sejam utilizados para:<br />- Felicitações de aniversário / estatística;<br />- Divulgações de todas as informações, campanhas, horários e aulas;<br />- Marketing ou publicidade (novidades, preços do ginásio);<br />- Ofertas;<br />- Descontos;<br />- Newsletters, artigos e vídeos.';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @consentimento output;

                                            SET @traducao = 'Se desejares que os teus dados sejam utilizados para os termos anteriormente descritos, por favor seleciona esta opção. Caso pretendas, poderás desativar esta opção sempre que desejares via email para happybodyfitcoach@gmail.com';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @checkboxconsentimento output;

                                            SET @traducao = 'FAZ AQUI A TUA PRÉ-INSCRIÇÃO PARA O TEU TREINO PERSONALIZADO';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @titulo output;

                                            SET @traducao = 'ENVIAR';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @enviar output;

                                            SET @traducao = 'Data 1ª Avaliação Física';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @dataaf output;

                                            SET @traducao = 'Hora 1ª Avaliação Física';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @horaaf output;

                                            SET @traducao = 'Data 1º Treino';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @dataprimeirotreino output;

                                            SELECT 
                                                @nome as nome,
                                                @morada as morada,
                                                @codpostal as codpostal,
                                                @localidade as localidade,
                                                @telemovel as telemovel,
                                                @profissao as profissao,
                                                @ccnr as ccnr,
                                                @validadecc as validade,
                                                @datanascimento as datanascimento,
                                                @email as email,
                                                @contactoemergencia as contactoemergencia,
                                                @graucontactoemergencia as graucontactoemergencia,
                                                @tituloconsentimento as tituloconsentimento,
                                                @consentimento as consentimento,
                                                @checkboxconsentimento as checkboxconsentimento,
                                                @titulo as titulo,
                                                @enviar as enviar,
                                                @dataaf as dataaf,
                                                @horaaf as horaaf,
                                                @dataprimeirotreino as dataprimeirotreino", lingua);

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
                    fieldNome = oDs.Tables[count].Rows[i]["nome"].ToString();
                    fieldMorada = oDs.Tables[count].Rows[i]["morada"].ToString();
                    fieldCodPostal = oDs.Tables[count].Rows[i]["codpostal"].ToString();
                    fieldLocalidade = oDs.Tables[count].Rows[i]["localidade"].ToString();
                    fieldTelemovel = oDs.Tables[count].Rows[i]["telemovel"].ToString();
                    fieldProfissao = oDs.Tables[count].Rows[i]["profissao"].ToString();
                    fieldCCNr = oDs.Tables[count].Rows[i]["ccnr"].ToString();
                    fieldValidade = oDs.Tables[count].Rows[i]["validade"].ToString();
                    fieldDataNascimento = oDs.Tables[count].Rows[i]["datanascimento"].ToString();
                    fieldEmail = oDs.Tables[count].Rows[i]["email"].ToString();
                    fieldContactoEmergencia = oDs.Tables[count].Rows[i]["contactoemergencia"].ToString();
                    fieldGrauContactoEmergencia = oDs.Tables[count].Rows[i]["graucontactoemergencia"].ToString();
                    fieldDataAF = oDs.Tables[count].Rows[i]["dataaf"].ToString();
                    fieldHoraAF = oDs.Tables[count].Rows[i]["horaaf"].ToString();
                    fieldDataPrimeiroTreino = oDs.Tables[count].Rows[i]["dataprimeirotreino"].ToString();
                    titulo = oDs.Tables[count].Rows[i]["titulo"].ToString();

                    if(isMobileBrowser())
                    {
                        titulo = titulo.Replace(" (", "<br />(");
                    }

                    formPreInscricao.AppendFormat(@"    <div class='col-lg-12 mx-auto placeholdercolor text-center'>
                                                            <h3 class='text-uppercase mb-4'>{15}</h3>
                                                            <div class='control-group'>
                                                                <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                    <label>{0}</label>
                                                                    <input class='form-control placeholdercolor' id='namePreInscricao' type='text' placeholder='{0}' required='required' data-validation-required-message='{20}'>
                                                                    <p class='help-block text-danger display_none' id='dangernamePreInscricao'>{20}</p>
                                                                </div>
                                                            </div>
                                                            <div class='control-group'>
                                                                <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                    <label>{1}</label>
                                                                    <input class='form-control placeholdercolor' id='morada' type='text' placeholder='{1}' required='required' data-validation-required-message='{21}'>
                                                                    <p class='help-block text-danger display_none' id='dangermorada'>{21}</p>
                                                                </div>
                                                            </div>
                                                            <div class='control-group'>
                                                                <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                    <label>{2}</label>
                                                                    <input class='form-control placeholdercolor' id='codpostal' type='text' placeholder='{2}' required='required' data-validation-required-message='{22}'>
                                                                    <p class='help-block text-danger display_none' id='dangercodpostal'>{22}</p>
                                                                </div>
                                                            </div>
                                                            <div class='control-group'>
                                                                <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                    <label>{3}</label>
                                                                    <input class='form-control placeholdercolor' id='localidade' type='text' placeholder='{3}' required='required' data-validation-required-message='{23}'>
                                                                    <p class='help-block text-danger display_none' id='dangerlocalidade'>{23}</p>
                                                                </div>
                                                            </div>
                                                            <div class='control-group'>
                                                                <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                    <label>{4}</label>
                                                                    <input class='form-control placeholdercolor' id='telemovel' type='tel' placeholder='{4}' required='required' data-validation-required-message='{24}'>
                                                                    <p class='help-block text-danger display_none' id='dangertelemovel'>{24}</p>
                                                                </div>
                                                            </div>
                                                            <div class='control-group'>
                                                                <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                    <label>{9}</label>
                                                                    <input class='form-control placeholdercolor' id='emailPreInscricao' type='email' placeholder='{9}' required='required' data-validation-required-message='{25}'>
                                                                    <p class='help-block text-danger display_none' id='dangeremailPreInscricao'>{25}</p>
                                                                </div>
                                                            </div>
                                                            <div class='control-group'>
                                                                <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                    <label>{5}</label>
                                                                    <input class='form-control placeholdercolor' id='profissao' type='text' placeholder='{5}' required='required' data-validation-required-message='{26}'>
                                                                    <p class='help-block text-danger display_none' id='dangerprofissao'>{26}</p>
                                                                </div>
                                                            </div>
                                                            <div class='control-group{35}'>
                                                                <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                    <label>{6}</label>
                                                                    <input class='form-control placeholdercolor' id='ccnr' type='text' placeholder='{6}' required='required' data-validation-required-message='{27}'>
                                                                    <p class='help-block text-danger display_none' id='dangerccnr'>{27}</p>
                                                                </div>
                                                            </div>
                                                            <div class='control-group{35}'>
                                                                <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                    <label>{7}</label>
                                                                    <input class='form-control placeholdercolor' id='validadecc' type='text' placeholder='{7}' required='required' data-validation-required-message='{28}'>
                                                                    <p class='help-block text-danger display_none' id='dangervalidadecc'>{28}</p>
                                                                </div>
                                                            </div>
                                                            <div class='control-group'>
                                                                <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                    <label>{8}</label>
                                                                    <input class='form-control placeholdercolor' id='datanascimento' type='text' placeholder='{8}' required='required' data-validation-required-message='{29}'>
                                                                    <p class='help-block text-danger display_none' id='dangerdatanascimento'>{29}</p>
                                                                </div>
                                                            </div>
                                                            <div class='control-group{35}'>
                                                                <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                    <label>{10}</label>
                                                                    <input class='form-control placeholdercolor' id='contactoemergencia' type='tel' placeholder='{10}' required='required' data-validation-required-message='{30}'>
                                                                    <p class='help-block text-danger display_none' id='dangercontactoemergencia'>{30}</p>
                                                                </div>
                                                            </div>
                                                            <div class='control-group{35}'>
                                                                <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                    <label>{11}</label>
                                                                    <input class='form-control placeholdercolor' id='graucontactoemergencia' type='text' placeholder='{11}' required='required' data-validation-required-message='{31}'>
                                                                    <p class='help-block text-danger display_none' id='dangergraucontactoemergencia'>{31}</p>
                                                                </div>
                                                            </div>
                                                            <div class='control-group'>
                                                                <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                    <label>{17}</label>
                                                                    <input class='form-control placeholdercolor' id='dataAF' type='text' placeholder='{17}' required='required' data-validation-required-message='{32}'>
                                                                    <p class='help-block text-danger display_none' id='dangerdataAF'>{32}</p>
                                                                </div>
                                                            </div>
                                                            <div class='control-group'>
                                                                <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                    <label>{19}</label>
                                                                    <input class='form-control placeholdercolor' id='dataTreino' type='text' placeholder='{19}' required='required' data-validation-required-message='{34}'>
                                                                    <p class='help-block text-danger display_none' id='dangerdataTreino'>{34}</p>
                                                                </div>
                                                            </div>
                                                            <br /><br />
                                                            <p class='lead'>{12}</p>
                                                            <div class='control-group'>
                                                                <div class='form-group floating-label-form-group controls mb-0 pb-2 text-left'>
                                                                    <p class='lead_small'>{13}<br />{14}</p>
                                                                    <input class='form-control placeholdercolor' id='checkboxConsentimento' type='checkbox'>
                                                                </div>
                                                            </div>
                                                            <div id='successPreInscricao' class='text-center' style='margin-top:10px; margin-bottom: 10px; font-size: 1.5rem;'></div>
                                                            <div id='errorPreInscricao' class='text-center' style='margin-top:10px; margin-bottom: 10px; font-size: 1.5rem;'></div>
                                                            <div class='form-group'>
                                                                <button type='button' class='btn btn-light btn-xl mw-100 w-100' id='sendMessageButton' onclick='sendPreInscricao();'>{16}</button>
                                                            </div>
                                                        </div>", fieldNome, fieldMorada, fieldCodPostal, fieldLocalidade, fieldTelemovel, fieldProfissao, 
                                                        fieldCCNr, fieldValidade, fieldDataNascimento, fieldEmail, fieldContactoEmergencia, fieldGrauContactoEmergencia, 
                                                        oDs.Tables[count].Rows[i]["tituloconsentimento"].ToString().ToUpper().Replace("\\n", "<br/>"),
                                                        oDs.Tables[count].Rows[i]["consentimento"].ToString().Replace("\\n", "<br/>"),
                                                        oDs.Tables[count].Rows[i]["checkboxconsentimento"].ToString().Replace("\\n", "<br/>"),
                                                        titulo, oDs.Tables[count].Rows[i]["enviar"].ToString(),
                                                        fieldDataAF, fieldHoraAF, fieldDataPrimeiroTreino,
                                                        validLabel.Replace(FIELD, fieldNome),
                                                        validLabel.Replace(FIELD, fieldMorada),
                                                        validLabel.Replace(FIELD, fieldCodPostal),
                                                        validLabel.Replace(FIELD, fieldLocalidade),
                                                        validLabel.Replace(FIELD, fieldTelemovel),
                                                        validLabel.Replace(FIELD, fieldEmail),
                                                        validLabel.Replace(FIELD, fieldProfissao),
                                                        validLabel.Replace(FIELD, fieldCCNr),
                                                        validLabel.Replace(FIELD, fieldValidade),
                                                        validLabel.Replace(FIELD, fieldDataNascimento),
                                                        validLabel.Replace(FIELD, fieldContactoEmergencia),
                                                        validLabel.Replace(FIELD, fieldGrauContactoEmergencia),
                                                        validLabel.Replace(FIELD, fieldDataAF),
                                                        validLabel.Replace(FIELD, fieldHoraAF),
                                                        validLabel.Replace(FIELD, fieldDataPrimeiroTreino),
                                                        variaveis);
                }
            }
            else
            {
                
            }  
        }
        catch (Exception exc)
        {
            formPreInscricao.Clear();
            formPreInscricao.AppendFormat(@"{0}", exc.ToString());
        }
    }

    private void getLanguage()
    {
        switch(lingua.ToUpper())
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

        if(language == "PT" || language == "ES" || language == "FR")
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
        campaign = Request.QueryString["campaign"];
        separator.InnerHtml = pagina;

        if(String.IsNullOrEmpty(sound))
        {
            sound = "0";
        }

        if(String.IsNullOrEmpty(campaign) || campaign == "0")
        {
            black_overlay_campaign.Attributes.Add("class", "black_overlay variaveis");
            divCampaign.InnerHtml = "";
        }
        else
        {
            loadCampaign();
        }

        if (String.IsNullOrEmpty(lingua))
        {
            lingua = String.IsNullOrEmpty(getLanguageCode()) ? "PT" : getLanguageCode();
            return;
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

    private void getHeader(Boolean showVideo)
    {
        var table = new StringBuilder();
        
        /*if(showVideo)
        {
            if (Request.Browser.IsMobileDevice)
            {
                //header.Attributes.Add("class", "masthead text-white text-center background_page");
                table.AppendFormat(@"   <div id='player'></div>
                                    <div class='container d-flex align-items-center flex-column variaveis' id='headerContainer'>
                                        <!-- Masthead Avatar Image -->
                                        <img class='masthead-avatar mt-5' src='img/logo.png' alt='HappyBody Gym'>

                                        <!-- Masthead Heading -->
                                        <!-- <h1 class='masthead-heading text-uppercase mb-0'>Atelier - Arte dos Trapos</h1> -->

                                        <!-- Icon Divider -->
                                        <!-- <div class='divider-custom divider-light'>
                                            <div class='divider-custom-line'></div>
                                            <div class='divider-custom-icon'>
                                                <i class='fas fa-star'></i>
                                            </div>
                                            <div class='divider-custom-line'></div>
                                        </div> -->

                                        <!-- Masthead Subheading -->
                                        <!-- <p class='masthead-subheading font-weight-light mb-0'>Vestuário e Acessórios</p> -->

                                    </div><span id='isMobile' class='variaveis'>1</span>");
            }
            else
            {
                table.AppendFormat(@"   <video id='video' class='w-100' controlslist='nodownload' preload='auto' autobuffer autoplay playsinline>
                                        <source src='video/video.mp4' type='video/mp4' />
                                        <source src='video/video.mkv' type='video/mkv' />
                                        <source src='video/video.webm' type='video/webm' />
                                    </video>

                                    <div class='container d-flex align-items-center flex-column variaveis' id='headerContainer'>
                                        <!-- Masthead Avatar Image -->
                                        <img class='masthead-avatar mt-5' src='img/logo.png' alt='HappyBody Gym'>

                                        <!-- Masthead Heading -->
                                        <!-- <h1 class='masthead-heading text-uppercase mb-0'>Atelier - Arte dos Trapos</h1> -->

                                        <!-- Icon Divider -->
                                        <!-- <div class='divider-custom divider-light'>
                                            <div class='divider-custom-line'></div>
                                            <div class='divider-custom-icon'>
                                                <i class='fas fa-star'></i>
                                            </div>
                                            <div class='divider-custom-line'></div>
                                        </div> -->

                                        <!-- Masthead Subheading -->
                                        <!-- <p class='masthead-subheading font-weight-light mb-0'>Vestuário e Acessórios</p> -->

                                    </div><span id='isMobile' class='variaveis'>0</span>");
            }

        }
        else
        {
            header.Attributes.Add("class", "masthead text-white text-center background_page");
            table.AppendFormat(@"   
                                    <div class='container d-flex align-items-center flex-column' id='headerContainer'>
                                        <!-- Masthead Avatar Image -->
                                        <img class='masthead-avatar mt-5' src='img/logo.png' alt='HappyBody Gym'>

                                        <!-- Masthead Heading -->
                                        <!-- <h1 class='masthead-heading text-uppercase mb-0'>Atelier - Arte dos Trapos</h1> -->

                                        <!-- Icon Divider -->
                                        <!-- <div class='divider-custom divider-light'>
                                            <div class='divider-custom-line'></div>
                                            <div class='divider-custom-icon'>
                                                <i class='fas fa-star'></i>
                                            </div>
                                            <div class='divider-custom-line'></div>
                                        </div> -->

                                        <!-- Masthead Subheading -->
                                        <!-- <p class='masthead-subheading font-weight-light mb-0'>Vestuário e Acessórios</p> -->

                                    </div><span id='isMobile' class='variaveis'>0</span>");
        }*/

        header.Attributes.Add("class", "masthead text-white text-center background_page w-100 h-100");
        table.AppendFormat(@"   
                                    <div class='container d-flex align-items-center flex-column' id='headerContainer'>
                                        <!-- Masthead Avatar Image -->
                                        <img class='masthead-avatar mt-25' src='img/logo.png' alt='HappyBody Gym'>

                                        <!-- Masthead Heading -->
                                        <!-- <h1 class='masthead-heading text-uppercase mb-0'>Atelier - Arte dos Trapos</h1> -->

                                        <!-- Icon Divider -->
                                        <!-- <div class='divider-custom divider-light'>
                                            <div class='divider-custom-line'></div>
                                            <div class='divider-custom-icon'>
                                                <i class='fas fa-star'></i>
                                            </div>
                                            <div class='divider-custom-line'></div>
                                        </div> -->

                                        <!-- Masthead Subheading -->
                                        <!-- <p class='masthead-subheading font-weight-light mb-0'>Vestuário e Acessórios</p> -->

                                    </div><span id='isMobile' class='variaveis'>0</span>");

        header.InnerHtml = table.ToString();
    }

    private void loadParceiros()
    {
        var table = new StringBuilder();
        var socialMedia = new StringBuilder();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        int intervalClass = 2;
        int val = 0;
        string website = "", facebook = "", instagram = "";

        try
        {
            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_parceiro int;
                                            DECLARE @path varchar(max) = (select concat(site, parceiros) as path from report_paths());

                                            select nome, ordem, concat(@path, icon) as icon, website, facebook, instagram
                                            from HB_WS_REPORT_PARCEIROS(@id_parceiro)
                                            order by ordem asc");

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                table.AppendFormat(@"<div class='col-sm-12 col-md-12 col-lg-12 col-xl-12'><p class='lead text-white'>{0}</p></div>", titleParceiros);

                val = oDs.Tables[0].Rows.Count;
                while (val > 12)
                {
                    val -= 12;
                }
                intervalClass = (int)Math.Floor(12.0 / oDs.Tables[0].Rows.Count);

                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    socialMedia = new StringBuilder();
                    website = oDs.Tables[0].Rows[i]["website"].ToString();
                    facebook = oDs.Tables[0].Rows[i]["facebook"].ToString();
                    instagram = oDs.Tables[0].Rows[i]["instagram"].ToString();

                    if (!String.IsNullOrEmpty(website) || !String.IsNullOrEmpty(facebook) || !String.IsNullOrEmpty(instagram))
                    {
                        socialMedia.AppendFormat(@" <div class='col-sm-12 col-md-12 col-lg-12 col-xl-12 mx-auto text-center'>
                                                        {0}{1}{2}
                                                    </div>",
                                                    !String.IsNullOrEmpty(facebook) ? String.Format(@"<a class='btn btn-outline-light btn-social mx-1' href='{0}' target='_blank'><i class='fab fa-fw fa-facebook-f'></i></a>", facebook) : "",
                                                    !String.IsNullOrEmpty(instagram) ? String.Format(@"<a class='btn btn-outline-light btn-social mx-1' href='{0}' target='_blank'><i class='fab fa-fw fa-instagram'></i></a>", instagram) : "",
                                                    !String.IsNullOrEmpty(website) ? String.Format(@"<a class='btn btn-outline-light btn-social mx-1' href='{0}' target='_blank'><i class='fab fa-fw fa-windows'></i></a>", website) : "");
                    }

                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <div class='col-sm-12 col-md-6 col-lg-{2} col-xl-{2}'>
                                                <div class='col-sm-12 col-md-12 col-lg-12 col-xl-12'>
                                                    <img src='../{0}' alt='{1}' class='img-fluid rounded mb-5' style='width:100%; height: auto;'>
                                                </div>
                                                {3}
                                            </div>", oDs.Tables[0].Rows[i]["icon"].ToString(), oDs.Tables[0].Rows[i]["nome"].ToString(), intervalClass.ToString(), socialMedia.ToString());
                }

                divParceiros.InnerHtml = table.ToString();
            }
            else
            {
                
            }
        }
        catch (Exception exc)
        {
            
        }
    }

    private void loadCampaign()
    {
        var table = new StringBuilder();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        string path = "";
        string title = "";
        string send = "";
        string email = "";
        string campovalido = "";
        string directory = "";

        try
        {
            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @traducao varchar(max);
                                            DECLARE @campanha_traduzida varchar(max);
                                            DECLARE @enviar varchar(max);
                                            DECLARE @email varchar(max);
                                            DECLARE @campovalido varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';

                                            select @traducao = texto_pt from ws_traducoes where campanha_inicial = 1
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @campanha_traduzida output;

                                            SET @traducao = 'ENVIAR';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @enviar output;

                                            SET @traducao = 'Email';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @email output;

                                            SET @traducao = 'Por favor, insira um(a) [FIELD] válido(a)!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @campovalido output;
                                            
                                            select 
                                                concat('../', site, campanha, '/campanha.jpg') as campanha_path,
                                                @campanha_traduzida as title,
                                                @enviar as enviar,
                                                @email as email,
                                                @campovalido as campovalido
                                            from REPORT_PATHS()", lingua);

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
                    path = oDs.Tables[0].Rows[i]["campanha_path"].ToString();
                    title = oDs.Tables[0].Rows[i]["title"].ToString();
                    send = oDs.Tables[0].Rows[i]["enviar"].ToString();
                    email = oDs.Tables[0].Rows[i]["email"].ToString();
                    campovalido = oDs.Tables[0].Rows[i]["campovalido"].ToString();
                }
            }
            else
            {
                path = "img/campanha.jpg";
            }

            table.AppendFormat(@"   <div class='modal-content' style='width: 150% !important;' id='divFormCampaign'>
                                        <button type='button' class='close' onclick='closeCampaign();' style='position: absolute; z-index: 1; right: 1.5rem; top: 1rem; font-size: 3rem; line-height: 3rem; color: #8b0000; opacity: 1;'>
                                            <span style='margin-right: 5px;'>
                                                <i class='fas fa-times'></i>
                                            </span>
                                        </button>
                                        <div class='modal-body text-center'>
                                            <div class='container'>
                                                <div class='row justify-content-center'>
                                                    <div class='col-lg-12 mx-auto text-center'>
                                                        <h5 class='portfolio-modal-title text-secondary text-uppercase mb-0' id='titleCampaign'>{0}</h5>
                                                        <div class='divider-custom'>
                                                            <div class='divider-custom-line'></div>
                                                            <div class='divider-custom-icon'>
                                                                <i class='fas fa-star'></i>
                                                            </div>
                                                            <div class='divider-custom-line'></div>
                                                        </div>
                                                        <img id='imageCampaign' class='img-fluid rounded mb-5' style='max-height:75% !important' src='{1}' alt='{1}'/>
                                                        <div class='control-group'>
                                                            <div class='form-group floating-label-form-group controls mb-0 pb-2'>
                                                                <label id='emailFormCampaignLabel'>{3}</label>
                                                                <input class='form-control' id='emailFormCampaign' type='email' placeholder='{3}' required='required' data-validation-required-message='{4}'>
                                                                <p class='help-block text-danger display_none' id='emailFormCampaignDanger'>{4}</p>
                                                            </div>
                                                        </div>
                                                        <div id='successFormCampaign' class='text-center'></div>
                                                        <div class='form-group'>
                                                            <button type='button' class='btn btn-primary btn-xl mw-100 w-100' id='formCampaignButton' onclick='sendCampaignMail();'>{2}</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>", title, path, send, email, campovalido.Replace("[FIELD]", email));

            divCampaign.InnerHtml = table.ToString();

            directory = Server.MapPath(path);

            if (File.Exists(directory))
            {
                black_overlay_campaign.Style.Add("display", "block");
                black_overlay_campaign.Style.Add("z-index", "1032 !important");
                divCampaign.Attributes.Add("class", "divCampaign modal-dialog-scrollable modal-dialog-centered variaveis");
            }
            else
            {
                black_overlay_campaign.Attributes.Add("class", "black_overlay variaveis");
                divCampaign.InnerHtml = "";
            }
        }
        catch (Exception exc)
        {
            divCampaign.InnerHtml = "";
        }
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

    [WebMethod]
    public static string savePreInscricao(string nome, string morada, string codpostal, string localidade, string tlf_emergencia, string tlm,
        string dataNascimento, string cc, string validadeCC, string profissao, string email, string publicidade,
        string dataAF, string dataTreino, string lingua)
    {
        var table = new StringBuilder();
        string intro = "";
        string body = "";
        string assunto = "";
        string servidor = "";
        string ginasiohappybody = "";
        string rsp = "";
        string _from = "";
        string _emailpwd = "";
        string _smtp = "";
        string _smtpport = "";
        string _emailsend = "";
        string preinscricao = "";
        string welcometohb = "";
        string estamosatuaespera = "";
        string[] emailVector;
        string fb = "", ig = "", whatsapp = "", yt = "", address = "", tlf = "", email_contacto = "";
        int timeout = 50000;
        int i = 0;

        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
	                                        DECLARE @nome varchar(max) = '{0}';
	                                        DECLARE @morada varchar(max) = '{1}';
	                                        DECLARE @codpostal varchar(10) = '{2}';
	                                        DECLARE @localidade varchar(max) = '{3}';
	                                        DECLARE @tlf_emergencia varchar(200) = '{4}';
	                                        DECLARE @telemovel varchar(20) = '{5}';
	                                        DECLARE @data_nascimento datetime = '{6}';
	                                        DECLARE @cc_nr varchar(20) = '{7}';
	                                        DECLARE @validade_cc datetime = '{8}';
	                                        DECLARE @profissao varchar(max) = '{9}';
	                                        DECLARE @email varchar(200) = '{10}';
                                            DECLARE @publicidade bit = {11};
                                            DECLARE @dataAF datetime = '{12}';
                                            DECLARE @dataTreino datetime = '{13}';
                                            DECLARE @lingua varchar(10) = '{14}';
                                            DECLARE @ret varchar(max);
                                            DECLARE @ginasiohappybody varchar(max);
                                            DECLARE @rsp varchar(max);
                                            DECLARE @traducao varchar(max);
                                            DECLARE @preinscricao varchar(max);
                                            DECLARE @bemvindo varchar(max);
                                            DECLARE @estamosatuaespera varchar(max);
                                            DECLARE @emails varchar(max);
                                            DECLARE @telefones varchar(max);
                                            DECLARE @retInt int;

                                            EXEC CRIA_PRE_INSCRICAO @nome, @morada, @codpostal, @localidade, @tlf_emergencia, @telemovel, @data_nascimento, 
                                                @cc_nr, @validade_cc, @profissao, @email, @publicidade, @dataTreino, @dataAF, 
                                                @lingua, @retInt output, @ret output
                                            
                                            SET @traducao = 'Happy Body® Personal Trainer';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @ginasiohappybody output;

                                            SET @traducao = 'Happy Body® Personal Trainer agradece a tua pré-inscrição. Entraremos em contacto contigo o mais brevemente possível. Obrigado!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @rsp output;

                                            SET @traducao = 'Pré-Inscrição Happy Body®';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @preinscricao output;
                        
                                            SET @traducao = 'Bem-Vind@ ao Happy Body®!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @bemvindo output;

                                            SET @traducao = 'Estamos à tua espera na data agendada!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @estamosatuaespera output;

                                            SET @traducao = 'Telefones';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @telefones output;

                                            SET @traducao = 'Emails';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @emails output;

                                            SELECT 
                                                @ginasiohappybody as ginasiohappybody,
                                                @rsp as rsp,
                                                CONCAT(SITE, TEMPLATES) as server,
                                                p.SENDEMAIL_EMAIL,
		                                        p.SENDEMAIL_PASSWORD,
		                                        p.SENDEMAIL_SMTP,
		                                        p.SENDEMAIL_SMTPPORT,
		                                        p.SENDEMAIL_EMAILSEND,
                                                @ret as ret,
                                                @preinscricao as preinscricao,
                                                @bemvindo as bemvindo,
                                                @estamosatuaespera as estamosatuaespera,
                                                cont.morada, 
                                                cont.facebook, 
                                                cont.instagram,
	                                            CASE
		                                            WHEN ISNULL(cont.email2, '') = '' THEN CONCAT(@emails, ': ', cont.email1)
		                                            ELSE CONCAT(@emails, ': ', cont.email1, ' / ', cont.email2)
	                                            END AS email,
                                                CASE
		                                            WHEN ISNULL(cont.tlf2, '') = '' THEN CONCAT(@telefones, ': ', cont.tlf1)
		                                            ELSE CONCAT(@telefones, ': ', cont.tlf1, ' / ', cont.tlf2)
	                                            END AS tlf, 
                                                cont.whatsapp, 
                                                cont.youtube,
                                                @retInt as retInt
                                            FROM REPORT_PATHS() p
                                            INNER JOIN HB_WS_REPORT_CONTACTOS() cont on 1=1",
                                                nome, morada, codpostal, localidade, tlf_emergencia, tlm,
                                             dataNascimento, cc, validadeCC, profissao, email, publicidade == "0" ? "1" : "0", dataAF, dataTreino, lingua);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"{1}<#SEP#>{0}", myReader["ret"].ToString(), myReader["retInt"].ToString());
                    servidor = myReader["server"].ToString();
                    ginasiohappybody = myReader["ginasiohappybody"].ToString();
                    rsp = myReader["rsp"].ToString();
                    _from = myReader["SENDEMAIL_EMAIL"].ToString();
                    _emailpwd = myReader["SENDEMAIL_PASSWORD"].ToString();
                    _smtp = myReader["SENDEMAIL_SMTP"].ToString();
                    _smtpport = myReader["SENDEMAIL_SMTPPORT"].ToString();
                    _emailsend = myReader["SENDEMAIL_EMAILSEND"].ToString();
                    preinscricao = myReader["preinscricao"].ToString();
                    welcometohb = myReader["bemvindo"].ToString();
                    estamosatuaespera = myReader["estamosatuaespera"].ToString();
                    fb = myReader["facebook"].ToString();
                    ig = myReader["instagram"].ToString();
                    whatsapp = myReader["whatsapp"].ToString();
                    yt = myReader["youtube"].ToString();
                    address = myReader["morada"].ToString();
                    tlf = myReader["tlf"].ToString();
                    email_contacto = myReader["email"].ToString();
                }

                emailVector = _emailsend.Split(';');

                connection.Close();
            }
            else
            {
                return "-1<#SEP#>Ocorreu um erro ao inserir a sua pré-inscrição! Por favor, tente novamente! Obrigado!";
            }
        }
        catch (Exception exc)
        {
            return "-1<#SEP#>Ocorreu um erro ao inserir a sua pré-inscrição! Por favor, tente novamente! Obrigado!";
        }

        /*
        //Envia email para HB
        try
        {
            MailMessage mailMessage = new MailMessage();

            string newsletterText = string.Empty;
            newsletterText = File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "//" + servidor + "template.html");

            assunto = intro = preinscricao;
            body = "Nome: " + nome + "<br />"
                + "Morada: " + morada + "<br />"
                + codpostal + " " + localidade + "<br />"
                + "Telefone Emergência: " + tlf_emergencia + "<br />"
                + "Telemóvel: " + tlm + "<br />"
                + "Data de Nascimento: " + dataNascimento + "<br />"
                + "CC: " + cc + " valido até " + validadeCC + "<br />"
                + "Profissão: " + profissao + "<br />"
                + "Email: " + email + "<br />"
                + "Avaliação Física: " + dataAF + "<br />"
                + "1º Treino: " + dataTreino + "<br />"
                + (publicidade == "1" ? "Não quer receber publicidade<br />" : "");

            newsletterText = newsletterText.Replace("[EMAIL_INTRO]", intro);
            newsletterText = newsletterText.Replace("[EMAIL_TEXTBODY]", body);
            newsletterText = newsletterText.Replace("[FB_LINK]", fb);
            newsletterText = newsletterText.Replace("[IG_LINK]", ig);
            newsletterText = newsletterText.Replace("[WHATSAPP]", whatsapp);
            newsletterText = newsletterText.Replace("[YT_LINK]", yt);
            newsletterText = newsletterText.Replace("[ADDRESS]", address);
            newsletterText = newsletterText.Replace("[PHONE]", tlf);
            newsletterText = newsletterText.Replace("[EMAIL_ADDRESS]", email_contacto);

            mailMessage.From = new MailAddress(_from, ginasiohappybody);

            i = 0;

            foreach (var word in emailVector)
            {
                if (i == 0)
                {
                    mailMessage.To.Add(word);
                }
                else
                {
                    mailMessage.Bcc.Add(word);
                }
                i++;
            }

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
            return "-2<#SEP#>Ocorreu um erro ao inserir a pré-inscrição!";
        }

        //Envia email ao cliente
        try
        {
            MailMessage mailMessage = new MailMessage();

            string newsletterText = string.Empty;
            newsletterText = File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "//" + servidor + "template.html");

            assunto = intro = welcometohb;
            body = rsp + "<br />" + estamosatuaespera + "<br />" + dataTreino + "<br />";

            newsletterText = newsletterText.Replace("[EMAIL_INTRO]", intro);
            newsletterText = newsletterText.Replace("[EMAIL_TEXTBODY]", body);
            newsletterText = newsletterText.Replace("[FB_LINK]", fb);
            newsletterText = newsletterText.Replace("[IG_LINK]", ig);
            newsletterText = newsletterText.Replace("[WHATSAPP]", whatsapp);
            newsletterText = newsletterText.Replace("[YT_LINK]", yt);
            newsletterText = newsletterText.Replace("[ADDRESS]", address);
            newsletterText = newsletterText.Replace("[PHONE]", tlf);
            newsletterText = newsletterText.Replace("[EMAIL_ADDRESS]", email_contacto);

            mailMessage.From = new MailAddress(_from, ginasiohappybody);

            mailMessage.To.Add(email);

            i = 0;

            foreach (var word in emailVector)
            {
                mailMessage.Bcc.Add(word);
                i++;
            }

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
            return "-3<#SEP#>Ocorreu um erro ao inserir a pré-inscrição!";
        }*/

        return table.ToString();
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
                                            DECLARE @erroPreInscricao varchar(max);
                                            DECLARE @traducao varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';
                                            
                                            SET @traducao = 'Limpar';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @limpar output;

                                            SET @traducao = 'Cancelar';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @cancelar output;

                                            SET @traducao = 'Confirmar';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @confirmar output;

                                            SET @traducao = 'Ocorreu um erro ao inserir a sua pré-inscrição! Por favor, tente novamente! Obrigado!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @erroPreInscricao output;

                                            SELECT 
                                                UPPER(@limpar) as limpar,
                                                UPPER(@cancelar) as cancelar,
                                                UPPER(@confirmar) as confirmar,
                                                @erroPreInscricao as erroPreInscricao,
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
                    datepicker_clean.InnerHtml = oDs.Tables[count].Rows[i]["limpar"].ToString();
                    datepicker_cancel.InnerHtml = oDs.Tables[count].Rows[i]["cancelar"].ToString();
                    datepicker_confirm.InnerHtml = oDs.Tables[count].Rows[i]["confirmar"].ToString();
                    preinscricao_generic_error.InnerHtml = oDs.Tables[count].Rows[i]["erroPreInscricao"].ToString();
                    servidor_site.InnerHtml = oDs.Tables[count].Rows[i]["SERVIDOR_SITE"].ToString();
                }
                return;
            }
        }
        catch (Exception exc)
        {
            datepicker_clean.InnerHtml = "LIMPAR";
            datepicker_cancel.InnerHtml = "CANCELAR";
            datepicker_confirm.InnerHtml = "CONFIRMAR";
            preinscricao_generic_error.InnerHtml = "Ocorreu um erro ao inserir a sua pré-inscrição! Por favor, tente novamente! Obrigado!";
            servidor_site.InnerHtml = "happybody/";
            return;
        }

        datepicker_clean.InnerHtml = "LIMPAR";
        datepicker_cancel.InnerHtml = "CANCELAR";
        datepicker_confirm.InnerHtml = "CONFIRMAR";
        preinscricao_generic_error.InnerHtml = "Ocorreu um erro ao inserir a sua pré-inscrição! Por favor, tente novamente! Obrigado!";
        servidor_site.InnerHtml = "happybody/";
        return;
    }

    public bool isMobileBrowser()
    {
        //GETS THE CURRENT USER CONTEXT    
        HttpContext context = HttpContext.Current;

        //FIRST TRY BUILT IN ASP.NT CHECK
        //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER  
        //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP 
        if (context.Request.Browser.IsMobileDevice || context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null ||
            (context.Request.ServerVariables["HTTP_ACCEPT"] != null && context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap")))
        {
            return true;
        }

        //AND FINALLY CHECK THE HTTP_USER_AGENT     
        //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING    
        if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
        {
            //Create a list of all mobile types    
            string[] mobiles =
                new[]
            {
            "midp", "j2me", "avant", "docomo",
            "novarra", "palmos", "palmsource",
            "240x320", "opwv", "chtml",
            "pda", "windows ce", "mmp/",
            "blackberry", "mib/", "symbian",
            "wireless", "nokia", "hand", "mobi",
            "phone", "cdm", "up.b", "audio",
            "SIE-", "SEC-", "samsung", "HTC",
            "mot-", "mitsu", "sagem", "sony"
            , "alcatel", "lg", "eric", "vx",
            "NEC", "philips", "mmm", "xx",
            "panasonic", "sharp", "wap", "sch",
            "rover", "pocket", "benq", "java",
            "pt", "pg", "vox", "amoi",
            "bird", "compal", "kg", "voda",
            "sany", "kdd", "dbt", "sendo",
            "sgh", "gradi", "jb", "dddi",
            "moto", "iphone"
            };

            //Loop through each item in the list created above     
            //and check if the header contains that text    
            foreach (string s in mobiles)
            {
                if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                    ToLower().Contains(s.ToLower()))
                {
                    return true;
                }
            }
        }

        return false;
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
