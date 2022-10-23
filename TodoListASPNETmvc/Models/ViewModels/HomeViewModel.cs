using System.Collections.Generic;
using System.Linq;
using TodoListDal.Models;
using TodoListDal.Repo.Interfaces;

namespace TodoListASPNETmvc.Models.ViewModels
{
    public class HomeViewModel
    {
        private readonly IListRepository _listRepository;
        private readonly ITaskRepository _taskRepository;

        public HomeViewModel(IListRepository listRepository, ITaskRepository taskRepository)
        {
            _listRepository = listRepository;
            _taskRepository = taskRepository;
        }
        public IEnumerable<TodoList> HiddenLists { get; set; }
        public IEnumerable<Task> ReminderTasks { get; set; }

        public void Initialize(string user)
        {
            ReminderTasks = _taskRepository.GetAll()
                .Where(x => x.UserName == user &&
                            x.IsVisible &&
                            x.IsListVisible &&
                            x.Status != "Completed" &&
                            x.HasReminder);

            HiddenLists = _listRepository.GetAll()
                .Where(x => x.User == user && !x.IsVisible);
        }
    }
}
