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
using System.ServiceModel.Activities;
using System.Web.UI.WebControls;

public partial class consent : Page
{
    string separador = "";
    string page = "";
    string lingua = "";

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        separador = HttpContext.Current.Request.Url.PathAndQuery;
        
        try
        {
            page = Request.QueryString["pagina"];
        }
        catch (Exception exc)
        {
            page = "";
        }

        try
        {
            lingua = Request.QueryString["language"];
        }
        catch (Exception exc)
        {
            lingua = "";
        }

        if (!IsPostBack)
        {
            ClientScriptManager oCsm = this.Page.ClientScript;
            if (!oCsm.IsStartupScriptRegistered(GetType(), "consentimento"))
            {
                
            }

            pagelink.InnerHtml = page;
            language.InnerHtml = lingua;
            getPage();
        }
    }

    [WebMethod]
    public static string sendEmailFromTemplate(string nome, string email, string telefone, string autorizo, string treinooferecidopor, string link)
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

            string intro = "";
            string body = "";

            if (string.IsNullOrEmpty(link))
            {
                body += "";
                intro += "CONSENTIMENTO DO TITULAR DE DADOS INFORMADO";
            }
            else
            {
                if (link == "gymsummerpack")
                {
                    body = "<img src='https://happybody.site/campanhas/img/gymsummer.jpg' style='width:100%; height:auto; margin-bottom: 10px;' />";
                    intro += "HAPPYBODY GYM SUMMER PACK";
                }
            }
            
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
                 + "Treino Experimental: " + treinooferecidopor + "<br />"
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
                                            DECLARE @treinoexperimental datetime = '{4}';
                                            DECLARE @link varchar(max) = '{5}';
                                            DECLARE @ip varchar(250) = '{6}';

                                            INSERT INTO RGPD(NOME, EMAIL, TELEFONE, AUTORIZO, TREINOEXPERIMENTAL, LINK, IP)
                                            VALUES(@nome, @email, @telefone, @autorizacao, @treinoexperimental, @link, @ip)", nome,
                                                                                                                            email,
                                                                                                                            telefone,
                                                                                                                            autorizo,
                                                                                                                            treinooferecidopor,
                                                                                                                            string.IsNullOrEmpty(link) ? HttpContext.Current.Request.Url.AbsoluteUri : link,
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
        string titulo = "", enviar = "", rgpd = "", nome = "", marcartreino = "", data = "", hora = "", email = "", tlm = "", consentimento = "";
        string limpar = "", cancelar = "", confirmar = "", field = "";

        try
        {
            string sql = string.Format(@"   DECLARE @titulo varchar(max);
                                            DECLARE @enviar varchar(max);
                                            DECLARE @rgpd varchar(max);
                                            DECLARE @nome varchar(max);
                                            DECLARE @marcartreino varchar(max);
                                            DECLARE @data varchar(max);
                                            DECLARE @hora varchar(max);
                                            DECLARE @email varchar(max);
                                            DECLARE @tlm varchar(max);
                                            DECLARE @informacaoconsentimento varchar(max);
                                            DECLARE @limpar varchar(max);
                                            DECLARE @cancelar varchar(max);
                                            DECLARE @confirmar varchar(max);
                                            DECLARE @field varchar(max);
                                            DECLARE @traducao varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';

                                            set @traducao = 'CONSENTIMENTO DO TITULAR DE DADOS INFORMADO';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @titulo output;

                                            set @traducao = 'ENVIAR';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @enviar output;

                                            set @traducao = 'Nos termos do RGPD, consinto que os meus dados pessoais (nome, email e telefone ou telemóvel) sejam utilizados para:<br />- Felicitações de aniversário / estatística;<br />- Divulgações de todas as informações, campanhas, horários e aulas;<br />- Marketing ou publicidade (novidades, preços do ginásio);<br />- Ofertas;<br />- Descontos;<br />- Newsletters, artigos e vídeos.'
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @rgpd output;

                                            set @traducao = 'Nome'
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @nome output;

                                            set @traducao = 'Marcar Treino'
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @marcartreino output;

                                            set @traducao = 'Data'
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @data output;

                                            set @traducao = 'Hora'
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @hora output;

                                            set @traducao = 'Email'
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @email output;

                                            set @traducao = 'Telemóvel'
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @tlm output;

                                            set @traducao = 'Fui informado dos meus direitos de acesso, retificação, oposição e esquecimento. A conservação dos dados será efetuada desde a presente data até indicação do contrário via email para happybodyfitcoach@gmail.com.';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @informacaoconsentimento output;
                                            
                                            SET @traducao = 'Limpar';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @limpar output;

                                            SET @traducao = 'Cancelar';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @cancelar output;

                                            SET @traducao = 'Confirmar';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @confirmar output;

                                            SET @traducao = 'Por favor, insira um(a) [FIELD] válido(a)!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @field output;

                                            SELECT 
                                                @enviar as enviar,
                                                @titulo as titulo,
                                                @rgpd as rgpd,
                                                @nome as nome,
                                                @marcartreino as marcartreino,
                                                @data as data,
                                                @hora as hora,
                                                @email as email,
                                                @tlm as tlm,
                                                @informacaoconsentimento as informacaoconsentimento,
                                                @limpar as limpar, 
                                                @cancelar as cancelar,
                                                @confirmar as confirmar,
                                                @field as field", lingua);

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
                    titulo = oDs.Tables[count].Rows[i]["titulo"].ToString();
                    enviar = oDs.Tables[count].Rows[i]["enviar"].ToString();
                    rgpd = oDs.Tables[count].Rows[i]["rgpd"].ToString();
                    nome = oDs.Tables[count].Rows[i]["nome"].ToString();
                    marcartreino = oDs.Tables[count].Rows[i]["marcartreino"].ToString();
                    data = oDs.Tables[count].Rows[i]["data"].ToString();
                    hora = oDs.Tables[count].Rows[i]["hora"].ToString();
                    email = oDs.Tables[count].Rows[i]["email"].ToString();
                    tlm = oDs.Tables[count].Rows[i]["tlm"].ToString();
                    consentimento = oDs.Tables[count].Rows[i]["informacaoconsentimento"].ToString();
                    limpar = oDs.Tables[count].Rows[i]["limpar"].ToString();
                    cancelar = oDs.Tables[count].Rows[i]["cancelar"].ToString();
                    confirmar = oDs.Tables[count].Rows[i]["confirmar"].ToString();
                    field = oDs.Tables[count].Rows[i]["field"].ToString();
                }
            }
        }
        catch (Exception exc)
        {
            
        }

        footerDiv.InnerHtml = String.Format(@"<button class='btn btn-primary btn-xl text-uppercase' style='width: 100% !important; max-width: 100% !important' onclick='sendRGPDEmail();' runat='server'>{0}</button>", enviar);

        table.AppendFormat(@"   <div class='row'>
                                    <div class='col-lg-12 col-md-12 col-xs-12 col-sm-12 col-12' style='font-weight: bold; text-align: center'>
                                        <h1 id='title'>{0}</h1>
                                    </div>
                                </div>
                                <div class='row'>
                                    <div class='col-lg-12 col-md-12 col-xs-12 col-sm-12 col-12' style='font-weight: bold; text-align: justify;'>
                                        <h5>{1}<br /><br /></h5>
                                    </div>
                                </div>
                                <div class='row' style='font-weight: bold; margin-bottom: 15px;'>
                                    <div class='col-lg-3 col-md-3 col-xs-3 col-sm-3'>
                                        <div class='form-group' style='text-align: right !important;'>
                                            <input type='checkbox' id='autorizacao' class='form-control form-control-alternative'>
                                        </div>
                                    </div>
                                    <div class='col-lg-9 col-md-9 col-xs-9 col-sm-9' style='text-align: left !important;'>
                                        <div class='form-group' style='text-align: left !Important;'>
                                            <label class='form-control-label' for='autorizacao'>{8}</label>
                                        </div>
                                    </div>
                                </div>
                                <div class='row' style='font-weight: bold; margin-bottom: 15px;'>
                                    <div class='col-lg-12 col-md-12 col-xs-12 col-sm-12 col-12'>
                                        <div class='form-group'>
                                            <label class='form-control-label' for='name' id='namelabel'>{2}</label>
                                            <input type='text' id='name' class='form-control form-control-alternative' placeholder='{2}'>
                                        </div>
                                    </div>
                                </div>
                                <div class='row' style='font-weight: bold; margin-bottom: 15px;'>
                                    <div class='col-lg-6 col-md-6 col-xs-6 col-sm-6'>
                                        <div class='form-group'>
                                            <label class='form-control-label' for='email' id='emaillabel'>{6}</label>
                                            <input type='email' id='email' class='form-control form-control-alternative' placeholder='{6}'>
                                        </div>
                                    </div>
                                    <div class='col-lg-6 col-md-6 col-xs-6 col-sm-6'>
                                        <div class='form-group'>
                                            <label class='form-control-label' for='tlf' id='tlflabel'>{7}</label>
                                            <input type='tel' id='tlf' class='form-control form-control-alternative' placeholder='{7}'>
                                        </div>
                                    </div>
                                </div>
                                <div class='row' style='font-weight: bold; margin-bottom: 20px;'>
                                    <div class='col-lg-12 col-md-12 col-xs-12 col-sm-12 col-12'>
                                        <div class='form-group'>
                                            <label class='form-control-label'>{3}</label>
                                        </div>
                                    </div>
                                    <div class='col-lg-6 col-md-6 col-xs-6 col-sm-6'>
                                        <div class='form-group'>
                                            <label class='form-control-label' for='data' id='datalabel'>{4}</label>
                                            <input type='text' id='data' class='form-control form-control-alternative' placeholder='{4}'>
                                        </div>
                                    </div>
                                    <div class='col-lg-6 col-md-6 col-xs-6 col-sm-6'>
                                        <div class='form-group'>
                                            <label class='form-control-label' for='hora' id='horalabel'>{5}</label>
                                            <input type='text' id='hora' class='form-control form-control-alternative' placeholder='{5}'>
                                        </div>
                                    </div>
                                </div>", titulo, rgpd, nome, marcartreino, data, hora, email, tlm, consentimento);

        textContent.InnerHtml = table.ToString();
        clean.InnerHtml = limpar;
        cancel.InnerHtml = cancelar;
        confirm.InnerHtml = confirmar;
        validfield.InnerHtml = field;
    }

    [WebMethod]
    public static string saveRGPDConsent(string nome, string email, string telefone, string autorizo, string treinooferecidopor, string link)
    {
        try
        {
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
                                                                                                                            treinooferecidopor,
                                                                                                                            string.IsNullOrEmpty(link) ? HttpContext.Current.Request.Url.AbsoluteUri : link,
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
}
