using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
//using Correios.Net; // NÃO ESTÁ SENDO UTILIZADO
//using ViaCep;

namespace Consultorio
{
    public partial class frmPacientes : Form
    {
        //Criando variáveis para controle do menu
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);

        public frmPacientes()
        {
            InitializeComponent();
            desabilitarCampos();
            carregarComboBox();
            testarConexaoBanco();
        }

        // Criando construtor com parâmetros
        public frmPacientes(string nome)
        {
            InitializeComponent();
            desabilitarCampos();
            carregarComboBox();
            testarConexaoBanco();
            preencheCampos(nome);
        }

        public void preencheCampos(string nome)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = $"select * from tb_pacientes where nome_pac like '%{nome}%';";
            comm.CommandType = CommandType.Text;
            comm.Connection = Conexao.obterConexao();

            MySqlDataReader DR;
            DR = comm.ExecuteReader();
            DR.Read();

            mskCodigo.Text = DR.GetInt32(0).ToString();
            txtNome.Text = DR.GetString(1);
            txtEmail.Text = DR.GetString(2);
            mskTelefone.Text = DR.GetString(3);
            mskCPF.Text = DR.GetString(4);
            txtEndereco.Text = DR.GetString(5);
            txtNumero.Text = DR.GetString(6);
            mskCEP.Text = DR.GetString(7);
            txtBairro.Text = DR.GetString(8);
            txtCidade.Text = DR.GetString(9);
            txtComplemento.Text = DR.GetString(10);
            cbbEstados.Text = DR.GetString(11);

            Conexao.fecharConexao();
        }

        public void testarConexaoBanco()
        {
            bool bancoOnline = Conexao.testarConexao();
            if (bancoOnline)
            {
                lblEstadoDoBanco.Text = "Online";
                lblEstadoDoBanco.ForeColor = Color.Green;
            }
            else
            {
                lblEstadoDoBanco.Text = "Offline";
                lblEstadoDoBanco.ForeColor = Color.Red;
            }
        }

        public void carregarComboBox()
        {
            cbbEstados.Items.Add("Acre (AC)");
            cbbEstados.Items.Add("Alagoas (AL)");
            cbbEstados.Items.Add("Amapá (AP)");
            cbbEstados.Items.Add("Amazonas (AM)");
            cbbEstados.Items.Add("Bahia (BA)");
            cbbEstados.Items.Add("Ceará (CE)");
            cbbEstados.Items.Add("Distrito Federal (DF)");
            cbbEstados.Items.Add("Espírito Santo (ES)");
            cbbEstados.Items.Add("Goiás (GO)");
            cbbEstados.Items.Add("Maranhão (MA)");
            cbbEstados.Items.Add("Mato Grosso (MT)");
            cbbEstados.Items.Add("Mato Grosso do Sul (MS)");
            cbbEstados.Items.Add("Minas Gerais (MG)");
            cbbEstados.Items.Add("Pará (PA)");
            cbbEstados.Items.Add("Paraíba (PB)");
            cbbEstados.Items.Add("Paraná (PR)");
            cbbEstados.Items.Add("Pernambuco (PE)");
            cbbEstados.Items.Add("Piauí (PI)");
            cbbEstados.Items.Add("Rio de Janeiro (RJ)");
            cbbEstados.Items.Add("Rio Grande do Norte (RN)");
            cbbEstados.Items.Add("Rio Grande do Sul (RS)");
            cbbEstados.Items.Add("Rondônia (RO)");
            cbbEstados.Items.Add("Roraima (RR)");
            cbbEstados.Items.Add("Santa Catarina (SC)");
            cbbEstados.Items.Add("São Paulo (SP)");
            cbbEstados.Items.Add("Sergipe (SE)");
            cbbEstados.Items.Add("Tocantins (TO)");
        }

        public void desabilitarCampos()
        {
            mskCPF.Enabled = false;
            mskCEP.Enabled = false;
            txtNome.Enabled = false;
            txtEmail.Enabled = false;
            mskCodigo.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            btnLimpar.Enabled = false;
            cbbEstados.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            mskTelefone.Enabled = false;
            txtEndereco.Enabled = false;
            btnCadastrar.Enabled = false;
            txtComplemento.Enabled = false;
            txtNumero.Enabled = false;
            btnNovo.Enabled = true;
        }

        public void habilitarCampos()
        {
            mskCPF.Enabled = true;
            mskCEP.Enabled = true;
            txtNome.Enabled = true;
            txtEmail.Enabled = true;
            btnLimpar.Enabled = true;
            txtBairro.Enabled = true;
            txtCidade.Enabled = true;
            cbbEstados.Enabled = true;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            txtEndereco.Enabled = true;
            btnCadastrar.Enabled = true;
            mskTelefone.Enabled = true;
            txtComplemento.Enabled = true;
            txtNumero.Enabled = true;
            txtNome.Focus();
        }

        public bool verificarCampo()
        {
            bool cpfValido = validaCPF(mskCPF.Text);
            bool emailValido = validaEmail(txtEmail.Text);
            
            if (txtNome.Text.Equals("")
                || cbbEstados.Text == ""
                || txtEmail.Text.Equals("")
                || mskCPF.Text.Contains(" ")
                || mskCEP.Text.Contains(" ")
                || txtBairro.Text.Equals("")
                || txtCidade.Text.Equals("")
                || txtEndereco.Text.Equals("")
                || mskCEP.Text.Equals("     -")
                || mskTelefone.Text.Contains("  ")
                || mskCPF.Text.Equals("   .   .   -")
                || mskTelefone.Text.Equals("(  )      -")
                || cpfValido
                || emailValido)
            {
                MessageBox.Show("Favor inserir valores corretamente!",
                    "Mensagem do Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);

                txtNome.Focus();
                return false;
            }
            else
            {
                //MessageBox.Show("Cadastrado com Sucesso!",
                //    "Mensagem do Sistema",
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Information,
                //    MessageBoxDefaultButton.Button1);
                desabilitarCampos();
                //limparCampos();
                return true;
            }
        }

        public void limparCampos()
        {
            mskCEP.Clear();
            mskCPF.Clear();
            txtNome.Clear();
            txtEmail.Clear();
            mskCodigo.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            txtNumero.Clear();
            mskTelefone.Clear();
            txtEndereco.Clear();
            cbbEstados.Text = "";
            txtComplemento.Clear();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            if (!btnNovo.Enabled)
            {
                DialogResult res = MessageBox.Show("Todas as informações digitadas serão perdidas, continuar?", "Mensagem do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (res == DialogResult.Yes)
                {
                    limparCampos();
                    desabilitarCampos();
                }
            }
            else
            {
                frmMenuPrincipal voltar = new frmMenuPrincipal();
                voltar.Show();
                this.Hide();
            }
        }

        private void frmPacientes_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
            txtNome.Focus();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            habilitarCampos();
            btnNovo.Enabled = false;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmPesquisar abrir = new frmPesquisar();
            abrir.ShowDialog();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //if (validaCPF(mskCPF.Text) &&
            //validaEmail(txtEmail.Text))
            verificarCampo();

            // Executar o método de cadastrar paciente
            cadastrarPaciente();
        }

        public void cadastrarPaciente()
        {
            MySqlCommand comm = new MySqlCommand();

            comm.CommandText = "insert into tb_pacientes (nome_pac, email_pac, telefone_pac, cpf_pac, endereco_pac, numero_pac, cep_pac, complemento_pac, bairro_pac, cidade_pac, uf_pac) " +
                "values (@nome, @email, @telefone, @cpf, @endereco, @numero, @cep, @complemento, @bairro, @cidade, @uf);";
            comm.CommandType = CommandType.Text;
            comm.Parameters.Clear();
            comm.Parameters.Add("@nome", MySqlDbType.VarChar, 100).Value = txtNome.Text;
            comm.Parameters.Add("@email", MySqlDbType.VarChar, 100).Value = txtEmail.Text;
            comm.Parameters.Add("@telefone", MySqlDbType.VarChar, 14).Value = mskTelefone.Text;
            comm.Parameters.Add("@cpf", MySqlDbType.VarChar, 14).Value = mskCPF.Text;
            comm.Parameters.Add("@endereco", MySqlDbType.VarChar, 100).Value = txtEndereco.Text;
            comm.Parameters.Add("@numero", MySqlDbType.VarChar, 10).Value = txtNumero.Text;
            comm.Parameters.Add("@cep", MySqlDbType.VarChar, 8).Value = mskCEP.Text;
            comm.Parameters.Add("@complemento", MySqlDbType.VarChar, 50).Value = txtComplemento.Text;
            comm.Parameters.Add("@bairro", MySqlDbType.VarChar, 50).Value = txtBairro.Text;
            comm.Parameters.Add("@cidade", MySqlDbType.VarChar, 50).Value = txtCidade.Text;
            comm.Parameters.Add("@uf", MySqlDbType.VarChar, 2).Value = cbbEstados.Text;

            comm.Connection = Conexao.obterConexao();

            int res = comm.ExecuteNonQuery();
            MessageBox.Show("Valores inseridos com sucesso!" + res, "Mensagem do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Conexao.fecharConexao();
            limparCampos();
        }

        private void btnCarregaEndereco_Click(object sender, EventArgs e)
        {

            
            // NÃO FUNCIONA
            //Address endereco = SearchZip.GetAddress("04037000", 1000);
            //txtEndereco.Text = endereco.Street;
            //txtBairro.Text = endereco.District;
            //txtCidade.Text = endereco.City;
            //cbbEstados.Text = endereco.State;

            // NÃO FUNCIONA
            //SearchZip endereco = new SearchZip();
            //mskCEP.Text = endereco.ToString();
            //Address address = new Address();
            //txtEndereco.Text = address.Street;
            //txtBairro.Text = address.District;
            //txtCidade.Text = address.City;
            //cbbEstados.Items.Add(address.State);

            // FUNCIONA
            //ViaCepClient endereco = new ViaCepClient();
            //ViaCepResult resultado = endereco.Search(mskCEP.Text);
            //txtEndereco.Text = resultado.Street;
            //txtCidade.Text = resultado.City;
            //txtBairro.Text = resultado.Neighborhood;
            //cbbEstados.Text = resultado.StateInitials;
        }

        public void buscaCEP(string cep)
        {
            WSCorreios.AtendeClienteClient ws = new WSCorreios.AtendeClienteClient();

            try
            {
                WSCorreios.enderecoERP end = ws.consultaCEP(cep);
                txtEndereco.Text = end.end;
                txtBairro.Text = end.bairro;
                txtCidade.Text = end.cidade;
                cbbEstados.Text = end.uf;
                txtComplemento.Text = end.complemento2;
            }
            catch (Exception)
            {
                MessageBox.Show("Insira um CEP válido!",
                    "Mensagem do Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                mskCEP.Clear();
                mskCEP.Focus();
            }
        }

        private void mskCEP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                buscaCEP(mskCEP.Text);
                txtNumero.Focus();
            }
        }

        public static bool validaEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                /*
                 * O que esperava de cada trecho:
                 * [a-z0-9.]+ - parte antes do @ do e-mail, nome do usuário;
                 * @ - caractere de arroba obrigatório;
                 * [a-z0-9]+ - parte depois do @ do e-mail, nome do provedor;
                 * \. - caractere de ponto depois do nome do provedor;
                 * [a-z]+ - geralmente onde é colocado o .com;
                 * \. - caractere de ponto depois do .com, só deveria ser obrigatório caso haja por exemplo um .br ou a abreviação de qualquer outro país no final do e-mail;
                 * ([a-z]+)? - geralmente onde é colocado a abreviação do país.
                 */
                email = Regex.Replace(email, @"(@)(.+)$", 
                    DomainMapper, RegexOptions.None, 
                    TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    string domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase,
                    TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool validaCPF (string cpf)
        {
            string valor = cpf.Replace(".", "");
            valor = valor.Replace("-", "");
            
            if (valor.Length != 11)
                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != 11 - resultado)
                return false;
            return true;
        }

        private void mskCPF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                bool valida = validaCPF(mskCPF.Text);

                if (valida == true)
                {
                    mskCEP.Focus();
                }
                else
                {
                    MessageBox.Show("Insira um CPF válido!",
                    "Mensagem do Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                    mskCPF.Clear();
                    mskCPF.Focus();
                }
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                bool valida = validaEmail(txtEmail.Text);

                if (valida == true)
                {
                    mskTelefone.Focus();
                }
                else
                {
                    MessageBox.Show("Insira um E-mail válido!",
                    "Mensagem do Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                    txtEmail.Clear();
                    txtEmail.Focus();
                }
            }
        }
    }
}
