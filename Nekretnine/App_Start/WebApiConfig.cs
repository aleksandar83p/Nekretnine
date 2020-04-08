using Microsoft.Owin.Security.OAuth;
using Nekretnine.Repository;
using Nekretnine.Repository.Intefaces;
using Nekretnine.Resolver;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace Nekretnine
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Unity
            var container = new UnityContainer();

            container.RegisterType<IAgentRepository, AgentRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<INekretninaRepository, NekretninaRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
