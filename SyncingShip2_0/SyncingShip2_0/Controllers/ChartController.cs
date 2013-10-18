using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SyncingShip2_0.Models;

namespace SyncingShip2_0.Controllers
{
    public class ChartController : ApiController
    {

        public IEnumerable<ChartData> GetAllCharts()
        {
            var list = new List<ChartData>();
            list.Add(new ChartData { FirstName = "Test1", LastName = "TestLast1" });
            list.Add(new ChartData { FirstName = "Test2", LastName = "TestLast2" });
            list.Add(new ChartData { FirstName = "Test3", LastName = "TestLast3" });

            return list;
        }

        public ChartData PostChartData(ChartData chartData)
        {
            // Only place we need mapping, so setup the map
            AutoMapper.Mapper.CreateMap<ChartData,SyncingShip.TemplateEnvelope>();
            //AutoMapper.Mapper.CreateMap<SyncingShip.TemplateEnvelope, ChartData>();
            
            SyncingShip.Manager.ProcessTemplate(AutoMapper.Mapper.Map<SyncingShip.TemplateEnvelope>(chartData) );

            return chartData;
        }

    }
}
