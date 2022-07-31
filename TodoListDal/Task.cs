using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListDal
{
    public class Task
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 50 characters long")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 100 characters long")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Text)]
        public string Status { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Maximum 100 characters long")]
        public string Notes { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [Required]
        public int ToDoListID { get; set; }

        public bool IsVisible { get; set; }
        public bool IsListVisible { get; set; }
        public bool HasReminder { get; set; }
        public int ReminderDuration { get; set; }
        public string User { get; set; }
        public bool EditMode { get; set; }       
        public TodoList TodoList { get; set; }
    }
}
