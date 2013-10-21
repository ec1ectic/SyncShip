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
            var vitalSigns = new VitalSigns
            {
                Date = "09/30/1992",
                Time = DateTime.Now.ToString(),
                Name = "Jordan Hughes",
                Temp = 98,
                Sys = "80",
                Dias = "150",
                Pulse = 90,
                Resp = 90,
                Wt = 210,
                PulseOx = 20,
                PeakFlow = 20,
                Comments = "Test Case"
            };
            //vitalSigns.seq_no = Guid.NewGuid();

            vitalSigns.SaveToDatabase(new Guid("468C3119-BD73-4191-A8B1-2673D7C9D344"), "00001", "0001");
        }
        [TestCase]
        public void SaveTestGlucose()
        {
            var glucoseMonitor = new GlucoseMonitor();
            glucoseMonitor.Date = "09/30/1992";
            glucoseMonitor.Time = DateTime.Now.Hour.ToString();
            glucoseMonitor.Name = "Jordan Hughes";
            glucoseMonitor.Random = "200";
            glucoseMonitor.Breakfast = "200";
            glucoseMonitor.Lunch = "200";
            glucoseMonitor.Dinner = "200";
            glucoseMonitor.Bedtime = "200";
            glucoseMonitor.Comments = "";

            glucoseMonitor.SaveToDatabase(new Guid("468C3119-BD73-4191-A8B1-2673D7C9D344"), "00001", "0001");
        }
    }
}
