using BLL;
using BLL.Contracts;
using BLL.Services;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace HypernationAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IDocManagement, DocManagement>();
            container.RegisterType<IBarbarismManagement, BarbarismManagement>();
            container.RegisterType<IMorphologyManagement, MorphologyManagement>();
            container.RegisterType<ICleanManagement, CleanManagement>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}