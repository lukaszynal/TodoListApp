using System;
using System.Collections.Generic;
using System.Linq;
using TodoListDal;
using TodoListDal.Repo.Interfaces;

namespace TodoListASPNETmvc.Models.ViewModels
{
    public class TasksListViewModel
    {
        private readonly IListRepository _listRepository;
        private readonly ITaskRepository _taskRepository;

        public TasksListViewModel(IListRepository listRepository, ITaskRepository taskRepository)
        {
            _listRepository = listRepository;
            _taskRepository = taskRepository;
        }
        public IEnumerable<Task> Tasks { get; set; }
        public IEnumerable<Task> HiddenTasks { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public Task CurrentTask { get; set; }
        public TodoList CurrentList { get; set; }

        public void Initialize(string user, string listTitle, int tasksPage, int pageSize)
        {
            CurrentList = _listRepository.GetByTitle(listTitle, user);

            var allTasks = _taskRepository.GetTasks(CurrentList.ID);
            var hiddenTasksCount = allTasks.Count(x => !x.IsVisible);
            var tasksNotHidden = allTasks.Where(x => x.IsVisible);
            var tasksNotCompleted = tasksNotHidden.Where(x => x.Status != "Completed");

            int tasksToDisplay;
            if (CurrentList.CompletedTasksVisible)
            {
                Tasks = tasksNotHidden
                    .OrderBy(p => p.DueDate)
                    .Skip((tasksPage - 1) * pageSize)
                    .Take(pageSize);
                tasksToDisplay = tasksNotHidden.Count() - hiddenTasksCount;
            }
            else
            {
                Tasks = tasksNotCompleted
                    .OrderBy(p => p.DueDate)
                    .Skip((tasksPage - 1) * pageSize)
                    .Take(pageSize);
                tasksToDisplay = tasksNotCompleted.Count() - hiddenTasksCount;
            }

            HiddenTasks = allTasks.Where(x => !x.IsVisible);

            PagingInfo = new PagingInfo
            {
                CurrentPage = tasksPage,
                ItemsPerPage = pageSize,
                TotalItems = tasksToDisplay
            };

            CurrentTask = new Task
            {
                ToDoListID = CurrentList.ID,
                DueDate = DateTime.Today,
                Notes = string.Empty
            };
        }
    }
}
