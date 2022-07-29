using System.Collections.Generic;
using System.Linq;
using TodoListDal.Repo.Interfaces;

namespace TodoListDal.Repo
{
    public class ListRepository : IListRepository
    {
        private readonly TodoListDataDbContext context;

        public ListRepository(TodoListDataDbContext ctx)
        {
            context = ctx;
        }

        public List<TodoList> GetAll()
        {
            return context.TodoLists.ToList();
        }

        public TodoList GetById(int listId)
        {
            return context.TodoLists.First(x => x.ID == listId);
        }

        public TodoList GetByTitle(string listTitle)
        {
            return context.TodoLists.First(x => x.Title == listTitle);
        }

        public void Add(TodoList newList)
        {
            string title = newList.Title;
            string user = newList.User;
            if (!BeUniqueTitle(title, user))
            {
                var listsStartWithSameTitle = GetAll().Count(x => x.Title.StartsWith(title) && x.User == user);
                var newValue = listsStartWithSameTitle.ToString();

                newList.Title = title + " (" + newValue + ")";
            }
            context.TodoLists.Add(newList);
            context.SaveChanges();
        }

        public void Delete(int listId)
        {
            context.TodoLists.Where(x => x.ID == listId).DeleteFromQuery();
        }

        public void Rename(TodoList currentList)
        {
            var list = context.TodoLists.First(x => x.ID == currentList.ID);
            list.Title = currentList.Title;
            context.SaveChanges();
        }

        public void UpdateVisible(TodoList currentList)
        {
            var list = context.TodoLists.First(x => x.ID == currentList.ID);
            list.IsVisible = currentList.IsVisible;

            var tasks = context.Tasks.Where(x => x.ToDoListID == currentList.ID).ToList();
            foreach (var task in tasks)
            {
                task.IsListVisible = currentList.IsVisible;
            }

            context.SaveChanges();           
        }

        public void UpdateTasksVisible(TodoList currentList)
        {
            var list = context.TodoLists.First(x => x.ID == currentList.ID);
            list.CompletedTasksVisible = currentList.CompletedTasksVisible;
            context.SaveChanges();
        }

        public TodoList ValidateId(int listId)
        {
            return context.TodoLists.Single(x => x.ID == listId);
        }

        private bool BeUniqueTitle(string title, string user)
        {
            return context.TodoLists.Where(x => x.Title.StartsWith(title)).FirstOrDefault(x => x.Title == title && x.User == user) == null;
        }
    }
}
