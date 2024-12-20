using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class ContactosAtivos : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "ContactosAtivos"))
            {

            }
        }

        loadTable();
    }

    private void loadTable()
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
                                            DECLARE @id_socio int;
                                            DECLARE @nr_socio int;
                                            DECLARE @ativo bit = 1;
                                            
                                            select NR_SOCIO, NOME, TELEMOVEL, EMAIL
                                            FROM CONTACTOS_SOCIOS(@ativo, @nr_socio, @id_socio)
                                            ORDER BY NR_SOCIO", id);

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid' style='border: none !important;'>
                                            <thead>
						                        <tr>
                                                    <th class='headerLeft'>Sócio</th>
                                                    <th class='headerCenter'>Telemóvel</th>
                                                    <th class='headerRight'>Email</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr>
                                            <td>{0} - {1}</td>
                                            <td>{2}</td>
                                            <td>{3}</td>
                                        </tr>",
                                                oDs.Tables[0].Rows[i]["NR_SOCIO"].ToString(),
                                                oDs.Tables[0].Rows[i]["NOME"].ToString(),
                                                oDs.Tables[0].Rows[i]["TELEMOVEL"].ToString(),
                                                oDs.Tables[0].Rows[i]["EMAIL"].ToString());
                }

                table.AppendFormat("</tbody></table>");
            }
            else
            {
                connection.Close();
                table.AppendFormat(@"Não existem dados a apresentar");
            }
        }
        catch (Exception exc)
        {
            divTable.InnerHtml = exc.ToString();
        }

        divTable.InnerHtml = table.ToString();
    }
}