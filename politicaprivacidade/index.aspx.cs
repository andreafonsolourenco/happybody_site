using System;
using System.Activities.Statements;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : Page
{
    string separador = "";
    string lingua = "";

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        separador = HttpContext.Current.Request.Url.PathAndQuery;

        try
        {
            lingua = Request.QueryString["language"];
        }
        catch (Exception exc)
        {
            lingua = "";
        }

        if (!IsPostBack)
        {
            ClientScriptManager oCsm = this.Page.ClientScript;
            if (!oCsm.IsStartupScriptRegistered(GetType(), "consentimento"))
            {

            }

            language.InnerHtml = lingua;
            getPage();
        }
    }

    private void getPage()
    {
        var table = new StringBuilder();
        var language = new StringBuilder();
        string connectionstring = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        string titulo = "", politicaprivacidade = "", subtitulo = "", link = "";
        string title11 = "", title12 = "", title13 = "", title14 = "", title15 = "", title16 = "", title17 = "";
        string text11 = "", text12 = "", text13 = "", text14 = "", text15 = "", text16 = "", text17 = "";

        try
        {
            string sql = string.Format(@"   DECLARE @titulo varchar(max);
                                            DECLARE @politicaprivacidade varchar(max);
                                            DECLARE @subtitulo varchar(max);
                                            DECLARE @link varchar(max);
                                            DECLARE @title11 varchar(max);
                                            DECLARE @title12 varchar(max);
                                            DECLARE @title13 varchar(max);
                                            DECLARE @title14 varchar(max);
                                            DECLARE @title15 varchar(max);
                                            DECLARE @title16 varchar(max);
                                            DECLARE @title17 varchar(max);
                                            DECLARE @text11 varchar(max);
                                            DECLARE @text12 varchar(max);
                                            DECLARE @text13 varchar(max);
                                            DECLARE @text14 varchar(max);
                                            DECLARE @text15 varchar(max);
                                            DECLARE @text16 varchar(max);
                                            DECLARE @text17 varchar(max);
                                            DECLARE @traducao varchar(max);
                                            DECLARE @lingua varchar(10) = '{0}';

                                            set @traducao = 'Política de Privacidade';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @politicaprivacidade output;

                                            set @traducao = 'WebSite Happy Body - Personal Trainer';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @titulo output;

                                            set @traducao = 'No âmbito da utilização do website https://www.happybody.site/ poderão ser recolhidos dados pessoais dos utilizadores.<br />O Happy Body preocupa-se com a sua privacidade e pretende assegurar que são adotadas todas as medidas necessárias para que os seus dados pessoais sejam tratados de forma segura e de acordo com a legislação aplicável relativo à proteção de dados pessoais e em conformidade com o aqui exposto.'
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @subtitulo output;

                                            set @traducao = 'Para preencher o formulário de consentimento, por favor clique aqui!';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @link output;

                                            set @traducao = '1.1. Responsável pelo Tratamento e Encarregado de Proteção de Dados';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @title11 output;
                        
                                            set @traducao = '1.2. Finalidades do Tratamento';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @title12 output;
            
                                            set @traducao = '1.3. Destinatários dos dados pessoais';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @title13 output;

                                            set @traducao = '1.4. Fundamento jurídico para o tratamento dos dados pessoais';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @title14 output;

                                            set @traducao = '1.5. Durante quanto tempo conservamos os seus dados pessoais?';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @title15 output;

                                            set @traducao = '1.6. Direitos dos titulares dos dados pessoais';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @title16 output;

                                            set @traducao = '1.7. Atualização desta Política de Privacidade';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @text17 output;

                                            set @traducao = 'Identificação: Happy Body<br />Morada: online<br />Telefone: +41 774468932<br />E-mail: happybodyfitcoach@gmail.com<br />Contactos do Encarregado de Proteção de Dados: +41 774468932';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @text11 output;
                        
                                            set @traducao = 'Os seus dados pessoais serão tratados com as seguintes finalidades:<br />1.2.1. Gestão do Website;<br />1.2.2. Envio de comunicações relativas à promoção de produtos e serviços do Happy Body;<br />1.2.3. Permitir o acesso a áreas restritas do Website.';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @text12 output;
            
                                            set @traducao = 'O Happy Body poderá contratar prestadores de serviços externos, que atuam como subcontratantes, para lhe prestar serviços em diferentes áreas (consultar AIDP existente).<br />Neste sentido, o Happy Body segue critérios rigorosos na seleção de prestadores de serviços, a fim de cumprir com as suas obrigações de proteção de dados, comprometendo-se a subscrever com os mesmos um acordo de tratamento de dados, que inclui, entre outras, as seguintes obrigações:<br />- aplicar medidas técnicas e organizacionais adequadas;<br />- tratar os dados pessoais para os fins acordados e respondendo exclusivamente às instruções documentadas do Happy Body e apagar ou devolver os dados pessoais após solicitação do responsável dos dados.';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @text13 output;

                                            set @traducao = 'O Happy Body trata os dados pessoais recolhidos com os seguintes fundamentos jurídicos:<br />1. No caso da finalidade identificada no ponto 1.2.1. o fundamento jurídico para o tratamento dos seus dados pessoais é o interesse legítimo do Happy Body em manter o conteúdo do seu Website constantemente atualizado por forma a prestar-lhe informações úteis e de qualidade;<br />2. No caso da finalidade identificada no ponto 1.2.2. o fundamento jurídico para o tratamento é o seu consentimento, podendo o mesmo ser retirado a qualquer momento, sem que tal, no entanto, torne ilegítimo o tratamento de dados pessoais realizado com base nesse consentimento até à data em que o mesmo seja retirado. O não fornecimento do seu consentimento irá impedir o envio de comunicações relativas à promoção de produtos e serviços relativos ao Happy Body;<br />3. No caso da finalidade identificada no ponto 1.2.3. o fundamento jurídico para o tratamento dos seus dados pessoais é a execução contratual.';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @text14 output;

                                            set @traducao = 'O período pelo qual conservamos os seus dados pessoais poderá variar consoante a finalidade para a qual foram recolhidos e são tratados. Assim, iremos conservar os seus dados pessoais durante os seguintes prazos:<br />1. No caso das finalidades identificadas nos pontos 1.2.1. e 1.2.3.: até ao momento em que os dados pessoais já não sejam necessários para o cumprimento desta finalidade;<br />2. No caso da finalidade identificada no ponto 1.2.2.: até que retire o seu consentimento;<br />Sem prejuízo dos prazos identificados, os seus dados pessoais serão também conservados durante o tempo necessário para dar cumprimento às obrigações legais que em cada caso sejam aplicáveis.<br />No caso dos dados pessoais serem utilizados para várias finalidades, que nos obriguem a conservá-los durante prazos diferentes, aplicaremos o prazo mais longo.';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @text15 output;

                                            set @traducao = 'De acordo com a legislação de proteção de dados aplicável, tem direito de:<br />1. Solicitar o acesso aos seus dados pessoais: Tem o direito de obter confirmação sobre se quaisquer dados pessoais que lhe digam respeito estão, ou não, a ser tratados e, se for esse o caso, solicitar acesso aos seus dados pessoais. As informações de acesso incluem – entre outras coisas – as finalidades do tratamento, as categorias de dados pessoais em questão e as categorias de destinatários ou os destinatários a quem os seus dados pessoais foram ou serão divulgados. Pode ter o direito de obter uma cópia dos dados pessoais que estão a ser objeto de tratamento.<br />2. Solicitar a retificação dos seus dados pessoais: tem o direito de obter a retificação das inexatidões relativas aos seus dados pessoais. Tendo em conta as finalidades do tratamento, tem o direito a que os seus dados pessoais incompletos sejam completados, incluindo por meio de uma declaração adicional.<br />3. Solicitar a limitação do tratamento dos seus dados pessoais: em determinadas circunstâncias, pode ter o direito de obter a limitação do tratamento dos seus dados pessoais. Nesse caso, os respetivos dados serão marcados e só podem ser tratados por nós com o seu consentimento ou para determinados fins.<br />4. Solicitar a portabilidade dos dados: em determinadas circunstâncias, pode ter o direito de receber os dados pessoais que nos forneceu, num formato estruturado, de uso corrente e de leitura automática e pode ter o direito a transmitir esses dados para outra entidade sem que o possamos impedir.<br />5. Opor-se ao tratamento dos seus dados pessoais: tem o direito de, por motivos relacionados com a sua situação particular, a qualquer momento, se opor ao tratamento dos seus dados pessoais. Nesse caso, o Happy Body irá cessar o tratamento dos seus dados pessoais, a não ser que se verifiquem razões imperiosas e legítimas para esse tratamento que prevaleçam sobre os seus interesses, direitos e liberdades, ou para efeitos de declaração, exercício ou defesa de um direito num processo judicial. Também tem o direito de apresentar uma queixa junto da autoridade competente de supervisão da proteção de dados.<br />Para exercer os direitos referidos, ou retirar os consentimentos prestados, o Utilizador pode contactar o Happy Body através do envio e um e-mail para o contacto happybodyfitcoach@gmail.com, indicando como referência Proteção de Dados e fornecendo um comprovativo da sua identidade.';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @text16 output;

                                            set @traducao = 'A presente Política de Privacidade teve a sua última atualização em Janeiro de 2023 e está atualizada de acordo com o Regulamento Geral sobre a Proteção de Dados (RGPD) e demais legislação aplicável.';
                                            EXEC DEVOLVE_TRADUCAO @traducao, @lingua, @text17 output;

                                            SELECT 
                                                @politicaprivacidade as politicaprivacidade,
                                                @titulo as titulo,
                                                @subtitulo as subtitulo,
                                                @link as link,
                                                @title11 as title11,
                                                @title12 as title12,
                                                @title13 as title13,
                                                @title14 as title14,
                                                @title15 as title15,
                                                @title16 as title16,
                                                @title17 as title17,
                                                @text11 as text11,
                                                @text12 as text12,
                                                @text13 as text13,
                                                @text14 as text14,
                                                @text15 as text15,
                                                @text16 as text16,
                                                @text17 as text17", lingua);

            command.CommandText = sql.ToString();
            da.SelectCommand = command;
            DataSet oDs = new DataSet();

            connection.Open();
            da.Fill(oDs);
            connection.Close();

            if (oDs.Tables != null && oDs.Tables.Count > 0 && oDs.Tables[0].Rows.Count > 0)
            {
                int count = oDs.Tables.Count - 1;

                for (int i = 0; i < oDs.Tables[count].Rows.Count; i++)
                {
                    titulo = oDs.Tables[count].Rows[i]["titulo"].ToString();
                    politicaprivacidade = oDs.Tables[count].Rows[i]["politicaprivacidade"].ToString();
                    subtitulo = oDs.Tables[count].Rows[i]["subtitulo"].ToString();
                    link = oDs.Tables[count].Rows[i]["link"].ToString();

                    title11 = oDs.Tables[count].Rows[i]["title11"].ToString();
                    title12 = oDs.Tables[count].Rows[i]["title12"].ToString();
                    title13 = oDs.Tables[count].Rows[i]["title13"].ToString();
                    title14 = oDs.Tables[count].Rows[i]["title14"].ToString();
                    title15 = oDs.Tables[count].Rows[i]["title15"].ToString();
                    title16 = oDs.Tables[count].Rows[i]["title16"].ToString();
                    title17 = oDs.Tables[count].Rows[i]["title17"].ToString();

                    text11 = oDs.Tables[count].Rows[i]["text11"].ToString();
                    text12 = oDs.Tables[count].Rows[i]["text12"].ToString();
                    text13 = oDs.Tables[count].Rows[i]["text13"].ToString();
                    text14 = oDs.Tables[count].Rows[i]["text14"].ToString();
                    text15 = oDs.Tables[count].Rows[i]["text15"].ToString();
                    text16 = oDs.Tables[count].Rows[i]["text16"].ToString();
                    text17 = oDs.Tables[count].Rows[i]["text17"].ToString();
                }
            }
        }
        catch (Exception exc)
        {

        }

        table.AppendFormat(@"   <div class='row text-center' style='margin-bottom: 20px !important;'>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>
                                        <img src='../general/img/logo.png' style='width:auto; height: 75px; margin-top: 5px;'/>
                                    </div>
                                </div>
                                <div class='row text-center' style='margin-bottom: 20px !important;'>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'><h3>{14}<br />{15}</h3></div>
                                </div>
                                <div class='row text-center' style='margin-bottom: 20px !important; font-weight: bold;'>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>
                                        <h4>{17}</h4>
                                    </div>
                                </div>
                                <div class='row' style='margin-bottom: 20px !important;'>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:justify;'>
                                        <span style='font-weight: bold;'>{0}<br /></span>
                                    </div>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:justify;'>
                                        {1}
                                    </div>
                                </div>
                                <div class='row' style='margin-bottom: 20px !important;'>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:justify;'>
                                        <span style='font-weight: bold;'>{2}<br /></span>
                                    </div>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:justify;'>
                                        {3}
                                    </div>
                                </div>
                                <div class='row' style='margin-bottom: 20px !important;'>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:justify;'>
                                        <span style='font-weight: bold;'>{4}<br /></span>
                                    </div>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:justify;'>
                                        {5}
                                    </div>
                                </div>
                                <div class='row' style='margin-bottom: 20px !important;'>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:justify;'>
                                        <span style='font-weight: bold;'>{6}<br /></span>
                                    </div>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:justify;'>
                                        {7}
                                    </div>
                                </div>
                                <div class='row' style='margin-bottom: 20px !important;'>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:justify;'>
                                        <span style='font-weight: bold;'>{8}<br /></span>
                                    </div>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:justify;'>
                                        {9}
                                    </div>
                                </div>
                                <div class='row' style='margin-bottom: 20px !important;'>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:justify;'>
                                        <span style='font-weight: bold;'>{10}<br /></span>
                                    </div>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:justify;'>
                                        {11}
                                    </div>
                                </div>
                                <div class='row' style='margin-bottom: 20px !important;'>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:justify;'>
                                        <span style='font-weight: bold;'>{12}<br /></span>
                                    </div>
                                    <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:justify;'>
                                        {13}
                                    </div>
                                </div>
                                <div class='row text-center' style='padding-top: 10px;'>
                                    <div class='form-group col-12 col-md-12 col-xl-12 col-sm-12 col-lg-12 text-center' style='vertical-align: middle !important;'>
                                        <button class='btn btn-primary btn-xl text-uppercase' style='max-width: 100% !important; margin: auto !important;' onclick='loadConsentPage();' runat='server' id='btnPT'>{16}</button>
                                    </div>
                                </div>", title11, text11, title12, text12, title13, text13, title14, text14, title15, text15, title16, text16, title17, text17, 
                                    politicaprivacidade, titulo, link, subtitulo);

        content.InnerHtml = table.ToString();
    }
}