using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Consultorio
{
    public partial class frmPesquisar : Form
    {
        public frmPesquisar()
        {
            InitializeComponent();
            txtDescricao.Enabled = false;
            rdbCodigo.Checked = false;
            rdbNome.Checked = false;
        }

        private void rdbCodigo_CheckedChanged(object sender, EventArgs e)
        {
            txtDescricao.Enabled = true;
            txtDescricao.Focus();
        }

        private void rdbNome_CheckedChanged(object sender, EventArgs e)
        {
            txtDescricao.Enabled = true;
            txtDescricao.Focus();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtDescricao.Clear();
            txtDescricao.Enabled = false;
            ltbResultado.Items.Clear();
            rdbNome.Checked = false;
            rdbCodigo.Checked = false;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //ltbResultado.Items.Clear();
            if (rdbCodigo.Checked)
            {
                ltbResultado.Items.Add(txtDescricao.Text);
            }
            else if (rdbNome.Checked)
            {
                ltbResultado.Items.Add(txtDescricao.Text);
            }
        }

        private void ltbResultado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ltbResultado.Items.Count > 0)
            {
                int indice = ltbResultado.SelectedIndex;
                string valor = ltbResultado.SelectedItem.ToString();
                MessageBox.Show($"Índice selecionado: {indice}\nValor: {valor}");
                Console.WriteLine($"Valor: {valor} - Índice: {indice}");
            }
        }
    }
}
