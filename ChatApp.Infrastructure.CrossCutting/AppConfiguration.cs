using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace ChatApp.Infrastructure.CrossCutting
{
    public class AppConfiguration
    {
        /// <summary>
        /// Settings configuration section.
        /// </summary>
        public static IDictionary<string, string> RabbitMQ { get; private set; } = new Dictionary<string, string>();

        public static void Load(IConfiguration configuration)
        {
            RabbitMQ = configuration
                .GetSection("RabbitMQ")
                .AsEnumerable()
                .ToDictionary(i => i.Key.Replace("RabbitMQ:",""), i => i.Value, StringComparer.OrdinalIgnoreCase);
        }
    }
}
