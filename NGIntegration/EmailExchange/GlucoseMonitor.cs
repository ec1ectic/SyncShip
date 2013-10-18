using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using NextGen.Data;
using NextGen.Core;

namespace SyncingShip
{
    [TableName("chm_glucose_monitor_data_")]
    [PrimaryKey("seq_no", autoIncrement = false)]
    public class GlucoseMonitor : ITemplate
    {
        public string enterprise_id { get; set; }
        public string practice_id { get; set; }
        public Guid person_id { get; set; }
        public Guid seq_no { get; set; }
        [Column("gluc_date")]
        public string Date { get; set; }
        [Column("glucTime")]
        public string Time { get; set; }
        [Column("userName")]
        public string Name { get; set; }
        [Column("random")]
        public string Random { get; set; }
        [Column("breakfast")]
        public string Breakfast { get; set; }
        [Column("lunch")]
        public string Lunch { get; set; }
        [Column("dinner")]
        public string Dinner { get; set; }
        [Column("bedtime")]
        public string Bedtime { get; set; }
        [Column("opt_comments")]
        public string Comments { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
        public DateTime create_timestamp { get; set; }
        public DateTime modify_timestamp { get; set; }


        public GlucoseMonitor() { }
        public GlucoseMonitor(string date, string time, string name, string random, string breakfast, string lunch, string dinner, string bedtime, string comments)
        {
            this.Date = date;
            this.Time = time;
            this.Name = name;
            this.Random = random;
            this.Breakfast = breakfast;
            this.Lunch = lunch;
            this.Dinner = dinner;
            this.Bedtime = bedtime;
            this.Comments= comments;
        }

        public void SaveToDatabase(Guid patientId, string enterpriseId, string practiceId)
        {
            person_id = patientId;
            enterprise_id = enterpriseId;
            practice_id = practiceId;
            create_timestamp = DateTime.Now;
            modify_timestamp = DateTime.Now;
            created_by = -99;
            modified_by = -99;
            seq_no = Guid.NewGuid();

            EmailExchange.SQLConnectClass sqlConnect = new EmailExchange.SQLConnectClass();
            sqlConnect.DetermineRoute();
            string connString = "Data Source=127.0.0.1;Initial Catalog=NG56GR_81;Integrated Security=false;User ID=sa;Password=nextgen;MultipleActiveResultSets=true";
            var db = new Database(connString, "System.Data.SqlClient");

            db.Insert(this); 
        }
    }
}
