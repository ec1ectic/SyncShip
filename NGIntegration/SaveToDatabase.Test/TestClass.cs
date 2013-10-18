using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SyncingShip;

namespace SaveToDatabase.Test
{
    [TestFixture]
    public class TestClass
    {
        [TestCase]
        public void SaveTestVitals()
        {
            VitalSigns vitalSigns = new VitalSigns();
            vitalSigns.Date = "09/30/1992";
            vitalSigns.Time = DateTime.Now.ToString();
            vitalSigns.seq_no = Guid.NewGuid();
            vitalSigns.Name = "Jordan Hughes";
            vitalSigns.Temp = 98;
            vitalSigns.Sys = "80";
            vitalSigns.Dias = "150";
            vitalSigns.Pulse = 90;
            vitalSigns.Resp = 90;
            vitalSigns.Wt = 210;
            vitalSigns.PulseOx = 20;
            vitalSigns.PeakFlow = 20;
            vitalSigns.Comments = "Test Case";
            
            vitalSigns.SaveToDatabase(new Guid("966ED44E-D96A-4908-9D1A-65241B3893BB"), "00001", "0001");
        }
        [TestCase]
        public void SaveTestGlucose()
        {
            GlucoseMonitor glucoseMonitor = new GlucoseMonitor();
            glucoseMonitor.Date = "09/30/1992";
            glucoseMonitor.Time = DateTime.Now.Hour.ToString();
            glucoseMonitor.Name = "Jordan Hughes";
            glucoseMonitor.Random = "200";
            glucoseMonitor.Breakfast = "200";
            glucoseMonitor.Lunch = "200";
            glucoseMonitor.Dinner = "200";
            glucoseMonitor.Bedtime = "200";
            glucoseMonitor.Comments = "";

            glucoseMonitor.SaveToDatabase(new Guid("966ED44E-D96A-4908-9D1A-65241B3893BB"), "00001", "0001");
        }
    }
}
