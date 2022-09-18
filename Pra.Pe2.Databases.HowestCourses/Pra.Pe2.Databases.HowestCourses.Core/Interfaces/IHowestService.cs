using Pra.Pe2.Databases.HowestCourses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pra.Pe2.Databases.HowestCourses.Core.Interfaces
{
    public interface IHowestService
    {
        List<Lecturer> GetLecturers();
        List<Curriculum> GetSemesters(string curriculumname);

        List<FieldOfStudy> GetPrograms();

        List<Courses> GetCourses(string fieldOfStudy, int semester);

        bool AddLecturer(Lecturer lecturer);

        bool UpdateLecturer(Lecturer lecturer);

        bool DeleteLecturer(Lecturer lecturer);

        









    }
}
