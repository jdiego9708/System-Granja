using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SISGranja.API.Services
{
    public class Connection
    {
        public static string Cn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
    }
}