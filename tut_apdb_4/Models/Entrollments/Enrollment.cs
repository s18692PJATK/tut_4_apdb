using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tut_apdb_4.Models.Entrollments
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int IdStudy { get; set; }
        public int Semester { get; set; }
        public DateTime StartDate { get; set; }
    }


}
