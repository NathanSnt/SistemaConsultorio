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
//using MySql.Data.MySqlClient;

namespace Consultorio
{
    public partial class frmAgenda : Form
    {
        //Criando variáveis para controle do menu
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);

        public frmAgenda()
        {
            InitializeComponent();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal voltar = new frmMenuPrincipal();
            voltar.Show();
            this.Hide();
        }

        private void frmAgenda_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);
        }

        private void btnTestar_Click(object sender, EventArgs e)
        {
            /*
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = "Server=localhost;port=3306; uid=senac; pwd='@jqEuO6(4jkKg@pk'; database=db_usuarios";
            try
            {
                conn.Open();
                MessageBox.Show("Banco de dados acessado com sucesso!", "Mensagem do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
            catch (MySqlException)
            {
                MessageBox.Show("Erro ao acessar o banco de dados!", "Mensagem do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            conn.Close();
            */
            MessageBox.Show("Conexão com o banco encerrada com sucesso!", "Mensagem do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            Conexao.obterConexao();
            MessageBox.Show("Banco de dados acessado com sucesso!", "Mensagem do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            Conexao.fecharConexao();
            MessageBox.Show("Conexão com o banco encerrada com sucesso!", "Mensagem do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
    }
}
