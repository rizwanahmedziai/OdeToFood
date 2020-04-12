using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using OdeToFood.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace OdeToFood.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            //Register MVC Controller
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            // Register Web Api Controller
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<InMemoryRestaurantData>()
                   .As<IRestaurantData>()
                   .SingleInstance();

            var container = builder.Build();
            // Dependency Resolver for MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Dependency Resolver for Web Api
            // Both MVC and WebApi both frameworks use different methods for this
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}