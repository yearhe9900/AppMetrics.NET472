using App.Metrics.Gauge;
using AppMetrics.NET472.Library.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace New.Api.AppMetrics.NET472
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var metrics = ApiMetrics.SetMetrics(new InitAppMetricsModel()
            {
                BaseUri = "http://192.168.137.253:8086",
                UserName = "hzp",
                Password = "he181904",
                Database = "test"
            });

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
