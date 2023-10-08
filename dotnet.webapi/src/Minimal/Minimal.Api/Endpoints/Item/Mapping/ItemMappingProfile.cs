using System;
using AutoMapper;

namespace Minimal.Api.Endpoints.Item.Mapping
{
    using Minimal.Api.Endpoints.Item.Models;

    public class ItemMappingProfile : Profile
    {
        public ItemMappingProfile()
        {
            this.CreateMap<CreateItemRequest, Item>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ReverseMap();
            this.CreateMap<UpdateItemRequest, Item>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ReverseMap();
        }
    }
}

