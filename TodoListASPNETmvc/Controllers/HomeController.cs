using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListASPNETmvc.Models;
using TodoListASPNETmvc.Models.ViewModels;
using TodoListDal.Repo.Interfaces;

namespace TodoListASPNETmvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public const int PageSize = 5;
        private readonly IListRepository _listRepository;
        private readonly ITaskRepository _taskRepository;

        public HomeController(IListRepository listRepository, ITaskRepository taskRepository)
        {
            _listRepository = listRepository ?? throw new ArgumentNullException(nameof(listRepository));
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        public ViewResult Index()
        {
            var user = User.Identity.Name;
            var homeViewModel = new HomeViewModel(_listRepository, _taskRepository);
            homeViewModel.Initialize(user);
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
