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

public partial class index_old : Page
{
    string separador = "";
    string page = "";

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            separador = HttpContext.Current.Request.Url.PathAndQuery;
            page = Request.QueryString["pagina"];
        }
        catch (Exception exc)
        {
            page = "";
        }

        if (!IsPostBack)
        {
            ClientScriptManager oCsm = this.Page.ClientScript;
            if (!oCsm.IsStartupScriptRegistered(GetType(), "consentimento"))
            {
                
            }

            pagelink.InnerHtml = page;
        }
    }

    [WebMethod]
    public static string sendEmailFromTemplate(string nome, string email, string telefone, string autorizo, string datahora)
    {
        try
        {
            MailMessage mailMessage = new MailMessage();

            string newsletterText = string.Empty;
            newsletterText = File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "\\campanhas//templates//rgpd.html");

            string check = "";

            if(autorizo == "1")
                check = "checked";

            string assunto = "";
            string sendto = "";
            string sendcc = "";
            string sendbcc = "";

            string intro = "";
            string body = "";

            body = "<img src='https://happybody.site/campanhas/img/landing_photo.jpg' style='width:100%; height:auto; margin-bottom: 10px;' />";
            intro += "GINÁSIO HAPPY BODY - COIMBRA";

            body += "Nos termos do RGPD, consinto que os meus dados pessoais (nome, email e telefone ou telemóvel) sejam utilizados para:<br />"
                 + "- Felicitações de aniversário / estatística;<br />"
                 + "- Divulgações de todas as informações, campanhas, horários e aulas;<br />"
                 + "- Marketing ou publicidade (novidades, preços do ginásio);<br />"
                 + "- Ofertas;<br />"
                 + "- Descontos;<br />"
                 + "- Newsletters, artigos e vídeos.<br /><br />"
                 + "<label><input type='checkbox' id='autorizacao' " + check + " disabled>Fui informado dos meus direitos de acesso, retificação, oposição e esquecimento.<br />"
                 + "A conservação dos dados será efetuada desde a presente data até indicação do contrário "
                 + "via email para happybodyfitcoach@gmail.com<br /><br /></label>"
                 + "Nome: " + nome + "<br />"
                 + "Email: " + email + "<br />"
                 + "Telefone / Telemóvel: " + telefone + "<br />"
                 + "Data e Hora de Contacto: " + datahora + "<br />";

            assunto = intro + ": " + nome;
            sendto = email;
            sendbcc = "happybodyfitcoach@gmail.com";

            // ------------------------------------
            // Processa o template 
            // ------------------------------------
            newsletterText = newsletterText.Replace("[EMAIL_INTRO]", intro);
            newsletterText = newsletterText.Replace("[EMAIL_TEXTBODY]", body);


            // ------------------------------------
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

            //smtpClient.Port = Convert.ToInt32(_smtpport);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = mailAuthentication;
            smtpClient.Timeout = 50000;
            smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            smtpClient.Send(mailMessage);
            smtpClient.Dispose();

            var table = new StringBuilder();
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(cs);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @nome varchar(500) = '{0}';
                                            DECLARE @email varchar(250) = '{1}';
                                            DECLARE @telefone varchar(50) = '{2}';
                                            DECLARE @autorizacao bit = {3};
                                            DECLARE @treinoexperimental datetime = '{4}';
                                            DECLARE @link varchar(max) = '{5}';
                                            DECLARE @ip varchar(250) = '{6}';

                                            INSERT INTO RGPD(NOME, EMAIL, TELEFONE, AUTORIZO, TREINOEXPERIMENTAL, LINK, IP)
                                            VALUES(@nome, @email, @telefone, @autorizacao, @treinoexperimental, @link, @ip)", nome,
                                                                                                                            email,
                                                                                                                            telefone,
                                                                                                                            autorizo,
                                                                                                                            datahora,
                                                                                                                            HttpContext.Current.Request.Url.AbsoluteUri,
                                                                                                                            HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            return "0";
        }
        catch (Exception ex)
        {
            return "-1";
        }
    }
}
