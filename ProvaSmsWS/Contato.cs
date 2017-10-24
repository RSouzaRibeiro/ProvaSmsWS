using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProvaSmsWS
{
    public class Contato
    {
        public int id { get; set; }
        public int idUsuario{ get; set;}
        public string nomeContato { get; set; }
        public string telContato { get; set; }
        
    }
}