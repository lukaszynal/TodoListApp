using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoListASPNETmvc.Models.ViewModels;
using TodoListDal.Repo.Interfaces;

namespace TodoListASPNETmvc.Components
{
    public class ListMenuViewComponent : ViewComponent
    {
        private readonly IListRepository _listRepository;
        private readonly ITaskRepository _taskRepository;

        public ListMenuViewComponent(IListRepository listRepository, ITaskRepository taskRepository)
        {
            _listRepository = listRepository;
            _taskRepository = taskRepository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedList = RouteData?.Values["listTitle"];
            return View(new MenuViewModel
            {
                ListTitles = _listRepository.GetAll()
               .Where(x => x.User == User.Identity.Name && x.IsVisible)
               .Select(x => x.Title)
               .OrderBy(x => x),

                Lists = _listRepository.GetAll()
                .Where(t => t.User == User.Identity.Name && t.IsVisible),

                Tasks = _taskRepository.GetAll()
                .Where(t => t.User == User.Identity.Name && t.IsVisible && t.IsListVisible),
            }) ;


        }
    }
}
