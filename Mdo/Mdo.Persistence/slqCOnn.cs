using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdo.Persistence
{
    public class slqCOnn
    {
        private string conStr = @"Server=(localdb)\v11.0;Integrated Security=true;";
        public slqCOnn()
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
