using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class ContactosWS : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "ContactosWS"))
            {
                
            }
        }

        getContacts();
    }

    private void getContacts()
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            
                                            SELECT 
                                                MORADA,
                                                FACEBOOK,
                                                INSTAGRAM,
                                                EMAIL1,
                                                EMAIL2,
                                                EMAIL3,
                                                TLF1,
                                                TLF2,
                                                TLF3,
                                                WHATSAPP,
                                                EMAIL4,
                                                YOUTUBE,
                                                linkedin
                                            FROM WS_CONTACTOS");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>
                                                <input id='btnEdit' value='Guardar' runat='server' type='button' onclick='editInfo();' style='background-color: #4282b5; 
                                                    width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: 'Noto Sans', sans-serif !important; height:100%'>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Morada:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Facebook:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Instagram:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>WhatsApp:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>YouTube:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Linkedin:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Email 1:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Email 2:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Email 3:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Email 4:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Telefone 1:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Telefone 2:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Telefone 3:</div>
                                                </div>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='morada' name='morada' placeholder='Morada' style='width: 100%; margin: auto; height: 100%;' value='{0}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='facebook' name='facebook' placeholder='Facebook' style='width: 100%; margin: auto; height: 100%;' value='{1}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='instagram' name='instagram' placeholder='Instagram' style='width: 100%; margin: auto; height: 100%;' value='{2}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='whatsapp' name='whatsapp' placeholder='WhatsApp' style='width: 100%; margin: auto; height: 100%;' value='{10}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='youtube' name='youtube' placeholder='YouTube' style='width: 100%; margin: auto; height: 100%;' value='{11}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='linkedin' name='linkedin' placeholder='Linkedin' style='width: 100%; margin: auto; height: 100%;' value='{12}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='email1' name='email1' placeholder='Email' style='width: 100%; margin: auto; height: 100%;' value='{3}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='email2' name='email2' placeholder='Email' style='width: 100%; margin: auto; height: 100%;' value='{4}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='email3' name='email3' placeholder='Email' style='width: 100%; margin: auto; height: 100%;' value='{5}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='email4' name='email4' placeholder='Email' style='width: 100%; margin: auto; height: 100%;' value='{9}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='tlf1' name='tlf1' placeholder='Telefone' style='width: 100%; margin: auto; height: 100%;' value='{6}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='tlf2' name='tlf2' placeholder='Telefone' style='width: 100%; margin: auto; height: 100%;' value='{7}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='tlf3' name='tlf3' placeholder='Telefone' style='width: 100%; margin: auto; height: 100%;' value='{8}'/>
                                                    </div>
                                                </div>
                                            </div>",
                                                myReader["MORADA"].ToString(),
                                                myReader["FACEBOOK"].ToString(),
                                                myReader["INSTAGRAM"].ToString(),
                                                myReader["EMAIL1"].ToString(),
                                                myReader["EMAIL2"].ToString(),
                                                myReader["EMAIL3"].ToString(),
                                                myReader["TLF1"].ToString(),
                                                myReader["TLF2"].ToString(),
                                                myReader["TLF3"].ToString(),
                                                myReader["EMAIL4"].ToString(),
                                                myReader["WHATSAPP"].ToString(),
                                                myReader["YOUTUBE"].ToString(),
                                                myReader["linkedin"].ToString());
                }
                connection.Close();
                divTable.InnerHtml = table.ToString();
            }
            else
            {
                // Adiciona as linhas com dados
                table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: 'Noto Sans', sans-serif !important; height:100%'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar.</span>        
                                        </div>");
                connection.Close();
                divTable.InnerHtml = table.ToString();
            }
        }
        catch (Exception exc)
        {
            // Adiciona as linhas com dados
            table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: 'Noto Sans', sans-serif !important; height:100%'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar.</span>        
                                        </div>");
            connection.Close();
            divTable.InnerHtml = table.ToString();
        }
    }

    [WebMethod]
    public static string editar(string id_operador, string morada, string fb, string instagram, string email1, string email2, string email3, string tlf1, string tlf2, string tlf3, string email4, string whatsapp, string youtube, string linkedin)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @codigo_op char(30) = (select ltrim(rtrim(codigo)) from operadores where operadoresid = @id_operador);
	                                        DECLARE @morada varchar(max) = '{1}';
	                                        DECLARE @fb varchar(max) = '{2}';
	                                        DECLARE @instagram varchar(max) = '{3}';
                                            DECLARE @email1 varchar(max) = '{4}';
	                                        DECLARE @email2 varchar(max) = '{5}';
	                                        DECLARE @email3 varchar(max) = '{6}';
                                            DECLARE @tlf1 varchar(max) = '{7}';
	                                        DECLARE @tlf2 varchar(max) = '{8}';
	                                        DECLARE @tlf3 varchar(max) = '{9}';
                                            DECLARE @email4 varchar(max) = '{10}';
                                            DECLARE @whatsapp varchar(max) = '{11}';
                                            DECLARE @youtube varchar(max) = '{12}';
                                            DECLARE @linkedin varchar(max) = '{13}';

                                            UPDATE WS_CONTACTOS
                                                SET morada = @morada,
                                                facebook = @fb,
                                                instagram = @instagram,
                                                email1 = @email1,
                                                email2 = @email2,
                                                email3 = @email3,
                                                tlf1 = @tlf1,
                                                tlf2 = @tlf2,
                                                tlf3 = @tlf3,
                                                email4 = @email4,
                                                whatsapp = @whatsapp,
                                                youtube = @youtube,
                                                linkedin = @linkedin,
                                                ctrlcodop = @codigo_op,
                                                ctrldata = getdate()",
                                                id_operador, morada, fb, instagram, email1, email2, email3, tlf1, tlf2, tlf3, email4, whatsapp, youtube, linkedin);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"Contactos atualizado com sucesso!");
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao atualizar contactos!");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao atualizar contactos!");
        connection.Close();
        return table.ToString();
    }
}
