using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncingShip2_0.Models
{
    interface ILogin
    {
        //IEnumerable<ChartData> GetAll();
        //ChartData Get(int id);
        LoginCredential CheckLogin(LoginCredential item);
        //void Remove(int id);
        //bool Update(ChartData item);
    }
}
