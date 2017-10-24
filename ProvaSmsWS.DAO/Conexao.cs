using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ProvaSmsWS.DAO
{
    public class Conexao
    {

        private string stringConexao = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
      
        public SqlConnection Conn = null;




        public bool Conectar()
        {
            Conn = new SqlConnection(stringConexao);
            try
            {
                Conn.Open();
                return true;
            }
            catch (Exception)
            {

                return false;

            }
        }

        public bool Desconectar()
        {
            if(Conn.State != ConnectionState.Closed){
                Conn.Close();
                Conn.Dispose();
                return true;

            }
            else{
                Conn.Dispose();
                return false;
            }
        }
    }
}