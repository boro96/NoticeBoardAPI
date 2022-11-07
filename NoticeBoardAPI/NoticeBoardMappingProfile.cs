﻿using AutoMapper;
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
                .ForMember(a => a.LastName, c => c.MapFrom(s => s.User.LastName));

            CreateMap<Comment, CommentDto>()
                .ForMember(a => a.FirstName, c => c.MapFrom(s => s.User.FirstName))
                .ForMember(a => a.LastName, c => c.MapFrom(s => s.User.LastName));
        }

    }
}
