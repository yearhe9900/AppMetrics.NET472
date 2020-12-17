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
            ApiMetrics.SetMetrics(new InitAppMetricsModel()
            {
                DefaultContextLabel = "apiappmetricsnet472"
            });
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
