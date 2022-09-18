using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pra.Pe2.Databases.HowestCourses.Core.Services
{
    class Helper
    {
        public static string GetConnectionString()
        {
            return @"data source=LAPTOP-2I795PCN;initial catalog=Howest;trusted_connection=true;";
        }

        public static string HandleQuotes(string value)
        {
            return value.Trim().Replace("'", "''");
        }
    }
}
