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
    public class Curriculum
    {
        [ExplicitKey]
        public string ID { get; }
        
        public int Semester { get; set; }

        public Curriculum(int semester)
        {
            Semester = semester;
        }

        public Curriculum(string id)
        {
            ID = id;
        }
        public override string ToString()
        {
            return $"Semester {Semester}";
        }
    }
}
