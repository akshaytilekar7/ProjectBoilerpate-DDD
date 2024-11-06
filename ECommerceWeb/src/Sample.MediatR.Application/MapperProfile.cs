using AutoMapper;
using Sample.MediatR.Application.Client.Create;
using Sample.MediatR.Application.Client.Get;
using Sample.MediatR.Application.Product.Create;
using Sample.MediatR.Application.Product.Get;
using Sample.MediatR.Dto;

namespace Sample.MediatR.Application;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Domain.Client, CreateClientCommand>().ReverseMap();
        CreateMap<Domain.Client, CreateClientRequestDto>().ReverseMap();
        CreateMap<Domain.Client, CreateClientResponseDto>().ReverseMap();
        CreateMap<Domain.Product, CreateProductCommand>().ReverseMap();

        CreateMap<Domain.Client, GetClientsQueryResponse>().ReverseMap();
        CreateMap<Domain.Product, GetProductsQueryResponse>().ReverseMap();

    }
}
