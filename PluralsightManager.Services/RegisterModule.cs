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
                .RegisterType<PluralsightService>()
                .As<IPluralsightService>();

            builder
                .RegisterType<FolderManager>()
                .As<IFolderManager>()
                .WithParameter("configuration", _configuration);

            builder
                .RegisterType<VideoManager>()
                .As<IVideoManager>()
                .WithParameter("configuration", _configuration);
        }
    }
}