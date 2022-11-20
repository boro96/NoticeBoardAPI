using AutoMapper;
using NoticeBoardAPI.Models;
using NoticeBoardAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI
{
    public class NoticeBoardMappingProfile : Profile
    {
        public NoticeBoardMappingProfile()
        {
            CreateMap<Advert, AdvertDto>()
                .ForMember(a => a.FirstName, c => c.MapFrom(s => s.User.FirstName))
                .ForMember(a => a.LastName, c => c.MapFrom(s => s.User.LastName))
                .ForMember(a => a.CategoryName, c => c.MapFrom(s => s.Category.Name));

            CreateMap<Comment, CommentDto>()
                .ForMember(a => a.FirstName, c => c.MapFrom(s => s.User.FirstName))
                .ForMember(a => a.LastName, c => c.MapFrom(s => s.User.LastName));

            CreateMap<RegisterUserDto, User>()
                .ForMember(a => a.Address, c => c.MapFrom(b => new Address
                {
                    City = b.City, Street = b.Street, ApartamentNumber = b.ApartamentNumber, PostalCode = b.PostalCode
                }));

        }

    }
}
