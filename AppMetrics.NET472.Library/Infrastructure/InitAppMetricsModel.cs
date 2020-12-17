using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMetrics.NET472.Library.Infrastructure
{
    public class InitAppMetricsModel
    {
        /// <summary>
        /// 期望别名，这里建议采用项目的简写名称，保证项目之间不存在冲突即可
        /// </summary>
        public string DefaultContextLabel { get; set; } = "API";

        /// <summary>
        /// 事件标记
        /// </summary>
        public string EnvTag { get; set; } = "Dev";

        public Dictionary<string, string> GlobalTags { get; set; } = new Dictionary<string, string>() { { "my_custom_tag", "MyCustomValue" } };
    }
}
