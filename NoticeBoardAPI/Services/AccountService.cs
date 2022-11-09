using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(IMapper mapper, NoticeBoardDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _mapper = mapper;
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public void Register(RegisterUserDto dto)
        {
            var newUser = _mapper.Map<User>(dto);

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}
