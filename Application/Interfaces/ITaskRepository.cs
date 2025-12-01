using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;

namespace Application.Interfaces
{
    public interface ITaskRepository
    {

            Task<List<TaskToDo>> GetAllByUserAsync(Guid userId);
            Task<TaskToDo?> GetByIdAsync(Guid id, Guid userId);

            Task AddAsync(TaskToDo task);
            Task UpdateAsync(TaskToDo task);
            Task DeleteAsync(TaskToDo task);

            Task SaveAsync();
        

    }
}
