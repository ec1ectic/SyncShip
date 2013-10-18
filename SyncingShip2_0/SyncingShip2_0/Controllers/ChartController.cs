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
            chartData.JSonPayload = "";
            return chartData;
        }

        //public HttpResponseMessage PostChartData(ChartData chartData)
        //{
        //    chartData.JSonPayload = "";
        //    var response = Request.CreateResponse<ChartData>(HttpStatusCode.Created, chartData);

            
        //    response.Headers.Location = new Uri("www.trimeloni.com");


        //    return response;
        //}
    }
}
