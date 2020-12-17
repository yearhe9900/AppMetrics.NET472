using Api.AppMetrics.NET472.Infrastructure;
using App.Metrics;
using App.Metrics.Extensions.Owin.WebApi;
using App.Metrics.Extensions.Reporting.InfluxDB;
using App.Metrics.Extensions.Reporting.InfluxDB.Client;
using App.Metrics.Reporting.Abstractions;
using App.Metrics.Reporting.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

[assembly: OwinStartup(typeof(Api.AppMetrics.NET472.Startup))]

namespace Api.AppMetrics.NET472
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MessageHandlers.Add(new MetricsWebApiMessageHandler());
            httpConfiguration.RegisterWebApi();

            var services = new ServiceCollection();
            ConfigureServices(services);
            var provider = services.SetDependencyResolver(httpConfiguration);

            app.UseMetrics(provider);

            HostingEnvironment.QueueBackgroundWorkItem(cancellationToken =>
            {
                var reportFactory = provider.GetRequiredService<IReportFactory>();
                var metrics = provider.GetRequiredService<IMetrics>();
                var reporter = reportFactory.CreateReporter();
                reporter.RunReports(metrics, cancellationToken);
            });

            app.UseWebApi(httpConfiguration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var influxDbSettings = new InfluxDBSettings("apiappmetricsnet472", new Uri("http://192.168.137.253:8086"));
            influxDbSettings.UserName = "hzp";
            influxDbSettings.Password = "he181904";
            services.AddLogging();

            services.AddControllersAsServices();

            services
                .AddMetrics(options =>
                {
                    options.DefaultContextLabel = "Api.AppMetrics.NET472";//期望别名，这里建议采用项目的简写名称，保证项目之间不存在冲突即可
                    options.ReportingEnabled = true;
                }, Assembly.GetExecutingAssembly().GetName())
                 .AddReporting(factory =>
                 {
                     factory.AddInfluxDb(new InfluxDBReporterSettings
                     {
                         HttpPolicy = new HttpPolicy
                         {
                             FailuresBeforeBackoff = 3,//指标未能向指标入口端点报告时，在回退之前的失败次数。
                             BackoffPeriod = TimeSpan.FromSeconds(30),//当指标无法向指标入口端点报告时，从后退
                             Timeout = TimeSpan.FromSeconds(3)//尝试向度量标准入口端点报告度量标准时的HTTP超时持续时间
                         },
                         InfluxDbSettings = influxDbSettings,//influx database,url

                         ReportInterval = TimeSpan.FromSeconds(5),
                     });
                 })
                .AddHealthChecks(factory =>
                {
                    factory.RegisterProcessPrivateMemorySizeHealthCheck("Private Memory Size", 200);
                    factory.RegisterProcessVirtualMemorySizeHealthCheck("Virtual Memory Size", 200);
                    factory.RegisterProcessPhysicalMemoryHealthCheck("Working Set", 200);
                })
                .AddJsonSerialization()
                .AddMetricsMiddleware(options =>
                {

                });
        }
    }
}
