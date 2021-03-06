﻿using AppMetrics.NET472.Library.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web.Http;

namespace New.Api.AppMetrics.NET472.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public IHttpActionResult MyGet()
        {
            Thread.Sleep(10);
            var random = new Random().Next(100, 200);
            Thread.Sleep(random);
            return Json(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
        }

        [HttpPost]
        public IHttpActionResult MyPost()
        {
            Thread.Sleep(10);
            var random = new Random().Next(300, 500);
            Thread.Sleep(random);
            return Json(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
        }

        [HttpGet]
        public HttpResponseMessage GetMetrics()
        {
            var formatter = new App.Metrics.Formatters.InfluxDB.MetricsInfluxDbLineProtocolOutputFormatter();
            var snapshot = ApiMetrics.GetMetrics().Snapshot.Get();

            using (var ms = new MemoryStream())
            {
                formatter.WriteAsync(ms, snapshot).GetAwaiter();
                var result = Encoding.UTF8.GetString(ms.ToArray());

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(result, Encoding.UTF8, formatter.MediaType.ContentType);

                return response;
            }
        }
    }
}
