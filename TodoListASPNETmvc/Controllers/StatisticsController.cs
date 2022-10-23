using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TodoListASPNETmvc.Models.ViewModels;
using TodoListDal.Repo;

namespace TodoListASPNETmvc.Controllers
{
    [Route("/[controller]")]
    public class StatisticsController : Controller
    {
        private readonly TodoListDataDbContext _dbContext;

        public StatisticsController(TodoListDataDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var model = new StatisticsViewModel
            {
                User = User.Identity.Name,
                AllTasks = _dbContext.AllTasks.ToList()              
            };

            return View(model);
        }
    }
}
