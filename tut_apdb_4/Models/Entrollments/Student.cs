using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tut_apdb_4.Models.Entrollments
{
    public class Student
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Studies { get; set; }

        public bool IsValid()
        {
            if (String.IsNullOrEmpty(IndexNumber) ||
               String.IsNullOrEmpty(FirstName) ||
               String.IsNullOrEmpty(LastName) ||
               String.IsNullOrEmpty(BirthDate) ||
               String.IsNullOrEmpty(Studies))
                return false;
            else return true;
        }
    }
}
