using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using Application.Interfaces;

namespace Application.Services
{
    public class UserServices
    {
        private readonly IUserRepository _repo;

        public UserServices(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _repo.GetByUsernameAsync(username);
            if (user == null) return null;

            if (user.Password != password) return null;

            return user;
        }

        public async Task<User> RegisterAsync(string username, string password)
        {
            var existing = await _repo.GetByUsernameAsync(username);
            if (existing != null)
                throw new Exception("Usuario ya existe");

            var user = new User { Username = username, Password = password, Tasks = new List<TaskToDo>() };
            await _repo.AddAsync(user);
            return user;
        }
    }
}
