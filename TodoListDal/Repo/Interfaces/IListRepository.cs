using System.Collections.Generic;
using System.Linq;

namespace TodoListDal.Repo.Interfaces
{
    public interface IListRepository
    {
        List<TodoList> GetAll();
        TodoList GetById(int listId);
        TodoList GetByTitle(string listTitle, string user);
        void Add(TodoList newList);
        void Rename(TodoList currentList);
        void Delete(int listId);
        void UpdateVisible(TodoList currentList);
        void UpdateTasksVisible(TodoList currentList);
        TodoList ValidateId(int listId);
    }
}
