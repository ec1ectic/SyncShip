using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using NextGen.Core;
using NextGen.Data;


namespace SyncingShip
{
    [TableName("chm_vital_signs_")]
    [PrimaryKey("seq_no", autoIncrement = false)]
    public class VitalSigns : ITemplate
    {
        public string enterprise_id { get; set; }
        public string practice_id { get; set; }
        public Guid person_id { get; set; }
        public Guid seq_no { get; set; }
        [Column("vitalSignsDate")]
        public string Date { get; set; }
        [Column("vitalSignsTime")]
        public string Time { get; set; }
        [Column("measured_by")]
        public string Name { get; set; }
        [Column("temp_deg_F")]
        public decimal Temp { get; set; }
        [Column("bp_systolic")]
        public string Sys { get; set; }
        [Column("bp_diastolic")]
        public string Dias { get; set; }
        [Column("pulse_rate")]
        public int Pulse { get; set; }
        [Column("respiration_rate")]
        public int Resp { get; set; }
        [Column("weight_lb")]
        public decimal Wt { get; set; }
        [Column("sp_o2_dtl")]
        public int PulseOx { get; set; }
        [Column("peakFlow")]
        public int PeakFlow { get; set; }
        [Column("comments")]
        public string Comments { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
        public DateTime create_timestamp { get; set; }
        public DateTime modify_timestamp { get; set; }

        public static VitalSigns FromJSon(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<VitalSigns>(json);
        }

        public VitalSigns() { }
        public VitalSigns(string date, string time, string name, decimal temp, string sys, string dias, int pulse, int resp, decimal wt, int pulseOx, int peakFlow, string comments)
        {
            this.Date = date;
            this.Time = time;
            this.Name = name;
            this.Temp = temp;
            this.Sys = sys;
            this.Dias = dias;
            this.Pulse = pulse;
            this.Resp = resp;
            this.Wt = wt;
            this.PulseOx = pulseOx;
            this.PeakFlow = peakFlow;
            this.Comments = comments;
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
