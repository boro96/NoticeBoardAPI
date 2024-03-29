﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NoticeBoardAPI.Authorization;
using NoticeBoardAPI.Entities;
using NoticeBoardAPI.Exceptions;
using NoticeBoardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Services
{
    public interface IAdvertService
    {
        AdvertDto GetById(int id);
        PagedResult<AdvertDto> GetAll(AdvertQuery query);
        int Create(CreateAdvertDto dto);
        void Delete(int id);
        void Update(UpdateAdvertDto dto, int id);
    }

    public class AdvertService : IAdvertService
    {
        private readonly NoticeBoardDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _contextService;

        public AdvertService(NoticeBoardDbContext dbContext, IMapper mapper,
            IAuthorizationService authorizationService, IUserContextService contextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _contextService = contextService;
        }
        public PagedResult<AdvertDto> GetAll(AdvertQuery query)
        {
            var baseQuery = _dbContext.Adverts
                .Include(a => a.User)
                .Include(b => b.Category)
                .Include(c => c.Comments)
                .ThenInclude(a => a.User)
                .Where(a => query.searchPhrase == null || (a.Category.Name.ToLower().Contains(query.searchPhrase.ToLower())
                || a.Description.ToLower().Contains(query.searchPhrase.ToLower())));


            var adverts = baseQuery
                .Include(a => a.User)
                .Include(b => b.Category)
                .Include(c => c.Comments)
                .ThenInclude(a => a.User)
                .Where(a => query.searchPhrase == null || (a.Category.Name.ToLower().Contains(query.searchPhrase.ToLower())
                || a.Description.ToLower().Contains(query.searchPhrase.ToLower())))
                .Skip(query.pageSize * (query.pageNumber-1))
                .Take(query.pageSize)
                .ToList();


            var advertsDto = _mapper.Map<List<AdvertDto>>(adverts);

            var totalCount = baseQuery.Count();

            var result = new PagedResult<AdvertDto>(advertsDto,totalCount ,query.pageSize, query.pageNumber);

            return result;
        }

        public AdvertDto GetById(int id)
        {
            var advert = _dbContext.Adverts
                .Include(a => a.User)
                .Include(b => b.Category)
                .Include(c => c.Comments)
                .ThenInclude(a => a.User)
                .FirstOrDefault(d => d.Id == id);

            if (advert is null)
            {
                throw new NotFoundException("Advert doesnt exist");
            }
            var advertDto = _mapper.Map<AdvertDto>(advert);

            return advertDto; 
        }

        public int Create(CreateAdvertDto dto)
        {
            var result = _dbContext.Categories.FirstOrDefault(a => a.Name.ToLower() == dto.CategoryName.ToLower());
            var advert = new Advert()
            {
                CategoryId = result.Id,
                Description = dto.Description,
                UserId = _contextService.GetUseId
            };
            _dbContext.Adverts.Add(advert);
            _dbContext.SaveChanges();

            return advert.Id;
        }
        public void Delete(int id)
        {
            var advert = _dbContext.Adverts.FirstOrDefault(a => a.Id == id);
            if(advert is null)
            {
                throw new NotFoundException("Advert doesnt exist");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_contextService.User, advert,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if(!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("You dont have access");
            }

            _dbContext.Adverts.Remove(advert);
            _dbContext.SaveChanges();
        }
        public void Update(UpdateAdvertDto dto, int id)
        {

            var advert = _dbContext.Adverts.FirstOrDefault(a => a.Id == id);
            if(advert is null)
            {
                throw new NotFoundException("Advert doesnt exist");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_contextService.User, advert,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if(!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("You dont have access");
            }

            advert.Description = dto.Description;

            _dbContext.SaveChanges();
        }
    }
}
