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
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Title must be between 3 and 50 characters long")]
        public string Title { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Description must be between 3 and 100 characters long")]
        public string Description { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        [StringLength(100,
            ErrorMessage = "Maximum 100 characters long")]
        public string Notes { get; set; }
        public bool IsVisible { get; set; }
        public bool IsListVisible { get; set; }
        public bool HasReminder { get; set; }
        public int ReminderDuration { get; set; }
        public string User { get; set; }
        public DateTime CreationDate { get; set; }
        public int ToDoListID { get; set; }
        public bool EditMode { get; set; }       
        public TodoList TodoList { get; set; }
    }
}
