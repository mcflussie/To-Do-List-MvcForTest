using System;
using Application.Entities;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using ToDoList_Mvc.Data;

namespace ToDoList_Mvc.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ToDoListDbContext _context;

        public TaskRepository(ToDoListDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskToDo>> GetAllByUserAsync(Guid userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<TaskToDo?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task AddAsync(TaskToDo task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public Task UpdateAsync(TaskToDo task)
        {
            _context.Tasks.Update(task);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TaskToDo task)
        {
            _context.Tasks.Remove(task);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
