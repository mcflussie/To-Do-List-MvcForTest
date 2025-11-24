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
    }
}
