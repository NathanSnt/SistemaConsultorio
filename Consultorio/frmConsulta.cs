﻿using System;
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
    public partial class frmConsulta : Form
    {
        //Criando variáveis para controle do menu
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);

        public frmConsulta()
        {
            InitializeComponent();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            //this.Close();
            frmMenuPrincipal voltar = new frmMenuPrincipal();
            voltar.Show();
            this.Hide();
        }

        private void frmConsulta_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);
        }

        private void btnTestarConexao_Click(object sender, EventArgs e)
        {

            MySqlConnection conn = new MySqlConnection();

            conn.ConnectionString = "Server=localhost;Port=3306;Database=db_usuarios;Uid=root;Pwd='';";

            conn.Open();
            MessageBox.Show("Banco de dados conectado!");

            conn.Close();

        }
    }
}
