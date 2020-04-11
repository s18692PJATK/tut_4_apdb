using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tut_apdb_4.Services
{
   public interface IStudentNumberService
    {
        public bool exist(string index);
    }
}
