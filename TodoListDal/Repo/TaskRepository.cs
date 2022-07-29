using System.Collections.Generic;
using System.Linq;
using TodoListDal.Repo.Interfaces;

namespace TodoListDal.Repo
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoListDataDbContext context;

        public TaskRepository(TodoListDataDbContext ctx)
        {
            context = ctx;
        }

        public void Add(Task newTask)
        {
            context.Tasks.Add(newTask);
            context.SaveChanges();
        }

        public List<Task> GetAll()
        {
            return context.Tasks.ToList();
        }

        public List<Task> GetTasks(int listId)
        {
            return context.Tasks.Where(x => x.ToDoListID == listId).ToList();
        }

        public Task Get(int taskId)
        {
            return context.Tasks.First(x => x.ID == taskId);
        }

        public void Edit(Task currentTask)
        {
            var task = context.Tasks.First(x => x.ID == currentTask.ID);
            task.Title = currentTask.Title;
            task.Description = currentTask.Description;
            task.Notes = currentTask.Notes;
            task.DueDate = currentTask.DueDate;
            task.Status = currentTask.Status;
            task.IsVisible = currentTask.IsVisible;
            task.HasReminder = currentTask.HasReminder;
            task.ReminderDuration = currentTask.ReminderDuration;
            context.SaveChanges();
        }

        public void UpdateStatus(Task currentTask)
        {
            var task = context.Tasks.First(x => x.ID == currentTask.ID);
            task.Status = currentTask.Status;
            context.SaveChanges();
        }

        public void Reminder(Task currentTask)
        {
            var task = context.Tasks.First(x => x.ID == currentTask.ID);
            task.HasReminder = currentTask.HasReminder;
            task.ReminderDuration = currentTask.ReminderDuration;
            context.SaveChanges();
        }

        public void UpdateVisible(Task currentTask)
        {
            var task = context.Tasks.First(x => x.ID == currentTask.ID);
            task.IsVisible = currentTask.IsVisible;
            context.SaveChanges();
        }

        public void Delete(int taskId)
        {
            context.Tasks.Where(x => x.ID == taskId).DeleteFromQuery();
        }

        public void ClearList(int listId)
        {
            context.Tasks.Where(x => x.ToDoListID == listId).DeleteFromQuery();
        }

        public Task ValidateId(int taskId)
        {
            return context.Tasks.Single(x => x.ID == taskId);
        }
    }
}
