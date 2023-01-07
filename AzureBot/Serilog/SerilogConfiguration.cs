using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScreenApi.Serilog
{
    public static class SerilogConfiguration
    {
        public static ILogger CreateLogger() {
            return SerilogConfiguration.GetLoggerConfiguration().CreateLogger();
        }
        public static LoggerConfiguration GetLoggerConfiguration() {

            var logConfig = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console(
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] ({SourceContext}) {Message:lj}{NewLine}{Exception}"
                )
                .WriteTo.Async(log => log
                    .File(
                        restrictedToMinimumLevel: LogEventLevel.Debug,
                        path: "log.log",
                        rollOnFileSizeLimit: true,
                        fileSizeLimitBytes: 10000000,
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] ({SourceContext}) {Message:lj}{NewLine}{Exception}"
                    )
                );

            return logConfig;
        }
    }
}
