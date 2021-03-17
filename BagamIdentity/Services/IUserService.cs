using BagamIdentity.Entities;
using BagamIdentity.Models;
using BagamIdentity.Repositories.Impl;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BagamIdentity.Services
{
    public interface IUserService
    {
        User GetUserById(Guid id);
        IEnumerable<User> GetAllUsers();
        User AddUser(User user);
        AuthenticationResponse Authenticate(string username, string password);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User AddUser(User user)
        {
            user.Id = Guid.NewGuid();
            user.Role = Role.User;
            user.TimeAdded = DateTime.UtcNow;
            return _repository.Add(user);
        }

        public AuthenticationResponse Authenticate(string username, string password)
        {
            var user = _repository.GetAll().Where(u => u.Username == username && u.Password == password).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ciubi si vasluineii 2");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Role = user.Role,
                Token = tokenHandler.WriteToken(token)
            };
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _repository.GetAll();
        }

        public User GetUserById(Guid id)
        {
            return _repository.GetById(id);
        }
    }
}
