using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ModelPortal.Db_Connection
{
    public class DBConnection
    {
        public static string GetConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    }
}