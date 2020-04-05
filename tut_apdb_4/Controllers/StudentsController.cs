using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using tut_apdb_4.Services;
using tut_apdb4.Models;

namespace tut_apdb4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        IStudentDbService service;

        public StudentsController(IStudentDbService service)
        {
            this.service = service;
        }


        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {

            var students = service.GetStudents(orderBy);
            return Ok(students);
        }

        [HttpGet("semesters/{id}")]
        public IActionResult getSemesters(string id)
        {
            var semesters = service.getSemesters(id);         
            return Ok(semesters);
        }




       

      
    }
}
