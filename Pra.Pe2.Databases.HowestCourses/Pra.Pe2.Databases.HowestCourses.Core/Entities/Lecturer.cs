using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Dapper;

namespace Pra.Pe2.Databases.HowestCourses.Core.Entities
{
    [Table ("Lecturers")]
    public class Lecturer
    {
        [ExplicitKey]
        public string ID { get;  }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public Lecturer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            ID = Guid.NewGuid().ToString();
        }

        public Lecturer(string id, string firstName, string lastName) : this (firstName, lastName)
        {
            ID = id;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }


    }

    
}
