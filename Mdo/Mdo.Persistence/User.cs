using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdo.Persistence
{
    public class User
    {
        private string conStr = @"Server=(localdb)\v11.0;Integrated Security=true;";
        public User()
        {
            using (var conn = new SqlConnection(conStr))
            using (var command = new SqlCommand("ProcedureName", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
