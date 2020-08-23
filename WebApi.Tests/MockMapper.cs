using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WebApi.Helpers;

namespace WebApi.Tests
{
    static class MockMapper
    {
        public static IMapper GetNew()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
