using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
//using System.Web.Mail;
using System.Net.Mail;
using System.IO;

public partial class SendNewsletter : Page
{
    string separador = "";
    string id = "";

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        separador = HttpContext.Current.Request.Url.PathAndQuery;
        id = Request.QueryString["id"];
        lbloperatorid.InnerHtml = id;

        if (!IsPostBack)
        {
            ClientScriptManager oCsm = this.Page.ClientScript;
            if (!oCsm.IsStartupScriptRegistered(GetType(), "SendNewsletter"))
            {
                
            }
        }
    }

    [WebMethod]
    public static string load(string filtro)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @FILTRO varchar(MAX) = {0};

                                            select email, socio, ativo, tag, enviado, nr_socio
                                            from REPORT_EMAILS_NEWSLETTER(@filtro)
                                            order by socio desc, nr_socio asc, tag asc", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th style='text-align: center; width: 80%;'>Emails</th>
                                                    <th style='text-align: center; width: 10%;' onclick='selectAll();'><img id='checkboxAll' src='img/icons/off.jpg' style='width:auto; height:auto; max-width: 50px; cursor: pointer; display:block; margin: 0 auto;'/></th>
                                                    <th style='text-align: center; width: 10%;'></th>
                                                </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr onclick='selectEmail({1});'>
                                            <td style='display:none' id='email_{1}'>{0}</td>
                                            <td style='display:none' id='enviado_{1}'>{5}</td>
                                            <td style='width:80%'>
                                                {0}<br/>
                                                <span style='font-size: 12px; {2}'>{3}</span>
                                            </td>
                                            <td style='width:10%' onclick='select({1});'>
                                                <img id='sendEmailCheckbox_{1}' src='img/icons/off.jpg' style='width:auto; height:auto; max-width: 50px; cursor: pointer; display:block; margin: 0 auto;'/>
                                            </td>
                                            <td style='width:10%; {4}'></td>
                                        </tr>",
                                                myReader["EMAIL"].ToString(),
                                                conta.ToString(),
                                                myReader["ATIVO"].ToString() == "1" ? " " : " color: red; ",
                                                myReader["TAG"].ToString(),
                                                myReader["ENVIADO"].ToString() == "0" ? " " : " background-color: green; ",
                                                myReader["ENVIADO"].ToString());

                    conta++;
                }

                table.AppendFormat("</tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem emails a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem emails a apresentar.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string sendEmailFromTemplate(string sendto)
    {
        string mes = "";
        string servidor = "";
        string servidor_software = "";

        try
        {
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            SET LANGUAGE PORTUGUESE;

                                            SELECT UPPER(DATENAME(month, DATEADD(hh, -1, GETDATE()))) AS MES, SERVIDOR_SITE, SERVIDOR_SOFTWARE FROM APPLICATION_CONFIG");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    mes = myReader["MES"].ToString();
                    servidor = myReader["SERVIDOR_SITE"].ToString();
                    servidor_software = myReader["SERVIDOR_SOFTWARE"].ToString();
                }
                connection.Close();
            }
            else
            {
                connection.Close();
            }
        }
        catch (Exception exc)
        {

        }

        try
        {
            MailMessage mailMessage = new MailMessage();

            string newsletterText = string.Empty;
            newsletterText = File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "\\" + servidor_software + "//templates//newsletter.html");

            // ------------------------------------
            // Processa o template 
            // ------------------------------------
            newsletterText = newsletterText.Replace("[EMAIL_INTRO]", "NEWSLETTER " + mes);
            newsletterText = newsletterText.Replace("[EMAIL_TEXTBODY]", "<img src='https://happybody.site/" + servidor + "/Newsletter/newsletter_001.jpg' style='width: 100%; height: auto; margin: auto; border: 0;' /><img src='https://happybody.site/" + servidor + "/Newsletter/newsletter_002.jpg' style='width: 100%; height: auto; margin: auto; border: 0;' />");
            newsletterText = newsletterText.Replace("[CANCELAR_SUBSCRICAO]", "https://happybody.site/" + servidor + "/CancelarSubscricaoNewsletter.aspx?email=" + sendto);
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

            mailMessage.Bcc.Add("happybodyfitcoach@gmail.com");

            mailMessage.Subject = "Newsletter " + mes;
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
            return ex.ToString();
        }

        try
        {
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @email varchar(max) = '{0}';

                                            UPDATE WS_NEWSLETTER_EMAIL SET ENVIADO = 1 WHERE EMAIL = @email
                                            
                                            UPDATE list 
                                            SET ENVIADO = 1
                                            FROM LISTAGEM_EMAILS_ENVIADOS list
                                            INNER JOIN SOCIOS soc on soc.SOCIOSID = list.ID_SOCIO
                                            WHERE soc.EMAIL = @email
                                            and list.TIPO = 'NEWSLETTER'
                                            and list.MES = MONTH(DATEADD(hh, -1, GETDATE()))", sendto);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    mes = myReader["MES"].ToString();
                    servidor = myReader["SERVIDOR_SITE"].ToString();
                    servidor_software = myReader["SERVIDOR_SOFTWARE"].ToString();
                }
                connection.Close();
            }
            else
            {
                connection.Close();
            }
        }
        catch (Exception exc)
        {

        }

        return "Newsletter Enviada com sucesso!";
    }
}
