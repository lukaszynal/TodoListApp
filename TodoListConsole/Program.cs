using TodoListDal.Repo;
using TodoListDal.Repo.Interfaces;

namespace ToDoListConsole
{
    public static class Program
    {
        static void Main()
        {
            TodoListDataDbContext TodoDbContext = new TodoListDataDbContext();
            IListRepository listRepository = new ListRepository(TodoDbContext);
            ITaskRepository taskRepository = new TaskRepository(TodoDbContext);
            UserInterface userInterface = new UserInterface(listRepository, taskRepository);
                
            userInterface.Menu();
        }
    }
}
