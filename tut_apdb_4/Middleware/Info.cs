using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tut_apdb_4.Middleware
{
    public class Info
    {
        public string Method { get; set; }
        public string Endpoint { get; set; }
        public string Body { get; set; }

        public string QueryString { get; set; }

       override public string ToString()
        {
            return "Method = " + Method + "\n" +
                    "Endpoint= " + Endpoint + "\n" +
                    "Body= " + Body + "\n" +
                    "QueryString= " + QueryString + "\n";
        }
    }
}
