using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SyncingShip2_0.Models;

namespace SyncingShip2_0.Controllers
{
    public class Template1Controller : ApiController
    {
        static readonly ITemplate1Repository repository = new Template1Repository();


        #region GET

        public IEnumerable<Template1> GetAllTemplate1s()
        {
            return repository.GetAll();
        }

        public Template1 GetTemplate1(int id)
        {
            Template1 item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public IEnumerable<Template1> GetProductsByFirstName(string FirstName)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.FirstName, FirstName, StringComparison.OrdinalIgnoreCase));
        }

        #endregion


        #region POST



        #endregion

    }
}