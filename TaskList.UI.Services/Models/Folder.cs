namespace TaskList.UI.Services.Models
{
    public class Folder
    {
        public int Id { get; set; }

        public string FolderName { get; set; } = null!;

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }

    public class NewFolder
    {
        public string FolderName { get; set; } = null!;
    }

    public class ModifiedFolder
    {
        public int Id { get; set; }

        public string FolderName { get; set; } = null!;
    }
}
