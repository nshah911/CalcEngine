using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileManagerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Home")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("start")]
        [Produces("application/json")]
        public string Start()
        {
            return "CalcEngine.API Started.";
        }
    }
}