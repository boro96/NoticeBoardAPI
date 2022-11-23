﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NoticeBoardAPI.Entities;
using NoticeBoardAPI.Exceptions;
using NoticeBoardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Services
{
    public interface ICommentService
    {
        int Create(int advertId, CreateCommentDto dto);
        IEnumerable<CommentDto> GetAll(int advertId);
        CommentDto GetById(int advertId, int commentId);
    }

    public class CommentService : ICommentService
    {
        private readonly NoticeBoardDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _contextService;

        public CommentService(NoticeBoardDbContext dbContext, IMapper mapper, IUserContextService contextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _contextService = contextService;
        }
        public int Create(int advertId, CreateCommentDto dto)
        {
            var advert = _dbContext.Adverts.FirstOrDefault(a => a.Id == advertId);
            if (advert is null)
            {
                throw new NotFoundException("Advert doesnt exist");
            }

            var commentEntity = _mapper.Map<Comment>(dto);
            commentEntity.UserId = _contextService.GetUseId;
            commentEntity.AdvertId = advertId;

            _dbContext.Comments.Add(commentEntity);
            _dbContext.SaveChanges();

            return commentEntity.Id;
        }
        public IEnumerable<CommentDto> GetAll(int advertId)
        {
            var advert = _dbContext.Adverts
                .Include(b => b.Comments)
                .ThenInclude(a => a.User)
                .FirstOrDefault(a => a.Id == advertId);
            if(advert is null)
            {
                throw new NotFoundException("Advert doesnt exist");
            }

            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(advert.Comments);

            return commentsDto;
        }
        public CommentDto GetById(int advertId, int commentId)
        {
            var advert = _dbContext.Adverts.FirstOrDefault(b => b.Id == advertId);
            if(advert is null)
            {
                throw new NotFoundException("Advert doesnt exist");
            }
            var comment = _dbContext.Comments
                .Include(a => a.User)
                .FirstOrDefault(a => a.Id == commentId);
            if(comment is null || comment.AdvertId != advertId)
            {
                throw new NotFoundException("Comment doesnt exist");
            }
            var commentDto = _mapper.Map<CommentDto>(comment);

            return commentDto;
            
        }
    }
}
