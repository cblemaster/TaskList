using RestSharp;
using Task = TaskList.UI.Services.Models.Task;

namespace TaskList.UI.Services
{
    public class TaskService
    {
        //https://stackoverflow.com/questions/10226089/restsharp-simple-complete-example

        private readonly static string API_BASE_URL = "https://localhost:7086/";
        private readonly RestClient client = new(API_BASE_URL);

        public Task GetById(int id)
        {
            RestRequest request = new("Task/{id}", Method.Get);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            RestResponse<Task> response = client.Execute<Task>(request);
            return response.Data!;
        }

        public List<Task> GetImportant()
        {
            RestRequest request = new("Task/Important", Method.Get);
            RestResponse<List<Task>> response = client.Execute<List<Task>>(request);
            return response.Data!;
        }

        public List<Task> GetCompleted()
        {
            RestRequest request = new("Task/Completed", Method.Get);
            RestResponse<List<Task>> response = client.Execute<List<Task>>(request);
            return response.Data!;
        }

        public List<Task> GetRecurring()
        {
            RestRequest request = new("Task/Recurring", Method.Get);
            RestResponse<List<Task>> response = client.Execute<List<Task>>(request);
            return response.Data!;
        }

        public List<Task> GetPlanned()
        {
            RestRequest request = new("Task/Planned", Method.Get);
            RestResponse<List<Task>> response = client.Execute<List<Task>>(request);
            return response.Data!;
        }

        public Task Create(Task newTask)
        {
            RestRequest request = new("Task", Method.Post) { RequestFormat = DataFormat.Json };
            request.AddBody(newTask);
            RestResponse<Task> response = client.Execute<Task>(request);
            return response.Data!;
        }

        public bool Delete(int id)
        {
            RestRequest request = new("Task/{id}", Method.Delete);
            request.AddParameter("id", id);
            RestResponse<bool> response = client.Execute<bool>(request);
            return response.Data;
        }

        public Task Update(Task modifiedTask)
        {
            RestRequest request = new("Task/{id}", Method.Put) { RequestFormat = DataFormat.Json };
            request.AddParameter("id", modifiedTask.Id);
            request.AddBody(modifiedTask);
            RestResponse<Task> response = client.Execute<Task>(request);
            return response.Data!;
        }
    }
}