using System;
using System.Linq;
using NUnit.Framework;
using TodoListDal.Models;
using TodoListDal.Repo;
using TodoListDal.Repo.Interfaces;

#pragma warning disable SA1600

namespace TodoListApp.Tests
{
    [TestFixture]
    public class Tests
    {
        private IListRepository listRepository;
        private ITaskRepository taskRepository;

        [SetUp]
        public void Init()
        {
            TodoListDataDbContext todoDbContext = new TodoListDataDbContext();
            IListRepository listRepo = new ListRepository(todoDbContext);
            ITaskRepository taskRepo = new TaskRepository(todoDbContext);
            this.listRepository = listRepo;
            this.taskRepository = taskRepo;
        }

        [TestCase("TestList", "testUser@asp.net", "01.01.2020", true, true)]
        [TestCase("ListToClear", "testUser@asp.net", "01.01.2020", true, true)]
        [Order(1)]
        public void List_Add(string title, string user, DateTime creationDate, bool isVisible, bool completedTaskVisible)
        {
            int beforeAdd = this.listRepository.GetAll().Count;

            TodoList newList = new TodoList { Title = title, User = user, CreationDate = creationDate, IsVisible = isVisible, CompletedTasksVisible = completedTaskVisible };
            this.listRepository.Add(newList);

            int afterAdd = this.listRepository.GetAll().Count;

            Assert.AreEqual(afterAdd, beforeAdd + 1);
        }

        [TestCase("TestList")]
        [Order(2)]
        public void List_Compare(string title)
        {
            TodoList currentListByTitle = this.listRepository.GetByTitle(title, "testUser@asp.net");
            TodoList currentListById = this.listRepository.GetById(currentListByTitle.ID);

            Assert.AreEqual(currentListByTitle, currentListById);
        }

        [TestCase("NewTestList")]
        [Order(3)]
        public void List_Rename(string title)
        {
            TodoList currentList = this.listRepository.GetByTitle("TestList", "testUser@asp.net");
            TodoList renamedList = new TodoList() { ID = currentList.ID, Title = title, };

            this.listRepository.Rename(renamedList);

            Assert.AreEqual("NewTestList", currentList.Title);
        }

        [TestCase("NewTestList", false)]
        [Order(4)]
        public void List_UpdateVisible(string title, bool isVisible)
        {
            TodoList currentList = this.listRepository.GetByTitle(title, "testUser@asp.net");
            var beforeUpdate = currentList.IsVisible;
            TodoList updatedList = new TodoList() { ID = currentList.ID, IsVisible = isVisible, };

            this.listRepository.UpdateVisible(updatedList);
            var afterUpdate = currentList.IsVisible;

            Assert.AreNotEqual(beforeUpdate, afterUpdate);
        }

        [TestCase("NewTestList", false)]
        [Order(5)]
        public void List_UpdateTasksVisible(string title, bool completedTasksVisible)
        {
            TodoList currentList = this.listRepository.GetByTitle(title, "testUser@asp.net");
            var beforeUpdate = currentList.CompletedTasksVisible;
            TodoList updatedList = new TodoList() { ID = currentList.ID, IsVisible = completedTasksVisible, };

            this.listRepository.UpdateTasksVisible(updatedList);
            var afterUpdate = currentList.CompletedTasksVisible;

            Assert.AreNotEqual(beforeUpdate, afterUpdate);
        }

        [TestCase("NewTestList", "TestTask", "TestDescription", "01.01.2022", "Not Started", "TestNotes", true, true, 24, "testUser@asp.net", "01.01.2020")]
        [TestCase("ListToClear", "TaskNumberOne", "TestDescription", "01.01.2022", "Not Started", "TestNotes", true, true, 24, "testUser@asp.net", "01.01.2020")]
        [TestCase("ListToClear", "TaskNumberTwo", "TestDescription", "01.01.2022", "Not Started", "TestNotes", true, true, 24, "testUser@asp.net", "01.01.2020")]
        [TestCase("ListToClear", "TaskNUmberThree", "TestDescription", "01.01.2022", "Not Started", "TestNotes", true, true, 24, "testUser@asp.net", "01.01.2020")]
        [Order(6)]
        public void Task_Add(string listTitle, string title, string description, DateTime dueDate, string status, string notes, bool visible, bool hasReminder, int reminderDuration, string user, DateTime creationDate)
        {
            TodoList list = this.listRepository.GetByTitle(listTitle, "testUser@asp.net");
            int beforeAdd = this.taskRepository.GetTasks(list.ID).Count;
            Task newTask = new Task
            {
                Title = title,
                Description = description,
                DueDate = dueDate,
                Status = status,
                Notes = notes,
                IsVisible = visible,
                HasReminder = hasReminder,
                ReminderDuration = reminderDuration,
                UserName = user,
                CreationDate = creationDate,
                ToDoListID = list.ID,
            };

            this.taskRepository.Add(newTask);

            int afterAdd = this.taskRepository.GetTasks(list.ID).Count;

            Assert.AreEqual(afterAdd, beforeAdd + 1);
        }

        [TestCase("NewTestTask", "NewTestDescription", "11.11.2022", "Completed", "NewTestNotes", false, false, 12)]
        [Order(7)]
        public void Task_Edit(string title, string description, DateTime dueDate, string status, string notes, bool visible, bool hasReminder, int reminderDuration)
        {
            Task currentTask = this.taskRepository.GetAll().First(x => x.Title == "TestTask");
            Task editedTask = new Task()
            {
                ID = currentTask.ID,
                Title = title,
                Description = description,
                DueDate = dueDate,
                Status = status,
                Notes = notes,
                IsVisible = visible,
                HasReminder = hasReminder,
                ReminderDuration = reminderDuration,
                ToDoListID = currentTask.ToDoListID,
            };

            this.taskRepository.Edit(editedTask);

            Assert.AreEqual("NewTestTask", currentTask.Title);
            Assert.AreEqual("NewTestDescription", currentTask.Description);
            Assert.AreEqual(new DateTime(2022, 11, 11), currentTask.DueDate);
            Assert.AreEqual("Completed", currentTask.Status);
            Assert.AreEqual("NewTestNotes", currentTask.Notes);
            Assert.AreEqual(false, currentTask.IsVisible);
            Assert.AreEqual(false, currentTask.HasReminder);
            Assert.AreEqual(12, currentTask.ReminderDuration);
        }

        [TestCase("NewTestTask", true)]
        [Order(8)]
        public void Task_UpdateVisible(string title, bool isVisible)
        {
            Task currentTask = this.taskRepository.GetAll().First(x => x.Title == title);
            var beforeUpdate = currentTask.Status;
            Task updatedTask = new Task() { ID = currentTask.ID, IsVisible = isVisible, };

            this.taskRepository.UpdateVisible(updatedTask);
            var afterUpdate = currentTask.IsVisible;

            Assert.AreNotEqual(beforeUpdate, afterUpdate);
        }

        [TestCase("NewTestTask", "In Progress")]
        [Order(9)]
        public void Task_UpdateStatus(string title, string status)
        {
            Task currentTask = this.taskRepository.GetAll().First(x => x.Title == title);
            var beforeUpdate = currentTask.Status;
            Task updatedTask = new Task() { ID = currentTask.ID, Status = status, };

            this.taskRepository.UpdateStatus(updatedTask);
            var afterUpdate = currentTask.Status;

            Assert.AreNotEqual(beforeUpdate, afterUpdate);
        }

        [TestCase("NewTestTask", true, 1)]
        [Order(10)]
        public void Task_Reminder(string title, bool hasReminder, int reminderDuration)
        {
            Task currentTask = this.taskRepository.GetAll().First(x => x.Title == title);
            Task updatedTask = new Task() { ID = currentTask.ID, HasReminder = hasReminder, ReminderDuration = reminderDuration, };

            this.taskRepository.Reminder(updatedTask);

            Assert.AreEqual(true, currentTask.HasReminder);
            Assert.AreEqual(1, currentTask.ReminderDuration);
        }

        [TestCase("NewTestList", "NewTestTask")]
        [Order(11)]
        public void Task_Delete(string listTitle, string taskTitle)
        {
            TodoList list = this.listRepository.GetByTitle(listTitle, "testUser@asp.net");
            int beforeDelete = this.taskRepository.GetTasks(list.ID).Count;
            Task task = this.taskRepository.GetTasks(list.ID).First(x => x.Title == taskTitle);

            this.taskRepository.Delete(task.ID);

            int afterDelete = this.taskRepository.GetTasks(list.ID).Count;

            Assert.AreEqual(afterDelete, beforeDelete - 1);
        }

        [TestCase("ListToClear")]
        [Order(12)]
        public void List_Clear(string listTitle)
        {
            TodoList list = this.listRepository.GetByTitle(listTitle, "testUser@asp.net");
            int beforeClear = this.taskRepository.GetTasks(list.ID).Count;
            this.taskRepository.ClearList(list.ID);

            int afterClear = this.taskRepository.GetTasks(list.ID).Count;

            Assert.AreEqual(0, beforeClear + (afterClear - beforeClear));
        }

        [TestCase("NewTestList")]
        [TestCase("ListToClear")]
        [Order(13)]
        public void List_Delete(string title)
        {
            int beforeDelete = this.listRepository.GetAll().Count;
            TodoList list = this.listRepository.GetByTitle(title, "testUser@asp.net");

            this.listRepository.Delete(list.ID);

            int afterDelete = this.listRepository.GetAll().Count;

            Assert.AreEqual(afterDelete, beforeDelete - 1);
        }
    }
}
