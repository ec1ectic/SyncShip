using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncingShip
{
    public class TemplateEnvelope
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string TemplateName { get; set; }
        public string JsonPayload { get; set; }
        //public string JsonSubPayload { get; set; }        
        //public string PatientId { get; set; }

        //public Guid DeterminePatient()
        //{
        //    GetPatientId getPatient = new GetPatientId();
        //    return new Guid(getPatient.GetPatient(FirstName, LastName, DateOfBirth));
        //}

        ////Save the template data depending on what template is being used

        //public void CreateTask()
        //{
        //    CreateTask _createTask = new CreateTask();
        //    _createTask.Create();
        //}
    }
}
