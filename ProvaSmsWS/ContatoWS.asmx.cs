using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace ProvaSmsWS
{
    /// <summary>
    /// Summary description for ContatoWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ContatoWS : System.Web.Services.WebService

    {

        private string stringConexao = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;        
        public SqlConnection Conn = null;       
        public SqlCommand sqlComand = null;

        private string query;

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public string HelloWorld()
        {

            List<string> list = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                list.Add("teste:" + i);
            }

            

            return JsonConvert.SerializeObject(list);
                
        }

        
        public bool TestConexao()
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

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public bool InsertContato(String TOKEN, String ID_USUARIO, String NOME_CONTATO, String TEL_CONTATO)
        {
            if (TOKEN.Equals("1234567890"))
            {
                query = "INSERT INTO CONTATO (ID_USUARIO,NOME_CONTATO,TEL_CONTATO)" +
                               "VALUES(@ID_USUARIO,@NOME_CONTATO,@TEL_CONTATO)";



                if (TestConexao())
                {
                    try
                    {
                        sqlComand = new SqlCommand(query, Conn);
                        sqlComand.Parameters.AddWithValue("@ID_USUARIO", Convert.ToInt32(ID_USUARIO));
                        sqlComand.Parameters.AddWithValue("@NOME_CONTATO", NOME_CONTATO);
                        sqlComand.Parameters.AddWithValue("@TEL_CONTATO", TEL_CONTATO);

                        sqlComand.ExecuteNonQuery();

                        return true;
                    }
                    catch (Exception sqlerr)
                    {
                        return false;
                        throw sqlerr;
                    }
                    finally
                    {
                        Conn.Close();
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

           

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
         public string ListarContatosPorUsuario(string TOKEN, int ID_USUARIO)
         {
            if(TOKEN.Equals("1234567890"))
            {
                List<Contato> listContatos = new List<Contato>();
                query = "SELECT ID, ID_USUARIO,NOME_CONTATO, TEL_CONTATO FROM CONTATO WHERE ID_USUARIO = @ID_USUARIO";

                SqlDataReader dr;
                if (TestConexao())
                {
                    try
                    {
                        sqlComand = new SqlCommand(query, Conn);
                        sqlComand.Parameters.AddWithValue("@ID_USUARIO", Convert.ToInt32(ID_USUARIO));

                        dr = sqlComand.ExecuteReader();

                        while (dr.Read())
                        {
                            Contato contato = new Contato();

                            contato.id = Convert.ToInt32(dr[0].ToString());
                            contato.idUsuario = Convert.ToInt32(dr[1].ToString());
                            contato.nomeContato = dr[2].ToString();
                            contato.telContato = dr[3].ToString();

                            listContatos.Add(contato);

                        }


                        return JsonConvert.SerializeObject(listContatos);



                        //return js.Serialize(listContatos);


                    }
                    catch (Exception sqlerr)
                    {

                        throw sqlerr;
                    }
                    finally
                    {
                        Conn.Close();
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return "Token Invalido";
            }
             
         }

         /*public bool DeleteContato(int idContato)
         {

         }


         public bool AlterarContato(Contato contato)
         {

         }*/
           
        }


       
    }


