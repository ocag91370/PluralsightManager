using Autofac;
using PluralsightManager.Contracts;
using PluralsightManager.Repositories;
using PluralsightManager.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightManager.Services
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new PluralsightManager.Repositories.RegisterModule());

            builder.RegisterType<PluralsightService>().As<IPluralsightService>();
        }
    }
}