

using System.Data.SqlClient;

namespace tut_apdb_4.Services
{
    public class StudentNumberService : IStudentNumberService
    {
        public bool exist(string index)
        {
            using (var con = new SqlConnection("Data Source=db-mssql16;Initial Catalog=s18692;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "select count(1) " +
                "from student " +
                "where IndexNumber = @index";
                com.Parameters.AddWithValue("index", index);
                var count = int.Parse(com.ExecuteScalar().ToString());
                return count > 0;
            }
        }

    }
}
