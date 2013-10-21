using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NextGen.Data;
using NextGen.Core;
using System.Data;
using System.Data.SqlClient;

namespace SyncingShip
{
    public class GetPatientId
    {
        public string GetPatient(string FirstName, string LastName, string DateOfBirth)
        {
            var sql = string.Format(@"SELECT person_id FROM person WHERE first_name like '%{0}%'
                                        AND last_name LIKE '%{1}%' AND date_of_birth = {2}", FirstName, LastName, DateOfBirth);
            var dbClient = new DatabaseClient(InstanceMgr.ProcessInstance);

            using (var sqlConn = new SqlConnection(dbClient.ConnectionString))
            {
                var sqlCmd = new SqlCommand(sql, sqlConn) {CommandType = CommandType.Text};

                try
                {
                    sqlConn.Open();
                    return sqlCmd.ExecuteScalar().ToString();
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
