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
        public string DefaultContextLabel { get; set; } = "API472";

        /// <summary>
        /// 事件标记，比如开发就用Dev，测试用Test，生产用Prod等
        /// </summary>
        public string EnvTag { get; set; } = "Dev";


        public Dictionary<string, string> GlobalTags { get; set; }

        /// <summary>
        /// influx db 链接地址
        /// </summary>
        public string BaseUri { get; set; }

        /// <summary>
        /// influx db 库名
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
