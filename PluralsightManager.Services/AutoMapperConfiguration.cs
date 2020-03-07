﻿using AutoMapper;
using PluralsightManager.Models.Models;
using PluralsightManager.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightManager.Services
{

    public static class AutoMapperConfiguration
    {
        public static void Init()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CourseEntity, CourseModel>()
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Trim()))
                    .ForMember(dest => dest.HasTranscript, opt => opt.MapFrom(src => (src.HasTranscript.HasValue) ? Convert.ToBoolean(src.HasTranscript.Value) : false))
                    .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => TimeSpan.FromMilliseconds(src.DurationInMilliseconds.GetValueOrDefault(0))));

                cfg.CreateMap<ModuleEntity, ModuleModel>()
                    .ForMember(dest => dest.Index, opt => opt.MapFrom(src => src.ModuleIndex))
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Trim()))
                    .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => TimeSpan.FromMilliseconds(src.DurationInMilliseconds.GetValueOrDefault(0))));

                cfg.CreateMap<ClipEntity, ClipModel>()
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Trim()))
                    .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.ModuleId.Value))
                    .ForMember(dest => dest.Index, opt => opt.MapFrom(src => src.ClipIndex))
                    .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => TimeSpan.FromMilliseconds(src.DurationInMilliseconds.GetValueOrDefault(0))));

                cfg.CreateMap<TranscriptEntity, TranscriptModel>()
                    .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeSpan.FromMilliseconds(src.StartTime.GetValueOrDefault(0))))
                    .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeSpan.FromMilliseconds(src.EndTime.GetValueOrDefault(0))));
            });

            Mapper = MapperConfiguration.CreateMapper();
        }

        public static IMapper Mapper { get; private set; }

        public static MapperConfiguration MapperConfiguration { get; private set; }
    }
}
