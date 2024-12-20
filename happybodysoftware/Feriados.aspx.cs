using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Feriados : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "Feriados"))
            {
                
            }
        }
    }

    [WebMethod]
    public static string load(string filtro)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @filtro varchar(max) = {0};

                                            SELECT 
                                                FERIADOSID as ID_FERIADO,
                                                DIA, MES, ANO, DESIGNACAO,
                                                CONVERT(VARCHAR(10), 
                                                ((CASE WHEN DIA > 9 THEN LTRIM(RTRIM(STR(DIA))) ELSE '0' + LTRIM(RTRIM(STR(DIA))) END) + '-' +
                                                (CASE WHEN MES > 9 THEN LTRIM(RTRIM(STR(MES))) ELSE '0' + LTRIM(RTRIM(STR(MES))) END) + '-' + LTRIM(RTRIM(STR(ANO))))) as DATA_COMPLETA,
                                                CAST(ABERTO AS INT) AS ABERTO
                                            FROM FERIADOS fer
                                            WHERE (@filtro is null or DESIGNACAO like '%' + @filtro + '%' or DIA like '%' + @filtro + '%'
                                            or MES like '%' + @filtro + '%' or ANO like '%' + @filtro + '%' or (CONVERT(VARCHAR(10), 
                                                ((CASE WHEN DIA > 9 THEN LTRIM(RTRIM(STR(DIA))) ELSE '0' + LTRIM(RTRIM(STR(DIA))) END) + '-' +
                                                (CASE WHEN MES > 9 THEN LTRIM(RTRIM(STR(MES))) ELSE '0' + LTRIM(RTRIM(STR(MES))) END) + '-' + LTRIM(RTRIM(STR(ANO)))))) like '%' + @filtro + '%')
                                            ORDER BY ANO, MES, DIA", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerTable' colspan='4'>Feriados</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick='openFeriado({0});'>
                                            <td colspan='4'>
                                                {1}<br />
                                                {2}
                                            </td>
                                        </tr>",
                                                myReader["ID_FERIADO"].ToString(),
                                                myReader["DATA_COMPLETA"].ToString(),
                                                myReader["DESIGNACAO"].ToString(),
                                                myReader["DIA"].ToString(),
                                                myReader["MES"].ToString(),
                                                myReader["ANO"].ToString());
                }

                // Adiciona as linhas com dados
                table.AppendFormat(@"   <tr>
                                            <td>
                                                Data
                                                <input type='text' class='form-control' placeholder='Data' style='width: 100%' id='data' />
                                            </td>
                                            <td>
                                                Notas
                                                <input type='text' class='form-control' placeholder='Notas' style='width: 100%' id='notas' />
                                            </td>
                                            <td>
                                                Aberto
                                                <input type='checkbox' class='form-control' style='width: 100%' id='aberto' />
                                            </td>
                                            <td>
                                                <input type='button' class='form-control' value='Guardar' style='width: 100%' onclick='saveFeriado();' />
                                            </td>
                                        </tr>");

                table.AppendFormat("</tbody></table>");

                connection.Close();
                return table.ToString();
            }
            else
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerTable' colspan='4'>Feriados</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                // Adiciona as linhas com dados
                table.AppendFormat(@"   <tr>
                                            <td>
                                                Data
                                                <input type='text' class='form-control' placeholder='Data' style='width: 100%' id='data' />
                                            </td>
                                            <td>
                                                Notas
                                                <input type='text' class='form-control' placeholder='Notas' style='width: 100%' id='notas' />
                                            </td>
                                            <td>
                                                Aberto
                                                <input type='checkbox' class='form-control' style='width: 100%' id='aberto' />
                                            </td>
                                            <td>
                                                <input type='button' class='form-control' value='Guardar' style='width: 100%' onclick='saveFeriado();' />
                                            </td>
                                        </tr>");

                table.AppendFormat("</tbody></table>");

                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:medium;margin: auto;color:#000'>Ocorreu um erro ao carregar os feriados: {0}</div>", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }


    [WebMethod]
    public static string guardaFeriado(string id_operador, string data, string notas, string aberto)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @codop char(30) = (select ltrim(rtrim(codigo)) from operadores where operadoresid = @id_operador)
                                            DECLARE @data datetime = '{1}';
                                            DECLARE @notas varchar(max) = '{2}';
                                            DECLARE @aberto bit = {3};
                                            DECLARE @newid int;
                                            SET @data = DATEADD(hh, 12, @data);

                                            INSERT INTO FERIADOS(DIA, MES, ANO, DESIGNACAO, CTRLCODOP)
                                            SELECT DAY(@data), MONTH(@data), YEAR(@data), @notas, @codop

                                            SET @newid = SCOPE_IDENTITY();
                                            SELECT @newid as newid", id_operador, data, notas, aberto);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"{0}", myReader["newid"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                // Adiciona as linhas
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
    }


    [WebMethod]
    public static string loadFeriado(string id_feriado)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_feriado int = {0};

                                            SELECT 
                                                FERIADOSID as ID_FERIADO,
                                                DIA, MES, ANO, DESIGNACAO,
                                                CONVERT(VARCHAR(10), 
                                                ((CASE WHEN DIA > 9 THEN LTRIM(RTRIM(STR(DIA))) ELSE '0' + LTRIM(RTRIM(STR(DIA))) END) + '-' +
                                                (CASE WHEN MES > 9 THEN LTRIM(RTRIM(STR(MES))) ELSE '0' + LTRIM(RTRIM(STR(MES))) END) + '-' + LTRIM(RTRIM(STR(ANO))))) as DATA_COMPLETA,
                                                CAST(ABERTO AS INT) AS ABERTO
                                            FROM FERIADOS fer
                                            WHERE FERIADOSID = @id_feriado", id_feriado);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   Data
                                            <input type='text' class='form-control' placeholder='Data' style='width: 100%; margin-bottom: 5px;' id='dataedit' value='{2}'/>
                                            Notas
                                            <input type='text' class='form-control' placeholder='Notas' style='width: 100%; margin-bottom: 5px;' id='notasedit' value='{1}' />
                                            Aberto
                                            <input type='checkbox' class='form-control' style='width: 100%; margin-bottom: 25px;' id='abertoedit' {3}/>

                                            <input type='button' class='form-control' value='Apagar' style='width: 48%; margin-bottom: 5px; float: left;' onclick='deleteFeriado({0});' />
                                            <input type='button' class='form-control' value='Guardar' style='width: 48%; margin-bottom: 5px; float: right;' onclick='updateFeriado({0});' />",
                                                myReader["ID_FERIADO"].ToString(),
                                                myReader["DESIGNACAO"].ToString(),
                                                myReader["DATA_COMPLETA"].ToString(),
                                                myReader["ABERTO"].ToString() == "1" ? " checked " : "");
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                // Adiciona as linhas
                table.AppendFormat(@"<span style='color: #FFF'>Não existem dados relativos ao feriado selecionado</span>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            // Adiciona as linhas
            table.AppendFormat(@"<span style='color: #FFF'>Não existem dados relativos ao feriado selecionado: {0}</span>", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }


    [WebMethod]
    public static string apagaFeriado(string id_feriado)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_feriado int = {0};

                                            DELETE FROM FERIADOS WHERE FERIADOSID = @id_feriado", id_feriado);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            // Adiciona as linhas
            table.AppendFormat(@"0");
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"-999");
            connection.Close();
            return table.ToString();
        }
    }


    [WebMethod]
    public static string atualizaFeriado(string id_operador, string data, string notas, string id_feriado, string aberto)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_operador int = {0};
                                            DECLARE @codop char(30) = (select ltrim(rtrim(codigo)) from operadores where operadoresid = @id_operador)
                                            DECLARE @data datetime = '{1}';
                                            DECLARE @notas varchar(max) = '{2}';
                                            DECLARE @id_feriado int = {3};
                                            DECLARE @aberto bit = {4};
                                            SET @data = DATEADD(hh, 12, @data);

                                            UPDATE FERIADOS
                                                SET DESIGNACAO = @notas, CTRLCODOP = @codop, DIA = DAY(@data), MES = MONTH(@data), ANO = YEAR(@data), ABERTO = @aberto
                                            WHERE FERIADOSID = @id_feriado", id_operador, data, notas, id_feriado, aberto);

            //return sql;

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            // Adiciona as linhas
            table.AppendFormat(@"0");
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"-999");
            connection.Close();
            return table.ToString();
        }
    }
}
