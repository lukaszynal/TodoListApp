using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

#pragma warning disable S1481, CS0219

namespace TodoListDal.Repo
{
    public class TodoListDataDbContext : DbContext
    {
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public TodoListDataDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string AZURE_SQL_CONNECTIONSTRING = "Data Source=todolistapp.database.windows.net,1433;Initial Catalog=TodoListDataDatabase;User ID=lukaszynal;Password=";
            string TodoListDataDbContextConnection = "Server=(localdb)\\mssqllocaldb;Database=TodoListDataDatabase;Trusted_Connection=True;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(TodoListDataDbContextConnection);
        }
    }
}
