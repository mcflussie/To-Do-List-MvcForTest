using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Entities;
using Application.Interfaces;
using Application.ViewModels;

namespace Application.Services
{
    public class TaskServices
    {
        private readonly ITaskRepository _repo;

        public TaskServices(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<TaskViewModel>> GetAllAsync(Guid userId)
        {
            var tasks = await _repo.GetAllByUserAsync(userId);
            return tasks.Select(t => new TaskViewModel
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();
        }

        public async Task AddTaskAsync(string name, Guid userId)
        {
            var task = new TaskToDo
            {
                Id = Guid.NewGuid(),
                Name = name,
                UserId = userId
            };

            await _repo.AddAsync(task);
            await _repo.SaveAsync();
        }

        public async Task EditTaskAsync(Guid userId, Guid taskId, string newName)
        {
            var task = await _repo.GetByIdAsync(taskId, userId);
            if (task == null) return;

            task.Name = newName;
            await _repo.UpdateAsync(task);
            await _repo.SaveAsync();
        }

        public async Task DeleteTaskAsync(Guid userId, Guid taskId)
        {
            var task = await _repo.GetByIdAsync(taskId, userId);
            if (task == null) return;

            await _repo.DeleteAsync(task);
            await _repo.SaveAsync();
        }
    }
}