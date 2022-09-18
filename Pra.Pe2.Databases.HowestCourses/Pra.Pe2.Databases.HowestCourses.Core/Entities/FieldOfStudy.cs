using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Pra.Pe2.Databases.HowestCourses.Core.Entities
{
    [Table("Curriculum")]
    public class FieldOfStudy
    {
        [ExplicitKey]    
        public string ID { get; }

        public string Program { get; set; }

       public FieldOfStudy()
        {

        }

        public FieldOfStudy(string program)
        {
            Program = program;
        }
        public override string ToString()
        {
            return Program;
        }
        
    }
}
