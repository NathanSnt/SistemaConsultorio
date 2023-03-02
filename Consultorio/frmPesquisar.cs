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
            txtDescricao.Clear();
            ltbResultado.Items.Clear();
            txtDescricao.Focus();
        }

        private void rdbNome_CheckedChanged(object sender, EventArgs e)
        {
            txtDescricao.Enabled = true;
            txtDescricao.Clear();
            ltbResultado.Items.Clear();
            txtDescricao.Focus();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtDescricao.Clear();
            ltbResultado.Items.Clear();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (rdbCodigo.Checked)
            {
                if(txtDescricao.Text == "")
                {
                    MessageBox.Show("Informe um valor a ser pesquisado", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDescricao.Focus();
                }
                else
                {
                    pesquisaPorCodigo(txtDescricao.Text);
                }
            }
            else if (rdbNome.Checked)
            {
                if (txtDescricao.Text == "")
                {
                    MessageBox.Show("Informe um valor a ser pesquisado", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDescricao.Focus();
                }
                else
                {
                    pesquisaPorNome(txtDescricao.Text);
                }
            }
            else
            {
                MessageBox.Show("Escolha uma das duas opções de pesquisa", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ltbResultado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ltbResultado.Items.Count > 0)
            {
                string valor = ltbResultado.SelectedItem.ToString();
                frmPacientes abrir = new frmPacientes(valor);
                abrir.Show();
                this.Hide();
            }

        }

        public void pesquisaPorNome(string nome)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = $"select * from tb_pacientes where nome_pac like '%{nome}%';";
            comm.CommandType = CommandType.Text;
            comm.Connection = Conexao.obterConexao();

            MySqlDataReader DR;
            DR = comm.ExecuteReader();
            ltbResultado.Items.Clear();
            while (DR.Read())
            {
                ltbResultado.Items.Add(DR.GetString(1));
            }

            Conexao.fecharConexao();
        }

        public void pesquisaPorCodigo(string codigo)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = $"select * from tb_pacientes where cod_pac like '%{codigo}%';";
            comm.CommandType = CommandType.Text;
            comm.Connection = Conexao.obterConexao();

            MySqlDataReader DR;
            DR = comm.ExecuteReader();
            DR.Read();

            ltbResultado.Items.Clear();

            try
            {
                ltbResultado.Items.Add(DR.GetString(1));
            }
            catch (MySqlException)
            {
                MessageBox.Show("Nenhum dado encontrado.", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDescricao.Clear();
                txtDescricao.Focus();
            }

            Conexao.fecharConexao();
        }
    }
}
