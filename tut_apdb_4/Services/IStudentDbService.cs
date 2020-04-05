using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tut_apdb_4.Models.Entrollments;

namespace tut_apdb_4.Services
{
    public interface IStudentDbService

    {
        public Enrollment promote(PromoteRequest request);
        public Enrollment AddStudent( Student student);
        public List<Student> GetStudents(string orderBy);
        public List<string> getSemesters(string id);
    }
}
