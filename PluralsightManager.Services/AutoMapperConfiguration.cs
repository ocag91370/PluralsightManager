using AutoMapper;
using PluralsightManager.Models.Models;
using PluralsightManager.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightManager.Services
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }
    }

    public static class AutoMapperConfiguration
    {
        public static void Init()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {

                #region Post
                // Mapping domain entities to view entities
                cfg.CreateMap<CourseEntity, CourseModel>();
                ;
                // Mapping view entities to domain entities
                //cfg.CreateMap<PostViewModel, Post>();
                #endregion
            });

            Mapper = MapperConfiguration.CreateMapper();
        }

        public static IMapper Mapper { get; private set; }

        public static MapperConfiguration MapperConfiguration { get; private set; }
    }
}
