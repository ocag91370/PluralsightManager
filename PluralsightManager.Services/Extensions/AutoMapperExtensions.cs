using PluralsightManager.Models.Models;

namespace PluralsightManager.Services
{
    public static class AutoMapperExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TDestination>(this object source)
        {
            return AutoMapperConfiguration.Mapper.Map<TDestination>(source.GetType());
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }

        public static ResultModel<TModel> Map<TEntity, TModel>(this TEntity entity)
            where TEntity : class
            where TModel : class
        {
            if (entity is TEntity entityResult)
            {
                var model = entityResult.MapTo<TEntity, TModel>();
                if (model is TModel modelEntity)
                    return new ResultModel<TModel> { Ok = true, Data = modelEntity };
            }

            return new ResultModel<TModel> { Ok = false, Data = null };
        }
    }
}
