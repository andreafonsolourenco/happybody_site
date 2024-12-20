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

public partial class Ausencias : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "Ausencias"))
            {
                
            }
        }
    }

    [WebMethod]
    public static string load(string filtro, string query, string dia_min, string dia_max)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = "";

            if (filtro == "")
            {
                if (dia_max == "999999")
                {
                    sql = string.Format(@"  SET DATEFORMAT DMY;
                                            DECLARE @nr_dias_min int = {0};
                                            DECLARE @nr_dias_max int = {1};
                                            DECLARE @id_socio int;
                                            DECLARE @dia_superior datetime;
                                            DECLARE @dia_inferior datetime;

                                            SELECT 
                                                NR_SOCIO,
	                                            NOME,
	                                            DIAS_AUSENCIA,
	                                            ULTIMA_DATA_ENTRADA,
                                                TELEMOVEL,
                                                EMAIL,
                                                NAO_QUER_RECEBER_PUBLICIDADE,
                                                ENVIADO
                                            FROM REPORT_AUSENCIAS(@id_socio, @dia_inferior, @dia_superior)
                                            WHERE DIAS_AUSENCIA >= @nr_dias_min and DIAS_AUSENCIA <= @nr_dias_max
                                            ORDER BY DIAS_AUSENCIA asc, NR_SOCIO asc", dia_min, dia_max);
                }
                else
                {
                    sql = string.Format(@"  SET DATEFORMAT DMY;
                                            DECLARE @nr_dias_min int = {0};
                                            DECLARE @nr_dias_max int = {1};
                                            DECLARE @id_socio int;
                                            DECLARE @dia_superior datetime = DATEADD(hh, -1, GETDATE());
                                            DECLARE @dia_inferior datetime = CAST(DATEADD(dd, (@nr_dias_max * (-1)), @dia_superior) as DATE);

                                            SELECT 
                                                NR_SOCIO,
	                                            NOME,
	                                            DIAS_AUSENCIA,
	                                            ULTIMA_DATA_ENTRADA,
                                                TELEMOVEL,
                                                EMAIL,
                                                NAO_QUER_RECEBER_PUBLICIDADE,
                                                ENVIADO
                                            FROM REPORT_AUSENCIAS(@id_socio, @dia_inferior, @dia_superior)
                                            WHERE DIAS_AUSENCIA >= @nr_dias_min and DIAS_AUSENCIA <= @nr_dias_max
                                            ORDER BY DIAS_AUSENCIA asc, NR_SOCIO asc", dia_min, dia_max);
                }
            }
            else
            {
                sql = string.Format(@"  SET DATEFORMAT DMY;
                                        DECLARE @id_socio int;
                                        DECLARE @dia_superior datetime;
                                        DECLARE @dia_inferior datetime;
                                        DECLARE @filtro varchar(max) = '{0}';

                                        SELECT 
                                            NR_SOCIO,
	                                        NOME,
	                                        DIAS_AUSENCIA,
	                                        ULTIMA_DATA_ENTRADA,
                                            TELEMOVEL,
                                            EMAIL,
                                            NAO_QUER_RECEBER_PUBLICIDADE,
                                            ENVIADO
                                        FROM REPORT_AUSENCIAS(@id_socio, @dia_inferior, @dia_superior)
                                        where (@filtro IS NULL OR ({1}))
                                        ORDER BY DIAS_AUSENCIA asc, NR_SOCIO asc", filtro, query);
            }

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerLeft'>Sócio</th>
                                                    <th class='headerRight'>Nº Dias</th>
                                                    <th class='headerRight'></th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick='openEmailTemplate({0}, {6});'>
                                            <td style='width: 70%'>
                                                {0} - {1}<br />
                                                <span style='font-size:small'>Telemóvel: {4}<br />Email: {5}</span>
                                            </td>
                                            <td style='width: 20%'>{2}{3}</td>
                                            <td style='{7}'></td>
                                        </tr>",
                                                myReader["NR_SOCIO"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["ULTIMA_DATA_ENTRADA"].ToString() == "" ? "Sem Entradas" : (myReader["DIAS_AUSENCIA"].ToString() + " dias"),
                                                myReader["ULTIMA_DATA_ENTRADA"].ToString() == "" ? "" : ("<br /><span style='font-size: small'>" + myReader["ULTIMA_DATA_ENTRADA"].ToString() + "</span>"),
                                                myReader["TELEMOVEL"].ToString(),
                                                myReader["EMAIL"].ToString(),
                                                myReader["NAO_QUER_RECEBER_PUBLICIDADE"].ToString(),
                                                myReader["ENVIADO"].ToString() == "1" ? "width:10%;background-color: green;" : "width:10%;");
                }

                table.AppendFormat("</tbody></table></div>");

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:medium;margin: auto;color:#000'>Não existem ausências a apresentar no período de tempo selecionado.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:medium;margin: auto;color:#000'>Não existem ausências a apresentar no período de tempo selecionado.<br />{0}</div>", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string loadTextEmail(string nr_socio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @nr_socio int = {0};

                                            SELECT 
                                                NOME,
                                                EMAIL,
                                                SEXO
                                            FROM SOCIOS
                                            WHERE NR_SOCIO = @nr_socio", nr_socio);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            string emailIntro = "";
            string emailText = "";

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    string[] name = myReader["NOME"].ToString().Split(' ');

                    if (myReader["SEXO"].ToString() == "M")
                        emailIntro = "Estimado " + name[0];
                    else
                        emailIntro = "Estimada " + name[0];

                    emailText = "Notámos a sua ausência no Ginásio!\nSe fizermos um treino de 30 min, continua a ser um bom treino!🏃\nFale connosco! Contamos consigo na 2ª feira!\nJUNTOS CONSEGUIMOS! 💪";
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <span class='variaveis' id='emailIntro'>{0}</span>
                                            <span class='variaveis' id='emailText'>{1}</span>
                                            <span class='variaveis' id='emailSocio'>{2}</span>
                                            <span class='variaveis' id='emailSubject'>NOTÁMOS A SUA AUSÊNCIA</span>",
                                                emailIntro, emailText,
                                                myReader["EMAIL"].ToString());
                }

                table.AppendFormat("</tbody></table></div>");

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:medium;margin: auto;color:#000'>Não existem dados a apresentar para o nº de sócio indicado!</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:medium;margin: auto;color:#000'>Não existem dados a apresentar para o nº de sócio indicado! {0}</div>", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string sendEmailFromTemplate(string assunto, string intro, string sendto, string sendcc, string sendbcc, string body, string nr_socio)
    {
        if (string.IsNullOrEmpty(assunto) || string.IsNullOrEmpty(intro) || string.IsNullOrEmpty(body))
        {
            return "Erro: o email não pode ser enviado com texto vazio!";
        }

        try
        {
            MailMessage mailMessage = new MailMessage();

            string newsletterText = string.Empty;
            newsletterText = File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "//happybodysoftware//templates//aniversario.html");

            body = body.Replace("\n", "<br />");

            // ------------------------------------
            // Processa o template 
            // ------------------------------------
            newsletterText = newsletterText.Replace("[EMAIL_INTRO]", intro);
            newsletterText = newsletterText.Replace("[EMAIL_TEXTBODY]", body.Replace("\n", "<br />"));
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
            return ex.ToString();
        }

        try
        {
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(cs);

            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;

                                            DECLARE @nr_socio int = {0};
                                            DECLARE @tipo varchar(500) = '{1}';

                                            UPDATE list
                                            SET enviado = 1
                                            FROM SOCIOS soc
                                            INNER JOIN LISTAGEM_EMAILS_ENVIADOS list on list.id_socio = soc.sociosid
                                            WHERE soc.NR_SOCIO = @nr_socio
                                            and list.tipo = @tipo", nr_socio, "AUSENCIAS");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            connection.Close();
        }
        catch (Exception exc)
        {

        }

        return "Email de ausência enviado com sucesso!";
    }
}
