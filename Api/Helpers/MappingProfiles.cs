using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Core.Entities;

namespace Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturnDto>().
            ForMember(x => x.ProductBrand,i => i.MapFrom(s => s.ProductBrand.Name)).
            ForMember(x => x.ProductType,i => i.MapFrom(s => s.ProductType.Name));
            CreateMap<ProductToCreateDto,Product>();
        }
    }
}