using Autofac;
using AutoMapper;
using PluralsightManager.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightManager.Repositories
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<PluralsightDbContext>().InstancePerRequest();
            //builder.RegisterType<DatabaseRepository>().As<IDatabaseRepository>();
            builder.RegisterType<PluralsightRepository>().As<IPluralsightRepository>();
        }
    }
}