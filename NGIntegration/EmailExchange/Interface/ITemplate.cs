using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncingShip
{
    public interface ITemplate
    {
        //void DeterminePerson(string FirstName, string LastName, string DateOfBirth);
        void SaveToDatabase(Guid patientId, string practiceId, string enterpriseId);
        //void CreateTask();
    }
}
