namespace TaskList.Data.Models;

public partial class Task
{
    public int Id { get; set; }

    public string TaskName { get; set; } = null!;

    public DateTime? DueDate { get; set; }

    public int RecurrenceId { get; set; }

    public bool IsImportant { get; set; }

    public bool IsComplete { get; set; }

    public int FolderId { get; set; }

    public string? Note { get; set; }

    public Folder Folder { get; set; } = null!;
}
