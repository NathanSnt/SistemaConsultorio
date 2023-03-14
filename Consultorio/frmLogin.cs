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
using MySql.Data.MySqlClient;

namespace Consultorio
{
    public partial class frmLogin : Form
    {
        //Criando variáveis para controle do menu
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);

        public frmLogin()
        {
            InitializeComponent();
        }

        public bool acessarSistema(string usuario, string senha)
        {
            MySqlCommand comm = new MySqlCommand
            {
                CommandText = $"select * from tb_usuarios where nome_usu='{usuario}' and senha_usu='{senha}';",
                CommandType = CommandType.Text,
                Connection = Conexao.obterConexao()
            };
            
            MySqlDataReader DR = comm.ExecuteReader();
            bool resultado = DR.HasRows;
            Conexao.fecharConexao();
            
            return resultado;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string senha = txtSenha.Text;
            
            if (acessarSistema(usuario, senha))
            {
                //Hide();
                //using (frmMenuPrincipal menu = new frmMenuPrincipal())
                //    menu.ShowDialog();
                //Show();
                frmMenuPrincipal menu = new frmMenuPrincipal();
                menu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                txtUsuario.Clear();
                txtSenha.Clear();
                txtUsuario.Focus();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Close();
            //this.Close();
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);

        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSenha.Focus();
            }
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEntrar.Focus();
            }
        }
    }
}
