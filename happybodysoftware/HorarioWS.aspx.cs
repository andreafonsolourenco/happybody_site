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

public partial class HorarioWS : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "GaleriaModalidadesWS"))
            {
                
            }
        }

        loadModalidades();
    }

    [WebMethod]
    public static string load(string id_modalidade)
    {
        var table = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        string horaAnterior = "";
        int dia = 2;
        string diaText = "seg";

        try
        {
            string sql = string.Format(@"   declare @horainicial time = '07:30'
                                            declare @horas table (horas time, dia varchar(10))
                                            declare @id_modalidade int = {0};

                                            insert into @horas (horas, dia) SELECT @horainicial, '2'
                                            insert into @horas (horas, dia) SELECT @horainicial, '3'
                                            insert into @horas (horas, dia) SELECT @horainicial, '4'
                                            insert into @horas (horas, dia) SELECT @horainicial, '5'
                                            insert into @horas (horas, dia) SELECT @horainicial, '6'
                                            insert into @horas (horas, dia) SELECT @horainicial, 'SAB'

                                            set @horainicial = dateadd(mi, 15, @horainicial)

                                            while @horainicial <= '21:30'
                                            begin
                                                if(@horainicial = '20:45')
                                                begin
                                                    insert into @horas (horas, dia) SELECT '20:40', '2'
                                                    insert into @horas (horas, dia) SELECT '20:40', '3'
                                                    insert into @horas (horas, dia) SELECT '20:40', '4'
                                                    insert into @horas (horas, dia) SELECT '20:40', '5'
                                                    insert into @horas (horas, dia) SELECT '20:40', '6'
                                                    insert into @horas (horas, dia) SELECT '20:40', 'SAB'
                                                end
                                                
	                                            insert into @horas (horas, dia) SELECT @horainicial, '2'
                                                insert into @horas (horas, dia) SELECT @horainicial, '3'
                                                insert into @horas (horas, dia) SELECT @horainicial, '4'
                                                insert into @horas (horas, dia) SELECT @horainicial, '5'
                                                insert into @horas (horas, dia) SELECT @horainicial, '6'
                                                insert into @horas (horas, dia) SELECT @horainicial, 'SAB'
	                                            set @horainicial = dateadd(mi, 15, @horainicial)
                                            end

                                            SELECT convert(varchar(5), horas, 14) as horas, h.dia, isnull(rpt.duracao, 0) as duracao
					                        FROM @horas h
					                        outer apply [REPORT_HORARIO](null, @id_modalidade, h.horas, h.dia) rpt
                                            order by h.horas", id_modalidade);

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            int conta = 0;

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                // Adiciona as linhas
                table.AppendFormat(@"   <table id='tableGrid'>
                                            <thead>
						                        <tr>
                                                    <th style='text-align: center; width: 10%;'></th>
                                                    <th style='text-align: center; width: 15%;'>2ª</th>
                                                    <th style='text-align: center; width: 15%;'>3ª</th>
                                                    <th style='text-align: center; width: 15%;'>4ª</th>
                                                    <th style='text-align: center; width: 15%;'>5ª</th>
                                                    <th style='text-align: center; width: 15%;'>6ª</th>
                                                    <th style='text-align: center; width: 15%;'>Sab</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                for (int i = 0; i < oDs.Tables[0].Rows.Count; i++)
                {
                    if(oDs.Tables[0].Rows[i]["horas"].ToString() != horaAnterior)
                    {
                        table.AppendFormat(@"{0}<tr>", horaAnterior == "" ? "" : "</tr>");

                        if (horaAnterior != "")
                        {
                            conta++;
                            dia = 2;
                        }

                        horaAnterior = oDs.Tables[0].Rows[i]["horas"].ToString();
                        table.AppendFormat(@"<td id='hora_{1}' style='background-color: #000 !important; color: #FFF !important;'>{0}</td>", horaAnterior, conta);
                    }

                    switch(dia)
                    {
                        case 2: diaText = "seg";
                            break;
                        case 3:
                            diaText = "ter";
                            break;
                        case 4:
                            diaText = "qua";
                            break;
                        case 5:
                            diaText = "qui";
                            break;
                        case 6:
                            diaText = "sex";
                            break;
                        case 7:
                            diaText = "sab";
                            break;
                    }

                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <td id='{3}_{1}'>
                                                <select class='form-control' id='duracao_{3}_{1}' style='width:100%; height: 40px; font-size: small; float: left;'>
                                                    <option value='0'{4}>Sem aula neste horário</option>
                                                    <option value='15'{5}>15 min</option>
                                                    <option value='30'{6}>30 min</option>
                                                    <option value='45'{7}>45 min</option>
                                                    <option value='60'{8}>60 min</option>
                                                </select>
                                                <span id='duracao_{0}_{3}' style='display:none'>{2}</span>
                                            </td>",
                                                oDs.Tables[0].Rows[i]["horas"].ToString(),
                                                conta.ToString(),
                                                oDs.Tables[0].Rows[i]["duracao"].ToString(),
                                                diaText,
                                                oDs.Tables[0].Rows[i]["duracao"].ToString() == "0" ? " selected " : "",
                                                oDs.Tables[0].Rows[i]["duracao"].ToString() == "15" ? " selected " : "",
                                                oDs.Tables[0].Rows[i]["duracao"].ToString() == "30" ? " selected " : "",
                                                oDs.Tables[0].Rows[i]["duracao"].ToString() == "45" ? " selected " : "",
                                                oDs.Tables[0].Rows[i]["duracao"].ToString() == "60" ? " selected " : "");

                    dia++;
                }

                table.AppendFormat("</tr></tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe horário a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe horário a apresentar.<br />{0}</div>", exc.ToString());
            connection.Close();
            return table.ToString();
        }
    }

    private void loadModalidades()
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SELECT 
                                                CODIGO, WS_MODALIDADESID as ID_MODALIDADE, TITULO
                                            FROM WS_MODALIDADES");

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                table.AppendFormat(@"<select class='form-control' id='selectModalidade' style='width:100%; height: 40px; font-size: small; float: left;' onchange='load();'>");
                table.AppendFormat(@"<option value='0'>Selecione uma modalidade</option>");
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<option value='{1}'>{0}</option>", myReader["TITULO"].ToString(), myReader["ID_MODALIDADE"].ToString());
                }
                table.AppendFormat(@"</select>");
                connection.Close();
            }
            else
            {
                table.AppendFormat(@"<select class='form-control' id='selectModalidade' style='width:100%; height: 40px; font-size: small; float: left;' onchange='load();'>");
                table.AppendFormat(@"<option value='0'>Não existem modalidades a apresentar.</option>");
                table.AppendFormat(@"</select>");
                connection.Close();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"<select class='form-control' id='selectModalidade' style='width:100%; height: 40px; font-size: small; float: left;' onchange='load();'>");
            table.AppendFormat(@"<option value='0'>Não existem modalidades a apresentar.</option>");
            table.AppendFormat(@"</select>");
            connection.Close();
        }

        select.InnerHtml = table.ToString();
    }

    [WebMethod]
    public static string updateHorario(string id_operador, string xml)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_op int = {0};
                                            DECLARE @xml nvarchar(max) = '{1}';
                                            DECLARE @ret int;

                                            EXEC UPDATE_HORARIO_MODALIDADES @id_op, @xml, @ret output

                                            SELECT @ret as ret", id_operador, xml);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"{0}", myReader["ret"].ToString());
                }

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("-1");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("-1");
            connection.Close();
            return table.ToString();
        }
    }
}
