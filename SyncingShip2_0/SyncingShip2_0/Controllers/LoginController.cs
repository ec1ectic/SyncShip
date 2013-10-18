using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SyncingShip2_0.Models;


namespace SyncingShip2_0.Controllers
{
    public class LoginController : ApiController
    {
        public LoginCredential PostLoginCredential(LoginCredential loginCredential)
        {
            var login = new LoginRepository();
            return login.CheckLogin(loginCredential);
        }
    }
}
