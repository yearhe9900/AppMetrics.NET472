﻿using Api.AppMetrics.NET472.Infrastructure;
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
        [Route("get")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
        }

        [Route("post")]
        [HttpPost]
        public IHttpActionResult Post()
        {
            return Json(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
        }
    }
}
