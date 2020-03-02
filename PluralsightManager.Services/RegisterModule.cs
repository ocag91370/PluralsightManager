using Autofac;
using PluralsightManager.Contracts;
using PluralsightManager.Models;
using PluralsightManager.Services.Contracts;

namespace PluralsightManager.Services
{
    public class RegisterModule : Module
    {
        private readonly PluralsightConfiguration _configuration;

        public RegisterModule(PluralsightConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new PluralsightManager.Repositories.RegisterModule());

            builder
                .RegisterType<ConsoleService>()
                .As<IConsoleService>();

            builder
                .RegisterType<DirectoryService>()
                .As<IDirectoryService>();

            builder
                .RegisterType<PluralsightService>()
                .As<IPluralsightService>();

            builder
                .RegisterType<CourseFolderService>()
                .As<ICourseFolderService>()
                .WithParameter("configuration", _configuration);

            builder
                .RegisterType<CourseVideoService>()
                .As<ICourseVideoService>()
                .WithParameter("configuration", _configuration);

            builder
                .RegisterType<CourseTranscriptService>()
                .As<ICourseTranscriptService>()
                .WithParameter("configuration", _configuration);
        }
    }
}