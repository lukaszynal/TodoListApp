using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TodoListDal.Models;
using TodoListDal.Repo.Interfaces;

namespace TodoListASPNETmvc.Controllers
{
    public class TaskManagerController : Controller
    {
        private readonly IListRepository _listRepository;
        private readonly ITaskRepository _taskRepository;

        public TaskManagerController(IListRepository listRepository, ITaskRepository taskRepository)
        {
            _listRepository = listRepository ?? throw new ArgumentNullException(nameof(listRepository));
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        public RedirectToRouteResult UpdateStatus(Task CurrentTask)
        {
            _taskRepository.UpdateStatus(CurrentTask);
            string listTitle = _listRepository.GetById(CurrentTask.ToDoListID).Title;

            var routeValue = new RouteValueDictionary
                (new { action = "Index", controller = "ListManager", listTitle, tasksPage = 1 });
            return RedirectToRoute(routeValue);
        }

        [HttpPost]
        public ViewResult Edit(Task CurrentTask)
        {
            if (!ModelState.IsValid || CurrentTask.EditMode)
            {
                return View(CurrentTask);
            }

            CurrentTask.EditMode = false;
            _taskRepository.Edit(CurrentTask);

            return View("Updated");
        }

        public RedirectToRouteResult UpdateVisible(Task CurrentTask)
        {
            _taskRepository.UpdateVisible(CurrentTask);
            string listTitle = _listRepository.GetById(CurrentTask.ToDoListID).Title;

            var routeValue = new RouteValueDictionary
                (new { action = "Index", controller = "ListManager", listTitle, tasksPage = 1 });
            return RedirectToRoute(routeValue);
        }

        [HttpPost]
        public IActionResult Delete(Task CurrentTask)
        {
            _taskRepository.Delete(CurrentTask.ID);

            return View();
        }

        public RedirectToRouteResult Reminder(Task CurrentTask)
        {
            _taskRepository.Reminder(CurrentTask);
            string listTitle = _listRepository.GetById(CurrentTask.ToDoListID).Title;

            var routeValue = new RouteValueDictionary
                (new { action = "Index", controller = "ListManager", listTitle, tasksPage = 1 });
            return RedirectToRoute(routeValue);
        }
    }
}
