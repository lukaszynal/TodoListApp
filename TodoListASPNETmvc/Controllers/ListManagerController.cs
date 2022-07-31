using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TodoListASPNETmvc.Models.ViewModels;
using TodoListDal;
using TodoListDal.Repo.Interfaces;

namespace TodoListASPNETmvc.Controllers
{
    [Authorize]
    public class ListManagerController : Controller
    {
        public const int PageSize = 5;
        private readonly IListRepository _listRepository;
        private readonly ITaskRepository _taskRepository;

        public ListManagerController(IListRepository listRepository, ITaskRepository taskRepository)
        {
            _listRepository = listRepository ?? throw new ArgumentNullException(nameof(listRepository));
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        public ViewResult Index(string ListTitle, int TasksPage = 1)
        {
            var user = User.Identity.Name;
            var scheduleViewModel = new ScheduleViewModel(_taskRepository);

            if (ListTitle == "Today")
            {
                scheduleViewModel.Today(user, TasksPage, PageSize);
                return View("Today", scheduleViewModel);
            }
            if (ListTitle == "Incomming")
            {
                scheduleViewModel.Incomming(user, TasksPage, PageSize);
                return View("Incomming", scheduleViewModel);
            }

            var viewModel = new TasksListViewModel(_listRepository, _taskRepository);
            viewModel.Initialize(user, ListTitle, TasksPage, PageSize);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TodoList NewList)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            NewList.User = User.Identity.Name;
            NewList.CreationDate = DateTime.Today;
            NewList.IsVisible = true;
            NewList.CompletedTasksVisible = true;
            _listRepository.Add(NewList);

            return View();
        }

        public IActionResult Copy(TodoList CurrentList)
        {
            TodoList copyList = new TodoList
            {
                Title = CurrentList.Title,
                User = CurrentList.User,
                IsVisible = CurrentList.IsVisible,
                CompletedTasksVisible = CurrentList.CompletedTasksVisible,
                CreationDate = DateTime.Now
            };

            _listRepository.Add(copyList);

            List<Task> currentListTasks = _taskRepository.GetTasks(CurrentList.ID);

            foreach (var task in currentListTasks)
            {
                Task CopyTask = new Task
                {
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    User = task.User,
                    CreationDate = DateTime.Now,
                    Status = task.Status,
                    IsVisible = task.IsVisible,
                    IsListVisible = task.IsListVisible,
                    HasReminder = task.HasReminder,
                    ReminderDuration = task.ReminderDuration,
                    ToDoListID = copyList.ID
                };

                _taskRepository.Add(CopyTask);
            }

            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Clear(TodoList CurrentList)
        {
            _taskRepository.ClearList(CurrentList.ID);

            var routeValue = new RouteValueDictionary
                (new { action = "Index", controller = "ListManager", listTitle = CurrentList.Title, tasksPage = 1 });
            return RedirectToRoute(routeValue);
        }

        [HttpPost]
        public IActionResult Delete(TodoList CurrentList)
        {
            _taskRepository.ClearList(CurrentList.ID);
            _listRepository.Delete(CurrentList.ID);

            return View();
        }

        [HttpPost]
        public IActionResult Rename(TodoList CurrentList)
        {
            if (!ModelState.IsValid)
            {
                return View("RenameList_partial", CurrentList);
            }

            _listRepository.Rename(CurrentList);

            return View();
        }

        [HttpPost]
        public ViewResult AddTask(Task CurrentTask)
        {
            if (!ModelState.IsValid)
            {
                return View("AddTask_partial", CurrentTask);
            }
            CurrentTask.User = User.Identity.Name;
            CurrentTask.CreationDate = DateTime.Now;
            CurrentTask.Status = "Not Started";
            CurrentTask.IsVisible = true;
            CurrentTask.IsListVisible = true;
            CurrentTask.HasReminder = false;
            CurrentTask.ReminderDuration = 0;

            _taskRepository.Add(CurrentTask);

            return View();
        }

        [HttpPost]
        public RedirectToRouteResult IsVisible(TodoList CurrentList)
        {
            _listRepository.UpdateVisible(CurrentList);

            var routeValue = new RouteValueDictionary
                (new { action = "Index", controller = "ListManager", listTitle = CurrentList.Title, tasksPage = 1 });
            return RedirectToRoute(routeValue);
        }

        [HttpPost]
        public RedirectToRouteResult CompletedTasksVisible(TodoList CurrentList)
        {
            _listRepository.UpdateTasksVisible(CurrentList);

            var routeValue = new RouteValueDictionary
                (new { action = "Index", controller = "ListManager", listTitle = CurrentList.Title, tasksPage = 1 });
            return RedirectToRoute(routeValue);
        }
    }
}
