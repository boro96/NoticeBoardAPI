using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public interface IAdvertService
    {
        AdvertDto GetById(int id);
        IEnumerable<AdvertDto> GetAll();
        int Create(CreateAdvertDto dto, int userId);
        void Delete(int id);
        void Update(UpdateAdvertDto dto, int id);
    }

    public class AdvertService : IAdvertService
    {
        private readonly NoticeBoardDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public AdvertService(NoticeBoardDbContext dbContext, IMapper mapper, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }
        public IEnumerable<AdvertDto> GetAll()
        {
            var adverts = _dbContext.Adverts
                .Include(a => a.User)
                .Include(b => b.Category)
                .Include(c => c.Comments)
                .ToList();


            var advertsDto = _mapper.Map<List<AdvertDto>>(adverts);
            return advertsDto;
        }

        public AdvertDto GetById(int id)
        {
            var advert = _dbContext.Adverts
                .Include(a => a.User)
                .Include(b => b.Category)
                .Include(c => c.Comments)
                .FirstOrDefault(d => d.Id == id);

            if (advert is null)
            {
                throw new NotFoundException("Advert doesnt exist");
            }
            var advertDto = _mapper.Map<AdvertDto>(advert);

            return advertDto; 
        }

        public int Create(CreateAdvertDto dto, int userId)
        {
            var result = _dbContext.Categories.FirstOrDefault(a => a.Name.ToLower() == dto.CategoryName.ToLower());
            var advert = new Advert()
            {
                CategoryId = result.Id,
                Description = dto.Description,
                UserId = userId
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

            _dbContext.Adverts.Remove(advert);
            _dbContext.SaveChanges();
        }
        public void Update(UpdateAdvertDto dto, int id)
        {
            _authorizationService.AuthorizeAsync();

            var advert = _dbContext.Adverts.FirstOrDefault(a => a.Id == id);
            if(advert is null)
            {
                throw new NotFoundException("Advert doesnt exist");
            }
            advert.Description = dto.Description;

            _dbContext.SaveChanges();
        }
    }
}
