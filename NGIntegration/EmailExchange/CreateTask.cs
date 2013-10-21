using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using NextGen.Core;
using NextGen.Data;
using PetaPoco;

namespace SyncingShip
{
    public class CreateTask
    {
        public void Create()
        {
            Guid _guid = Guid.NewGuid();
            //Hardcoded values could be broken out eventually
            string _storeProcedure = string.Format(@"EXEC ng_add_todo_task ['00001'], ['0001'], [-99], [1], [{0}], ['Blah'], ['blah'], [-99],NULL,NULL,'',NULL, '','{1}', NULL, ''", DateTime.Now.ToString(), Guid.NewGuid());

            //TASK ACTION ID OMITTED, MAY BE 
            string connString = "Data Source=127.0.0.1;Initial Catalog=NG56GR_81;Integrated Security=false;User ID=sa;Password=nextgen;MultipleActiveResultSets=true";
            var db = new Database(connString, "System.Data.SqlClient");

            db.Execute(_storeProcedure);

            /*
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
             */ 
        }
    }
}


	