using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoListDal;
using TodoListDal.Repo.Interfaces;

namespace ToDoListConsole
{
    public class UserInterface
    {
        private readonly IListRepository _listRepository;
        private readonly ITaskRepository _taskRepository;

        public UserInterface(IListRepository listRepository, ITaskRepository taskRepository)
        {
            _listRepository = listRepository;
            _taskRepository = taskRepository;
        }

        public void Menu()
        {
            int input = int.MaxValue;
            while (input != 0)
            {
                Console.Clear();
                Console.WriteLine("++=============================================================================++\n" +
                                  "||                              TODO LIST MANAGER                              ||\n" +
                                  "++=============================================================================++");
                Console.WriteLine("|| Your Todo Lists:\n" +
                                  "||\n" +
                                  "|| ID - Title");
                GetAllLists();
                Console.WriteLine("||-----------------------------------------------------------------------------++");
                Console.WriteLine("|| Choose an option:                                                           ||");
                Console.WriteLine("|| [1] Create new TODO-List                                                    ||");
                Console.WriteLine("|| [2] Select TODO-List                                                        ||");
                Console.WriteLine("|| [0] Exit                                                                    ||");
                Console.WriteLine("++-----------------------------------------------------------------------------++");

                var numberOfOptions = 2;
                input = ValidationOptionInput(input, numberOfOptions);

                switch (input)
                {
                    case 1:
                        CreateNewList();
                        break;
                    case 2:
                        ListManager();
                        break;
                }
                input = input == 0 ? 0 : int.MaxValue;
                Console.Write("   Tap button to conitnue... ");
                Console.ReadLine();
            }
        }

        public void ListManager()
        {
            int listId = ValidationListId();
            int input = int.MaxValue;
            while (input != 0)
            {
                Console.Clear();
                Console.WriteLine("++=============================================================================++\n" +
                                  "||                                LIST MANAGER                                 ||\n" +
                                  "++=============================================================================++");
                GetTasks(listId);
                Console.WriteLine("++-----------------------------------------------------------------------------++");
                Console.WriteLine("|| Choose an option:                                                           ||");
                Console.WriteLine("|| [1] Change title of current TODO-List                                       ||");
                Console.WriteLine("|| [2] Add task to current TODO-List                                           ||");
                Console.WriteLine("|| [3] Update tasks of current TODO-List                                       ||");
                Console.WriteLine("|| [4] Delete current TODO-List                                                ||");
                Console.WriteLine("|| [0] Back                                                                    ||");
                Console.WriteLine("++-----------------------------------------------------------------------------++");

                var numberOfOptions = 4;
                input = ValidationOptionInput(input, numberOfOptions);

                switch (input)
                {
                    case 1:
                        ChangeTitleList(listId);
                        break;
                    case 2:
                        AddTask(listId);
                        break;
                    case 3:
                        TaskManager();
                        break;
                    case 4:
                        DeleteList(listId);
                        input = 0;
                        break;
                }
                input = input == 0 ? 0 : int.MaxValue;
            }
        }

        public void TaskManager()
        {
            int taskId = ValidationTaskId();

            int input = int.MaxValue;
            while (input != 0)
            {
                Console.Clear();
                Console.WriteLine("++=============================================================================++\n" +
                                  "||                                TASK MANAGER                                 ||\n" +
                                  "++=============================================================================++");
                TaskInfo(taskId);
                Console.WriteLine("++-----------------------------------------------------------------------------++");
                Console.WriteLine("|| Choose an option:                                                           ||");
                Console.WriteLine("|| [1] Change title of current TASK                                            ||");
                Console.WriteLine("|| [2] Change description of current TASK                                      ||");
                Console.WriteLine("|| [3] Change Due Date of current TASK                                         ||");
                Console.WriteLine("|| [4] Change Status of current TASK                                           ||");
                Console.WriteLine("|| [5] Change Visibility of current TASK                                       ||");
                Console.WriteLine("|| [6] Delete current TASK                                                     ||");
                Console.WriteLine("|| [0] Back                                                                    ||");
                Console.WriteLine("++-----------------------------------------------------------------------------++");

                var numberOfOptions = 6;
                input = ValidationOptionInput(input, numberOfOptions);

                switch (input)
                {
                    case 1:
                        ChangeTitleTask(taskId);
                        break;
                    case 2:
                        ChangeDescriptionTask(taskId);
                        break;
                    case 3:
                        ChangeDueDateTask(taskId);
                        break;
                    case 4:
                        ChangeStatusTask(taskId);
                        break;
                    case 5:
                        ChangeVisibilityTask(taskId);
                        break;
                    case 6:
                        DeleteTask(taskId);
                        input = 0;
                        break;
                }
                input = input == 0 ? 0 : int.MaxValue;
            }
        }

        public void GetAllLists()
        {
            var lists = _listRepository.GetAll().ToList();
            lists.ForEach(x => Console.WriteLine($"|| {x.ID} - {x.Title}"));
        }

        public void CreateNewList()
        {
            Console.Write("   Enter the title of new TODO-List: ");
            var title = Console.ReadLine();
            TodoList newList = new TodoList { Title = title, CreationDate = DateTime.Today, IsVisible = true, CompletedTasksVisible = true };
            _listRepository.Add(newList);
        }

        public void GetTasks(int listId)
        {
            var tasks = _taskRepository.GetTasks(listId).Where(x => x.IsVisible);
            var title = _listRepository.GetById(listId).Title;

            Console.WriteLine($"++==================\n" +
                              $"||  List Title : {title}\n" +
                              $"++==================");

            foreach (Task task in tasks)
            {
                Console.WriteLine($"+--------------\n" +
                                  $"|  ID : {task.ID}\n" +
                                  $"|  Title: {task.Title}\n" +
                                  $"|  Description: {task.Description}\n" +
                                  $"|  Due Date: {task.DueDate}\n" +
                                  $"|  Status: {task.Status}\n" +
                                  $"+--------------");
            }
        }

        public void TaskInfo(int taskId)
        {
            var task = _taskRepository.Get(taskId);
            Console.WriteLine($"++==================\n" +
                              $"|  ID : {task.ID}\n" +
                              $"|  Title: {task.Title}\n" +
                              $"|  Description: {task.Description}\n" +
                              $"|  Due Date: {task.DueDate}\n" +
                              $"|  Status: {task.Status}\n" +
                              $"++==================");
        }

        public void DeleteList(int listId)
        {
            _listRepository.Delete(listId);
        }

        public void ChangeTitleList(int listId)
        {
            Console.Write("   Enter the NEW title of current TODO-List: ");
            var newTitle = Console.ReadLine();
            TodoList currentList = new TodoList { ID = listId, Title = newTitle };
            _listRepository.Rename(currentList);
        }

        public void AddTask(int listId)
        {
            Console.Write("   Enter the title of new TASK: ");
            var title = Console.ReadLine();
            Console.Write("   Enter the description: ");
            var description = Console.ReadLine();
            Console.Write("   Enter the Due Date: ");
            var dueDate = ValidationDateInput(Console.ReadLine());

            Task newTask = new Task { Title = title, Description = description, DueDate = dueDate, ToDoListID = listId, IsVisible = true, IsListVisible = true};
            _taskRepository.Add(newTask);
        }

        public void ChangeTitleTask(int taskId)
        {
            Console.Write("   Enter the NEW title of choosen Task: ");
            var newTitle = Console.ReadLine();


            Task currentTask = _taskRepository.Get(taskId);
            currentTask.Title = newTitle;

            _taskRepository.Edit(currentTask);
        }

        public void ChangeDescriptionTask(int taskId)
        {
            Console.Write("   Enter the NEW description of choosen Task: ");
            var newDescription = Console.ReadLine();

            Task currentTask = _taskRepository.Get(taskId);
            currentTask.Description = newDescription;

            _taskRepository.Edit(currentTask);
        }

        public void ChangeDueDateTask(int taskId)
        {
            Console.Write("   Enter the NEW Due Date of choosen Task: ");
            var newDueDate = ValidationDateInput(Console.ReadLine());

            Task currentTask = _taskRepository.Get(taskId);
            currentTask.DueDate = newDueDate;

            _taskRepository.Edit(currentTask);
        }

        public void ChangeStatusTask(int taskId)
        {
            Task currentTask = _taskRepository.Get(taskId);
            currentTask.Status = currentTask.Status == "Not Started" ? "Completed" : "Not Started";

            _taskRepository.Edit(currentTask);
        }
        public void ChangeVisibilityTask(int taskId)
        {
            Task currentTask = _taskRepository.Get(taskId);
            currentTask.IsVisible = !currentTask.IsVisible;

            _taskRepository.Edit(currentTask);
        }

        public void DeleteTask(int taskId)
        {
            _taskRepository.Delete(taskId);
        }

        public static int ValidationOptionInput(int input, int higherValue)
        {
            while (input < 0 || input > higherValue)
            {
                Console.Write("   Select an option: ");
                try
                {
                    input = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("   Enter a valid number!");
                }
            }
            return input;
        }

        public int ValidationListId()
        {
            int listId;
            while (true)
            {
                Console.Write("   Enter the ID of TODO-List: ");
                try
                {
                    listId = int.Parse(Console.ReadLine());
                    _listRepository.ValidateId(listId);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("   List ID have to be a number");
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("   List ID not valid");
                }
            }
            return listId;
        }

        public int ValidationTaskId()
        {
            int taskId;
            while (true)
            {
                Console.Write("   Enter the ID of TASK to be updated:");
                try
                {
                    taskId = int.Parse(Console.ReadLine());
                    _taskRepository.ValidateId(taskId);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("   Enter a valid number!");
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("   Task ID not valid");
                }
            }
            return taskId;
        }

        public static DateTime ValidationDateInput(string input)
        {
            DateTime date;
            while (true)
            {
                try
                {
                    date = DateTime.Parse(input);
                    break;
                }
                catch (FormatException)
                {
                    Console.Write("   Invalid Date Format. Try again: ");
                    input = Console.ReadLine();
                }
            }
            return date;
        }
    }
}
