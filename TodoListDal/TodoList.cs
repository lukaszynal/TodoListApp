using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoListDal
{
    public class TodoList
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Title must be between 3 and 100 characters long")]
        public string Title { get; set; }
        public string User { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsVisible { get; set; }
        public bool CompletedTasksVisible { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
