using System.Reflection;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Owin;

namespace StockLists
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HubConfiguration();
            config.EnableJSONP = true;
            app.MapSignalR(config);
        }
    }

}


namespace StockLists.App_Start
{
   
    /// <summary>
    /// 
    /// </summary>
    public static class SimpleInjectorInitializer
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            InitializeContainer(container);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        /// <summary>
        /// Initializes the container.
        /// </summary>
        /// <param name="container">The container.</param>
        private static void InitializeContainer(Container container)
        {
            // conviences over configuration
            var allAssemblies = GetAllAssemblies();

            foreach (var allAssembly in allAssemblies)
            {
                var registrations = allAssembly.GetExportedTypes()
                    .Where(t => t.Namespace != null && t.Namespace.StartsWith("N") && !t.Namespace.Contains("N*.IOC")
                                && t.IsClass && !t.IsGenericType && t.GetInterfaces().Any())
                    .Select(type => new { Service = type.GetInterfaces().First(), Implementation = type }).ToArray();

                foreach (var reg in registrations)
                {
                    container.Register(reg.Service, reg.Implementation, Lifestyle.Transient);
                }
            }

        }

        private static List<Assembly> GetAllAssemblies()
        {
            List<Assembly> allAssemblies = new List<Assembly>();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (path != null)
            {
                foreach (string dll in Directory.GetFiles(path, "N*.dll"))
                {
                    allAssemblies.Add(Assembly.LoadFile(dll));
                }
            }
            return allAssemblies;
        }

    }
}