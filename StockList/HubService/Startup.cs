using System.Diagnostics;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;

namespace HubService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();
            app.UseCors(CorsOptions.AllowAll);

            var hubConfiguration = new HubConfiguration
            {
#if DEBUG
                EnableDetailedErrors = true
#else
                EnableDetailedErrors = false
#endif
            };

            app.MapSignalR(hubConfiguration);
            

            // Turn tracing on programmatically
            GlobalHost.TraceManager.Switch.Level = SourceLevels.All;

        }
    }
}
