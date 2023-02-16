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
//using Correios.Net; // NÃO ESTÁ SENDO UTILIZADO
using ViaCep;

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
            txtNome.Focus();
        }

        public void verificarCampo()
        {
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
                || mskTelefone.Text.Equals("(  )      -"))
            {
                MessageBox.Show("Favor inserir valores!",
                    "Mensagem do Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                txtNome.Focus();
            }
            else
            {
                MessageBox.Show("Cadastrado com Sucesso!",
                    "Mensagem do Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                desabilitarCampos();
                limparCampos();
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
            mskTelefone.Clear();
            txtEndereco.Clear();
            cbbEstados.Text = "";
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal voltar = new frmMenuPrincipal();
            voltar.Show();
            this.Hide();
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
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmPesquisar abrir = new frmPesquisar();
            abrir.ShowDialog();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            verificarCampo();
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
    }
}
