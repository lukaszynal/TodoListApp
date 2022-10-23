using System.Collections.Generic;
using System.Linq;
using TodoListDal.Models;
using TodoListDal.Repo.Interfaces;

namespace TodoListASPNETmvc.Models.ViewModels
{
    public class StatisticsViewModel
    {
        public IEnumerable<AllTasks> AllTasks { get; set; }
        public string User { get; set; }
    }
}
