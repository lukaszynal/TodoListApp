using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListDal.Models
{
    public class TaskHistory
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserName { get; set; }
        public DateTime DeleteDate { get; set; }
    }
}
