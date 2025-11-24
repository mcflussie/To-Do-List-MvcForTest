using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.ViewModels;

namespace Application.Services
{
    public class TaskServices
    {
        public static TasksListViewModel model = new TasksListViewModel();

        public static void AddTask(string name)
        {
            TaskViewModel newTask = new TaskViewModel
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            model.Tasks.Add(newTask);
        }

        public static void EditTask(Guid id, string newName)
        {
            var taskVieja = model.Tasks.FirstOrDefault(x => x.Id == id);
            taskVieja.Name = newName;

        }

        public static void DeleteTask(Guid id)
        {
            var task = model.Tasks.FirstOrDefault(t => t.Id == id);
            model.Tasks.Remove(task);
        }
    }
}
