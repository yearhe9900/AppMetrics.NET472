using App.Metrics;
using System;
using System.Collections.Generic;
using App.Metrics.Extensions.Reporting.InfluxDB;
using System.Linq;
using System.Reflection;
using System.Web;
using App.Metrics.Formatters.InfluxDB;
using App.Metrics.Filtering;

namespace AppMetrics.NET472.Library.Infrastructure
{
    public class ApiMetrics
    {
        private static IMetricsRoot _metrics;

        public static IMetricsRoot GetMetrics() => _metrics;

        public static IMetricsRoot SetMetrics()
        {
            if (_metrics == null)
            {
                InitAppMetricsModel initAppMetricsModel = new InitAppMetricsModel();
                _metrics = InitAppMetrics(initAppMetricsModel);
            }

            return _metrics;
        }

        public static IMetricsRoot SetMetrics(InitAppMetricsModel initAppMetricsModel)
        {
            if (_metrics == null)
            {
                _metrics = InitAppMetrics(initAppMetricsModel);
            }

            return _metrics;
        }

        private static IMetricsRoot InitAppMetrics(InitAppMetricsModel initAppMetricsModel)
        {
            GlobalMetricTags globalMetricTags = new GlobalMetricTags();
            if (initAppMetricsModel.GlobalTags != null)
            {
                foreach (var item in initAppMetricsModel.GlobalTags)
                {
                    globalMetricTags.Add(item.Key, item.Value);
                }
            }

            var metrics = new MetricsBuilder()
                            .Configuration.Configure(options =>
                            {
                                options.DefaultContextLabel = initAppMetricsModel.DefaultContextLabel;
                                options.AddAppTag(Assembly.GetExecutingAssembly().GetName().Name);
                                options.AddServerTag(Environment.MachineName);
                                options.AddEnvTag(initAppMetricsModel.EnvTag);
                                options.GlobalTags = globalMetricTags;
                            })
                            .Report.ToInfluxDb(options =>
                            {
                                options.InfluxDb.BaseUri = new Uri(initAppMetricsModel.BaseUri);
                                options.InfluxDb.Database = initAppMetricsModel.Database;
                                options.InfluxDb.UserName = initAppMetricsModel.UserName;
                                options.InfluxDb.Password = initAppMetricsModel.Password;
                                options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                                options.HttpPolicy.FailuresBeforeBackoff = 5;
                                options.HttpPolicy.Timeout = TimeSpan.FromSeconds(3);
                                options.FlushInterval = TimeSpan.FromSeconds(5);

                                options.InfluxDb.CreateDataBaseIfNotExists = true;//如果没有库，则创建
                                options.MetricsOutputFormatter = new MetricsInfluxDbLineProtocolOutputFormatter();
                            })
                            .Build();

            return metrics;
        }
    }
}