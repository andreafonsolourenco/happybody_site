using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class AgenteNaoPagos : Page
{
    string separador = "";
    string operador = "";

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        separador = HttpContext.Current.Request.Url.PathAndQuery;
        operador = Request.QueryString["operador"];
        lbloperatorcode.InnerHtml = operador;

        if (!IsPostBack)
        {
            ClientScriptManager oCsm = this.Page.ClientScript;
            if (!oCsm.IsStartupScriptRegistered(GetType(), "MainMenu"))
            {
                
            }
        }

        agente();
    }

    private void agente()
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @operador varchar(100) = '{0}';
                                            DECLARE @ret int;

                                            EXEC AGENTE_NAO_PAGOS @operador, @ret OUTPUT
                                            SELECT @ret as ret", operador);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if (Convert.ToInt32(myReader["ret"].ToString().Trim()) == 1)
                    {
                        divTable.InnerHtml = "Pagamentos em falta atualizados com sucesso para Não Pagos.";
                    }
                    else if (Convert.ToInt32(myReader["ret"].ToString().Trim()) == 0)
                    {
                        divTable.InnerHtml = "Atualização de pagamentos em falta para não pagos não foi realizada automaticamente pelo Sistema devido ao operador indicado não ter sido o Agente.";
                    }
                    else
                    {
                        divTable.InnerHtml = "Atualização de pagamentos em falta para não pagos não foi realizada automaticamente pelo Sistema devido a um erro no sistema.";
                    }
                }
            }
            else
            {
                connection.Close();
                divTable.InnerHtml = "Atualização de pagamentos em falta para não pagos não foi realizada automaticamente pelo Sistema devido a um erro no sistema.";
            }
        }
        catch (Exception exc)
        {
            connection.Close();
            divTable.InnerHtml = "Atualização de pagamentos em falta para não pagos não foi realizada automaticamente pelo Sistema devido a um erro no sistema: " + exc.ToString();
        }
    }
}
