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
                    template = GlucoseMonitor.FromJSon(templateEnvelope.JsonPayload);
                    break;
                case "VITALSIGNS" :
                    template = VitalSigns.FromJSon(templateEnvelope.JsonPayload);
                    break;
                default :
                    throw new Exception(string.Format("Invalid template type: {0}", templateEnvelope.TemplateName));
            }
            
            //GetPatientId getPatientId = new GetPatientId();
            //We need to grab the enterprise and practice ids
            //template.SaveToDatabase(new Guid(getPatientId.GetPatient(templateEnvelope.FirstName, templateEnvelope.LastName, templateEnvelope.DateOfBirth)));
            template.SaveToDatabase(new Guid("468C3119-BD73-4191-A8B1-2673D7C9D344"), "00001", "0001");
            CreateTask createTask = new CreateTask();
            createTask.Create();
        }
    }
}
