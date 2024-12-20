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

public partial class AvaliacoesFisicasEmail : Page
{
    string mes = "";

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClientScriptManager oCsm = this.Page.ClientScript;
            if (!oCsm.IsStartupScriptRegistered(GetType(), "MainMenu"))
            {
                
            }
        }

        getMes();
    }

    private void getMes()
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        try
        {
            string sql = string.Format(@"   set language portuguese
                                            declare @mes varchar(100) = (select DATENAME(month, dateadd(hh, -1, getdate())))

                                            select UPPER(LEFT(@mes,1))+LOWER(SUBSTRING(@mes,2,LEN(@mes))) as mes");

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
                    mes = oDs.Tables[0].Rows[i]["mes"].ToString();
                }

                string body = "";

                body = "<h4 style='font-weight:bold'>OLÁ!</h4><br/><br />Está na hora de lembrar e renovar <b>OBJETIVOS!</b><br />";
                body += "A sua Avaliação Física está marcada para " + mes + ". Solicitamos confirmação da data e hora disponiveis.<br /><br />";
                body += "<h4 style='font-weight: bold'>Avaliação Física GRATUITA</h4>";
                body += "<ul><li>Composição Corporal;</li>";
                body += "<li>Peso e Altura;</li>";
                body += "<li>Perímetros de cintura e anca;</li>";
                body += "<li>Anamenese médica e patologias;</li>";
                body += "<li>Anamenese desportiva</li></ul>";
                body += "<table border='0'><tbody>";
                body += "<tr><td style='width:50%; text-align: center;'>";
                body += "<img src='https://happybody.site//happybodysoftware//afs//email_img_1.JPG' style='max-width: 100%; height:auto; border-radius: 40%;' /></td>";
                body += "<td style='width:50%; text-align: center;'><img src='https://happybody.site//happybodysoftware//afs//email_img_2.JPG' style='max-width: 100%; height:auto; border-radius: 40%;' /></td></tr></tbody></table>";
                body += "<br /><br /><br />";
                body += "<h4 style='font-weight: bold'>Serviço Especial em Personal Trainer</h4>";
                body += "<ul><li>Programa BURNING FAT (3 meses);</li>";
                body += "<li>Treino para a 3ª Idade;</li>";
                body += "<li>Avaliação Postural;</li>";
                body += "<li>Avaliação do movimento;</li>";
                body += "<li>Treino Postural;</li>";
                body += "<li>Encurtamentos musculares</li></ul>";
                body += "<table border='0'><tbody>";
                body += "<tr><td style='width:50%; text-align: center;'>";
                body += "<img src='https://happybody.site//happybodysoftware//afs//email_img_3.JPG' style='max-width: 100%; height:auto; border-radius: 40%;' /></td>";
                body += "<td style='width:50%; text-align: center;'><img src='https://happybody.site//happybodysoftware//afs//email_img_4.JPG' style='max-width: 100%; height:auto; border-radius: 40%;' /></td></tr></tbody></table>";
                body += "<br /><br /><br />";
                body += "Saudações Desportivas";

                sendEmailFromTemplate("Avaliação Física " + mes, "Avaliação Física " + mes + " GRATUITA - HAPPY BODY", "happybodyfitcoach@gmail.com", "", "afonsopereira6@gmail.com", body);
            }
            else
            {
                
            }
        }
        catch (Exception exc)
        {
            
        }
    }

    private void sendEmailFromTemplate(string assunto, string intro, string sendto, string sendcc, string sendbcc, string body)
    {
        try
        {
            MailMessage mailMessage = new MailMessage();

            string newsletterText = string.Empty;
            newsletterText = File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "\\happybodysoftware//afs//template.html");

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
        }
        catch (Exception ex)
        {
            
        }
    }
}
