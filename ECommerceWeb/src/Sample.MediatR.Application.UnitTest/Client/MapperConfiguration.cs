using AutoMapper;

namespace Sample.MediatR.Application.UnitTest.Client
{
    public static class MapperInstace
    {
        public static IMapper SetMapper(this IMapper mapper)
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            mapper = mapperConfig.CreateMapper();
            return mapper;

        }
    }
}
