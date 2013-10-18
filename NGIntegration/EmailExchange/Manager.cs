using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncingShip
{
    public class Manager
    {
        public static void ProcessTemplate(TemplateEnvelope templateEnvelope)
        {
            ITemplate template;

            switch (templateEnvelope.TemplateName.ToUpper())
            {
                case "GLUCOSEMONITOR" : 
                    template = new GlucoseMonitor();
                    break;
                case "VITALSIGNS" :
                    template = new VitalSigns();
                    break;
                default :
                    throw new Exception(string.Format("Invalid template type: {0}", templateEnvelope.TemplateName));
            }
            GetPatientId getPatientId = new GetPatientId();
            //We need to grab the enterprise and practice ids
            //template.SaveToDatabase(new Guid(getPatientId.GetPatient(templateEnvelope.FirstName, templateEnvelope.LastName, templateEnvelope.DateOfBirth)));
            CreateTask createTask = new CreateTask();
            createTask.Create();
        }


        //private CreateTask _createTask = new CreateTask();
        //private GetPatientId _getPatientId = new GetPatientId();
        //private GlucoseMonitor _glucoseMonitor = new GlucoseMonitor();
        //private VitalSigns _vitalSigns = new VitalSigns();
        
        ////First we get the person_id 
        //public string GetPatientId(string FirstName, string LastName, string DateOfBirth)
        //{
        //    return _getPatientId.GetPatient(FirstName, LastName, DateOfBirth);
        //}

        ////We use the person_id to insert whatever data has been given
        ////public void InsertVitalSignData
        ////{
        ////
        ////}
        ////public void InsertGlucoseMonitorData
        ////{
        ////
        ////}

        ////Create the task
        //public void CreateTask()
        //{
        //    _createTask.Create();
        //}
    }
}
