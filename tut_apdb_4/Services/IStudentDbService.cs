using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tut_apdb_4.Models.Entrollments;

namespace tut_apdb_4.Services
{
    interface IStudentDbService

    {
        public IActionResult promote(PromoteRequest request);
        public IActionResult AddStudent([FromBody] Models.Entrollments.Student student);
        public IActionResult GetStudents(string orderBy);
        public IActionResult getSemesters(string id);
    }
}
