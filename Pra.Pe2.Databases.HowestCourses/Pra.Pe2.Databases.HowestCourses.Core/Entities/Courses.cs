using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Pra.Pe2.Databases.HowestCourses.Core.Entities
{
    [Table("Courses")]
   public class Courses
    {
        [ExplicitKey]
        public string ID { get;  }

        public string Name { get; }
        
        public string CoordinatorID {get;}

        public string CurriculumID { get; }

       

        public Courses(string name, string coordinatorId, string curriculumId)
        {
            
            Name = name;
            CoordinatorID = coordinatorId;
            CurriculumID = curriculumId;
        }

        public Courses()
        {
            
        }

        public override string ToString()
        {
            return Name;
        }


    }
}
