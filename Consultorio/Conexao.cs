﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Consultorio
{
    class Conexao
    {
        //private static string connString = "Server=10.23.49.43;port=3306; uid=senac; pwd='123456'; database=db_clinica_medica"; // Acessando o banco em outra máquina.
        private static string connString = "Server=localhost;port=3306; uid=senac; pwd='@jqEuO6(4jkKg@pk'; database=db_clinica_medica";
        private static MySqlConnection conn = null;

        public static MySqlConnection obterConexao()
        {
            conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
            }
            catch (MySqlException)
            {
                conn = null;
            }
            return conn;
        }

        public static void fecharConexao()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }

        public static bool testarConexao()
        {
            conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
                conn.Close();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }
    }
}
