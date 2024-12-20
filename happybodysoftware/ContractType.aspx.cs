using System;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

public partial class ContractType : Page
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
            if (!oCsm.IsStartupScriptRegistered(GetType(), "ContractType"))
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

            string sql = string.Format(@"   DECLARE @id_tipo_contrato int;
                                            DECLARE @FILTRO varchar(MAX) = {0};

                                            SELECT 
                                                TIPO_CONTRATOID,
	                                            CODIGO,
	                                            DESIGNACAO,
	                                            NOTAS,
	                                            DATA_ULTIMA_ALTERACAO,
	                                            OP_ULTIMA_ALTERACAO,
	                                            ACESSOS_POR_SEMANA,
	                                            SABADO,
	                                            NR_AVALIACOES_FISICAS,
	                                            PT,
	                                            NUTRICAO,
	                                            COACH,
	                                            PRECO,
	                                            JOIA,
	                                            DURACAO_MINIMA_MESES,
	                                            ATIVO,
                                                RENOVACAO_AUTOMATICA
                                            FROM REPORT_TIPOS_CONTRATO(@id_tipo_contrato)
                                            WHERE (@FILTRO IS NULL OR (CODIGO LIKE '%' + @FILTRO + '%' OR DESIGNACAO LIKE '%' + @FILTRO + '%'))
                                            ORDER BY CODIGO", filtro == string.Empty ? "NULL" : string.Format("'{0}'", filtro));
            //return sql;

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
                                                    <th style='text-align: center; width: 100%;'>Tipo de Contrato</th>
						                        </tr>
						                    </thead>
                                            <tbody>");

                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"<tr ondblclick='openTypeContractInfo({0}, {2});'>
                                            <td style='display:none;' id='id_{2}'>{0}</td>
                                            <td class='tbodyTrTd'>{1}</td>
                                        </tr>",
                                                myReader["TIPO_CONTRATOID"].ToString(),
                                                myReader["DESIGNACAO"].ToString(),
                                                conta.ToString());

                    conta++;
                }

                table.AppendFormat("</tbody></table></div>");
                table.AppendFormat("<span class='variaveis' id='countElements'>{0}</span>", conta.ToString());

                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem tipos de contrato a apresentar.</div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat("<div style='height:auto' class='panel-heading' id='panel-heading'><span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem tipos de contrato a apresentar.</div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string getTypeContractData(string id)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   DECLARE @id_tipo_contrato int = {0};

                                            SELECT 
                                                TIPO_CONTRATOID,
	                                            CODIGO,
	                                            DESIGNACAO,
	                                            NOTAS,
	                                            DATA_ULTIMA_ALTERACAO,
	                                            OP_ULTIMA_ALTERACAO,
	                                            ACESSOS_POR_SEMANA,
	                                            SABADO,
	                                            NR_AVALIACOES_FISICAS,
	                                            PT,
	                                            NUTRICAO,
	                                            COACH,
	                                            PRECO,
	                                            JOIA,
	                                            DURACAO_MINIMA_MESES,
	                                            ATIVO,
                                                RENOVACAO_AUTOMATICA,
                                                SEG,
	                                            TER,
	                                            QUA,
	                                            QUI,
	                                            SEX,
	                                            SAB,
	                                            DOM,
	                                            HORARIO_LIVRE,
	                                            TOLERANCIA_ENTRADA,
	                                            HORA_ENTRADA_SEG,
	                                            HORA_SAIDA_SEG,
	                                            HORA_ENTRADA_TER,
	                                            HORA_SAIDA_TER,
	                                            HORA_ENTRADA_QUA,
	                                            HORA_SAIDA_QUA,
	                                            HORA_ENTRADA_QUI,
	                                            HORA_SAIDA_QUI,
	                                            HORA_ENTRADA_SEX,
	                                            HORA_SAIDA_SEX,
	                                            HORA_ENTRADA_SAB,
	                                            HORA_SAIDA_SAB,
	                                            HORA_ENTRADA_DOM,
	                                            HORA_SAIDA_DOM
                                            FROM REPORT_TIPOS_CONTRATO(@id_tipo_contrato)", id);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    // Adiciona as linhas com dados
                    table.AppendFormat(@"   <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:left; margin-top: 5px; margin-bottom: 10px;'>
                                                <input id='btnSave' value='Guardar' runat='server' type='button' onclick='updateInfo();' style='background-color: #4282b5; 
                                                    width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                                <input id='btnApagar' value='Apagar' runat='server' type='button' onclick='deleteContractType();' style='background-color: #4282b5; 
                                                    width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                                            </div>
                                            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                                <img src='img/icons/icon_close.png' style='cursor:pointer;' onclick='closeCustomerInfo();'/>
                                            </div>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: 'Noto Sans', sans-serif !important; height:100%'>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Código:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Designação:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Última Modificação:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Realizada por:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Acessos por semana:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Sábado:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Nº Avaliações Físicas:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>PT:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Nutrição:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Coach:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Duração:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Preço:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Jóia:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Ativo:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Renovação Automática:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Horário Livre:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Segunda:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Terça:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Quarta:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Quinta:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Sexta:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Sábado:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Domingo:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Tolerância Entrada:</div>
                                                    <div style='height:200px; width:100%; margin-bottom: 10px'>Notas:</div>
                                                </div>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='codigo' name='codigo' placeholder='Código' style='width: 100%; margin: auto; height: 100%;' value='{1}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='designacao' name='designacao' placeholder='Designação' style='width: 100%; margin: auto; height: 100%;' value='{2}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='ultimamodificacao' name='ultimamodificacao' placeholder='Última Modificação' style='width: 100%; margin: auto; height: 100%;' value='{4}' disabled/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='realizadapor' name='realizadapor' placeholder='Realizada por' style='width: 100%; margin: auto; height: 100%;' value='{5}' disabled/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='number' class='form-control' id='acessos' name='acessos' placeholder='Acessos por semana' style='width: 100%; margin: auto; height: 100%;' value='{6}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='sabado' name='sabado' style='width: 100%; margin: auto; height: 100%;' {7}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='number' class='form-control' id='nravaliacoes' name='nravaliacoes' placeholder='Nº Avaliações Físicas' style='width: 100%; margin: auto; height: 100%;' value='{8}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='number' class='form-control' id='pt' name='pt' placeholder='PT' style='width: 100%; margin: auto; height: 100%;' value='{9}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='number' class='form-control' id='nutricao' name='nutricao' placeholder='Nutrição' style='width: 100%; margin: auto; height: 100%;' value='{10}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='coach' name='coach' style='width: 100%; margin: auto; height: 100%;' {11}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='number' class='form-control' id='duracao' name='duracao' placeholder='Duração' style='width: 100%; margin: auto; height: 100%;' value='{14}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px' class='input-group'>
                                                        <input type='number' class='form-control' id='preco' name='preco' placeholder='Preço' style='margin: auto; height: 100%;' value='{12}' aria-describedby='basic-addon-preco'/>
                                                        <span class='input-group-addon addon-euro' id='basic-addon-preco'>€</span>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px' class='input-group'>
                                                        <input type='number' class='form-control' id='joia' name='joia' placeholder='Jóia' style='margin: auto; height: 100%;' value='{13}' aria-describedby='basic-addon-joia'/>
                                                        <span class='input-group-addon addon-euro' id='basic-addon-joia'>€</span>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='ativo' name='ativo' style='width: 100%; margin: auto; height: 100%;'{15}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='renovacao' name='renovacao' style='width: 100%; margin: auto; height: 100%;' {16}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='horariolivre' name='horariolivre' style='width: 100%; margin: auto; height: 100%;' {17}/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='seg' name='seg' style='width: 33.33%; margin: auto; height: 100%; float: left' {19} />
                                                        <input data-format='hh:mm' type='text' class='form-control' value='{26}' id='horaEntradaSeg' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                                                        <input data-format='hh:mm' type='text' class='form-control' value='{27}' id='horaSaidaSeg' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='ter' name='ter' style='width: 33.33%; margin: auto; height: 100%; float: left' {20} />
                                                        <input data-format='hh:mm' type='text' class='form-control' value='{28}' id='horaEntradaTer' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                                                        <input data-format='hh:mm' type='text' class='form-control' value='{29}' id='horaSaidaTer' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='qua' name='qua' style='width: 33.33%; margin: auto; height: 100%; float: left' {21} />
                                                        <input data-format='hh:mm' type='text' class='form-control' value='{30}' id='horaEntradaQua' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                                                        <input data-format='hh:mm' type='text' class='form-control' value='{31}' id='horaSaidaQua' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='qui' name='qui' style='width: 33.33%; margin: auto; height: 100%; float: left' {22} />
                                                        <input data-format='hh:mm' type='text' class='form-control' value='{32}' id='horaEntradaQui' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                                                        <input data-format='hh:mm' type='text' class='form-control' value='{33}' id='horaSaidaQui' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='sex' name='sex' style='width: 33.33%; margin: auto; height: 100%; float: left' {23} />
                                                        <input data-format='hh:mm' type='text' class='form-control' value='{34}' id='horaEntradaSex' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                                                        <input data-format='hh:mm' type='text' class='form-control' value='{35}' id='horaSaidaSex' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='sab' name='sab' style='width: 33.33%; margin: auto; height: 100%; float: left' {24} />
                                                        <input data-format='hh:mm' type='text' class='form-control' value='{36}' id='horaEntradaSab' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                                                        <input data-format='hh:mm' type='text' class='form-control' value='{37}' id='horaSaidaSab' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='checkbox' class='form-control' id='dom' name='dom' style='width: 33.33%; margin: auto; height: 100%; float: left' {25} />
                                                        <input data-format='hh:mm' type='text' class='form-control' value='{38}' id='horaEntradaDom' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                                                        <input data-format='hh:mm' type='text' class='form-control' value='{39}' id='horaSaidaDom' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='number' class='form-control' id='tolerancia' name='tolerancia' placeholder='Tolerância Entrada' style='width: 100%; margin: auto; height: 100%;' value='{18}' />
                                                    </div>
                                                    <div style='height:200px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='notas' name='notas' style='width: 100%; margin: auto; height: 100%;' rows='5'>{3}</textarea>
                                                    </div>
                                                </div>
                                            </div>",
                                                myReader["TIPO_CONTRATOID"].ToString(),
                                                myReader["CODIGO"].ToString(),
                                                myReader["DESIGNACAO"].ToString(),
                                                myReader["NOTAS"].ToString().Trim(),
                                                myReader["DATA_ULTIMA_ALTERACAO"].ToString(),
                                                myReader["OP_ULTIMA_ALTERACAO"].ToString(),
                                                myReader["ACESSOS_POR_SEMANA"].ToString(),
                                                myReader["SABADO"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["NR_AVALIACOES_FISICAS"].ToString(),
                                                myReader["PT"].ToString(),
                                                myReader["NUTRICAO"].ToString(),
                                                myReader["COACH"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["PRECO"].ToString(),
                                                myReader["JOIA"].ToString(),
                                                myReader["DURACAO_MINIMA_MESES"].ToString(),
                                                myReader["ATIVO"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["RENOVACAO_AUTOMATICA"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["HORARIO_LIVRE"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["TOLERANCIA_ENTRADA"].ToString().Trim(),
                                                myReader["SEG"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["TER"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["QUA"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["QUI"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["SEX"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["SAB"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["DOM"].ToString().Trim() == "1" ? "checked" : "",
                                                myReader["HORA_ENTRADA_SEG"].ToString().Trim(),
                                                myReader["HORA_SAIDA_SEG"].ToString().Trim(),
                                                myReader["HORA_ENTRADA_TER"].ToString().Trim(),
                                                myReader["HORA_SAIDA_TER"].ToString().Trim(),
                                                myReader["HORA_ENTRADA_QUA"].ToString().Trim(),
                                                myReader["HORA_SAIDA_QUA"].ToString().Trim(),
                                                myReader["HORA_ENTRADA_QUI"].ToString().Trim(),
                                                myReader["HORA_SAIDA_QUI"].ToString().Trim(),
                                                myReader["HORA_ENTRADA_SEX"].ToString().Trim(),
                                                myReader["HORA_SAIDA_SEX"].ToString().Trim(),
                                                myReader["HORA_ENTRADA_SAB"].ToString().Trim(),
                                                myReader["HORA_SAIDA_SAB"].ToString().Trim(),
                                                myReader["HORA_ENTRADA_DOM"].ToString().Trim(),
                                                myReader["HORA_SAIDA_DOM"].ToString().Trim());
                }
                connection.Close();
                return table.ToString();
            }
            else
            {
                table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                                            <img src='img/icon_close.png' style='cursor:pointer;' onclick='closeCustomerInfo();'/>
                                        </div>
                                        <div style='height:auto' class='panel-heading' id='panel-heading'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existem sócios a apresentar.</span>
                                        </div>");
                connection.Close();
                return table.ToString();
            }
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"   <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:right; margin-top: 5px;'>
                                            <img src='img/iconsicon_close.png' style='cursor:pointer;' onclick='closeCustomerInfo();'/>
                                        </div>
                                        <div style='height:auto' class='panel-heading' id='panel-heading'>
                                            <span id='lblGroup' style='font-size:1.5vw;margin: auto;color:#000'>Não existe informação a apresentar para este tipo de contrato.</span>
                                        </div>");
            connection.Close();
            return table.ToString();
        }
    }

    [WebMethod]
    public static string editar(string id_operador, string id_tipo_contrato, string codigo, string designacao, string acessos, string sabado, string nravaliacoes, string pt,
        string nutricao, string coach, string duracao, string preco, string joia, string ativo, string notas, string renovacao_automatica,
        string horario_livre, string tolerancia, string seg, string ter, string qua, string qui, string sex, string sab, string dom,
        string hora_ent_seg, string hora_sai_seg, string hora_ent_ter, string hora_sai_ter, string hora_ent_qua, string hora_sai_qua, string hora_ent_qui, string hora_sai_qui,
        string hora_ent_sex, string hora_sai_sex, string hora_ent_sab, string hora_sai_sab, string hora_ent_dom, string hora_sai_dom)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @id_tipo_contrato int = {1};
	                                        DECLARE @codigo char(30) = '{2}';
	                                        DECLARE @designacao varchar(100) = '{3}';
	                                        DECLARE @notas varchar(max) = '{4}';
	                                        DECLARE @acessos_por_semana int = {5};
	                                        DECLARE @sabado bit = {6};
	                                        DECLARE @nr_avaliacoes_fisicas int = {7};
	                                        DECLARE @pt int = {8};
	                                        DECLARE @nutricao bit = {9};
	                                        DECLARE @coach bit = {10};
	                                        DECLARE @preco decimal(15,2) = {11};
	                                        DECLARE @joia decimal(15,2) = {12};
	                                        DECLARE @duracao_minima_meses int = {13};
	                                        DECLARE @ativo bit = {14};
                                            DECLARE @renovacao_automatica bit = {15};
                                            DECLARE @horario_livre bit = {16};
                                            DECLARE @tolerancia_entrada int = {17};
                                            DECLARE @seg bit = {18};
                                            DECLARE @ter bit = {19};
                                            DECLARE @qua bit = {20};
                                            DECLARE @qui bit = {21};
                                            DECLARE @sex bit = {22};
                                            DECLARE @sab bit = {23};
                                            DECLARE @dom bit = {24};
                                            DECLARE @hora_entrada_seg time = {25};
                                            DECLARE @hora_saida_seg time = {26};
                                            DECLARE @hora_entrada_ter time = {27};
                                            DECLARE @hora_saida_ter time = {28};
                                            DECLARE @hora_entrada_qua time = {29};
                                            DECLARE @hora_saida_qua time = {30};
                                            DECLARE @hora_entrada_qui time = {31};
                                            DECLARE @hora_saida_qui time = {32};
                                            DECLARE @hora_entrada_sex time = {33};
                                            DECLARE @hora_saida_sex time = {34};
                                            DECLARE @hora_entrada_sab time = {35};
                                            DECLARE @hora_saida_sab time = {36};
                                            DECLARE @hora_entrada_dom time = {37};
                                            DECLARE @hora_saida_dom time = {38};
                                            DECLARE @res int;

                                            EXEC ALTERA_TIPO_CONTRATO @id_tipo_contrato, @id_operador, @codigo, @designacao, @notas, @acessos_por_semana, @sabado,
                                                @nr_avaliacoes_fisicas, @pt, @nutricao, @coach, @preco, @joia, @duracao_minima_meses, @ativo, @renovacao_automatica,
                                                @horario_livre, @tolerancia_entrada, @seg, @ter, @qua, @qui, @sex, @sab, @dom, @hora_entrada_seg, @hora_saida_seg, 
                                                @hora_entrada_ter, @hora_saida_ter, @hora_entrada_qua, @hora_saida_qua, @hora_entrada_qui, @hora_saida_qui, 
                                                @hora_entrada_sex, @hora_saida_sex, @hora_entrada_sab, @hora_saida_sab, @hora_entrada_dom, @hora_saida_dom, 
                                                @res output", id_operador, id_tipo_contrato, codigo, designacao, notas, acessos, sabado, nravaliacoes,
                                                            pt, nutricao, coach, preco, joia, duracao, ativo, renovacao_automatica, horario_livre, 
                                                            tolerancia == "" ? "0" : tolerancia,
                                                            seg, ter, qua, qui, sex, sab, dom,
                                                            hora_ent_seg == "" ? "NULL" : "'" + hora_ent_seg + "'", hora_sai_seg == "" ? "NULL" : "'" + hora_ent_seg + "'",
                                                            hora_ent_ter == "" ? "NULL" : "'" + hora_ent_ter + "'", hora_sai_ter == "" ? "NULL" : "'" + hora_sai_ter + "'",
                                                            hora_ent_qua == "" ? "NULL" : "'" + hora_ent_qua + "'", hora_sai_qua == "" ? "NULL" : "'" + hora_sai_qua + "'",
                                                            hora_ent_qui == "" ? "NULL" : "'" + hora_ent_qui + "'", hora_sai_qui == "" ? "NULL" : "'" + hora_sai_qui + "'",
                                                            hora_ent_sex == "" ? "NULL" : "'" + hora_ent_sex + "'", hora_sai_sex == "" ? "NULL" : "'" + hora_sai_sex + "'",
                                                            hora_ent_sab == "" ? "NULL" : "'" + hora_ent_sab + "'", hora_sai_sab == "" ? "NULL" : "'" + hora_sai_sab + "'",
                                                            hora_ent_dom == "" ? "NULL" : "'" + hora_ent_dom + "'", hora_sai_dom == "" ? "NULL" : "'" + hora_sai_dom + "'");
            //return sql;
            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"Tipo de Contrato '{0}' atualizado com sucesso!", codigo);
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao atualizar tipo de contrato '{0}'!", codigo);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao atualizar tipo de contrato '{0}'!", codigo);
        connection.Close();
        return table.ToString();
    }

    [WebMethod]
    public static string criar(string id_operador, string codigo, string designacao, string acessos, string sabado, string nravaliacoes, string pt,
        string nutricao, string coach, string duracao, string preco, string joia, string ativo, string notas, string renovacao_automatica,
        string horario_livre, string tolerancia, string seg, string ter, string qua, string qui, string sex, string sab, string dom,
        string hora_ent_seg, string hora_sai_seg, string hora_ent_ter, string hora_sai_ter, 
        string hora_ent_qua, string hora_sai_qua, string hora_ent_qui, string hora_sai_qui,
        string hora_ent_sex, string hora_sai_sex, string hora_ent_sab, string hora_sai_sab, string hora_ent_dom, string hora_sai_dom)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @codigo char(30) = '{1}';
	                                        DECLARE @designacao varchar(100) = '{2}';
	                                        DECLARE @notas varchar(max) = '{3}';
	                                        DECLARE @acessos_por_semana int = {4};
	                                        DECLARE @sabado bit = {5};
	                                        DECLARE @nr_avaliacoes_fisicas int = {6};
	                                        DECLARE @pt int = {7};
	                                        DECLARE @nutricao bit = {8};
	                                        DECLARE @coach bit = {9};
	                                        DECLARE @preco decimal(15,2) = {10};
	                                        DECLARE @joia decimal(15,2) = {11};
	                                        DECLARE @duracao_minima_meses int = {12};
	                                        DECLARE @ativo bit = {13};
                                            DECLARE @renovacao_automatica bit = {14};
                                            DECLARE @horario_livre bit = {15};
                                            DECLARE @tolerancia_entrada int = {16};
                                            DECLARE @seg bit = {17};
                                            DECLARE @ter bit = {18};
                                            DECLARE @qua bit = {19};
                                            DECLARE @qui bit = {20};
                                            DECLARE @sex bit = {21};
                                            DECLARE @sab bit = {22};
                                            DECLARE @dom bit = {23};
                                            DECLARE @hora_entrada_seg time = {24};
                                            DECLARE @hora_saida_seg time = {25};
                                            DECLARE @hora_entrada_ter time = {26};
                                            DECLARE @hora_saida_ter time = {27};
                                            DECLARE @hora_entrada_qua time = {28};
                                            DECLARE @hora_saida_qua time = {29};
                                            DECLARE @hora_entrada_qui time = {30};
                                            DECLARE @hora_saida_qui time = {31};
                                            DECLARE @hora_entrada_sex time = {32};
                                            DECLARE @hora_saida_sex time = {33};
                                            DECLARE @hora_entrada_sab time = {34};
                                            DECLARE @hora_saida_sab time = {35};
                                            DECLARE @hora_entrada_dom time = {36};
                                            DECLARE @hora_saida_dom time = {37};
                                            DECLARE @res int;

                                            EXEC CRIA_TIPO_CONTRATO @id_operador, @codigo, @designacao, @notas, @acessos_por_semana, @sabado,
                                                @nr_avaliacoes_fisicas, @pt, @nutricao, @coach, @preco, @joia, @duracao_minima_meses, @ativo, @renovacao_automatica,
                                                @horario_livre, @tolerancia_entrada, @seg, @ter, @qua, @qui, @sex, @sab, @dom, @hora_entrada_seg, @hora_saida_seg, 
                                                @hora_entrada_ter, @hora_saida_ter, @hora_entrada_qua, @hora_saida_qua, @hora_entrada_qui, @hora_saida_qui, 
                                                @hora_entrada_sex, @hora_saida_sex, @hora_entrada_sab, @hora_saida_sab, @hora_entrada_dom, @hora_saida_dom,
                                                @res output",
                                                id_operador, codigo, designacao, notas, acessos, sabado, nravaliacoes,
                                                pt, nutricao, coach, preco, joia, duracao, ativo, renovacao_automatica, horario_livre,
                                                            tolerancia == "" ? "0" : tolerancia,
                                                            seg, ter, qua, qui, sex, sab, dom,
                                                            hora_ent_seg == "" ? "NULL" : "'" + hora_ent_seg + "'", hora_sai_seg == "" ? "NULL" : "'" + hora_ent_seg + "'",
                                                            hora_ent_ter == "" ? "NULL" : "'" + hora_ent_ter + "'", hora_sai_ter == "" ? "NULL" : "'" + hora_sai_ter + "'",
                                                            hora_ent_qua == "" ? "NULL" : "'" + hora_ent_qua + "'", hora_sai_qua == "" ? "NULL" : "'" + hora_sai_qua + "'",
                                                            hora_ent_qui == "" ? "NULL" : "'" + hora_ent_qui + "'", hora_sai_qui == "" ? "NULL" : "'" + hora_sai_qui + "'",
                                                            hora_ent_sex == "" ? "NULL" : "'" + hora_ent_sex + "'", hora_sai_sex == "" ? "NULL" : "'" + hora_sai_sex + "'",
                                                            hora_ent_sab == "" ? "NULL" : "'" + hora_ent_sab + "'", hora_sai_sab == "" ? "NULL" : "'" + hora_sai_sab + "'",
                                                            hora_ent_dom == "" ? "NULL" : "'" + hora_ent_dom + "'", hora_sai_dom == "" ? "NULL" : "'" + hora_sai_dom + "'");
            //return sql;
            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"Tipo de Contrato '{0}' criado com sucesso!", codigo);
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao criar tipo de contrato '{0}'!", codigo);
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao criar tipo de contrato '{0}'!", codigo);
        connection.Close();
        return table.ToString();
    }



    [WebMethod]
    public static string apagar(string id_operador, string id_tipo_contrato)
    {
        var table = new StringBuilder();
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        SqlConnection connection = new SqlConnection(cs);

        try
        {
            connection.Open();

            string sql = string.Format(@"   SET DATEFORMAT dmy;
                                            DECLARE @id_operador int = {0};
	                                        DECLARE @id_tipo_contrato int = {1};
                                            DECLARE @res int;

                                            EXEC APAGA_TIPO_CONTRATO @id_tipo_contrato, @id_operador, @res output",
                                                id_operador, id_tipo_contrato);

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);

            table.AppendFormat(@"Tipo de Contrato apagado com sucesso!");
            connection.Close();
            return table.ToString();
        }
        catch (Exception exc)
        {
            table.AppendFormat(@"Erro ao apagar Tipo de Contrato!");
            connection.Close();
            return table.ToString();
        }

        table.AppendFormat(@"Erro ao apagar Tipo de Contrato!");
        connection.Close();
        return table.ToString();
    }
}
