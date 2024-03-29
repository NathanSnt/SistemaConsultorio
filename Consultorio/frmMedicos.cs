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

namespace Consultorio
{
    public partial class frmMedicos : Form
    {
        //Criando variáveis para controle do menu
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);

        public frmMedicos()
        {
            InitializeComponent();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal voltar = new frmMenuPrincipal();
            voltar.Show();
            this.Hide();
        }

        private void frmMedicos_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);
            carregaCombo();
        }
        //Criando método para carregar a combobox especialidades
        public void carregaCombo()
        {
            cbbEspecialidade.Items.Add("Clínico Geral");
            cbbEspecialidade.Items.Add("Cardiologista");
            cbbEspecialidade.Items.Add("Anestesiologista");
            cbbEspecialidade.Items.Add("Cirurgião Cardiovascular");
            cbbEspecialidade.Items.Add("Cirurgião de Mão");
            cbbEspecialidade.Items.Add("Cirurgião de Cabeça e Pescoço");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmEspecialidade abrir = new frmEspecialidade();
            abrir.ShowDialog();
        }
    }
}
