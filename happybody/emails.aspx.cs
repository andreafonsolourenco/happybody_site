using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Xml.Linq;
using System.Web.Services.Description;

public partial class emails : Page
{
    private static string ZERO = "0";
    private static string EMPTY = "";
    private static string connectionStringName = "connectionString";
    private static string cs = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
    private static string servidor = "", templatePath = "", ginasiohappybody = "", rsp = "", _from = "", _emailpwd = "", _smtp = "", _smtpport = "", _emailsend = "", pathToTemplate = "";
    private static string[] emailVector;
    private static string fb = "", ig = "", whatsapp = "", yt = "", address = "", tlf = "", email = "", campanha = "", erro = "";
    private static string marcarTreino = "", contacto = "", rgpd = "", preInscricao = "", treinoPersonalizado = "", candidatura = "", rgpdRsp = "", politicaPrivacidade = "";
    private static string testPath = "/";
    private static string url = "https://happybody.site" + testPath;
    private static string urlPoliticaPrivacidade = url + "/politicaprivacidade/index.aspx";

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    [WebMethod]
    public static string sendCampaignEmail(string sendto, string language)
    {
        string type = "CAMPANHA";
        string ret = saveEmailTempOnDB(type, ZERO, ZERO, ZERO, ZERO, EMPTY, sendto, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, language);
        string[] retSplit = ret.Split('#');

        getTranslations(type, language);
        string introCustomer = campanha + "<br /><img src='" + url + servidor + "//img//campanha.jpg' style='width:100%; height: auto;' />";
        string subjectCustomer = campanha;
        pathToTemplate = HttpContext.Current.Server.MapPath("~") + testPath + servidor + templatePath + "template.html";
        string bodyCustomer = getEmailTemplate(introCustomer, rsp);
        string introHB = "Campanha Happy Body® Personal Trainer<br /><img src='" + url + servidor + "//img//campanha.jpg' style='width:100%; height: auto;' />";
        string subjectHB = "Campanha Happy Body® Personal Trainer";
        string bodyHB = "A pessoa com o seguinte email<br /><br />" + sendto + "<br /><br />deseja receber mais informações acerca da campanha em vigor.<br /><br />Por favor, responder em " + language;
        bodyHB = getEmailTemplate(introHB, bodyHB);
        sendMail(sendto, type, subjectCustomer, bodyCustomer, subjectHB, bodyHB);

        return retSplit[0] + "<#SEP#>" + retSplit[1];
    }

    [WebMethod]
    public static string sendServicesEmail(string name, string email, string tlf, string age, string date, string hour, string idService, string language)
    {
        string type = "SERVIÇOS";
        string ret = saveEmailTempOnDB(type, ZERO, idService, ZERO, ZERO, name, email, tlf, age, date, hour, EMPTY, EMPTY, language);
        string[] retSplit = ret.Split('#');
        string servicoPT = "", servicoLingua = "", img = "", serviceInfo = "";

        getTranslations(type, language);
        serviceInfo = getServicesData(idService, language);
        string[] serviceDataSplit = serviceInfo.Split(new string[] { "<#SEP#>" }, StringSplitOptions.None);
        servicoPT = serviceDataSplit[0];
        servicoLingua = serviceDataSplit[1];
        img = serviceDataSplit[2];
        string introCustomer = ginasiohappybody + "<br />" + servicoLingua + "<br /><img src='" + url + servidor + img + "' style='width:100%; height: auto;' />";
        string subjectCustomer = ginasiohappybody + " - " + servicoLingua;
        pathToTemplate = HttpContext.Current.Server.MapPath("~") + testPath + servidor + templatePath + "template.html";
        string bodyCustomer = getEmailTemplate(introCustomer, rsp);
        string introHB = ginasiohappybody + "<br />" + servicoPT + "<br /><img src='" + url + servidor + img + "' style='width:100%; height: auto;' />";
        string subjectHB = ginasiohappybody + " - " + servicoPT;
        string bodyHB = "A pessoa com os seguintes dados<br /><br />";
        bodyHB += "Nome: " + name + "<br />Email: " + email + "<br />Telefone: " + tlf + "<br />Idade: " + age + "<br /><br /> ";
        bodyHB += "deseja efetuar uma marcação para o serviço " + servicoPT + " no dia " + date + " às " + hour + " horas!<br /><br />Por favor, responder em " + language;
        bodyHB = getEmailTemplate(introHB, bodyHB);
        sendMail(email, type, subjectCustomer, bodyCustomer, subjectHB, bodyHB);

        return retSplit[0] + "<#SEP#>" + retSplit[1];
    }

    [WebMethod]
    public static string sendMarcarTreinoEmail(string name, string email, string tlf, string language)
    {
        string type = "MARCAR TREINO";
        string ret = saveEmailTempOnDB(type, ZERO, ZERO, ZERO, ZERO, name, email, tlf, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, language);
        string[] retSplit = ret.Split('#');

        getTranslations(type, language);
        string introCustomer = ginasiohappybody + " - " + marcarTreino;
        string subjectCustomer = ginasiohappybody + " - " + marcarTreino;
        pathToTemplate = HttpContext.Current.Server.MapPath("~") + testPath + servidor + templatePath + "template.html";
        string bodyCustomer = getEmailTemplate(introCustomer, rsp);
        string introHB = ginasiohappybody + " - " + marcarTreino;
        string subjectHB = ginasiohappybody + " - " + marcarTreino;
        string bodyHB = "A pessoa com os seguintes dados<br /><br />";
        bodyHB += "Nome: " + name + "<br />Email: " + email + "<br />Telefone: " + tlf + "<br /><br /> ";
        bodyHB += "deseja efetuar uma marcação de treino!<br /><br />Por favor, responder em " + language;
        bodyHB = getEmailTemplate(introHB, bodyHB);
        sendMail(email, type, subjectCustomer, bodyCustomer, subjectHB, bodyHB);

        return retSplit[0] + "<#SEP#>" + retSplit[1];
    }

    [WebMethod]
    public static string sendContactEmail(string name, string email, string tlf, string subject, string emailText, string language)
    {
        string type = "CONTACTOS";
        string ret = saveEmailTempOnDB(type, ZERO, ZERO, ZERO, ZERO, name, email, tlf, EMPTY, EMPTY, EMPTY, subject, emailText, language);
        string[] retSplit = ret.Split('#');

        getTranslations(type, language);
        string introCustomer = ginasiohappybody + " - " + contacto + " - " + subject;
        string subjectCustomer = ginasiohappybody + " - " + contacto + " - " + subject;
        pathToTemplate = HttpContext.Current.Server.MapPath("~") + testPath + servidor + templatePath + "template.html";
        string bodyCustomer = getEmailTemplate(introCustomer, rsp);
        string introHB = ginasiohappybody + " - " + contacto + " - " + subject;
        string subjectHB = ginasiohappybody + " - " + contacto + " - " + subject;
        string bodyHB = "A pessoa com os seguintes dados<br /><br />";
        bodyHB += "Nome: " + name + "<br />Email: " + email + "<br />Telefone: " + tlf + "<br /><br /> ";
        bodyHB += "enviou o seguinte email através do website:<br /><br />" + emailText + "<br /><br />Por favor, responder em " + language;
        bodyHB = getEmailTemplate(introHB, bodyHB);
        sendMail(email, type, subjectCustomer, bodyCustomer, subjectHB, bodyHB);

        return retSplit[0] + "<#SEP#>" + retSplit[1];
    }

    [WebMethod]
    public static string sendPreInscricaoEmail(string id_pre_inscricao, string language)
    {
        string type = "PRÉ INSCRIÇÃO";
        string ret = saveEmailTempOnDB(type, id_pre_inscricao, ZERO, ZERO, ZERO, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, language);
        string[] retSplit = ret.Split('#');

        getTranslations(type, language);
        string introCustomer = ginasiohappybody + "<br />" + preInscricao;
        string subjectCustomer = ginasiohappybody + " - " + preInscricao;
        pathToTemplate = HttpContext.Current.Server.MapPath("~") + testPath + servidor + templatePath + "template.html";
        string bodyCustomer = getEmailTemplate(introCustomer, rsp);
        string introHB = ginasiohappybody + "<br />" + preInscricao;
        string subjectHB = ginasiohappybody + " - " + preInscricao;
        string preInscriptionData = getPreInscriptionData(id_pre_inscricao, language);
        string[] preInscriptionDataSplit = preInscriptionData.Split(new string[] { "<#SEP#>" }, StringSplitOptions.None);
        string bodyHB = preInscriptionDataSplit[0];
        string sendTo = preInscriptionDataSplit[1];
        bodyHB = getEmailTemplate(introHB, bodyHB);
        sendMail(email, type, subjectCustomer, bodyCustomer, subjectHB, bodyHB);

        return retSplit[0] + "<#SEP#>" + retSplit[1];
    }

    [WebMethod]
    public static string sendTreinoPersonalizadoEmail(string name, string email, string tlf, string age, string date, string language, string id_modalidade)
    {
        string type = "TREINO PERSONALIZADO";
        string ret = saveEmailTempOnDB(type, ZERO, ZERO, id_modalidade, ZERO, name, email, tlf, age, date, EMPTY, EMPTY, EMPTY, language);
        string[] retSplit = ret.Split('#');
        string modalidadePT = "", modalidadeLingua = "", img = "", modalidadeInfo = "";

        getTranslations(type, language);
        modalidadeInfo = getModalidadeData(id_modalidade, language);
        string[] modalidadeDataSplit = modalidadeInfo.Split(new string[] { "<#SEP#>" }, StringSplitOptions.None);
        modalidadePT = modalidadeDataSplit[0];
        modalidadeLingua = modalidadeDataSplit[1];
        img = modalidadeDataSplit[2];
        string introCustomer = ginasiohappybody + "<br />" + treinoPersonalizado + " - " + modalidadeLingua + "<br /><img src='" + url + servidor + img + "' style='width:100%; height: auto;' />";
        string subjectCustomer = ginasiohappybody + " - " + treinoPersonalizado + " - " + modalidadeLingua;
        pathToTemplate = HttpContext.Current.Server.MapPath("~") + testPath + servidor + templatePath + "template.html";
        string bodyCustomer = getEmailTemplate(introCustomer, rsp);
        string introHB = ginasiohappybody + "<br />Marcar Treino Personalizado - " + modalidadePT + "<br /><img src='" + url + servidor + img + "' style='width:100%; height: auto;' />";
        string subjectHB = ginasiohappybody + " - Marcar Treino Personalizado - " + modalidadePT;
        string bodyHB = "A pessoa com os seguintes dados<br /><br />";
        bodyHB += "Nome: " + name + "<br />Email: " + email + "<br />Telefone: " + tlf + "<br />Idade: " + age + "<br /><br /> ";
        bodyHB += "deseja efetuar uma marcação de treino personalizado de " + modalidadePT + " para o dia " + date + "!<br /><br />Por favor, responder em " + language;
        bodyHB = getEmailTemplate(introHB, bodyHB);
        sendMail(email, type, subjectCustomer, bodyCustomer, subjectHB, bodyHB);

        return retSplit[0] + "<#SEP#>" + retSplit[1];
    }

    [WebMethod]
    public static string sendRGPDConsentEmail(string name, string email, string tlf, string date, string hour, string language)
    {
        string type = "RGPD";
        string ret = saveEmailTempOnDB(type, ZERO, ZERO, ZERO, ZERO, name, email, tlf, EMPTY, date, hour, EMPTY, EMPTY, language);
        string[] retSplit = ret.Split('#');

        getTranslations(type, language);
        string introCustomer = ginasiohappybody + " - " + rgpd;
        string subjectCustomer = ginasiohappybody + " - " + rgpd;
        pathToTemplate = HttpContext.Current.Server.MapPath("~") + testPath + servidor + templatePath + "template.html";
        string customer = rsp + "<br />" + rgpdRsp + "<br /><a href='" + urlPoliticaPrivacidade + "?language=" + language + "' target='_blank'>" + politicaPrivacidade + "</a>";
        string bodyCustomer = getEmailTemplate(introCustomer, customer);
        string introHB = ginasiohappybody + " - " + rgpd;
        string subjectHB = ginasiohappybody + " - " + rgpd;
        string bodyHB = "A pessoa com os seguintes dados<br /><br />";
        bodyHB += "Nome: " + name + "<br />Email: " + email + "<br />Telefone: " + tlf + "<br /><br /><br /> ";
        bodyHB += "deseja efetuar uma marcação de treino para o dia " + date + " às " + hour + " horas! <br />< br /> Por favor, responder em " + language;
        bodyHB = getEmailTemplate(introHB, bodyHB);
        sendMail(email, type, subjectCustomer, bodyCustomer, subjectHB, bodyHB);

        return retSplit[0] + "<#SEP#>" + retSplit[1];
    }

    [WebMethod]
    public static string sendRGPDEmail(string name, string email, string tlf, string treinooferecidopor, string language)
    {
        string type = "RGPD";
        string ret = saveEmailTempOnDB(type, ZERO, ZERO, ZERO, ZERO, name, email, tlf, EMPTY, EMPTY, EMPTY, EMPTY, treinooferecidopor, language);
        string[] retSplit = ret.Split('#');

        getTranslations(type, language);
        string introCustomer = ginasiohappybody + " - " + rgpd;
        string subjectCustomer = ginasiohappybody + " - " + rgpd;
        pathToTemplate = HttpContext.Current.Server.MapPath("~") + testPath + servidor + templatePath + "template.html";
        string customer = rsp + "<br />" + rgpdRsp + "<br /><a href='" + urlPoliticaPrivacidade + "?language=" + language + "' target='_blank'>" + politicaPrivacidade + "</a>";
        string bodyCustomer = getEmailTemplate(introCustomer, customer);
        string introHB = ginasiohappybody + " - " + rgpd;
        string subjectHB = ginasiohappybody + " - " + rgpd;
        string bodyHB = "A pessoa com os seguintes dados<br /><br />";
        bodyHB += "Nome: " + name + "<br />Email: " + email + "<br />Telefone: " + tlf + "<br /><br /><br /> ";
        bodyHB += "deseja efetuar uma marcação de treino oferecida por " + treinooferecidopor + "<br />< br /> Por favor, responder em " + language;
        bodyHB = getEmailTemplate(introHB, bodyHB);
        sendMail(email, type, subjectCustomer, bodyCustomer, subjectHB, bodyHB);

        return retSplit[0] + "<#SEP#>" + retSplit[1];
    }

    [WebMethod]
    public static string sendRecruitmentEmail(string idRecrutamento, string language)
    {
        string type = "RECRUTAMENTO";
        string ret = saveEmailTempOnDB(type, ZERO, ZERO, ZERO, idRecrutamento, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, language);
        string[] retSplit = ret.Split('#');

        getTranslations(type, language);
        string introCustomer = ginasiohappybody + " - " + candidatura;
        string subjectCustomer = ginasiohappybody + " - " + candidatura;
        pathToTemplate = HttpContext.Current.Server.MapPath("~") + testPath + servidor + templatePath + "template.html";
        string bodyCustomer = getEmailTemplate(introCustomer, rsp);
        string introHB = ginasiohappybody + " - " + candidatura;
        string subjectHB = ginasiohappybody + " - " + candidatura;
        string recruitmentData = getRecruitmentData(idRecrutamento, language);
        string[] recruitmentDataSplit = recruitmentData.Split(new string[] { "<#SEP#>" }, StringSplitOptions.None);
        string bodyHB = recruitmentDataSplit[0];
        string sendTo = recruitmentDataSplit[1];
        bodyHB = getEmailTemplate(introHB, bodyHB);
        sendMail(sendTo, type, subjectCustomer, bodyCustomer, subjectHB, bodyHB);

        return retSplit[0] + "<#SEP#>" + retSplit[1];
    }

    [WebMethod]
    public static string saveSendEmailError(Exception e, string emailType)
    {
        string ret = "", retMsg = "";

        try
        {
            SqlConnection connection = new SqlConnection(cs);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            declare @emailtype varchar(200) = '{0}';
                                            declare @error nvarchar(max) = '{1}';
                                            declare @ret int;
                                            declare @retMsg varchar(max);

                                            EXEC save_email_error @emailtype, @error, @ret output, @retMsg output;

                                            select @ret as ret, @retMsg as retMsg", emailType, e.ToString());

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    ret = myReader["ret"].ToString();
                    retMsg = myReader["retMsg"].ToString();
                }
            }

            connection.Close();

            return ret;
        }
        catch (Exception exc)
        {
            return "-999";
        }
    }

    [WebMethod]
    public static string saveSendEmailError(string e, string emailType)
    {
        string ret = "", retMsg = "";

        try
        {
            SqlConnection connection = new SqlConnection(cs);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            declare @emailtype varchar(200) = '{0}';
                                            declare @error nvarchar(max) = '{1}';
                                            declare @ret int;
                                            declare @retMsg varchar(max);

                                            EXEC save_email_error @emailtype, @error, @ret output, @retMsg output;

                                            select @ret as ret, @retMsg as retMsg", emailType, e);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    ret = myReader["ret"].ToString();
                    retMsg = myReader["retMsg"].ToString();
                }
            }

            connection.Close();

            return ret;
        }
        catch (Exception exc)
        {
            return "-999";
        }
    }

    [WebMethod]
    public static string saveEmailTempOnDB(string emailType, string id_pre_inscricao, string id_servico, string id_modalidade, string id_recrutamento, string name, string email, 
        string tlf, string age, string date, string hour, string subject, string emailText, string language)
    {
        string ret = "", retMsg = "";

        try
        {
            SqlConnection connection = new SqlConnection(cs);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            declare @tipo_email varchar(max) = '{0}'
	                                        declare @id_pre_inscricao int = {1};
	                                        declare @id_servico int = {2};
	                                        declare @id_modalidade int = {3};
	                                        declare @nome varchar(max) = '{4}';
	                                        declare @email varchar(max) = '{5}';
	                                        declare @telefone varchar(max) = '{6}';
	                                        declare @idade varchar(max) = '{7}';
	                                        declare @data varchar(max) = '{8}';
	                                        declare @hora varchar(max) = '{9}';
	                                        declare @assunto varchar(max) = '{10}';
	                                        declare @texto varchar(max) = '{11}';
	                                        declare @lingua varchar(max) = '{12}';
                                            declare @id_recrutamento int = {13};
                                            declare @textoPT varchar(max) = 'Muito obrigado! Iremos responder com a maior brevidade possível!';
                                            declare @ret int;
                                            declare @retMsg varchar(max);

                                            EXEC insere_novo_email @tipo_email, @id_pre_inscricao, @id_servico, @id_modalidade, @id_recrutamento, @nome, @email, @telefone, @idade, 
                                                @data, @hora, @assunto, @texto, @lingua, @ret OUTPUT, @retMsg OUTPUT

                                            EXEC DEVOLVE_TRADUCAO @textoPT, @lingua, @retMsg output;

                                            select @ret as ret, @retMsg as retMsg", emailType, id_pre_inscricao, id_servico, id_modalidade, name, email, tlf,
                                                age, date, hour, subject, emailText, language, id_recrutamento);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    ret = myReader["ret"].ToString();
                    retMsg = myReader["retMsg"].ToString();
                }
            }

            connection.Close();

            return ret + "#" + retMsg;
        }
        catch (Exception exc)
        {
            return "-999#ERRO";
        }
    }

    public static Boolean sendMail(string sendto, string type, string subjectCustomer, string emailCustomer, string subjectHB, string emailHB)
    {
        int timeout = 50000;
        int i = 0;

        try
        {
            // Envio de email para HB
            MailMessage mailMessage = new MailMessage();
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

            mailMessage.Subject = subjectHB;
            mailMessage.Body = emailHB;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.BodyEncoding = Encoding.UTF8;

            SmtpClient smtpClient = new SmtpClient(_smtp);
            NetworkCredential mailAuthentication = new NetworkCredential(_from, _emailpwd);

            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = mailAuthentication;
            smtpClient.EnableSsl = false;
            //smtpClientHB.Port = Int32.Parse(_smtpport);
            smtpClient.Timeout = timeout;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpClient.Send(mailMessage);
            smtpClient.Dispose();


            // Envio de email para o cliente
            mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_from, ginasiohappybody);
            mailMessage.To.Add(sendto);

            i = 0;

            foreach (var word in emailVector)
            {
                mailMessage.Bcc.Add(word);
            }

            mailMessage.Subject = subjectCustomer;
            mailMessage.Body = emailCustomer;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.BodyEncoding = Encoding.UTF8;

            smtpClient = new SmtpClient(_smtp);
            mailAuthentication = new NetworkCredential(_from, _emailpwd);

            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = mailAuthentication;
            smtpClient.EnableSsl = false;
            //smtpClientHB.Port = Int32.Parse(_smtpport);
            smtpClient.Timeout = timeout;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpClient.Send(mailMessage);
            smtpClient.Dispose();

        }
        catch (Exception ex)
        {
            saveSendEmailError(ex, type);
            return false;
        }

        return true;
    }

    public static string getEmailTemplate(string intro, string body)
    {
        string template = File.ReadAllText(pathToTemplate);
        template = template.Replace("[EMAIL_INTRO]", intro);
        template = template.Replace("[EMAIL_TEXTBODY]", body);
        template = template.Replace("[FB_LINK]", fb);
        template = template.Replace("[IG_LINK]", ig);
        template = template.Replace("[WHATSAPP]", whatsapp);
        template = template.Replace("[YT_LINK]", yt);
        template = template.Replace("[ADDRESS]", address);
        template = template.Replace("[PHONE]", tlf);
        template = template.Replace("[EMAIL_ADDRESS]", email);

        return template;
    }

    public static void getTranslations(string type, string language)
    {
        try
        {
            SqlConnection connection = new SqlConnection(cs);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @hb varchar(max);
                                            DECLARE @rsp varchar(max);
                                            DECLARE @campanha varchar(max);
                                            DECLARE @telefones varchar(max);
                                            DECLARE @emails varchar(max);
                                            DECLARE @erro varchar(max);
                                            DECLARE @marcarTreino varchar(max);
                                            DECLARE @marcarTreinoPersonalizado varchar(max);
                                            DECLARE @preInscricao varchar(max);
                                            DECLARE @recrutamento varchar(max);
                                            DECLARE @consentimento varchar(max);
                                            DECLARE @consentimentoRsp varchar(max);
                                            DECLARE @politicaPrivacidade varchar(max);
                                            DECLARE @contacto varchar(max);
                                            DECLARE @traducao varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';

                                            SET @traducao = 'Happy Body® Personal Trainer';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @hb output;

                                            SET @traducao = 'Agradecemos o teu email!<br />Entraremos em contacto contigo com a maior brevidade possível! Obrigado!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @rsp output;

                                            SELECT
	                                            @emails = case when isnull(email1, '') <> '' and ISNULL(email2, '') <> '' then 'Emails' else 'Email' end,
	                                            @telefones = case when isnull(tlf1, '') <> '' and ISNULL(tlf2, '') <> '' then 'Telefones' else 'TelefoneEmail' end
                                            FROM HB_WS_REPORT_CONTACTOS()

                                            SET @traducao = @telefones;
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @telefones output;

                                            SET @traducao = @emails;
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @emails output;

                                            SET @traducao = 'Campanha Happy Body® Personal Trainer';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @campanha output;

                                            SET @traducao = 'Ocorreu um erro ao enviar o email.<br />Por favor, tente mais tarde.';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @erro output;

                                            SET @traducao = 'Marcar Treino';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @marcarTreino output;
            
                                            SET @traducao = 'Pré-Inscrição';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @preInscricao output;

                                            SET @traducao = 'Marcar Treino Personalizado';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @marcarTreinoPersonalizado output;

                                            SET @traducao = 'Recrutamento';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @recrutamento output;
            
                                            SET @traducao = 'CONSENTIMENTO DO TITULAR DE DADOS INFORMADO';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @consentimento output;

                                            SET @traducao = 'Contacto através do website happybody.site';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @contacto output;

                                            SET @traducao = 'Fui informado dos meus direitos de acesso, retificação, oposição e esquecimento. A conservação dos dados será efetuada desde a presente data até indicação do contrário via email para happybodyfitcoach@gmail.com.'
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @consentimentoRsp output;

                                            SET @traducao = 'Em caso de dúvida poderá aceder à nossa política de privacidade aqui!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @politicaPrivacidade output;

                                            SELECT 
                                                @hb as ginasiohappybody,
                                                @rsp as rsp,
                                                @campanha as campanha,
                                                @erro as erro,
                                                @marcarTreino as marcarTreino,
                                                @marcarTreinoPersonalizado as marcarTreinoPersonalizado,
                                                @preInscricao as preInscricao,
                                                @recrutamento as recrutamento,
                                                @consentimento as consentimento,
                                                @contacto as contacto,
                                                @consentimentoRsp as consentimentoRsp,
                                                @politicaPrivacidade as politicaPrivacidade,
                                                paths.SITE as server,
                                                paths.TEMPLATES as template_path,
                                                paths.SENDEMAIL_EMAIL,
                                                paths.SENDEMAIL_PASSWORD,
                                                paths.SENDEMAIL_SMTP,
                                                paths.SENDEMAIL_SMTPPORT,
                                                paths.SENDEMAIL_EMAILSEND,
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
                                                cont.youtube
                                            FROM REPORT_PATHS() paths
                                            INNER JOIN HB_WS_REPORT_CONTACTOS() cont on 1=1", language);

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
                    fb = myReader["facebook"].ToString();
                    ig = myReader["instagram"].ToString();
                    whatsapp = myReader["whatsapp"].ToString();
                    yt = myReader["youtube"].ToString();
                    address = myReader["morada"].ToString();
                    tlf = myReader["tlf"].ToString();
                    email = myReader["email"].ToString();
                    campanha = myReader["campanha"].ToString();
                    erro = myReader["erro"].ToString();
                    templatePath = myReader["template_path"].ToString();
                    marcarTreino = myReader["marcarTreino"].ToString();
                    treinoPersonalizado = myReader["marcarTreinoPersonalizado"].ToString();
                    preInscricao = myReader["preInscricao"].ToString();
                    candidatura = myReader["recrutamento"].ToString();
                    rgpd = myReader["consentimento"].ToString();
                    contacto = myReader["contacto"].ToString();
                    rgpdRsp = myReader["consentimentoRsp"].ToString();
                    politicaPrivacidade = myReader["politicaPrivacidade"].ToString();
                }
            }

            emailVector = _emailsend.Split(';');
            connection.Close();
        }
        catch (Exception exc)
        {
            saveSendEmailError(exc, type);
        }
    }

    public static string getServicesData(string id, string language)
    {
        string designacaoPT = "", designacaoLingua = "", img = "";

        try
        {
            SqlConnection connection = new SqlConnection(cs);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @traducao varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';
                                            DECLARE @id int = {1};
                                            DECLARE @designacaoPT varchar(500);
                                            DECLARE @designacaoLingua varchar(500);
                                            DECLARE @img varchar(max);
                                            DECLARE @path varchar(500) = (select servicos from report_paths());

                                            select @designacaoLingua = designacao, @img = imagem from HB_WS_REPORT_SERVICOS(@id, @lingua)
                                            select @designacaoPT = designacao from HB_WS_REPORT_SERVICOS(@id, 'PT')

                                            select 
                                                @designacaoPT as designacaoPT, 
                                                @designacaoLingua as designacaoLingua,
                                                CONCAT(@path, '/', @img) as imgPath", language, id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    designacaoPT = myReader["designacaoPT"].ToString();
                    designacaoLingua = myReader["designacaoLingua"].ToString();
                    img = myReader["imgPath"].ToString();
                }
            }

            connection.Close();
        }
        catch (Exception exc)
        {
            
        }

        return designacaoPT +"<#SEP#>" + designacaoLingua + "<#SEP#>" + img;
    }

    public static string getPreInscriptionData(string id, string language)
    {
        string nome = "", morada = "", codPostal = "", localidade = "", telefone = "", email = "", dataTreino = "", dataAF = "";

        try
        {
            SqlConnection connection = new SqlConnection(cs);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id int = {0};

                                            select nome, morada, codpostal, localidade, telemovel, email, data_primeiro_treino, data_hora_primeira_af from report_pre_inscricoes(@id)", id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    nome = myReader["nome"].ToString();
                    morada = myReader["morada"].ToString();
                    codPostal = myReader["codpostal"].ToString();
                    localidade = myReader["localidade"].ToString();
                    telefone = myReader["telemovel"].ToString();
                    email = myReader["email"].ToString();
                    dataTreino = myReader["data_primeiro_treino"].ToString();
                    dataAF = myReader["data_hora_primeira_af"].ToString();
                }
            }

            connection.Close();
        }
        catch (Exception exc)
        {

        }

        string rsp = "A pessoa com os seguintes dados<br /><br />";
        rsp += "Nome: " + nome + "<br />Email: " + email + "<br />Telefone: " + telefone + "<br />Morada: " + morada + "<br />Localidade: " + codPostal + " " + localidade;
        rsp += "<br />Data Primeiro Treino: " + dataTreino + "<br />Data Primeira AF: " + dataAF + "<br /><br />";
        rsp += "efetuou a sua Pré-Inscrição!<br /><br />Por favor, responder em " + language;
        rsp += "<#SEP#>" + email;

        return rsp;
    }

    public static string getRecruitmentData(string id, string language)
    {
        string nome = "", telefone = "", email = "", texto = "", doc = "", tipo = "";

        try
        {
            SqlConnection connection = new SqlConnection(cs);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id int = {0};

                                            select NOME, EMAIL, TLF, TEXTO, TIPO, CONCAT(id_candidatura, '.', EXTENSAO) as doc from hb_software_report_ws_candidaturas(@id, null)", id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    nome = myReader["nome"].ToString();
                    texto = myReader["TEXTO"].ToString();
                    doc = myReader["doc"].ToString().Replace("..", ".");
                    tipo = myReader["tipo"].ToString();
                    telefone = myReader["TLF"].ToString();
                    email = myReader["email"].ToString();
                }
            }

            connection.Close();
        }
        catch (Exception exc)
        {

        }

        string rsp = "A pessoa com os seguintes dados<br /><br />";
        rsp += "Nome: " + nome + "<br />Email: " + email + "<br />Telefone: " + telefone + "<br />Doc: " + doc;
        rsp += "<br />efetuou a sua candidatura para o cargo: " + tipo + "<br /><br />" + texto + "<br /><br />";
        rsp += "Por favor, responder em " + language;
        rsp += "<#SEP#>" + email;

        return rsp;
    }

    public static string getModalidadeData(string id, string language)
    {
        string designacaoPT = "", designacaoLingua = "", img = "";

        try
        {
            SqlConnection connection = new SqlConnection(cs);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @traducao varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';
                                            DECLARE @id int = {1};
                                            DECLARE @designacaoPT varchar(500);
                                            DECLARE @designacaoLingua varchar(500);
                                            DECLARE @img varchar(max);
                                            DECLARE @path varchar(500) = (select modalidades from report_paths());

                                            select @designacaoLingua = titulo, @img = ficheiro from HB_WS_REPORT_GALERIA_MODALIDADES(@id, @lingua)
                                            select @designacaoPT = titulo from HB_WS_REPORT_GALERIA_MODALIDADES(@id, 'PT')

                                            select 
                                                @designacaoPT as designacaoPT, 
                                                @designacaoLingua as designacaoLingua,
                                                CONCAT(@path, '/', @img) as imgPath", language, id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    designacaoPT = myReader["designacaoPT"].ToString();
                    designacaoLingua = myReader["designacaoLingua"].ToString();
                    img = myReader["imgPath"].ToString();
                }
            }

            connection.Close();
        }
        catch (Exception exc)
        {

        }

        return designacaoPT + "<#SEP#>" + designacaoLingua + "<#SEP#>" + img;
    }
}
