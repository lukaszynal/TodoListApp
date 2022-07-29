using System.Collections.Generic;
using System.Linq;

namespace TodoListDal.Repo.Interfaces
{
    public interface ITaskRepository
    {
        void Add(Task newTask);
        List<Task> GetAll();
        List<Task> GetTasks(int listId);
        Task Get(int taskId);
        void Edit(Task currentTask);
        void UpdateStatus(Task currentTask);
        void UpdateVisible(Task currentTask);
        void Reminder(Task currentTask);
        void Delete(int taskId);
        void ClearList(int listId);
        Task ValidateId(int taskId);
    }
}
