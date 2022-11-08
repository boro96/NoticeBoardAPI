using AutoMapper;
using NoticeBoardAPI.Entities;
using NoticeBoardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Services
{
    public interface IAccountService
    {
        void Register(RegisterUserDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly NoticeBoardDbContext _context;

        public AccountService(IMapper mapper, NoticeBoardDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public void Register(RegisterUserDto dto)
        {
            var newUser = _mapper.Map<User>(dto);

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}
