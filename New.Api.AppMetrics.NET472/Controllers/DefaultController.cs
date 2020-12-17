using System.Collections.Generic;
using System.Web.Http;

namespace New.Api.AppMetrics.NET472.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
        }

        [HttpPost]
        public IHttpActionResult Post()
        {
            return Json(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
        }
    }
}
