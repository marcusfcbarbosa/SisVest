using SisVest.WebUI.Infraestrutura;
using SisVest.WebUI.Infraestrutura.FilterProvider;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SisVest.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Admin", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //Quando a aplicação inicia ele seta as depencias de cada repositorio
            //A principio a injeção de dependencia esta apontando para os Controllers
            //ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            //Todas as dependencias serão resolvidas pelo NinjectDependencyResolver, definindo as injeções nao somente a nivel de Controller, mas a toda a aplicação
            DependencyResolver.SetResolver(new NinjectDependencyResolver());

            
            //Como esta sendo criado um novo, devemos limpar os antigos
            FilterProviders.Providers.Clear();
            FilterProviders.Providers.Add(new FilterProviderCustom());


        }
    }
}