using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.IO;

public partial class FotosParceirosWS : Page
{
    string separador = "";
    string id = "";
    string path = "";

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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "FotosParceirosWS"))
            {
                
            }
        }

        getPartnersPath();
    }

    private string getPartnersPath()
    {
        string cs = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = "SELECT CONCAT('../', site, parceiros) PATH from REPORT_PATHS()";

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    path = myReader["PATH"].ToString();
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string load(string filtro)
    {
        var table = new StringBuilder();
        string cs = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_parceiro int;
                                            DECLARE @FILTRO varchar(MAX) = {0};
                                            DECLARE @path varchar(max) = (SELECT CONCAT('../', site, parceiros) from REPORT_PATHS())

                                            SELECT 
                                                id_parceiro,
	                                            nome,
	                                            ordem,
                                                CONCAT(@path, icon) as icon
                                            FROM REPORT_PARCEIROS(@id_parceiro)
                                            WHERE (@FILTRO IS NULL OR NOME LIKE '%' + @FILTRO + '%')
                                            ORDER BY ORDEM asc, NOME asc", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

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
                                                    <th style='text-align: center; width: 100%;'>Parceiros</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr onclick='selectRow({0}, {2});' id='ln_{2}'>
                                            <td style='display:none;' id='id_{2}'>{0}</td>
                                            <td style='display:none;' id='img_{2}'>{3}</td>
                                            <td class='tbodyTrTd'>{1}</td>
                                        </tr>",
                                                myReader["id_parceiro"].ToString(),
                                                myReader["nome"].ToString(),
                                                conta.ToString(),
                                                myReader["icon"].ToString());

                    conta++;
                }

                table.AppendFormat("</tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>N�o existem parceiros a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>N�o existem parceiros a apresentar.</div>");
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
                string partnerID = PartnerID.Text.ToString();
                FileUploadControl.SaveAs(Server.MapPath(path + filename));

                if(String.IsNullOrEmpty(filename))
                {
                    StatusLabel.Text = "Estado do Carregamento: Por favor selecione uma imagem v�lida!";
                    return;
                }

                if (String.IsNullOrEmpty(partnerID))
                {
                    StatusLabel.Text = "Estado do Carregamento: Por favor selecione um parceiro!";
                    return;
                }

                StatusLabel.Text = "Estado do Carregamento: Fotografia carregada com sucesso!";

                connection.Open();

                string sql = string.Format(@"   SET DATEFORMAT dmy;
                                                DECLARE @id int = {1};
                                                DECLARE @parceiroID int = {2};
                                                DECLARE @imagem varchar(max) = '{0}';
                                                DECLARE @operatorcode char(30) = (select ltrim(rtrim(codigo)) from operadores where operadoresid = @id)
                                                DECLARE @notaslog varchar(max) = CONCAT('Foi carregada a foto de parceiros ', @imagem, ' pelo operador ', @operatorcode);
                                                DECLARE @reslog int                                                
                                        
                                                EXEC REGISTA_LOG @id, 'CARREGARFOTOS', @notaslog, @reslog output;

                                                UPDATE WS_PARCEIROS
                                                    SET ICON = @imagem,
                                                        CTRLDATA = GETDATE(),
                                                        CTRLCODOP = @operatorcode
                                                WHERE WS_PARCEIROSID = @parceiroID", filename, id, partnerID);

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
        else
        {
            StatusLabel.Text = "Estado do Carregamento: Por favor selecione uma imagem v�lida!";
        }

        return;
    }
}