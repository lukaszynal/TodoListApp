using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TodoListDal.Models;

#pragma warning disable S1481, CS0219

namespace TodoListDal.Repo
{
    public class TodoListDataDbContext : DbContext
    {
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskHistory> TasksHistory { get; set; }
        public DbSet<AllTasks> AllTasks { get; set; }

        public TodoListDataDbContext()
        {
            Database.EnsureCreated();
            Database.ExecuteSqlRaw(@"DROP TRIGGER IF EXISTS [TasksDeleted]");
            Database.ExecuteSqlRaw(@"CREATE TRIGGER [TasksDeleted]
                                     ON[dbo].[Tasks]
                                     FOR DELETE
                                     AS
                                     BEGIN
                                        INSERT INTO TasksHistory (TaskID, Title, Description, DueDate, Status, Notes, CreationDate, UserName, DeleteDate)
                                        SELECT Tasks.ID, Tasks.Title, Tasks.Description, Tasks.DueDate, Tasks.Status, Tasks.Notes, Tasks.CreationDate, Tasks.UserName, GETDATE()
                                        FROM deleted Tasks
                                     END");
            Database.ExecuteSqlRaw(@"DROP VIEW IF EXISTS [vwAllTasks]");
            Database.ExecuteSqlRaw(@"CREATE VIEW[dbo].[vwAllTasks]
                                    AS SELECT Tasks.Title, Tasks.Status, Tasks.UserName FROM [Tasks]");          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<AllTasks>(eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("vwAllTasks");
                });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string TodoListDataDbContextConnection = "Server=(localdb)\\mssqllocaldb;Database=TodoListDataDatabase;Trusted_Connection=True;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(TodoListDataDbContextConnection);
        }
    }
}
