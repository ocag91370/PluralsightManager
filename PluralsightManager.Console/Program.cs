using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using PluralsightManager.Contracts;
using PluralsightManager.Models.Models;
using PluralsightManager.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace PluralsightManager.Console
{

    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            RegisterAutofac();
            RegisterAutoMapper();

            var pluralsightService = Container.Resolve<IPluralsightService>();
            var courses = pluralsightService.GetAllCourses().ToList();

            return;
        }

        static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<PluralsightManager.Services.RegisterModule>();

            Container = builder.Build();

            // Setting Dependency Injection Parser
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }

        /// <summary>
        /// Configuration initialization of AutoMapper
        /// </summary>
        static void RegisterAutoMapper()
        {
            new AutoMapperStartupTask().Execute();
        }

        /// <summary>
        /// AutoMapper initialization class
        /// </summary>
        public class AutoMapperStartupTask
        {
            public void Execute()
            {
                PluralsightManager.Services.AutoMapperConfiguration.Init();
            }
        }
    }
}
