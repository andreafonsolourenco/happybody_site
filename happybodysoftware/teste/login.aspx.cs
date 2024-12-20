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
using System.Web.Script.Services;

public partial class login : Page
{
    string erro = "";
    string msgErro = "";

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            erro = HttpContext.Current.Request.Url.PathAndQuery;
            msgErro = Request.QueryString["msg"];

            if (msgErro != "")
            {
                errorMessage.Text = msgErro;
            }
            else
            {
                errorMessage.Text = "";
            }
        }
        catch (Exception exc)
        {
            errorMessage.Text = "";
        }
    }

    protected static void RegistaLog(string id_operador, string tipo_log, string notas)
    {
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
                connection.Close();
                return;
            }
            else
            {
                connection.Close();
                return;
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            return;
        }
    }

    protected static string Encrypt(string str)
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

    protected void ValidateLogin(object sender, EventArgs e)
    {
        int id_op = 0;
        string ret_msg = "";
        string id_encrypted = "";
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        string user = username.Text.ToString();
        string pass = password.Text.ToString();

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @user char(30) = '{0}';
                                            DECLARE @pass varchar(60) = '{1}';
                                            DECLARE @id_op int;
                                            DECLARE @msg varchar(max);

                                            EXEC [ENTRADA_OPERADOR] @user, @pass, @id_op output, @msg output

                                            select @id_op as id_op, @msg as ret", user, pass);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["id_op"].ToString()) < 0)
                        ret_msg = myReader["ret"].ToString();
                    else
                    {
                        ret_msg = myReader["ret"].ToString();
                        id_op = Convert.ToInt32(myReader["id_op"].ToString());
                        RegistaLog(id_op.ToString(), "LOGIN", "Login efetuado com o user " + user);
                    }
                }

                connection.Close();
            }
            else
            {
                ret_msg = "Utilizador não encontrado!";
                connection.Close();
            }
        }
        catch (Exception exc)
        {
            ret_msg = "Utilizador não encontrado!";
            connection.Close();
        }

        if(id_op <= 0)
        {
            errorMessage.Text = ret_msg;
            return;
        }

        id_encrypted = Encrypt(id_op.ToString());
        Response.Redirect("MainMenu.aspx?id=" + id_encrypted);
    }
}
