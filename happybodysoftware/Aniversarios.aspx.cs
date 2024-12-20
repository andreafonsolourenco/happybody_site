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

public partial class Aniversarios : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "MainMenu"))
            {
                
            }
        }
    }

    [WebMethod]
    public static string sendEmailFromTemplate(string assunto, string intro, string sendto, string sendcc, string sendbcc, string body, string nr_socio, string tipo)
    {
        if(string.IsNullOrEmpty(assunto) || string.IsNullOrEmpty(intro) || string.IsNullOrEmpty(body))
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
                                            DECLARE @dataatual datetime = dateadd(hh, -1, getdate());

                                            UPDATE list
                                            SET enviado = 1
                                            FROM SOCIOS soc
                                            INNER JOIN LISTAGEM_EMAILS_ENVIADOS list on list.id_socio = soc.sociosid
                                            WHERE soc.NR_SOCIO = @nr_socio
                                            and list.tipo = @tipo
                                            and list.mes = MONTH(@dataatual)
                                            and list.ano = YEAR(@dataatual)", nr_socio, tipo);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            connection.Close();
        }
        catch (Exception exc)
        {

        }

        return "Email de aniversário enviado com sucesso!";
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

                    if(myReader["SEXO"].ToString() == "M")
                        emailIntro = "Estimado " + name[0];
                    else
                        emailIntro = "Estimada " + name[0];

                    emailText = "Feliz Aniversário! 🎂\nJUNTOS temos sonhos, esperanças, planos... e JUNTOS atingimos o teu objetivo. Um dia muito especial! Que este próximo ano seja de realizações!\nJUNTOS CONSEGUIMOS! 💪";
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <span class='variaveis' id='emailIntro'>{0}</span>
                                            <span class='variaveis' id='emailText'>{1}</span>
                                            <span class='variaveis' id='emailSocio'>{2}</span>
                                            <span class='variaveis' id='emailSubject'>FELIZ ANIVERSÁRIO</span>",
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
    public static string loadTextEmailMes(string nr_socio)
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

                    emailText = "Não há presente de aniversário 🎁 que possa expressar o quanto és importante para nós!\nPor seres especial oferecemos-te:\n- 50% de desconto em PT +\n- 5€ de desconto em Nutrição +\n- 10% de desconto em Estética.\n\nOFERTA válida até ao final do mês corrente.\n\nNão te esqueças de marcar tudo ainda este mês na nossa receção!\n\n(Estética apenas para mulheres)\n\nJUNTOS CONSEGUIMOS💪\n\nGinásio Happy Body";
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <span class='variaveis' id='emailIntro'>{0}</span>
                                            <span class='variaveis' id='emailText'>{1}</span>
                                            <span class='variaveis' id='emailSocio'>{2}</span>
                                            <span class='variaveis' id='emailSubject'>FELIZ ANIVERSÁRIO 🎂</span>",
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
    public static string getAniversarioMensal(string operatorID)
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
                                        
                                            SELECT 
                                                NR_SOCIO,
                                                NOME,
                                                TELEMOVEL,
                                                EMAIL,
                                                ATIVO,
                                                DIA,
                                                DIA_DN,
                                                ENVIADO
                                            FROM REPORT_ANIVERSARIOS_MES(null)
                                            WHERE ATIVO = 1
                                            ORDER BY ATIVO DESC, DIA_DN ASC");

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGridMes'>
                                            <thead>
						                        <tr>
                                                    <th class='headerColspan' colspan='3'>MENSAL</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick='openEmailTemplateMes({0}, {8});' style='cursor:pointer;'>
                                            <td style='width:70%'>{0} - {1}<br />{6}<br /><span style='font-size: small; color: {5};'>{4}</span></td>
                                            <td style='width:20%'>{2}<br />{3}</td>
                                            <td style='width:70%; {7}'></td>
                                        </tr>",
                                                oDs.Tables[0].Rows[i]["NR_SOCIO"].ToString(),
                                                oDs.Tables[0].Rows[i]["NOME"].ToString(),
                                                oDs.Tables[0].Rows[i]["TELEMOVEL"].ToString(),
                                                oDs.Tables[0].Rows[i]["EMAIL"].ToString(),
                                                oDs.Tables[0].Rows[i]["ATIVO"].ToString() == "1" ? "ATIVO" : "INATIVO",
                                                oDs.Tables[0].Rows[i]["ATIVO"].ToString() == "1" ? "#000" : "red",
                                                oDs.Tables[0].Rows[i]["DIA"].ToString(),
                                                oDs.Tables[0].Rows[i]["ENVIADO"].ToString() == "1" ? "background-color: green" : "",
                                                oDs.Tables[0].Rows[i]["ENVIADO"].ToString());
                }

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='tableGridMes'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem aniversários para o dia de hoje.</div>");
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='tableGridMes'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Erro ao carregar aniversários: {0}</div>", exc.ToString());
        }

        return table.ToString();
    }

    [WebMethod]
    public static string getAniversarioDiario(string operatorID)
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
                                        
                                            SELECT 
                                                NR_SOCIO,
                                                NOME,
                                                TELEMOVEL,
                                                EMAIL,
                                                ATIVO,
                                                ENVIADO
                                            FROM REPORT_ANIVERSARIOS()
					                        ORDER BY ATIVO DESC");

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerColspan' colspan='3'>DIÁRIO</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick='openEmailTemplate({0}, {7});' style='cursor:pointer;'>
                                            <td style='width:70%;'>{0} - {1}<br /><span style='font-size: small; color: {5};'>{4}</span></td>
                                            <td style='width:20%;'>{2}<br />{3}</td>
                                            <td style='wdith:10%; {6}'></td>
                                        </tr>",
                                                oDs.Tables[0].Rows[i]["NR_SOCIO"].ToString(),
                                                oDs.Tables[0].Rows[i]["NOME"].ToString(),
                                                oDs.Tables[0].Rows[i]["TELEMOVEL"].ToString(),
                                                oDs.Tables[0].Rows[i]["EMAIL"].ToString(),
                                                oDs.Tables[0].Rows[i]["ATIVO"].ToString() == "1" ? "ATIVO" : "INATIVO",
                                                oDs.Tables[0].Rows[i]["ATIVO"].ToString() == "1" ? "#000" : "red",
                                                oDs.Tables[0].Rows[i]["ENVIADO"].ToString() == "1" ? "background-color: green" : "",
                                                oDs.Tables[0].Rows[i]["ENVIADO"].ToString());
                }

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='tableGrid'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem aniversários para o dia de hoje.</div>");
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='tableGrid'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Erro ao carregar aniversários: {0}</div>", exc.ToString());
        }

        return table.ToString();
    }
}
