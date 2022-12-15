using Task = TaskList.Data.Models.Task;

namespace TaskList.Data.DAL.Interfaces
{
    public interface ITaskDAO
    {
        Task GetById(int id);
        List<Task> GetImportant();
        List<Task> GetCompleted();
        List<Task> GetRecurring();
        List<Task> GetPlanned();
        Task Create(Task newTask);
        bool Delete(int id);
        Task Update(int id, Task modifiedTask);
    }
}
