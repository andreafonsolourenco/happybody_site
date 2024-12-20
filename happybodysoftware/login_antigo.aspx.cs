using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Specialized;

public partial class login : Page
{
    string erro = "";
    string msgErro = "";
    string mac = "";

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtLogin.Focus();

        try
        {
            erro = HttpContext.Current.Request.Url.PathAndQuery;
            msgErro = Request.QueryString["msg"];

            if (msgErro != "")
            {
                lblerror.InnerHtml = msgErro;
            }
            else
            {
                lblerror.InnerHtml = "";
            }
        }
        catch (Exception exc)
        {
            lblerror.InnerHtml = "";
        }


        //lblip.InnerHtml = GetMACAddress();
        //lblname.InnerHtml = GetMACAddress();
    }

    [WebMethod]
    public static string getOperator(string user, string pass, string pcname)
    {
        var table = new StringBuilder();

        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @user char(30) = '{0}';
                                            DECLARE @pass varchar(60) = '{1}';
                                            DECLARE @id_op int;
                                            DECLARE @msg varchar(max);

                                            EXEC [ENTRADA_OPERADOR] @user, @pass, @id_op output, @msg output

                                            select @id_op as id_op, @msg as ret", user, pass, pcname);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["id_op"].ToString()) < 0)
                        table.AppendFormat(@"{0}", myReader["ret"].ToString());
                    else
                        table.AppendFormat(@"{0}", myReader["id_op"].ToString());
                }

                connection.Close();


                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"Utilizador não encontrado!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Utilizador não encontrado! {0}", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string RegistaLog(string id_operador, string tipo_log, string notas)
    {
        var table = new StringBuilder();

        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @tipo_log varchar(50) = '{1}';
                                            DECLARE @notas varchar(max) = '{2}';
                                            DECLARE @res int;
                                        
                                            EXEC REGISTA_LOG @id_operador, @tipo_log, @notas, @res output

                                            SELECT @res as res", id_operador, tipo_log, notas);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"{0}", myReader["res"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"-1");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"-999");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"-1");
        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string Encrypt(string str)
    {
        string EncryptionKey = "GinasioHappyBody";
        byte[] clearBytes = Encoding.Unicode.GetBytes(str);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                str = Convert.ToBase64String(ms.ToArray());
            }
        }
        return str;
    }

    [WebMethod]
    public static string RequestLicence(string name, string pcname)
    {
        var table = new StringBuilder();

        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @nome varchar(500) = '{0}';
                                            DECLARE @ip varchar(100) = '';
                                            DECLARE @android_id varchar(100)= '{1}';
                                            DECLARE @ret int;
                                        
                                            EXEC INSERE_NOVO_DISPOSITIVO @nome, @ip, @android_id, @ret output

                                            SELECT @ret as ret", name, pcname);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"{0}", myReader["ret"].ToString());
                }

                connection.Close();


                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"-1");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"-1");
            connection.Close();
            return table.ToString();
        }
    }
}
