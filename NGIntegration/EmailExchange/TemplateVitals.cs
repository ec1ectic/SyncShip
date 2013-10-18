using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncingShip
{
    public class TemplateVitals : ITemplate
    {
        public void SaveToDatabase(Guid patientId, string enterpriseId, string practiceId)
        {
            //Save vitals to template table
        }
    }
}
