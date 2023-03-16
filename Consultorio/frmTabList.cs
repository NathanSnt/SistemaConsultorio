using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Consultorio
{
    public partial class frmTabList : Form
    {
        public frmTabList()
        {
            InitializeComponent();
        }

        private void mskCPF_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void mskCEP_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        private void btnCadastrar_Click_1(object sender, EventArgs e)
        {
            if (txtCodigo.Equals("") || txtNome.Equals(""))
            {
                dgvRegistros.Rows.Add(txtCodigo.Text, txtNome.Text, chkAnuncios.Checked);
                txtCodigo.Clear();
                txtNome.Clear();
                chkAnuncios.Checked = false;
            }
            else
            {
                MessageBox.Show("Preencha todos os campos!", "Mensagem do sistema");
            }
                txtCodigo.Focus();
        }

        private void btnLimpar_Click_1(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtNome.Clear();
            chkAnuncios.Checked = false;
            txtCodigo.Focus();
        }
    }
}
