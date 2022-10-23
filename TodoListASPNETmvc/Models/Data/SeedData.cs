using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TodoListDal.Models;
using TodoListDal.Repo;

namespace TodoListASPNETmvc.Models.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            TodoListDataDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<TodoListDataDbContext>();
            ListRepository listRepository = new ListRepository(context);
            TaskRepository taskRepository = new TaskRepository(context);

            listRepository.Add(new TodoList()
            {
                Title = "School",
                User = "lukaszynal@gmail.com",
                IsVisible = true,
                CompletedTasksVisible = true,
                CreationDate = DateTime.Today
            });

            listRepository.Add(new TodoList()
            {
                Title = "Programming",
                User = "lukaszynal@gmail.com",
                IsVisible = true,
                CompletedTasksVisible = true,
                CreationDate = DateTime.Today
            });

            listRepository.Add(new TodoList()
            {
                Title = "ASP.Net",
                User = "epam@gmail.com",
                IsVisible = true,
                CompletedTasksVisible = true,
                CreationDate = DateTime.Today
            });

            int schoolList = listRepository.GetByTitle("School", "lukaszynal@gmail.com").ID;
            int programmingList = listRepository.GetByTitle("Programming", "lukaszynal@gmail.com").ID;
            int aspList = listRepository.GetByTitle("ASP.Net", "epam@gmail.com").ID;

            taskRepository.Add(new Task()
            {
                Title = "Wake Up",
                Description = "Need to wake up.",
                Notes = string.Empty,
                DueDate = new DateTime(2022, 09, 01, 08, 00, 00),
                Status = "Not Started",
                UserName = "lukaszynal@gmail.com",
                CreationDate = DateTime.Now,
                IsVisible = true,
                IsListVisible = true,
                HasReminder = false,
                ReminderDuration = 0,
                ToDoListID = schoolList
            });
            taskRepository.Add(new Task()
            {
                Title = "Eat Breakfast",
                Description = "Need to eat something.",
                Notes = string.Empty,
                DueDate = new DateTime(2022, 09, 01, 08, 15, 00),
                Status = "Not Started",
                UserName = "lukaszynal@gmail.com",
                CreationDate = DateTime.Now,
                IsVisible = true,
                IsListVisible = true,
                HasReminder = false,
                ReminderDuration = 0,
                ToDoListID = schoolList
            });
            taskRepository.Add(new Task()
            {
                Title = "Go To School",
                Description = "I'm going to school to learn something.",
                Notes = string.Empty,
                DueDate = new DateTime(2022, 09, 01, 09, 00, 00),
                Status = "Not Started",
                UserName = "lukaszynal@gmail.com",
                CreationDate = DateTime.Now,
                IsVisible = true,
                IsListVisible = true,
                HasReminder = false,
                ReminderDuration = 0,
                ToDoListID = schoolList
            });
            taskRepository.Add(new Task()
            {
                Title = "Comeback From School",
                Description = "Home sweet home.",
                Notes = string.Empty,
                DueDate = new DateTime(2022, 09, 01, 14, 00, 00),
                Status = "Not Started",
                UserName = "lukaszynal@gmail.com",
                CreationDate = DateTime.Now,
                IsVisible = true,
                IsListVisible = true,
                HasReminder = false,
                ReminderDuration = 0,
                ToDoListID = schoolList
            });
            taskRepository.Add(new Task()
            {
                Title = "Relax",
                Description = "Play video games.",
                Notes = string.Empty,
                DueDate = new DateTime(2022, 09, 01, 15, 00, 00),
                Status = "Not Started",
                UserName = "lukaszynal@gmail.com",
                CreationDate = DateTime.Now,
                IsVisible = true,
                IsListVisible = true,
                HasReminder = false,
                ReminderDuration = 0,
                ToDoListID = schoolList
            });
            taskRepository.Add(new Task()
            {
                Title = "Eat dinner",
                Description = "Kill the hunger.",
                Notes = string.Empty,
                DueDate = new DateTime(2022, 09, 01, 20, 00, 00),
                Status = "Not Started",
                UserName = "lukaszynal@gmail.com",
                CreationDate = DateTime.Now,
                IsVisible = true,
                IsListVisible = true,
                HasReminder = false,
                ReminderDuration = 0,
                ToDoListID = schoolList
            });
            taskRepository.Add(new Task()
            {
                Title = "Sleep",
                Description = "Sleeping time.",
                Notes = string.Empty,
                DueDate = new DateTime(2022, 09, 01, 21, 00, 00),
                Status = "Not Started",
                UserName = "lukaszynal@gmail.com",
                CreationDate = DateTime.Now,
                IsVisible = true,
                IsListVisible = true,
                HasReminder = false,
                ReminderDuration = 0,
                ToDoListID = schoolList
            });

            taskRepository.Add(new Task()
            {
                Title = "Learn",
                Description = "I have to learn to code.",
                Notes = string.Empty,
                DueDate = new DateTime(2022, 07, 01, 00, 00, 00),
                Status = "Completed",
                UserName = "lukaszynal@gmail.com",
                CreationDate = DateTime.Now,
                IsVisible = true,
                IsListVisible = true,
                HasReminder = false,
                ReminderDuration = 0,
                ToDoListID = programmingList
            });
            taskRepository.Add(new Task()
            {
                Title = "Code",
                Description = "Write some code.",
                Notes = string.Empty,
                DueDate = new DateTime(2022, 07, 15, 00, 00, 00),
                Status = "In Progress",
                UserName = "lukaszynal@gmail.com",
                CreationDate = DateTime.Now,
                IsVisible = true,
                IsListVisible = true,
                HasReminder = false,
                ReminderDuration = 0,
                ToDoListID = programmingList
            });
            taskRepository.Add(new Task()
            {
                Title = "Deploy",
                Description = "Publish my app on internet",
                Notes = string.Empty,
                DueDate = new DateTime(2022, 07, 30, 00, 00, 00),
                Status = "Not Started",
                UserName = "lukaszynal@gmail.com",
                CreationDate = DateTime.Now,
                IsVisible = true,
                IsListVisible = true,
                HasReminder = false,
                ReminderDuration = 0,
                ToDoListID = programmingList
            });

            taskRepository.Add(new Task()
            {
                Title = "Introduction",
                Description = "Introduction to ASP.Net Core",
                Notes = string.Empty,
                DueDate = new DateTime(2022, 06, 26, 00, 00, 00),
                Status = "Completed",
                UserName = "epam@gmail.com",
                CreationDate = DateTime.Now,
                IsVisible = true,
                IsListVisible = true,
                HasReminder = false,
                ReminderDuration = 0,
                ToDoListID = aspList
            });
            taskRepository.Add(new Task()
            {
                Title = "Web API",
                Description = "ASP.Net Core Web Api",
                Notes = string.Empty,
                DueDate = new DateTime(2022, 07, 03, 00, 00, 00),
                Status = "Completed",
                UserName = "epam@gmail.com",
                CreationDate = DateTime.Now,
                IsVisible = true,
                IsListVisible = true,
                HasReminder = false,
                ReminderDuration = 0,
                ToDoListID = aspList
            });
            taskRepository.Add(new Task()
            {
                Title = "MVC",
                Description = "ASP.Net Core MVC",
                Notes = string.Empty,
                DueDate = new DateTime(2022, 07, 10, 00, 00, 00),
                Status = "Completed",
                UserName = "epam@gmail.com",
                CreationDate = DateTime.Now,
                IsVisible = true,
                IsListVisible = true,
                HasReminder = false,
                ReminderDuration = 0,
                ToDoListID = aspList
            });
            taskRepository.Add(new Task()
            {
                Title = "Test",
                Description = "Final Test",
                Notes = string.Empty,
                DueDate = new DateTime(2022, 07, 31, 00, 00, 00),
                Status = "Not Started",
                UserName = "epam@gmail.com",
                CreationDate = DateTime.Now,
                IsVisible = true,
                IsListVisible = true,
                HasReminder = false,
                ReminderDuration = 0,
                ToDoListID = aspList
            });
            taskRepository.Add(new Task()
            {
                Title = "Capstone Project",
                Description = "Todo List app",
                Notes = string.Empty,
                DueDate = new DateTime(2022, 07, 31, 00, 00, 00),
                Status = "In Progress",
                UserName = "epam@gmail.com",
                CreationDate = DateTime.Now,
                IsVisible = true,
                IsListVisible = true,
                HasReminder = false,
                ReminderDuration = 0,
                ToDoListID = aspList
            });
        }
    }
}
