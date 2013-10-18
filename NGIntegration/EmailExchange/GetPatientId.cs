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
            string SQL = string.Format(@"SELECT person_id FROM person WHERE first_name like '%{0}%'
                                        AND last_name LIKE '%{1}%' AND date_of_birth = {2}", FirstName, LastName, DateOfBirth);
            DatabaseClient DBClient = new DatabaseClient(InstanceMgr.ProcessInstance);

            using (SqlConnection SQLConn = new SqlConnection(DBClient.ConnectionString))
            {
                SqlCommand SQLCmd = new SqlCommand(SQL, SQLConn);
                SQLCmd.CommandType = CommandType.Text;

                try
                {
                    SQLConn.Open();
                    return SQLCmd.ExecuteScalar().ToString();
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
