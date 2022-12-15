using TaskList.Data.Validation;

namespace TaskList.Data.Models
{
    public partial class Task
    {
        Recurrence Recurrence => (Recurrence)this.RecurrenceId;
        bool IsPlanned => this.DueDate.HasValue;
        bool IsRecurring => this.Recurrence != Recurrence.None;
    }
}
