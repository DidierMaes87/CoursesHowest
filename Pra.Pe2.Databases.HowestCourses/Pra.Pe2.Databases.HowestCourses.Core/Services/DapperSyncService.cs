using Pra.Pe2.Databases.HowestCourses.Core.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Pra.Pe2.Databases.HowestCourses.Core.Entities;


namespace Pra.Pe2.Databases.HowestCourses.Core.Services
{
    public class DapperSyncService : IHowestService
    {
        public List<Lecturer> GetLecturers()
        {
            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    List<Lecturer> lecturers = connection.GetAll<Lecturer>().ToList();
                    lecturers = lecturers.OrderBy(p => p.FirstName).ToList();
                    return lecturers;
                }
                catch
                {
                    return null;
                }
            }
        }


        public bool AddLecturer(Lecturer lecturer)
        {
            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    connection.Insert(lecturer);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }

        public bool UpdateLecturer(Lecturer lecturer)
        {
            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    connection.Update(lecturer);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }

        public bool DeleteLecturer(Lecturer lecturer)
        {


            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    connection.Delete(lecturer);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }


        public List<Curriculum> GetSemesters(string fieldOfStudy)
        {
            List<Curriculum> semesters;
            string sql = "SELECT c.Semester FROM Curriculum c WHERE c.Program = '" + fieldOfStudy + "'";
            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    semesters = connection.Query<Curriculum>(sql).OrderBy(p => p.Semester).ToList();
                }
                catch
                {
                    return null;
                }
                return semesters;
            }

        }

        public List<FieldOfStudy> GetPrograms()
        {
            List<FieldOfStudy> programs;
            string sql = "Select DISTINCT c.Program from Curriculum c";
            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    programs = connection.Query<FieldOfStudy>(sql).ToList();
                }
                catch
                {
                    return null;
                }
                return programs;
            }

        }

        public List<Courses> GetCourses(string fieldOfStudy, int semester)
        {
            List<Courses> courses;
            string sql = "SELECT * FROM Courses c JOIN Curriculum cu ON c.CurriculumID = cu.ID WHERE Semester = " + semester + " AND Program ='" + fieldOfStudy + "'";
            using (SqlConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    courses = connection.Query<Courses>(sql).ToList();
                }
                catch
                {
                    return null;
                }
                return courses;
            }
        }

    }


}





