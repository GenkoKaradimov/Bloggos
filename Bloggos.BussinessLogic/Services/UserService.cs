using Bloggos.BussinessLogic.IServices;
using Bloggos.BussinessLogic.Models.User;
using Bloggos.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bloggos.BussinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly BloggosDbContext _context;

        public UserService(BloggosDbContext context) 
        {
            _context = context;
        }

        public async Task<UserModel> GetUser(string username) 
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

            if (user == null) throw new ArgumentException("User not found!");

            var model = new UserModel()
            {
                Username = username,
                IsAdmin = user.IsAdmin
            };

            return model;
        }

        public async Task<UserModel> LoginUser(UserLoginModel model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == model.Username);
            if (user == null) throw new ArgumentException("User not found!");

            string hash = CalculateHash(model.Password, user.PasswordSalt);
            if(user.PasswordHash != hash) throw new ArgumentException("Password not correct!");

            return new UserModel()
            {
                Username = user.Username,
                IsAdmin = user.IsAdmin,
            };
        }

        public async Task<UserModel> RegisterUser(UserRegistrationModel model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == model.Username);
            if (user != null) throw new ArgumentException("Username is taken!");

            user = new Database.Entities.User()
            {
                Username = model.Username,
                IsAdmin = false
            };

            user.PasswordSalt = GenerateSalt();
            user.PasswordHash = CalculateHash(model.Password, user.PasswordSalt);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new UserModel()
            {
                Username = user.Username,
                IsAdmin = user.IsAdmin
            };
        }

        private string GenerateSalt()
        {
            var saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            string salt = Convert.ToBase64String(saltBytes);
            return salt;
        }

        public string CalculateHash(string password, string salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 100000, HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32);

                string hash = Convert.ToBase64String(hashBytes);

                return hash;
            }
        }

    }
}
