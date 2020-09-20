using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wishlist.Core.Models;


namespace Wishlist.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DTO.ClientDTO, Client>();
            CreateMap<DTO.ProductDTO, Product>();
            CreateMap<DTO.WishClientDTO, WishClient>();
        }
    }
}
