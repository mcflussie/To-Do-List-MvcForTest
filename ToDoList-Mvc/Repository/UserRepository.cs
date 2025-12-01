using Application.Entities;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using ToDoList_Mvc.Data;

namespace ToDoList_Mvc.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ToDoListDbContext _context;

        public UserRepository(ToDoListDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
