using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Avisos : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "Avisos"))
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
                                                av.AVISOSID as ID_AVISO,
                                                LTRIM(RTRIM(av.NOTAS)) as NOTAS,
                                                av.VALOR,
	                                            LTRIM(RTRIM(soc.NOME)) as SOCIO,
                                                soc.NR_SOCIO,
                                                CAST(av.ENTRADA as INT) as ENTRADA,
                                                CONVERT(VARCHAR(10), av.DATA_AVISO, 103) as DATA_AVISO
                                            FROM AVISOS av
                                            INNER JOIN SOCIOS soc on soc.SOCIOSID = av.ID_SOCIO
                                            WHERE (@filtro is null or av.NOTAS like '%' + @filtro + '%' or av.VALOR like '%' + @filtro + '%'
                                            or soc.NR_SOCIO like '%' + @filtro + '%' or soc.NOME like '%' + @filtro + '%' or soc.TELEMOVEL like '%' + @filtro + '%'
                                            or soc.EMAIL like '%' + @filtro + '%')
                                            ORDER BY soc.NR_SOCIO asc", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerTable' colspan='6'>Avisos</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick='openAviso({0});'>
                                            <td colspan='6'>
                                                {4} - {3}<br />
                                                {1}<br />
                                                {2} €<br />
                                                {5}<br />
                                                {6}
                                            </td>
                                        </tr>",
                                                myReader["ID_AVISO"].ToString(),
                                                myReader["NOTAS"].ToString(),
                                                myReader["VALOR"].ToString(),
                                                myReader["SOCIO"].ToString(),
                                                myReader["NR_SOCIO"].ToString(),
                                                myReader["DATA_AVISO"].ToString() == "" ? "Sem Data" : myReader["DATA_AVISO"].ToString(),
                                                myReader["ENTRADA"].ToString() == "1" ? "Aviso na Entrada" :"");
                }

                // Adiciona as linhas com dados
                table.AppendFormat(@"   <tr>
                                            <td>
                                                Nº Sócio
                                                <input type='number' class='form-control' placeholder='Nº Sócio' style='width: 100%' id='nrsocio' />
                                            </td>
                                            <td>
                                                Valor
                                                <input type='number' class='form-control' placeholder='Valor' style='width: 100%' id='valor' />
                                            </td>
                                            <td>
                                                Notas
                                                <input type='text' class='form-control' placeholder='Notas' style='width: 100%' id='notas' />
                                            </td>
                                            <td>
                                                Data
                                                <input type='text' class='form-control' placeholder='Data' style='width: 100%' id='data' />
                                            </td>
                                            <td>
                                                Entrada
                                                <input type='checkbox' class='form-control' placeholder='Entrada' style='width: 100%' id='entrada' />
                                            </td>
                                            <td>
                                                <input type='button' class='form-control' value='Guardar' style='width: 100%' onclick='saveAviso();' />
                                            </td>
                                        </tr>");

                table.AppendFormat("</tbody></table></div>");

                connection.Close();
                return table.ToString();
            }
            else
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th class='headerTable' colspan='6'>Avisos</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                // Adiciona as linhas com dados
                table.AppendFormat(@"   <tr>
                                            <td>
                                                Nº Sócio
                                                <input type='number' class='form-control' placeholder='Nº Sócio' style='width: 100%' id='nrsocio' />
                                            </td>
                                            <td>
                                                Valor
                                                <input type='number' class='form-control' placeholder='Valor' style='width: 100%' id='valor' />
                                            </td>
                                            <td>
                                                Notas
                                                <input type='text' class='form-control' placeholder='Notas' style='width: 100%' id='notas' />
                                            </td>
                                            <td>
                                                Data
                                                <input type='text' class='form-control' placeholder='Data' style='width: 100%' id='data' />
                                            </td>
                                            <td>
                                                Entrada
                                                <input type='checkbox' class='form-control' placeholder='Entrada' style='width: 100%' id='entrada' />
                                            </td>
                                            <td>
                                                <input type='button' class='form-control' value='Guardar' style='width: 100%' onclick='saveAviso();' />
                                            </td>
                                        </tr>");

                table.AppendFormat("</tbody></table>");

                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:medium;margin: auto;color:#000'>Ocorreu um erro ao carregar os avisos: {0}</div>", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }


    [WebMethod]
    public static string guardaAviso(string id_operador, string nr_socio, string valor, string notas, string data, string entrada)
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
                                            DECLARE @nr_socio int = {1};
                                            DECLARE @valor decimal(15,2) = {2};
                                            DECLARE @notas varchar(max) = '{3}';
                                            DECLARE @data datetime = {4};
                                            DECLARE @entrada bit = {5};
                                            DECLARE @newid int;
                                            DECLARE @notaslog varchar(max) = 'Foi guardado um aviso pelo operador ' + @codop + ' para o sócio ' + ltrim(rtrim(str(@nr_socio))) + ' no valor de ' + ltrim(rtrim(str(@valor))) + ' com as seguintes notas ' + @notas;
                                            DECLARE @reslog int;

                                            INSERT INTO AVISOS(ID_SOCIO, NOTAS, VALOR, CTRLCODOP, DATA_AVISO, ENTRADA)
                                            SELECT SOCIOSID, @notas, @valor, @codop, @data, @entrada
                                            FROM SOCIOS
                                            WHERE NR_SOCIO = @nr_socio

                                            EXEC REGISTA_LOG @id_operador, 'AVISOS', @notaslog, @reslog output;

                                            SET @newid = SCOPE_IDENTITY();
                                            SELECT @newid as newid", id_operador, nr_socio, valor, notas, string.IsNullOrEmpty(data) ? "NULL" : "'" + data + "'", entrada);

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
    public static string loadAviso(string id_aviso)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_aviso int = {0};

                                            SELECT 
                                                av.AVISOSID as ID_AVISO,
                                                LTRIM(RTRIM(av.NOTAS)) as NOTAS,
                                                av.VALOR,
	                                            LTRIM(RTRIM(soc.NOME)) as SOCIO,
                                                soc.NR_SOCIO,
                                                CAST(av.ENTRADA as INT) as ENTRADA,
                                                CONVERT(VARCHAR(10), av.DATA_AVISO, 103) as DATA_AVISO
                                            FROM AVISOS av
                                            INNER JOIN SOCIOS soc on soc.SOCIOSID = av.ID_SOCIO
                                            WHERE @id_aviso = av.AVISOSID", id_aviso);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   Nº Sócio
                                            <input type='number' class='form-control' placeholder='Nº Sócio' style='width: 100%; margin-bottom: 5px;' id='nrsocioedit' value='{4}'/>
                                            Valor
                                            <input type='number' class='form-control' placeholder='Valor' style='width: 100%; margin-bottom: 5px;' id='valoredit' value='{2}'/>
                                            Notas
                                            <input type='text' class='form-control' placeholder='Notas' style='width: 100%; margin-bottom: 25px;' id='notasedit' value='{1}' />
                                            Data
                                            <input type='text' class='form-control' placeholder='Data' style='width: 100%' id='dataedit' value='{6}'/>
                                            Entrada
                                            <input type='checkbox' class='form-control' placeholder='Entrada' style='width: 100%' id='entradaedit' {7}/>
                                            
                                            <input type='button' class='form-control' value='Apagar' style='width: 48%; margin-bottom: 5px; float: left;' onclick='deleteAviso({5});' />
                                            <input type='button' class='form-control' value='Guardar' style='width: 48%; margin-bottom: 5px; float: right;' onclick='updateAviso({5});' />",
                                                myReader["ID_AVISO"].ToString(),
                                                myReader["NOTAS"].ToString(),
                                                myReader["VALOR"].ToString(),
                                                myReader["SOCIO"].ToString(),
                                                myReader["NR_SOCIO"].ToString(),
                                                id_aviso,
                                                myReader["DATA_AVISO"].ToString(),
                                                myReader["ENTRADA"].ToString() == "1" ? "checked" : "");
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                // Adiciona as linhas
                table.AppendFormat(@"<span style='color: #FFF'>Não existem dados relativos ao aviso selecionado</span>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            // Adiciona as linhas
            table.AppendFormat(@"<span style='color: #FFF'>Não existem dados relativos ao aviso selecionado: {0}</span>", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }


    [WebMethod]
    public static string apagaAviso(string id_aviso, string id_operador)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT DMY;
                                            DECLARE @id_aviso int = {0};
                                            DECLARE @id_operador int = {1};
                                            DECLARE @operatorcode char(30) = (select ltrim(rtrim(codigo)) from operadores where operadoresid = @id_operador)
                                            DECLARE @notaslog varchar(max);
                                            DECLARE @reslog int;

                                            SELECT @notaslog = 'Foi removido o aviso do sócio ' + ltrim(rtrim(str(soc.nr_socio))) + ' com o valor '
                                            + ltrim(rtrim(str(av.valor))) + ' com as notas ' + av.notas
                                            FROM AVISOS av
                                            INNER JOIN SOCIOS soc on soc.sociosid = av.id_socio
                                            where av.AVISOSID = @id_aviso

                                            DELETE FROM AVISOS WHERE AVISOSID = @id_aviso

                                            EXEC REGISTA_LOG @id_operador, 'AVISOS', @notaslog, @reslog output;", id_aviso, id_operador);

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
    public static string atualizaAviso(string id_operador, string nr_socio, string valor, string notas, string id_aviso, string data, string entrada)
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
                                            DECLARE @nr_socio int = {1};
                                            DECLARE @id_socio int = (SELECT SOCIOSID FROM SOCIOS WHERE NR_SOCIO = @nr_socio)
                                            DECLARE @valor decimal(15,2) = {2};
                                            DECLARE @notas varchar(max) = '{3}';
                                            DECLARE @id_aviso int = {4};
                                            DECLARE @data datetime = {5};
                                            DECLARE @entrada bit = {6};
                                            DECLARE @notaslog varchar(max) = 'Foi atualizado o aviso do sócio ' + ltrim(rtrim(str(@nr_socio))) + ' com o valor ' + ltrim(rtrim(str(@valor))) + ' com as notas ' + @notas + ' pelo operador ' + @codop;
                                            DECLARE @reslog int;

                                            UPDATE AVISOS
                                                SET ID_SOCIO = @id_socio, NOTAS = @notas, VALOR = @valor, CTRLCODOP = @codop, DATA_AVISO = @data, ENTRADA = @entrada
                                            WHERE AVISOSID = @id_aviso

                                            EXEC REGISTA_LOG @id_operador, 'AVISOS', @notaslog, @reslog output;", id_operador, nr_socio, valor, notas, id_aviso
                                                                                                                , string.IsNullOrEmpty(data) ? "NULL" : "'" + data + "'", entrada);

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
