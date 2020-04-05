using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tut_apdb_4.Models.Entrollments;

namespace tut_apdb_4.Services
{
    public class StudentDbService : IStudentDbService
    {
        public IActionResult AddStudent([FromBody] Student student)
        {
            
        }

        public IActionResult getSemesters(string id)
        {
            throw new NotImplementedException();
        }

        public IActionResult GetStudents(string orderBy)
        {
            throw new NotImplementedException();
        }

        public IActionResult promote(PromoteRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
