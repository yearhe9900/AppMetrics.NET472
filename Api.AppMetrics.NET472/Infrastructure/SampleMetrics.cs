﻿using App.Metrics;
using App.Metrics.Core.Options;

namespace Api.AppMetrics.NET472.Infrastructure
{
    public static class SampleMetrics
    {
        public static CounterOptions BasicCounter = new CounterOptions
        {
            Name = "sample_counter",
            MeasurementUnit = Unit.Calls,
            ResetOnReporting = true
        };
    }
}