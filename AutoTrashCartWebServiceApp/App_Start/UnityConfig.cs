using System.Web.Http;
using AutoTrashCartWebServiceApp.DAL;
using AutoTrashCartWebServiceApp.Interfaces;
using Unity;
using Unity.WebApi;
using Unity.Lifetime;

namespace AutoTrashCartWebServiceApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IAutoTrashCartRepository, AutoTrashCartRepository>(new ContainerControlledLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}