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

public partial class VideosTreino : Page
{
    string separador = "";
    string id = "";
    string videos_aulas = "";
    string pathBack = "../";
    string video_name = "Treinos_HappyBody.mp4";

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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "VideosTreino"))
            {
                
            }
        }

        loadServers();
    }

    private void loadServers()
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   select 
		                                        videos_treino
                                            from report_paths()");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    videos_aulas = myReader["videos_treino"].ToString();
                }

                connection.Close();
                return;
            }
            else
            {
                return;
            }
        }
        catch (Exception exc)
        {
            StatusLabel.Text = exc.ToString();
            return;
        }
    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        if (FileUploadControl.HasFile)
        {
            try
            {
                string filename = Path.GetFileName(FileUploadControl.FileName);
                string extension = Path.GetExtension(FileUploadControl.FileName);
                int fileSize = FileUploadControl.PostedFile.ContentLength;

                if(!extension.Contains("mp4"))
                {
                    StatusLabel.Text = "O ficheiro tem de estar no formato mp4!";
                    return;
                }

                //if (fileSize > 3 145 728 000‬)
                //{
                //    StatusLabel.Text = "O tamanho do ficheiro excede o tamanho permitido (1GB). Por favor, diminua o tamanho do ficheiro ou carregue outro!";
                //    return;
                //}

                if (File.Exists((HttpContext.Current.Server.MapPath("~") + "/" + videos_aulas + video_name)))
                    File.Delete((HttpContext.Current.Server.MapPath("~") + "/" + videos_aulas + video_name));

                FileUploadControl.SaveAs(Server.MapPath("~") + videos_aulas + video_name);

                StatusLabel.Text = "Estado do Carregamento: Ficheiro carregado com sucesso!";
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Estado do Carregamento: Ocorreu um erro ao carregar o ficheiro: " + ex.Message;
            }
        }
        else
        {
            StatusLabel.Text = "Por favor, carregue um ficheiro para adicionar!";
            return;
        }
    }
}
