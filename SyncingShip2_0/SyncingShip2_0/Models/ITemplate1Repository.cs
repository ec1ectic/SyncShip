using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncingShip2_0.Models
{
    interface ITemplate1Repository
    {
        IEnumerable<Template1> GetAll();
        Template1 Get(int id);
        Template1 Add(Template1 item);
        void Remove(int id);
        bool Update(Template1 item);
    }
}
