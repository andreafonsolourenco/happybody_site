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

public partial class AgenteDiaUm : Page
{
    string separador = "";
    string operador = "";

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        separador = HttpContext.Current.Request.Url.PathAndQuery;
        operador = Request.QueryString["operador"];
        lbloperatorcode.InnerHtml = operador;

        if (!IsPostBack)
        {
            ClientScriptManager oCsm = this.Page.ClientScript;
            if (!oCsm.IsStartupScriptRegistered(GetType(), "MainMenu"))
            {
                
            }
        }

        agente();
    }

    private void agente()
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
                                            DECLARE @operador varchar(100) = '{0}';
                                            DECLARE @ret int;

                                            EXEC AGENTE_DIA_UM @operador, @ret OUTPUT", operador);

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            // Adiciona as linhas
            table.AppendFormat(@"   <table style='width:100%; height: auto; font-family: 'Noto Sans', sans-serif !important;'>
                                        <thead style='background-color:#000; color: #FFF; font-size: large; font-weight: bold;'>
						                    <tr style='height: 50px;'>
                                                <th style='padding: 5px; width: 80%; border-right: 1px red solid;'>Dados</th>
                                                <th style='padding: 5px; width: 20%; border-left: 1px red solid;'>Agente</th>
						                    </tr>
						                </thead>
                                        <tbody style='background-color:#FFF; color:#000; font-size: medium; border: 2px solid black; border-spacing: 1px; line-height: 1.2;'>");

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr style='border: 2px solid black; border-spacing: 1px;'>
                                            <td style='border: 2px solid black; border-spacing: 1px; padding: 5px;'>
                                                Sócio Nº {0}<br /><br />
                                                Tipo de Contrato: {1}<br /><br />
                                                Estado de Contrato: {2}<br /><br />
                                                Data de Início: {3}<br /><br />
                                                Data de Fim: {4}<br /><br />
                                                Valor: {5}<br />
                                            </td>
                                            <td style='border: 2px solid black; border-spacing: 1px; padding: 5px;'>{6}</td>
                                        </tr>",
                                                oDs.Tables[0].Rows[i]["nr_socio"].ToString(),
                                                oDs.Tables[0].Rows[i]["tipo"].ToString(),
                                                oDs.Tables[0].Rows[i]["estado"].ToString(),
                                                oDs.Tables[0].Rows[i]["data_inicio"].ToString(),
                                                oDs.Tables[0].Rows[i]["data_fim"].ToString(),
                                                oDs.Tables[0].Rows[i]["preco"].ToString(),
                                                oDs.Tables[0].Rows[i]["agente"].ToString());
                }

                sendEmailFromTemplate("Resultados Agente", "Resultados Agente", "happybodyfitcoach@gmail.com", "", "afonsopereira6@gmail.com", table.ToString());
                //sendEmailFromTemplate("Resultados Agente", "Resultados Agente", "afonsopereira6@gmail.com", "", "afonsopereira6@gmail.com", table.ToString());
            }
            else
            {
                sendEmailFromTemplate("Resultados Agente", "Resultados Agente", "happybodyfitcoach@gmail.com", "", "afonsopereira6@gmail.com", "O Agente não correu automaticamente pelo Sistema devido a um erro!");
                //sendEmailFromTemplate("Resultados Agente", "Resultados Agente", "afonsopereira6@gmail.com", "", "afonsopereira6@gmail.com", "O Agente não correu automaticamente pelo Sistema devido a um erro!");
            }
        }
        catch (Exception exc)
        {
            sendEmailFromTemplate("Resultados Agente", "Resultados Agente", "happybodyfitcoach@gmail.com", "", "afonsopereira6@gmail.com", "O Agente não correu automaticamente pelo Sistema devido a um erro: " + exc.ToString());
            //sendEmailFromTemplate("Resultados Agente", "Resultados Agente", "afonsopereira6@gmail.com", "", "afonsopereira6@gmail.com", "O Agente não correu automaticamente pelo Sistema devido a um erro: " + exc.ToString());
        }
    }

    private void sendEmailFromTemplate(string assunto, string intro, string sendto, string sendcc, string sendbcc, string body)
    {
        try
        {
            MailMessage mailMessage = new MailMessage();

            string newsletterText = string.Empty;
            newsletterText = File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "\\happybodysoftware//templates//aniversario.html");

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
            string _from = "happybodyfitcoach@gmail.com";//getConfigurationField("email_user");
            string _emailpwd = "happysr@1";// getConfigurationField("email_password");
            string _smtp = "smtp.gmail.com";// getConfigurationField("email_smtp");
            string _smtpport = "465";// getConfigurationField("email_smtpport");  // 587

            mailMessage.From = new MailAddress(_from, assunto);

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

            divTable.InnerHtml = "Agente correu com sucesso e foi enviado email com os respetivos resultados!";
        }
        catch (Exception ex)
        {
            divTable.InnerHtml = ex.ToString();
        }
    }
}
