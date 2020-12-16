using Api.AppMetrics.NET472.Infrastructure;
using App.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.AppMetrics.NET472.Controllers
{
    [RoutePrefix("api/default")]
    public class DefaultController : ApiController
    {
        private readonly IMetrics _metrics;

        public DefaultController(IMetrics metrics)
        {
            if (metrics == null)
            {
                throw new ArgumentNullException(nameof(metrics));
            }

            _metrics = metrics;
        }

        [Route("get")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            _metrics.Measure.Counter.Increment(SampleMetrics.BasicCounter);

            return Json(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
        }

        [Route("post")]
        [HttpPost]
        public IHttpActionResult Post()
        {
            _metrics.Measure.Counter.Increment(SampleMetrics.BasicCounter);
            return Json(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
        }
    }
}
