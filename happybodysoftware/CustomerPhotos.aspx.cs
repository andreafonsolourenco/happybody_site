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

public partial class CustomerPhotos : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "ServicosWS"))
            {
                
            }
        }
    }

    [WebMethod]
    public static string load(string id_operador, string filtro, string query)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string declares = "";

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @FILTRO VARCHAR(MAX) = {1};

                                            SELECT 
                                                soc.SOCIOSID, 
                                                soc.NOME,
	                                            soc.MORADA,
	                                            soc.CODPOSTAL,
	                                            soc.LOCALIDADE,
	                                            soc.TLF_EMERGENCIA,
	                                            soc.TELEMOVEL,
	                                            CAST(soc.DATA_NASCIMENTO as DATE) as DATA_NASCIMENTO,
	                                            soc.CC_NR,
	                                            CAST(soc.VALIDADE_CC as DATE) as VALIDADE_CC,
	                                            soc.PROFISSAO,
	                                            op.NOME as COMERCIAL,
                                                soc.SEXO,
	                                            soc.EMAIL,
                                                soc.NR_SOCIO
                                            FROM SOCIOS soc
                                            INNER JOIN OPERADORES op on op.OPERADORESID = soc.ID_COMERCIAL
                                            WHERE (@FILTRO IS NULL OR ({2}))
                                            ORDER BY soc.NR_SOCIO", id_operador,
                                                                  filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro),
                                                                  query,
                                                                  declares);

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
                                                    <th class='headerLeft'>Nº</th>
                                                    <th class='headerRight'>Nome</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr onclick='selectRow({3}, {2});' id='ln_{2}'>
                                            <td style='display:none;' id='id_{2}'>{0}</td>
                                            <td class='tbodyTrTdLeft'>{3}</td>
                                            <td class='tbodyTrTdRight'>{1}</td>
                                        </tr>",
                                                myReader["SOCIOSID"].ToString(),
                                                myReader["NOME"].ToString(),
                                                conta.ToString(),
                                                myReader["NR_SOCIO"].ToString());

                    conta++;
                }

                table.AppendFormat("</tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem sócios a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem sócios a apresentar.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        if (FileUploadControl.HasFile)
        {
            try
            {
                string filename = Path.GetFileName(FileUploadControl.FileName);
                string name = photoName.Text.ToString() + ".jpg";
                FileUploadControl.SaveAs(Server.MapPath("img/socios/") + name);

                //switch (localizacao.Text)
                //{
                //    case "Campanha":
                //        FileUploadControl.SaveAs(Server.MapPath("../happybody/img/") + "campanha.png");
                //        break;
                //    case "Servicos":
                //        FileUploadControl.SaveAs(Server.MapPath("../happybody/img/servicos/") + name);
                //        break;
                //    case "Eventos":
                //        FileUploadControl.SaveAs(Server.MapPath("../happybody/img/eventos/") + name);
                //        break;
                //    case "Artigos":
                //        FileUploadControl.SaveAs(Server.MapPath("../happybody/img/artigos/") + name);
                //        break;
                //}

                StatusLabel.Text = "Estado do Carregamento: Fotografia carregada com sucesso!";

                connection.Open();

                string sql = string.Format(@"   SET DATEFORMAT dmy;
                                                DECLARE @id int = {1};
                                                DECLARE @operatorcode char(30) = (select ltrim(rtrim(codigo)) from operadores where operadoresid = @id)
                                                DECLARE @notaslog varchar(max) = 'Foi carregada a foto {0} pelo operador ' + @operatorcode;
                                                DECLARE @reslog int
                                        
                                                EXEC REGISTA_LOG @id, 'FOTOS', @notaslog, @reslog output;", name, id);

                SqlCommand myCommand = new SqlCommand(sql, connection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
                connection.Close();
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Estado do Carregamento: Ocorreu um erro ao carregar a fotografia: " + ex.Message;
            }
        }
    }
}
