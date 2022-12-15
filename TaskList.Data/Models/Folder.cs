namespace TaskList.Data.Models;

public partial class Folder
{
    public int Id { get; set; }

    public string FolderName { get; set; } = null!;

    public ICollection<Task> Tasks { get; set; } = new List<Task>();
}
