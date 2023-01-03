using System.ComponentModel;
using Task = TaskList.UI.Services.Models.Task;

namespace TaskList.UI.WPF.Tasks
{
    public class TaskDetailViewModel : INotifyPropertyChanged
    {
        public TaskDetailViewModel()
        {

        }

        private Task _task = new();

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public Task Task
        {
            get { return _task; }
            set
            {
                if (value != _task)
                {
                    _task = value;
                    PropertyChanged!(this, new PropertyChangedEventArgs(nameof(Task)));
                };
            }
        }
    }
}
