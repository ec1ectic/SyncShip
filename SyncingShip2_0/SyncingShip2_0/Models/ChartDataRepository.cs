using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SyncingShip2_0.Models
{
    public class ChartDataRepository : IChartData
    {
        public ChartData Add(ChartData item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            return item;
        }

    }
}