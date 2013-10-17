using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncingShip2_0.Models
{
    interface IChartData
    {
        //IEnumerable<ChartData> GetAll();
        //ChartData Get(int id);
        ChartData Add(ChartData item);
        //void Remove(int id);
        //bool Update(ChartData item);
    }
}
