using System;
using System.Web.UI;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.IO;

public partial class UploadPhotos : Page
{
    string separador = "";
    string id = "";
    string campanha = "";
    string servicos = "";
    string artigos = "";
    string eventos = "";
    string parceiros = "";
    string newsletter = "";
    string staff = "";
    string icons_modalidades = "";
    string modalidades = "";
    string landing = "";
    string horarios = "";
    string banners = "";
    string musica = "";
    string video = "";
    string fotos_carregadas = "";
    string cv = "";
    string pathBack = "../";
    string imgCampanhaName = "campanha.jpg";
    string imgLandingPhotoName = "landing_photo.jpg";
    string site = "";
    string imgHorarioName = "";

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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "UploadPhotos"))
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
		                                        site,
		                                        software,
		                                        campanha,
		                                        servicos,
		                                        artigos,
		                                        eventos,
		                                        parceiros,
		                                        newsletter,
		                                        staff,
		                                        icons_modalidades,
		                                        modalidades,
		                                        landing,
		                                        horarios,
		                                        banners,
		                                        musica,
		                                        video,
		                                        fotos_carregadas,
		                                        cv
                                            from report_paths()");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    site = myReader["site"].ToString();

                    campanha = site + myReader["campanha"].ToString();
                    servicos = site + myReader["servicos"].ToString();
                    artigos = site + myReader["artigos"].ToString();
                    eventos = site + myReader["eventos"].ToString();
                    parceiros = site + myReader["parceiros"].ToString();
                    newsletter = site + myReader["newsletter"].ToString();
                    staff = site + myReader["staff"].ToString();
                    icons_modalidades = site + myReader["icons_modalidades"].ToString();
                    modalidades = site + myReader["modalidades"].ToString();
                    landing = myReader["landing"].ToString();
                    horarios = site + myReader["horarios"].ToString();
                    banners = site + myReader["banners"].ToString();
                    musica = site + myReader["musica"].ToString();
                    video = site + myReader["video"].ToString();
                    fotos_carregadas = site + myReader["fotos_carregadas"].ToString();
                    cv = site + myReader["cv"].ToString();
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
            return;
        }
    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        StatusLabel.Text = "EM PROCESSAMENTO! POR FAVOR, NÃO CLIQUE EM NENHUMA TECLA E AGUARDE...";

        if (FileUploadControl.HasFile)
        {
            try
            {
                string filename = Path.GetFileName(FileUploadControl.FileName);
                string extension = Path.GetExtension(FileUploadControl.FileName);
                string name = photoName.Text.ToString();
                string pathNameComplete = "";
                int fileSize = FileUploadControl.PostedFile.ContentLength;
                imgHorarioName = "horario" + extension;

                if (fileSize > 1048576000)
                {
                    StatusLabel.Text = "O tamanho do ficheiro excede o tamanho permitido (1GB). Por favor, diminua o tamanho do ficheiro ou carregue outro!";
                    return;
                }

                name = name.Replace("\"", "");
                name = name.Replace(" ", "_");
                name = name.Replace("'", "");

                filename = filename.Replace("\"", "");
                filename = filename.Replace(" ", "_");
                filename = filename.Replace("'", "");

                if ((localizacao.Text != "Campanha" || localizacao.Text != "Landing") && string.IsNullOrEmpty(name))
                {
                    name = filename.Replace(extension, "");
                }

                if (extension != ".mp3" && localizacao.Text == "Musica")
                {
                    StatusLabel.Text = "O ficheiro carregado tem de ter a extensão mp3!";
                    return;
                }

                if (extension != ".mp4" && extension != ".mkv" && extension != ".webm" && localizacao.Text == "Video")
                {
                    StatusLabel.Text = "O ficheiro carregado tem de ter a extensão mp4 / mkv / webm!";
                    return;
                }

                switch (localizacao.Text)
                {
                    case "Campanha":
                        FileUploadControl.SaveAs(Server.MapPath(pathBack + campanha) + imgCampanhaName);
                        pathNameComplete = Server.MapPath(pathBack + campanha) + imgCampanhaName;
                        break;
                    case "Servicos":
                        FileUploadControl.SaveAs(Server.MapPath(pathBack + servicos) + name + extension);
                        pathNameComplete = Server.MapPath(pathBack + servicos) + name + extension;
                        break;
                    case "Eventos":
                        FileUploadControl.SaveAs(Server.MapPath(pathBack + eventos) + name + extension);
                        pathNameComplete = Server.MapPath(pathBack + eventos) + name + extension;
                        break;
                    case "Artigos":
                        FileUploadControl.SaveAs(Server.MapPath(pathBack + artigos) + name + extension);
                        pathNameComplete = Server.MapPath(pathBack + artigos) + name + extension;
                        break;
                    case "Newsletter":
                        FileUploadControl.SaveAs(Server.MapPath(pathBack + newsletter) + name + extension);
                        pathNameComplete = Server.MapPath(pathBack + newsletter) + name + extension;
                        break;
                    case "Staff":
                        FileUploadControl.SaveAs(Server.MapPath(pathBack + staff) + name + extension);
                        pathNameComplete = Server.MapPath(pathBack + staff) + name + extension;
                        break;
                    case "IconsModalidades":
                        FileUploadControl.SaveAs(Server.MapPath(pathBack + icons_modalidades) + name + extension);
                        pathNameComplete = Server.MapPath(pathBack + icons_modalidades) + name + extension;
                        break;
                    case "Modalidades":
                        FileUploadControl.SaveAs(Server.MapPath(pathBack + modalidades) + name + extension);
                        pathNameComplete = Server.MapPath(pathBack + modalidades) + name + extension;
                        break;
                    case "Parceiros":
                        FileUploadControl.SaveAs(Server.MapPath(pathBack + parceiros) + name + extension);
                        pathNameComplete = Server.MapPath(pathBack + parceiros) + name + extension;
                        break;
                    case "Landing":
                        FileUploadControl.SaveAs(Server.MapPath(pathBack + landing) + imgLandingPhotoName);
                        pathNameComplete = Server.MapPath(pathBack + landing) + imgLandingPhotoName;
                        break;
                    case "Horario":
                        try
                        {
                            if (File.Exists((HttpContext.Current.Server.MapPath("~") + "/" + horarios + "/horario.pdf")) && extension.Contains("pdf"))
                                File.Delete((HttpContext.Current.Server.MapPath("~") + "/" + horarios + "/horario.pdf"));

                            if (File.Exists((HttpContext.Current.Server.MapPath("~") + "/" + horarios + "/horario.png")) && extension.Contains("png"))
                                File.Delete((HttpContext.Current.Server.MapPath("~") + "/" + horarios + "/horario.png"));

                            if (File.Exists((HttpContext.Current.Server.MapPath("~") + "/" + horarios + "/horario.JPG")) && extension.Contains("JPG"))
                                File.Delete((HttpContext.Current.Server.MapPath("~") + "/" + horarios + "/horario.JPG"));

                            if (File.Exists((HttpContext.Current.Server.MapPath("~") + "/" + horarios + "/horario.jpg")) && extension.Contains("jpg"))
                                File.Delete((HttpContext.Current.Server.MapPath("~") + "/" + horarios + "/horario.jpg"));
                        }
                        catch (Exception exc)
                        {

                        }

                        FileUploadControl.SaveAs(Server.MapPath(pathBack + horarios) + imgHorarioName);
                        pathNameComplete = Server.MapPath(pathBack + horarios) + imgHorarioName;
                        break;
                    case "Banners":
                        FileUploadControl.SaveAs(Server.MapPath(pathBack + banners) + name + extension);
                        pathNameComplete = Server.MapPath(pathBack + banners) + name + extension;
                        break;
                    case "Musica":
                        FileUploadControl.SaveAs(Server.MapPath(pathBack + musica) + "music_background.mp3");
                        pathNameComplete = Server.MapPath(pathBack + musica) + "music_background.mp3";
                        break;
                    case "Video":
                        FileUploadControl.SaveAs(Server.MapPath(pathBack + video) + "video" + extension);
                        pathNameComplete = Server.MapPath(pathBack + video) + "video" + extension;
                        break;
                }

                StatusLabel.Text = "Estado do Carregamento: Ficheiro carregado com sucesso em " + pathNameComplete + "!";
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
