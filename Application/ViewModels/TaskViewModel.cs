using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "No se debe dejar el campo vacío.")]
        public string Name { get; set; }
    }
}
