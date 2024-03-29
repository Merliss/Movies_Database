﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;
using Movies_Database.Entities;
using Movies_Database.Exceptions;
using Movies_Database.Models;

namespace Movies_Database.Services
{

    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);

        bool Delete(DeleteUserDto userDto);
    }
    public class AccountService : IAccountService
    {
        private readonly MovieDbContext _context;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly MovieDbContext _movieDbContext;

        public AccountService(MovieDbContext context, IPasswordHasher<Users> passwordHasher, AuthenticationSettings authenticationSettings, MovieDbContext movieDbContext)
        {

            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _movieDbContext = movieDbContext;

        }
        public void RegisterUser(RegisterUserDto dto)
        {
            
            var newUser = new Users()
            {
                Email = dto.Email,
                Username = dto.Username,
                RoleId = 2 // default user
            };


            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _context.Users
                .Include(x => x.Role)
                .FirstOrDefault(u => u.Username == dto.Username);

            if (user == null)
            {
                throw new BadRequestException("Invalid data from user (username or password)");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid data from user (username or password)");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Username} {user.Email}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);


            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires:  expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public bool Delete(DeleteUserDto userDto)
        {
            var user = _movieDbContext
                .Users
                .FirstOrDefault(u => u.Id == userDto.UserId && u.Username == userDto.UserName && u.Email == userDto.UserEmail && u.RoleId == userDto.RoleId);

            if (user == null)
            {
                return false;
            }

            _movieDbContext.Users.Remove(user);
            _movieDbContext.SaveChanges();
            return true;

        }

    }
}
