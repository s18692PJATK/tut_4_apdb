using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using tut_apdb4.Models;

namespace tut_apdb4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        

        public StudentsController()
        {
            
        }
        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {

            var students = new List<Student>();
            using (var con = new SqlConnection("Data Source=db-mssql16;Initial Catalog=s18692;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                 com.CommandText = "select s.IndexNumber, s.FirstName, s.LastName, s.BirthDate, s.IdEnrollment, e.Semester " +
                   "from Student s " +
                 "join Enrollment e on e.IdEnrollment = s.IdEnrollment " +
                 "join Studies st on st.IdStudy = e.IdStudy;";
                con.Open();
                
                var response = com.ExecuteReader();
                while (response.Read())
                {
                    Console.WriteLine(response.FieldCount);
                  
                    var st = new Student
                    {
               
                        IndexNumber = response["IndexNumber"].ToString(),
                        FirstName = response["FirstName"].ToString(),
                        LastName = response["LastName"].ToString(),
                        BirthDate = DateTime.Parse(response["BirthDate"].ToString()),
                        IdEnrollment = response["IdEnrollment"].ToString()
                    };
                    students.Add(st);
                }




            }
            return Ok(students);
        }

        [HttpGet("semesters/{id}")]
        public IActionResult getSemesters(string id)
        {
            List<string> semesters = new List<string>();
            using (var con = new SqlConnection("Data Source=db-mssql16;Initial Catalog=s18692;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select e.semester " +
                    "from Enrollment e " +
                    "join Student s on e.IdEnrollment = s.IdEnrollment " +
                    "where " +
                    "s.IndexNumber = @id;";
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = id;
                com.Parameters.Add(param);
                con.Open();
                var response = com.ExecuteReader();
                while (response.Read())
                    semesters.Add(response["Semester"].ToString());

                }

            return Ok(semesters);
        }




       

      
    }
}
