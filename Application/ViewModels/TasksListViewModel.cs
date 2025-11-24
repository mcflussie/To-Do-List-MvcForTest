using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class TasksListViewModel
    {
        public List<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
        public List<TaskViewModel> Filtered { get; set; } = new List<TaskViewModel>();
        public string SearchTerm { get; set; }

    }
}
