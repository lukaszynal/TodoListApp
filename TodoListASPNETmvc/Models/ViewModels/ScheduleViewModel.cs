using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TodoListDal.Models;
using TodoListDal.Repo.Interfaces;

namespace TodoListASPNETmvc.Models.ViewModels
{
    public class ScheduleViewModel
    {
        private readonly ITaskRepository _taskRepository;

        public ScheduleViewModel(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public IEnumerable<Task> Tasks { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public TodoList CurrentList { get; set; }

        public void Today(string user, int tasksPage, int pageSize)
        {
            var todayTasks = _taskRepository.GetAll().Where(t =>
                t.UserName == user &&
                t.IsVisible &&
                t.IsListVisible &&
                t.DueDate.Date.CompareTo(DateTime.Today) == 0);

            Tasks = todayTasks
                .OrderBy(p => p.DueDate)
                .Skip((tasksPage - 1) * pageSize)
                .Take(pageSize);

            CurrentList = new TodoList
            {
                Title = "Today"
            };

            PagingInfo = new PagingInfo
            {
                CurrentPage = tasksPage,
                ItemsPerPage = pageSize,
                TotalItems = todayTasks.Count()
            };
        }

        public void Incomming(string user, int tasksPage, int pageSize)
        {
            var incommingTasks = _taskRepository.GetAll().Where(t =>
                t.UserName == user &&
                t.IsVisible &&
                t.IsListVisible &&
                t.DueDate.Date.CompareTo(DateTime.Today) > 0);

            Tasks = incommingTasks
                .OrderBy(p => p.DueDate)
                .Skip((tasksPage - 1) * pageSize)
                .Take(pageSize);

            CurrentList = new TodoList
            {
                Title = "Incomming"
            };

            PagingInfo = new PagingInfo
            {
                CurrentPage = tasksPage,
                ItemsPerPage = pageSize,
                TotalItems = incommingTasks.Count()
            };
        }
    }
}
