using AutoMapper;
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
        void Create(CreateAdvertDto dto, int userId);
    }

    public class AdvertService : IAdvertService
    {
        private readonly NoticeBoardDbContext _dbContext;
        private readonly IMapper _mapper;

        public AdvertService(NoticeBoardDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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

        public void Create(CreateAdvertDto dto, int userId)
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
        }
    }
}
