using Serilog;

namespace Chesham.Forza
{
    public static class Common
    {
        public static ILogger Logger { get; set; } = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
    }
}
