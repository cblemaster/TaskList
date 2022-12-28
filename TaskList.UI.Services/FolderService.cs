using RestSharp;
using TaskList.UI.Services.Models;

namespace TaskList.UI.Services
{
    public class FolderService
    {
        //https://stackoverflow.com/questions/10226089/restsharp-simple-complete-example

        private readonly static string API_BASE_URL = "https://localhost:7086/";
        private readonly RestClient client = new(API_BASE_URL);

        public Folder GetById(int id)
        {
            RestRequest request = new("Folder/{id}", Method.Get);
            request.AddParameter("id", id);
            RestResponse<Folder> response = client.Execute<Folder>(request);
            return response.Data!;
        }

        public List<Folder> GetAll()
        {
            RestRequest request = new("Folder", Method.Get);
            RestResponse<List<Folder>> response = client.Execute<List<Folder>>(request);
            return response.Data!;
        }

        public Folder GetByFolderName(string folderName)
        {
            RestRequest request = new("Folder/{folderName}", Method.Get);
            request.AddParameter("folderName", folderName);
            RestResponse<Folder> response = client.Execute<Folder>(request);
            return response.Data!;
        }

        public Folder Create(NewFolder newFolder)
        {
            RestRequest request = new("Folder", Method.Post) { RequestFormat = DataFormat.Json };
            request.AddBody(newFolder);
            RestResponse<Folder> response = client.Execute<Folder>(request);
            return response.Data!;
        }

        public bool Delete(int id)
        {
            RestRequest request = new("Folder/{id}", Method.Delete);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            RestResponse<bool> response = client.Execute<bool>(request);
            return response.Data;
        }

        public Folder Update(int id, ModifiedFolder modifiedFolder)
        {
            RestRequest request = new("Folder/{id}", Method.Put) { RequestFormat = DataFormat.Json };
            request.AddParameter("id", id);
            request.AddBody(modifiedFolder);
            RestResponse<Folder> response = client.Execute<Folder>(request);
            return response.Data!;
        }
    }
}
