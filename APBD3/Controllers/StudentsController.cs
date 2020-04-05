using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using APBD3.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD3.Controllers
{
    [ApiController]
    [Route("api/students")]

    public class StudentsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetStudent(string orderBy)
        {
            var listOfStudents = new List<Student>();
            using (var connection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True"))
            {
                using (var com = new SqlCommand())
                {

                    com.Connection = connection;
                    com.CommandText = @"select s.FirstName, s.LastName, s.BirthDate, st.Name as Studies, e.Semester
                                            from Student s
                                            join Enrollment e on e.IdEnrollment = s.IdEnrollment
                                            join Studies st on st.IdStudy = e.IdStudy;";

                    connection.Open();
                    var dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        var st = new Student();
                        st.FirstName = dr["FirstName"].ToString();
                        st.LastName = dr["LastName"].ToString();
                        st.DateOfBirth = DateTime.Parse(dr["BirthDate"].ToString());
                        st.Semester = int.Parse(dr["Semester"].ToString());
                        st.Studies = dr["Studies"].ToString();
                        listOfStudents.Add(st);
                    }
                }
            }
            return Ok(listOfStudents);
        }
        [HttpGet("{id}")]
        public IActionResult GetSemester(string id)
        {
            var listOfEnrolment = new List<Enrollment>();
            


            using (var connection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True"))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT * FROM Enrollment e
                                            join Student s on s.IdEnrollment = e.IdEnrollment
                                            WHERE s.IndexNumber=@id;";
                    command.Parameters.AddWithValue("id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var enrollment = new Enrollment
                        {
                            IdEnrollment = int.Parse(reader["IdEnrollment"].ToString()),
                            IdStudy = int.Parse(reader["IdStudy"].ToString()),
                            Semester = int.Parse(reader["Semester"].ToString()),
                            StartDate = DateTime.Parse(reader["StartDate"].ToString()),
                        };
                        listOfEnrolment.Add(enrollment);
                    }

                    return Ok(listOfEnrolment);

                }
            }
        }
    }
}   