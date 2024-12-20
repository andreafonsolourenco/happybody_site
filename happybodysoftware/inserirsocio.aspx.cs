using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class inserirsocio : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "MainMenu"))
            {
                
            }
        }

        string name = loadOperatorName();
        loadComerciais();

        if (name == "Utilizador não encontrado!" || id == "" || id == null)
        {
            Response.Redirect("login.aspx?msg=Utilizador inválido");
        }
        else
        {
            //operatorName.InnerHtml = name;
        }
    }

    [WebMethod]
    public static string load(string id_operador, string filtro)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @FILTRO varchar(MAX) = {1}

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
                                            WHERE (@FILTRO IS NULL OR (soc.NR_SOCIO LIKE '%' + @FILTRO + '%' OR soc.NOME LIKE '%' + @FILTRO + '%'
                                            OR soc.TELEMOVEL LIKE '%' + @FILTRO + '%'))
                                            ORDER BY soc.NR_SOCIO", id_operador, filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));

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
                    table.AppendFormat(@"<tr id='ln_{2}' onclick='selectRow({2});'>
                                            <td style='display:none' id='id_{2}'>{0}</td>
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

    [WebMethod]
    public static string insert(string id_operador, string nome, string morada, string codpostal, string localidade, string tlf_emergencia, string tlm,
        string dataNascimento, string cc, string validadeCC, string profissao, string id_comercial, string sexo, string email, string nrSocio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @nome varchar(max) = '{1}';
	                                        DECLARE @morada varchar(max) = '{2}';
	                                        DECLARE @codpostal varchar(10) = '{3}';
	                                        DECLARE @localidade varchar(max) = '{4}';
	                                        DECLARE @tlf_emergencia varchar(20) = '{5}';
	                                        DECLARE @telemovel varchar(20) = '{6}';
	                                        DECLARE @data_nascimento datetime = '{7}';
	                                        DECLARE @cc_nr varchar(20) = '{8}';
	                                        DECLARE @validade_cc datetime = '{9}';
	                                        DECLARE @profissao varchar(max) = '{10}';
	                                        DECLARE @id_comercial int = {11};
	                                        DECLARE @sexo char(1) = '{12}';
	                                        DECLARE @email varchar(200) = '{13}';
                                            DECLARE @nr_socio int = {14};
	                                        DECLARE @id_novo_socio int;

                                            EXEC CRIA_SOCIO @id_operador, @nome, @morada, @codpostal, @localidade, @tlf_emergencia, @telemovel,
	                                            @data_nascimento, @cc_nr, @validade_cc, @profissao, @id_comercial, @sexo, @email, @nr_socio, @id_novo_socio output

                                            SELECT @id_novo_socio as id", id_operador, nome, morada, codpostal, localidade, tlf_emergencia, tlm,
                                                                        dataNascimento, cc, validadeCC, profissao, id_comercial, sexo, email, nrSocio);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    if(Convert.ToInt32(myReader["id"].ToString())>0)
                        table.AppendFormat(@"Sócio criado com sucesso!", myReader["id"].ToString());
                    else
                        table.AppendFormat(@"Erro ao criar sócio!");
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"Erro ao criar sócio!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao criar sócio!");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao criar sócio!");
        connection.Close();
        return table.ToString();
    }

    private string loadOperatorName()
    {
        StringBuilder table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id INT = {0};
                                        
                                            SELECT 
                                                LTRIM(RTRIM(NOME)) as NOME
                                            FROM OPERADORES
                                            WHERE OPERADORESID = @id", id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    table.AppendFormat(@"{0}", myReader["NOME"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"Utilizador não encontrado!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
        }

        table.AppendFormat(@"Utilizador não encontrado!");
        return table.ToString();
    }

    private void loadComerciais()
    {
        StringBuilder table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT
                                                OPERADORESID as ID,
                                                LTRIM(RTRIM(NOME)) as NOME
                                            FROM OPERADORES
                                            WHERE LTRIM(RTRIM(CODIGO)) <> 'MCOELHO'
                                            AND LTRIM(RTRIM(CODIGO)) <> 'ALOURENCO'
                                            ORDER BY ADMINISTRADOR DESC");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"<select class='form-control' id='comercial'>");
                while (myReader.Read())
                {
                    table.AppendFormat(@"<option value='{0}'>{1}</option>", myReader["ID"].ToString(), myReader["NOME"].ToString());
                }
                table.AppendFormat(@"</select>");

                connection.Close();
                comercialDiv.InnerHtml = table.ToString();
            }
            else
            {
                table.AppendFormat(@"Utilizador não encontrado!");
                connection.Close();
                comercialDiv.InnerHtml = table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            comercialDiv.InnerHtml = table.ToString();
        }
    }

    [WebMethod]
    public static string getCustomerData(string id_operador, string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_operador int = {0};
                                            DECLARE @id_socio int = {1};

                                            SELECT 
                                                soc.SOCIOSID, 
                                                soc.NOME,
	                                            soc.MORADA,
	                                            soc.CODPOSTAL,
	                                            soc.LOCALIDADE,
	                                            soc.TLF_EMERGENCIA,
	                                            soc.TELEMOVEL,
	                                            CONVERT(VARCHAR(10), CAST(soc.DATA_NASCIMENTO as DATE), 103) as DATA_NASCIMENTO,
	                                            soc.CC_NR,
                                                CONVERT(VARCHAR(10), CAST(soc.VALIDADE_CC as DATE), 103) as VALIDADE_CC,
	                                            soc.PROFISSAO,
	                                            op.NOME as COMERCIAL,
                                                op.OPERADORESID as COMERCIAL_ID,
                                                soc.SEXO,
	                                            soc.EMAIL,
                                                soc.NR_SOCIO
                                            FROM SOCIOS soc
                                            INNER JOIN OPERADORES op on op.OPERADORESID = soc.ID_COMERCIAL
                                            WHERE @id_socio = soc.SOCIOSID", id_operador, id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            int conta = 0;

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    string s = "";

                    if (myReader["SEXO"].ToString()=="M"){
                        s = "<label class='radio-inline'><input type='radio' name='optradioEdit' value='M' checked='checked'>Masculino</label><label class='radio-inline'><input type='radio' name='optradioEdit' value='F'>Feminino</label>";
                    }
                    else {
                        s = "<label class='radio-inline'><input type='radio' name='optradioEdit' value='M'>Masculino</label><label class='radio-inline'><input type='radio' name='optradioEdit' value='F' checked='checked'>Feminino</label>";
                    }

                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <div class='col-lg-2 col-md-2 col-sm-2 col-xs-2 line'>
                                                <input type='text' class='form-control' id='nrSocioEdit' name='nrSocioEdit' placeholder='Nº' required='required' style='width: 100%; margin: auto;' value='{14}'/>
                                            </div>
                                            <div class='col-lg-10 col-md-10 col-sm-10 col-xs-10 line'>
                                                <input type='text' class='form-control' id='nameEdit' name='nameEdit' placeholder='Nome' required='required' style='width: 100%; margin: auto;' value='{1}'/>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 line'>
                                                <input type='text' class='form-control' id='moradaEdit' name='moradaEdit' placeholder='Morada' required='required' style='width: 100%; margin: auto;' value='{2}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                <input type='text' class='form-control' id='codpostalEdit' name='codpostalEdit' placeholder='Código Postal: xxxx-xxx' required='required' style='width: 100%; margin: auto;' value='{3}'/>
                                            </div>
                                            <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8 line'>
                                                <input type='text' class='form-control' id='localidadeEdit' name='localidadeEdit' placeholder='Localidade' required='required' style='width: 100%; margin: auto; float: left' value='{4}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                <input type='text' class='form-control' id='tlfEdit' name='tlfEdit' placeholder='Telefone' required='required' style='width: 100%; margin: auto; float: left' value='{6}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                <input type='text' class='form-control' id='tlfemergenciaEdit' name='tlfemergenciaEdit' placeholder='Telefone de Emergência' required='required' style='width: 100%; margin: auto;' value='{5}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                <input type='text' class='form-control' id='emailEdit' name='emailEdit' placeholder='Email' required='required' style='width: 100%; margin: auto;' value='{12}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                <input type='text' class='form-control' id='datanascEdit' name='datanascEdit' placeholder='Data de Nascimento' required='required' style='width: 100%; margin: auto;' value='{7}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                <input type='text' class='form-control' id='ccEdit' name='ccEdit' placeholder='Nº CC' required='required' style='width: 100%; margin: auto;' value='{8}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                <input type='text' class='form-control' id='validadeccEdit' name='validadeccEdit' placeholder='Validade CC' required='required' style='width: 100%; margin: auto;' value='{9}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                <input type='text' class='form-control' id='profissaoEdit' name='profissao' placeholder='Profissão' required='required' style='width: 100%; margin: auto;' value='{10}'/>
                                            </div>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line' id='comercialDivEdit' runat='server'></div>
                                            <span class='variaveis' id='id_comercialedit'>{13}</span>
                                            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                                                <form style='width:100%' id='sexoEdit' name='sexoEdit'>{11}</form>
                                            </div>",
                                                myReader["SOCIOSID"].ToString(),
                                                myReader["NOME"].ToString(),
                                                myReader["MORADA"].ToString(),
                                                myReader["CODPOSTAL"].ToString(),
                                                myReader["LOCALIDADE"].ToString(),
                                                myReader["TLF_EMERGENCIA"].ToString(),
                                                myReader["TELEMOVEL"].ToString(),
                                                myReader["DATA_NASCIMENTO"].ToString(),
                                                myReader["CC_NR"].ToString(),
                                                myReader["VALIDADE_CC"].ToString(),
                                                myReader["PROFISSAO"].ToString(),
                                                s,
                                                myReader["EMAIL"].ToString(),
                                                myReader["COMERCIAL_ID"].ToString(),
                                                myReader["NR_SOCIO"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem sócios com o número indicado.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem sócios com o número indicado.</div>");
            table.AppendFormat("{0}", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string loadComerciaisEdit(string id)
    {
        StringBuilder table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT
                                                OPERADORESID as ID,
                                                LTRIM(RTRIM(NOME)) as NOME
                                            FROM OPERADORES
                                            WHERE LTRIM(RTRIM(CODIGO)) <> 'MCOELHO'
                                            AND LTRIM(RTRIM(CODIGO)) <> 'ALOURENCO'
                                            ORDER BY ADMINISTRADOR DESC");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"<select class='form-control' id='comercialEdit'>");
                while (myReader.Read())
                {
                    table.AppendFormat(@"<option value='{0}' {2}>{1}</option>", 
                        myReader["ID"].ToString(), 
                        myReader["NOME"].ToString(),
                        id == myReader["ID"].ToString() ? "selected" : "");
                }
                table.AppendFormat(@"</select>");

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"Utilizador não encontrado!");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"{0}", exc.ToString());
            return table.ToString();
        }
    }

    [WebMethod]
    public static string editar(string id_operador, string nome, string morada, string codpostal, string localidade, string tlf_emergencia, string tlm,
        string dataNascimento, string cc, string validadeCC, string profissao, string id_comercial, string sexo, string email, string id_socio, string nrSocio)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @nome varchar(max) = '{1}';
	                                        DECLARE @morada varchar(max) = '{2}';
	                                        DECLARE @codpostal varchar(10) = '{3}';
	                                        DECLARE @localidade varchar(max) = '{4}';
	                                        DECLARE @tlf_emergencia varchar(20) = '{5}';
	                                        DECLARE @telemovel varchar(20) = '{6}';
	                                        DECLARE @data_nascimento datetime = '{7}';
	                                        DECLARE @cc_nr varchar(20) = '{8}';
	                                        DECLARE @validade_cc datetime = '{9}';
	                                        DECLARE @profissao varchar(max) = '{10}';
	                                        DECLARE @id_comercial int = {11};
	                                        DECLARE @sexo char(1) = '{12}';
	                                        DECLARE @email varchar(200) = '{13}';
	                                        DECLARE @id_novo_socio int = {14};
                                            DECLARE @nr_socio int = {15};
                                            DECLARE @code_op char(30) = (SELECT LTRIM(RTRIM(CODIGO)) FROM OPERADORES WHERE OPERADORESID = @id_operador);

                                            IF(@nr_socio is null or @nr_socio = 0)
                                            BEGIN
                                                SET @nr_socio = ((SELECT MAX(NR_SOCIO) FROM SOCIOS)+1);
                                            END

                                            UPDATE SOCIOS
                                                SET NOME = @nome, 
	                                            MORADA = @morada,
	                                            CODPOSTAL = @codpostal,
	                                            LOCALIDADE = @localidade,
	                                            TLF_EMERGENCIA = @tlf_emergencia,
	                                            TELEMOVEL = @telemovel,
	                                            DATA_NASCIMENTO = @data_nascimento,
	                                            CC_NR = @cc_nr,
	                                            VALIDADE_CC = @validade_cc,
	                                            PROFISSAO = @profissao,
	                                            ID_COMERCIAL = @id_comercial,
	                                            SEXO = @sexo,
	                                            EMAIL = @email,
                                                NR_SOCIO = @nr_socio,
                                                CTRLDATA = getdate(),
	                                            CTRLCODOP = @code_op
                                            WHERE SOCIOSID = @id_novo_socio", id_operador, nome, morada, codpostal, localidade, tlf_emergencia, tlm,
                                                                        dataNascimento, cc, validadeCC, profissao, id_comercial, sexo, email, id_socio, nrSocio);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"Sócio {0} atualizado com sucesso!", nrSocio);
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao atualizar sócio {0}!", nrSocio);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao atualizar sócio {0}!", nrSocio);
        connection.Close();
        return table.ToString();
    }
}
