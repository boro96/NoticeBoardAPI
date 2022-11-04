using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NoticeBoardAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Services
{
    public class AdvertService
    {
        private readonly NoticeBoardDbContext _dbContext;
        private readonly IMapper _mapper;

        public AdvertService(NoticeBoardDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<Advert> GetAll()
        {
            var adverts = _dbContext.Adverts
                .Include(a => a.User)
                .Include(b => b.Category)
                .ToList();

            return adverts;
        }
    }
}
