using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoListDal
{
    public class TodoList
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters long")]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        public string User { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        public bool IsVisible { get; set; }
        public bool CompletedTasksVisible { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
