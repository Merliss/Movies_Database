using Microsoft.AspNetCore.Identity;
using Movies_Database.Entities;
using Movies_Database.Models;

namespace Movies_Database.Services
{

    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly MovieDbContext _context;
        private readonly IPasswordHasher<Users> _passwordHasher;

        


        public AccountService(MovieDbContext context, IPasswordHasher<Users> passwordHasher)
        {

            _context = context;
            _passwordHasher = passwordHasher;

        }
        public void RegisterUser(RegisterUserDto dto)
        {
            
            var newUser = new Users()
            {
                Email = dto.Email,
                Username = dto.Username,
                RoleId = dto.RoleId
            };


            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }


    }
}
