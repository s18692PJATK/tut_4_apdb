using System;

using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tut_apdb_4.Models.Entrollments;
using tut_apdb_4.Services;
using tut_apdb4.Models;

namespace tut_apdb_4.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    /*"procedure1",con) { CommandType = System.Data.CommandType.StoredProcedure}*/
    public class EnrollmentsController : ControllerBase
    {
        IStudentDbService service;

        public EnrollmentsController(IStudentDbService service)
        {
            this.service = service;
        }
        [HttpPost("add")]
        public IActionResult AddStudent([FromBody] Models.Entrollments.Student student)
        {

            if (!student.IsValid()) return BadRequest("cant get student");
            var enrollment = service.AddStudent(student);
            if (enrollment == null) return BadRequest();
            return CreatedAtAction(nameof(AddStudent), enrollment);
        }


    

    [HttpPost("promote")]
    public IActionResult promote(PromoteRequest request)
    {
            var enrollment = service.promote(request);
            if (enrollment == null) return NotFound();
            return CreatedAtAction(nameof(promote), enrollment);


    }
}
}