using MVCDemoService;
using MVCWebsite.Desgin_Parttern;
using MVCWebsite.Models;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace MVCWebsite
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            container.RegisterType<IBookEntities, BookEntities>();
            container.RegisterType<IBookService, BookService>();
            container.RegisterType<IAuthorService, AuthorService>();
        }
    }
}