using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.IO;

public partial class GestaoFicheirosWS : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "GaleriaEventosWS"))
            {
                
            }
        }

        loadPath();
    }

    private void loadPath()
    {
        var table = new StringBuilder();
        string server = "";
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT 
                                                SERVIDOR_SITE
                                            FROM APPLICATION_CONFIG");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    server = myReader["SERVIDOR_SITE"].ToString();
                }
                connection.Close();
            }
            else
            {
                server = "";
                connection.Close();
            }

            table.AppendFormat(@"<h2>PASTA</h2>");
            table.AppendFormat(@"<select class='form-control' id='selectPath' style='width:100%; height: 40px; font-size: small; float: left;' onchange='loadImages();'>");
            table.AppendFormat(@"<option value='0'>Selecione uma pasta</option>");
            table.AppendFormat(@"<option value='img'>img</option>");

            foreach (string name in Directory.GetDirectories(HttpContext.Current.Server.MapPath("~") + "//" + server + "img//", "*", System.IO.SearchOption.AllDirectories))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}'>{0}</option>", info.Name);
            }

            table.AppendFormat(@"</select><span class='variaveis' id='generalPath'>{0}</span>", "//" + server);
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            connection.Close();
        }

        divSelectPath.InnerHtml = table.ToString();
    }

    [WebMethod]
    public static string load(string caminho)
    {
        var table = new StringBuilder();
        string server = "";
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT 
                                                SERVIDOR_SITE
                                            FROM APPLICATION_CONFIG");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    server = myReader["SERVIDOR_SITE"].ToString();
                }
                connection.Close();
            }
            else
            {
                server = "";
                connection.Close();
            }

            table.AppendFormat(@"<h2>FICHEIRO</h2>");
            table.AppendFormat(@"<select class='form-control' id='selectFiles' style='width:100%; height: 40px; font-size: small; float: right;' onchange='changeImg();'>");
            table.AppendFormat(@"<option value='0'>Selecione um ficheiro</option>");

            foreach (string name in Directory.GetFiles(HttpContext.Current.Server.MapPath("~") + caminho))
            {
                FileInfo info = new FileInfo(name);
                table.AppendFormat(@"<option value='{0}'>{0}</option>", info.Name);
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            connection.Close();
        }

        return table.ToString();
    }

    [WebMethod]
    public static string deleteFile(string caminho)
    {
        try
        {
            if (File.Exists((HttpContext.Current.Server.MapPath("~") + caminho)))
                File.Delete((HttpContext.Current.Server.MapPath("~") + caminho));
        }
        catch (Exception exc)
        {
            return "Ocorreu um erro ao eliminar o ficheiro selecionado! Por favor, tente novamente!";
        }

        return "Ficheiro apagado com sucesso!";
    }
}
