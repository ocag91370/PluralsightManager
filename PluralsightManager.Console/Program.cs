using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using PluralsightManager.Contracts;
using PluralsightManager.Models;
using PluralsightManager.Models.Models;
using PluralsightManager.Repositories;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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

            var result = pluralsightService.DownloadCourse("636041d7-7b62-4dfd-9341-3712ac58f6d0");
            //var result = pluralsightService.DownloadAllCourses();
        }

        static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            var configuration = new PluralsightConfiguration
            {
                InputPath = ConfigurationManager.AppSettings["InputPath"],
                VideoFolder = ConfigurationManager.AppSettings["VideoFolder"],
                OutputPath = ConfigurationManager.AppSettings["OutputPath"],
                VideoFileExtension = ConfigurationManager.AppSettings["VideoFileExtension"],
                TranscriptFileExtension = ConfigurationManager.AppSettings["TranscriptFileExtension"]
            };

            builder.RegisterModule(new PluralsightManager.Services.RegisterModule(configuration));
            //builder.RegisterModule<PluralsightManager.Services.RegisterModule>();

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
