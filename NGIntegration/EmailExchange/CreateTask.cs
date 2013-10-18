using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using NextGen.Core;
using NextGen.Data;

namespace SyncingShip
{
    public class CreateTask
    {
        public void Create()
        {
            Guid _guid = Guid.NewGuid();
            //Hardcoded values could be broken out eventually
            string _storeProcedure = string.Format(@"EXEC ng_add_task ['00001'], ['0001'], [{0}], ['AA'], ['AA'], ['1'], 
                                                     'N', ['1DE71BF7-669C-4839-9FB8-5681D5EBA81A'], 
                                                     ['-99'], [GETDATE()], ['-99'], [GETDATE()]", _guid);
            //TASK ACTION ID OMITTED, MAY BE 
            DatabaseClient dbClient = new DatabaseClient(InstanceMgr.ProcessInstance);
            using (SqlConnection SQLConn = new SqlConnection(dbClient.ConnectionString))
            {
                SqlCommand SQLCmd = new SqlCommand(_storeProcedure, SQLConn);
                SQLCmd.CommandType = CommandType.Text;

                try
                {
                    SQLConn.Open();
                    SQLCmd.ExecuteNonQuery();
                }
                catch 
                {
                    //shhhhh no more words just emotions
                }
            }
        }
    }
}


	