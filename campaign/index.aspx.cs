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

public partial class index : Page
{
    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClientScriptManager oCsm = this.Page.ClientScript;
            if (!oCsm.IsStartupScriptRegistered(GetType(), "gymsummerpack"))
            {
                
            }
        }

        getPage();
    }

    [WebMethod]
    public static string sendEmailFromTemplate(string nome, string email, string telefone, string autorizo, string treinooferecidopor)
    {
        try
        {
            MailMessage mailMessage = new MailMessage();

            string newsletterText = string.Empty;
            newsletterText = File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "\\general//templates//rgpd.html");

            string check = "";

            if(autorizo == "1")
                check = "checked";

            string assunto = "";
            string sendto = "";
            string sendcc = "";
            string sendbcc = "";

            string intro = "HAPPYBODY GYM SUMMER PACK";
            string body = "<img src='https://happybody.site/general/img/gymsummer.jpg' style='width:100%; height:auto;' />Nos termos do RGPD, consinto que os meus dados pessoais (nome, email e telefone ou telemóvel) sejam utilizados para receber informação acerca da Campanha Gym Summer Pack.<br />"
                + "Nome: " + nome + "<br />"
                + "Email: " + email + "<br />"
                + "Telefone / Telemóvel: " + telefone + "<br />";

            assunto = intro + ": " + nome;
            sendto = email;
            sendbcc = "happybodyfitcoach@gmail.com";

            // ------------------------------------
            // Processa o template 
            // ------------------------------------
            newsletterText = newsletterText.Replace("[EMAIL_INTRO]", intro);
            newsletterText = newsletterText.Replace("[EMAIL_TEXTBODY]", body);
            //newsletterText = newsletterText.Replace("[EMAIL_INTROIMAGE]", "  <img style='width:280px;height:100px' src='http://teu site publico/" + lic_num + @"/logocustomer.png'  alt='Logo'  data-default='placeholder' /> ");
            //newsletterText = newsletterText.Replace("[EMAIL_RODAPEIMAGE]", "  <img style='width:200px;height:50px' src='http:// teu site publico /" + lic_num + @"/logocustomer.png'    alt='Logo'  data-default='placeholder' /> ");
            //newsletterText = newsletterText.Replace("[EMAIL_LICNAME]", lic_name);
            //newsletterText = newsletterText.Replace("[EMAIL_LICEMAIL]", lic_email);


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
                                            DECLARE @treinooferecidopor varchar(500) = '{4}';
                                            DECLARE @link varchar(max) = '{5}';
                                            DECLARE @ip varchar(250) = '{6}';

                                            INSERT INTO RGPD(NOME, EMAIL, TELEFONE, AUTORIZO, TREINO_OFERECIDO_POR, LINK, IP)
                                            VALUES(@nome, @email, @telefone, @autorizacao, @treinooferecidopor, @link, @ip)", nome, 
                                                                                                                            email, 
                                                                                                                            telefone, 
                                                                                                                            autorizo, 
                                                                                                                            treinooferecidopor,
                                                                                                                            HttpContext.Current.Request.Url.AbsoluteUri,
                                                                                                                            HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());

            //return sql;

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

    private void getPage()
    {
        var table = new StringBuilder();
        var language = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        string textPT = "", textEN = "", textFR = "", textES = "", campanha_img = "";

        try
        {
            string sql = string.Format(@"   DECLARE @textPT varchar(max);
                                            DECLARE @textEN varchar(max);
                                            DECLARE @textFR varchar(max);
                                            DECLARE @textES varchar(max);
                                            DECLARE @traducao varchar(max) = 'Se desejar receber mais informações acerca desta campanha, por favor CLIQUE AQUI!';
                                            DECLARE @pt varchar(10) = 'PT';
                                            DECLARE @en varchar(10) = 'EN';
                                            DECLARE @fr varchar(10) = 'FR';
                                            DECLARE @es varchar(10) = 'ES';

                                            EXEC DEVOLVE_TRADUCAO @traducao, @pt, @textPT output;
                                            EXEC DEVOLVE_TRADUCAO @traducao, @en, @textEN output;
                                            EXEC DEVOLVE_TRADUCAO @traducao, @fr, @textFR output;
                                            EXEC DEVOLVE_TRADUCAO @traducao, @es, @textES output;

                                            SELECT 
                                                @textPT as textPT,
                                                @textEN as textEN,
                                                @textFR as textFR,
                                                @textES as textES,
                                                concat('../', site, campanha, '/campanha.jpg') as campanha_path
                                            from REPORT_PATHS()");

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
                    textPT = oDs.Tables[count].Rows[i]["textPT"].ToString();
                    textEN = oDs.Tables[count].Rows[i]["textEN"].ToString();
                    textFR = oDs.Tables[count].Rows[i]["textFR"].ToString();
                    textES = oDs.Tables[count].Rows[i]["textES"].ToString();
                    campanha_img = oDs.Tables[count].Rows[i]["campanha_path"].ToString();
                }
            }
            else
            {
                textPT = "Se desejar receber mais informações acerca desta campanha, por favor CLIQUE AQUI!";
                textEN = "If you want to know more info about this campaign, please CLICK HERE!";
                textFR = "En savoir plus sur cette campagne, CLIQUEZ ICI!";
                textES = "¡Si desea recibir más información sobre esta campaña, haga CLIC AQUÍ!";
                campanha_img = "../happybody/img/campanha.jpg";
            }
        }
        catch (Exception exc)
        {
            textPT = "Se desejar receber mais informações acerca desta campanha, por favor CLIQUE AQUI!";
            textEN = "If you want to know more info about this campaign, please CLICK HERE!";
            textFR = "En savoir plus sur cette campagne, CLIQUEZ ICI!";
            textES = "¡Si desea recibir más información sobre esta campaña, haga CLIC AQUÍ!";
            campanha_img = "../happybody/img/campanha.jpg";
        }

        table.AppendFormat(@"   <div class='row' style='height:80% !important; max-height: 80% !important;'>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 contentCenter' id='textContent' runat='server' style='height:100%;'>
                                        <img src='{4}' style='width:auto; height: 95%; max-height: 95% !important; margin: auto !important;' />
                                    </div>
                                </div>
                                <div class='row text-center' style='height:20% !important; max-height: 20% !important; padding-top: 10px;'>
                                    <div class='form-group col-3 col-md-3 col-xl-3 col-sm-3 col-lg-3 text-center' style='vertical-align: middle !important;'>
                                        <button class='btn btn-primary btn-xl text-uppercase' style='max-width: 100% !important; margin: auto !important;' onclick='openRGPDPagePT();' runat='server' id='btnPT'>{0}</button>
                                    </div>
                                    <div class='form-group col-3 col-md-3 col-xl-3 col-sm-3 col-lg-3 text-center' style='vertical-align: middle !important;'>
                                        <button class='btn btn-primary btn-xl text-uppercase' style='max-width: 100% !important; margin: auto !important;' onclick='openRGPDPageEN();' runat='server' id='btnEN'>{1}</button>
                                    </div>
                                    <div class='form-group col-3 col-md-3 col-xl-3 col-sm-3 col-lg-3 text-center' style='vertical-align: middle !important;'>
                                        <button class='btn btn-primary btn-xl text-uppercase' style='max-width: 100% !important; margin: auto !important;' onclick='openRGPDPageFR();' runat='server' id='btnFR'>{2}</button>
                                    </div>
                                    <div class='form-group col-3 col-md-3 col-xl-3 col-sm-3 col-lg-3 text-center' style='vertical-align: middle !important;'>
                                        <button class='btn btn-primary btn-xl text-uppercase' style='max-width: 100% !important; margin: auto !important;' onclick='openRGPDPageES();' runat='server' id='btnES'>{3}</button>
                                    </div>
                                </div>", textPT, textEN, textFR, textES, campanha_img);

        content.InnerHtml = table.ToString();
    }
}
