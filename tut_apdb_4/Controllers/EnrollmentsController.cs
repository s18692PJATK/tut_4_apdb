using System;

using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tut_apdb_4.Models.Entrollments;
using tut_apdb4.Models;

namespace tut_apdb_4.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    /*"procedure1",con) { CommandType = System.Data.CommandType.StoredProcedure}*/
    public class EnrollmentsController : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult AddStudent([FromBody] Models.Entrollments.Student student)
        {
          
            if (!student.IsValid()) return BadRequest("cant get student");
            string studies = student.Studies;
            using(var con = new SqlConnection("Data Source=db-mssql16;Initial Catalog=s18692;Integrated Security=True"))
            using (var com = new SqlCommand())

            {
                con.Open();
                var transaction  = con.BeginTransaction();
                try
                {

                    com.Connection = con;

                    com.CommandText = "SELECT idStudy " +
                        "FROM Studies " +
                        "WHERE Name = @name;";
                    com.Parameters.AddWithValue("name", studies);
                    var response = com.ExecuteReader();
                    if (!response.Read())
                        return BadRequest("there is no study of that name");
                    int id = (int)response["idStudy"];
                    response.Close();
                    com.CommandText = "SELECT IdEnrollment " +
                        "FROM ENROLLMENT E " +
                        "WHERE E.IdStudy = @id " +
                        "AND Semester = 1;";
                    com.Parameters.AddWithValue("id", id);
                    response = com.ExecuteReader();
                    int idEnrollment = -1;
                    if (response.Read())
                        idEnrollment = (int)response["IdEnrollment"];
                    response.Close();
                    var enrollment = new Enrollment();
                    if (idEnrollment == -1)
                    {
                        com.CommandText = "SELECT MAX(idEnrollment) " +
                            "from enrollment;";
                        idEnrollment = (int)com.ExecuteScalar() + 1;
                        enrollment.Id = idEnrollment;
                        enrollment.IdStudy = id;
                        enrollment.Semester = 1;
                        enrollment.StartDate = DateTime.Now;
                        com.CommandText = "INSERT INTO ENROLLMENT(IdEnrollment,Semester,IdStudy,StartDate) VALUES (@idenrol,@sem,@s,@date);";
                        com.Parameters.AddWithValue("idenrol", enrollment.Id);
                        com.Parameters.AddWithValue("sem", enrollment.Semester);
                        com.Parameters.AddWithValue("s", enrollment.IdStudy);
                        com.Parameters.AddWithValue("date", enrollment.StartDate.ToString());
                        com.ExecuteNonQuery();
                    }
                    else
                    {
                        DateTime date = DateTime.Now;
                        com.CommandText = "SELECT StartDate " +
                            "FROM ENROLLMENT " +
                            "WHERE idEnrollment = @idenrol;";
                        com.Parameters.AddWithValue("idenrol", idEnrollment);
                        response = com.ExecuteReader();
                        if (response.Read())
                            date = (DateTime)response["StartDate"];
                        enrollment.Id = idEnrollment;
                        enrollment.Semester = 1;
                        enrollment.StartDate = date;
                        enrollment.IdStudy = id;
                        response.Close();
                    }

                    com.CommandText = "SELECT 1" +
                        "FROM STUDENT " +
                        "WHERE IndexNumber = @index;";
                    com.Parameters.AddWithValue("index", student.IndexNumber);
                    response = com.ExecuteReader();
                    if (response.Read()) return BadRequest("student with given index already exists");
                    com.CommandText = "INSERT INTO STUDENT(IndexNumber,FirstName,LastName,BirthDate,IdEnrollment) VALUES (@index,@first,@last,@date,@idenrol);";
                    com.Parameters.AddWithValue("index", student.IndexNumber);
                    com.Parameters.AddWithValue("first", student.FirstName);
                    com.Parameters.AddWithValue("last", student.LastName);
                    com.Parameters.AddWithValue("date", student.BirthDate);
                    com.Parameters.AddWithValue("idenrol", enrollment.Id);
                    transaction.Commit();
                    con.Close();
                    return CreatedAtAction(nameof(AddStudent), enrollment);
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                    return BadRequest();
                }

                



            }


        }

        [HttpPost ("promote")]
        public IActionResult promote(PromoteRequest request)
        {
            using (var con = new SqlConnection("Data Source=db-mssql16;Initial Catalog=s18692;Integrated Security=True"))
                
            {
                con.Open();
                using (var com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "Select * " +
                        "from enrollment e " +
                        "join studies s " +
                        "on s.idstudy = e.idstudy " +
                        "where s.name = @name " +
                        "and e.semester = @semester";
                    com.Parameters.AddWithValue("name", request.Studies);
                    com.Parameters.AddWithValue("semester", request.Semester);
                    var response = com.ExecuteReader();
                    if (!response.Read())
                    { 
                        response.Close();
                        return NotFound();
                    }
                    response.Close();

                }
                using (var com = new SqlCommand("PromoteProcedure", con) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    com.Parameters.AddWithValue("Studies", request.Studies);
                    com.Parameters.AddWithValue("semester", request.Semester);
                    com.ExecuteNonQuery();

                    var com1 = new SqlCommand();
                    com1.Connection = con;
                    com1.CommandText = "Select e.IdEnrollment, e.Semester, e.IdStudy, e.StartDate " +
                        "from enrollment e " +
                        "join studies s on e.idstudy = s.idstudy " +
                        "where e.semester = @sem " +
                        "and s.name = @name";
                    com1.Parameters.AddWithValue("sem", request.Semester);
                    com1.Parameters.AddWithValue("name", request.Studies);
                    var response = com.ExecuteReader();
                    var enrollment = new Enrollment();
                    if (response.Read())
                    {
                        enrollment.Id = (int)response["IdEnrollment"];
                        enrollment.Semester = (int)response["Semester"];
                        enrollment.IdStudy = (int)response["IdStudy"];
                        enrollment.StartDate = (DateTime)(response["StartDate"]);
                    }
                    return CreatedAtAction(nameof(promote), enrollment);


                }
                
            }
        }

      
    }
}