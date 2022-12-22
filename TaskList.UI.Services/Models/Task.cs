namespace TaskList.UI.Services.Models
{
    public class Task
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

        public Recurrence Recurrence => (Recurrence)this.RecurrenceId;
        public bool IsPlanned => this.DueDate.HasValue;
        public bool IsRecurring => this.Recurrence != Recurrence.None;
    }

    public class NewTask
    {
        public string TaskName { get; set; } = null!;

        public DateTime? DueDate { get; set; }

        public int RecurrenceId { get; set; }

        public bool IsImportant { get; set; }

        public bool IsComplete { get; set; }

        public int FolderId { get; set; }

        public string? Note { get; set; }

        public Folder Folder { get; set; } = null!;
    }

    public class ModifiedTask
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

    public enum Recurrence
    {
        None = 0,
        Daily = 1,
        Weekdays = 2,
        Weekly = 3,
        Monthly = 4,
        Annually = 5,
        Custom = 6,
    }
}
