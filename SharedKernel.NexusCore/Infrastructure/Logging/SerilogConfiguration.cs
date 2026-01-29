using Microsoft.AspNetCore.Builder;
using Serilog;
namespace SharedKernel.NexusCore.Infrastructure.Logging
{
    /// <summary>
    /// serilog config
    /// </summary>
    public static class SerilogConfiguration
    {
        /// <summary>
        /// configure serilog
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureSerilog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .Enrich.WithEnvironmentName()
                    .WriteTo.Console()
                    .WriteTo.Seq(builder.Configuration["Seq:ServerUrl"])
                    .CreateLogger();

            builder.
                Host.UseSerilog();

        }

    }
}
