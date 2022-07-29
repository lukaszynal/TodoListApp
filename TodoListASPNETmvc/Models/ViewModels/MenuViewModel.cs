using System.Collections.Generic;
using TodoListDal;

namespace TodoListASPNETmvc.Models.ViewModels
{
    public class MenuViewModel
    {
        public IEnumerable<string> ListTitles { get; set; }
        public IEnumerable<TodoList> Lists { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
    }
}
